using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class Rental : Form
    {
        private readonly string _maPhong;

        // Danh sách khách trong phòng
        private readonly List<CustomerViewModel> listcustomer = new List<CustomerViewModel>();

        // Quản lý vai trò: Chinh / Phu (không sửa CustomerViewModel)
        private readonly Dictionary<CustomerViewModel, string> vaiTroMap
            = new Dictionary<CustomerViewModel, string>();

        public Rental(string maPhong)
        {
            InitializeComponent();
            _maPhong = maPhong;

            SetupDataGridView();

            dtpNgayThue.MinDate = DateTime.Today;
            dtpNgayThue.Value = DateTime.Today;
        }

        private void SetupDataGridView()
        {
            var dgv = dgvKhachHang;

            // ===== BASIC =====
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
            dgv.ScrollBars = ScrollBars.Vertical;

            // ===== HEADER STYLE =====
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // ===== ROW STYLE =====
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 248, 255);

            // ===== CLEAR & ADD COLUMNS =====
            dgv.Columns.Clear();

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 60,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaKhach",
                HeaderText = "Mã Khách"
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                HeaderText = "Họ Tên"
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CMND",
                HeaderText = "CMND"
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenLoaiKhach",
                HeaderText = "Loại Khách"
            });

            dgv.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "BtnVaiTro",
                HeaderText = "Vai Trò",
                UseColumnTextForButtonValue = false,
                FlatStyle = FlatStyle.Flat
            });

            // ===== EVENTS =====
            dgv.CellContentClick += DgvKhachHang_CellContentClick;

            // ===== STT =====
            dgv.RowPostPaint += (s, e) =>
            {
                var grid = s as DataGridView;
                var stt = (e.RowIndex + 1).ToString();

                var centerFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                var headerBounds = new Rectangle(
                    e.RowBounds.Left,
                    e.RowBounds.Top,
                    grid.Columns["STT"].Width,
                    e.RowBounds.Height
                );

                e.Graphics.DrawString(
                    stt,
                    grid.Font,
                    Brushes.Black,
                    headerBounds,
                    centerFormat
                );
            };

            // ===== HOVER EFFECT =====
            dgv.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && !dgv.Rows[e.RowIndex].Selected)
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                        Color.FromArgb(220, 235, 255);
                }
            };

            dgv.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    dgv.InvalidateRow(e.RowIndex);
                }
            };
        }


        #region ===== THÊM KHÁCH =====
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var frm = new InsertCustomer())
            {
                if (frm.ShowDialog() != DialogResult.OK) return;

                var customer = frm.SelectedCustomer;
                if (customer == null) return;

                if (listcustomer.Any(c => c.CMND == customer.CMND))
                {
                    MessageBox.Show(
                        "Khách hàng này đã có trong danh sách!",
                        "Trùng CMND",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                listcustomer.Add(customer);

                // ⭐ Người đầu tiên mặc định là Chinh
                vaiTroMap[customer] = listcustomer.Count == 1 ? "Chinh" : "Phu";

                AddCustomerToGrid(customer);
            }
        }

        private void AddCustomerToGrid(CustomerViewModel customer)
        {
            int rowIndex = dgvKhachHang.Rows.Add();
            var row = dgvKhachHang.Rows[rowIndex];

            row.Cells["MaKhach"].Value = customer.MaKhach;
            row.Cells["HoTen"].Value = customer.HoTen;
            row.Cells["CMND"].Value = customer.CMND;
            row.Cells["TenLoaiKhach"].Value = customer.TenLoaiKhach;

            // Gán Tag để biết khách nào
            row.Tag = customer;

            UpdateVaiTroDisplay();
        }
        #endregion

        #region ===== CLICK BUTTON VAI TRÒ =====
        private void DgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvKhachHang.Columns[e.ColumnIndex].Name != "BtnVaiTro") return;

            var customer = dgvKhachHang.Rows[e.RowIndex].Tag as CustomerViewModel;
            if (customer == null) return;

            // Tất cả về Phu
            foreach (var c in listcustomer)
            {
                vaiTroMap[c] = "Phu";
            }

            // Người được click là Chinh
            vaiTroMap[customer] = "Chinh";

            UpdateVaiTroDisplay();
        }
        #endregion

        #region ===== HIỂN THỊ VAI TRÒ =====
        private void UpdateVaiTroDisplay()
        {
            foreach (DataGridViewRow row in dgvKhachHang.Rows)
            {
                var customer = row.Tag as CustomerViewModel;
                if (customer == null) continue;

                string vaiTro = vaiTroMap.ContainsKey(customer)
                    ? vaiTroMap[customer]
                    : "Phu";

                var btn = row.Cells["BtnVaiTro"] as DataGridViewButtonCell;
                btn.Value = vaiTro;

                if (vaiTro == "Chinh")
                {
                    btn.Style.BackColor = Color.Orange;
                    btn.Style.ForeColor = Color.White;
                    btn.Style.Font = new Font(dgvKhachHang.Font, FontStyle.Bold);
                }
                else
                {
                    btn.Style.BackColor = SystemColors.Control;
                    btn.Style.ForeColor = SystemColors.ControlText;
                    btn.Style.Font = dgvKhachHang.Font;
                }
            }

            dgvKhachHang.Invalidate();
        }
        #endregion

        #region ===== XOÁ KHÁCH =====
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null) return;

            var row = dgvKhachHang.CurrentRow;
            var customer = row.Tag as CustomerViewModel;
            if (customer == null) return;

            var result = MessageBox.Show(
                $"Xóa khách hàng: {customer.HoTen}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            listcustomer.Remove(customer);
            vaiTroMap.Remove(customer);
            dgvKhachHang.Rows.Remove(row);

            // Nếu còn khách mà chưa có Chinh → gán người đầu
            if (listcustomer.Any() &&
                listcustomer.All(c => !vaiTroMap.ContainsKey(c) || vaiTroMap[c] != "Chinh"))
            {
                vaiTroMap[listcustomer[0]] = "Chinh";
            }

            UpdateVaiTroDisplay();
        }
        #endregion

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            // 1️⃣ Kiểm tra có khách
            if (!listcustomer.Any())
            {
                MessageBox.Show(
                    "Chưa có khách hàng nào!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 2️⃣ Phải có đúng 1 khách Chính
            if (vaiTroMap.Count(x => x.Value == "Chinh") != 1)
            {
                MessageBox.Show(
                    "Phải có đúng 1 khách là Chinh!",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // 3️⃣ Tạo Dictionary <MaKhach, VaiTro>
                Dictionary<string, string> dsKhach = listcustomer.ToDictionary(
                    c => c.MaKhach,
                    c => vaiTroMap.ContainsKey(c) ? vaiTroMap[c] : "Phu"
                );

                // 4️⃣ Gọi service
                RentalService rentalService = new RentalService();
                bool result = rentalService.CreateRental(
                    _maPhong,
                    dtpNgayThue.Value,
                    dsKhach
                );

                if (!result) return;

                // 5️⃣ Thành công
                MessageBox.Show(
                    "Lập phiếu thuê phòng thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }



        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
