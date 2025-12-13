using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class Rental : Form
    {
        private readonly string _maPhong;
        private readonly RentalService _rentalService = new RentalService();
        private Label lblSoKhachChon; // Tự tạo nếu chưa có

        public Rental(string maPhong)
        {
            InitializeComponent();
            _maPhong = maPhong;
        }

        private void RentalForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Đặt phòng - Phòng {_maPhong}";
            lblMaPhong.Text = _maPhong;

            dtpNgayThue.Value = DateTime.Today;
            dtpNgayThue.MinDate = DateTime.Today;

            TaoLabelSoKhach(); // Tạo label nếu chưa có
            //LoadKhachHang();
        }

        private void TaoLabelSoKhach()
        {
            lblSoKhachChon = this.Controls.Find("lblSoKhachChon", true).FirstOrDefault() as Label;
            if (lblSoKhachChon == null)
            {
                lblSoKhachChon = new Label
                {
                    Name = "lblSoKhachChon",
                    Text = "Đã chọn: 0 / 3 khách",
                    Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.DarkBlue,
                    AutoSize = true,
                    Location = new System.Drawing.Point(20, 300) // điều chỉnh vị trí
                };
                this.Controls.Add(lblSoKhachChon);
            }
        }

        //private void LoadKhachHang()
        //{
        //    try
        //    {
        //        using (var svc = new CustomerService())
        //        {
        //            var data = svc.GetAllCustomers()
        //                .Select(k => new
        //                {
        //                    MaKhach = k.MaKhach,
        //                    TenHienThi = $"{k.HoTen} ({(string.IsNullOrEmpty(k.CMND) ? "Chưa có CMND" : k.CMND)}) - {(k.MaLoaiKhach == "NN" ? "Nước ngoài" : "Nội địa")}"
        //                })
        //                .OrderBy(x => x.TenHienThi)
        //                .ToList();

        //            clbKhachHang.DisplayMember = "TenHienThi";
        //            clbKhachHang.ValueMember = "MaKhach";
        //            clbKhachHang.DataSource = data;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi tải khách hàng:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void clbKhachHang_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                int count = clbKhachHang.CheckedIndices.Count;
                if (e.NewValue == CheckState.Checked) count++;
                else count--;

                lblSoKhachChon.Text = $"Đã chọn: {count} / 3 khách";

                if (count > 3)
                {
                    e.NewValue = CheckState.Unchecked;
                    MessageBox.Show("Chỉ được chọn tối đa 3 khách!", "Giới hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }));
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            if (clbKhachHang.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 khách!", "Thiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // LẤY MÃ KHÁCH ĐÃ CHỌN – CÁCH CHẮC CHẮN NHẤT
            var dsMaKhach = new List<string>();
            foreach (var item in clbKhachHang.CheckedItems)
            {
                var obj = item as dynamic;
                dsMaKhach.Add(obj.MaKhach);
            }

            DateTime ngayThue = dtpNgayThue.Value.Date;

            bool success = _rentalService.CreateRental(_maPhong, ngayThue, dsMaKhach.ToArray());

            if (success)
            {
                MessageBox.Show($"Đặt phòng {_maPhong} thành công!\nSố khách: {dsMaKhach.Count}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnLapPhieu_Click(null, null);
                return true;
            }
            if (keyData == Keys.Escape)
            {
                btnHuy_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}