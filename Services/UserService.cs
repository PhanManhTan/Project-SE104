using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;


namespace Services
{
    public class UserService
    {
        private readonly DBDataContext qlks = new DBDataContext();

        // 1. Đăng nhập
        public User_ Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            // BƯỚC 1: Lấy user chỉ bằng Username (LINQ to SQL hiểu)
            var user = qlks.User_s.FirstOrDefault(u =>
                u.Username.Trim().ToLower() == username.Trim().ToLower());

            // BƯỚC 2: So sánh mật khẩu BÊN NGOÀI (C# hiểu, không cần SQL hiểu)
            if (user != null && user.CheckPassword(password))
            {
                Global_.CurrentUser = user;
                return user;
            }

            return null; // sai tài khoản hoặc mật khẩu
        }

        public bool Update(User_ user)
        {
            try
            {
                var existing = qlks.User_s.FirstOrDefault(u => u.Username == user.Username);
                if (existing == null) return false;

                existing.FullName = user.FullName;
                existing.Role_ = user.Role_;

                // Chỉ cập nhật mật khẩu nếu người dùng nhập mới (logic này tùy bạn xử lý ở Form)
                // Ở đây mình gán thẳng, Form sẽ lo việc truyền password nào xuống
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


        // 2. Đổi mật khẩu
        //public bool ChangePassword(string username, string oldPass, string newPass)
        //{
        //    // SỬA: qlks.User_s
        //    var user = qlks.User_s.FirstOrDefault(u =>
        //        u.Username.Trim().Equals(username.Trim(), StringComparison.OrdinalIgnoreCase));

        //    if (user == null || !user.CheckPassword(oldPass))
        //        return false;

        //    user.Password_ = newPass;

        //    // SỬA: Bảng User_ của bạn KHÔNG có cột UpdatedDate → bỏ dòng này đi!
        //    // user.UpdatedDate = DateTime.Now;   ← XÓA DÒNG NÀY

        //    qlks.SubmitChanges();
        //    return true;
        //}

        // 3. Đăng ký user mới
        public bool Create(User_ newUser)
        {
            // SỬA: qlks.User_s + Any
            if (qlks.User_s.Any(u => u.Username == newUser.Username))
                return false;

            newUser.CreatedDate = DateTime.Now;
            // newUser.UpdatedDate = DateTime.Now;   ← Bỏ nếu không có cột

            // SỬA: InsertOnSubmit vào qlks.User_s
            qlks.User_s.InsertOnSubmit(newUser);
            qlks.SubmitChanges();
            return true;
        }

        // 4. Lấy tất cả user
        public IQueryable<User_> GetAll() => qlks.User_s;   // đúng rồi

        // 5. Xóa user
        public bool Delete(string username)
        {
            try
            {
                // Tìm user cần xóa
                var user = qlks.User_s.FirstOrDefault(u => u.Username == username);

                // Nếu không tìm thấy -> coi như xóa thất bại
                if (user == null) return false;

                // Thực hiện xóa
                qlks.User_s.DeleteOnSubmit(user);
                qlks.SubmitChanges(); // Lưu xuống DB

                return true;
            }
            catch (Exception)
            {
                // Nếu có lỗi (VD: User này đang có ràng buộc khóa ngoại với bảng khác mà chưa xử lý)
                // Trả về false để bên ngoài thông báo lỗi, không làm crash app
                return false;
            }
        }

        // 6. Kiểm tra quyền
        public bool IsAdmin() => Global_.CurrentUser?.IsAdmin ?? false;
        public bool IsNhanVien() => Global_.CurrentUser?.IsNhanVien ?? false;
    }
}