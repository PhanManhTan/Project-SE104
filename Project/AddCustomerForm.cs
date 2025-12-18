using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class AddCustomerForm : Form
    {
        private readonly CustomerService _customerService = new CustomerService();
        private BindingSource _bindingSource = new BindingSource();
        private CustomerViewModel _selectedCustomer;

        public CustomerViewModel SelectedCustomer => _selectedCustomer;

        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private void InsertCustomer_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadCustomersAsync();
        }

        #region DataGridView Configuration

        private void ConfigureDataGridView()
        {
            var dgv = dgvBody;

            // General settings
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

            // Header style
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // Row style
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            // Columns configuration
            ConfigureColumns();

            // Events
            dgv.DataBindingComplete += OnDataBindingComplete;
            dgv.SelectionChanged += OnSelectionChanged;

            dgv.DataSource = _bindingSource;
        }

        private void ConfigureColumns()
        {
            var dgv = dgvBody;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            // STT Column
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 60,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            });

            // Other columns
            AddColumn("MaKhach", "Mã khách", DataGridViewContentAlignment.MiddleCenter);
            AddColumn("HoTen", "Họ tên", DataGridViewContentAlignment.MiddleLeft, paddingLeft: 10);
            AddColumn("CMND", "CMND/CCCD", DataGridViewContentAlignment.MiddleCenter);
            AddColumn("TenLoaiKhach", "Loại khách", DataGridViewContentAlignment.MiddleCenter);
            AddColumn("DiaChi", "Địa chỉ", DataGridViewContentAlignment.MiddleLeft, paddingLeft: 10, fill: true);
        }

        private void AddColumn(string name, string headerText, DataGridViewContentAlignment alignment,
            int paddingLeft = 0, bool fill = false)
        {
            var column = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                DataPropertyName = name,
                AutoSizeMode = fill ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = alignment,
                    Padding = new Padding(paddingLeft, 0, 0, 0)
                }
            };

            dgvBody.Columns.Add(column);
        }

        private void OnDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = dgvBody;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }

            dgv.ClearSelection();
            _selectedCustomer = null;
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            var dgv = dgvBody;
            _selectedCustomer = dgv.CurrentRow?.DataBoundItem as CustomerViewModel;
        }

        #endregion

        #region Data Loading & Refresh

        private async void LoadCustomersAsync()
        {
            // Nếu dữ liệu nặng, có thể dùng async/await thực sự từ service
            var customers = await Task.Run(() => _customerService.GetAllCustomers());
            _bindingSource.DataSource = customers;
        }

        public void RefreshGrid()
        {
            LoadCustomersAsync();
        }

        #endregion

        #region Search Functionality

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = textSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                _bindingSource.DataSource = _customerService.GetAllCustomers(); // Hoặc cache lại danh sách gốc
                return;
            }

            var allCustomers = _bindingSource.DataSource as IEnumerable<CustomerViewModel>;
            if (allCustomers == null) return;

            var filtered = allCustomers.Where(c =>
                Contains(c.MaKhach, keyword) ||
                Contains(c.CMND, keyword)
            ).ToList();

            _bindingSource.DataSource = filtered;
        }

        private static bool Contains(string source, string keyword)
            => !string.IsNullOrEmpty(source) && source.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;

        #endregion

        #region Button Actions

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var form = new CustomerDetailForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để thêm.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}