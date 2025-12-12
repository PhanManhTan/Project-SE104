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
    public partial class CustomerManager : Form
    {
        public CustomerManager()
        {
            InitializeComponent();
            LoadCustomers();


        }

        private void CustomerManager_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }
        private KhachHang KhachHangdangchon = null;
        private void LoadCustomers()
        {
            CustomerService customerService = new CustomerService();
            List<KhachHang> list = customerService.GetAllCustomers();

            dgvCustomerManager.AutoGenerateColumns = false;
            dgvCustomerManager.Columns.Clear();
            dgvCustomerManager.RowHeadersVisible = false;

            // ===== Cột STT =====
            var colSTT = new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "STT",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                }
            };
            dgvCustomerManager.Columns.Add(colSTT);

            // ===== Mã khách =====
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaKhach",
                HeaderText = "Mã khách",
                DataPropertyName = "MaKhach",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // ===== Họ tên =====
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                HeaderText = "Họ tên",
                DataPropertyName = "HoTen",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            });

            // ===== CMND / CCCD =====
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CMND",
                HeaderText = "CMND",
                DataPropertyName = "CMND",
                //AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // ===== Loại khách =====
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiKhach",
                HeaderText = "Loại khách",
                DataPropertyName = "MaLoaiKhach",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // ===== Địa chỉ =====
            dgvCustomerManager.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaChi",
                HeaderText = "Địa chỉ",
                DataPropertyName = "DiaChi",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            });

            // ===== Gán dữ liệu =====
            dgvCustomerManager.DataSource = list;

            // ===== Đánh số STT =====
            for (int i = 0; i < dgvCustomerManager.Rows.Count; i++)
                dgvCustomerManager.Rows[i].Cells["STT"].Value = (i + 1).ToString();

            // ===== Thiết kế bảng =====
            dgvCustomerManager.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomerManager.MultiSelect = false;
            dgvCustomerManager.AllowUserToResizeRows = false;

            // Header
            dgvCustomerManager.EnableHeadersVisualStyles = false;
            dgvCustomerManager.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomerManager.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvCustomerManager.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvCustomerManager.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCustomerManager.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgvCustomerManager.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            using (var frm = new LoaiKhachForm())
            {
                if (frm.ShowDialog() == DialogResult.OK) this.Close();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            CustomerService customerService = new CustomerService();
            DialogResult result = MessageBox.Show(
            "Are you sure you want to delete this Customer?",
            "Warning!",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
            );
            if (result == DialogResult.Yes)
            {
                RoomService roomService = new RoomService();
                customerService.DeleteCustomer(KhachHangdangchon.MaKhach);

            }
            LoadCustomers();
        }

        private void dgvCustomerManager_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomerManager.CurrentRow != null && dgvCustomerManager.CurrentRow.Index >= 0)
            {
                KhachHangdangchon = (KhachHang)dgvCustomerManager.CurrentRow.DataBoundItem;
            }
            else
            {
                dgvCustomerManager = null;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DetailCustomer detailCustomer = new DetailCustomer(KhachHangdangchon);
            detailCustomer.ShowDialog();
            LoadCustomers();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            DetailCustomer detailCustomer1 = new DetailCustomer();
            detailCustomer1.ShowDialog();
            LoadCustomers();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
