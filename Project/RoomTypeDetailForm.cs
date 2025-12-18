using System;
using System.Linq; // Thêm dòng này để dùng LINQ
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class DetailRoomType : Form
    {
        private readonly RoomService roomService = new RoomService();
        private LoaiPhong _current = null; // null = thêm mới, có giá trị = cập nhật

        // Constructor cho Thêm mới
        public DetailRoomType()
        {
            InitializeComponent();
            this.Text = "Thêm loại phòng mới";
        }

        // Constructor cho Cập nhật
        public DetailRoomType(LoaiPhong loaiPhong)
        {
            InitializeComponent();
            _current = loaiPhong;
            this.Text = "Sửa thông tin loại phòng";
        }

        private void DetailRoomType_Load(object sender, EventArgs e)
        {
            if (_current != null) // Chế độ cập nhật
            {
                tbMaLP.Text = _current.MaLoaiPhong?.Trim() ?? "";
                tbTenLP.Text = _current.TenLoaiPhong?.Trim() ?? "";
                tbDG.Text = _current.DonGia.ToString("N0");

                // Kiểm tra loại phòng có đang được sử dụng không
                bool dangSuDung = roomService.IsRoomTypeInUse(_current.MaLoaiPhong);
                if (dangSuDung)
                {
                    tbMaLP.Enabled = false;
                    this.Text = "Cập nhật loại phòng (Mã đang sử dụng - Không thể thay đổi)";
                }
                else
                {
                    tbMaLP.Enabled = true;
                    this.Text = "Cập nhật loại phòng";
                }

                tbTenLP.Focus();
            }
            else // Chế độ thêm mới
            {
                tbMaLP.Text = GenerateNewMaLoaiPhong();
                tbMaLP.Enabled = false; // KHÓA MÃ KHI THÊM MỚI
                tbTenLP.Focus();
            }
        }

        /// <summary>
        /// Tự động sinh mã loại phòng mới dạng LP001, LP002,...
        /// </summary>
        private string GenerateNewMaLoaiPhong()
        {
            try
            {
                var last = roomService.GetAllRoomTypes()
                    .Where(lp => !string.IsNullOrEmpty(lp.MaLoaiPhong) &&
                                lp.MaLoaiPhong.StartsWith("LP", StringComparison.OrdinalIgnoreCase))
                    .Select(lp => lp.MaLoaiPhong.ToUpper())
                    .OrderByDescending(m => m)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(last))
                    return "LP001";

                string numberPart = last.Substring(2); // Bỏ "LP"
                if (int.TryParse(numberPart, out int num))
                {
                    return "LP" + (num + 1).ToString("D3"); // D3 = 3 chữ số, thêm số 0 nếu cần
                }
            }
            catch
            {
                // Nếu có lỗi bất ngờ, trả về mặc định
            }

            return "LP001";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(tbMaLP.Text))
            {
                MessageBox.Show("Mã loại phòng không hợp lệ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbTenLP.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên loại phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbTenLP.Focus();
                return;
            }

            // Xử lý đơn giá
            string cleanPrice = tbDG.Text.Replace(".", "").Trim();
            if (!decimal.TryParse(cleanPrice, out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số dương hợp lệ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbDG.Focus();
                return;
            }

            var loaiPhong = new LoaiPhong
            {
                MaLoaiPhong = tbMaLP.Text.Trim(),
                TenLoaiPhong = tbTenLP.Text.Trim(),
                DonGia = donGia
            };

            bool success;
            string action = _current != null ? "cập nhật" : "thêm mới";

            if (_current != null)
            {
                success = roomService.UpdateRoomType(loaiPhong, _current.MaLoaiPhong);
            }
            else
            {
                success = roomService.AddRoomType(loaiPhong);
            }

            if (success)
            {
                MessageBox.Show($"Thành công! Đã {action} loại phòng.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show($"Không thể {action} loại phòng!\n\nLý do phổ biến:\n• Mã loại phòng đã tồn tại\n• Có lỗi kết nối CSDL",
                    "Lỗi lưu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Định dạng lại đơn giá khi rời khỏi ô nhập
        private void tbDG_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(tbDG.Text.Replace(".", ""), out decimal value))
            {
                tbDG.Text = value.ToString("N0");
            }
        }
    }
}