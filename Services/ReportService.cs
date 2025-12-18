using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Services
{
    
    public class ReportData
    {
        public string TenLoaiPhong { get; set; }
        public decimal DoanhThu { get; set; }
        public float TyLe { get; set; }
    }

    public class ReportService
    {
        private readonly DBDataContext db = new DBDataContext();

        public List<ReportData> GetReportByMonth(int month, int year)
        {
            var dataRaw = from hd in db.HoaDons
                          join cthd in db.ChiTietHoaDons on hd.MaHoaDon equals cthd.MaHoaDon
                          join pt in db.PhieuThues on cthd.MaPhieuThue equals pt.MaPhieuThue
                          join p in db.Phongs on pt.MaPhong equals p.MaPhong
                          join lp in db.LoaiPhongs on p.MaLoaiPhong equals lp.MaLoaiPhong
                          where hd.NgayLap.Month == month && hd.NgayLap.Year == year
                          select new
                          {
                              lp.TenLoaiPhong,
                              cthd.ThanhTien
                          };

            decimal tongDoanhThu = dataRaw.Any() ? dataRaw.Sum(x => x.ThanhTien) : 0;

            var result = dataRaw
                .GroupBy(x => x.TenLoaiPhong)
                .Select(g => new ReportData 
                {
                    TenLoaiPhong = g.Key,
                    DoanhThu = g.Sum(x => x.ThanhTien),

                    TyLe = tongDoanhThu > 0 ? (float)((g.Sum(x => x.ThanhTien) / tongDoanhThu) * 100) : 0
                })
                .OrderByDescending(x => x.DoanhThu) 
                .ToList();

            return result;
        }
    }
}