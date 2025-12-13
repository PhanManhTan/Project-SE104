using System;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class RoomTypeUpdate : Form
    {
        private readonly RoomService roomService = new RoomService();
        private readonly string maLoaiPhong;

        public RoomTypeUpdate(string maLoaiPhong)
        {
            InitializeComponent();
            this.maLoaiPhong = maLoaiPhong;
        }

        private void RoomTypes_UpdateForm_Load(object sender, EventArgs e)
        {
            btnUpdate.DialogResult = DialogResult.None;
            var lp = roomService.GetRoomTypeById(maLoaiPhong);
            if (lp != null)
            {
                txtMaLP.Text = lp.MaLoaiPhong;
                txtTenLP.Text = lp.TenLoaiPhong;
                txtDG.Text = lp.DonGia.ToString("N0");

                // --- ĐOẠN CODE MỚI ---
                // Kiểm tra xem mã này có đang được dùng không
                bool dangSuDung = roomService.IsRoomTypeInUse(maLoaiPhong);

                if (dangSuDung)
                {
                    // Nếu đang dùng -> Khóa không cho sửa mã
                    txtMaLP.Enabled = false;
                    // (Tùy chọn) Có thể hiện tooltip hoặc label báo cho người dùng biết tại sao bị khóa
                    this.Text = "Cập nhật loại phòng (Mã đang sử dụng - Không thể đổi)";
                }
                else
                {
                    // Nếu chưa dùng -> Cho phép sửa mã thoải mái
                    txtMaLP.Enabled = true;
                    this.Text = "Cập nhật loại phòng";
                }
                // ---------------------
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1. Validate dữ liệu nhập vào
            if (string.IsNullOrWhiteSpace(txtMaLP.Text)) // Kiểm tra thêm Mã
            {
                MessageBox.Show("Vui lòng nhập Mã loại phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenLP.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên loại phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLP.Focus();
                return;
            }

            // Xử lý giá tiền (code đã sửa ở câu trước)
            string cleanPrice = txtDG.Text.Replace(".", "").Replace(",", "").Trim();
            if (!decimal.TryParse(cleanPrice, out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Tạo đối tượng cập nhật với Mã MỚI (lấy từ textbox)
            var updatedType = new LoaiPhong
            {
                MaLoaiPhong = txtMaLP.Text.Trim(), // Lấy mã mới nhỡ người dùng có sửa
                TenLoaiPhong = txtTenLP.Text.Trim(),
                DonGia = donGia
            };

            // 3. Gọi Service
            // Lưu ý: Tham số thứ 2 là 'maLoaiPhong' (biến global lưu mã CŨ ban đầu khi mở form)
            // Nếu txtMaLP khác maLoaiPhong cũ -> Service sẽ tự động xóa cũ thêm mới.
            if (roomService.UpdateRoomType(updatedType, maLoaiPhong))
            {
                MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Trường hợp sửa mã trùng với một mã KHÁC đã tồn tại trong CSDL
                MessageBox.Show("Cập nhật thất bại! Có thể mã loại phòng mới đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
