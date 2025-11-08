using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        private string connString = "Data Source=ACER\\SQLEXPRESS;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
            SetupComboBox();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Cấu hình ComboBox
        private void SetupComboBox()
        {
            cboTrangThai.Items.Add("Đang dùng");
            cboTrangThai.Items.Add("Không dùng");
            cboTrangThai.SelectedIndex = 0; // Mặc định: Đang dùng
        }

        // TẢI DỮ LIỆU
        private void btnLoad_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData1()
        {
            string query = "SELECT * FROM vw_LoaiPhong";  // DÙNG VIEW
            SqlDataAdapter da = new SqlDataAdapter(query, connString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvLoaiPhong.DataSource = dt;
        }
        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM vw_LoaiPhong";
                SqlDataAdapter da = new SqlDataAdapter(query, connString);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvLoaiPhong.DataSource = dt;

                // ĐỔI TÊN CỘT
                dgvLoaiPhong.Columns["MaLoaiPhong"].HeaderText = "Mã loại phòng";
                dgvLoaiPhong.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvLoaiPhong.Columns["TrangThaiText"].HeaderText = "Trạng thái sử dụng";

                // LOẠI BỎ KHOẢNG TRỐNG 2 BÊN + DƯỚI
               // dgvLoaiPhong.Dock = DockStyle.Fill;
                dgvLoaiPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // CĂN PHẢI CHO CỘT TIỀN
                dgvLoaiPhong.Columns["DonGia"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                // ẨN CỘT MŨI TÊN (LOẠI BỎ PHẦN DƯỚI Ở ĐẦU)
                dgvLoaiPhong.RowHeadersVisible = false;

                // KHÔNG CHO THÊM DÒNG TRỐNG DƯỚI CÙNG
                dgvLoaiPhong.AllowUserToAddRows = false;

                // TỰ ĐỘNG CHỌN DÒNG ĐẦU
                if (dgvLoaiPhong.Rows.Count > 0)
                    dgvLoaiPhong.Rows[0].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // THÊM LOẠI PHÒNG
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"INSERT INTO LoaiPhong (MaLoaiPhong, DonGia, TrangThaiSuDung) 
                                   VALUES (@Ma, @DonGia, @TrangThai)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ma", txtMa.Text.Trim());
                    cmd.Parameters.AddWithValue("@DonGia", decimal.Parse(txtDonGia.Text));
                    cmd.Parameters.AddWithValue("@TrangThai", cboTrangThai.SelectedIndex == 0 ? 1 : 0);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm loại phòng thành công!", "Thành công",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearInput();
                    LoadData();
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // Lỗi trùng khóa chính
                    MessageBox.Show("Mã loại phòng đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // XÓA TRẮNG Ô NHẬP
        private void ClearInput()
        {
            txtMa.Clear();
            txtDonGia.Clear();
            cboTrangThai.SelectedIndex = 0;
            txtMa.Focus();
        }

        // KIỂM TRA ĐẦU VÀO
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã loại phòng!");
                txtMa.Focus();
                return false;
            }

            if (txtMa.Text.Length > 1)
            {
                MessageBox.Show("Mã loại phòng chỉ được 1 ký tự (VD: A, B, C)!");
                txtMa.Focus();
                return false;
            }

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số dương!");
                txtDonGia.Focus();
                return false;
            }

            return true;
        }

        // Click vào dòng để chỉnh sửa (tùy chọn)
        private void dgvLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void dgvLoaiPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}