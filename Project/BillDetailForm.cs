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


        private string _tenKhach = "";
        private string _diaChi = "";
        private decimal _tongTien = 0;

        public BillDetail(string _maHoaDon)
        {
            this.maHoaDon = _maHoaDon;
            InitializeComponent(); 
        }

        private void DetailBill_Load(object sender, EventArgs e)
        {
            LoadHoaDonDetail();
            SetupDataGridViewStyle();  
            ConfigureColumns();       
            LoadChiTietHoaDon();       
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

                // Cập nhật Label tiêu đề (đã khai báo bên Designer)
                lblSubTitle.Text = $"Mã HĐ: {hoaDon.MaHoaDon}  |  Ngày lập: {hoaDon.NgayLap:dd/MM/yyyy}";
                _tongTien = hoaDon.TriGia;

                // Tìm khách hàng đại diện
                var khachChinh = db.ChiTietHoaDons
                    .Where(ct => ct.MaHoaDon == maHoaDon)
                    .SelectMany(ct => ct.PhieuThue.ChiTietPhieuThues)
                    .FirstOrDefault(ctpt => ctpt.VaiTro == "Chinh");

                if (khachChinh != null)
                {
                    _tenKhach = khachChinh.KhachHang.HoTen;
                    _diaChi = khachChinh.KhachHang.DiaChi ?? "";
                }
                else
                {
                    _tenKhach = "Khách vãng lai";
                    _diaChi = "";
                }

                panelInfo.Invalidate();
            }
        }


        private void PanelInfo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            Brush brushText = Brushes.Black;
            Font fontTitle = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fontValue = new Font("Segoe UI", 11);
            Font fontTotal = new Font("Segoe UI", 12, FontStyle.Bold);

            int w = panelInfo.Width - 40; 
            int h = panelInfo.Height - 10;
            int x = 20;
            int y = 0;

            int midX = x + (w / 2);
            int midY = y + (h / 2);

            // Vẽ khung
            g.DrawRectangle(pen, x, y, w, h);
            g.DrawLine(pen, midX, y, midX, y + h); 
            g.DrawLine(pen, midX, midY, x + w, midY); 

            // Điền chữ
            g.DrawString("Khách hàng/Cơ quan:", fontTitle, brushText, x + 10, y + 15);
            g.DrawString(_tenKhach, fontValue, brushText, x + 180, y + 13);

            g.DrawString("Địa chỉ:", fontTitle, brushText, midX + 10, y + 15);
            g.DrawString(_diaChi, fontValue, brushText, midX + 80, y + 13);

            g.DrawString("Trị giá:", fontTitle, brushText, midX + 10, midY + 15);
            g.DrawString($"{_tongTien:N0} VNĐ", fontTotal, Brushes.Red, midX + 80, midY + 12);
        }

        // === CẤU HÌNH GRID ===
        private void SetupDataGridViewStyle()
        {
            var dgv = dgvBody;

            // Header
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Rows
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 90, 180);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            // STT Tự động
            dgv.DataBindingComplete += (s, e) => {
                for (int i = 0; i < dgv.Rows.Count; i++) dgv.Rows[i].Cells["STT"].Value = (i + 1).ToString();
                dgv.ClearSelection();
            };
        }

        private void ConfigureColumns()
        {
            var dgv = dgvBody;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "STT", Name = "STT", Width = 50, AutoSizeMode = DataGridViewAutoSizeColumnMode.None, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 10F, FontStyle.Bold) } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Phòng", DataPropertyName = "TenPhong", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số Ngày Thuê", DataPropertyName = "SoNgayThue", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đơn Giá", DataPropertyName = "DonGia", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thành Tiền", DataPropertyName = "ThanhTien", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.Navy } });

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
                        TenPhong = ct.PhieuThue.Phong.MaPhong,
                        ct.SoNgayThue,
                        DonGia = ct.PhieuThue.Phong.LoaiPhong.DonGia,
                        ct.ThanhTien
                    })
                    .ToList();
                bindingSource.DataSource = data;
            }
        }

        // === IN HÓA ĐƠN ===
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintPage);
            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pd;
            preview.Width = 800; preview.Height = 600;
            preview.ShowDialog();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontBold = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fontNormal = new Font("Segoe UI", 11);
            int y = 50;

            g.DrawString("HÓA ĐƠN THANH TOÁN", fontBold, Brushes.Black, new PointF(300, y));
            y += 50;
            g.DrawString($"Mã hóa đơn: {maHoaDon}", fontNormal, Brushes.Black, new PointF(50, y));
            g.DrawString($"Ngày lập: {hoaDon.NgayLap:dd/MM/yyyy}", fontNormal, Brushes.Black, new PointF(500, y));
            y += 30;
            g.DrawString($"Khách hàng: {_tenKhach}", fontNormal, Brushes.Black, new PointF(50, y));
            g.DrawString($"Địa chỉ: {_diaChi}", fontNormal, Brushes.Black, new PointF(500, y));
            y += 50;


            int[] widths = { 50, 200, 150, 150, 150 };
            string[] headers = { "STT", "Phòng", "Số Ngày", "Đơn Giá", "Thành Tiền" };
            int x = 50;

            for (int i = 0; i < headers.Length; i++)
            {
                g.DrawRectangle(Pens.Black, x, y, widths[i], 30);
                g.DrawString(headers[i], new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Black, x + 5, y + 5);
                x += widths[i];
            }
            y += 30;


            int stt = 1;
            foreach (DataGridViewRow row in dgvBody.Rows)
            {
                x = 50;
                string[] values = { stt.ToString(), row.Cells[1].Value?.ToString(), row.Cells[2].Value?.ToString(), string.Format("{0:N0}", row.Cells[3].Value), string.Format("{0:N0}", row.Cells[4].Value) };
                for (int i = 0; i < values.Length; i++)
                {
                    g.DrawRectangle(Pens.Black, x, y, widths[i], 30);
                    g.DrawString(values[i], fontNormal, Brushes.Black, x + 5, y + 5);
                    x += widths[i];
                }
                y += 30;
                stt++;
            }
            y += 20;
            g.DrawString($"Tổng cộng: {_tongTien:N0} VNĐ", fontBold, Brushes.Black, new PointF(500, y));
        }
    }
}