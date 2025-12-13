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
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            SetupMain();
            LoadData();
        }
        private void SetupMain()
        {
            dgvMain.RowHeadersVisible = false;
            dgvMain.AllowUserToAddRows = false;
            dgvMain.AllowUserToDeleteRows = false;
            dgvMain.AllowUserToResizeRows = false;
            dgvMain.AllowUserToResizeColumns = false;
            dgvMain.ReadOnly = true;
            dgvMain.MultiSelect = false;
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMain.ScrollBars = ScrollBars.Vertical;

            // Tắt style Windows mặc định để custom header
            dgvMain.EnableHeadersVisualStyles = false;

            // Header - Navy + chữ trắng đậm
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvMain.ColumnHeadersHeight = 45;
            dgvMain.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvMain.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMain.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvMain.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMain.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgvMain.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // Dòng dữ liệu
            dgvMain.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvMain.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgvMain.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgvMain.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            // Tô màu theo tình trạng phòng
            dgvMain.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || dgvMain.Rows[e.RowIndex].IsNewRow) return;
                var row = dgvMain.Rows[e.RowIndex];
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


            // Hiệu ứng hover dòng
            dgvMain.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgvMain.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);
            };

            dgvMain.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgvMain.InvalidateRow(e.RowIndex);
            };

            List<string> cb = new List<string>();

            RoomService roomService = new RoomService();

            // Lấy danh sách loại phòng
            var types = roomService.GetAllRoomTypes()
                .Select(x => x.MaLoaiPhong.Trim())
                .ToList();

            // Lấy danh sách tình trạng phòng
            var status = roomService.GetAllStatus()
                .Select(x => x.Trim())
                .ToList();

            // Gộp 2 danh sách lại
            cb = types.Concat(status).ToList();
            cb.Insert(0, "Tất cả");

            // Gán vào ComboBox
            cbFilter.DataSource = cb;
            cbFilter.SelectedIndex = 0;
        }

        private List<Phong> listRoomOriginal = new List<Phong>();

        private void LoadData()
        {
            RoomService roomService = new RoomService();
            var listRoom = roomService.GetAllRooms();
            listRoomOriginal = listRoom.ToList();

            dgvMain.AutoGenerateColumns = false;
            dgvMain.Columns.Clear();

            // Cột STT
            dgvMain.Columns.Add(new DataGridViewTextBoxColumn
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

            // Các cột còn lại...
            dgvMain.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaPhong",
                HeaderText = "Mã phòng",
                DataPropertyName = "MaPhong",
            });

            dgvMain.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiPhong",
                HeaderText = "Loại phòng",
                DataPropertyName = "MaLoaiPhong",
            });

            dgvMain.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TinhTrang",
                HeaderText = "Tình trạng",
                DataPropertyName = "TinhTrang",
            });

            dgvMain.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GhiChu",
                HeaderText = "Ghi chú",
                DataPropertyName = "GhiChu",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Gán dữ liệu
            dgvMain.DataSource = listRoom;

            // Gán STT sau khi binding xong
            dgvMain.DataBindingComplete += (s, e) =>
            {
                for (int i = 0; i < dgvMain.Rows.Count; i++)
                {
                    dgvMain.Rows[i].Cells["STT"].Value = (i + 1).ToString();
                }
            };
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            //searching

            string keyword = tbSearch.Text.Trim().ToLower();

            // Nếu không nhập gì thì trả về danh sách gốc
            if (string.IsNullOrEmpty(keyword))
            {
                dgvMain.DataSource = listRoomOriginal;
            }
            else
            {
                // Lọc theo mã phòng
                var filtered = listRoomOriginal
                    .Where(r => r.MaPhong.ToLower().Contains(keyword))
                    .ToList();

                dgvMain.DataSource = filtered;
            }

            // Cập nhật lại STT
            for (int i = 0; i < dgvMain.Rows.Count; i++)
            {
                dgvMain.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listRoomOriginal == null || listRoomOriginal.Count == 0)
                return;

            string value = cbFilter.SelectedItem.ToString().Trim();

            List<Phong> filtered;

            if (value == "Tất cả")
            {
                filtered = listRoomOriginal;
            }
            else
            {
                filtered = listRoomOriginal
                    .Where(r =>
                        r.MaLoaiPhong.Trim() == value ||
                        r.TinhTrang.Trim() == value
                    )
                    .ToList();
            }

            dgvMain.DataSource = filtered;

            // Update STT
            for (int i = 0; i < dgvMain.Rows.Count; i++)
            {
                dgvMain.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
        }

    }
}
