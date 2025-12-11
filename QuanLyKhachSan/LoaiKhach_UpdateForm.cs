using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class LoaiKhach_UpdateForm : Form
    {
        private string connString;
        private string oldMaLoai; // Lưu mã loại khách cũ

        public LoaiKhach_UpdateForm(string connString, string maLoai)
        {
            InitializeComponent();
            this.connString = connString;
            this.oldMaLoai = maLoai;
            SetupForm();
            LoadLoaiKhach(maLoai);
        }

        private void SetupForm()
        {
            cboTTSD.Items.Add("Đang sử dụng");
            cboTTSD.Items.Add("Ngừng sử dụng");
            cboTTSD.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadLoaiKhach(string maLoai)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM LoaiKhach WHERE MaLoaiKhach = @MaLoai";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMaLK.Text = reader["MaLoaiKhach"].ToString();
                                txtTenLK.Text = reader["TenLoaiKhach"].ToString();
                                int trangThai = Convert.ToInt32(reader["TrangThaiSuDung"]);
                                cboTTSD.SelectedIndex = trangThai == 1 ? 0 : 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Kiểm tra Mã loại khách
            if (string.IsNullOrWhiteSpace(txtMaLK.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã loại khách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLK.Focus();
                return;
            }

            if (txtMaLK.Text.Trim().Length != 4)
            {
                MessageBox.Show("Mã loại khách phải có đúng 4 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLK.Focus();
                return;
            }

            // Kiểm tra Tên loại khách
            if (string.IsNullOrWhiteSpace(txtTenLK.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên loại khách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLK.Focus();
                return;
            }

            string newMaLoai = txtMaLK.Text.Trim();
            string newTenLoai = txtTenLK.Text.Trim();
            int trangThai = cboTTSD.SelectedIndex == 0 ? 1 : 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"UPDATE LoaiKhach 
                             SET MaLoaiKhach=@NewMa, TenLoaiKhach=@TenLoai, TrangThaiSuDung=@TrangThai 
                             WHERE MaLoaiKhach=@OldMa";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewMa", newMaLoai);
                        cmd.Parameters.AddWithValue("@TenLoai", newTenLoai);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        cmd.Parameters.AddWithValue("@OldMa", oldMaLoai);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            txtMaLK.Focus();
        }

        private void LoaiKhach_UpdateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
