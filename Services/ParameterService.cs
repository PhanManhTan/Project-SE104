// File: Services/ParameterService.cs
using Data; // Đảm bảo namespace này chứa DBDataContext và entity ThamSo
using System;
using System.Linq;
using System.Windows.Forms;

namespace Services
{
    public class ParameterService : IDisposable
    {
        private readonly DBDataContext db = new DBDataContext();

        // Định nghĩa các KEY ở đây để mọi nơi dùng chung, tránh lỗi typo
        public const string KEY_SO_KHACH_TOI_DA = "SoKhachToiDa";
        public const string KEY_TY_LE_PHU_THU = "TyLePhuThu";
        public const string KEY_HE_SO_NUOC_NGOAI = "HeSoNuocNgoai";

        /// <summary>
        /// Lấy giá trị tham số, nếu không có thì trả về giá trị mặc định
        /// </summary>
        public decimal GetThamSo(string tenThamSo, decimal defaultValue = 0m)
        {
            try
            {
                var ts = db.ThamSos.FirstOrDefault(t => t.TenThamSo == tenThamSo);
                return ts != null ? ts.GiaTri : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public decimal GetDecimal(string tenThamSo, decimal defaultValue = 1.0m)
            => GetThamSo(tenThamSo, defaultValue);

        /// <summary>
        /// Cập nhật hoặc thêm mới tham số
        /// </summary>
        public bool UpdateThamSo(string tenThamSo, decimal giaTri)
        {
            try
            {
                var ts = db.ThamSos.FirstOrDefault(t => t.TenThamSo == tenThamSo);

                if (ts == null)
                {
                    ts = new ThamSo
                    {
                        TenThamSo = tenThamSo,
                        GiaTri = giaTri
                    };
                    db.ThamSos.InsertOnSubmit(ts);
                }
                else
                {
                    ts.GiaTri = giaTri;
                }

                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Có thể ghi log ở đây
                MessageBox.Show("Lỗi khi lưu tham số: " + ex.Message);
                return false;
            }
        }

        // Giải phóng tài nguyên
        public void Dispose()
        {
            db?.Dispose();
        }
    }
}