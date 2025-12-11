// Services/ParameterService.cs
using System.Linq;
using Data;

namespace Services
{
    public class ParameterService
    {
        private readonly DBDataContext db = new DBDataContext();

        // Đọc hệ số
        public decimal HeSoKhachThu3 => GetValue("HeSoKhachThu3", 1.25m);
        public decimal HeSoNuocNgoai => GetValue("HeSoNuocNgoai", 1.5m);
        public int SoKhachToiDa => 3; // cố định theo đề

        private decimal GetValue(string tenThamSo, decimal defaultValue)
        {
            var ts = db.ThamSos.FirstOrDefault(t => t.TenThamSo == tenThamSo);
            return ts?.GiaTri ?? defaultValue;
        }

        // Cập nhật hệ số (BM6)
        public bool UpdateHeSo(string tenThamSo, decimal giaTri)
        {
            var ts = db.ThamSos.FirstOrDefault(t => t.TenThamSo == tenThamSo);
            if (ts == null)
            {
                ts = new ThamSo { TenThamSo = tenThamSo, GiaTri = giaTri };
                db.ThamSos.InsertOnSubmit(ts);
            }
            else
            {
                ts.GiaTri = giaTri;
            }
            db.SubmitChanges();
            return true;
        }
    }
}