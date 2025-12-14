using Data;
using Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class CustomerManager : Form
    {
        private CustomerViewModel selectedCustomerView = null;
        private BindingSource bindingSource = new BindingSource(); // Thêm để đồng bộ với RoomManager

        public CustomerManager()
        {
            InitializeComponent();
            if (Global_.CurrentUser != null && Global_.CurrentUser.Role_ == "Staff")
            {
                // Chỉ khóa nút XÓA khách hàng
                btnDelete.Enabled = false;
                btnDelete.BackColor = Color.DarkGray;

            }
        }

        private void CustomerManager_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns(); // Gọi một lần duy nhất ở đây
            LoadCustomers();

        }

        #region === CẤU HÌNH GIAO DIỆN DATAGRIDVIEW ===
        private void SetupDataGridView()
        {
            var dgv = dgvCustomerManager;
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
            dgv.DataBindingComplete += DgvCustomerManager_DataBindingComplete;
        }

        private void DgvCustomerManager_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvCustomerManager.Rows.Count; i++)
            {
                dgvCustomerManager.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
            dgvCustomerManager.ClearSelection();

            // Đảm bảo biến selectedRoom là null ban đầu
            selectedCustomerView = null;
        }
        #endregion

        #region === CẤU HÌNH CÁC CỘT ===
        private void ConfigureDataGridViewColumns()
        {
            var dgv = dgvCustomerManager;
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

        #region === LOAD + REFRESH DANH SÁCH KHÁCH HÀNG ===
        private void LoadCustomers()
        {
            var customerService = new CustomerService();
            var list = customerService.GetAllCustomers();
            bindingSource.DataSource = list; // Dùng BindingSource → refresh mượt hơn
        }

        public void RefreshGrid()
        {
            LoadCustomers();
            selectedCustomerView = null;
            dgvCustomerManager.ClearSelection(); // Bỏ highlight dòng cũ
        }
        #endregion

        #region === CHỌN DÒNG ===
        private void dgvCustomerManager_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomerManager.CurrentRow != null && dgvCustomerManager.CurrentRow.Index >= 0)
            {
                selectedCustomerView = dgvCustomerManager.CurrentRow.DataBoundItem as CustomerViewModel;
            }
            else
            {
                selectedCustomerView = null;
            }
        }
        #endregion

        #region === CÁC NÚT CHỨC NĂNG ===
        private void btnCreate_Click(object sender, EventArgs e)
        {
            var form = new DetailCustomer();
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshGrid();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCustomerView == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var customerService = new CustomerService();
            var khachHang = customerService.GetCustomerById(selectedCustomerView.MaKhach);
            if (khachHang == null)
            {
                MessageBox.Show("Không tìm thấy khách hàng để sửa!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                RefreshGrid();
                return;
            }

            var form = new DetailCustomer(khachHang);
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCustomerView == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khách hàng \"{selectedCustomerView.HoTen}\" (Mã: {selectedCustomerView.MaKhach}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                var customerService = new CustomerService();
                bool success = customerService.DeleteCustomer(selectedCustomerView.MaKhach);

                RefreshGrid();

                if (success)
                    MessageBox.Show("Xóa khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Không thể xóa khách hàng! (Có thể đang được sử dụng trong phiếu thuê)", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCustomerTypes_Click(object sender, EventArgs e)
        {
            using (var frm = new CustomerTypeManager())
            {
                frm.ShowDialog();
                RefreshGrid();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvCustomerManager_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        // ĐÃ XÓA: private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}