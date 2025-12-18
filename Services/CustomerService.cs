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
                    return false; 
                }

                db.LoaiKhaches.InsertOnSubmit(lk);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool UpdateCustomerType(LoaiKhach updatedLk, string oldID)
        {
            try
            {
                string newID = updatedLk.MaLoaiKhach.Trim();
                oldID = oldID.Trim();

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

                if (db.LoaiKhaches.Any(x => x.MaLoaiKhach == newID))
                {
                    return false; 
                }

                if (db.KhachHangs.Any(kh => kh.MaLoaiKhach == oldID))
                {
                    return false; 
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                if (db.KhachHangs.Any(kh => kh.MaLoaiKhach == maLoai))
                    return false;

                db.LoaiKhaches.DeleteOnSubmit(lk);
                db.SubmitChanges();

                return true;
            }
            catch 
            {
                return false;
            }
        }

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
                    return false; 
                }

                if (db.KhachHangs.Any(k => k.CMND == kh.CMND))
                {
                    return false; 
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
                    return false; 
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

        public string GenerateNewMaKhach()
        {
                var lastCode = db.KhachHangs
                    .Where(k => k.MaKhach != null && k.MaKhach.StartsWith("KH"))
                    .Select(k => k.MaKhach)
                    .ToList() 
                    .Where(code => code.Length > 2 && int.TryParse(code.Substring(2), out _))
                    .OrderByDescending(code => code)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(lastCode))
                {
                    return "KH001";
                }

                string numberPart = lastCode.Substring(2);
                if (int.TryParse(numberPart, out int number))
                {
                    return "KH" + (number + 1).ToString("D3"); 
                }
            return "KH001";
        }

        public string GenerateNewMaLoaiKhach()
        {
                var last = GetAllCustomerTypes()
                    .Where(lk => !string.IsNullOrEmpty(lk.MaLoaiKhach) && lk.MaLoaiKhach.StartsWith("LK"))
                    .Select(lk => lk.MaLoaiKhach)
                    .OrderByDescending(m => m)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(last))
                    return "LK001";

                string numberPart = last.Substring(2);
                if (int.TryParse(numberPart, out int num))
                {
                    return "LK" + (num + 1).ToString("D3");
                }
            return "LK001";
        }

        public bool DeleteCustomer(string maKhach)
        {
            try
            {
                bool coTrongPhieuThue = db.ChiTietPhieuThues.Any(ct => ct.MaKhach == maKhach);
                if (coTrongPhieuThue)
                {
                    return false;
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