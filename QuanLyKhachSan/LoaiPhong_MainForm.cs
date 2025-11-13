using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class LoaiPhong_MainForm : Form
    {
        private string connString = ""; //"Data Source=DESKTOP-0A82EOD\\MSI;Initial Catalog=QLKS98;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        private string selectedMaLoai; // Lưu mã loại phòng đang chọn

        public LoaiPhong_MainForm(string _connString)
        {
            InitializeComponent();
            this.connString = connString;
            connString = _connString;
        }

        private void LoaiPhong_Load(object sender, EventArgs e)
        {
      
            SetupForm();
            LoadData();
               
            // Gán sự kiện khi chọn dòng trong DataGridView
            dgvLoaiPhong.SelectionChanged += DgvLoaiPhong_SelectionChanged;
        }

        private void SetupForm()
        {
            // === Cấu hình chung DataGridView ===
            dgvLoaiPhong.RowHeadersVisible = false;
            dgvLoaiPhong.AllowUserToAddRows = false;
            dgvLoaiPhong.ReadOnly = true;
            dgvLoaiPhong.MultiSelect = false;
            dgvLoaiPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoaiPhong.ScrollBars = ScrollBars.Vertical;
            dgvLoaiPhong.EnableHeadersVisualStyles = false;

            // === Header ===
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLoaiPhong.ColumnHeadersHeight = 30;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // === Dòng xen kẽ + chọn dòng ===
            dgvLoaiPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvLoaiPhong.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvLoaiPhong.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            // === Font dòng ===
            dgvLoaiPhong.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            // === Tăng độ cao dòng ===
            dgvLoaiPhong.RowTemplate.Height = 40;

            // === Cấm kéo cột/dòng ===
            dgvLoaiPhong.AllowUserToResizeColumns = false;
            dgvLoaiPhong.AllowUserToResizeRows = false;
            dgvLoaiPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void LoadData()
        {
            try
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_LoaiPhong", connString))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvLoaiPhong.DataSource = dt;

                    // Clear selection ban đầu
                    dgvLoaiPhong.ClearSelection();
                    selectedMaLoai = null;
                    ConfigureColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= XỬ LÝ CHỌN DÒNG =================
        private void DgvLoaiPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiPhong.SelectedRows.Count > 0)
            {
                var row = dgvLoaiPhong.SelectedRows[0];
                if (!row.IsNewRow && row.Cells["MaLoaiPhong"].Value != null)
                {
                    selectedMaLoai = row.Cells["MaLoaiPhong"].Value.ToString();
                }
            }
        }

        // ================= BUTTONS =================

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var frm = new LoaiPhong_CreateForm(connString))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        private void ConfigureColumns()
        {
            // === CỘT STT ===
            if (dgvLoaiPhong.Columns["STT"] != null)
            {
                var col = dgvLoaiPhong.Columns["STT"];
                col.HeaderText = "STT";
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 10; // tỷ lệ chiếm cột
            }

            // === CỘT MÃ LOẠI ===
            if (dgvLoaiPhong.Columns["MaLoaiPhong"] != null)
            {
                var col = dgvLoaiPhong.Columns["MaLoaiPhong"];
                col.HeaderText = "Mã loại phòng";
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 25;
            }

            // === CỘT ĐƠN GIÁ ===
            if (dgvLoaiPhong.Columns["DonGia"] != null)
            {
                var col = dgvLoaiPhong.Columns["DonGia"];
                col.HeaderText = "Đơn giá";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Format = "N0";
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 20;
            }

            // === CỘT TRẠNG THÁI ===
            if (dgvLoaiPhong.Columns["TrangThaiSuDung"] != null)
            {
                var col = dgvLoaiPhong.Columns["TrangThaiSuDung"];
                col.HeaderText = "Trạng thái sử dụng";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 20;
            }

            // === HEADER: NỀN TRẮNG, CHỮ ĐEN ĐẬM ===
            dgvLoaiPhong.EnableHeadersVisualStyles = false;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvLoaiPhong.ColumnHeadersHeight = 30;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // TẮT HIỆU ỨNG KHI CLICK HEADER
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvLoaiPhong.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // === DÒNG XEN KẼ + CHỌN DÒNG ===
            dgvLoaiPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvLoaiPhong.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvLoaiPhong.RowsDefaultCellStyle.SelectionForeColor = Color.White; 

            // CHỮ TRONG DÒNG
            dgvLoaiPhong.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            // TĂNG ĐỘ CAO DÒNG
            dgvLoaiPhong.RowTemplate.Height = 40;

            // Cho phép cột tự co giãn khi form resize
            dgvLoaiPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn một loại phòng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new LoaiPhong_UpdateForm(connString, selectedMaLoai))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn một loại phòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CoPhongDangDungLoai(selectedMaLoai))
            {
                MessageBox.Show($"Không thể xóa loại phòng {selectedMaLoai} vì đang có phòng sử dụng!",
                                "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa loại phòng {selectedMaLoai}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = "DELETE FROM LoaiPhong WHERE MaLoaiPhong = @MaLoai";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaLoai", selectedMaLoai);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool CoPhongDangDungLoai(string maLoai)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Phong WHERE MaLoaiPhong = @ma";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", maLoai);
                        return (int)cmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
