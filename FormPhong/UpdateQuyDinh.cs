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
    public partial class UpdateQuyDinh : Form
    {
        private string connString;
        public UpdateQuyDinh(string connectionString)
        {
            connString = connectionString;
            InitializeComponent();
            this.Load += Form_Load;
        }
        private void Form_Load(object sender, EventArgs e)
        {
            LoadHienTai();
            SetMinDate();
        }

        

        private void LoadHienTai()
        {
            try
            {
                using (SqlConnection c = new SqlConnection(connString))
                {
                    c.Open();
                    string query = "SELECT TOP 1 SoKhachToiDa, TyLePhuThu, NgayApDung FROM QuyDinh ORDER BY MaQuyDinh DESC";
                    using (SqlCommand cmd = new SqlCommand(query, c))
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            nudSoKhach.Value = r.GetInt32(0);
                            nudTyLe.Value = r.GetInt32(1);
                            dtpNgay.Value = r.GetDateTime(2).AddDays(1); ;
                        }
                    }
                }
            }
            catch { /* Không lỗi nếu chưa có dữ liệu */ }
        }

        private void SetMinDate()
        {
            try
            {
                using (SqlConnection c = new SqlConnection(connString))
                {
                    c.Open();
                    string query = "SELECT TOP 1 NgayApDung FROM QuyDinh ORDER BY MaQuyDinh DESC";
                    using (SqlCommand cmd = new SqlCommand(query, c))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            DateTime maxDate = (DateTime)result;
                            dtpNgay.MinDate = maxDate; 
                            dtpNgay.Value = maxDate;   
                        }
                        else
                        {
                            dtpNgay.MinDate = DateTime.Today;
                            dtpNgay.Value = DateTime.Today;
                        }
                    }
                }
            }
            catch { }
        }
        private string TaoMaQuyDinhMoi()
        {
            try
            {
                using (SqlConnection c = new SqlConnection(connString))
                {
                    c.Open();
                    string query = "SELECT MAX(MaQuyDinh) FROM QuyDinh";
                    using (SqlCommand cmd = new SqlCommand(query, c))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                            return "QD01";

                        string maxMa = result.ToString();
                        int so = int.Parse(maxMa.Substring(2));
                        return "QD" + (so + 1).ToString("00");
                    }
                }
            }
            catch
            {
                return "QD01";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dtpNgay.Value < dtpNgay.MinDate)
            {
                MessageBox.Show("Ngày áp dụng phải lớn hơn hoặc bằng ngày hiện hành!",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string maMoi = TaoMaQuyDinhMoi();

                using (SqlConnection c = new SqlConnection(connString))
                {
                    c.Open();
                    string query = @"
                        INSERT INTO QuyDinh (MaQuyDinh, SoKhachToiDa, TyLePhuThu, NgayApDung)
                        VALUES (@ma, @sk, @tl, @ngay)";

                    using (SqlCommand cmd = new SqlCommand(query, c))
                    {
                        cmd.Parameters.AddWithValue("@ma", maMoi);
                        cmd.Parameters.AddWithValue("@sk", (int)nudSoKhach.Value);
                        cmd.Parameters.AddWithValue("@tl", (int)nudTyLe.Value);
                        cmd.Parameters.AddWithValue("@ngay", dtpNgay.Value.Date);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Cập nhật thành công!\nMã mới: {maMoi}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
