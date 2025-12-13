using System;
using System.Drawing;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class RoomTypeManager : Form
    {
        private RoomService roomService = new RoomService();
        private string selectedMaLoai;

        public RoomTypeManager()
        {
            InitializeComponent();
        }

        private void RoomTypesForm_Load(object sender, EventArgs e)
        {
            SetupForm();
            LoadData();
            dgvLoaiPhong.SelectionChanged += DgvLoaiPhong_SelectionChanged;
        }

        private void SetupForm()
        {
            dgvLoaiPhong.RowHeadersVisible = false;
            dgvLoaiPhong.AllowUserToAddRows = false;
            dgvLoaiPhong.ReadOnly = true;
            dgvLoaiPhong.MultiSelect = false;
            dgvLoaiPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvLoaiPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvLoaiPhong.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvLoaiPhong.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgvLoaiPhong.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvLoaiPhong.RowTemplate.Height = 40;
        }

        private void LoadData()
        {
            // Quan trọng: Tạo mới service để lấy dữ liệu mới nhất từ DB
            roomService = new RoomService();

            // Lấy danh sách (đảm bảo chỉ khai báo biến list 1 lần)
            var list = roomService.GetAllRoomTypes();

            var dt = new System.Data.DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("MaLoaiPhong", typeof(string));
            dt.Columns.Add("TenLoaiPhong", typeof(string));
            dt.Columns.Add("DonGia", typeof(decimal));

            int stt = 1;
            foreach (var lp in list)
            {
                dt.Rows.Add(stt++, lp.MaLoaiPhong, lp.TenLoaiPhong, lp.DonGia);
            }

            // Gán DataSource trực tiếp, KHÔNG xóa ngay sau đó
            dgvLoaiPhong.DataSource = dt;

            // Định dạng cột
            if (dgvLoaiPhong.Columns["DonGia"] != null)
                dgvLoaiPhong.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (dgvLoaiPhong.Columns["STT"] != null)
                dgvLoaiPhong.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Clear selection
            dgvLoaiPhong.ClearSelection();
            selectedMaLoai = null;
        }


        private void DgvLoaiPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiPhong.SelectedRows.Count > 0)
            {
                selectedMaLoai = dgvLoaiPhong.SelectedRows[0].Cells["MaLoaiPhong"].Value?.ToString();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var frm = new RoomTypeCreate())
            {
                if (frm.ShowDialog() == DialogResult.OK) 
                    LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn một loại phòng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new RoomTypeUpdate(selectedMaLoai))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();  // Refresh ngay tại đây
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn một loại phòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa loại phòng \"{selectedMaLoai}\"?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (roomService.DeleteRoomType(selectedMaLoai))
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}