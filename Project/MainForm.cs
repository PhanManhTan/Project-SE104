using Data;
using System;
using System.Drawing; // Thêm để dùng Color.FromArgb
using System.Windows.Forms;

namespace Project
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Định nghĩa màu sắc
        private readonly Color NormalButtonColor = Color.FromArgb(225, 240, 255); // Màu nền mặc định
        private readonly Color ActiveButtonColor = Color.FromArgb(0, 120, 215);   // Màu khi active (xanh đậm)
        private readonly Color NormalForeColor = SystemColors.ControlText;       // Màu chữ mặc định (ControlText)
        private readonly Color ActiveForeColor = Color.White;                    // Màu chữ khi active

        private Button currentActiveButton = null;

        private void Main_Load(object sender, EventArgs e)
        {
            if (Global_.CurrentUser != null)
            {
                lbName.Text = Global_.CurrentUser.DisplayName;
                lbRole.Text = Global_.CurrentUser.Role.ToUpper();
            }

            // Mở trang chủ mặc định và highlight nút Home
            OpenForm(new HomeForm());
            ActivateButton(btnHome);
        }

        private Form curentFormChild;

        private void OpenForm(Form form)
        {
            if (curentFormChild != null)
            {
                curentFormChild.Close();
            }
            curentFormChild = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelBody.Controls.Add(form);
            panelBody.Tag = form;
            form.BringToFront();
            form.Show();
        }

        private void ActivateButton(Button btn)
        {
            if (currentActiveButton != null && currentActiveButton != btn)
            {
                // Trả về trạng thái bình thường cho button cũ
                currentActiveButton.BackColor = NormalButtonColor;
                currentActiveButton.ForeColor = NormalForeColor;
            }

            // Kích hoạt button mới
            btn.BackColor = ActiveButtonColor;
            btn.ForeColor = ActiveForeColor;

            currentActiveButton = btn;
        }

        // Các sự kiện click button - đã thêm highlight
        private void btnHome_Click(object sender, EventArgs e)
        {
            ActivateButton(btnHome);
            OpenForm(new HomeForm());
        }

        private void btnReservationList_Click(object sender, EventArgs e)
        {
            ActivateButton(btnReservationList);
            // Nếu có form tương ứng thì thêm OpenForm ở đây sau
            OpenForm(new RentalsForm());
        }

        private void btnBillList_Click(object sender, EventArgs e)
        {
            ActivateButton(btnBillList);
            OpenForm(new BillsForm());
        }

        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            ActivateButton(btnCustomerList);
            OpenForm(new CustomersForm());
        }

        private void btnRoomList_Click(object sender, EventArgs e)
        {
            ActivateButton(btnRoomList);
            OpenForm(new RoomsForm());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ActivateButton(btnReport);
            OpenForm(new ReportForm());
        }

        private void btnPolicy_Click(object sender, EventArgs e)
        {
            ActivateButton(btnPolicy);
            OpenForm(new ParametersForm());
        }

        private void btnAccountManagement_Click(object sender, EventArgs e)
        {
            ActivateButton(btnAccountManagement);
            OpenForm(new UsersForm());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ActivateButton(btnLogout);
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Global_.CurrentUser = null;
                this.Close();
            }

        }
    }
}