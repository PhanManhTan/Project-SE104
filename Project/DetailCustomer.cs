using Data;
using Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Project
{
    public partial class DetailCustomer : Form
    {
        private KhachHang cur = null;
        private readonly CustomerService customerService = new CustomerService();

        public DetailCustomer()
        {
            InitializeComponent();
            this.Text = "Thêm khách hàng mới";
        }

        public DetailCustomer(KhachHang kh)
        {
            InitializeComponent();
            cur = kh;
            this.Text = "Sửa thông tin khách hàng";
        }

        private void DetailCustomer_Load(object sender, EventArgs e)
        {
            // Load loại khách
            var loaiKhachList = customerService.GetAllCustomerTypes();
            cbLoaiKhach.DisplayMember = "TenLoaiKhach";
            cbLoaiKhach.ValueMember = "MaLoaiKhach";
            cbLoaiKhach.DataSource = loaiKhachList;

            if (cur != null) // Chế độ sửa
            {
                tbMaKhachHang.Text = cur.MaKhach?.Trim() ?? "";
                tbHoTen.Text = cur.HoTen?.Trim() ?? "";
                tbCMND.Text = cur.CMND?.Trim() ?? "";
                tbDiaChi.Text = cur.DiaChi?.Trim() ?? "";

                // An toàn hơn: kiểm tra SelectedValue có tồn tại không
                if (!string.IsNullOrEmpty(cur.MaLoaiKhach))
                {
                    string maLoai = cur.MaLoaiKhach.Trim();
                    if (loaiKhachList.Any(lk => lk.MaLoaiKhach == maLoai))
                        cbLoaiKhach.SelectedValue = maLoai;
                    else
                        cbLoaiKhach.SelectedIndex = -1; // Không tìm thấy loại khách (đã bị xóa?)
                }

                tbMaKhachHang.Enabled = false;
            }
            else // Chế độ thêm mới
            {
                tbMaKhachHang.Text = GenerateNewMaKhach();
                tbMaKhachHang.Enabled = false;
                cbLoaiKhach.SelectedIndex = -1;
            }
        }

        private string GenerateNewMaKhach()
        {
            try
            {
                var last = customerService.GetAllCustomersFromEntity()
                    .Where(k => !string.IsNullOrEmpty(k.MaKhach) && k.MaKhach.StartsWith("KH"))
                    .Select(k => k.MaKhach)
                    .OrderByDescending(m => m)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(last))
                    return "KH001";

                string numberPart = last.Substring(2);
                if (int.TryParse(numberPart, out int num))
                    return "KH" + (num + 1).ToString("D3");
            }
            catch
            {
                // Không bao giờ nên xảy ra, nhưng phòng thân
            }

            return "KH001";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(tbHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbCMND.Text))
            {
                MessageBox.Show("Vui lòng nhập CMND/CCCD!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbCMND.Focus();
                return;
            }

            if (cbLoaiKhach.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại khách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLoaiKhach.Focus();
                return;
            }

            var kh = new KhachHang
            {
                MaKhach = tbMaKhachHang.Text.Trim(),
                HoTen = tbHoTen.Text.Trim(),
                CMND = tbCMND.Text.Trim(),
                DiaChi = tbDiaChi.Text.Trim(),
                MaLoaiKhach = cbLoaiKhach.SelectedValue.ToString()
            };

            bool success = cur != null
                ? customerService.UpdateCustomer(kh)
                : customerService.AddCustomer(kh);

            if (success)
            {
                MessageBox.Show(cur != null ? "Cập nhật khách hàng thành công!" : "Thêm khách hàng thành công!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Lấy danh sách 1 lần duy nhất để kiểm tra lỗi
                var allCustomers = customerService.GetAllCustomersFromEntity();

                string msg = "Không thể lưu khách hàng!\n\nLý do:\n";
                bool hasError = false;

                if (allCustomers.Any(k => string.Equals(k.MaKhach, kh.MaKhach, StringComparison.OrdinalIgnoreCase)))
                {
                    msg += "• Mã khách hàng đã tồn tại.\n";
                    hasError = true;
                }

                if (allCustomers.Any(k => string.Equals(k.CMND, kh.CMND, StringComparison.OrdinalIgnoreCase) &&
                                          (cur == null || !string.Equals(k.MaKhach, cur.MaKhach, StringComparison.OrdinalIgnoreCase))))
                {
                    msg += "• CMND/CCCD đã được sử dụng bởi khách hàng khác.\n";
                    hasError = true;
                }

                if (!hasError)
                    msg += "• Lỗi không xác định (kiểm tra kết nối CSDL hoặc quyền truy cập).";

                MessageBox.Show(msg, "Lỗi lưu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}