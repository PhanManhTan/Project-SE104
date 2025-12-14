using System;
using System.Drawing;
using System.Windows.Forms;
using Services;
using Data;

namespace Project
{
    public partial class RoomTypeManager : Form
    {
        private readonly RoomService roomService = new RoomService();
        private string selectedMaLoai = null;
        private BindingSource bindingSource = new BindingSource();

        public RoomTypeManager()
        {
            InitializeComponent();

            if (Global_.CurrentUser != null && Global_.CurrentUser.Role_ == "Staff")
            {
                // Chỉ khóa nút XÓA khách hàng
                btnDelete.Enabled = false;
                btnDelete.BackColor = Color.DarkGray;
                btnUpdate.Enabled = false;
                btnUpdate.BackColor = Color.DarkGray;
                btnCreate.Enabled = false;
                btnCreate.BackColor = Color.DarkGray;
            }
        }

        private void RoomTypesForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns();
            LoadData();

            dgvLoaiPhong.SelectionChanged += DgvLoaiPhong_SelectionChanged;




        }

        #region === CẤU HÌNH GIAO DIỆN DATAGRIDVIEW (ĐỒNG BỘ VỚI CUSTOMERMANAGER) ===

        private void SetupDataGridView()
        {
            var dgv = dgvLoaiPhong;

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
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 45;

            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Row style
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            dgv.ScrollBars = ScrollBars.Vertical;

            // STT tự động
            dgv.DataBindingComplete += DgvLoaiPhong_DataBindingComplete;
            // Định dạng hiển thị đơn giá với " đ"
            dgv.CellFormatting += DgvLoaiPhong_CellFormatting;
        }

        private void DgvLoaiPhong_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvLoaiPhong.Rows.Count; i++)
            {
                dgvLoaiPhong.Rows[i].Cells["STT"].Value = (i + 1).ToString();

            }
        }

        #endregion

        #region === CẤU HÌNH CÁC CỘT ===

        private void ConfigureDataGridViewColumns()
        {
            var dgv = dgvLoaiPhong;
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

            // Mã loại phòng
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaLoaiPhong",
                HeaderText = "Mã loại phòng",
                DataPropertyName = "MaLoaiPhong",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Tên loại phòng
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenLoaiPhong",
                HeaderText = "Tên loại phòng",
                DataPropertyName = "TenLoaiPhong",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                }
            });

            // Đơn giá
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Đơn giá",
                DataPropertyName = "DonGia",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N0",
                    Padding = new Padding(0, 0, 10, 0)
                }
            });

            dgv.DataSource = bindingSource;
        }

        #endregion

        #region === LOAD + REFRESH ===

        private void LoadData()
        {
            // Tạo service mới mỗi lần load → tránh cache context cũ
            var tempService = new RoomService();
            var list = tempService.GetAllRoomTypes();

            bindingSource.DataSource = list;
            bindingSource.ResetBindings(false); // Thêm để chắc chắn

            dgvLoaiPhong.ClearSelection();
            selectedMaLoai = null;
        }

        public void RefreshGrid()
        {
            LoadData();
        }

        #endregion

        #region === CHỌN DÒNG ===

        private void DgvLoaiPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiPhong.CurrentRow != null && dgvLoaiPhong.CurrentRow.Index >= 0)
            {
                var row = dgvLoaiPhong.CurrentRow;
                if (row.Cells["MaLoaiPhong"].Value != null)
                    selectedMaLoai = row.Cells["MaLoaiPhong"].Value.ToString();
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
            using (var frm = new DetailRoomType())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaLoai))
            {
                MessageBox.Show("Vui lòng chọn một loại phòng để cập nhật.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loaiPhongCanSua = roomService.GetRoomTypeById(selectedMaLoai);

            if (loaiPhongCanSua == null)
            {
                MessageBox.Show("Không tìm thấy loại phòng cần cập nhật.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
                return;
            }

            using (var frm = new DetailRoomType(loaiPhongCanSua))
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
                MessageBox.Show("Vui lòng chọn một loại phòng để xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loaiPhong = roomService.GetRoomTypeById(selectedMaLoai);
            string tenLoai = loaiPhong?.TenLoaiPhong ?? selectedMaLoai;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa loại phòng \"{tenLoai}\" (Mã: {selectedMaLoai}) không?\n\n" +
                "Cảnh báo: Nếu loại phòng này đang được sử dụng trong phòng hoặc phiếu thuê, việc xóa sẽ thất bại.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (roomService.DeleteRoomType(selectedMaLoai))
                {
                    MessageBox.Show("Xóa loại phòng thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không thể xóa loại phòng!\n\nLý do phổ biến:\n• Loại phòng đang được sử dụng trong danh sách phòng\n• Đang có phiếu thuê sử dụng loại này",
                        "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void DgvLoaiPhong_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Chỉ áp dụng cho cột DonGia
            if (dgvLoaiPhong.Columns[e.ColumnIndex].Name == "DonGia" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal price))
                {
                    e.Value = price.ToString("N0") + " đ";
                    e.FormattingApplied = true;
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}