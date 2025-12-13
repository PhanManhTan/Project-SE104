using Data;
using Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class RoomManager : Form
    {
        private Phong phongDangChon = null; // Lưu phòng đang được chọn trên grid
        private bool isGridConfigured = false; // Đánh dấu đã cấu hình cột chưa

        public RoomManager()
        {
            InitializeComponent();
        }

        private void RoomManager_Load(object sender, EventArgs e)
        {
            SetupDataGridView();     // Thiết lập giao diện và các sự kiện
            LoadDanhSachPhong();     // Load dữ liệu lần đầu
        }

        #region === THIẾT LẬP GIAO DIỆN DATAGRIDVIEW ===
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

            // Tắt style header mặc định để custom
            dgv.EnableHeadersVisualStyles = false;

            // Header: màu navy, chữ trắng đậm
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Dòng dữ liệu
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            // Tô màu theo tình trạng phòng
            dgv.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                var row = dgv.Rows[e.RowIndex];
                var tinhTrang = row.Cells["TinhTrang"].Value?.ToString().Trim();

                switch (tinhTrang)
                {
                    case "Trống":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
                        row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                        break;
                    case "Đã đặt":
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
            };

            dgv.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

                if (dgv.Columns[e.ColumnIndex].Name == "STT")
                {
                    e.Value = (e.RowIndex + 1).ToString();
                    e.FormattingApplied = true;
                }
            };

            // Hover effect nhẹ
            dgv.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0) dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);
            };
            dgv.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0) dgv.InvalidateRow(e.RowIndex);
            };
        }
        #endregion

        #region === LOAD VÀ REFRESH DANH SÁCH PHÒNG ===
        private void LoadDanhSachPhong()
        {
            RoomService roomService = new RoomService();
            var listRoom = roomService.GetAllRooms();

            var dgv = dgvRoomManeger;
            dgv.AutoGenerateColumns = false;

            // Chỉ tạo cột 1 lần duy nhất
            if (!isGridConfigured)
            {
                dgv.Columns.Clear();

                // Cột STT (không bind dữ liệu, chỉ hiển thị số thứ tự)
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "STT",
                    Name = "STT",
                    Width = 60,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        ForeColor = Color.Navy
                    }
                });

                // Các cột dữ liệu khác
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "MaPhong",
                    HeaderText = "Mã phòng",
                    DataPropertyName = "MaPhong",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "LoaiPhong",
                    HeaderText = "Loại phòng",
                    DataPropertyName = "MaLoaiPhong",
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                dgv.Columns.Add(new DataGridViewTextBoxColumn
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

                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "GhiChu",
                    HeaderText = "Ghi chú",
                    DataPropertyName = "GhiChu",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) }
                });

                isGridConfigured = true;
            }

            dgv.DataSource = null;
            dgv.DataSource = listRoom;

        }

        public void RefreshGrid()
        {
            LoadDanhSachPhong();
            phongDangChon = null;
        }
        #endregion

        #region === XỬ LÝ CHỌN DÒNG ===
        private void dgvRoomManeger_SelectionChanged(object sender, EventArgs e)
        {
            phongDangChon = dgvRoomManeger.CurrentRow?.DataBoundItem as Phong;
        }
        #endregion

        #region === CÁC NÚT CHỨC NĂNG ===
        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var detailRoom = new DetailRoom())
            {
                if (detailRoom.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (phongDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một phòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var detailRoom = new DetailRoom(phongDangChon))
            {
                if (detailRoom.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                }
            }
        }

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (phongDangChon == null)
        //    {
        //        MessageBox.Show("Vui lòng chọn một phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }

        //    var result = MessageBox.Show(
        //        $"Bạn có chắc chắn muốn xóa phòng \"{phongDangChon.MaPhong}\" không?",
        //        "Xác nhận xóa",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Warning);

        //    if (result == DialogResult.Yes)
        //    {
        //        var roomService = new RoomService();
        //        roomService.DeleteRoom(phongDangChon.MaPhong);
        //        RefreshGrid();
        //        MessageBox.Show("Xóa phòng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
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

        private void btnRoomTypes_Click(object sender, EventArgs e)
        {
            using (var frm = new RoomTypeManager())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    this.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}