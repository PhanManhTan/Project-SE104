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
    public partial class DetailRoom : Form
    {
        public DetailRoom()
        {
            InitializeComponent();
            tbMaPhong.Text = "";
            //tbMaPhong.ReadOnly = true;
            cbStatus.Items.Clear();
            cbTypeRoom.Items.Clear();
            RoomService roomService = new RoomService();
            List<LoaiPhong> lp = roomService.GetAllTypeofRoom();
            List<string> mlp = new List<string>();
            foreach (LoaiPhong l in lp)
            {
                mlp.Add(l.MaLoaiPhong.ToString());
            }
            cbTypeRoom.DataSource = mlp;
            cbStatus.DataSource = roomService.GetAllStatus();
        }
        Phong cur;
        public DetailRoom(Phong x)
        {
            InitializeComponent();
            cur = x;
            tbMaPhong.Text = x.MaPhong;
            tbMaPhong.ReadOnly = true;
            cbStatus.Items.Clear();
            cbTypeRoom.Items.Clear();
            RoomService roomService = new RoomService();
            List<LoaiPhong> lp = roomService.GetAllTypeofRoom();
            List<string> mlp = new List<string>();
            foreach (LoaiPhong l in lp)
            {
                mlp.Add(l.MaLoaiPhong.ToString().Trim());  
            }
            cbTypeRoom.DataSource = mlp;
            cbStatus.DataSource = roomService.GetAllStatus();
            string loaiphong = x.LoaiPhong.MaLoaiPhong.Trim();
            string tinhtrang = x.TinhTrang.Trim();
            cbTypeRoom.SelectedItem = loaiphong;
            cbStatus.SelectedItem = tinhtrang;
        }
        private void DetailRoom_Load(object sender, EventArgs e)
        {

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            RoomService roomService = new RoomService();
            cur.MaLoaiPhong = cbTypeRoom.SelectedItem.ToString()+"    ";
            string temp = cur.LoaiPhong.MaLoaiPhong;
            cur.TinhTrang = cbStatus.SelectedItem.ToString();
            cur.GhiChu = tbNote.Text;
            roomService.UpdateRoom(cur);
            this.Close();
        }
    }
}
