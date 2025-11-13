using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormPhong
{
    public partial class Phong : Form
    {
        private string connString = "Data Source=ACER\\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        private string selectedMaPhong = "";

        public Phong()
        {
            InitializeComponent();
            this.Load += Phong_Load;
        }

        private void Phong_Load(object sender, EventArgs e)
        {

            SetupForm();
            LoadData();
            dgvPhong.SelectionChanged += dgvPhong_SelectionChanged;

           
        }


        private void SetupForm()
        {
            // === Cấu hình chung DataGridView ===
            dgvPhong.RowHeadersVisible = false;
            dgvPhong.AllowUserToAddRows = false;
            dgvPhong.ReadOnly = true;
            dgvPhong.MultiSelect = false;
            dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhong.ScrollBars = ScrollBars.Vertical;
            dgvPhong.EnableHeadersVisualStyles = false;

            // === Header ===
            dgvPhong.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvPhong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPhong.ColumnHeadersHeight = 30;
            dgvPhong.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvPhong.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // === Dòng xen kẽ + chọn dòng ===
            dgvPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvPhong.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvPhong.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            // === Font dòng ===
            dgvPhong.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            // === Tăng độ cao dòng ===
            dgvPhong.RowTemplate.Height = 40;

            // === Cấm kéo cột/dòng ===
            dgvPhong.AllowUserToResizeColumns = false;
            dgvPhong.AllowUserToResizeRows = false;
            dgvPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void LoadData()
        {
            try
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_Phong ORDER BY MaPhong", connString))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPhong.DataSource = dt;
                    ConfigureColumns();
                    dgvPhong.ClearSelection();
                    selectedMaPhong = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void ConfigureColumns()
        {

            if (dgvPhong.Columns["STT"] != null)
            {
                var col = dgvPhong.Columns["STT"];
                col.HeaderText = "STT";
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 15; 
            }

            if (dgvPhong.Columns["MaPhong"] != null)
            {
                var col = dgvPhong.Columns["MaPhong"];
                col.HeaderText = "Mã phòng";
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 25;
            }
            if (dgvPhong.Columns["MaLoaiPhong"] != null)
            {
                var col = dgvPhong.Columns["MaLoaiPhong"];
                col.HeaderText = "Mã loại phòng";
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 80;
            }
            if (dgvPhong.Columns["DonGia"] != null)
            {
                var col = dgvPhong.Columns["DonGia"];
                col.HeaderText = "Đơn giá";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Format = "N0";
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 40;
            }
            if (dgvPhong.Columns["TinhTrangPhong"] != null)
            {
                var col = dgvPhong.Columns["TinhTrangPhong"];
                col.HeaderText = "Tình trạng phòng";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 20;
            }
            if (dgvPhong.Columns["GhiChu"] != null)
            {
                var col = dgvPhong.Columns["GhiChu"];
                col.HeaderText = "Ghi chú";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 100;
            }

            // === HEADER: NỀN TRẮNG, CHỮ ĐEN ĐẬM ===
            dgvPhong.EnableHeadersVisualStyles = false;
            dgvPhong.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvPhong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvPhong.ColumnHeadersHeight = 30;
            dgvPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // TẮT HIỆU ỨNG KHI CLICK HEADER
            dgvPhong.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvPhong.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // === DÒNG XEN KẼ + CHỌN DÒNG ===
            dgvPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvPhong.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvPhong.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            // CHỮ TRONG DÒNG
            dgvPhong.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            // TĂNG ĐỘ CAO DÒNG
            dgvPhong.RowTemplate.Height = 40;

            // Cho phép cột tự co giãn khi form resize
            dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhong.SelectedRows.Count > 0)
            {
                var row = dgvPhong.SelectedRows[0];
                if (!row.IsNewRow && row.Cells["MaPhong"].Value != null)
                {
                    selectedMaPhong = row.Cells["MaPhong"].Value.ToString();
                }
            }    
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var f = new CreatePhongForm(connString))
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaPhong))
            {
                MessageBox.Show("Vui lòng chọn phòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var f = new UpdatePhongForm(connString, selectedMaPhong))
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaPhong)) return;
            if (MessageBox.Show($"Xóa phòng {selectedMaPhong}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection c = new SqlConnection(connString))
                    {
                        c.Open();
                        new SqlCommand("DELETE FROM Phong WHERE MaPhong = @ma", c)
                        {
                            Parameters = { new SqlParameter("@ma", selectedMaPhong) }
                        }.ExecuteNonQuery();
                    }
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch
                {
                    MessageBox.Show("Không thể xóa: Phòng đang được sử dụng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Phong_Load_1(object sender, EventArgs e)
        {

        }
        private void btnLoaiPhong_Click(object sender, EventArgs e)
        {
            using (var f = new LoaiPhong())
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }
    }
}
