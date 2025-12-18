using Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Services
{
    public class ParameterService : IDisposable
    {
        private readonly DBDataContext db = new DBDataContext();

        // Định nghĩa các KEY tập trung để tránh lỗi typo
        public const string KEY_SO_KHACH_TOI_DA = "SoKhachToiDa";
        public const string KEY_TY_LE_PHU_THU = "TyLePhuThu";
        public const string KEY_HE_SO_NUOC_NGOAI = "HeSoNuocNgoai";

        /// <summary>
        /// Lấy giá trị tham số từ database. Nếu không tồn tại hoặc lỗi → trả về 0
        /// </summary>
        public decimal GetThamSo(string tenThamSo)
        {
            try
            {
                var ts = db.ThamSos.FirstOrDefault(t => t.TenThamSo == tenThamSo);
                return ts?.GiaTri ?? 0m;
            }
            catch
            {
                return 0m;
            }
        }

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
                MessageBox.Show("Lỗi khi lưu tham số: " + ex.Message);
                return false;
            }
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}