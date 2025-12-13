using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Services
{
    public class RoomService
    {
        private readonly DBDataContext qlks = new DBDataContext();

        // ==================== QUẢN LÝ PHÒNG ====================
        public List<Phong> GetAllRooms() => qlks.Phongs.ToList();

        public List<Phong> GetAvailableRooms() =>
            qlks.Phongs.Where(p => p.TinhTrang == "Trống").ToList();

        public List<Phong> GetRoomsByType(string maLoaiPhong) =>
            qlks.Phongs.Where(p => p.MaLoaiPhong == maLoaiPhong).ToList();

        public bool UpdateRoomStatus(string maPhong, string tinhTrang)
        {
            try
            {
                var phong = qlks.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
                if (phong == null) return false;

                if (tinhTrang != "Trống" && tinhTrang != "Đã thuê" && tinhTrang != "Đang dọn")
                    return false;

                phong.TinhTrang = tinhTrang;
                qlks.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddRoom(Phong newRoom)
        {
            try
            {
                if (qlks.Phongs.Any(p => p.MaPhong == newRoom.MaPhong))
                    return false; // Mã phòng đã tồn tại

                newRoom.TinhTrang = "Trống"; // Mặc định khi thêm mới
                qlks.Phongs.InsertOnSubmit(newRoom);
                qlks.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateRoom(Phong updatedRoom)
        {
            try
            {
                var phong = qlks.Phongs.FirstOrDefault(p => p.MaPhong == updatedRoom.MaPhong);
                if (phong == null) return false;

                phong.MaLoaiPhong = updatedRoom.MaLoaiPhong;
                phong.GhiChu = updatedRoom.GhiChu;
                phong.TinhTrang = updatedRoom.TinhTrang;
                qlks.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteRoom(string maPhong)
        {
            try
            {
                var phong = qlks.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
                if (phong == null) return false;

                // Không cho xóa nếu phòng đang có phiếu thuê
                if (qlks.PhieuThues.Any(pt => pt.MaPhong == maPhong))
                    return false;

                qlks.Phongs.DeleteOnSubmit(phong);
                qlks.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ==================== QUẢN LÝ LOẠI PHÒNG ====================
        public List<LoaiPhong> GetAllRoomTypes()
        {
            return qlks.LoaiPhongs.OrderBy(lp => lp.MaLoaiPhong).ToList();
        }

        public bool AddRoomType(LoaiPhong newType)
        {
            try
            {
                if (qlks.LoaiPhongs.Any(lp => lp.MaLoaiPhong == newType.MaLoaiPhong))
                    return false; // Mã loại đã tồn tại

                qlks.LoaiPhongs.InsertOnSubmit(newType);
                qlks.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateRoomType(LoaiPhong updatedType, string originalMaLoai)
        {
            try
            {
                string oldID = originalMaLoai.Trim();
                string newID = updatedType.MaLoaiPhong.Trim();
                bool isIdUnchanged = string.Equals(oldID, newID, StringComparison.OrdinalIgnoreCase);

                if (isIdUnchanged)
                {
                    var lp = qlks.LoaiPhongs.FirstOrDefault(p => p.MaLoaiPhong == oldID);
                    if (lp == null) return false;

                    lp.TenLoaiPhong = updatedType.TenLoaiPhong;
                    lp.DonGia = updatedType.DonGia;
                    qlks.SubmitChanges();
                    return true;
                }
                else
                {
                    // Kiểm tra mã mới đã tồn tại chưa
                    if (qlks.LoaiPhongs.Any(p => p.MaLoaiPhong == newID))
                        return false;

                    // Không cho đổi mã nếu loại phòng đang được sử dụng
                    if (qlks.Phongs.Any(p => p.MaLoaiPhong == oldID))
                        return false;

                    var oldLp = qlks.LoaiPhongs.FirstOrDefault(p => p.MaLoaiPhong == oldID);
                    if (oldLp == null) return false;

                    qlks.LoaiPhongs.DeleteOnSubmit(oldLp);
                    qlks.LoaiPhongs.InsertOnSubmit(updatedType);
                    qlks.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteRoomType(string maLoaiPhong)
        {
            try
            {
                var loaiPhong = qlks.LoaiPhongs.FirstOrDefault(lp => lp.MaLoaiPhong == maLoaiPhong);
                if (loaiPhong == null) return false;

                // Không cho xóa nếu có phòng đang dùng loại này
                if (qlks.Phongs.Any(p => p.MaLoaiPhong == maLoaiPhong))
                    return false;

                qlks.LoaiPhongs.DeleteOnSubmit(loaiPhong);
                qlks.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public LoaiPhong GetRoomTypeById(string maLoaiPhong)
        {
            return qlks.LoaiPhongs.FirstOrDefault(lp => lp.MaLoaiPhong == maLoaiPhong);
        }

        public bool IsRoomTypeInUse(string maLoaiPhong)
        {
            return qlks.Phongs.Any(p => p.MaLoaiPhong == maLoaiPhong);
        }

        // ==================== VIEW MODEL CHO GRID ====================
        public List<RoomViewModel> GetAllRoomsView()
        {
            var query = from p in qlks.Phongs
                        join lp in qlks.LoaiPhongs on p.MaLoaiPhong equals lp.MaLoaiPhong into gj
                        from loaiPhong in gj.DefaultIfEmpty()
                        select new RoomViewModel
                        {
                            MaPhong = p.MaPhong,
                            MaLoaiPhong = p.MaLoaiPhong,
                            TenLoaiPhong = loaiPhong != null ? loaiPhong.TenLoaiPhong : "Không xác định",
                            TinhTrang = p.TinhTrang,
                            GhiChu = p.GhiChu ?? "",
                            DonGia = loaiPhong != null ? loaiPhong.DonGia : 0
                        };
            return query.ToList();
        }

        // ==================== DANH SÁCH TÌNH TRẠNG ====================
        public List<string> GetAllStatus()
        {
            return new List<string> { "Trống", "Đã thuê", "Đang dọn" };
        }
    }
}