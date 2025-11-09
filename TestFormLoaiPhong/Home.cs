using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFormLoaiPhong
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        public string connString = "Data Source=CCCUATAZ\\SQLEXPRESS01;Initial Catalog=QLKS99;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";


        private Form curentFormChild;
        private void OpenForm(Form form)
        {
            if(curentFormChild != null)
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

        private void button_qlloaiphong_Click(object sender, EventArgs e)
        {
            OpenForm(new Form1());
            label1.Text = "QUẢN LÝ PHÒNG";
        }

        private void button_thanhtoan_Click(object sender, EventArgs e)
        {
            OpenForm(new ThanhToan());
            label1.Text = "HÓA ĐƠN THANH TOÁN";

        }

        private void button_quydinh_Click(object sender, EventArgs e)
        {
            OpenForm(new QuyDinh(connString));
            label1.Text = "Quy Định";
        }
    }
}
