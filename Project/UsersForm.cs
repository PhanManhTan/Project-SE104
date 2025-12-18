using Data;
using Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class UsersForm : Form
    {
        private User_ selectedUser = null;
        private BindingSource bindingSource = new BindingSource();

        public UsersForm()
        {
            InitializeComponent();
        }

        private void UserManager_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns(); // Gọi một lần duy nhất
            LoadUsers();
        }

        #region === CẤU HÌNH GIAO DIỆN DATAGRIDVIEW ===

        private void SetupDataGridView()
        {
            var dgv = dgvBody;

            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 45;

            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // Row style
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            dgv.ScrollBars = ScrollBars.Vertical;

            // Sự kiện đánh số STT tự động
            dgv.DataBindingComplete += DgvUser_DataBindingComplete;
            dgv.SelectionChanged += dgvUser_SelectionChanged;
        }

        private void DgvUser_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvBody.Rows.Count; i++)
            {
                dgvBody.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }

            dgvBody.ClearSelection();
            selectedUser = null;
        }

        #endregion

        #region === CẤU HÌNH CÁC CỘT ===

        private void ConfigureDataGridViewColumns()
        {
            var dgv = dgvBody;

            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            // STT
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "STT",
                Width = 60,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            });

            // Họ tên
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                HeaderText = "Họ tên",
                DataPropertyName = "FullName",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                }
            });

            // Tên đăng nhập
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                HeaderText = "Tên đăng nhập",
                DataPropertyName = "Username",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Mật khẩu (hiển thị dưới dạng ****** hoặc để trống tùy ý, nhưng vẫn bind để edit)
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Password_",
                HeaderText = "Mật khẩu",
                DataPropertyName = "Password_",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Vị trí (Role)
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Role_",
                HeaderText = "Vai trò",
                DataPropertyName = "Role_",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Gán BindingSource
            dgv.DataSource = bindingSource;
        }

        #endregion

        #region === LOAD + REFRESH DANH SÁCH ===

        private void LoadUsers()
        {
            try
            {
                var userService = new UserService();
                var list = userService.GetAll()
                                      .Where(u => u.Role_ == "Staff") // Chỉ lấy Staff
                                      .ToList();

                bindingSource.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshGrid()
        {
            LoadUsers();
            selectedUser = null;
            dgvBody.ClearSelection();
        }

        #endregion

        #region === CHỌN DÒNG ===

        private void dgvUser_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBody.CurrentRow != null && dgvBody.CurrentRow.Index >= 0)
            {
                selectedUser = dgvBody.CurrentRow.DataBoundItem as User_;
            }
            else
            {
                selectedUser = null;
            }
        }

        #endregion

        #region === CÁC NÚT CHỨC NĂNG ===

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new UserDetailForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var userService = new UserService();
            var userFromDB = userService.GetByUsername(selectedUser.Username);

            if (userFromDB == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên để sửa!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                RefreshGrid();
                return;
            }

            using (var form = new UserDetailForm(userFromDB))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Không cho xóa tài khoản đang đăng nhập
            if (selectedUser.Username == Global_.CurrentUser?.Username)
            {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhân viên \"{selectedUser.FullName}\" (Tên đăng nhập: {selectedUser.Username}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                var userService = new UserService();
                bool success = userService.Delete(selectedUser.Username);

                RefreshGrid();

                MessageBox.Show(success
                    ? "Xóa nhân viên thành công!"
                    : "Không thể xóa nhân viên! (Có thể có dữ liệu ràng buộc hoặc lỗi hệ thống)",
                    success ? "Thành công" : "Lỗi",
                    MessageBoxButtons.OK,
                    success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion


    }
}