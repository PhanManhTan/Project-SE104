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
    public partial class QuyDinh : Form
    {
        private string connString = "Data Source=ACER\\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        public QuyDinh()
        {
            InitializeComponent();
            this.Load += QuyDinh_Load;
        }
        private void QuyDinh_Load(object sender, EventArgs e)
        {
            SetupForm();
            LoadData();
        }

        private void SetupForm()
        {
            // === Cấu hình chung DataGridView ===
            dgvQuyDinh.RowHeadersVisible = false;
            dgvQuyDinh.AllowUserToAddRows = false;
            dgvQuyDinh.ReadOnly = true;
            dgvQuyDinh.MultiSelect = false;
            dgvQuyDinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvQuyDinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvQuyDinh.ScrollBars = ScrollBars.Vertical;
            dgvQuyDinh.EnableHeadersVisualStyles = false;

            // === Header ===
            dgvQuyDinh.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvQuyDinh.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvQuyDinh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvQuyDinh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvQuyDinh.ColumnHeadersHeight = 30;
            dgvQuyDinh.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvQuyDinh.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // === Dòng xen kẽ + chọn dòng ===
            dgvQuyDinh.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvQuyDinh.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvQuyDinh.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            // === Font dòng ===
            dgvQuyDinh.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            // === Tăng độ cao dòng ===
            dgvQuyDinh.RowTemplate.Height = 40;

            // === Cấm kéo cột/dòng ===
            dgvQuyDinh.AllowUserToResizeColumns = false;
            dgvQuyDinh.AllowUserToResizeRows = false;
            dgvQuyDinh.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        
        }

        private void LoadData()
        {
            try
            {
                using (SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT MaQuyDinh, SoKhachToiDa, TyLePhuThu, NgayApDung FROM QuyDinh ORDER BY MaQuyDinh DESC", connString))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvQuyDinh.DataSource = dt;
                    ConfigureColumns();

                    // Tô đậm dòng đầu tiên (mã lớn nhất)
                    if (dgvQuyDinh.Rows.Count > 0)
                    {
                        var row = dgvQuyDinh.Rows[0];
                        row.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(0, 123, 255);
                    }

                    dgvQuyDinh.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void ConfigureColumns()
        {
            if (dgvQuyDinh.Columns["MaQuyDinh"] != null)
            {
                var col = dgvQuyDinh.Columns["MaQuyDinh"];
                col.HeaderText = "Mã quy định";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.FillWeight = 25;
            }
            if (dgvQuyDinh.Columns["SoKhachToiDa"] != null)
            {
                var col = dgvQuyDinh.Columns["SoKhachToiDa"];
                col.HeaderText = "Số khách tối đa";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.FillWeight = 70;
            }
            if (dgvQuyDinh.Columns["TyLePhuThu"] != null)
            {
                var col = dgvQuyDinh.Columns["TyLePhuThu"];
                col.HeaderText = "Phụ thu";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Format = "0'%'";
                col.FillWeight = 25;
            }
            if (dgvQuyDinh.Columns["NgayApDung"] != null)
            {
                var col = dgvQuyDinh.Columns["NgayApDung"];
                col.HeaderText = "Ngày áp dụng";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Format = "dd/MM/yyyy";
                col.FillWeight = 35;
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var f = new UpdateQuyDinh(connString))
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }
    }
}
