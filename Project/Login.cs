using System;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class Login : Form
    {
        private readonly UserService userService = new UserService();

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Username và mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = userService.Login(username, password);

            if (user != null)
            {
                Global_.CurrentUser = user;

                MessageBox.Show($"Đăng nhập thành công!\nChào mừng {user.DisplayName}", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();

                var mainForm = new Main();
                mainForm.ShowDialog();
           
                Global_.CurrentUser = null;        
                txtPassword.Clear();
                txtUsername.Clear();
                this.Show();
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