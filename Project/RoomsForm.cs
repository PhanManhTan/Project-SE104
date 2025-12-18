using Data;
using Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class RoomsForm : Form
    {
        private RoomViewModel selectedRoom = null;
        private BindingSource bindingSource = new BindingSource();

        public RoomsForm()
        {
            InitializeComponent();
            if (Global_.CurrentUser != null && Global_.CurrentUser.Role_ == "Staff")
            {
                // Chỉ khóa nút XÓA khách hàng
                btnDelete.Enabled = false;
                btnDelete.BackColor = Color.DarkGray;
                btnUpdate.Enabled = false;
                btnUpdate.BackColor = Color.DarkGray;
                btnCreate.Enabled = false;
                btnCreate.BackColor = Color.DarkGray;
            }
        }

        private void RoomManager_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns(); // Gọi một lần duy nhất ở đây
            LoadDanhSachPhong();

        }

        #region === THIẾT LẬP GIAO DIỆN DATAGRIDVIEW ===
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

        private void DgvRoomManeger_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvBody.Rows.Count; i++)
            {
                dgvBody.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
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

        #region === LOAD + REFRESH DANH SÁCH PHÒNG ===
        private void LoadDanhSachPhong()
        {
            var roomService = new RoomService();
            var listRoom = roomService.GetAllRoomsView();
            bindingSource.DataSource = listRoom;
        }

        public void RefreshGrid()
        {
            LoadDanhSachPhong();
            selectedRoom = null;
            dgvBody.ClearSelection();
        }
        #endregion

        #region === XỬ LÝ CHỌN DÒNG ===
        //private void dgvRoomManeger_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (dgvRoomManeger.CurrentRow != null && dgvRoomManeger.CurrentRow.Index >= 0)
        //    {
        //        selectedRoom = dgvRoomManeger.CurrentRow.DataBoundItem as RoomViewModel;
        //    }
        //    else
        //    {
        //        selectedRoom = null;
        //    }
        //}
        private void dgvRoomManeger_SelectionChanged(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu dòng được chọn
            if (dgvBody.SelectedRows.Count > 0)
            {
                selectedRoom = dgvBody.SelectedRows[0].DataBoundItem as RoomViewModel;
            }
            else
            {
                selectedRoom = null;
            }

        }
        #endregion

        #region === CÁC NÚT CHỨC NĂNG ===
        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var detailRoom = new RoomDetailForm())
            {
                if (detailRoom.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRoom == null)
            {
                MessageBox.Show("Vui lòng chọn một phòng để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var roomService = new RoomService();
            var phong = roomService.GetAllRooms().FirstOrDefault(p => p.MaPhong == selectedRoom.MaPhong);
            if (phong == null)
            {
                MessageBox.Show("Không tìm thấy phòng để sửa!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                RefreshGrid();
                return;
            }

            using (var detailRoom = new RoomDetailForm(phong))
            {
                if (detailRoom.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRoom == null)
            {
                MessageBox.Show("Vui lòng chọn một phòng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa phòng \"{selectedRoom.MaPhong}\" không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                var roomService = new RoomService();
                bool success = roomService.DeleteRoom(selectedRoom.MaPhong);

                RefreshGrid();

                if (success)
                {
                    MessageBox.Show("Xóa phòng thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể xóa phòng (có thể đang có khách thuê hoặc lỗi khác).", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnRoomTypes_Click(object sender, EventArgs e)
        {
            using (var frm = new RoomTypesForm())
            {
                frm.ShowDialog();
                RefreshGrid();
            }
        }
        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}