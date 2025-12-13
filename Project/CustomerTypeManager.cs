using System;
using System.Drawing;
using System.Windows.Forms;
using Services; // Thêm dòng này
using Data;     // Thêm dòng này

namespace Project
{
    public partial class CustomerTypeManager : Form
    {
        private string selectedMaLoai = null;
        private readonly CustomerService customerService = new CustomerService(); // Khai báo Service

        // Giữ nguyên constructor nhận connString để code cũ không bị lỗi, nhưng ta không dùng biến đó nữa
        public CustomerTypeManager()
        {
            InitializeComponent();
        }

        private void LoaiKhachForm_Load(object sender, EventArgs e)
        {

            SetupForm();
            LoadData();
            dgvLoaiKhach.SelectionChanged += DgvLoaiKhach_SelectionChanged;
        }

        // ... (Giữ nguyên các hàm SetupDataGridView, SetupForm) ...


        private void SetupForm()
        {
            dgvLoaiKhach.RowHeadersVisible = false;
            dgvLoaiKhach.AllowUserToAddRows = false;
            dgvLoaiKhach.ReadOnly = true;
            dgvLoaiKhach.MultiSelect = false;
            dgvLoaiKhach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiKhach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoaiKhach.ScrollBars = ScrollBars.Vertical;

            dgvLoaiKhach.EnableHeadersVisualStyles = false;

            // Header
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLoaiKhach.ColumnHeadersHeight = 30;
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dgvLoaiKhach.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Dòng xen kẽ
            dgvLoaiKhach.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvLoaiKhach.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvLoaiKhach.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            // Font dòng
            dgvLoaiKhach.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            // Tăng độ cao dòng
            dgvLoaiKhach.RowTemplate.Height = 40;

            // Cấm kéo cột/dòng
            dgvLoaiKhach.AllowUserToResizeColumns = false;
            dgvLoaiKhach.AllowUserToResizeRows = false;
            dgvLoaiKhach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void LoadData()
        {
            // SỬA: Dùng Service thay vì SqlDataAdapter
            // Tạo mới service để làm mới context
            var list = new CustomerService().GetAllCustomerTypes();

            // Tạo DataTable thủ công để control hiển thị (giống RoomTypesForm của bạn)
            var dt = new System.Data.DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("MaLoaiKhach", typeof(string));
            dt.Columns.Add("TenLoaiKhach", typeof(string));

            int stt = 1;
            foreach (var item in list)
            {
                dt.Rows.Add(stt++, item.MaLoaiKhach, item.TenLoaiKhach);
            }

            dgvLoaiKhach.DataSource = dt;
            dgvLoaiKhach.ClearSelection();
            selectedMaLoai = null;

            // Gọi lại cấu hình cột (nếu cần)
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            // Code cũ của bạn, giữ nguyên
            void SetupColumn(string columnName, string headerText, int fillWeight = 20, DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleCenter)
            {
                if (dgvLoaiKhach.Columns[columnName] != null)
                {
                    var col = dgvLoaiKhach.Columns[columnName];
                    col.HeaderText = headerText;
                    col.DefaultCellStyle.Alignment = align;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.FillWeight = fillWeight;
                }
            }

            SetupColumn("STT", "STT", 10);
            SetupColumn("MaLoaiKhach", "Mã loại khách", 30);
            SetupColumn("TenLoaiKhach", "Tên loại khách", 60);
        }

        private void DgvLoaiKhach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiKhach.SelectedRows.Count > 0)
            {
                var row = dgvLoaiKhach.SelectedRows[0];
                if (!row.IsNewRow && row.Cells["MaLoaiKhach"].Value != null)
                    selectedMaLoai = row.Cells["MaLoaiKhach"].Value.ToString();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Bỏ truyền connString, Forms con tự xử lý service
            using (var frm = new CustomerTypeCreate())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn 1 loại khách để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bỏ truyền connString
            using (var frm = new CustomerTypeUpdate(selectedMaLoai))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn 1 loại khách để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa loại khách {selectedMaLoai}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            // SỬA: Gọi Service
            if (customerService.DeleteCustomerType(selectedMaLoai))
            {
                LoadData();
                MessageBox.Show("Xóa thành công!", "Thông báo");
            }
            // Không cần else vì Service đã hiện MessageBox lỗi nếu có
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}