// Services/RoomService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Data;

namespace Services
{
    public class RoomService
    {
        private readonly DBDataContext qlks = new DBDataContext();

        public List<Phong> GetAllRooms() => qlks.Phongs.ToList();

        public List<Phong> GetAvailableRooms() =>
            qlks.Phongs.Where(p => p.TinhTrang == "Trong").ToList();

        public List<Phong> GetRoomsByType(string maLoaiPhong) =>
            qlks.Phongs.Where(p => p.MaLoaiPhong == maLoaiPhong).ToList();

        public bool UpdateRoomStatus(string maPhong, string tinhTrang)
        {
            var phong = qlks.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
            if (phong != null && (tinhTrang == "Trong" || tinhTrang == "Da thue" || tinhTrang == "Dang don"))
            {
                phong.TinhTrang = tinhTrang;
                qlks.SubmitChanges();
                return true;
            }
            return false;
        }   

        public bool AddRoom(Phong newRoom)
        {
            try
            {
                if (qlks.Phongs.Any(p => p.MaPhong == newRoom.MaPhong))
                {
                    MessageBox.Show("Mã phòng đã tồn tại!", "Lỗi");
                    return false;
                }
                newRoom.TinhTrang = "Trong";
                qlks.Phongs.InsertOnSubmit(newRoom);
                qlks.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm phòng: " + ex.Message);
                return false;
            }
        }

        public bool UpdateRoom(Phong updatedRoom)
        {
            try
            {
                var phong = qlks.Phongs.FirstOrDefault(p => p.MaPhong == updatedRoom.MaPhong);
                if (phong != null)
                {
                    phong.MaLoaiPhong = updatedRoom.MaLoaiPhong;
                    phong.GhiChu = updatedRoom.GhiChu;
                    phong.TinhTrang = updatedRoom.TinhTrang;
                    qlks.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa phòng: " + ex.Message);
                return false;
            }
        }

        public bool DeleteRoom(string maPhong)
        {
            try
            {
                var phong = qlks.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
                if (phong == null) return false;

                if (qlks.PhieuThues.Any(pt => pt.MaPhong == maPhong))
                {
                    MessageBox.Show("Không thể xóa phòng đang có khách thuê!");
                    return false;
                }

                qlks.Phongs.DeleteOnSubmit(phong);
                qlks.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa phòng: " + ex.Message);
                return false;
            }
        }
    }
}