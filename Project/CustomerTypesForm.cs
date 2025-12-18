using Data;
using Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class CustomerTypesForm : Form
    {
        private string selectedMaLoai = null;
        private readonly CustomerService customerService = new CustomerService();
        private BindingSource bindingSource = new BindingSource();

        public CustomerTypesForm()
        {
            InitializeComponent();

            if (Global_.CurrentUser != null && Global_.CurrentUser.Role_ == "Staff")
            {
                btnCreate.Enabled = false;
                btnCreate.BackColor = Color.DarkGray;
                btnUpdate.Enabled = false;
                btnUpdate.BackColor = Color.DarkGray;
                btnDelete.Enabled = false;
                btnDelete.BackColor = Color.DarkGray;
            }
        }

        private void CustomerTypeManagerForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns();
            LoadData();
            dgvBody.SelectionChanged += dgv_SelectionChanged;
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
            dgv.ScrollBars = ScrollBars.Vertical;

            dgv.DataBindingComplete += Dgv_DataBindingComplete;
        }

        private void Dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvBody.Rows.Count; i++)
            {
                dgvBody.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
            dgvBody.ClearSelection();
            selectedMaLoai = null;
        }

        private void ConfigureDataGridViewColumns()
        {
            var dgv = dgvBody;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

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
                Name = "MaLoaiKhach",
                HeaderText = "Mã loại khách",
                DataPropertyName = "MaLoaiKhach",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenLoaiKhach",
                HeaderText = "Tên loại khách",
                DataPropertyName = "TenLoaiKhach",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                }
            });

            dgv.DataSource = bindingSource;
        }

        private void LoadData()
        {
            var cS = new CustomerService();
            var list = cS.GetAllCustomerTypes();
            bindingSource.DataSource = list;
            dgvBody.ClearSelection();
            selectedMaLoai = null;
        }

        public void RefreshGrid()
        {
            LoadData();

        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBody.CurrentRow?.Index >= 0 && dgvBody.CurrentRow.Cells["MaLoaiKhach"].Value != null)
            {
                selectedMaLoai = dgvBody.CurrentRow.Cells["MaLoaiKhach"].Value.ToString();
            }
            else
            {
                selectedMaLoai = null;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var frm = new CustomerTypeDetailForm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn 1 loại khách để cập nhật.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loaiKhach = customerService.GetCustomerTypeById(selectedMaLoai);
            if (loaiKhach == null)
            {
                MessageBox.Show("Không tìm thấy loại khách cần cập nhật. Có thể dữ liệu đã bị xóa.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                RefreshGrid();
                return;
            }

            using (var frm = new CustomerTypeDetailForm(loaiKhach))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {

                 RefreshGrid();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn 1 loại khách để xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loaiKhach = customerService.GetCustomerTypeById(selectedMaLoai);
            if (loaiKhach == null)
            {
                MessageBox.Show("Không tìm thấy loại khách.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
                return;
            }

            string tenLoai = loaiKhach.TenLoaiKhach?.Trim() ?? selectedMaLoai;

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa loại khách \"{tenLoai}\"\n",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            bool success = customerService.DeleteCustomerType(selectedMaLoai);

            LoadData();

            if (success)
            {
                MessageBox.Show("Xóa loại khách thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Không thể xóa loại khách này!\n\n" +
                    "Nguyên nhân thường gặp:\n" +
                    "• Có khách hàng đang thuộc loại khách này\n" +
                    "• Dữ liệu đang được sử dụng ở nơi khác",
                    "Không thể xóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}