    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Data;
    using Services;

    namespace Project
    {
        public partial class MainForm : Form
        {
            private readonly UserService userService = new UserService();
            private readonly RoomService roomService = new RoomService();

            public MainForm()
            {
                InitializeComponent();
            }

            private void MainForm_Load(object sender, EventArgs e)
            {
                this.Text = "HOTEL MANAGEMENT SYSTEM - Not logged in";
                txtUsername.Focus();
                ApplyPermissions(); // Ẩn menu chỉ Admin từ đầu
            }

            private void btnLogin_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter both username and password!", "Missing Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var user = userService.Login(txtUsername.Text.Trim(), txtPassword.Text);

                if (user != null)
                {
                    Global_.CurrentUser = user;

                    // Ẩn login, hiện giao diện chính
                    panelLogin.Visible = false;
                    panelUserInfo.Visible = true;

                    lblWelcome.Text = $"Welcome, {user.DisplayName}!";
                    lblRole.Text = user.IsAdmin ? "ADMINISTRATOR" : "STAFF";
                    lblRole.ForeColor = user.IsAdmin ? Color.Red : Color.DarkGreen;

                    this.Text = $"HOTEL MANAGEMENT - {user.DisplayName} ({user.Role_})";

                    // DÒNG QUAN TRỌNG NHẤT – TẢI PHÒNG SAU KHI LOGIN
                    LoadRoomData();

                    ApplyPermissions(); // Phân quyền menu + nút

                    MessageBox.Show($"Login successful!\nWelcome back, {user.DisplayName}!", "SUCCESS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid username or password!", "LOGIN FAILED",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }

            // TẢI DỮ LIỆU PHÒNG – CẢ ADMIN VÀ NHÂN VIÊN ĐỀU THẤY
            private void LoadRoomData()
            {
                try
                {
                    var allRooms = roomService.GetAllRooms();

                    if (allRooms == null || allRooms.Count == 0)
                    {
                        MessageBox.Show("KHÔNG CÓ DỮ LIỆU PHÒNG! Chạy lại script database!", "LỖI DỮ LIỆU",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                dataGridViewRooms.AutoGenerateColumns = true;

                dataGridViewRooms.DataSource = null; // reset trước khi bind
                dataGridViewRooms.DataSource = allRooms;


                // HIỆN THÔNG BÁO ĐỂ TEST
                MessageBox.Show($"TÌM THẤY {allRooms.Count} PHÒNG!\n" +
                                   $"Phòng đầu tiên: {allRooms[0].MaPhong} - {allRooms[0].TinhTrang}",
                                   "DỮ LIỆU OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dataGridViewRooms.DataSource = allRooms;

                    var availableRooms = roomService.GetAvailableRooms();
                    cmbAvailableRooms.DataSource = availableRooms;
                    cmbAvailableRooms.DisplayMember = "MaPhong";
                    cmbAvailableRooms.ValueMember = "MaPhong";

                    lblAvailableCount.Text = $"Available Rooms: {availableRooms.Count}";

                    // Tô màu theo trạng thái
                    foreach (DataGridViewRow row in dataGridViewRooms.Rows)
                    {
                        var status = row.Cells["TinhTrang"]?.Value?.ToString();
                        if (status == "Trong") row.DefaultCellStyle.BackColor = Color.LightGreen;
                        else if (status == "Da thue") row.DefaultCellStyle.BackColor = Color.LightCoral;
                        else if (status == "Dang don") row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("LỖI KẾT NỐI HOẶC DỮ LIỆU:\n" + ex.Message, "LỖI",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // PHÂN QUYỀN – CHỈ ẨN MENU + NÚT, KHÔNG ẨN PHÒNG
            private void ApplyPermissions()
            {
                bool isAdmin = Global_.CurrentUser?.IsAdmin ?? false;

                if (menuStrip1 != null)
                {
                    var item = menuStrip1.Items["mnuUserManagement"] as ToolStripMenuItem;
                    if (item != null) item.Visible = isAdmin;

                    item = menuStrip1.Items["mnuSettings"] as ToolStripMenuItem;
                    if (item != null) item.Visible = isAdmin;

                    item = menuStrip1.Items["mnuBackup"] as ToolStripMenuItem;
                    if (item != null) item.Visible = isAdmin;

                    item = menuStrip1.Items["mnuReports"] as ToolStripMenuItem;
                    if (item != null) item.Visible = true;
                }

                if (btnAddRoom != null) btnAddRoom.Visible = isAdmin;
                if (btnDeleteRoom != null) btnDeleteRoom.Visible = isAdmin;
                if (btnChangeSettings != null) btnChangeSettings.Visible = isAdmin;

                // PHÒNG LUÔN HIỆN CHO CẢ 2
                if (dataGridViewRooms != null) dataGridViewRooms.Visible = true;
                if (cmbAvailableRooms != null) cmbAvailableRooms.Visible = true;
                if (lblAvailableCount != null) lblAvailableCount.Visible = true;
            }

            private void btnLogout_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show("Are you sure you want to logout?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Global_.CurrentUser = null;

                    panelLogin.Visible = true;
                    panelUserInfo.Visible = false;

                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();

                    this.Text = "HOTEL MANAGEMENT SYSTEM - Not logged in";
                    ApplyPermissions();
                }
            }

            private void btnExit_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show("Exit application?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Application.Exit();
            }
        }
    }