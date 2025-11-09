using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TestFormLoaiPhong
{
    public partial class Form1 : Form
    {
        public string connString = "Data Source=CCCUATAZ\\SQLEXPRESS01;Initial Catalog=QLKS99;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        private string _oldMa = "";

        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // ComboBox
            cboTrangThai.Items.Add("Đang dùng");
            cboTrangThai.Items.Add("Không dùng");
            cboTrangThai.SelectedIndex = 0;

            // DataGridView
            dgvLoaiPhong.RowHeadersVisible = false;
            dgvLoaiPhong.AllowUserToAddRows = false;
            dgvLoaiPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoaiPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiPhong.MultiSelect = false;

            // GẮN SỰ KIỆN CHỈ 1 LẦN
            dgvLoaiPhong.SelectionChanged += dgvLoaiPhong_SelectionChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_LoaiPhong", connString))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // TẮT SỰ KIỆN TRƯỚC KHI GÁN DỮ LIỆU
                    dgvLoaiPhong.SelectionChanged -= dgvLoaiPhong_SelectionChanged;

                    dgvLoaiPhong.DataSource = dt;

                    // Cấu hình cột
                    if (dgvLoaiPhong.Columns["MaLoaiPhong"] != null)
                        dgvLoaiPhong.Columns["MaLoaiPhong"].HeaderText = "Mã loại phòng";
                    if (dgvLoaiPhong.Columns["DonGia"] != null)
                    {
                        dgvLoaiPhong.Columns["DonGia"].HeaderText = "Đơn giá";
                        dgvLoaiPhong.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvLoaiPhong.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                    }
                    if (dgvLoaiPhong.Columns["TrangThaiSuDung"] != null)
                        dgvLoaiPhong.Columns["TrangThaiSuDung"].HeaderText = "Trạng thái sử dụng";

                    // CHỌN DÒNG ĐẦU + HIỂN THỊ DỮ LIỆU
                    if (dt.Rows.Count > 0)
                    {
                        dgvLoaiPhong.ClearSelection();
                        dgvLoaiPhong.Rows[0].Selected = true;
                        dgvLoaiPhong.CurrentCell = dgvLoaiPhong.Rows[0].Cells[0];
                        FillInputsFromRow(dgvLoaiPhong.Rows[0]);
                    }
                    else
                    {
                        ClearInputs();
                    }

                    // BẬT LẠI SỰ KIỆN
                    dgvLoaiPhong.SelectionChanged += dgvLoaiPhong_SelectionChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillInputsFromRow(DataGridViewRow row)
        {
            if (row == null) return;

            _oldMa = row.Cells["MaLoaiPhong"].Value?.ToString().Trim() ?? "";
            txtMa.Text = _oldMa;
            txtDonGia.Text = row.Cells["DonGia"].Value?.ToString() ?? "";
            string trangThai = row.Cells["TrangThaiSuDung"].Value?.ToString() ?? "Đang dùng";
            cboTrangThai.SelectedIndex = trangThai == "Đang dùng" ? 0 : 1;
        }

        private void dgvLoaiPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaiPhong.SelectedRows.Count > 0)
                FillInputsFromRow(dgvLoaiPhong.SelectedRows[0]);
        }

        private void ClearInputs()
        {
            txtMa.Clear();
            txtDonGia.Clear();
            cboTrangThai.SelectedIndex = 0;
            _oldMa = "";
            txtMa.Enabled = true;
            txtMa.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã loại phòng!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMa.Focus();
                return false;
            }
            if (txtMa.Text.Length > 1)
            {
                MessageBox.Show("Mã loại phòng chỉ được 1 ký tự!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMa.Focus();
                return false;
            }
            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số dương!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }
            return true;
        }

        private bool IsMaExists(string ma)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM LoaiPhong WHERE MaLoaiPhong = @Ma AND MaLoaiPhong <> @OldMa";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ma", ma);
                        cmd.Parameters.AddWithValue("@OldMa", _oldMa);
                        return (int)cmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch { return false; }
        }

        // ========== NÚT THÊM ==========
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string ma = txtMa.Text.Trim();
            if (IsMaExists(ma))
            {
                MessageBox.Show("Mã loại phòng đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "INSERT INTO LoaiPhong (MaLoaiPhong, DonGia, TrangThaiSuDung) VALUES (@Ma, @DonGia, @TTS)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ma", ma);
                        cmd.Parameters.AddWithValue("@DonGia", decimal.Parse(txtDonGia.Text));
                        cmd.Parameters.AddWithValue("@TTS", cboTrangThai.SelectedIndex == 0 ? 1 : 0);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Thêm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ========== NÚT SỬA ==========
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLoaiPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng để sửa!", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;

            string newMa = txtMa.Text.Trim();
            if (newMa != _oldMa && IsMaExists(newMa))
            {
                MessageBox.Show("Mã loại phòng đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE LoaiPhong SET MaLoaiPhong = @NewMa, DonGia = @DonGia, TrangThaiSuDung = @TTS WHERE MaLoaiPhong = @OldMa";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewMa", newMa);
                        cmd.Parameters.AddWithValue("@OldMa", _oldMa);
                        cmd.Parameters.AddWithValue("@DonGia", decimal.Parse(txtDonGia.Text));
                        cmd.Parameters.AddWithValue("@TTS", cboTrangThai.SelectedIndex == 0 ? 1 : 0);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Sửa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ========== NÚT XÓA ==========
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLoaiPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng để xóa!", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ma = dgvLoaiPhong.SelectedRows[0].Cells["MaLoaiPhong"].Value.ToString();
            if (MessageBox.Show($"Xóa loại phòng '{ma}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM LoaiPhong WHERE MaLoaiPhong = @Ma", conn))
                        {
                            cmd.Parameters.AddWithValue("@Ma", ma);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    MessageBox.Show("Không thể xóa: Loại phòng đang được sử dụng!", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        // ========== NÚT REFRESH ==========
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadData();
        }
    }
}