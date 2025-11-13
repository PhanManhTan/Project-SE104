using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class KhachHang_CreateForm : Form
    {
        private string connString;

        public KhachHang_CreateForm(string connString)
        {
            InitializeComponent();
            this.connString = connString;

            SetupForm();
            LoadLoaiKhach();
        }

        // Thiết lập form ban đầu
        private void SetupForm()
        {
            txtMaKH.Focus(); // focus vào Mã khách hàng
            cboLoaiKhach.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // Load danh sách loại khách vào ComboBox
        private void LoadLoaiKhach()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT MaLoaiKhach, TenLoaiKhach FROM LoaiKhach WHERE TrangThaiSuDung = 1";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cboLoaiKhach.DataSource = dt;
                        cboLoaiKhach.DisplayMember = "TenLoaiKhach";
                        cboLoaiKhach.ValueMember = "MaLoaiKhach";

                        if (dt.Rows.Count > 0)
                            cboLoaiKhach.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại khách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa dữ liệu trên form
        private void ClearInputs()
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtCMND.Clear();
            txtDiaChi.Clear();

            if (cboLoaiKhach.Items.Count > 0)
                cboLoaiKhach.SelectedIndex = 0;

            txtMaKH.Focus();
        }

        // Sự kiện click nút Create
        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu hợp lệ
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            if (txtMaKH.Text.Trim().Length != 3)
            {
                MessageBox.Show("Mã khách hàng phải có đúng 6 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCMND.Text))
            {
                MessageBox.Show("Vui lòng nhập CMND/CCCD.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCMND.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            string maKH = txtMaKH.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string cmnd = txtCMND.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string maLoaiKhach = cboLoaiKhach.SelectedValue.ToString();

            // Insert vào database
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"INSERT INTO KhachHang 
                                     (MaKhach, HoTen, CMND, DiaChi, MaLoaiKhach) 
                                     VALUES (@MaKH, @HoTen, @CMND, @DiaChi, @MaLoaiKhach)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@CMND", cmnd);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmd.Parameters.AddWithValue("@MaLoaiKhach", maLoaiKhach);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearInputs();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Thêm khách hàng thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện click nút Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KhachHang_CreateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
