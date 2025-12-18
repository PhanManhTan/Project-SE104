using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class RentalsForm : Form
    {
        private RentalViewModel selectedRental = null;
        private BindingSource bindingSource = new BindingSource();

        public RentalsForm()
        {
            InitializeComponent();

            SetupDataGridView();
            ConfigureColumns();

            LoadRentals();
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


            dgv.MultiSelect = true;                    
            dgv.ScrollBars = ScrollBars.Vertical;      

            dgv.DataBindingComplete += (s, e) =>
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells["STT"] != null)
                        dgv.Rows[i].Cells["STT"].Value = (i + 1).ToString();
                }
            };


            dgv.CellDoubleClick += DgvRentalManager_CellDoubleClick;
            dgv.SelectionChanged += DgvRentalManager_SelectionChanged;
        }



        private void ConfigureColumns()
        {
            var dgv = dgvBody;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();


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
                DataPropertyName = "MaPhieuThue",
                HeaderText = "Mã Phiếu",
                Name = "MaPhieuThue",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaPhong",
                HeaderText = "Mã Phòng",
                Name = "MaPhong",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenKhachChinh",
                HeaderText = "Khách Đại Diện",
                Name = "TenKhachChinh",
                Width = 160,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayBatDauThue",
                HeaderText = "Ngày Thuê",
                Name = "NgayBatDauThue",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TinhTrang",
                HeaderText = "Trạng Thái",
                Name = "TinhTrang",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.DataSource = bindingSource;
        }





        private void LoadRentals(string search = null)
        {
            using (var service = new RentalService())
            {
                var list = string.IsNullOrEmpty(search)
                    ? service.SearchRentals()
                    : service.SearchRentals(search);

                bindingSource.DataSource = list;
            }
        }

        public void RefreshGrid()
        {
            LoadRentals(txtSearch.Text);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRentals(txtSearch.Text.Trim());
        }



        private void DgvRentalManager_SelectionChanged(object sender, EventArgs e)
        {
            int selectedCount = dgvBody.SelectedRows.Count;

            if (dgvBody.CurrentRow != null)
            {
                selectedRental = dgvBody.CurrentRow.DataBoundItem as RentalViewModel;
            }
        }



        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (dgvBody.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một phiếu để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<string> selectedRentals = new List<string>();
            string firstCustomerName = null;

            foreach (DataGridViewRow row in dgvBody.SelectedRows)
            {
                var rental = row.DataBoundItem as RentalViewModel;
                if (rental == null) continue;

                if (rental.TinhTrang == "Đã thanh toán")
                {
                    MessageBox.Show($"Phiếu {rental.MaPhieuThue} đã thanh toán rồi. Vui lòng bỏ chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvBody.ClearSelection();
                    return;
                }

                if (firstCustomerName == null)
                {
                    firstCustomerName = rental.TenKhachChinh;
                }
                else if (rental.TenKhachChinh != firstCustomerName)
                {
                    MessageBox.Show("Chỉ được thanh toán gộp các phiếu của cùng một khách đại diện!", "Lỗi khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    dgvBody.ClearSelection();
                    return;
                }

                selectedRentals.Add(rental.MaPhieuThue);
            }

            var paymentForm = new PaymentForm(selectedRentals);
            if (paymentForm.ShowDialog() == DialogResult.OK)
            {
                RefreshGrid();
                dgvBody.ClearSelection();
            }
        }

        private void DgvRentalManager_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) ShowDetail();
        }

        private void BtnViewDetail_Click(object sender, EventArgs e)
        {
            ShowDetail();
        }

        private void ShowDetail()
        {
            if (selectedRental == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu thuê để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var detailForm = new RentalDetailForm(selectedRental.MaPhieuThue, selectedRental.MaPhong, selectedRental.NgayBatDauThue);
            detailForm.ShowDialog();
        }


        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}