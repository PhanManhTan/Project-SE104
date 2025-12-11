using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class KhachHang_UpdateForm : Form
    {
        private string connString;
        private string oldMaKH; // Mã khách hàng cũ dùng để update

        public KhachHang_UpdateForm(string connString, string maKH)
        {
            InitializeComponent();
            this.connString = connString;
            this.oldMaKH = maKH;

            SetupForm();
            LoadLoaiKhach();
            LoadKhachHang(maKH);
        }

        private void SetupForm()
        {
            txtMaKH.Focus();
            cboLoaiKhach.DropDownStyle = ComboBoxStyle.DropDownList;
        }

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

        private void LoadKhachHang(string maKH)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM KhachHang WHERE MaKhach = @MaKH";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMaKH.Text = reader["MaKhach"].ToString();
                                txtHoTen.Text = reader["HoTen"].ToString();
                                txtCMND.Text = reader["CMND"].ToString();
                                txtDiaChi.Text = reader["DiaChi"].ToString();

                                string maLoai = reader["MaLoaiKhach"].ToString();
                                cboLoaiKhach.SelectedValue = maLoai;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            if (txtMaKH.Text.Trim().Length != 3)
            {
                MessageBox.Show("Mã khách hàng phải có đúng 3 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            string newMaKH = txtMaKH.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string cmnd = txtCMND.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string maLoaiKhach = cboLoaiKhach.SelectedValue.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"UPDATE KhachHang SET 
                                     MaKhach = @NewMaKH, 
                                     HoTen = @HoTen, 
                                     CMND = @CMND, 
                                     DiaChi = @DiaChi, 
                                     MaLoaiKhach = @MaLoaiKhach 
                                     WHERE MaKhach = @OldMaKH";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewMaKH", newMaKH);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@CMND", cmnd);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmd.Parameters.AddWithValue("@MaLoaiKhach", maLoaiKhach);
                        cmd.Parameters.AddWithValue("@OldMaKH", oldMaKH);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật khách hàng thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khách hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtCMND.Clear();
            txtDiaChi.Clear();
            if (cboLoaiKhach.Items.Count > 0)
                cboLoaiKhach.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KhachHang_UpdateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
