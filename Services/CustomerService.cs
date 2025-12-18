using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Services
{
    public class CustomerService
    {
        private readonly DBDataContext db = new DBDataContext();

        // ==================== QUẢN LÝ LOẠI KHÁCH ====================
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
                    return false; // Mã loại khách đã tồn tại
                }

                db.LoaiKhaches.InsertOnSubmit(lk);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCustomerType(LoaiKhach updatedLk, string oldID)
        {
            try
            {
                string newID = updatedLk.MaLoaiKhach.Trim();
                oldID = oldID.Trim();

                // Trường hợp không thay đổi mã
                if (string.Equals(oldID, newID, StringComparison.OrdinalIgnoreCase))
                {
                    var existing = db.LoaiKhaches.FirstOrDefault(x => x.MaLoaiKhach == oldID);
                    if (existing != null)
                    {
                        existing.TenLoaiKhach = updatedLk.TenLoaiKhach;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }

                // Trường hợp thay đổi mã
                if (db.LoaiKhaches.Any(x => x.MaLoaiKhach == newID))
                {
                    return false; // Mã mới đã tồn tại
                }

                if (db.KhachHangs.Any(kh => kh.MaLoaiKhach == oldID))
                {
                    return false; // Loại khách đang được sử dụng
                }

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
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCustomerType(string maLoai)
        {
            try
            {
                var lk = db.LoaiKhaches.FirstOrDefault(x => x.MaLoaiKhach == maLoai);
                if (lk == null)
                    return false;

                // Kiểm tra ràng buộc (dù có FK hay không)
                if (db.KhachHangs.Any(kh => kh.MaLoaiKhach == maLoai))
                    return false;

                db.LoaiKhaches.DeleteOnSubmit(lk);
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Ghi log hoặc trả về thông tin lỗi (tùy dự án)
                // Ví dụ: System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        // ==================== QUẢN LÝ KHÁCH HÀNG ====================
        public List<CustomerViewModel> GetAllCustomers()
        {
            var query = from kh in db.KhachHangs
                        join lk in db.LoaiKhaches on kh.MaLoaiKhach equals lk.MaLoaiKhach into gj
                        from loaiKhach in gj.DefaultIfEmpty()
                        select new CustomerViewModel
                        {
                            MaKhach = kh.MaKhach,
                            HoTen = kh.HoTen,
                            CMND = kh.CMND,
                            DiaChi = kh.DiaChi,
                            MaLoaiKhach = kh.MaLoaiKhach,
                            TenLoaiKhach = loaiKhach != null ? loaiKhach.TenLoaiKhach : "Không xác định"
                        };
            return query.ToList();
        }

        public List<KhachHang> GetAllCustomersFromEntity()
        {
            return db.KhachHangs.ToList();
        }


        public bool AddCustomer(KhachHang kh)
        {
            try
            {
                if (db.KhachHangs.Any(k => k.MaKhach == kh.MaKhach))
                {
                    return false; // Mã khách đã tồn tại
                }

                if (db.KhachHangs.Any(k => k.CMND == kh.CMND))
                {
                    return false; // CMND đã tồn tại
                }

                db.KhachHangs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCustomer(KhachHang kh)
        {
            try
            {
                var existing = db.KhachHangs.FirstOrDefault(k => k.MaKhach == kh.MaKhach);
                if (existing == null) return false;

                if (db.KhachHangs.Any(k => k.CMND == kh.CMND && k.MaKhach != kh.MaKhach))
                {
                    return false; // CMND trùng với khách khác
                }

                existing.HoTen = kh.HoTen;
                existing.CMND = kh.CMND;
                existing.DiaChi = kh.DiaChi;
                existing.MaLoaiKhach = kh.MaLoaiKhach;
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCustomer(string maKhach)
        {
            try
            {
                bool coTrongPhieuThue = db.ChiTietPhieuThues.Any(ct => ct.MaKhach == maKhach);
                if (coTrongPhieuThue)
                {
                    return false; // Khách hàng đang có trong phiếu thuê
                }

                var khachHang = db.KhachHangs.FirstOrDefault(k => k.MaKhach == maKhach);
                if (khachHang == null)
                {
                    return false;
                }

                db.KhachHangs.DeleteOnSubmit(khachHang);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public KhachHang GetCustomerById(string maKhach)
        {
            return db.KhachHangs.FirstOrDefault(k => k.MaKhach == maKhach);
        }
    }
}