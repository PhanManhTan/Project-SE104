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
    public partial class RoomManeger : Form
    {
        public RoomManeger()
        {
            InitializeComponent();
            LoadDanhSachPhong();

        }

        private void RoomManeger_Load(object sender, EventArgs e)
        {
            //btnClose.Enabled = true;
            //btnCreate.Enabled = true;
            //btnDelete.Enabled = false;
            //btnUpdate.Enabled = false;
            LoadDanhSachPhong();

        }
        private Phong phongDangChon = null;
        private void LoadDanhSachPhong()
        {
            RoomService roomService = new RoomService();
            List<Phong> listRoom = roomService.GetAllRooms();

            dgvRoomManeger.AutoGenerateColumns = false;
            dgvRoomManeger.Columns.Clear();
            dgvRoomManeger.RowHeadersVisible = false;

            // Cột STT - Căn giữa, chữ đậm
            var colSTT = new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "STT",
                Width = 60,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                }
            };
            dgvRoomManeger.Columns.Add(colSTT);

            // Cột Mã phòng - Căn giữa
            dgvRoomManeger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaPhong",
                HeaderText = "Mã phòng",
                DataPropertyName = "MaPhong",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Cột Loại phòng - Căn giữa
            dgvRoomManeger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiPhong",
                HeaderText = "Loại phòng",
                DataPropertyName = "MaLoaiPhong",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Cột Tình trạng - Căn giữa, chữ đậm nổi bật
            dgvRoomManeger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TinhTrang",
                HeaderText = "Tình trạng",
                DataPropertyName = "TinhTrang",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                }
            });

            // Cột Ghi chú - Căn trái (dễ đọc khi dài), giãn hết phần còn lại
            dgvRoomManeger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GhiChu",
                HeaderText = "Ghi chú",
                DataPropertyName = "GhiChu",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft   // Căn trái cho ghi chú
                },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Gán dữ liệu
            dgvRoomManeger.DataSource = listRoom;

            // Đánh số thứ tự STT
            for (int i = 0; i < dgvRoomManeger.Rows.Count; i++)
            {
                dgvRoomManeger.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }

            // Đổi màu dòng theo tình trạng
            foreach (DataGridViewRow row in dgvRoomManeger.Rows)
            {
                if (row.Cells["TinhTrang"].Value == null) continue;

                string tinhTrang = row.Cells["TinhTrang"].Value.ToString();
                switch (tinhTrang)
                {
                    case "Trống":
                    case "Sẵn sàng":
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                        break;
                    case "Đã đặt":
                    case "Đang sử dụng":
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                        break;
                    case "Đang dọn":
                    case "Bảo trì":
                        row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        row.DefaultCellStyle.ForeColor = Color.DarkOrange;
                        break;
                }
                row.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            }

            dgvRoomManeger.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoomManeger.MultiSelect = false;
            dgvRoomManeger.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoomManeger.AllowUserToResizeRows = false;

            // Header đẹp và KHÔNG đổi màu khi select
            dgvRoomManeger.EnableHeadersVisualStyles = false;
            dgvRoomManeger.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRoomManeger.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvRoomManeger.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvRoomManeger.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRoomManeger.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;   // Quan trọng!
            dgvRoomManeger.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void dgvRoomManeger_SelectionChanged_1(object sender, EventArgs e)
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

        // Bạn có thể thêm hàm refresh khi cần (sau thêm/sửa/xóa)
        public void RefreshGrid()
        {
            LoadDanhSachPhong();
            // Sau khi refresh, tự động tắt button vì chưa chọn dòng nào
            phongDangChon = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            using (var frm = new RoomTypesForm())
            {
                if (frm.ShowDialog() == DialogResult.OK) this.Close();

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DetailRoom detailRoom = new DetailRoom(phongDangChon);
            detailRoom.ShowDialog();
            LoadDanhSachPhong();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Are you sure you want to delete this Room?",
            "Warning!",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
            );
            if (result == DialogResult.Yes) { 
                RoomService roomService = new RoomService();
                roomService.DeleteRoom(phongDangChon.MaPhong);
            }
            LoadDanhSachPhong();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            DetailRoom room = new DetailRoom();
            room.ShowDialog();
            LoadDanhSachPhong();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
