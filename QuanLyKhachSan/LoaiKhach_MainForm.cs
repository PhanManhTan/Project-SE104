using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class LoaiKhach_MainForm : Form
    {
        private string selectedMaLoai = null;
        private readonly string connString;

        public LoaiKhach_MainForm(string _connString)
        {
            InitializeComponent();
            connString = _connString;
        }

        private void LoaiKhach_MainForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            SetupForm();
            LoadData();
            dgvLoaiKhach.SelectionChanged += DgvLoaiKhach_SelectionChanged;
        }

        private void SetupDataGridView()
        {
            dgvLoaiKhach.RowHeadersVisible = false;
            dgvLoaiKhach.AllowUserToAddRows = false;
            dgvLoaiKhach.ReadOnly = true;
            dgvLoaiKhach.MultiSelect = false;
            dgvLoaiKhach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiKhach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoaiKhach.ScrollBars = ScrollBars.Vertical;
        }

        private void SetupForm()
        {
            // Header
            dgvLoaiKhach.EnableHeadersVisualStyles = false;
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLoaiKhach.ColumnHeadersHeight = 30;
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Dòng xen kẽ + chọn dòng
            dgvLoaiKhach.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvLoaiKhach.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvLoaiKhach.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            // Font dòng
            dgvLoaiKhach.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            // Tăng độ cao dòng
            dgvLoaiKhach.RowTemplate.Height = 40;

            // Cấm kéo cột/dòng
            dgvLoaiKhach.AllowUserToResizeColumns = false;
            dgvLoaiKhach.AllowUserToResizeRows = false;
            dgvLoaiKhach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void LoadData()
        {
            try
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_LoaiKhach", connString))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    //// Thêm cột STT
                    //if (!dt.Columns.Contains("STT"))
                    //    dt.Columns.Add("STT", typeof(int));
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //    dt.Rows[i]["STT"] = i + 1;

                    dgvLoaiKhach.DataSource = dt;

                    dgvLoaiKhach.ClearSelection();
                    selectedMaLoai = null;

                    ConfigureColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureColumns()
        {
            void SetupColumn(string columnName, string headerText, int fillWeight = 20, DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleCenter)
            {
                if (dgvLoaiKhach.Columns[columnName] != null)
                {
                    var col = dgvLoaiKhach.Columns[columnName];
                    col.HeaderText = headerText;
                    col.DefaultCellStyle.Alignment = align;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.FillWeight = fillWeight;
                }
            }

            SetupColumn("STT", "STT", 5);
            SetupColumn("MaLoaiKhach", "Mã loại khách", 15);
            SetupColumn("TenLoaiKhach", "Tên loại khách", 25);
            SetupColumn("TrangThaiSuDung", "Trạng thái sử dụng", 15);
        }

        private void DgvLoaiKhach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiKhach.SelectedRows.Count > 0)
            {
                var row = dgvLoaiKhach.SelectedRows[0];
                if (!row.IsNewRow && row.Cells["MaLoaiKhach"].Value != null)
                    selectedMaLoai = row.Cells["MaLoaiKhach"].Value.ToString();
            }
        }

        // ================= BUTTONS ====================

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var frm = new LoaiKhach_CreateForm(connString))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn 1 loại khách để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new LoaiKhach_UpdateForm(connString, selectedMaLoai))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn 1 loại khách để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa loại khách {selectedMaLoai}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "DELETE FROM LoaiKhach WHERE MaLoaiKhach=@MaLoai";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLoai", selectedMaLoai);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                MessageBox.Show("Xóa thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
