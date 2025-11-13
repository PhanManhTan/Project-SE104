using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{

    public partial class LoaiKhach_CreateForm : Form
    {

        string connString;
        public LoaiKhach_CreateForm(string connString)
        {
            InitializeComponent();
            this.connString = connString;
            FormSetup();
        }


        private void FormSetup()
        {
            // Thiết lập ComboBox trạng thái
            cboTTSD.Items.Add("Đang sử dụng");
            cboTTSD.Items.Add("Ngừng sử dụng");
            cboTTSD.SelectedIndex = 0;
            cboTTSD.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ClearInputs()
        {
            txtMaLK.Clear();
            txtTenLK.Clear();
            cboTTSD.SelectedIndex = 0; // "Đang sử dụng"
            txtMaLK.Enabled = true;  // Cho phép nhập mã mới
            txtMaLK.Focus();         // Tự động focus vào Mã loại khách
        }

        private void btnCreate_Click(object sender, EventArgs e)
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

            string maLoai = txtMaLK.Text.Trim();
            string tenLoai = txtTenLK.Text.Trim();
            int trangThai = cboTTSD.SelectedIndex == 0 ? 1 : 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"INSERT INTO LoaiKhach (MaLoaiKhach, TenLoaiKhach, TrangThaiSuDung)
                                         VALUES (@MaLoai, @TenLoai, @TrangThai)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        cmd.Parameters.AddWithValue("@TenLoai", tenLoai);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearInputs();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Thêm mới thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void CreateLoaiKhachForm_Load(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void CreateForm_Load(object sender, EventArgs e)
        {

        }

        private void LoaiKhach_CreateForm_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
