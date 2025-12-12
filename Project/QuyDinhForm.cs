using System;
using System.Windows.Forms;
using Services;

namespace Project
{
    public partial class QuyDinhForm : Form
    {
        // Khởi tạo Service
        private readonly ParameterService paramService = new ParameterService();

        public QuyDinhForm()
        {
            InitializeComponent();
        }

        private void QuyDinhForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            // 1. Số khách tối đa (DB: 3 -> Form: 3)
            numMaxGuest.Value = paramService.GetThamSo(ParameterService.KEY_SO_KHACH_TOI_DA, 3);

            // 2. Tỷ lệ phụ thu (DB: 0.25 -> Form: 25)
            // Lấy 0.25 nhân 100 để hiển thị 25% cho người dùng dễ hiểu
            decimal tyLe = paramService.GetThamSo(ParameterService.KEY_TY_LE_PHU_THU, 0.25m);
            numPhuThu.Value = tyLe * 100;

            // 3. Hệ số khách nước ngoài (DB: 1.5 -> Form: 1.5)
            numHeSo.Value = paramService.GetThamSo(ParameterService.KEY_HE_SO_NUOC_NGOAI, 1.5m);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lưu Số khách tối đa
                paramService.UpdateThamSo(ParameterService.KEY_SO_KHACH_TOI_DA, numMaxGuest.Value);

                // 2. Lưu Tỷ lệ phụ thu (Form: 25 -> DB: 0.25)
                // Chia 100 trước khi lưu xuống DB
                paramService.UpdateThamSo(ParameterService.KEY_TY_LE_PHU_THU, numPhuThu.Value / 100);

                // 3. Lưu Hệ số nước ngoài
                paramService.UpdateThamSo(ParameterService.KEY_HE_SO_NUOC_NGOAI, numHeSo.Value);

                MessageBox.Show("Cập nhật quy định thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}