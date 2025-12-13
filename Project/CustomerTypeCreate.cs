using System;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class CustomerTypeCreate : Form
    {
        private readonly CustomerService customerService = new CustomerService();

        public CustomerTypeCreate()
        {
            InitializeComponent();
        }

        private void ClearInputs()
        {
            txtMaLK.Clear();
            txtTenLK.Clear();
            txtMaLK.Focus();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLK.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã loại khách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLK.Focus();
                return;
            }
            // Check độ dài CHAR(5)
            if (txtMaLK.Text.Trim().Length > 5)
            {
                MessageBox.Show("Mã loại khách tối đa 5 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLK.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenLK.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên loại khách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLK.Focus();
                return;
            }

            var newItem = new LoaiKhach
            {
                MaLoaiKhach = txtMaLK.Text.Trim(),
                TenLoaiKhach = txtTenLK.Text.Trim()
            };

            if (customerService.AddCustomerType(newItem))
            {
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
        private void LoaiKhach_CreateForm_Load(object sender, EventArgs e) => ClearInputs();
    }
}