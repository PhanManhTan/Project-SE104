using Data;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns();
            LoadDanhSachPhong();
            RoomService roomService = new RoomService();
            var statuses = roomService.GetAllStatus().ToList();
            statuses.Insert(0, "Tất cả");
            cbStatus.DataSource = statuses;
            cbStatus.SelectedIndex = 0;
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            var roomTypes = roomService.GetAllRoomTypes().Select(l => l.TenLoaiPhong).ToList();
            roomTypes.Insert(0, "Tất cả");
            cbTypeRoom.DataSource = roomTypes;
            cbTypeRoom.SelectedIndex = 0;
            cbTypeRoom.DropDownStyle = ComboBoxStyle.DropDownList;

        }
        private RoomViewModel selectedRoom = null;
        private BindingSource bindingSource = new BindingSource();
        private BindingSource bsStatus = new BindingSource();
        private BindingSource bsRoomType = new BindingSource();

        private void SetupDataGridView()
        {
            var dgv = dgvHomePage;
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
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);
            dgv.ScrollBars = ScrollBars.Vertical;

            // Hover effect
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

            // Tô màu theo tình trạng phòng
            dgv.CellFormatting += Dgv_CellFormatting;

            // Đánh số STT tự động
            dgv.DataBindingComplete += DgvRoomManeger_DataBindingComplete;


            dgv.DoubleClick += DgvHomePage_DoubleClick;
            dgv.SelectionChanged += dgvRoomManeger_SelectionChanged;
        }

        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvHomePage.Rows[e.RowIndex];
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

        private void DgvRoomManeger_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvHomePage.Rows.Count; i++)
            {
                dgvHomePage.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
        }

        #region === CẤU HÌNH CÁC CỘT ===
        private void ConfigureDataGridViewColumns()
        {
            var dgv = dgvHomePage;
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
        #endregion
        private List<RoomViewModel> listRoom;
        #region === LOAD + REFRESH DANH SÁCH PHÒNG ===
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
            dgvHomePage.ClearSelection();
        }
        #endregion

        #region === XỬ LÝ CHỌN DÒNG ===
        private void dgvRoomManeger_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHomePage.CurrentRow != null && dgvHomePage.CurrentRow.Index >= 0)
            {
                selectedRoom = dgvHomePage.CurrentRow.DataBoundItem as RoomViewModel;
            }
            else
            {
                selectedRoom = null;
            }
        }

        #endregion

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cbTypeRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void ApplyFilter()
        {
            if (listRoom == null) return;

            string keyword = tbSearch.Text.Trim().ToLower();
            string typeRoom = cbTypeRoom.SelectedItem?.ToString();
            string status = cbStatus.SelectedItem?.ToString();

            var filtered = listRoom.Where(r =>
                // Search theo mã phòng
                (string.IsNullOrEmpty(keyword) ||
                 r.MaPhong.ToLower().Contains(keyword))

                // Filter loại phòng
                && (typeRoom == "Tất cả" || r.TenLoaiPhong == typeRoom)

                // Filter tình trạng
                && (status == "Tất cả" || r.TinhTrang == status)
            ).ToList();

            bindingSource.DataSource = filtered;
        }
        private void DgvHomePage_DoubleClick(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào đang được chọn không
            if (selectedRoom == null)
                return;

            // Kiểm tra tình trạng phòng: chỉ cho phép thuê khi phòng "Trống"
            if (selectedRoom.TinhTrang != "Trống")
            {
                MessageBox.Show("Chỉ có thể thuê phòng khi phòng đang ở trạng thái Trống.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở form Rental và truyền thông tin phòng
            using (var rentalForm = new Rental(selectedRoom.MaPhong))
            {
                // Nếu form Rental trả về DialogResult.OK nghĩa là đã thuê thành công
                if (rentalForm.ShowDialog() == DialogResult.OK)
                {
                    // Cập nhật lại danh sách và grid sau khi thuê thành công
                    RefreshGrid();
                }
            }






        }
    }
}


   