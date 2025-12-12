// Services/CustomerService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Services
{
    public class CustomerService
    {
        private readonly DBDataContext db = new DBDataContext();

        public List<LoaiKhach> GetAllCustomerTypes() => db.LoaiKhaches.ToList();

        public List<KhachHang> GetAllCustomers() => db.KhachHangs.ToList();

        public List<KhachHang> SearchCustomer(string keyword)
        {
            keyword = keyword?.Trim().ToLower() ?? "";
            return db.KhachHangs
                .Where(k => k.HoTen.Contains(keyword) ||
                           k.CMND.Contains(keyword) ||
                           k.MaKhach.Contains(keyword))
                .ToList();
        }

        public bool AddCustomer(KhachHang kh)
        {
            try
            {
                if (db.KhachHangs.Any(k => k.MaKhach == kh.MaKhach))
                    return false;

                db.KhachHangs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool UpdateCustomer(KhachHang kh)
        {
            var existing = db.KhachHangs.FirstOrDefault(k => k.MaKhach == kh.MaKhach);
            if (existing == null) return false;

            existing.HoTen = kh.HoTen;
            existing.CMND = kh.CMND;
            existing.DiaChi = kh.DiaChi;
            existing.MaLoaiKhach = kh.MaLoaiKhach;
            db.SubmitChanges();
            return true;
        }

        public bool DeleteCustomer(string maKhach)
        {
            var kh = db.KhachHangs.FirstOrDefault(k => k.MaKhach == maKhach);
            if (kh == null) return false;
            if (db.ChiTietPhieuThues.Any(pt => pt.MaKhach == maKhach)) return false;

            db.KhachHangs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return true;
        }
    }
}