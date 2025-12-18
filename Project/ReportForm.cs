using Data;
using Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class ReportForm : Form
    {
        private readonly ReportService reportService = new ReportService();
        private readonly BindingSource bindingSource = new BindingSource();

        public ReportForm()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns();
            SetupControls();


            dgvBody.CellFormatting += DgvReport_CellFormatting;
        }


        private void SetupControls()
        {

            for (int i = 1; i <= 12; i++)
            {
                cboMonth.Items.Add(i.ToString("D2")); 
            }
            cboMonth.SelectedIndex = DateTime.Now.Month - 1;

            numYear.Minimum = 2000;
            numYear.Maximum = 2100;
            numYear.Value = DateTime.Now.Year;
        }


 
        private void SetupDataGridView()
        {
            var dgv = dgvBody;
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ScrollBars = ScrollBars.Vertical;

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            dgv.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);
            };
            dgv.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgv.InvalidateRow(e.RowIndex);
            };

            dgv.DataBindingComplete += DgvReport_DataBindingComplete;
            dgv.DataSource = bindingSource;
        }

        private void DgvReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvBody.Rows.Count; i++)
            {
                dgvBody.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
        }



        private void ConfigureDataGridViewColumns()
        {
            var dgv = dgvBody;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();
            // STT
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "STT",
                Width = 60,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            });
            // Loại phòng
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Loại Phòng",
                Name = "TenLoaiPhong",
                DataPropertyName = "TenLoaiPhong",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                }
            });
            // Doanh thu
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Doanh Thu",
                Name = "DoanhThu",
                DataPropertyName = "DoanhThu",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N0", 
                }
            });
            // Tỷ lệ
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tỷ Lệ",
                Name = "TyLe",
                DataPropertyName = "TyLe",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2",  
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                }
            });
        }



        private void btnXem_Click(object sender, EventArgs e)
        {
            if (cboMonth.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tháng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int month = int.Parse(cboMonth.SelectedItem.ToString());
            int year = (int)numYear.Value;
            var data = reportService.GetReportByMonth(month, year);
            bindingSource.DataSource = data.Any() ? data : null;
            if (!data.Any())
            {
                MessageBox.Show($"Không có dữ liệu doanh thu trong tháng {month:00}/{year}!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void DgvReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dgv = dgvBody;

            // Thêm " đ" cho cột DoanhThu
            if (e.ColumnIndex == dgv.Columns["DoanhThu"].Index && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal value))
                {
                    e.Value = value.ToString("N0") + " đ";
                    e.FormattingApplied = true;
                }
            }

            // Thêm "%" cho cột TyLe
            if (e.ColumnIndex == dgv.Columns["TyLe"].Index && e.Value != null)
            {
                if (double.TryParse(e.Value.ToString(), out double value))
                {
                    e.Value = value.ToString("N2") + "%";
                    e.FormattingApplied = true;
                }
            }
        }



    }
}