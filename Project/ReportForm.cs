using System;
using System.Drawing;
using System.Windows.Forms;
using Services;

namespace Project
{
    public partial class ReportForm : Form
    {
        private readonly ReportService reportService = new ReportService();

        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            SetupForm();
        }

        private void SetupForm()
        {
            // 1. Fill dữ liệu tháng (1 -> 12)
            for (int i = 1; i <= 12; i++)
            {
                cboMonth.Items.Add(i);
            }
            cboMonth.SelectedIndex = DateTime.Now.Month - 1; // Chọn tháng hiện tại
            numYear.Value = DateTime.Now.Year; // Chọn năm hiện tại

            // 2. Format Grid
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.ReadOnly = true;
            dgvReport.AllowUserToAddRows = false;
            dgvReport.RowHeadersVisible = false;
            dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Font & Màu sắc (giống các form khác của bạn)
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvReport.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            int month = int.Parse(cboMonth.SelectedItem.ToString());
            int year = (int)numYear.Value;

            var data = reportService.GetReportByMonth(month, year);

            // Tạo DataTable để hiển thị có STT
            var dt = new System.Data.DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("TenLoaiPhong", typeof(string));
            dt.Columns.Add("DoanhThu", typeof(decimal));
            dt.Columns.Add("TyLe", typeof(float));

            int stt = 1;
            foreach (var item in data)
            {
                dt.Rows.Add(stt++, item.TenLoaiPhong, item.DoanhThu, item.TyLe);
            }

            dgvReport.DataSource = dt;

            // Đặt tên cột tiếng Việt
            dgvReport.Columns["TenLoaiPhong"].HeaderText = "Loại Phòng";
            dgvReport.Columns["DoanhThu"].HeaderText = "Doanh Thu";
            dgvReport.Columns["TyLe"].HeaderText = "Tỷ Lệ (%)";
            dgvReport.Columns["STT"].Width = 50;

            // Format tiền tệ và %
            dgvReport.Columns["DoanhThu"].DefaultCellStyle.Format = "N0"; // Có dấu phẩy ngăn cách
            dgvReport.Columns["TyLe"].DefaultCellStyle.Format = "N2";     // 2 số lẻ thập phân

            // Canh lề
            dgvReport.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReport.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvReport.Columns["TyLe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (data.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu hóa đơn trong tháng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}