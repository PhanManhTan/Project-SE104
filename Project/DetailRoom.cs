using Data;
using Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class DetailRoom : Form
    {
        private Phong cur = null;
        private readonly RoomService roomService = new RoomService();

        public DetailRoom()
        {
            InitializeComponent();
            this.Text = "Thêm phòng mới";
        }

        public DetailRoom(Phong phong)
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

            // Load tình trạng (trực tiếp dùng giá trị DB)
            cbTinhTrang.Items.Clear();
            cbTinhTrang.Items.Add("Trống");
            cbTinhTrang.Items.Add("Đã thuê");
            cbTinhTrang.Items.Add("Đang dọn");

            if (cur != null)
            {
                // Chế độ SỬA
                tbMaPhong.Text = cur.MaPhong;
                tbMaPhong.ReadOnly = true;
                tbMaPhong.BackColor = SystemColors.Window;
                tbMaPhong.TabStop = false;

                tbNote.Text = cur.GhiChu ?? "";
                cbTinhTrang.Text = cur.TinhTrang?.Trim() ?? "Trống"; // Lấy trực tiếp từ DB

                if (!string.IsNullOrEmpty(cur.MaLoaiPhong))
                {
                    cbTypeRoom.SelectedValue = cur.MaLoaiPhong.Trim();
                }

                UpdateDonGia();
            }
            else
            {
                // Chế độ THÊM MỚI
                tbMaPhong.Text = GenerateNewMaPhong();
                tbMaPhong.ReadOnly = true;
                tbMaPhong.BackColor = SystemColors.Window;
                tbMaPhong.TabStop = false;

                tbNote.Text = "";
                cbTinhTrang.Text = "Trống"; // Mặc định theo DB
                cbTypeRoom.SelectedIndex = -1;
                tbDonGia.Text = "";
            }
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

        private string GenerateNewMaPhong()
        {
            var lastRoom = roomService.GetAllRooms()
                .OrderByDescending(p => p.MaPhong)
                .FirstOrDefault();

            if (lastRoom == null || string.IsNullOrEmpty(lastRoom.MaPhong))
                return "P001";

            string lastCode = lastRoom.MaPhong.Trim().ToUpper();
            if (lastCode.StartsWith("P") && int.TryParse(lastCode.Substring(1), out int num))
            {
                return "P" + (num + 1).ToString("D3");
            }
            return "P001";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (cbTypeRoom.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Phong phong = new Phong
            {
                MaPhong = tbMaPhong.Text.Trim(),
                MaLoaiPhong = cbTypeRoom.SelectedValue.ToString().Trim(),
                GhiChu = tbNote.Text.Trim(),
                TinhTrang = cbTinhTrang.Text.Trim() // Lưu trực tiếp giá trị hiển thị (đã đồng bộ với DB)
            };

            bool success = cur != null
                ? roomService.UpdateRoom(phong)
                : roomService.AddRoom(phong);

            string action = cur != null ? "cập nhật" : "thêm";

            if (success)
            {
                MessageBox.Show($"Phòng đã được {action} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show($"Không thể {action} phòng. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}