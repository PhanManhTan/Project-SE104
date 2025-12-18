using System;
using System.Linq;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class CustomerTypeDetailForm : Form
    {
        private LoaiKhach _current = null;
        private readonly CustomerService customerService = new CustomerService();


        public CustomerTypeDetailForm()
        {
            InitializeComponent();
            this.Text = "Thêm loại khách mới";
        }


        public CustomerTypeDetailForm(LoaiKhach loaiKhach)
        {
            InitializeComponent();
            _current = loaiKhach;
            this.Text = "Sửa thông tin loại khách";
        }

        private void DetailCustomerType_Load(object sender, EventArgs e)
        {
            if (_current != null) 
            {
                tbMaLoaiKhach.Text = _current.MaLoaiKhach?.Trim() ?? "";
                tbTenLoaiKhach.Text = _current.TenLoaiKhach?.Trim() ?? "";

                
                tbMaLoaiKhach.Enabled = false;
                tbTenLoaiKhach.Focus();
            }
            else 
            {
                tbMaLoaiKhach.Text = customerService.GenerateNewMaLoaiKhach();
                tbMaLoaiKhach.Enabled = false; 
                tbTenLoaiKhach.Focus();
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {

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

                var allTypes = customerService.GetAllCustomerTypes();

                string msg = "Không thể lưu loại khách!\n\nLý do:\n";
                bool hasError = false;


                if (allTypes.Any(lk => string.Equals(lk.MaLoaiKhach, maLoai, StringComparison.OrdinalIgnoreCase) &&
                                      (_current == null || !string.Equals(lk.MaLoaiKhach, _current.MaLoaiKhach, StringComparison.OrdinalIgnoreCase))))
                {
                    msg += "• Mã loại khách đã tồn tại.\n";
                    hasError = true;
                }


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