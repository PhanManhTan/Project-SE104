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
    public partial class Phong_CreateForm : Form
    {
        private string connString;
        public Phong_CreateForm(string connString)
        {
            InitializeComponent();
            this.connString = connString;
            SetupForm();
            LoadLoaiPhong();
        }

        private void SetupForm()
        {
            cboTinhTrang.Items.AddRange(new[] { "Trống", "Đã thuê", "Bảo trì" });
            cboTinhTrang.SelectedIndex = 0;
            cboTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;

            btnCreate.Click += btnCreate_Click;
        }

        private void LoadLoaiPhong()
        {
            using (SqlConnection c = new SqlConnection(connString))
            {
                c.Open();
                var da = new SqlDataAdapter("SELECT MaLoaiPhong, DonGia FROM LoaiPhong WHERE TrangThaiSuDung = 1", c);
                var dt = new DataTable();
                da.Fill(dt);
                cboLoaiPhong.DisplayMember = "MaLoaiPhong";
                cboLoaiPhong.ValueMember = "MaLoaiPhong";
                cboLoaiPhong.DataSource = dt;
            }
            cboLoaiPhong.SelectedIndexChanged += (s, e) => UpdateDonGia();
        }

        private void UpdateDonGia()
        {
            if (cboLoaiPhong.SelectedItem is DataRowView drv)
                txtDG.Text = Convert.ToDecimal(drv["DonGia"]).ToString("N0");
            else
                txtDG.Clear();
        }

        private bool MaDaTonTai(string ma)
        {
            using (SqlConnection c = new SqlConnection(connString))
            {
                c.Open();
                var cmd = new SqlCommand("SELECT COUNT(*) FROM Phong WHERE MaPhong = @ma", c);
                cmd.Parameters.AddWithValue("@ma", ma);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string ma = txtMaPhong.Text.Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(ma))
            {
                MessageBox.Show("Vui lòng nhập mã phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhong.Focus();
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(ma, @"^P\d{2}$"))
            {
                MessageBox.Show("Mã phòng phải có dạng P + 2 số (VD: P01, P12)!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhong.Focus();
                return;
            }
            
            if (MaDaTonTai(ma))
            {
                MessageBox.Show("Mã phòng đã tồn tại!", "Lỗi trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboLoaiPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection c = new SqlConnection(connString))
                {
                    c.Open();
                    var cmd = new SqlCommand("INSERT INTO Phong (MaPhong, MaLoaiPhong, TinhTrang, GhiChu) VALUES (@ma, @loai, @tt, @gc)", c);
                    cmd.Parameters.AddWithValue("@ma", ma);
                    cmd.Parameters.AddWithValue("@loai", cboLoaiPhong.SelectedValue);
                    cmd.Parameters.AddWithValue("@tt", cboTinhTrang.Text.Trim());
                    cmd.Parameters.AddWithValue("@gc", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Thêm phòng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void CreatePhongForm_Load(object sender, EventArgs e)
        {

        }
    }
}
