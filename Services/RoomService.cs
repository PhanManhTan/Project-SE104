using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Services
{
    public class RoomService
    {
        private readonly DBDataContext qlks = new DBDataContext();

        public List<Phong> GetAllRooms()
        {
            return qlks.Phongs.ToList();
        }

        public bool AddRoom(Phong newRoom)
        {
            try
            {
                if (qlks.Phongs.Any(p => p.MaPhong == newRoom.MaPhong))
                    return false;

                newRoom.TinhTrang = "Trống";
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

        public List<LoaiPhong> GetAllRoomTypes()
        {
            return qlks.LoaiPhongs.OrderBy(lp => lp.MaLoaiPhong).ToList();
        }

        public bool AddRoomType(LoaiPhong newType)
        {
            try
            {
                if (qlks.LoaiPhongs.Any(lp => lp.MaLoaiPhong == newType.MaLoaiPhong))
                    return false;

                qlks.LoaiPhongs.InsertOnSubmit(newType);
                qlks.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateRoomType(LoaiPhong updatedType)
        {
            try
            {
                var lp = qlks.LoaiPhongs.FirstOrDefault(p => p.MaLoaiPhong == updatedType.MaLoaiPhong);
                if (lp == null) return false;

                lp.TenLoaiPhong = updatedType.TenLoaiPhong;
                lp.DonGia = updatedType.DonGia;
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
                    return UpdateRoomType(updatedType); 
                }
                else
                {
                    if (qlks.LoaiPhongs.Any(p => p.MaLoaiPhong == newID))
                        return false;
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
                            TinhTrang = p.TinhTrang ?? "Trống",
                            GhiChu = p.GhiChu ?? "",
                            DonGia = loaiPhong != null ? loaiPhong.DonGia : 0
                        };
            return query.ToList();
        }

        public List<string> GetAllStatus()
        {
            return new List<string> { "Trống", "Đã thuê"};
        }




        public string GenerateNewMaLoaiPhong()
        {
            try
            {
                var last = GetAllRoomTypes()
                    .Where(lp => !string.IsNullOrEmpty(lp.MaLoaiPhong) &&
                                lp.MaLoaiPhong.StartsWith("LP", StringComparison.OrdinalIgnoreCase))
                    .Select(lp => lp.MaLoaiPhong.ToUpper())
                    .OrderByDescending(m => m)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(last))
                    return "LP001";

                string numberPart = last.Substring(2);
                if (int.TryParse(numberPart, out int num))
                {
                    return "LP" + (num + 1).ToString("D3");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return "LP001";
        }


        public string GenerateNewMaPhong()
        {
            try
            {
                var lastRoom = GetAllRooms()
                    .Where(p => !string.IsNullOrEmpty(p.MaPhong) &&
                                p.MaPhong.StartsWith("P", StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(p => p.MaPhong, StringComparer.OrdinalIgnoreCase)
                    .FirstOrDefault();

                if (lastRoom == null || string.IsNullOrEmpty(lastRoom.MaPhong))
                    return "P001";

                string lastCode = lastRoom.MaPhong.Trim().ToUpper();
                if (lastCode.StartsWith("P") && int.TryParse(lastCode.Substring(1), out int num))
                {
                    return "P" + (num + 1).ToString("D3");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // Nếu lỗi, trả về mặc định
            }

            return "P001";
        }
    }
}