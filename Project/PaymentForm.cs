using Services;
using Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class PaymentForm : Form
    {
        private List<string> _maPhieus;
        private BindingSource bindingSource = new BindingSource();
        private decimal _totalAmount = 0;
        private string _tenKhach = "";
        private string _diaChi = "";

        public PaymentForm(List<string> maPhieus)
        {
            InitializeComponent();
            _maPhieus = maPhieus ?? new List<string>();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns();
            LoadData();
        }

        #region === CẤU HÌNH GIAO DIỆN DATAGRIDVIEW ===
        private void SetupDataGridView()
        {
            var dgv = dgvBill;
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
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);
            dgv.ScrollBars = ScrollBars.Vertical;

            dgv.DataBindingComplete += DgvBill_DataBindingComplete;
        }

        private void DgvBill_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvBill.Rows.Count; i++)
            {
                dgvBill.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }

            this.BeginInvoke((MethodInvoker)(() =>
            {
                dgvBill.ClearSelection();
            }));
        }
        #endregion

        #region === CẤU HÌNH CÁC CỘT ===
        private void ConfigureDataGridViewColumns()
        {
            var dgv = dgvBill;
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
                Name = "MaPhong",
                HeaderText = "Phòng",
                DataPropertyName = "MaPhong",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoNgay",
                HeaderText = "Số Ngày",
                DataPropertyName = "SoNgay",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Đơn Giá",
                DataPropertyName = "DonGia",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N0"
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ThanhTien",
                HeaderText = "Thành Tiền",
                DataPropertyName = "ThanhTien",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Format = "N0"
                }
            });

            dgv.DataSource = bindingSource;
        }
        #endregion

        #region === LOAD DỮ LIỆU ===
        private void LoadData()
        {
            try
            {
                var service = new BillingService();
                var list = new List<BillDetailItemViewModel>();
                _totalAmount = 0;
                _tenKhach = "";
                _diaChi = "";

                foreach (var ma in _maPhieus)
                {
                    var bill = service.GetBillPreview(ma);
                    if (bill != null)
                    {
                        list.Add(bill);
                        _totalAmount += bill.ThanhTien;

                        // Lấy thông tin khách từ phiếu đầu tiên (hoặc bạn có thể lấy riêng từ service nếu cần)
                        if (string.IsNullOrEmpty(_tenKhach))
                        {
                            _tenKhach = bill.TenKhach ?? "Khách lẻ";
                            _diaChi = bill.DiaChi ?? "-";
                        }
                    }
                }

                bindingSource.DataSource = list;
                UpdateHeaderInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu thanh toán:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                bindingSource.DataSource = null;
                UpdateHeaderInfo(); // Đặt về mặc định
            }
        }

        private void UpdateHeaderInfo()
        {
            lblTenKhach.Text = _tenKhach;
            lblDiaChiValue.Text = _diaChi;
            lblTotalAmount.Text = $"{_totalAmount:N0} VNĐ";
        }
        #endregion

        #region === NÚT CHỨC NĂNG ===
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (bindingSource.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để thanh toán!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Xác nhận thanh toán hóa đơn với tổng tiền {_totalAmount:N0} VNĐ?",
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            var service = new BillingService();
            try
            {
                string[] dsMa = _maPhieus.ToArray();
                string maHD = service.LapHoaDon(DateTime.Now, dsMa);

                if (!string.IsNullOrEmpty(maHD))
                {
                    MessageBox.Show(
                        $"Thanh toán thành công!\n\nMã hóa đơn: {maHD}\nTổng tiền: {_totalAmount:N0} VNĐ",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại. Vui lòng kiểm tra lại dữ liệu.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống khi lập hóa đơn:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        private void lblDiaChiValue_Click(object sender, EventArgs e)
        {

        }
    }
}