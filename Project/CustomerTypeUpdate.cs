using System;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class CustomerTypeUpdate : Form
    {
        private readonly CustomerService customerService = new CustomerService();
        private string oldMaLoai;

        public CustomerTypeUpdate(string maLoai)
        {
            InitializeComponent();
            this.oldMaLoai = maLoai;
        }

        private void LoaiKhach_UpdateForm_Load(object sender, EventArgs e)
        {
            var item = customerService.GetCustomerTypeById(oldMaLoai);
            if (item != null)
            {
                txtMaLK.Text = item.MaLoaiKhach;
                txtTenLK.Text = item.TenLoaiKhach;

                // --- ĐOẠN CODE MỚI THÊM ---

                // 1. Kiểm tra xem mã này đã dùng chưa
                bool dangSuDung = customerService.IsCustomerTypeInUse(oldMaLoai);

                if (dangSuDung)
                {
                    // Đang dùng -> KHÓA MÃ
                    txtMaLK.Enabled = false;

                    // (Tùy chọn) Đổi tiêu đề form để báo người dùng biết
                    this.Text = "Cập nhật (Mã đang dùng - Không được sửa)";
                }
                else
                {
                    // Chưa dùng -> CHO SỬA THOẢI MÁI
                    txtMaLK.Enabled = true;
                    this.Text = "Cập nhật loại khách";
                }
                // --------------------------
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLK.Text) || string.IsNullOrWhiteSpace(txtTenLK.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMaLK.Text.Trim().Length > 5)
            {
                MessageBox.Show("Mã loại khách tối đa 5 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLK.Focus();
                return;
            }

            var updateItem = new LoaiKhach
            {
                MaLoaiKhach = txtMaLK.Text.Trim(),
                TenLoaiKhach = txtTenLK.Text.Trim()
            };

            // Gọi Service update (Service đã có sẵn logic: 
            // Nếu mã không đổi -> Update tên. 
            // Nếu mã đổi -> Xóa cũ thêm mới).
            if (customerService.UpdateCustomerType(updateItem, oldMaLoai))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}