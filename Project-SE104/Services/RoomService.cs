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

        public List<LoaiPhong> GetAllTypeofRoom() => qlks.LoaiPhongs.ToList();

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
                // tìm phòng theo mã phòng
                var phong = qlks.Phongs.FirstOrDefault(p => p.MaPhong == updatedRoom.MaPhong);

                if (phong == null)
                    return false;   // không tìm thấy phòng → không update

                // cập nhật thông tin
                phong.MaLoaiPhong = updatedRoom.MaLoaiPhong;
                phong.TinhTrang = updatedRoom.TinhTrang;
                phong.GhiChu = updatedRoom.GhiChu;

                // lưu database
                qlks.SubmitChanges();
                MessageBox.Show("Sửa phòng thành công!");
                return true;
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
                MessageBox.Show("Xóa phòng thành công!");
                qlks.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa phòng: " + ex.Message);
                return false;
            }
        }
        public List<string> GetAllStatus()
        {
            return new List<string>
            {
                "Trong",
                "Da thue",
                "Dang don"
            };
        }

    }
}