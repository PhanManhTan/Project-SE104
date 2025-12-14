// Services/RentalService.cs
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Services
{
    public class RentalService : IDisposable
    {
        private readonly DBDataContext db = new DBDataContext();

        public void Dispose()
        {
            db?.Dispose();
        }

        /// <summary>
        /// Lấy danh sách phiếu thuê (có hỗ trợ tìm kiếm theo tên khách, mã phiếu, mã phòng)
        /// Được dùng trực tiếp trong RentalManager
        /// </summary>
        public List<RentalViewModel> SearchRentals(string searchKeyword = null)
        {
            var query = from p in db.PhieuThues
                        join ct in db.ChiTietPhieuThues on p.MaPhieuThue equals ct.MaPhieuThue
                        join k in db.KhachHangs on ct.MaKhach equals k.MaKhach
                        where ct.VaiTro == "Chinh"
                        select new
                        {
                            p.MaPhieuThue,
                            p.MaPhong,
                            p.NgayBatDauThue,
                            HoTen = k.HoTen,
                            IsPaid = db.ChiTietHoaDons.Any(h => h.MaPhieuThue == p.MaPhieuThue)
                        };

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                string keyword = searchKeyword.Trim();
                query = query.Where(x =>
                    x.HoTen.Contains(keyword) ||
                    x.MaPhieuThue.Contains(keyword) ||
                    x.MaPhong.Contains(keyword));
            }

            return query.AsEnumerable()
                .Select(item => new RentalViewModel
                {
                    MaPhieuThue = item.MaPhieuThue,
                    MaPhong = item.MaPhong,
                    NgayBatDauThue = item.NgayBatDauThue,
                    TenKhachChinh = item.HoTen,
                    TinhTrang = item.IsPaid ? "Đã thanh toán" : "Đang thuê"
                })
                .OrderBy(x => x.TinhTrang == "Đã thanh toán")           // Đang thuê lên trên
                .ThenByDescending(x => x.NgayBatDauThue)               // Ngày mới nhất trước
                .ThenByDescending(x => x.MaPhieuThue)                  // Mã phiếu mới hơn
                .ToList();
        }

        // Để tương thích với code cũ nếu có nơi gọi GetAllRentals
        public List<RentalViewModel> GetAllRentals(string searchKeyword = null) => SearchRentals(searchKeyword);

        /// <summary>
        /// Lấy chi tiết tính tiền cho một phiếu thuê (dùng cho hiển thị và thanh toán)
        /// </summary>
        public BillDetailItemViewModel GetBillDetails(string maPhieu)
        {
            var data = (from p in db.PhieuThues
                        join phong in db.Phongs on p.MaPhong equals phong.MaPhong
                        join lp in db.LoaiPhongs on phong.MaLoaiPhong equals lp.MaLoaiPhong
                        join ct in db.ChiTietPhieuThues on p.MaPhieuThue equals ct.MaPhieuThue
                        join k in db.KhachHangs on ct.MaKhach equals k.MaKhach
                        where p.MaPhieuThue == maPhieu && ct.VaiTro == "Chinh"
                        select new
                        {
                            p.MaPhieuThue,
                            p.MaPhong,
                            p.NgayBatDauThue,
                            GiaPhong = lp.DonGia,
                            TenKhach = k.HoTen ?? "Không rõ",
                            DiaChi = k.DiaChi ?? ""
                        }).FirstOrDefault();

            if (data == null) return null;

            DateTime ngayTra = DateTime.Now.Date;
            int soNgay = (ngayTra - data.NgayBatDauThue.Date).Days;
            if (soNgay < 1) soNgay = 1; // Tối thiểu 1 ngày

            decimal thanhTien = soNgay * data.GiaPhong;

            return new BillDetailItemViewModel
            {
                MaPhieuThue = data.MaPhieuThue,
                MaPhong = data.MaPhong,
                NgayThue = data.NgayBatDauThue,
                NgayTra = ngayTra,
                SoNgay = soNgay,
                DonGia = data.GiaPhong,
                ThanhTien = thanhTien,
                TenKhach = data.TenKhach,
                DiaChi = data.DiaChi
            };
        }

        /// <summary>
        /// Thanh toán gộp nhiều phiếu thuê cùng lúc (dùng trong RentalManager)
        /// </summary>
        public bool PayMultipleRentals(List<string> maPhieuList, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (maPhieuList == null || maPhieuList.Count == 0)
            {
                errorMessage = "Danh sách phiếu thuê trống.";
                return false;
            }

            try
            {
                // Tạo mã hóa đơn duy nhất
                string maHD = "HD" + DateTime.Now.ToString("yyMMddHHmmss");
                decimal tongTriGia = 0;
                var chiTietHDList = new List<ChiTietHoaDon>();

                foreach (string maPhieu in maPhieuList)
                {
                    // Kiểm tra phiếu đã thanh toán chưa
                    if (db.ChiTietHoaDons.Any(h => h.MaPhieuThue == maPhieu))
                    {
                        errorMessage = $"Phiếu {maPhieu} đã được thanh toán trước đó!";
                        return false;
                    }

                    var billDetail = GetBillDetails(maPhieu);
                    if (billDetail == null)
                    {
                        errorMessage = $"Không tìm thấy thông tin phiếu {maPhieu}";
                        return false;
                    }

                    tongTriGia += billDetail.ThanhTien;

                    chiTietHDList.Add(new ChiTietHoaDon
                    {
                        MaHoaDon = maHD,
                        MaPhieuThue = maPhieu,
                        SoNgayThue = billDetail.SoNgay,
                        ThanhTien = billDetail.ThanhTien
                    });

                    // Cập nhật trạng thái phòng thành "Trống"
                    var phieu = db.PhieuThues.FirstOrDefault(p => p.MaPhieuThue == maPhieu);
                    if (phieu != null)
                    {
                        var phong = db.Phongs.FirstOrDefault(p => p.MaPhong == phieu.MaPhong);
                        if (phong != null)
                            phong.TinhTrang = "Trống";
                    }
                }

                // Lưu hóa đơn chính
                var hoaDon = new HoaDon
                {
                    MaHoaDon = maHD,
                    NgayLap = DateTime.Now,
                    TriGia = tongTriGia
                };

                db.HoaDons.InsertOnSubmit(hoaDon);
                db.ChiTietHoaDons.InsertAllOnSubmit(chiTietHDList);
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi thanh toán: " + ex.Message;
                if (ex.InnerException != null)
                    errorMessage += "\nChi tiết: " + ex.InnerException.Message;
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra phòng có trống vào ngày thuê không
        /// </summary>
        public bool IsRoomAvailable(string maPhong, DateTime ngayThue)
        {
            return !db.PhieuThues.Any(pt =>
                pt.MaPhong == maPhong &&
                pt.NgayBatDauThue.Date <= ngayThue.Date &&
                pt.ChiTietHoaDons.Count == 0); // chưa có chi tiết hóa đơn → đang thuê
        }

        /// <summary>
        /// Tạo phiếu thuê mới (1-3 khách)
        /// </summary>
        public bool CreateRental(
            string maPhong,
            DateTime ngayThue,
            Dictionary<string, string> dsKhach
        )
        {
            // 1️⃣ Validate số lượng khách
            if (dsKhach == null || dsKhach.Count == 0 || dsKhach.Count > 3)
            {
                MessageBox.Show(
                    "Phải chọn từ 1 đến 3 khách cho một phòng!",
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            // 2️⃣ Phải có đúng 1 khách Chính
            if (dsKhach.Count(k => k.Value == "Chinh") != 1)
            {
                MessageBox.Show(
                    "Phải có đúng 1 khách là Chinh!",
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            // 3️⃣ Kiểm tra phòng trống
            if (!IsRoomAvailable(maPhong, ngayThue))
            {
                MessageBox.Show(
                    $"Phòng {maPhong} đã được thuê vào ngày {ngayThue:dd/MM/yyyy}!",
                    "Phòng không khả dụng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop
                );
                return false;
            }

            try
            {
                // 4️⃣ Tạo mã phiếu thuê
                string maPhieuThue = GenerateMaPhieuThue();

                // 5️⃣ Tạo phiếu thuê
                var phieuThue = new PhieuThue
                {
                    MaPhieuThue = maPhieuThue,
                    MaPhong = maPhong,
                    NgayBatDauThue = ngayThue.Date
                };
                db.PhieuThues.InsertOnSubmit(phieuThue);

                // 6️⃣ Thêm chi tiết khách
                foreach (var item in dsKhach)
                {
                    var chiTiet = new ChiTietPhieuThue
                    {
                        MaPhieuThue = maPhieuThue,
                        MaKhach = item.Key,
                        VaiTro = item.Value
                    };
                    db.ChiTietPhieuThues.InsertOnSubmit(chiTiet);
                }

                // 7️⃣ Cập nhật trạng thái phòng
                var phong = db.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
                if (phong != null)
                {
                    phong.TinhTrang = "Đã thuê";
                }

                db.SubmitChanges();

                MessageBox.Show(
                    "Lập phiếu thuê thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi lập phiếu thuê:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        /// <summary>
        /// Tạo mã phiếu thuê tự động PT00000001, PT00000002,...
        /// </summary>
        private string GenerateMaPhieuThue()
        {
            var last = db.PhieuThues
                .OrderByDescending(p => p.MaPhieuThue)
                .Select(p => p.MaPhieuThue)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(last)) return "PT001";

            if (last.StartsWith("PT") && int.TryParse(last.Substring(2), out int num))
                return "PT" + (num + 1).ToString("000");

            return "PT001";
        }

        /// <summary>
        /// Lấy danh sách khách thuộc một phiếu thuê (dùng cho form chi tiết)
        /// </summary>
        public List<CustomerViewModel> GetCustomersByRental(string maPhieu)
        {
            if (string.IsNullOrEmpty(maPhieu)) return new List<CustomerViewModel>();

            var query = from ct in db.ChiTietPhieuThues
                        join k in db.KhachHangs on ct.MaKhach equals k.MaKhach
                        join lk in db.LoaiKhaches on k.MaLoaiKhach equals lk.MaLoaiKhach
                        where ct.MaPhieuThue == maPhieu.Trim()
                        select new CustomerViewModel
                        {
                            MaKhach = k.MaKhach,
                            HoTen = k.HoTen,
                            CMND = k.CMND,
                            DiaChi = k.DiaChi,
                            MaLoaiKhach = k.MaLoaiKhach,
                            TenLoaiKhach = lk.TenLoaiKhach
                        };

            return query.ToList();
        }
    }
}