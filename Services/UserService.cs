using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Windows.Forms;


namespace Services
{
    public class UserService
    {
        private readonly DBDataContext qlks = new DBDataContext();

        public User_ Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = qlks.User_s.FirstOrDefault(u =>
                u.Username.Trim().ToLower() == username.Trim().ToLower());

            if (user != null && user.CheckPassword(password))
            {
                Global_.CurrentUser = user;
                return user;
            }

            return null; 
        }

        public bool Update(User_ user)
        {
            try
            {
                var existing = qlks.User_s.FirstOrDefault(u => u.Username == user.Username);
                if (existing == null) return false;

                existing.FullName = user.FullName;
                existing.Role_ = user.Role_;

                existing.Password_ = user.Password_;

                qlks.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User_ GetByUsername(string username)
        {
            return qlks.User_s.FirstOrDefault(u => u.Username == username);
        }


        public bool Create(User_ newUser)
        {
            if (qlks.User_s.Any(u => u.Username == newUser.Username))
                return false;

            newUser.CreatedDate = DateTime.Now;

            qlks.User_s.InsertOnSubmit(newUser);
            qlks.SubmitChanges();
            return true;
        }

        // 4. Lấy tất cả user
        public IQueryable<User_> GetAll() => qlks.User_s;  

        // 5. Xóa user
        public bool Delete(string username)
        {
            try
            {
                // Tìm user cần xóa
                var user = qlks.User_s.FirstOrDefault(u => u.Username == username);


                if (user == null) return false;

                // Thực hiện xóa
                qlks.User_s.DeleteOnSubmit(user);
                qlks.SubmitChanges(); // Lưu xuống DB

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        // 6. Kiểm tra quyền
        public bool IsAdmin() => Global_.CurrentUser?.IsAdmin ?? false;
        public bool IsNhanVien() => Global_.CurrentUser?.IsNhanVien ?? false;
    }
}