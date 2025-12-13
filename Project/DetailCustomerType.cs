using System;
using System.Linq;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class DetailCustomerType : Form
    {
        private LoaiKhach _current = null;
        private readonly CustomerService customerService = new CustomerService();

        // Constructor cho Thêm mới
        public DetailCustomerType()
        {
            InitializeComponent();
            this.Text = "Thêm loại khách mới";
        }

        // Constructor cho Cập nhật
        public DetailCustomerType(LoaiKhach loaiKhach)
        {
            InitializeComponent();
            _current = loaiKhach;
            this.Text = "Sửa thông tin loại khách";
        }

        private void DetailCustomerType_Load(object sender, EventArgs e)
        {
            if (_current != null) // Chế độ cập nhật
            {
                tbMaLoaiKhach.Text = _current.MaLoaiKhach?.Trim() ?? "";
                tbTenLoaiKhach.Text = _current.TenLoaiKhach?.Trim() ?? "";

                // KHÓA MÃ - không cho sửa khi cập nhật
                tbMaLoaiKhach.Enabled = false;
                tbTenLoaiKhach.Focus();
            }
            else // Chế độ thêm mới
            {
                tbMaLoaiKhach.Text = GenerateNewMaLoaiKhach();
                tbMaLoaiKhach.Enabled = false; // Vẫn khóa vì tự sinh
                tbTenLoaiKhach.Focus();
            }
        }

        private string GenerateNewMaLoaiKhach()
        {
            try
            {
                var last = customerService.GetAllCustomerTypes()
                    .Where(lk => !string.IsNullOrEmpty(lk.MaLoaiKhach) && lk.MaLoaiKhach.StartsWith("LK"))
                    .Select(lk => lk.MaLoaiKhach)
                    .OrderByDescending(m => m)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(last))
                    return "LK001";

                string numberPart = last.Substring(2);
                if (int.TryParse(numberPart, out int num))
                {
                    return "LK" + (num + 1).ToString("D3");
                }
            }
            catch
            {
                // Phòng trường hợp lỗi bất ngờ
            }

            return "LK001";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(tbTenLoaiKhach.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại khách!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbTenLoaiKhach.Focus();
                return;
            }

            string maLoai = tbMaLoaiKhach.Text.Trim();
            string tenLoai = tbTenLoaiKhach.Text.Trim();

            var loaiKhach = new LoaiKhach
            {
                MaLoaiKhach = maLoai,
                TenLoaiKhach = tenLoai
            };

            bool success = _current != null
                ? customerService.UpdateCustomerType(loaiKhach, _current.MaLoaiKhach)
                : customerService.AddCustomerType(loaiKhach);

            if (success)
            {
                MessageBox.Show(_current != null ? "Cập nhật loại khách thành công!" : "Thêm loại khách thành công!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Phân tích lỗi chi tiết giống form DetailCustomer
                var allTypes = customerService.GetAllCustomerTypes();

                string msg = "Không thể lưu loại khách!\n\nLý do:\n";
                bool hasError = false;

                // Kiểm tra trùng mã (chỉ áp dụng khi thêm mới, hoặc khi cập nhật mà đổi mã - nhưng ở đây không cho đổi mã)
                if (allTypes.Any(lk => string.Equals(lk.MaLoaiKhach, maLoai, StringComparison.OrdinalIgnoreCase) &&
                                      (_current == null || !string.Equals(lk.MaLoaiKhach, _current.MaLoaiKhach, StringComparison.OrdinalIgnoreCase))))
                {
                    msg += "• Mã loại khách đã tồn tại.\n";
                    hasError = true;
                }

                // Kiểm tra trùng tên (tùy chọn - nếu muốn không cho trùng tên)
                // Nếu bạn muốn cho phép trùng tên thì bỏ đoạn này
                if (allTypes.Any(lk => string.Equals(lk.TenLoaiKhach, tenLoai, StringComparison.OrdinalIgnoreCase) &&
                                      (_current == null || !string.Equals(lk.MaLoaiKhach, _current.MaLoaiKhach, StringComparison.OrdinalIgnoreCase))))
                {
                    msg += "• Tên loại khách đã được sử dụng.\n";
                    hasError = true;
                }

                if (!hasError)
                {
                    msg += "• Lỗi không xác định (kiểm tra kết nối CSDL hoặc quyền truy cập).";
                }

                MessageBox.Show(msg, "Lỗi lưu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}