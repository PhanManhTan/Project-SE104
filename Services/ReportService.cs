using System;
using System.Collections.Generic;
using System.Linq;
using Data; // Nhớ dùng namespace Data để gọi DBDataContext

namespace Services
{
    
    public class ReportData
    {
        public string TenLoaiPhong { get; set; }
        public decimal DoanhThu { get; set; }
        public float TyLe { get; set; }
    }

    // ==========================================
    // 2. CLASS XỬ LÝ LOGIC CHÍNH
    // ==========================================
    public class ReportService
    {
        private readonly DBDataContext db = new DBDataContext();

        public List<ReportData> GetReportByMonth(int month, int year)
        {
            // Bước 1: Lấy dữ liệu thô (Join các bảng)
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

            // Bước 2: Tính tổng doanh thu toàn tháng (để tính %)
            decimal tongDoanhThu = dataRaw.Any() ? dataRaw.Sum(x => x.ThanhTien) : 0;

            // Bước 3: Gom nhóm và tính toán ra danh sách báo cáo
            var result = dataRaw
                .GroupBy(x => x.TenLoaiPhong)
                .Select(g => new ReportData // Sử dụng class ReportData vừa khai báo ở trên
                {
                    TenLoaiPhong = g.Key,
                    DoanhThu = g.Sum(x => x.ThanhTien),
                    // Nếu tổng > 0 thì chia lấy %, ngược lại thì bằng 0
                    TyLe = tongDoanhThu > 0 ? (float)((g.Sum(x => x.ThanhTien) / tongDoanhThu) * 100) : 0
                })
                .OrderByDescending(x => x.DoanhThu) // Sắp xếp tiền nhiều nhất lên đầu
                .ToList();

            return result;
        }
    }
}