using Data;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class DetailCustomer : Form
    {
        public DetailCustomer()
        {
            InitializeComponent();
        }
        private KhachHang cur =null;
        public DetailCustomer(KhachHang kh)
        {
            InitializeComponent();
            cur = kh;   
        }
        private void DetailCustomer_Load(object sender, EventArgs e)
        {
            CustomerService customerService = new CustomerService();    
            cbLoaiKhach.DataSource = customerService
                   .GetAllCustomerTypes()
                   .Select(x => x.MaLoaiKhach.ToString().Trim())
                   .ToList();
            cbLoaiKhach.SelectedIndex = -1;
            if (cur != null) {
                tbHoTen.Text = cur.HoTen;
                tbMaKhachHang.Text = cur.MaKhach;
                tbCMND.Text = cur.CMND; 
                string loaikhach = cur.MaLoaiKhach.ToString().Trim();
                cbLoaiKhach.SelectedItem = loaikhach;
                tbDiaChi.Text = cur.DiaChi; 
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            KhachHang kh = new  KhachHang();
            CustomerService customerService = new CustomerService();
            if (cur != null) {
                kh.HoTen = tbHoTen.Text;
                kh.MaKhach = tbMaKhachHang.Text;
                kh.CMND = tbCMND.Text;  
                kh.MaLoaiKhach = cbLoaiKhach.SelectedValue.ToString();
                kh.DiaChi = tbDiaChi.Text;    
                customerService.UpdateCustomer(kh);
                this.Close();
            }
            else
            {
                kh.HoTen = tbHoTen.Text;
                kh.MaKhach = tbMaKhachHang.Text;
                kh.CMND = tbCMND.Text;
                kh.MaLoaiKhach = cbLoaiKhach.SelectedValue.ToString();
                kh.DiaChi = tbDiaChi.Text;
                customerService.AddCustomer(kh);
                this.Close();
            } 
        }
    }
}
