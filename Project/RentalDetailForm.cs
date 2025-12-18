using Data;
using Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class RentalDetailForm : Form
    {
        private readonly string _maPhieu;

        public RentalDetailForm(string maPhieu, string maPhong, DateTime ngayThue)
        {
            InitializeComponent();

            _maPhieu = maPhieu?.Trim() ?? string.Empty;

            SetupFormHeader(maPhong, ngayThue);
            SetupDataGridView();
            ConfigureColumns();

            LoadCustomers();
        }


        private void SetupFormHeader(string maPhong, DateTime ngayThue)
        {
            lblInfo.Text = $"PHIẾU THUÊ: {_maPhieu} | PHÒNG: {maPhong} | NGÀY THUÊ: {ngayThue:dd/MM/yyyy}";
            this.Text = $"Chi tiết phiếu thuê - {_maPhieu}";
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


            dgv.DataBindingComplete += (s, e) =>
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells["STT"].Value = (i + 1).ToString();
                }
                dgv.ClearSelection();
            };
        }


        private void ConfigureColumns()
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
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaKhach",
                HeaderText = "Mã khách",
                DataPropertyName = "MaKhach",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                HeaderText = "Họ tên",
                DataPropertyName = "HoTen",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CMND",
                HeaderText = "CMND/CCCD",
                DataPropertyName = "CMND",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenLoaiKhach",
                HeaderText = "Loại khách",
                DataPropertyName = "TenLoaiKhach",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaChi",
                HeaderText = "Địa chỉ",
                DataPropertyName = "DiaChi",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                }
            });
        }



        private void LoadCustomers()
        {
            if (string.IsNullOrEmpty(_maPhieu))
            {
                MessageBox.Show("Mã phiếu không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var service = new RentalService())
            {
                var customers = service.GetCustomersByRental(_maPhieu);

                if (customers == null || customers.Count == 0)
                {
                    MessageBox.Show("Không có thông tin khách hàng nào cho phiếu thuê này.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvBody.DataSource = null;
                    return;
                }

                dgvBody.DataSource = customers;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DetailRental_Load(object sender, EventArgs e)
        {

        }

    }
}