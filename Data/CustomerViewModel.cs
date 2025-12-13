namespace Data
{
    public class CustomerViewModel
    {
        public string MaKhach { get; set; }
        public string HoTen { get; set; }
        public string CMND { get; set; }
        public string DiaChi { get; set; }
        public string MaLoaiKhach { get; set; }      // Giữ lại nếu cần dùng nội bộ
        public string TenLoaiKhach { get; set; }     // Dùng để hiển thị
    }
}