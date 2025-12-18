// Data/User_.cs
using System;

namespace Data   // Đã để đúng namespace Data như bạn đang dùng
{
    public partial class User_
    {
        // Phần này do LINQ to SQL tự sinh (để trống là được)
    }

    public partial class User_
    {
        // Chuẩn hóa Role
        public string Role => Role_?.Trim().ToUpper() ?? "STAFF";

        // Phân quyền đúng theo database của bạn
        public bool IsAdmin => Role == "ADMIN";
        public bool IsNhanVien => Role == "STAFF" || IsAdmin;

        // Tên hiển thị đẹp
        public string DisplayName =>
            string.IsNullOrWhiteSpace(FullName) ? Username : FullName.Trim();

        // So sánh mật khẩu không phân biệt hoa thường, an toàn với null
        public bool CheckPassword(string password) =>
            (Password_ ?? "").Trim().ToLower() == (password ?? "").Trim().ToLower();

        // Hiển thị đẹp khi dùng MessageBox hoặc ToString()
        public override string ToString() =>
            $"{DisplayName} ({Username}) - {Role}";
    }
}