using Data;
using Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class RoomManager : Form
    {
        private Phong phongDangChon = null;

        public RoomManager()
        {
            InitializeComponent();
            LoadDanhSachPhong();
        }

        private void RoomManeger_Load(object sender, EventArgs e)
        {
            SetupDataGridView();      // Thiết lập giao diện 1 lần
            LoadDanhSachPhong();      // Load dữ liệu lần đầu
        }

        #region === CẤU HÌNH GIAO DIỆN DATAGRIDVIEW ===
        private void SetupDataGridView()
        {
            var dgv = dgvRoomManeger;

            // Cấu hình chung
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ScrollBars = ScrollBars.Vertical;

            // Tắt style Windows mặc định để custom header
            dgv.EnableHeadersVisualStyles = false;

            // Header - Navy + chữ trắng đậm
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;   // Giữ nguyên màu khi click
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // Dòng dữ liệu
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            // Tô màu theo tình trạng phòng (động + mượt)
            dgv.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || dgv.Rows[e.RowIndex].IsNewRow) return;

                var row = dgv.Rows[e.RowIndex];
                var cellValue = row.Cells["TinhTrang"].Value?.ToString().Trim();

                if (string.IsNullOrEmpty(cellValue)) return;

                switch (cellValue)
                {
                    case "Trống":
                    case "Sẵn sàng":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
                        row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                        break;

                    case "Đã đặt":
                    case "Đang sử dụng":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                        break;

                    case "Đang dọn":
                    case "Bảo trì":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);
                        row.DefaultCellStyle.ForeColor = Color.DarkOrange;
                        break;

                    default:
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                }
            };

            // Hover effect nhẹ (tùy chọn - rất đẹp)
            dgv.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0) dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);
            };

            dgv.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0) dgv.InvalidateRow(e.RowIndex); // Khôi phục màu theo tình trạng
            };
        }
        #endregion

        #region === LOAD + REFRESH DANH SÁCH PHÒNG ===
        private void LoadDanhSachPhong()
        {
            RoomService roomService = new RoomService();
            var listRoom = roomService.GetAllRooms();

            dgvRoomManeger.AutoGenerateColumns = false;
            dgvRoomManeger.Columns.Clear();

            // Cột STT
            dgvRoomManeger.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "STT",
                Width = 60,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.Navy
                }
            });

            // Các cột dữ liệu
            dgvRoomManeger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaPhong",
                HeaderText = "Mã phòng",
                DataPropertyName = "MaPhong",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvRoomManeger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiPhong",
                HeaderText = "Loại phòng",
                DataPropertyName = "MaLoaiPhong",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvRoomManeger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TinhTrang",
                HeaderText = "Tình trạng",
                DataPropertyName = "TinhTrang",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            });

            dgvRoomManeger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GhiChu",
                HeaderText = "Ghi chú",
                DataPropertyName = "GhiChu",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) }
            });

            // Gán dữ liệu
            dgvRoomManeger.DataSource = listRoom;

            // Đánh số thứ tự
            for (int i = 0; i < dgvRoomManeger.Rows.Count; i++)
            {
                dgvRoomManeger.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
        }

        public void RefreshGrid()
        {
            LoadDanhSachPhong();
            phongDangChon = null;
        }
        #endregion

        #region === SỰ KIỆN CHỌN DÒNG ===
        private void dgvRoomManeger_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRoomManeger.CurrentRow != null && dgvRoomManeger.CurrentRow.Index >= 0)
            {
                phongDangChon = (Phong)dgvRoomManeger.CurrentRow.DataBoundItem;
            }
            else
            {
                phongDangChon = null;
            }
        }
        #endregion

        #region === CÁC NÚT CHỨC NĂNG ===
        private void btnCreate_Click(object sender, EventArgs e)
        {
            var detailRoom = new DetailRoom();
            if (detailRoom.ShowDialog() == DialogResult.OK)
            {
                RefreshGrid();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (phongDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một phòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var detailRoom = new DetailRoom(phongDangChon);
            if (detailRoom.ShowDialog() == DialogResult.OK)
            {
                RefreshGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (phongDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa phòng \"{phongDangChon.MaPhong}\" không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                var roomService = new RoomService();
                roomService.DeleteRoom(phongDangChon.MaPhong);
                RefreshGrid();
                MessageBox.Show("Xóa phòng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void btnRoomTypes_Click(object sender, EventArgs e)
        {
            using (var frm = new RoomTypeManager())
            {
                if (frm.ShowDialog() == DialogResult.OK) this.Close();
            }
        }
    }
}