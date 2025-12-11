// Services/BillingService.cs
using System;
using System.Linq;
using Data;

namespace Services
{
    public class BillingService
    {
        private readonly DBDataContext db = new DBDataContext();
        private readonly ParameterService param = new ParameterService();

        // Tính tiền 1 phiếu thuê
        public decimal TinhTienPhieuThue(string maPhieuThue, DateTime ngayTra)
        {
            var pt = db.PhieuThues.FirstOrDefault(p => p.MaPhieuThue == maPhieuThue);
            if (pt == null) return 0;

            int soNgay = (ngayTra.Date - pt.NgayBatDauThue.Date).Days;
            if (soNgay < 1) soNgay = 1;

            decimal donGia = pt.Phong.LoaiPhong.DonGia;

            // Đếm số khách trong phòng (cùng MaPhong + cùng khoảng thời gian)
            var dsPhieuCungPhong = db.PhieuThues
                .Where(p => p.MaPhong == pt.MaPhong &&
                            p.NgayBatDauThue == pt.NgayBatDauThue)
                .ToList();

            decimal heSoKhach = dsPhieuCungPhong.Count >= 3 ? param.HeSoKhachThu3 : 1.0m;
            decimal heSoNuocNgoai = dsPhieuCungPhong.Any(p => p.KhachHang.MaLoaiKhach == "NN")
                                    ? param.HeSoNuocNgoai : 1.0m;

            return soNgay * donGia * heSoKhach * heSoNuocNgoai;
        }

        // Lập hóa đơn (nhiều phiếu thuê)
        public string LapHoaDon(DateTime ngayLap, params string[] dsMaPhieuThue)
        {
            var maHD = GenerateMaHoaDon();
            decimal tongTien = 0;

            var hd = new HoaDon { MaHoaDon = maHD, NgayLap = ngayLap, TriGia = 0 };
            db.HoaDons.InsertOnSubmit(hd);

            foreach (var maPT in dsMaPhieuThue)
            {
                decimal tien = TinhTienPhieuThue(maPT, ngayLap);
                tongTien += tien;

                var ct = new ChiTietHoaDon
                {
                    MaHoaDon = maHD,
                    MaPhieuThue = maPT,
                    SoNgayThue = (ngayLap.Date - db.PhieuThues.First(p => p.MaPhieuThue == maPT).NgayBatDauThue.Date).Days,
                    ThanhTien = tien
                };
                db.ChiTietHoaDons.InsertOnSubmit(ct);

                // Cập nhật phòng về Trống
                var phong = db.Phongs.First(p => p.MaPhong == db.PhieuThues.First(x => x.MaPhieuThue == maPT).MaPhong);
                phong.TinhTrang = "Trong";
            }

            hd.TriGia = tongTien;
            db.SubmitChanges();
            return maHD;
        }

        private string GenerateMaHoaDon()
        {
            var last = db.HoaDons.OrderByDescending(h => h.MaHoaDon).FirstOrDefault()?.MaHoaDon ?? "HD00000000";
            int num = int.Parse(last.Substring(2)) + 1;
            return "HD" + num.ToString("00000000");
        }
    }
}