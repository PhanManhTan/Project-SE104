using System;

namespace Data
{
    public class RentalViewModel
    {
        public string MaPhieuThue { get; set; }
        public string MaPhong { get; set; }
        public DateTime NgayBatDauThue { get; set; }
        public string TenKhachChinh { get; set; } // Lấy từ ChiTietPhieuThue có vai trò "Chinh"
        public string TinhTrang { get; set; } // Đang thuê / Đã thanh toán
    }
}