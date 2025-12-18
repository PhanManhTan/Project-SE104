using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class HomeForm : Form
    {
        private RoomViewModel selectedRoom = null;
        private BindingSource bindingSource = new BindingSource();
        private List<RoomViewModel> listRoom;

        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns();
            LoadDanhSachPhong();

            RoomService roomService = new RoomService();

            // ComboBox Tình trạng
            var statuses = roomService.GetAllStatus().ToList();
            statuses.Insert(0, "Tất cả");
            cbStatus.DataSource = statuses;
            cbStatus.SelectedIndex = 0;
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            // ComboBox Loại phòng
            var roomTypes = roomService.GetAllRoomTypes().Select(l => l.TenLoaiPhong).ToList();
            roomTypes.Insert(0, "Tất cả");
            cbTypeRoom.DataSource = roomTypes;
            cbTypeRoom.SelectedIndex = 0;
            cbTypeRoom.DropDownStyle = ComboBoxStyle.DropDownList;
        }

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
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);
            dgv.ScrollBars = ScrollBars.Vertical;


            dgv.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);
            };
            dgv.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgv.InvalidateRow(e.RowIndex);
            };

            dgv.CellFormatting += Dgv_CellFormatting;


            dgv.DataBindingComplete += Dgv_DataBindingComplete;


            dgv.DoubleClick += DgvHomePage_DoubleClick;


            dgv.SelectionChanged += dgv_SelectionChanged;
        }

        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvBody.Rows[e.RowIndex];
            var tinhTrangCell = row.Cells["TinhTrang"];
            if (tinhTrangCell.Value == null) return;

            string tinhTrang = tinhTrangCell.Value.ToString().Trim();
            switch (tinhTrang)
            {
                case "Trống":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
                    row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                    break;
                case "Đã thuê":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    break;
                case "Đang dọn":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);
                    row.DefaultCellStyle.ForeColor = Color.DarkOrange;
                    break;
                default:
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    break;
            }
        }

        private void Dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvBody.Rows.Count; i++)
            {
                dgvBody.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
            dgvBody.ClearSelection();
        }

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

            // Các cột khác
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaPhong",
                HeaderText = "Mã phòng",
                DataPropertyName = "MaPhong",
                MinimumWidth = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenLoaiPhong",
                HeaderText = "Loại phòng",
                DataPropertyName = "TenLoaiPhong",
                MinimumWidth = 180,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Đơn giá",
                DataPropertyName = "DonGiaFormatted",
                MinimumWidth = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.DarkGreen,
                    Padding = new Padding(0, 0, 10, 0)
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TinhTrang",
                HeaderText = "Tình trạng",
                DataPropertyName = "TinhTrang",
                MinimumWidth = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GhiChu",
                HeaderText = "Ghi chú",
                DataPropertyName = "GhiChu",
                FillWeight = 1.5f,
                MinimumWidth = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                }
            });

            dgv.DataSource = bindingSource;
        }

        private void LoadDanhSachPhong()
        {
            var roomService = new RoomService();
            listRoom = roomService.GetAllRoomsView();
            bindingSource.DataSource = listRoom;
        }

        public void RefreshGrid()
        {
            LoadDanhSachPhong();
            selectedRoom = null;
            dgvBody.ClearSelection();
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBody.CurrentRow != null && dgvBody.CurrentRow.Index >= 0)
            {
                selectedRoom = dgvBody.CurrentRow.DataBoundItem as RoomViewModel;
            }
            else
            {
                selectedRoom = null;
            }
        }

        private void ApplyFilter()
        {
            if (listRoom == null) return;

            string keyword = tbSearch.Text.Trim().ToLower();
            string typeRoom = cbTypeRoom.SelectedItem?.ToString();
            string status = cbStatus.SelectedItem?.ToString();

            var filtered = listRoom.Where(r =>
                (string.IsNullOrEmpty(keyword) || r.MaPhong.ToLower().Contains(keyword)) &&
                (typeRoom == "Tất cả" || r.TenLoaiPhong == typeRoom) &&
                (status == "Tất cả" || r.TinhTrang == status)
            ).ToList();

            bindingSource.DataSource = filtered;
        }

        private void tbSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();
        private void cbTypeRoom_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilter();
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilter();

        private void DgvHomePage_DoubleClick(object sender, EventArgs e)
        {
            if (selectedRoom == null) return;

            if (selectedRoom.TinhTrang != "Trống")
            {
                MessageBox.Show("Chỉ có thể thuê phòng khi phòng đang ở trạng thái Trống.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var rentalForm = new RentalRegistrationForm(selectedRoom.MaPhong))
            {
                if (rentalForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                }
            }
        }
    }
}