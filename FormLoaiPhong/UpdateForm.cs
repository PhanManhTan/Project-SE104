using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FormLoaiPhong
{
    public partial class UpdateForm : Form
    {
        private string connString;
        private string oldMaLoai; // Lưu mã loại cũ để dùng trong câu UPDATE

        public UpdateForm(string connString, string maLoai)
        {
            InitializeComponent();
            this.connString = connString;
            this.oldMaLoai = maLoai;
            SetupForm();
            LoadLoaiPhong(maLoai);
        }

        private void SetupForm()
        {
            cboTTSD.Items.Add("Đang sử dụng");
            cboTTSD.Items.Add("Ngưng sử dụng");
            cboTTSD.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private bool MaDaTonTai(string ma, string ignoreMa)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM LoaiPhong WHERE MaLoaiPhong = @ma AND MaLoaiPhong <> @ignore";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ma", ma);
                    cmd.Parameters.AddWithValue("@ignore", ignoreMa);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        private void LoadLoaiPhong(string maLoai)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM LoaiPhong WHERE MaLoaiPhong = @MaLoai";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMaLP.Text = reader["MaLoaiPhong"].ToString();
                                txtDG.Text = reader["DonGia"].ToString();
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

            if (txtMaLP.Text != oldMaLoai && MaDaTonTai(txtMaLP.Text, oldMaLoai))
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

            string newMaLoai = txtMaLP.Text.Trim();
            int trangThai = cboTTSD.SelectedIndex == 0 ? 1 : 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE LoaiPhong SET MaLoaiPhong=@NewMa, DonGia=@DonGia, TrangThaiSuDung=@TrangThai WHERE MaLoaiPhong=@OldMa";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewMa", newMaLoai);
                        cmd.Parameters.AddWithValue("@DonGia", donGia);
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
            txtMaLP.Focus();
        }
    }
}
