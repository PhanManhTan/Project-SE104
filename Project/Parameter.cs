using System;
using System.Windows.Forms;
using Services; // Quan trọng: phải có dòng này

namespace Project
{
    public partial class Parameter : Form
    {
        private readonly ParameterService paramService = new ParameterService();

        public Parameter()
        {
            InitializeComponent();
        }

        private void QuyDinhForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            // 1. Số khách tối đa
            numMaxGuest.Value = paramService.GetThamSo(
                ParameterService.KEY_SO_KHACH_TOI_DA, 3);

            // 2. Tỷ lệ phụ thu (hiển thị dạng %)
            decimal tyLe = paramService.GetThamSo(
                ParameterService.KEY_TY_LE_PHU_THU, 0.25m);
            numPhuThu.Value = tyLe * 100; // 0.25 → 25

            // 3. Hệ số khách nước ngoài
            numHeSo.Value = paramService.GetThamSo(
                ParameterService.KEY_HE_SO_NUOC_NGOAI, 1.5m);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Số khách tối đa
                paramService.UpdateThamSo(ParameterService.KEY_SO_KHACH_TOI_DA, numMaxGuest.Value);

                // 2. Tỷ lệ phụ thu (chuyển từ % về thập phân)
                paramService.UpdateThamSo(ParameterService.KEY_TY_LE_PHU_THU, numPhuThu.Value / 100);

                // 3. Hệ số nước ngoài
                paramService.UpdateThamSo(ParameterService.KEY_HE_SO_NUOC_NGOAI, numHeSo.Value);

                MessageBox.Show("Cập nhật quy định thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Tốt nhất nên Dispose service khi form đóng
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            paramService?.Dispose();
            base.OnFormClosed(e);
        }
    }
}