using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class RentalRegistrationForm : Form
    {
        private readonly string _maPhong;
        private readonly List<CustomerViewModel> listcustomer = new List<CustomerViewModel>();
        private readonly Dictionary<CustomerViewModel, string> vaiTroMap = new Dictionary<CustomerViewModel, string>();

        public RentalRegistrationForm(string maPhong)
        {
            InitializeComponent();
            _maPhong = maPhong;
            lblMaPhong.Text = maPhong;
            SetupDataGridView();
            dtpNgayThue.MinDate = DateTime.Today;
            dtpNgayThue.Value = DateTime.Today;
        }

        private void SetupDataGridView()
        {
            var dgv = dgvDanhSachThue;
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

            dgv.Columns.Clear();
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "STT", HeaderText = "STT", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaKhach", HeaderText = "Mã Khách" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "HoTen", HeaderText = "Họ Tên" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "CMND", HeaderText = "CMND/CCCD" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenLoaiKhach", HeaderText = "Loại Khách" });
            dgv.Columns.Add(new DataGridViewButtonColumn { Name = "BtnVaiTro", HeaderText = "Vai Trò", FlatStyle = FlatStyle.Flat });

            dgv.CellContentClick += DgvKhachHang_CellContentClick;

            dgv.RowPostPaint += (s, e) =>
            {
                var stt = (e.RowIndex + 1).ToString();
                var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                var rect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dgv.Columns["STT"].Width, e.RowBounds.Height);
                e.Graphics.DrawString(stt, dgv.Font, Brushes.Black, rect, format);
            };

            dgv.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && !dgv.Rows[e.RowIndex].Selected)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);
            };
            dgv.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0 && !dgv.Rows[e.RowIndex].Selected)
                    dgv.InvalidateRow(e.RowIndex);
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int soKhachToiDa;
            using (var paramService = new ParameterService())
            {
                soKhachToiDa = (int)paramService.GetThamSo(ParameterService.KEY_SO_KHACH_TOI_DA);
            }

            if (listcustomer.Count >= soKhachToiDa)
            {
                MessageBox.Show($"Phòng chỉ cho phép tối đa {soKhachToiDa} khách!", "Giới hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frm = new InsertCustomer())
            {
                if (frm.ShowDialog() != DialogResult.OK || frm.SelectedCustomer == null) return;

                var customer = frm.SelectedCustomer;
                if (listcustomer.Any(c => c.CMND == customer.CMND))
                {
                    MessageBox.Show("Khách này đã có trong danh sách (trùng CMND/CCCD)!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                listcustomer.Add(customer);
                vaiTroMap[customer] = listcustomer.Count == 1 ? "Chinh" : "Phu";
                AddCustomerToGrid(customer);

                if (listcustomer.Count == soKhachToiDa)
                    MessageBox.Show("Đã đủ số khách tối đa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachThue.CurrentRow == null || dgvDanhSachThue.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvDanhSachThue.CurrentRow;
            var customer = row.Tag as CustomerViewModel;
            if (customer == null) return;

            if (MessageBox.Show($"Xóa khách: {customer.HoTen} ({customer.CMND})?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            listcustomer.Remove(customer);
            vaiTroMap.Remove(customer);
            dgvDanhSachThue.Rows.Remove(row);

            if (listcustomer.Any() && !vaiTroMap.Values.Contains("Chinh"))
                vaiTroMap[listcustomer[0]] = "Chinh";

            UpdateVaiTroDisplay();
        }

        private void AddCustomerToGrid(CustomerViewModel customer)
        {
            var row = dgvDanhSachThue.Rows[dgvDanhSachThue.Rows.Add()];
            row.Cells["MaKhach"].Value = customer.MaKhach;
            row.Cells["HoTen"].Value = customer.HoTen;
            row.Cells["CMND"].Value = customer.CMND;
            row.Cells["TenLoaiKhach"].Value = customer.TenLoaiKhach;
            row.Tag = customer;
            UpdateVaiTroDisplay();
        }

        private void DgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvDanhSachThue.Columns[e.ColumnIndex].Name != "BtnVaiTro") return;

            var customer = dgvDanhSachThue.Rows[e.RowIndex].Tag as CustomerViewModel;
            if (customer == null) return;

            foreach (var c in listcustomer) vaiTroMap[c] = "Phu";
            vaiTroMap[customer] = "Chinh";
            UpdateVaiTroDisplay();
        }

        private void UpdateVaiTroDisplay()
        {
            foreach (DataGridViewRow row in dgvDanhSachThue.Rows)
            {
                if (row.IsNewRow) continue;
                var customer = row.Tag as CustomerViewModel;
                if (customer == null) continue;

                string vaiTro = vaiTroMap.TryGetValue(customer, out var vt) ? vt : "Phu";
                var btnCell = row.Cells["BtnVaiTro"] as DataGridViewButtonCell;
                btnCell.Value = vaiTro == "Chinh" ? "Chính" : "Phụ";

                if (vaiTro == "Chinh")
                {
                    btnCell.Style.BackColor = Color.Orange;
                    btnCell.Style.ForeColor = Color.White;
                    btnCell.Style.Font = new Font(dgvDanhSachThue.Font, FontStyle.Bold);
                }
                else
                {
                    btnCell.Style.BackColor = Color.FromArgb(230, 230, 230);
                    btnCell.Style.ForeColor = Color.Black;
                    btnCell.Style.Font = dgvDanhSachThue.Font;
                }
            }
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            if (!listcustomer.Any())
            {
                MessageBox.Show("Chưa thêm khách hàng nào!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (vaiTroMap.Values.Count(v => v == "Chinh") != 1)
            {
                MessageBox.Show("Phải chọn đúng 1 khách làm đại diện (Chính)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var dsKhach = listcustomer.ToDictionary(c => c.MaKhach, c => vaiTroMap.TryGetValue(c, out var vt) ? vt : "Phu");
                var rentalService = new RentalService();
                bool success = rentalService.CreateRental(_maPhong, dtpNgayThue.Value, dsKhach);

                if (success)
                {
                    MessageBox.Show("Lập phiếu thuê thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Rental_Load(object sender, EventArgs e) { }
    }
}