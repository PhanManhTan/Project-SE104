using Data;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class BillDetail : Form
    {
        private string maHoaDon;
        private HoaDon hoaDon;
        private BindingSource bindingSource = new BindingSource();

        public BillDetail(string _maHoaDon)
        {
            this.maHoaDon = _maHoaDon;
            InitializeComponent();
        }

        private void DetailBill_Load(object sender, EventArgs e)
        {
            LoadHoaDonDetail();

            // Cấu hình DataGridView giống CustomerManager/BillManager
            SetupDataGridView();
            ConfigureColumns();
            LoadChiTietHoaDon();

            // Phân quyền: Staff không được in (nếu cần)
            if (Global_.CurrentUser?.Role_ == "Staff")
            {
                btnPrint.Enabled = false;
                btnPrint.BackColor = Color.DarkGray;
            }
        }

        private void LoadHoaDonDetail()
        {
            using (var db = new DBDataContext())
            {
                hoaDon = db.HoaDons.FirstOrDefault(h => h.MaHoaDon == maHoaDon);
                if (hoaDon == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Hiển thị thông tin chung
                lblMaHoaDon.Text = hoaDon.MaHoaDon;
                lblNgayLap.Text = hoaDon.NgayLap.ToString("dd/MM/yyyy");
                lblTriGia.Text = hoaDon.TriGia.ToString("N0") + " VNĐ";

                // Lấy thông tin khách hàng đại diện (khách chính đầu tiên trong các phiếu)
                var khachChinh = db.ChiTietHoaDons
                    .Where(ct => ct.MaHoaDon == maHoaDon)
                    .SelectMany(ct => ct.PhieuThue.ChiTietPhieuThues)
                    .FirstOrDefault(ctpt => ctpt.VaiTro == "Chinh");

                if (khachChinh != null)
                {
                    lblKhachHang.Text = khachChinh.KhachHang.HoTen;
                    lblDiaChi.Text = khachChinh.KhachHang.DiaChi ?? "Không có";
                }
                else
                {
                    lblKhachHang.Text = "Không xác định";
                    lblDiaChi.Text = "-";
                }
            }
        }

        #region === CẤU HÌNH DATAGRIDVIEW ===

        private void SetupDataGridView()
        {
            var dgv = dgvChiTiet;
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
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
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            dgv.DataBindingComplete += (s, e) =>
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells["STT"].Value = (i + 1).ToString();
                }
            };
        }

        private void ConfigureColumns()
        {
            var dgv = dgvChiTiet;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            // STT
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "STT",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Phòng",
                Name = "Phong",
                DataPropertyName = "TenPhong",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Số Ngày Thuê",
                Name = "SoNgayThue",
                DataPropertyName = "SoNgayThue",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Đơn Giá",
                Name = "DonGia",
                DataPropertyName = "DonGia",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N0"
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Thành Tiền",
                Name = "ThanhTien",
                DataPropertyName = "ThanhTien",
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

        private void LoadChiTietHoaDon()
        {
            using (var db = new DBDataContext())
            {
                var data = db.ChiTietHoaDons
                    .Where(ct => ct.MaHoaDon == maHoaDon)
                    .Select(ct => new
                    {
                        TenPhong = ct.PhieuThue.Phong.MaPhong + " (" + ct.PhieuThue.Phong.LoaiPhong.TenLoaiPhong + ")",
                        ct.SoNgayThue,
                        DonGia = ct.PhieuThue.Phong.LoaiPhong.DonGia,
                        ct.ThanhTien
                    })
                    .ToList();

                bindingSource.DataSource = data;
            }
        }

        #endregion

        #region === IN HÓA ĐƠN ===

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintPage);
            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pd;
            preview.ShowDialog();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontBold = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fontNormal = new Font("Segoe UI", 11);
            Font fontSmall = new Font("Segoe UI", 9);
            int y = 50;

            // Tiêu đề
            g.DrawString("HÓA ĐƠN THANH TOÁN", fontBold, Brushes.Black, new PointF(300, y));
            y += 50;

            // Thông tin hóa đơn
            g.DrawString($"Mã hóa đơn: {lblMaHoaDon.Text}", fontNormal, Brushes.Black, new PointF(50, y));
            g.DrawString($"Ngày lập: {lblNgayLap.Text}", fontNormal, Brushes.Black, new PointF(500, y));
            y += 30;

            g.DrawString($"Khách hàng/Cơ quan: {lblKhachHang.Text}", fontNormal, Brushes.Black, new PointF(50, y));
            g.DrawString($"Địa chỉ: {lblDiaChi.Text}", fontNormal, Brushes.Black, new PointF(500, y));
            y += 50;

            // Bảng chi tiết
            string[] headers = { "STT", "Phòng", "Số Ngày Thuê", "Đơn Giá", "Thành Tiền" };
            int[] widths = { 60, 200, 120, 150, 150 };
            int x = 50;

            for (int i = 0; i < headers.Length; i++)
            {
                g.FillRectangle(Brushes.Navy, x, y, widths[i], 30);
                g.DrawString(headers[i], fontBold, Brushes.White, new RectangleF(x, y, widths[i], 30), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                x += widths[i];
            }
            y += 30;

            int stt = 1;
            foreach (var row in (System.Collections.IEnumerable)dgvChiTiet.DataSource)
            {
                x = 50;
                g.DrawString(stt.ToString(), fontNormal, Brushes.Black, new RectangleF(x, y, widths[0], 30), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                x += widths[0];

                var props = row.GetType().GetProperties();
                g.DrawString(props[0].GetValue(row).ToString(), fontNormal, Brushes.Black, new RectangleF(x, y, widths[1], 30), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                x += widths[1];
                g.DrawString(props[1].GetValue(row).ToString(), fontNormal, Brushes.Black, new RectangleF(x, y, widths[2], 30), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                x += widths[2];
                g.DrawString(((decimal)props[2].GetValue(row)).ToString("N0"), fontNormal, Brushes.Black, new RectangleF(x, y, widths[3], 30), new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
                x += widths[3];
                g.DrawString(((decimal)props[3].GetValue(row)).ToString("N0"), fontNormal, Brushes.Black, new RectangleF(x, y, widths[4], 30), new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });

                y += 30;
                stt++;
            }

            // Tổng tiền
            y += 20;
            g.DrawString($"Tổng cộng: {lblTriGia.Text}", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Black, new PointF(500, y));
        }

        #endregion
    }
}