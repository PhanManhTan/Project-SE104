using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Data;

namespace Services
{
    public class ParameterService
    {
        private readonly DBDataContext db = new DBDataContext();

        // --- SỬA CÁC KEY NÀY ĐỂ KHỚP VỚI DATABASE MỚI ---
        public const string KEY_SO_KHACH_TOI_DA = "SoKhachToiDa";
        public const string KEY_TY_LE_PHU_THU = "PhuThu_KhachThu3";      // Khớp với 'PhuThu_KhachThu3' trong SQL
        public const string KEY_HE_SO_NUOC_NGOAI = "HeSo_KhachNuocNgoai"; // Khớp với 'HeSo_KhachNuocNgoai' trong SQL

        /// <summary>
        /// Lấy giá trị tham số. Nếu chưa có thì tự tạo.
        /// </summary>
        public decimal GetThamSo(string tenThamSo, decimal giaTriMacDinh)
        {
            try
            {
                var thamSo = db.ThamSos.FirstOrDefault(ts => ts.TenThamSo == tenThamSo);
                if (thamSo == null)
                {
                    // Nếu chưa có thì Insert vào DB luôn
                    thamSo = new ThamSo { TenThamSo = tenThamSo, GiaTri = giaTriMacDinh };
                    db.ThamSos.InsertOnSubmit(thamSo);
                    db.SubmitChanges();
                }
                return thamSo.GiaTri;
            }
            catch (Exception)
            {
                // Trả về mặc định nếu lỗi kết nối
                return giaTriMacDinh;
            }
        }

        /// <summary>
        /// Cập nhật tham số
        /// </summary>
        public bool UpdateThamSo(string tenThamSo, decimal giaTriMoi)
        {
            try
            {
                var thamSo = db.ThamSos.FirstOrDefault(ts => ts.TenThamSo == tenThamSo);
                if (thamSo != null)
                {
                    thamSo.GiaTri = giaTriMoi;
                    db.SubmitChanges();
                    return true;
                }
                else
                {
                    // Nếu chưa có thì tạo mới
                    ThamSo newTs = new ThamSo { TenThamSo = tenThamSo, GiaTri = giaTriMoi };
                    db.ThamSos.InsertOnSubmit(newTs);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật tham số: " + ex.Message);
                return false;
            }
        }
    }
}