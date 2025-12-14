using Data;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class InsertCustomer : Form
    {
        public InsertCustomer()
        {
            InitializeComponent();
        }
        private CustomerViewModel selectedCustomerView = null;
        private BindingSource bindingSource = new BindingSource();
        private void InsertCustomer_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns(); // Gọi một lần duy nhất ở đây
            LoadCustomers();
        }

        #region === CẤU HÌNH GIAO DIỆN DATAGRIDVIEW ===
        private void SetupDataGridView()
        {
            var dgv = dgvInsertCustomer;
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
            dgv.ScrollBars = ScrollBars.Vertical;

            // Đánh số STT tự động
            dgv.DataBindingComplete += dgvInsertCustomer_DataBindingComplete;
        }

        private void dgvInsertCustomer_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvInsertCustomer.Rows.Count; i++)
            {
                dgvInsertCustomer.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
            dgvInsertCustomer.ClearSelection();

            // Đảm bảo biến selectedRoom là null ban đầu
            selectedCustomerView = null;
        }
        #endregion

        #region === CẤU HÌNH CÁC CỘT ===
        private void ConfigureDataGridViewColumns()
        {
            var dgv = dgvInsertCustomer;
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

            // Gán BindingSource
            dgv.DataSource = bindingSource;
        }
        #endregion
        List<CustomerViewModel> allCustomers;
        #region === LOAD + REFRESH DANH SÁCH KHÁCH HÀNG ===
        private void LoadCustomers()
        {
            var customerService = new CustomerService();
            allCustomers = customerService.GetAllCustomers();
            bindingSource.DataSource = allCustomers; // Dùng BindingSource → refresh mượt hơn
        }

        public void RefreshGrid()
        {
            LoadCustomers();
            selectedCustomerView = null;
            dgvInsertCustomer.ClearSelection(); // Bỏ highlight dòng cũ
        }
        #endregion

        #region === CHỌN DÒNG ===
        private void dgvInsertCustomer_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInsertCustomer.CurrentRow != null && dgvInsertCustomer.CurrentRow.Index >= 0)
            {
                selectedCustomerView = dgvInsertCustomer.CurrentRow.DataBoundItem as CustomerViewModel;
            }
            else
            {
                selectedCustomerView = null;
            }
        }
        #endregion

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var form = new DetailCustomer();
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshGrid();
            }
        }
        public CustomerViewModel SelectedCustomer { get; private set; }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            //this.Close();
            if(selectedCustomerView != null)
            {
                SelectedCustomer = selectedCustomerView;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng để thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }   
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            if (allCustomers == null) return;

            string keyword = textSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                bindingSource.DataSource = allCustomers;
                return;
            }

            var filtered = allCustomers
                .Where(kh =>
                    (!string.IsNullOrEmpty(kh.CMND) && kh.CMND.Contains(keyword)) ||
                    (!string.IsNullOrEmpty(kh.MaKhach) && kh.MaKhach.Contains(keyword))
                )
                .ToList();

            bindingSource.DataSource = filtered;
        }
    }
}
