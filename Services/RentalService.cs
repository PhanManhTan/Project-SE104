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
                .OrderBy(x => x.TinhTrang == "Đã thanh toán")
                .ThenByDescending(x => x.NgayBatDauThue)
                .ThenByDescending(x => x.MaPhieuThue)
                .ToList();
        }

        public bool IsRoomAvailable(string maPhong, DateTime ngayThue)
        {
            return !db.PhieuThues.Any(pt =>
                pt.MaPhong == maPhong &&
                pt.NgayBatDauThue.Date <= ngayThue.Date &&
                pt.ChiTietHoaDons.Count == 0);
        }

        public bool CreateRental(
            string maPhong,
            DateTime ngayThue,
            Dictionary<string, string> dsKhach
        )
        {
            if (dsKhach == null || dsKhach.Count == 0)
            {
                MessageBox.Show(
                    "Phải chọn từ 1 đến 3 khách cho một phòng!",
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

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
                string maPhieuThue = GenerateMaPhieuThue();

                var phieuThue = new PhieuThue
                {
                    MaPhieuThue = maPhieuThue,
                    MaPhong = maPhong,
                    NgayBatDauThue = ngayThue.Date
                };
                db.PhieuThues.InsertOnSubmit(phieuThue);

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

                var phong = db.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
                if (phong != null)
                {
                    phong.TinhTrang = "Đã thuê";
                }

                db.SubmitChanges();

                // ĐÃ XÓA MessageBox ở đây → tránh hiện 2 lần
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