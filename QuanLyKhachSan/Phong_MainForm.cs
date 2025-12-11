using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class Phong_MainForm : Form
    {
        private string selectedMaPhong; // Mã phòng đang chọn
        string connString;

        public Phong_MainForm(string _connString)
        {
            InitializeComponent();
            this.connString = _connString;
        }

        private void Phong_MainForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            SetupForm();
            LoadData();
            dgvPhong.SelectionChanged += DgvPhong_SelectionChanged;
        }

        private void SetupDataGridView()
        {
            dgvPhong.RowHeadersVisible = false;
            dgvPhong.AllowUserToAddRows = false;
            dgvPhong.ReadOnly = true;
            dgvPhong.MultiSelect = false;
            dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhong.ScrollBars = ScrollBars.Vertical;
        }

        private void SetupForm()
        {
            dgvPhong.EnableHeadersVisualStyles = false;
            // Header
            dgvPhong.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvPhong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPhong.ColumnHeadersHeight = 30;
            dgvPhong.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvPhong.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            // Dòng xen kẽ
            dgvPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvPhong.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvPhong.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            // Font dòng
            dgvPhong.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            // Tăng độ cao dòng
            dgvPhong.RowTemplate.Height = 40;
            // Cấm kéo cột/dòng
            dgvPhong.AllowUserToResizeColumns = false;
            dgvPhong.AllowUserToResizeRows = false;
            dgvPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM vw_Phong ORDER BY MaPhong";
                using (SqlDataAdapter da = new SqlDataAdapter(query, connString))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPhong.DataSource = dt;
                    dgvPhong.ClearSelection();
                    selectedMaPhong = null;
                    ConfigureColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureColumns()
        {
            void SetupColumn(string columnName, string headerText, int fillWeight = 20, DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleCenter)
            {
                if (dgvPhong.Columns.Contains(columnName))
                {
                    var col = dgvPhong.Columns[columnName];
                    col.HeaderText = headerText;
                    col.DefaultCellStyle.Alignment = align;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.FillWeight = fillWeight;

                    // Format riêng cho DonGia
                    if (columnName == "DonGia")
                    {
                        col.DefaultCellStyle.Format = "N0";
                    }
                }
            }

            SetupColumn("STT", "STT", 15);
            SetupColumn("MaPhong", "Mã phòng", 25);
            SetupColumn("MaLoaiPhong", "Mã loại phòng", 30);
            SetupColumn("DonGia", "Đơn giá", 25);
            SetupColumn("TinhTrangPhong", "Tình trạng", 10);
            SetupColumn("GhiChu", "Ghi chú", 35);
        }

        private void DgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhong.SelectedRows.Count > 0)
            {
                var row = dgvPhong.SelectedRows[0];
                if (!row.IsNewRow && row.Cells["MaPhong"].Value != null)
                {
                    selectedMaPhong = row.Cells["MaPhong"].Value.ToString();
                }
            }
            else
            {
                selectedMaPhong = null;
            }
        }

        // ================= BUTTONS =================

        private void btnLoaiPhong_Click(object sender, EventArgs e)
        {
            using (var frm = new LoaiPhong_MainForm(connString))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var frm = new Phong_CreateForm(connString))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaPhong))
            {
                MessageBox.Show("Vui lòng chọn phòng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new Phong_UpdateForm(connString, selectedMaPhong))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaPhong))
            {
                MessageBox.Show("Vui lòng chọn phòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa phòng {selectedMaPhong}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Phong WHERE MaPhong = @MaPhong";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaPhong", selectedMaPhong);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa phòng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    MessageBox.Show("Không thể xóa: Phòng đang được sử dụng trong đặt phòng hoặc hóa đơn!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Event trống nếu cần giữ trong Designer
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Không làm gì
        }

    }
}