using System;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class LoginForm : Form
    {
        private readonly UserService userService = new UserService();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DangNhap();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DangNhap();
            }
        }

        private void DangNhap()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Usernamename và mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = userService.Login(username, password);

            if (user != null)
            {
                MessageBox.Show($"Đăng nhập thành công!\nChào {user.DisplayName}", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ẩn form đăng nhập
                this.Hide();

                // Mở form chính
                var mainForm = new Main(); // Bạn phải có form MainForm
                mainForm.ShowDialog();

                // Khi MainForm đóng → thoát ứng dụng
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.SelectAll();
                txtPassword.Focus();
            }
        }
    }
}