// Services/RentalService.cs
using Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Services
{
    public class RentalService : IDisposable
    {
        private readonly DBDataContext db = new DBDataContext();

        /// <summary>
        /// Kiểm tra phòng có trống vào ngày thuê không 
        /// (không có phiếu thuê nào đang hoạt động - chưa thanh toán)
        /// </summary>
        public bool IsRoomAvailable(string maPhong, DateTime ngayThue)
        {
            return !db.PhieuThues.Any(pt =>
                pt.MaPhong == maPhong &&
                pt.NgayBatDauThue.Date <= ngayThue.Date &&
                pt.ChiTietHoaDons.Count == 0); // chưa xuất hóa đơn → đang thuê
        }

        /// <summary>
        /// Lập phiếu thuê cho 1 đến 3 khách (1 phiếu + nhiều chi tiết khách)
        /// </summary>
        public bool CreateRental(string maPhong, DateTime ngayThue, params string[] dsMaKhach)
        {
            // Validate số lượng khách
            if (dsMaKhach == null || dsMaKhach.Length == 0 || dsMaKhach.Length > 3)
            {
                MessageBox.Show("Phải chọn từ 1 đến 3 khách cho một phòng!", "Lỗi dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra phòng trống
            if (!IsRoomAvailable(maPhong, ngayThue))
            {
                MessageBox.Show($"Phòng {maPhong} đã được thuê hoặc đang sử dụng vào ngày {ngayThue:dd/MM/yyyy}!",
                    "Phòng không khả dụng", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            try
            {
                // 1. Tạo mã phiếu thuê tự động
                string maPhieuThue = GenerateMaPhieuThue();

                // 2. Tạo phiếu thuê chính
                var phieuThue = new PhieuThue
                {
                    MaPhieuThue = maPhieuThue,
                    MaPhong = maPhong,
                    NgayBatDauThue = ngayThue.Date
                };
                db.PhieuThues.InsertOnSubmit(phieuThue);

                // 3. Thêm chi tiết khách (khách đầu tiên là "Chính", còn lại là "Phụ")
                foreach (var maKhach in dsMaKhach)
                {
                    string vaiTro = maKhach == dsMaKhach[0] ? "Chinh" : "Phu";

                    var chiTiet = new ChiTietPhieuThue
                    {
                        MaPhieuThue = maPhieuThue,
                        MaKhach = maKhach,
                        VaiTro = vaiTro
                    };
                    db.ChiTietPhieuThues.InsertOnSubmit(chiTiet);
                }

                // 4. Cập nhật trạng thái phòng thành "Đã thuê"
                var phong = db.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
                if (phong != null)
                {
                    phong.TinhTrang = "Đã thuê";
                }

                // 5. Lưu tất cả thay đổi
                db.SubmitChanges();

                // 6. Thông báo thành công
                MessageBox.Show(
                    $"Đặt phòng thành công!\n" +
                    $"Mã phiếu: {maPhieuThue}\n" +
                    $"Phòng: {maPhong}\n" +
                    $"Ngày thuê: {ngayThue:dd/MM/yyyy}\n" +
                    $"Số khách: {dsMaKhach.Length}",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lập phiếu thuê:\n" + ex.Message + "\n\n" + ex.InnerException?.Message,
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Tạo mã phiếu thuê tự động: PT00000001, PT00000002,...
        /// </summary>
        private string GenerateMaPhieuThue()
        {
            var last = db.PhieuThues
                .OrderByDescending(p => p.MaPhieuThue)
                .Select(p => p.MaPhieuThue)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(last))
                return "PT00000001";

            if (last.StartsWith("PT") && int.TryParse(last.Substring(2), out int num))
            {
                return "PT" + (num + 1).ToString("00000000");
            }

            return "PT00000001"; // fallback nếu dữ liệu lỗi
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}