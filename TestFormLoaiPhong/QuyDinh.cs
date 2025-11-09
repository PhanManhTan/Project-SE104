using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFormLoaiPhong
{
    public partial class QuyDinh : Form
    {
        public QuyDinh()
        {
            InitializeComponent();
        }
        string connString;
        public QuyDinh(string db)
        {
            InitializeComponent();
            connString = db;
        }
        string connectionString;
        private void QuyDinh_Load(object sender, EventArgs e)
        {
            // Đọc chuỗi kết nối từ file database.txt
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đọc file database.txt\n" + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT MaQuyDinh, SoKhachToiDa, TyLePhuThu, NgayApDung FROM QuyDinh";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView_quydinh.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }




    }
}
