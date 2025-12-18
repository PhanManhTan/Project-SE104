using System;
using System.Drawing;
using System.Linq; 
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class RoomDetailForm : Form
    {
        private Phong cur = null;
        private readonly RoomService roomService = new RoomService();

        public RoomDetailForm()
        {
            InitializeComponent();
            this.Text = "Thêm phòng mới";
        }

        public RoomDetailForm(Phong phong)
        {
            InitializeComponent();
            cur = phong;
            this.Text = "Sửa thông tin phòng";
        }

        private void DetailRoom_Load(object sender, EventArgs e)
        {
            // Load loại phòng
            var loaiPhongList = roomService.GetAllRoomTypes();
            cbTypeRoom.DisplayMember = "TenLoaiPhong";
            cbTypeRoom.ValueMember = "MaLoaiPhong";
            cbTypeRoom.DataSource = loaiPhongList;

            // Load tình trạng
            cbTinhTrang.Items.Clear();
            cbTinhTrang.Items.Add("Trống");
            cbTinhTrang.Items.Add("Đã thuê");
            cbTinhTrang.Items.Add("Đang dọn");

            if (cur != null)
            {
                // Chế độ SỬA
                tbMaPhong.Text = cur.MaPhong?.Trim() ?? "";
                tbNote.Text = cur.GhiChu ?? "";
                cbTinhTrang.Text = cur.TinhTrang?.Trim() ?? "Trống";

                if (!string.IsNullOrEmpty(cur.MaLoaiPhong))
                {
                    cbTypeRoom.SelectedValue = cur.MaLoaiPhong.Trim();
                }

                UpdateDonGia();
            }
            else
            {

                tbMaPhong.Text = roomService.GenerateNewMaPhong();
                
                tbNote.Text = "";
                cbTinhTrang.SelectedIndex = 0; 
                cbTypeRoom.SelectedIndex = -1;
                tbDonGia.Text = "";
            }


            tbMaPhong.Enabled = false;
            tbMaPhong.BackColor = SystemColors.Window;
            tbMaPhong.TabStop = false; 


            cbTypeRoom.Focus();
        }

        private void cbTypeRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDonGia();
        }

        private void UpdateDonGia()
        {
            if (cbTypeRoom.SelectedValue != null)
            {
                string maLoai = cbTypeRoom.SelectedValue.ToString();
                var loaiPhong = roomService.GetRoomTypeById(maLoai);
                tbDonGia.Text = loaiPhong != null ? loaiPhong.DonGia.ToString("N0") + " đ" : "0 đ";
            }
            else
            {
                tbDonGia.Text = "";
            }
        }

        /// <summary>
        /// Tự động sinh mã phòng mới dạng P001, P002, P003,...
        /// </summary>


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (cbTypeRoom.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbTypeRoom.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbMaPhong.Text))
            {
                MessageBox.Show("Mã phòng không hợp lệ!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Phong phong = new Phong
            {
                MaPhong = tbMaPhong.Text.Trim(),
                MaLoaiPhong = cbTypeRoom.SelectedValue.ToString().Trim(),
                GhiChu = string.IsNullOrWhiteSpace(tbNote.Text) ? null : tbNote.Text.Trim(),
                TinhTrang = cbTinhTrang.Text.Trim()
            };

            bool success = cur != null
                ? roomService.UpdateRoom(phong)
                : roomService.AddRoom(phong);

            string action = cur != null ? "cập nhật" : "thêm mới";

            if (success)
            {
                MessageBox.Show($"Phòng đã được {action} thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show($"Không thể {action} phòng!\n\nLý do phổ biến:\n• Mã phòng đã tồn tại\n• Lỗi kết nối CSDL",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}