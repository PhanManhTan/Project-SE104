using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class KhachHang_MainForm : Form
    {
        //private string connString = "Data Source=DESKTOP-0A82EOD\\MSI;Initial Catalog=QLKS98;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        private string selectedMaKhach; // Mã khách hàng đang chọn
        string connString;
        public KhachHang_MainForm(string _connString)
        {
            InitializeComponent();
            this.connString = _connString;
        }

        private void KhachHang_MainForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            SetupForm();
            LoadData();

           // btnQLLK.Click += BtnQLLK_Click;
           // btnQLLK.Click += BtnQLLK_Click;
            //btnCreate.Click += btnCreate_Click;
            //btnUpdate.Click += btnUpdate_Click;
            //btnDelete.Click += btnDelete_Click;

            dgvKhachHang.SelectionChanged += DgvKhachHang_SelectionChanged;
        }

        private void SetupDataGridView()
        {
            dgvKhachHang.RowHeadersVisible = false;
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.MultiSelect = false;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.ScrollBars = ScrollBars.Vertical;
        }

        private void SetupForm()
        {
            dgvKhachHang.EnableHeadersVisualStyles = false;

            // Header
            dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvKhachHang.ColumnHeadersHeight = 30;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Dòng xen kẽ
            dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvKhachHang.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvKhachHang.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            // Font dòng
            dgvKhachHang.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            // Tăng độ cao dòng
            dgvKhachHang.RowTemplate.Height = 40;

            // Cấm kéo cột/dòng
            dgvKhachHang.AllowUserToResizeColumns = false;
            dgvKhachHang.AllowUserToResizeRows = false;
            dgvKhachHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void LoadData()
        {
            try
            {
                string query = @"
            SELECT KH.MaKhach, KH.HoTen, KH.CMND, KH.DiaChi, LK.TenLoaiKhach
            FROM KhachHang KH
            LEFT JOIN LoaiKhach LK ON KH.MaLoaiKhach = LK.MaLoaiKhach
            ORDER BY KH.MaKhach";

                using (SqlDataAdapter da = new SqlDataAdapter(query, connString))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Thêm cột STT vào đầu DataTable
                    dt.Columns.Add("STT", typeof(int));
                    dt.Columns["STT"].SetOrdinal(0); // Đặt cột STT ở vị trí đầu tiên

                    // Đánh số thứ tự
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["STT"] = i + 1;
                    }

                    dgvKhachHang.DataSource = dt;
                    dgvKhachHang.ClearSelection();
                    selectedMaKhach = null;
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
                if (dgvKhachHang.Columns[columnName] != null)
                {
                    var col = dgvKhachHang.Columns[columnName];
                    col.HeaderText = headerText;
                    col.DefaultCellStyle.Alignment = align;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.FillWeight = fillWeight;
                }
            }
            SetupColumn("STT", "STT", 7);
            SetupColumn("MaKhach", "Mã khách hàng", 15);
            SetupColumn("HoTen", "Họ tên", 25);
            SetupColumn("CMND", "CCCD", 15);
            SetupColumn("DiaChi", "Địa chỉ", 25);
            SetupColumn("TenLoaiKhach", "Loại khách", 20);
        }

        private void DgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count > 0)
            {
                var row = dgvKhachHang.SelectedRows[0];
                if (!row.IsNewRow && row.Cells["MaKhach"].Value != null)
                {
                    selectedMaKhach = row.Cells["MaKhach"].Value.ToString();
                }
            }
        }

        // ================= BUTTONS =================

        private void BtnQLLK_Click(object sender, EventArgs e)
        {
            using (var frm = new LoaiKhach_MainForm(connString))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                   LoadData();
                   
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var frm = new KhachHang_CreateForm(connString))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaKhach))
            {
                MessageBox.Show("Vui lòng chọn khách hàng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new KhachHang_UpdateForm(connString, selectedMaKhach))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaKhach))
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa khách hàng {selectedMaKhach}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = "DELETE FROM KhachHang WHERE MaKhach = @MaKhach";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaKhach", selectedMaKhach);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Không làm gì, chỉ để tồn tại event
        }
    }
}
