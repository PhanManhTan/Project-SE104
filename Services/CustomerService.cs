using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Data;

namespace Services
{
    public class CustomerService
    {
        private readonly DBDataContext db = new DBDataContext();

        // ==================== QUẢN LÝ LOẠI KHÁCH (Đã sửa theo DB mới) ====================

        public List<LoaiKhach> GetAllCustomerTypes()
        {
            return db.LoaiKhaches.OrderBy(lk => lk.MaLoaiKhach).ToList();
        }

        public LoaiKhach GetCustomerTypeById(string maLoai)
        {
            return db.LoaiKhaches.FirstOrDefault(lk => lk.MaLoaiKhach == maLoai);
        }

        public bool AddCustomerType(LoaiKhach lk)
        {
            try
            {
                if (db.LoaiKhaches.Any(x => x.MaLoaiKhach == lk.MaLoaiKhach))
                {
                    MessageBox.Show("Mã loại khách đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                db.LoaiKhaches.InsertOnSubmit(lk);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
                return false;
            }
        }

        public bool UpdateCustomerType(LoaiKhach updatedLk, string oldID)
        {
            try
            {
                string newID = updatedLk.MaLoaiKhach.Trim();
                oldID = oldID.Trim();

                // Trường hợp 1: Không sửa mã
                if (string.Equals(oldID, newID, StringComparison.OrdinalIgnoreCase))
                {
                    var existing = db.LoaiKhaches.FirstOrDefault(x => x.MaLoaiKhach == oldID);
                    if (existing != null)
                    {
                        existing.TenLoaiKhach = updatedLk.TenLoaiKhach;
                        // Không còn TrangThaiSuDung để update nữa
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                // Trường hợp 2: Có sửa mã
                else
                {
                    if (db.LoaiKhaches.Any(x => x.MaLoaiKhach == newID))
                    {
                        MessageBox.Show("Mã mới đã bị trùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (db.KhachHangs.Any(kh => kh.MaLoaiKhach == oldID))
                    {
                        MessageBox.Show("Không thể đổi Mã vì loại khách này đang được sử dụng!", "Ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }

                    // Xóa cũ -> Thêm mới
                    var oldRecord = db.LoaiKhaches.FirstOrDefault(x => x.MaLoaiKhach == oldID);
                    if (oldRecord != null)
                    {
                        db.LoaiKhaches.DeleteOnSubmit(oldRecord);
                        db.LoaiKhaches.InsertOnSubmit(updatedLk);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
                return false;
            }
        }

        public bool DeleteCustomerType(string maLoai)
        {
            try
            {
                if (db.KhachHangs.Any(kh => kh.MaLoaiKhach == maLoai))
                {
                    MessageBox.Show("Không thể xóa loại khách đang có người sử dụng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var lk = db.LoaiKhaches.FirstOrDefault(x => x.MaLoaiKhach == maLoai);
                if (lk != null)
                {
                    db.LoaiKhaches.DeleteOnSubmit(lk);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
                return false;
            }
        }

        // ==================== QUẢN LÝ KHÁCH HÀNG (Cập nhật theo DB mới) ====================
        // DB KhachHang: MaKhach, HoTen, CMND, DiaChi, MaLoaiKhach

        public List<KhachHang> GetAllCustomers() => db.KhachHangs.ToList();

        public List<KhachHang> SearchCustomer(string keyword)
        {
            keyword = keyword?.Trim().ToLower() ?? "";
            return db.KhachHangs
                .Where(k => k.HoTen.ToLower().Contains(keyword) ||
                           k.CMND.Contains(keyword) ||
                           k.MaKhach.ToLower().Contains(keyword))
                .ToList();
        }

        public bool AddCustomer(KhachHang kh)
        {
            try
            {
                if (db.KhachHangs.Any(k => k.MaKhach == kh.MaKhach))
                {
                    MessageBox.Show("Mã khách hàng đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (db.KhachHangs.Any(k => k.CMND == kh.CMND))
                {
                    MessageBox.Show("CMND đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                db.KhachHangs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); return false; }
        }

        public bool UpdateCustomer(KhachHang kh)
        {
            try
            {
                var existing = db.KhachHangs.FirstOrDefault(k => k.MaKhach == kh.MaKhach);
                if (existing == null) return false;

                // Check trùng CMND với người khác
                if (db.KhachHangs.Any(k => k.CMND == kh.CMND && k.MaKhach != kh.MaKhach))
                {
                    MessageBox.Show("CMND trùng với khách khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                existing.HoTen = kh.HoTen;
                existing.CMND = kh.CMND;
                existing.DiaChi = kh.DiaChi;
                existing.MaLoaiKhach = kh.MaLoaiKhach;

                db.SubmitChanges();
                return true;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); return false; }
        }

        public bool DeleteCustomer(string maKhach)
        {
            // Kiểm tra xem khách hàng này có xuất hiện trong bất kỳ phiếu thuê nào không
            bool coTrongPhieuThue = db.ChiTietPhieuThues.Any(ct => ct.MaKhach == maKhach);

            if (coTrongPhieuThue)
            {
                // Không cho xóa nếu khách hàng đang được ghi nhận trong phiếu thuê
                return false;
            }

            // Tìm khách hàng cần xóa
            var khachHang = db.KhachHangs.FirstOrDefault(k => k.MaKhach == maKhach);

            if (khachHang == null)
            {
                return false; // Không tìm thấy khách hàng
            }

            try
            {
                db.KhachHangs.DeleteOnSubmit(khachHang);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Có thể ghi log lỗi ở đây nếu cần
                // Ví dụ: Console.WriteLine(ex.Message);
                return false; // Xóa thất bại (có thể do ràng buộc khóa ngoại khác)
            }
        }

        // Helper để lấy chi tiết 1 khách
        public KhachHang GetCustomerById(string maKhach)
        {
            return db.KhachHangs.FirstOrDefault(k => k.MaKhach == maKhach);
        }

        /// <summary>
        /// Kiểm tra xem Mã loại khách này đã được dùng cho ai chưa?
        /// </summary>
        public bool IsCustomerTypeInUse(string maLoai)
        {
            // Kiểm tra trong bảng KhachHang xem có ai đang mang MaLoaiKhach này không
            return db.KhachHangs.Any(kh => kh.MaLoaiKhach == maLoai);
        }
    }
}