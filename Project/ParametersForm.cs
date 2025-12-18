using Data;
using Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class ParametersForm : Form
    {
        private readonly ParameterService paramService = new ParameterService();

        private const string KEY_SO_KHACH_TOI_DA = "SoKhachToiDa";
        private const string KEY_TY_LE_PHU_THU = "TyLePhuThu";
        private const string KEY_HE_SO_NUOC_NGOAI = "HeSoNuocNgoai";

        public ParametersForm()
        {
            InitializeComponent();

            numMaxGuest.Minimum = 1;   
            numPhuThu.Minimum = 0;     
            numHeSo.Minimum = 0.1m;    


            numMaxGuest.Maximum = 20;
            numPhuThu.Maximum = 200;   
            numHeSo.Maximum = 10;

            if (Global_.CurrentUser != null && Global_.CurrentUser.Role_ == "Staff")
            {
                btnDone.Enabled = false;
                btnDone.BackColor = Color.DarkGray;
                numMaxGuest.Enabled = false;
                numPhuThu.Enabled = false;
                numHeSo.Enabled = false;
            }
        }

        private void Parameter_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Lấy dữ liệu từ DB, dùng giá trị mặc định an toàn nếu không có
                int soKhach = paramService.GetThamSoInt(KEY_SO_KHACH_TOI_DA, 3); // mặc định 3
                numMaxGuest.Value = Math.Max(numMaxGuest.Minimum, Math.Min(numMaxGuest.Maximum, soKhach));

                decimal tyLe = paramService.GetThamSoDecimal(KEY_TY_LE_PHU_THU, 0.25m); // mặc định 25%
                numPhuThu.Value = Math.Max(numPhuThu.Minimum, Math.Min(numPhuThu.Maximum, tyLe * 100));

                decimal heSo = paramService.GetThamSoDecimal(KEY_HE_SO_NUOC_NGOAI, 1.5m); // mặc định 1.5
                numHeSo.Value = Math.Max(numHeSo.Minimum, Math.Min(numHeSo.Maximum, heSo));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải tham số: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                paramService.UpdateThamSo(KEY_SO_KHACH_TOI_DA, (int)numMaxGuest.Value);
                paramService.UpdateThamSo(KEY_TY_LE_PHU_THU, numPhuThu.Value / 100);
                paramService.UpdateThamSo(KEY_HE_SO_NUOC_NGOAI, numHeSo.Value);

                MessageBox.Show("Cập nhật quy định thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lưu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            paramService?.Dispose();
            base.OnFormClosed(e);
        }
    }


    public static class ParameterServiceExtensions
    {
        public static int GetThamSoInt(this ParameterService service, string key, int defaultValue = 0)
        {
            try
            {
                return (int)service.GetThamSo(key);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static decimal GetThamSoDecimal(this ParameterService service, string key, decimal defaultValue = 0)
        {
            try
            {
                return service.GetThamSo(key);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}