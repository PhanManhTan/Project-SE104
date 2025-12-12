using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Project
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (Global_.CurrentUser != null)
            {
                lbName.Text = Global_.CurrentUser.DisplayName;           // Tên đầy đủ
                lbRole.Text = Global_.CurrentUser.Role.ToUpper();        // ADMIN / NHÂN VIÊN
            }
            else
            {
                // Trường hợp mở MainForm trực tiếp (test)
                lbName.Text = "Chưa đăng nhập";
                lbRole.Text = "GUEST";
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Hỏi xác nhận
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Xóa thông tin người dùng
                Global_.CurrentUser = null;

                // ĐÓNG MainForm → tự động quay về LoginForm
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenForm(new RoomManeger()); 
        }


        private void OpenForm(Form form)
        {
            if (curentFormChild != null)
            {
                curentFormChild.Close();
            }

            curentFormChild = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel_body.Controls.Add(form);
            panel_body.Tag = form;
            form.BringToFront();
            form.Show();
        }



        private Form curentFormChild;

        private void button3_Click(object sender, EventArgs e)
        {
            OpenForm(new QuyDinhForm());
        }

        private void panel_body_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenForm(new RoomManeger());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenForm(new CustomerManager());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenForm(new ReportForm());
        }
    }
}
