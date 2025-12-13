using Data;
using Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class CustomerManager : Form
    {
        private KhachHang khachHangDangChon = null;

        public CustomerManager()
        {
            InitializeComponent();

            LoadCustomers();
        }

        private void CustomerManager_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadCustomers();
        }

        #region === CẤU HÌNH GIAO DIỆN DATAGRIDVIEW ===
        private void SetupDataGridView()
        {
            var dgv = dgvCustomerManager;

            // Cấu hình chung
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tắt style Windows mặc định
            dgv.EnableHeadersVisualStyles = false;

            // Header đẹp - Navy + chữ trắng
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // Dòng dữ liệu
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            // Hover effect nhẹ (rất pro)
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
        }
        #endregion

        #region === LOAD + REFRESH DANH SÁCH KHÁCH HÀNG ===
        private void LoadCustomers()
        {
            var customerService = new CustomerService();
            var list = customerService.GetAllCustomers();

            dgvCustomerManager.AutoGenerateColumns = false;
            dgvCustomerManager.Columns.Clear();

            // Cột STT
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "STT",
                Width = 70,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.Navy
                }
            });

            // Mã khách
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaKhach",
                HeaderText = "Mã khách",
                DataPropertyName = "MaKhach",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Họ tên
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
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

            // CMND/CCCD
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CMND",
                HeaderText = "CMND/CCCD",
                DataPropertyName = "CMND",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Loại khách
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiKhach",
                HeaderText = "Loại khách",
                DataPropertyName = "MaLoaiKhach",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Địa chỉ (giãn hết phần còn lại)
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
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

            // Gán dữ liệu
            dgvCustomerManager.DataSource = list;

            // Đánh số thứ tự
            for (int i = 0; i < dgvCustomerManager.Rows.Count; i++)
            {
                dgvCustomerManager.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
        }

        public void RefreshGrid()
        {
            LoadCustomers();
            khachHangDangChon = null;
        }
        #endregion

        #region === CHỌN DÒNG ===
        private void dgvCustomerManager_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomerManager.CurrentRow != null && dgvCustomerManager.CurrentRow.Index >= 0)
            {
                khachHangDangChon = (KhachHang)dgvCustomerManager.CurrentRow.DataBoundItem;
            }
            else
            {
                khachHangDangChon = null;
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
            if (khachHangDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var form = new DetailCustomer(khachHangDangChon);
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (khachHangDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khách hàng \"{khachHangDangChon.HoTen}\" (Mã: {khachHangDangChon.MaKhach}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                var customerService = new CustomerService();
                customerService.DeleteCustomer(khachHangDangChon.MaKhach);
                RefreshGrid();
                MessageBox.Show("Xóa khách hàng thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCustomerTypes_Click(object sender, EventArgs e)
        {
            using (var frm = new CustomerTypeManager())
            {
                frm.ShowDialog();
                // Không cần this.Close() nếu chỉ mở form loại khách để xem/sửa
            }
        }
        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}