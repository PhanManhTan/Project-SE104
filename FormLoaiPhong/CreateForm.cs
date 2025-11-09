using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FormLoaiPhong
{
    public partial class CreateForm : Form
    {
        private string connString;

        public CreateForm(string connString)
        {
            InitializeComponent();
            this.connString = connString;
            FormSetup();
        }

        private void FormSetup()
        {
            cboTTSD.Items.Add("Đang dùng");
            cboTTSD.Items.Add("Không dùng");
            cboTTSD.SelectedIndex = 0;
            cboTTSD.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ClearInputs()
        {
            txtMaLK.Clear();
            txtDG.Clear();
            cboTTSD.SelectedIndex = 0; // "Đang dùng"
            txtMaLK.Enabled = true;     // Cho phép nhập mã mới
            txtMaLK.Focus();            // Tự động focus vào Mã Loại
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(txtMaLK.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Loại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLK.Focus();
                return;
            }

            if (!decimal.TryParse(txtDG.Text, out decimal donGia))
            {
                MessageBox.Show("Đơn giá phải là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDG.Focus();
                return;
            }

            string maLoai = txtMaLK.Text.Trim();
            string trangThai = cboTTSD.SelectedItem.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "INSERT INTO LoaiPhong (MaLoaiPhong, DonGia, TrangThaiSuDung) VALUES (@MaLoai, @DonGia, @TrangThai)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        cmd.Parameters.AddWithValue("@DonGia", donGia);
                        cmd.Parameters.AddWithValue("@TrangThai", cboTTSD.SelectedIndex == 0 ? 1 : 0);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearInputs();
                            this.DialogResult = DialogResult.OK;
                            // Đóng CreateForm
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

        private void Add_Load(object sender, EventArgs e)
        {
            ClearInputs();
        }
    }
}
