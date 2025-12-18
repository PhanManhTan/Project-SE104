using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BillDetailItemViewModel
    {
        public string MaPhieuThue { get; set; }
        public string MaPhong { get; set; }
        public DateTime NgayThue { get; set; }
        public DateTime NgayTra { get; set; }
        public int SoNgay { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        public string TenKhach { get; set; }
        public string DiaChi { get; set; }
    }
}
