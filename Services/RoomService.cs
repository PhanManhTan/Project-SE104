// Services/RoomService.cs - ĐÃ BỔ SUNG ĐẦY ĐỦ QUẢN LÝ LOẠI PHÒNG
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

        // ==================== QUẢN LÝ PHÒNG ====================

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
                    MessageBox.Show("Mã phòng đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                newRoom.TinhTrang = "Trong";
                qlks.Phongs.InsertOnSubmit(newRoom);
                qlks.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm phòng:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lỗi sửa phòng:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Không thể xóa phòng đang có khách thuê!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                qlks.Phongs.DeleteOnSubmit(phong);
                qlks.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa phòng:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ==================== QUẢN LÝ LOẠI PHÒNG (MỚI THÊM) ====================

        /// <summary>
        /// Lấy danh sách tất cả loại phòng
        /// </summary>
        public List<LoaiPhong> GetAllRoomTypes()
        {
            return qlks.LoaiPhongs.OrderBy(lp => lp.MaLoaiPhong).ToList();
        }

        /// <summary>
        /// Thêm loại phòng mới
        /// </summary>
        public bool AddRoomType(LoaiPhong newType)
        {
            try
            {
                if (qlks.LoaiPhongs.Any(lp => lp.MaLoaiPhong == newType.MaLoaiPhong))
                {
                    MessageBox.Show("Mã loại phòng đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                qlks.LoaiPhongs.InsertOnSubmit(newType);
                qlks.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm loại phòng:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Cập nhật loại phòng
        /// </summary>

        public bool UpdateRoomType(LoaiPhong updatedType, string originalMaLoai)
        {
            try
            {
                // CHUẨN HÓA DỮ LIỆU ĐẦU VÀO:
                // Cắt sạch khoảng trắng 2 đầu để so sánh chính xác
                string oldID = originalMaLoai.Trim();
                string newID = updatedType.MaLoaiPhong.Trim();

                // 1. So sánh xem Mã có thay đổi không?
                // Dùng StringComparison.OrdinalIgnoreCase để không phân biệt hoa/thường (ví dụ "vip" == "VIP")
                bool isIdUnchanged = string.Equals(oldID, newID, StringComparison.OrdinalIgnoreCase);

                // TRƯỜNG HỢP 1: KHÔNG SỬA MÃ (Chỉ sửa Tên/Giá)
                if (isIdUnchanged)
                {
                    // Tìm theo mã cũ (oldID)
                    var lp = qlks.LoaiPhongs.FirstOrDefault(p => p.MaLoaiPhong == oldID);
                    if (lp != null)
                    {
                        lp.TenLoaiPhong = updatedType.TenLoaiPhong;
                        lp.DonGia = updatedType.DonGia;

                        // Quan trọng: Đảm bảo DataContext nhận biết thay đổi
                        qlks.SubmitChanges();
                        return true;
                    }
                    return false;
                }

                // TRƯỜNG HỢP 2: CÓ SỬA MÃ (Mã mới khác Mã cũ)
                else
                {
                    // A. Kiểm tra Mã MỚI đã tồn tại chưa?
                    if (qlks.LoaiPhongs.Any(p => p.MaLoaiPhong == newID))
                    {
                        MessageBox.Show($"Mã loại phòng '{newID}' đã tồn tại trong hệ thống!", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // B. Kiểm tra Mã CŨ có đang bị ràng buộc (đang dùng) không?
                    if (qlks.Phongs.Any(p => p.MaLoaiPhong == oldID))
                    {
                        MessageBox.Show($"Không thể đổi Mã vì loại phòng '{oldID}' đang được sử dụng bởi các phòng!", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // C. Thực hiện đổi mã: Xóa cũ -> Thêm mới
                    var oldLp = qlks.LoaiPhongs.FirstOrDefault(p => p.MaLoaiPhong == oldID);
                    if (oldLp != null)
                    {
                        // Copy dữ liệu cũ sang đối tượng mới (để tránh mất dữ liệu nếu có cột khác)
                        // Ở đây ta dùng updatedType đã có đủ thông tin

                        qlks.LoaiPhongs.DeleteOnSubmit(oldLp);    // Xóa cái cũ
                        qlks.LoaiPhongs.InsertOnSubmit(updatedType); // Thêm cái mới

                        qlks.SubmitChanges(); // Lưu xuống DB
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Xóa loại phòng (chỉ xóa được nếu không có phòng nào dùng loại này)
        /// </summary>
        public bool DeleteRoomType(string maLoaiPhong)
        {
            try
            {
                var loaiPhong = qlks.LoaiPhongs.FirstOrDefault(lp => lp.MaLoaiPhong == maLoaiPhong);
                if (loaiPhong == null) return false;

                // Kiểm tra xem có phòng nào đang dùng loại này không
                if (qlks.Phongs.Any(p => p.MaLoaiPhong == maLoaiPhong))
                {
                    MessageBox.Show("Không thể xóa loại phòng đang được sử dụng bởi các phòng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                qlks.LoaiPhongs.DeleteOnSubmit(loaiPhong);
                qlks.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa loại phòng:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Tìm loại phòng theo mã
        /// </summary>
        public LoaiPhong GetRoomTypeById(string maLoaiPhong)
        {
            return qlks.LoaiPhongs.FirstOrDefault(lp => lp.MaLoaiPhong == maLoaiPhong);
        }
        public bool IsRoomTypeInUse(string maLoaiPhong)
        {
            // Nếu tìm thấy bất kỳ phòng nào có MaLoaiPhong này -> trả về true (Đang dùng)
            return qlks.Phongs.Any(p => p.MaLoaiPhong == maLoaiPhong);
        }




        public List<string> GetAllStatus()
        {
            return qlks.Phongs
          .Select(p => p.TinhTrang)
          .Distinct()
          .ToList();
        }
    }
}