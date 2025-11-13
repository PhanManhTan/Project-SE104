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

namespace FormPhong
{
    public partial class UpdatePhongForm : Form
    {
        private string connString;
        private string oldMaPhong;

        public UpdatePhongForm(string connString, string maPhong)
        {
            InitializeComponent();
            this.connString = connString;
            this.oldMaPhong = maPhong;
            SetupForm();
            LoadLoaiPhong();
            LoadData(maPhong);
        }

        private void SetupForm()
        {
            cboTinhTrang.Items.AddRange(new[] { "Trống", "Đã thuê", "Bảo trì" });
            cboTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;

            btnUpdate.Click += btnUpdate_Click;
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

        private void LoadData(string ma)
        {
            using (SqlConnection c = new SqlConnection(connString))
            {
                c.Open();
                var cmd = new SqlCommand("SELECT * FROM Phong WHERE MaPhong = @ma", c);
                cmd.Parameters.AddWithValue("@ma", ma);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        txtMaPhong.Text = r["MaPhong"].ToString();
                        cboLoaiPhong.SelectedValue = r["MaLoaiPhong"];
                        cboTinhTrang.Text = r["TinhTrang"].ToString();
                        txtGhiChu.Text = r["GhiChu"]?.ToString() ?? "";
                        txtMaPhong.Enabled = true;
                    }
                }
            }
        }

        private bool MaDaTonTai(string ma, string ignore)
        {
            using (SqlConnection c = new SqlConnection(connString))
            {
                c.Open();
                var cmd = new SqlCommand("SELECT COUNT(*) FROM Phong WHERE MaPhong = @ma AND MaPhong <> @ignore", c);
                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.Parameters.AddWithValue("@ignore", ignore);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ma = txtMaPhong.Text.Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(ma))
            {
                MessageBox.Show("Mã phòng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(ma, @"^P\d{2}$"))
            {
                MessageBox.Show("Mã phòng phải có dạng P + 2 số (VD: P01, P12)!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhong.Focus();
                return;
            }
            if (ma != oldMaPhong && MaDaTonTai(ma, oldMaPhong))
            {
                MessageBox.Show("Mã phòng đã tồn tại!", "Lỗi trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection c = new SqlConnection(connString))
                {
                    c.Open();
                    // TẠO CÂU LỆNH ĐỘNG
                    string sql;
                    if (ma != oldMaPhong)
                    {
                        // NẾU SỬA MÃ → CẬP NHẬT CẢ MAPHONG
                        sql = @"
                    UPDATE Phong 
                    SET MaPhong = @maMoi, 
                        MaLoaiPhong = @loai, 
                        TinhTrang = @tt, 
                        GhiChu = @gc 
                    WHERE MaPhong = @old";
                    }
                    else
                    {
                        // NẾU KHÔNG SỬA MÃ → CHỈ CẬP NHẬT CÁC TRƯỜNG KHÁC
                        sql = @"
                    UPDATE Phong 
                    SET MaLoaiPhong = @loai, 
                        TinhTrang = @tt, 
                        GhiChu = @gc 
                    WHERE MaPhong = @old";
                    }

                    var cmd = new SqlCommand(sql, c);
                    cmd.Parameters.AddWithValue("@maMoi", ma);
                    cmd.Parameters.AddWithValue("@loai", cboLoaiPhong.SelectedValue);
                    cmd.Parameters.AddWithValue("@tt", cboTinhTrang.Text.Trim());
                    cmd.Parameters.AddWithValue("@gc", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text);
                    cmd.Parameters.AddWithValue("@old", oldMaPhong);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
