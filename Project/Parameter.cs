using Data;
using Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    public partial class Parameter : Form
    {
        private readonly ParameterService paramService = new ParameterService();

        public Parameter()
        {
            InitializeComponent();
            if (Global_.CurrentUser != null && Global_.CurrentUser.Role_ == "Staff")
            {
                // Vô hiệu hóa nút Lưu
                btnDone.Enabled = false;
                btnDone.BackColor = Color.DarkGray;

                // Khóa các ô nhập liệu (chế độ chỉ xem)
                numMaxGuest.Enabled = false;
                numPhuThu.Enabled = false;
                numHeSo.Enabled = false;

            }

        }

        private void Parameter_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        #region === LOAD DỮ LIỆU ===
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
        #endregion

        #region === LƯU THAY ĐỔI ===
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Số khách tối đa
                paramService.UpdateThamSo(ParameterService.KEY_SO_KHACH_TOI_DA, (int)numMaxGuest.Value);

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
        #endregion

        #region === HỦY (KHÔNG LƯU - GIỮ NGUYÊN QUY ĐỊNH CŨ) ===


        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            paramService?.Dispose();
            base.OnFormClosed(e);
        }
        #endregion
    }
}