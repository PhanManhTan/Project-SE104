using System;
using System.Drawing;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class CustomerTypeManager : Form
    {
        private string selectedMaLoai = null;
        private readonly CustomerService customerService = new CustomerService();
        private BindingSource bindingSource = new BindingSource(); // Đồng bộ với CustomerManager

        public CustomerTypeManager()
        {
            InitializeComponent();
        }

        private void LoaiKhachForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns(); // Gọi một lần ở đây
            LoadData();

            dgvLoaiKhach.SelectionChanged += DgvLoaiKhach_SelectionChanged;
        }

        #region === CẤU HÌNH GIAO DIỆN DATAGRIDVIEW (ĐỒNG BỘ VỚI CUSTOMERMANAGER) ===

        private void SetupDataGridView()
        {
            var dgv = dgvLoaiKhach;

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

            // Header style
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

            dgv.ScrollBars = ScrollBars.Vertical;

            // STT tự động
            dgv.DataBindingComplete += DgvLoaiKhach_DataBindingComplete;
        }

        private void DgvLoaiKhach_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvLoaiKhach.Rows.Count; i++)
            {
                dgvLoaiKhach.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
        }

        #endregion

        #region === CẤU HÌNH CÁC CỘT (THỦ CÔNG) ===

        private void ConfigureDataGridViewColumns()
        {
            var dgv = dgvLoaiKhach;
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
                },
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // Mã loại khách
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaLoaiKhach",
                HeaderText = "Mã loại khách",
                DataPropertyName = "MaLoaiKhach",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Tên loại khách
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

            // Gán BindingSource
            dgv.DataSource = bindingSource;
        }

        #endregion

        #region === LOAD + REFRESH DANH SÁCH ===

        private void LoadData()
        {
            var list = customerService.GetAllCustomerTypes();
            bindingSource.DataSource = list;
            dgvLoaiKhach.ClearSelection();
            selectedMaLoai = null;
        }

        public void RefreshGrid()
        {
            LoadData();
        }

        #endregion

        #region === CHỌN DÒNG ===

        private void DgvLoaiKhach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiKhach.CurrentRow != null && dgvLoaiKhach.CurrentRow.Index >= 0)
            {
                var row = dgvLoaiKhach.CurrentRow;
                if (row.Cells["MaLoaiKhach"].Value != null)
                    selectedMaLoai = row.Cells["MaLoaiKhach"].Value.ToString();
            }
            else
            {
                selectedMaLoai = null;
            }
        }

        #endregion

        #region === CÁC NÚT CHỨC NĂNG ===

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var frm = new DetailCustomerType())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
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

            var loaiKhachCanSua = customerService.GetCustomerTypeById(selectedMaLoai);

            if (loaiKhachCanSua == null)
            {
                MessageBox.Show("Không tìm thấy loại khách cần cập nhật. Có thể dữ liệu đã bị xóa.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
                return;
            }

            using (var frm = new DetailCustomerType(loaiKhachCanSua))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
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
                MessageBox.Show("Không tìm thấy loại khách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
                return;
            }

            string tenLoai = loaiKhach.TenLoaiKhach?.Trim() ?? selectedMaLoai;

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa loại khách \"{tenLoai}\" (Mã: {selectedMaLoai}) không?\n\n" +
                "Lưu ý: Nếu loại khách này đang có khách hàng sử dụng thì không thể xóa.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
                return;

            bool success = customerService.DeleteCustomerType(selectedMaLoai);

            if (success)
            {
                LoadData();
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
            this.Close();
        }

        #endregion
    }
}