using Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Services
{
    public class BillingService : IDisposable
    {
        private readonly DBDataContext db = new DBDataContext();

        // Helper lấy tham số trực tiếp từ DB (Thay thế ParameterService)
        private decimal GetParam(string key, decimal defaultValue = 1.0m)
        {
            var p = db.ThamSos.FirstOrDefault(x => x.TenThamSo == key);
            return p != null ? p.GiaTri : defaultValue;
        }

        public decimal TinhTienPhieuThue(string maPhieuThue, DateTime ngayTra)
        {
            var phieuThue = db.PhieuThues.FirstOrDefault(pt => pt.MaPhieuThue == maPhieuThue);
            if (phieuThue == null) return 0;

            // 1. Tính số ngày (Đã fix lỗi < 1 ngày)
            int soNgayThue = (ngayTra.Date - phieuThue.NgayBatDauThue.Date).Days;
            if (soNgayThue < 1) soNgayThue = 1;

            decimal donGiaPhong = phieuThue.Phong.LoaiPhong.DonGia;

            // 2. Lấy danh sách khách
            var dsKhach = phieuThue.ChiTietPhieuThues.Select(ct => ct.KhachHang).ToList();
            int soKhach = dsKhach.Count;

            // --- SỬA LỖI LOGIC TÍNH TIỀN TẠI ĐÂY ---

            // A. Xử lý Phụ thu khách thứ 3
            // Nếu >= 3 khách, lấy tham số (ví dụ 0.25) CỘNG THÊM vào 1. 
            // Nếu không, hệ số là 1.
            decimal tyLePhuThu = (soKhach >= 3) ? GetParam("PhuThu_KhachThu3", 0.0m) : 0.0m;
            decimal heSoPhuThu = 1.0m + tyLePhuThu; // Sẽ thành 1.25 nếu có khách thứ 3

            // B. Xử lý Khách nước ngoài
            // Logic này giữ nguyên vì DB lưu 1.5 (nghĩa là đã bao gồm gốc)
            bool coKhachNuocNgoai = dsKhach.Any(k => k.MaLoaiKhach == "NN");
            decimal heSoNuocNgoai = 1.0m;

            if (coKhachNuocNgoai)
            {
                // Lấy 1.5 từ DB. Nếu lỗi thì mặc định là 1.0
                heSoNuocNgoai = GetParam("HeSo_KhachNuocNgoai", 1.0m);
            }

            // 3. Công thức tính cuối cùng
            // Tiền = (Số ngày * Đơn giá) * Hệ số phụ thu * Hệ số nước ngoài
            return (soNgayThue * donGiaPhong) * heSoPhuThu * heSoNuocNgoai;
        }

        public string LapHoaDon(DateTime ngayLap, params string[] dsMaPhieuThue)
        {
            if (dsMaPhieuThue == null || dsMaPhieuThue.Length == 0) return null;

            try
            {
                // FIX 2: Validate trước khi xử lý
                foreach (var ma in dsMaPhieuThue)
                {
                    // Kiểm tra phiếu này đã có trong bất kỳ hóa đơn nào chưa
                    bool daThanhToan = db.ChiTietHoaDons.Any(ct => ct.MaPhieuThue == ma);
                    if (daThanhToan)
                    {
                        MessageBox.Show($"Phiếu thuê {ma} đã được thanh toán trước đó!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null; // Dừng lại ngay
                    }
                }

                string maHoaDon = GenerateMaHoaDon();
                decimal tongTien = 0;

                var hoaDon = new HoaDon
                {
                    MaHoaDon = maHoaDon,
                    NgayLap = ngayLap.Date,
                    TriGia = 0
                };
                db.HoaDons.InsertOnSubmit(hoaDon);

                foreach (var maPhieuThue in dsMaPhieuThue)
                {
                    var pt = db.PhieuThues.FirstOrDefault(p => p.MaPhieuThue == maPhieuThue);
                    if (pt == null) continue;

                    // FIX 3: Tính số ngày chính xác để lưu vào DB (tránh lỗi CHECK > 0)
                    int soNgay = (ngayLap.Date - pt.NgayBatDauThue.Date).Days;
                    if (soNgay < 1) soNgay = 1;

                    decimal thanhTien = TinhTienPhieuThue(maPhieuThue, ngayLap);

                    var chiTietHD = new ChiTietHoaDon
                    {
                        MaHoaDon = maHoaDon,
                        MaPhieuThue = maPhieuThue,
                        SoNgayThue = soNgay, // Dùng biến đã fix logic < 1
                        ThanhTien = thanhTien
                    };

                    db.ChiTietHoaDons.InsertOnSubmit(chiTietHD);
                    tongTien += thanhTien;

                    // Cập nhật trạng thái phòng
                    pt.Phong.TinhTrang = "Trống";
                }

                hoaDon.TriGia = tongTien;
                db.SubmitChanges();

                return maHoaDon;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lập hóa đơn:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private string GenerateMaHoaDon()
        {
            try
            {
                // 1. Lấy tất cả mã hóa đơn hiện có về để xử lý
                // (Dùng ToList để xử lý trên RAM, tránh lỗi so sánh chuỗi của SQL)
                var existingIds = db.HoaDons.Select(h => h.MaHoaDon).ToList();

                int maxVal = 0;

                // 2. Tìm số lớn nhất hiện có (Parse ra số để so sánh chính xác)
                foreach (var id in existingIds)
                {
                    if (id.Length > 2 && int.TryParse(id.Substring(2), out int num))
                    {
                        if (num > maxVal) maxVal = num;
                    }
                }

                // 3. Tạo mã mới: Bắt đầu từ max + 1
                int nextVal = maxVal + 1;
                string newCode = "HD" + nextVal.ToString("00000000");

                // 4. Vòng lặp kiểm tra an toàn (Double check)
                // Nếu mã này vẫn trùng (do lý do nào đó), thì cứ tăng tiếp đến khi hết trùng
                while (existingIds.Contains(newCode))
                {
                    nextVal++;
                    newCode = "HD" + nextVal.ToString("00000000");
                }

                return newCode;
            }
            catch
            {
                // Fallback nếu có lỗi nghiêm trọng, dùng thời gian thực để không bao giờ trùng
                return "HD" + DateTime.Now.ToString("yyMMddHHmmss");
            }
        }

        public Data.BillDetailItemViewModel GetBillPreview(string maPhieu)
        {
            var pt = db.PhieuThues.FirstOrDefault(p => p.MaPhieuThue == maPhieu);
            if (pt == null) return null;

            var khachChinh = pt.ChiTietPhieuThues.FirstOrDefault(ct => ct.VaiTro == "Chinh")?.KhachHang;
            decimal thanhTien = TinhTienPhieuThue(maPhieu, DateTime.Now);

            int soNgay = (DateTime.Now.Date - pt.NgayBatDauThue.Date).Days;
            if (soNgay < 1) soNgay = 1;

            return new Data.BillDetailItemViewModel
            {
                MaPhieuThue = pt.MaPhieuThue,
                MaPhong = pt.MaPhong,
                NgayThue = pt.NgayBatDauThue,
                NgayTra = DateTime.Now,
                SoNgay = soNgay,
                DonGia = pt.Phong.LoaiPhong.DonGia,
                ThanhTien = thanhTien,
                TenKhach = khachChinh != null ? khachChinh.HoTen : "N/A",
                DiaChi = khachChinh != null ? khachChinh.DiaChi : ""
            };
        }

        public void Dispose() => db?.Dispose();
    }
}