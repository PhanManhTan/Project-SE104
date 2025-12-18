using Data;
using Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class BillsForm : Form
    {
        private BillSummaryViewModel selectedBillView = null;
        private BindingSource bindingSource = new BindingSource();

        public BillsForm()
        {
            InitializeComponent();
        }

        private void BillManager_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            ConfigureDataGridViewColumns();
            LoadBills();
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
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);
            dgv.ScrollBars = ScrollBars.Vertical;

            dgv.DataBindingComplete += Dgv_DataBindingComplete;
            dgv.SelectionChanged += Dgv_SelectionChanged;
        }

        private void Dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgvBody.Rows.Count; i++)
            {
                dgvBody.Rows[i].Cells["STT"].Value = (i + 1).ToString();
            }
            dgvBody.ClearSelection();
            selectedBillView = null;
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
                Name = "MaHoaDon",
                HeaderText = "Mã hóa đơn",
                DataPropertyName = "MaHoaDon",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayLap",
                HeaderText = "Ngày lập",
                DataPropertyName = "NgayLap",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Format = "dd/MM/yyyy"
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuongPhieu",
                HeaderText = "Số phiếu thuê",
                DataPropertyName = "SoLuongPhieu",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TriGia",
                HeaderText = "Tổng tiền",
                DataPropertyName = "TriGiaFormatted",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N0",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            });

            dgv.DataSource = bindingSource;
        }

        private void LoadBills()
        {
            using (var db = new DBDataContext())
            {
                var list = db.HoaDons
                    .Select(h => new BillSummaryViewModel
                    {
                        MaHoaDon = h.MaHoaDon,
                        NgayLap = h.NgayLap,
                        SoLuongPhieu = h.ChiTietHoaDons.Count,
                        TriGia = h.TriGia
                    })
                    .OrderByDescending(h => h.NgayLap)
                    .ToList();

                bindingSource.DataSource = list;
            }
        }

        public void RefreshGrid()
        {
            LoadBills();
            selectedBillView = null;
            dgvBody.ClearSelection();
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBody.CurrentRow?.Index >= 0)
            {
                selectedBillView = dgvBody.CurrentRow.DataBoundItem as BillSummaryViewModel;
            }
            else
            {
                selectedBillView = null;
            }
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            if (selectedBillView == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var form = new BillDetail(selectedBillView.MaHoaDon);
            form.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (selectedBillView == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để in!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show($"Chức năng in hóa đơn {selectedBillView.MaHoaDon} đang được phát triển.", "Thông tin",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedBillView == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa hóa đơn \"{selectedBillView.MaHoaDon}\" (Ngày: {selectedBillView.NgayLap:dd/MM/yyyy}) không?\n" +
                "Thao tác này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                using (var db = new DBDataContext())
                {
                    var hoaDon = db.HoaDons.FirstOrDefault(h => h.MaHoaDon == selectedBillView.MaHoaDon);
                    if (hoaDon != null)
                    {
                        db.ChiTietHoaDons.DeleteAllOnSubmit(hoaDon.ChiTietHoaDons);
                        db.HoaDons.DeleteOnSubmit(hoaDon);
                        db.SubmitChanges();
                    }
                }

                RefreshGrid();
                MessageBox.Show("Xóa hóa đơn thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hóa đơn:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}