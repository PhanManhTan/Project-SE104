namespace Data
{
    public class RoomViewModel
    {
        public string MaPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public string MaLoaiPhong { get; set; }
        public string TinhTrang { get; set; }
        public string GhiChu { get; set; }

        // THÊM ĐƠN GIÁ
        public decimal DonGia { get; set; }  // Hoặc double nếu bạn dùng double trong DB

        // Tùy chọn: Định dạng hiển thị tiền tệ (ví dụ: 1.500.000 đ)
        public string DonGiaFormatted => DonGia.ToString("N0") + " đ";
    }
}