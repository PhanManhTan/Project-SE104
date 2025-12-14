using Services;
using Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class PaymentForm : Form
    {
        private List<string> _maPhieus;
        private List<BillDetailItemViewModel> _billDetails = new List<BillDetailItemViewModel>();
        private decimal _totalAmount = 0;

        // UI Controls
        private Label lblTitle;
        private Label lblCustomerTitle, lblCustomerValue;
        private Label lblAddressTitle, lblAddressValue;
        private Label lblTotalTitle, lblTotalValue;
        private DataGridView dgvBill;
        private Button btnConfirm, btnCancel;
        private Panel panelHeader;
        private Panel panelFooter;
        private Panel panelContent; // Panel chứa Grid để canh lề đẹp hơn

        public PaymentForm(List<string> maPhieus)
        {
            // Xây dựng giao diện trước khi load dữ liệu
            InitializeComponent_Custom();
            _maPhieus = maPhieus;
            LoadData();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            using (var service = new BillingService())
            {
                _totalAmount = 0;
                _billDetails.Clear();

                foreach (var ma in _maPhieus)
                {
                    BillDetailItemViewModel bill = service.GetBillPreview(ma);
                    if (bill != null)
                    {
                        _billDetails.Add(bill);
                        _totalAmount += bill.ThanhTien;
                    }
                }
            }

            // Hiển thị thông tin khách đầu tiên
            if (_billDetails.Count > 0)
            {
                var first = _billDetails[0];
                lblCustomerValue.Text = first.TenKhach;
                lblAddressValue.Text = first.DiaChi;
            }

            // Format tiền tệ
            lblTotalValue.Text = $"{_totalAmount:N0} VNĐ";

            // Đổ dữ liệu vào bảng
            dgvBill.DataSource = null;
            dgvBill.DataSource = _billDetails;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            using (var service = new BillingService())
            {
                string[] dsMa = _maPhieus.ToArray();
                string maHD = service.LapHoaDon(DateTime.Now, dsMa);

                if (!string.IsNullOrEmpty(maHD))
                {
                    MessageBox.Show($"Thanh toán thành công!\nMã hóa đơn: {maHD}\nTổng tiền: {_totalAmount:N0} VNĐ",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        // === PHẦN GIAO DIỆN ĐƯỢC THIẾT KẾ LẠI THEO STYLE RENTAL MANAGER ===
        // === PHẦN GIAO DIỆN ĐƯỢC THIẾT KẾ LẠI THEO STYLE RENTAL MANAGER ===
        private void InitializeComponent_Custom()
        {
            // 1. Cấu hình Form chính
            this.Size = new Size(900, 600);
            this.Text = "Xác Nhận Thanh Toán";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 2. Panel Header (Chứa thông tin khách hàng, tổng tiền)
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 140,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Tiêu đề lớn
            lblTitle = new Label
            {
                Text = "HÓA ĐƠN THANH TOÁN",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.Navy,
                AutoSize = true,
                Location = new Point(20, 15)
            };

            // Group 1: Khách hàng (Trái)
            lblCustomerTitle = new Label
            {
                Text = "Khách hàng / Cơ quan:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(25, 60)
            };
            lblCustomerValue = new Label
            {
                Text = "...",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(25, 85)
            };

            // Group 2: Địa chỉ (Giữa)
            lblAddressTitle = new Label
            {
                Text = "Địa chỉ:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(350, 60)
            };
            lblAddressValue = new Label
            {
                Text = "...",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(350, 85)
            };

            // Group 3: Tổng tiền (Phải)
            lblTotalTitle = new Label
            {
                Text = "TỔNG TRỊ GIÁ:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(650, 60)
            };
            lblTotalValue = new Label
            {
                Text = "0 VNĐ",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.Red,
                AutoSize = true,
                Location = new Point(650, 82)
            };

            Label lblDivider = new Label
            {
                AutoSize = false,
                Height = 2,
                BackColor = Color.Navy,
                Dock = DockStyle.Bottom
            };

            panelHeader.Controls.AddRange(new Control[] {
        lblTitle,
        lblCustomerTitle, lblCustomerValue,
        lblAddressTitle, lblAddressValue,
        lblTotalTitle, lblTotalValue,
        lblDivider
    });

            // 3. Panel Footer (Nút bấm)
            panelFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.WhiteSmoke
            };

            btnConfirm = new Button
            {
                Text = "THANH TOÁN",
                Size = new Size(140, 45),
                Location = new Point(580, 12),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += btnConfirm_Click;

            btnCancel = new Button
            {
                Text = "Hủy Bỏ",
                Size = new Size(120, 45),
                Location = new Point(740, 12),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += btnCancel_Click;

            panelFooter.Controls.AddRange(new Control[] { btnConfirm, btnCancel });

            // 4. DataGridView
            panelContent = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 10, 20, 0) };

            dgvBill = new DataGridView();
            dgvBill.Dock = DockStyle.Fill;
            dgvBill.BackgroundColor = Color.White;
            dgvBill.BorderStyle = BorderStyle.None;
            dgvBill.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // --- CẤU HÌNH KHÓA CHỈNH SỬA ---
            dgvBill.AllowUserToAddRows = false;
            dgvBill.AllowUserToDeleteRows = false; // Không cho xóa
            dgvBill.AllowUserToResizeRows = false; // [MỚI THÊM] Không cho kéo giãn hàng
            dgvBill.AllowUserToResizeColumns = false; // Không cho kéo giãn cột (nếu muốn khóa luôn cột)
            dgvBill.RowHeadersVisible = false;
            dgvBill.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBill.ReadOnly = true;
            dgvBill.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Style
            dgvBill.EnableHeadersVisualStyles = false;
            dgvBill.ColumnHeadersHeight = 45;
            dgvBill.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvBill.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBill.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvBill.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvBill.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvBill.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            // Cột
            dgvBill.AutoGenerateColumns = false;
            dgvBill.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "STT",
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 10F, FontStyle.Bold) }
            });
            dgvBill.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaPhong",
                HeaderText = "Phòng",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgvBill.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoNgay",
                HeaderText = "Số Ngày",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgvBill.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn Giá",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvBill.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThanhTien",
                HeaderText = "Thành Tiền",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.Navy }
            });

            dgvBill.DataBindingComplete += (s, e) => {
                for (int i = 0; i < dgvBill.Rows.Count; i++) dgvBill.Rows[i].Cells["STT"].Value = (i + 1).ToString();
                dgvBill.ClearSelection();
            };

            panelContent.Controls.Add(dgvBill);

            this.Controls.Add(panelContent);
            this.Controls.Add(panelHeader);
            this.Controls.Add(panelFooter);
        }
    }
}