using Data;
using System;
using System.Linq;

namespace Services
{
    public class BillingService
    {
        public BillingService()
        {
           
        }

        public decimal TinhTienPhieuThue(string maPhieuThue, DateTime ngayTra)
        {
            using (var db = new DBDataContext())
            using (var paramService = new ParameterService())
            {
                var query = from pt in db.PhieuThues
                            join p in db.Phongs on pt.MaPhong equals p.MaPhong
                            join lp in db.LoaiPhongs on p.MaLoaiPhong equals lp.MaLoaiPhong
                            where pt.MaPhieuThue == maPhieuThue
                            select new
                            {
                                pt.NgayBatDauThue,
                                DonGia = lp.DonGia,
                                pt.ChiTietPhieuThues
                            };

                var result = query.FirstOrDefault();
                if (result == null) return 0m;

                int soNgayThue = (ngayTra.Date - result.NgayBatDauThue.Date).Days;
                if (soNgayThue < 1) soNgayThue = 1;

                decimal donGiaPhong = result.DonGia;
                if (donGiaPhong == 0m)
                    throw new Exception($"Không tìm thấy đơn giá cho phiếu thuê {maPhieuThue}");


                var dsKhach = result.ChiTietPhieuThues.Select(ct => ct.KhachHang).ToList();
                int soKhach = dsKhach.Count;

                int soKhachToiDa = (int)paramService.GetThamSo(ParameterService.KEY_SO_KHACH_TOI_DA);
                decimal tyLePhuThu = paramService.GetThamSo(ParameterService.KEY_TY_LE_PHU_THU); 
                decimal heSoNuocNgoai = paramService.GetThamSo(ParameterService.KEY_HE_SO_NUOC_NGOAI);

                bool coKhachNuocNgoai = dsKhach.Any(k =>
                    k.LoaiKhach != null &&
                    string.Equals(k.LoaiKhach.TenLoaiKhach.Trim(), "Nước ngoài", StringComparison.OrdinalIgnoreCase));

                decimal hsPhuThu = (soKhach == soKhachToiDa) ? tyLePhuThu : 0.0m;
                decimal hsNuocNgoai = coKhachNuocNgoai ? heSoNuocNgoai : 0.0m;

                decimal tongHeSo = 1.0m + hsPhuThu + hsNuocNgoai;

                return soNgayThue * donGiaPhong * tongHeSo;
            }
        }

        public string LapHoaDon(DateTime ngayLap, params string[] dsMaPhieuThue)
        {
            if (dsMaPhieuThue == null || dsMaPhieuThue.Length == 0) return null;

            using (var db = new DBDataContext())
            {
                try
                {

                    foreach (var ma in dsMaPhieuThue)
                    {
                        if (db.ChiTietHoaDons.Any(ct => ct.MaPhieuThue == ma))
                            throw new InvalidOperationException($"Phiếu thuê {ma} đã được thanh toán!");
                    }

                    string maHoaDon = GenerateMaHoaDon(db);
                    decimal tongTien = 0m;

                    var hoaDon = new HoaDon
                    {
                        MaHoaDon = maHoaDon,
                        NgayLap = ngayLap.Date,
                        TriGia = 0m
                    };
                    db.HoaDons.InsertOnSubmit(hoaDon);

                    foreach (var maPhieuThue in dsMaPhieuThue)
                    {
                        var pt = db.PhieuThues.FirstOrDefault(p => p.MaPhieuThue == maPhieuThue);
                        if (pt == null) continue;

                        int soNgay = (ngayLap.Date - pt.NgayBatDauThue.Date).Days;
                        if (soNgay < 1) soNgay = 1;

                        decimal thanhTien = TinhTienPhieuThue(maPhieuThue, ngayLap);

                        var chiTiet = new ChiTietHoaDon
                        {
                            MaHoaDon = maHoaDon,
                            MaPhieuThue = maPhieuThue,
                            SoNgayThue = soNgay,
                            ThanhTien = thanhTien
                        };
                        db.ChiTietHoaDons.InsertOnSubmit(chiTiet);
                        tongTien += thanhTien;

                        pt.Phong.TinhTrang = "Trống";
                    }

                    hoaDon.TriGia = tongTien;
                    db.SubmitChanges();

                    return maHoaDon;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi lập hóa đơn: {ex.Message}");
                    throw;
                }
            }
        }

        private string GenerateMaHoaDon(DBDataContext db)
        {
            var maxNum = db.HoaDons
                .Select(h => h.MaHoaDon)
                .AsEnumerable()
                .Where(id => id.StartsWith("HD") && id.Length > 2)
                .Select(id => int.TryParse(id.Substring(2), out int n) ? n : 0)
                .DefaultIfEmpty(0)
                .Max();

            return "HD" + (maxNum + 1).ToString("000");
        }

        public Data.BillDetailItemViewModel GetBillPreview(string maPhieu)
        {
            using (var db = new DBDataContext())
            {
                var query = from pt in db.PhieuThues
                            join p in db.Phongs on pt.MaPhong equals p.MaPhong
                            join lp in db.LoaiPhongs on p.MaLoaiPhong equals lp.MaLoaiPhong
                            where pt.MaPhieuThue == maPhieu
                            select new
                            {
                                pt.MaPhieuThue,
                                pt.MaPhong,
                                pt.NgayBatDauThue,
                                DonGia = lp.DonGia,
                                pt.ChiTietPhieuThues
                            };

                var result = query.FirstOrDefault();
                if (result == null) return null;

                var khachChinh = result.ChiTietPhieuThues
                    .FirstOrDefault(ct => ct.VaiTro == "Chinh")?.KhachHang;

                int soNgay = (DateTime.Today.Date - result.NgayBatDauThue.Date).Days;
                if (soNgay < 1) soNgay = 1;

                decimal thanhTien = TinhTienPhieuThue(maPhieu, DateTime.Today);

                return new Data.BillDetailItemViewModel
                {
                    MaPhieuThue = result.MaPhieuThue,
                    MaPhong = result.MaPhong,
                    NgayThue = result.NgayBatDauThue,
                    NgayTra = DateTime.Today,
                    SoNgay = soNgay,
                    DonGia = result.DonGia,
                    ThanhTien = thanhTien,
                    TenKhach = khachChinh?.HoTen ?? "N/A",
                    DiaChi = khachChinh?.DiaChi ?? ""
                };
            }
        }
    }
}