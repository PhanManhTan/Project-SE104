using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;        // THƯ VIỆN CHÍNH CHO FILE
using System.Text;
namespace FormLoaiPhong
{
    public partial class LoaiPhong : Form
    {
        string connString = "Data Source=DESKTOP-0A82EOD\\MSI;Initial Catalog=QLKS98;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        public LoaiPhong()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            SetupForm();
        }
        private void SetupForm()
        {
            
            // ComboBox – CHỈ CÓ THỂ SET BẰNG CODE (Items)
            // ĐÃ XÓA – SET TRONG DESIGNER
            dgvLoaiPhong.RowHeadersVisible = false;
            dgvLoaiPhong.AllowUserToAddRows = false;
            dgvLoaiPhong.ReadOnly = true;
            dgvLoaiPhong.MultiSelect = false;
            dgvLoaiPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiPhong.EnableHeadersVisualStyles = false;
            dgvLoaiPhong.ShowCellToolTips = false;
            dgvLoaiPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoaiPhong.AutoSize = false;
            dgvLoaiPhong.ScrollBars = ScrollBars.Vertical;
        }
        private void LoadData()
        {
            try
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_LoaiPhong", connString))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                  //  Tắt tạm sự kiện
                    dgvLoaiPhong.SelectionChanged -= dgvLoaiPhong_SelectionChanged;

                    dgvLoaiPhong.DataSource = dt;
                    ConfigureColumns();

                    dgvLoaiPhong.ClearSelection();
                    if (dt.Rows.Count > 0)
                    {
                        dgvLoaiPhong.Rows[0].Selected = true;
                        dgvLoaiPhong.CurrentCell = null;
                    }
                    else
                    {
                       // ClearInputs();
                    }

                   // Bật lại sự kiện
                   dgvLoaiPhong.SelectionChanged += dgvLoaiPhong_SelectionChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvLoaiPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ConfigureColumns()
        {
            // === CỘT STT ===
            if (dgvLoaiPhong.Columns["STT"] != null)
            {
                var col = dgvLoaiPhong.Columns["STT"];
                col.HeaderText = "STT";
                col.Width = 20;
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // === CỘT MÃ LOẠI ===
            if (dgvLoaiPhong.Columns["MaLoaiPhong"] != null)
            {
                var col = dgvLoaiPhong.Columns["MaLoaiPhong"];
                col.HeaderText = "Mã loại";
                col.Width = 110;
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // === CỘT ĐƠN GIÁ ===
            if (dgvLoaiPhong.Columns["DonGia"] != null)
            {
                var col = dgvLoaiPhong.Columns["DonGia"];
                col.HeaderText = "Đơn giá";
                col.Width = 140;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Format = "N0";
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // === CỘT TRẠNG THÁI ===
            if (dgvLoaiPhong.Columns["TrangThaiSuDung"] != null)
            {
                var col = dgvLoaiPhong.Columns["TrangThaiSuDung"];
                col.HeaderText = "Trạng thái";
                col.Width = 130;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
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

            // TẮT MÀU Ô RIÊNG LẺ (CHỈ CHỌN CẢ DÒNG)
            dgvLoaiPhong.DefaultCellStyle.SelectionBackColor = dgvLoaiPhong.DefaultCellStyle.BackColor;
            dgvLoaiPhong.DefaultCellStyle.SelectionForeColor = dgvLoaiPhong.DefaultCellStyle.ForeColor;

            // TĂNG ĐỘ CAO DÒNG
            dgvLoaiPhong.RowTemplate.Height = 40;

            dgvLoaiPhong.AllowUserToResizeColumns = false; // Cấm kéo ngang toàn bộ
            dgvLoaiPhong.AllowUserToResizeRows = false;    // Cấm kéo dọc toàn bộ
            dgvLoaiPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new CreateForm(connString))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        private void dgvLoaiPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiPhong.SelectedRows.Count > 0)
            {
                var row = dgvLoaiPhong.SelectedRows[0];
                if (!row.IsNewRow)
                    FillInputsFromRow(row);
            }
        }
        private void FillInputsFromRow(DataGridViewRow row)
        {
            if (row == null || row.Cells["MaLoaiPhong"].Value == null) return;

           // _oldMa = row.Cells["MaLoaiPhong"].Value.ToString().Trim();
          //  txtMa.Text = _oldMa;

            var donGia = row.Cells["DonGia"].Value;
          //  txtDonGia.Text = donGia != null ? donGia.ToString() : "";

            string trangThai = row.Cells["TrangThaiSuDung"].Value?.ToString() ?? "Đang sử dụng";
           // cboTrangThai.SelectedIndex = trangThai.Contains("Đang sử dụng") ? 0 : 1;

            //txtMa.Enabled = false; // Không cho sửa mã khi đang chọn
        }
    }
}
