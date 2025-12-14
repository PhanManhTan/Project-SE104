using Data;
using Services;
using System;
using System.Windows.Forms;

namespace Project
{
    public partial class DetailUser : Form
    {
        private readonly UserService _userService = new UserService();
        private User_ _currentUser; // null = thêm mới, có giá trị = cập nhật

        // Constructor: Thêm mới nhân viên
        public DetailUser()
        {
            InitializeComponent();
            _currentUser = null;
            this.Text = "Thêm nhân viên mới";
        }

        // Constructor: Cập nhật nhân viên
        public DetailUser(User_ user)
        {
            InitializeComponent();
            _currentUser = user;
            this.Text = "Cập nhật nhân viên";
        }

        private void DetailUser_Load(object sender, EventArgs e)
        {
            // Cấu hình ComboBox quyền hạn
            cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRole.Items.Clear();

            if (_currentUser == null)
            {
                // === CHẾ ĐỘ THÊM MỚI ===
                cbRole.Items.Add("Staff");
                cbRole.SelectedIndex = 0;
                cbRole.Enabled = false; // Không cho chọn, chỉ được thêm Staff

                txbUserName.Enabled = true;
                txbUserName.Focus();
            }
            else
            {
                // === CHẾ ĐỘ CẬP NHẬT ===
                cbRole.Items.Add(_currentUser.Role_); // Chỉ hiển thị quyền hiện tại
                cbRole.SelectedIndex = 0;
                cbRole.Enabled = false; // Không cho thay đổi quyền (ngăn nâng cấp Admin)

                txbUserName.Text = _currentUser.Username;
                txbUserName.Enabled = false; // Không cho sửa Username

                txbDisplayName.Text = _currentUser.FullName;
                txbPassWord.Text = ""; // Để trống, người dùng nhập mới nếu muốn đổi
                txbPassWord.Text = "Để trống nếu không đổi mật khẩu";

                txbDisplayName.Focus();
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txbUserName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbUserName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txbDisplayName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbDisplayName.Focus();
                return;
            }

            string username = txbUserName.Text.Trim();
            string fullName = txbDisplayName.Text.Trim();
            string passwordInput = txbPassWord.Text;

            if (_currentUser == null)
            {
                // === THÊM MỚI: Bắt buộc phải nhập mật khẩu ===
                if (string.IsNullOrWhiteSpace(passwordInput))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cho tài khoản mới!", "Thiếu thông tin",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txbPassWord.Focus();
                    return;
                }

                // Kiểm tra trùng Username
                if (_userService.GetByUsername(username) != null)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng chọn tên khác.", "Trùng lặp",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txbUserName.Focus();
                    return;
                }

                User_ newUser = new User_
                {
                    Username = username,
                    FullName = fullName,
                    Password_ = passwordInput, // Nên hash ở đây nếu có thể (gợi ý nâng cao)
                    Role_ = "Staff",
                    CreatedDate = DateTime.Now
                };

                bool success = _userService.Create(newUser);
                if (success)
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại! Vui lòng thử lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // === CẬP NHẬT ===
                User_ updatedUser = new User_
                {
                    Username = _currentUser.Username,
                    FullName = fullName,
                    Password_ = string.IsNullOrWhiteSpace(passwordInput)
                        ? _currentUser.Password_  // Giữ nguyên mật khẩu cũ
                        : passwordInput,         // Đổi mật khẩu mới
                    Role_ = _currentUser.Role_,
                    CreatedDate = _currentUser.CreatedDate
                };

                bool success = _userService.Update(updatedUser);
                if (success)
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại! Vui lòng thử lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}