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
            cboTTSD.Items.Add("Đang sử dụng");
            cboTTSD.Items.Add("Ngừng sử dụng");
            cboTTSD.SelectedIndex = 0;
            cboTTSD.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private bool MaDaTonTai(string ma)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM LoaiPhong WHERE MaLoaiPhong = @ma";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ma", ma);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        private void ClearInputs()
        {
            txtMaLP.Clear();
            txtDG.Clear();
            cboTTSD.SelectedIndex = 0; // "Đang dùng"
            txtMaLP.Enabled = true;     // Cho phép nhập mã mới
            txtMaLP.Focus();            // Tự động focus vào Mã Loại
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            // Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(txtMaLP.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Loại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLP.Focus();
                return;
            }

            if (txtMaLP.Text.Length > 1)
            {
                MessageBox.Show("Mã loại không được quá 1 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MaDaTonTai(txtMaLP.Text))
            {
                MessageBox.Show("Mã loại phòng đã tồn tại!", "Lỗi trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtDG.Text, out decimal donGia))
            {
                MessageBox.Show("Đơn giá phải là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDG.Focus();
                return;
            }

            if(donGia <=0)
            {
                MessageBox.Show("Đơn giá phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDG.Focus();
                return;
            }    

            string maLoai = txtMaLP.Text.Trim();
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

        private void CreateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
