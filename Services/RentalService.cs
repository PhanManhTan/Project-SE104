// Services/RentalService.cs
using System;
using System.Linq;
using Data;

namespace Services
{
    public class RentalService
    {
        private readonly DBDataContext db = new DBDataContext();

        // Kiểm tra phòng còn trống không (dựa vào ngày)
        public bool IsRoomAvailable(string maPhong, DateTime ngayThue)
        {
            return !db.PhieuThues.Any(pt =>
                pt.MaPhong == maPhong &&
                pt.NgayBatDauThue <= ngayThue &&
                (pt.ChiTietHoaDons.Any() == false)); // chưa thanh toán = vẫn đang thuê
        }

        // Lập phiếu thuê cho nhiều khách (tối đa 3)
        public bool CreateRental(string maPhong, DateTime ngayThue, params string[] dsMaKhach)
        {
            if (dsMaKhach.Length == 0 || dsMaKhach.Length > 3) return false;
            if (!IsRoomAvailable(maPhong, ngayThue)) return false;

            foreach (var maKhach in dsMaKhach)
            {
                var pt = new PhieuThue
                {
                    MaPhieuThue = GenerateMaPhieuThue(),
                    MaPhong = maPhong,
                    MaKhach = maKhach,
                    NgayBatDauThue = ngayThue,
                    VaiTro = dsMaKhach.Length == 1 ? "Chính" : "Phụ"
                };
                db.PhieuThues.InsertOnSubmit(pt);
            }

            // Cập nhật trạng thái phòng
            var phong = db.Phongs.First(p => p.MaPhong == maPhong);
            phong.TinhTrang = "Da thue";

            db.SubmitChanges();
            return true;
        }

        private string GenerateMaPhieuThue()
        {
            var last = db.PhieuThues.OrderByDescending(p => p.MaPhieuThue)
                                   .FirstOrDefault()?.MaPhieuThue ?? "PT00000000";
            int num = int.Parse(last.Substring(2)) + 1;
            return "PT" + num.ToString("00000000");
        }
    }
}