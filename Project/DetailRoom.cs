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
            List<LoaiPhong> lp = roomService.GetAllRoomTypes();
            List<string> mlp = new List<string>();
            foreach (LoaiPhong l in lp)
            {
                mlp.Add(l.MaLoaiPhong.ToString());
            }
            cbTypeRoom.DataSource = mlp;
            cbStatus.DataSource = roomService.GetAllStatus();
        }
        private Phong cur=null;
        public DetailRoom(Phong x)
        {
            InitializeComponent();
            cur = x;
            tbMaPhong.Text = x.MaPhong;
            tbMaPhong.ReadOnly = true;
            cbStatus.Items.Clear();
            cbTypeRoom.Items.Clear();
            RoomService roomService = new RoomService();
            cbTypeRoom.DataSource= roomService
                .GetAllRoomTypes()
                .Select(a => a.MaLoaiPhong.ToString())
                .ToList(); 
            cbStatus.DataSource = roomService.GetAllStatus();
            string loaiphong = x.MaLoaiPhong;
            string tinhtrang = x.TinhTrang;
            cbTypeRoom.SelectedItem = loaiphong;
            cbStatus.SelectedItem = tinhtrang;
        }
        private void DetailRoom_Load(object sender, EventArgs e)
        {

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            RoomService roomService = new RoomService();

            if (cur != null)
            {
                Phong phong = new Phong();
                phong.MaPhong = tbMaPhong.Text;
                phong.MaLoaiPhong = cbTypeRoom.SelectedItem.ToString();
                phong.TinhTrang = cbStatus.SelectedItem.ToString();
                phong.GhiChu = tbNote.Text;
                roomService.UpdateRoom(phong);
                this.Close();
            }
            else
            {
                Phong phong = new Phong();
                phong.MaPhong = tbMaPhong.Text;
                phong.MaLoaiPhong = cbTypeRoom.SelectedItem.ToString();
                phong.TinhTrang = cbStatus.SelectedItem.ToString();
                phong.GhiChu = tbNote.Text;
                roomService.AddRoom(phong);
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
