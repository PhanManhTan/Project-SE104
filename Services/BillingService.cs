// Services/BillingService.cs
using Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Services
{
    public class BillingService : IDisposable
    {
        private readonly DBDataContext db = new DBDataContext();
        private readonly ParameterService param = new ParameterService();

        /// <summary>
        /// Tính tiền cho 1 phiếu thuê (dùng khi lập hóa đơn)
        /// </summary>
        public decimal TinhTienPhieuThue(string maPhieuThue, DateTime ngayTra)
        {
            var phieuThue = db.PhieuThues
                .FirstOrDefault(pt => pt.MaPhieuThue == maPhieuThue);

            if (phieuThue == null) return 0;

            // 1. Số ngày thuê (tối thiểu 1 ngày)
            int soNgayThue = (ngayTra.Date - phieuThue.NgayBatDauThue.Date).Days;
            if (soNgayThue < 1) soNgayThue = 1;

            // 2. Đơn giá phòng
            decimal donGiaPhong = phieuThue.Phong.LoaiPhong.DonGia;

            // 3. Lấy danh sách khách trong phiếu thuê này (qua ChiTietPhieuThue)
            var dsKhachTrongPhieu = phieuThue.ChiTietPhieuThues
                .Select(ct => ct.KhachHang)
                .ToList();

            int soKhach = dsKhachTrongPhieu.Count;

            // 4. Hệ số phụ thu khách thứ 3
            decimal heSoPhuThuKhachThu3 = (soKhach >= 3) ? param.GetDecimal("PhuThu_KhachThu3") : 1.0m;

            // 5. Hệ số khách nước ngoài (nếu có ít nhất 1 khách nước ngoài)
            decimal heSoNuocNgoai = dsKhachTrongPhieu.Any(k => k.MaLoaiKhach == "NN")
                ? param.GetDecimal("HeSo_KhachNuocNgoai")
                : 1.0m;

            // 6. Thành tiền = số ngày × đơn giá × hệ số phụ thu × hệ số nước ngoài
            return soNgayThue * donGiaPhong * heSoPhuThuKhachThu3 * heSoNuocNgoai;
        }

        /// <summary>
        /// Lập hóa đơn cho nhiều phiếu thuê (check-out)
        /// </summary>
        public string LapHoaDon(DateTime ngayLap, params string[] dsMaPhieuThue)
        {
            if (dsMaPhieuThue == null || dsMaPhieuThue.Length == 0)
                return null;

            try
            {
                string maHoaDon = GenerateMaHoaDon();
                decimal tongTien = 0;

                var hoaDon = new HoaDon
                {
                    MaHoaDon = maHoaDon,
                    NgayLap = ngayLap.Date,
                    TriGia = 0 // sẽ cập nhật sau
                };

                db.HoaDons.InsertOnSubmit(hoaDon);

                foreach (var maPhieuThue in dsMaPhieuThue)
                {
                    var pt = db.PhieuThues.FirstOrDefault(p => p.MaPhieuThue == maPhieuThue);
                    if (pt == null) continue;

                    decimal thanhTien = TinhTienPhieuThue(maPhieuThue, ngayLap);

                    var chiTietHD = new ChiTietHoaDon
                    {
                        MaHoaDon = maHoaDon,
                        MaPhieuThue = maPhieuThue,
                        SoNgayThue = (ngayLap.Date - pt.NgayBatDauThue.Date).Days,
                        ThanhTien = thanhTien
                    };

                    db.ChiTietHoaDons.InsertOnSubmit(chiTietHD);
                    tongTien += thanhTien;

                    // Cập nhật trạng thái phòng về "Trống"
                    pt.Phong.TinhTrang = "Trống";
                }

                // Cập nhật tổng tiền hóa đơn
                hoaDon.TriGia = tongTien;

                db.SubmitChanges();

                return maHoaDon;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lập hóa đơn:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Tạo mã hóa đơn tự động: HD00000001, HD00000002...
        /// </summary>
        private string GenerateMaHoaDon()
        {
            var last = db.HoaDons
                .OrderByDescending(h => h.MaHoaDon)
                .Select(h => h.MaHoaDon)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(last) || !int.TryParse(last.Substring(2), out int num))
                return "HD00000001";

            return "HD" + (num + 1).ToString("00000000");
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}