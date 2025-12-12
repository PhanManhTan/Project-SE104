using System;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class RoomTypes_CreateForm : Form
    {
        private readonly RoomService roomService = new RoomService();

        public RoomTypes_CreateForm()
        {
            InitializeComponent();
            FormSetup();
        }

        private void FormSetup()
        {
            this.Text = "Thêm mới loại phòng";
            // Không cần combo trạng thái nữa
        }

        private void ClearInputs()
        {
            txtMaLP.Clear();
            txtTenLP.Clear();
            txtDG.Clear();
            txtMaLP.Focus();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLP.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã loại phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLP.Focus();
                return;
            }

            if (txtMaLP.Text.Trim().Length > 10)
            {
                MessageBox.Show("Mã loại phòng không được quá dài!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenLP.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên loại phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLP.Focus();
                return;
            }

            if (!decimal.TryParse(txtDG.Text, out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số dương.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDG.Focus();
                return;
            }

            var newType = new LoaiPhong
            {
                MaLoaiPhong = txtMaLP.Text.Trim(),
                TenLoaiPhong = txtTenLP.Text.Trim(),
                DonGia = donGia
            };

            if (roomService.AddRoomType(newType))
            {
                MessageBox.Show("Thêm mới loại phòng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void RoomTypes_CreateForm_Load(object sender, EventArgs e)
        {
            btnCreate.DialogResult = DialogResult.None;
            ClearInputs();
        }
    }
}