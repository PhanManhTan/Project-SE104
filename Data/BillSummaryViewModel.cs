using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BillSummaryViewModel
    {
        public string MaHoaDon { get; set; }
        public DateTime NgayLap { get; set; }
        public int SoLuongPhieu { get; set; }
        public decimal TriGia { get; set; }


        

        // Tùy chọn: Định dạng hiển thị tiền tệ (ví dụ: 1.500.000 đ)
        public string TriGiaFormatted => TriGia.ToString("N0") + " đ";
    }
}
