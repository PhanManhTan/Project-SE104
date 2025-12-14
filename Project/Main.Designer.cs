namespace Project
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelButton = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnReservationList = new System.Windows.Forms.Button();
            this.btnBillList = new System.Windows.Forms.Button();
            this.btnCustomerList = new System.Windows.Forms.Button();
            this.btnRoomList = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnPolicy = new System.Windows.Forms.Button();
            this.btnAccountManagement = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelBody = new System.Windows.Forms.Panel();
            this.lbRole = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.panelButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.panelButton.Controls.Add(this.lbRole);
            this.panelButton.Controls.Add(this.lbName);
            this.panelButton.Controls.Add(this.btnHome);
            this.panelButton.Controls.Add(this.btnReservationList);
            this.panelButton.Controls.Add(this.btnBillList);
            this.panelButton.Controls.Add(this.btnCustomerList);
            this.panelButton.Controls.Add(this.btnRoomList);
            this.panelButton.Controls.Add(this.btnReport);
            this.panelButton.Controls.Add(this.btnPolicy);
            this.panelButton.Controls.Add(this.btnAccountManagement);
            this.panelButton.Controls.Add(this.btnLogout);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButton.Location = new System.Drawing.Point(0, 0);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(258, 843);
            this.panelButton.TabIndex = 0;
            // 
            // btnHome
            // 
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnHome.Location = new System.Drawing.Point(0, 141);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(258, 78);
            this.btnHome.TabIndex = 13;
            this.btnHome.Text = "Trang chủ";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnReservationList
            // 
            this.btnReservationList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnReservationList.FlatAppearance.BorderSize = 0;
            this.btnReservationList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReservationList.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnReservationList.Location = new System.Drawing.Point(0, 219);
            this.btnReservationList.Name = "btnReservationList";
            this.btnReservationList.Size = new System.Drawing.Size(258, 78);
            this.btnReservationList.TabIndex = 12;
            this.btnReservationList.Text = " Danh sách phiếu thuê";
            this.btnReservationList.UseVisualStyleBackColor = true;
            this.btnReservationList.Click += new System.EventHandler(this.btnReservationList_Click);
            // 
            // btnBillList
            // 
            this.btnBillList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBillList.FlatAppearance.BorderSize = 0;
            this.btnBillList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBillList.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnBillList.Location = new System.Drawing.Point(0, 297);
            this.btnBillList.Name = "btnBillList";
            this.btnBillList.Size = new System.Drawing.Size(258, 78);
            this.btnBillList.TabIndex = 11;
            this.btnBillList.Text = "Danh sách hóa đơn";
            this.btnBillList.UseVisualStyleBackColor = true;
            this.btnBillList.Click += new System.EventHandler(this.btnBillList_Click);
            // 
            // btnCustomerList
            // 
            this.btnCustomerList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCustomerList.FlatAppearance.BorderSize = 0;
            this.btnCustomerList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomerList.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnCustomerList.Location = new System.Drawing.Point(0, 375);
            this.btnCustomerList.Name = "btnCustomerList";
            this.btnCustomerList.Size = new System.Drawing.Size(258, 78);
            this.btnCustomerList.TabIndex = 10;
            this.btnCustomerList.Text = "Danh sách khách ";
            this.btnCustomerList.UseVisualStyleBackColor = true;
            this.btnCustomerList.Click += new System.EventHandler(this.btnCustomerList_Click);
            // 
            // btnRoomList
            // 
            this.btnRoomList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRoomList.FlatAppearance.BorderSize = 0;
            this.btnRoomList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomList.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnRoomList.Location = new System.Drawing.Point(0, 453);
            this.btnRoomList.Name = "btnRoomList";
            this.btnRoomList.Size = new System.Drawing.Size(258, 78);
            this.btnRoomList.TabIndex = 8;
            this.btnRoomList.Text = "Danh sách phòng";
            this.btnRoomList.UseVisualStyleBackColor = true;
            this.btnRoomList.Click += new System.EventHandler(this.btnRoomList_Click);
            // 
            // btnReport
            // 
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnReport.Location = new System.Drawing.Point(0, 531);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(258, 78);
            this.btnReport.TabIndex = 7;
            this.btnReport.Text = "Báo cáo";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnPolicy
            // 
            this.btnPolicy.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPolicy.FlatAppearance.BorderSize = 0;
            this.btnPolicy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPolicy.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnPolicy.Location = new System.Drawing.Point(0, 609);
            this.btnPolicy.Name = "btnPolicy";
            this.btnPolicy.Size = new System.Drawing.Size(258, 78);
            this.btnPolicy.TabIndex = 6;
            this.btnPolicy.Text = "Quy định";
            this.btnPolicy.UseVisualStyleBackColor = true;
            this.btnPolicy.Click += new System.EventHandler(this.btnPolicy_Click);
            // 
            // btnAccountManagement
            // 
            this.btnAccountManagement.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAccountManagement.FlatAppearance.BorderSize = 0;
            this.btnAccountManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccountManagement.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnAccountManagement.Location = new System.Drawing.Point(0, 687);
            this.btnAccountManagement.Name = "btnAccountManagement";
            this.btnAccountManagement.Size = new System.Drawing.Size(258, 78);
            this.btnAccountManagement.TabIndex = 5;
            this.btnAccountManagement.Text = "Quản lý tài khoản";
            this.btnAccountManagement.UseVisualStyleBackColor = true;
            this.btnAccountManagement.Click += new System.EventHandler(this.btnAccountManagement_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogout.Location = new System.Drawing.Point(0, 765);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(258, 78);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.White;
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelBody.Location = new System.Drawing.Point(258, 0);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(1132, 843);
            this.panelBody.TabIndex = 1;
            // 
            // lbRole
            // 
            this.lbRole.AutoSize = true;
            this.lbRole.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lbRole.Location = new System.Drawing.Point(36, 56);
            this.lbRole.Name = "lbRole";
            this.lbRole.Size = new System.Drawing.Size(61, 32);
            this.lbRole.TabIndex = 15;
            this.lbRole.Text = "Role";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lbName.Location = new System.Drawing.Point(36, 8);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(79, 32);
            this.lbName.TabIndex = 14;
            this.lbName.Text = "Name";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 843);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Quản lý khách sạn";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelButton.ResumeLayout(false);
            this.panelButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnAccountManagement;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnPolicy;
        private System.Windows.Forms.Button btnCustomerList;
        private System.Windows.Forms.Button btnRoomList;
        private System.Windows.Forms.Button btnBillList;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnReservationList;
        private System.Windows.Forms.Label lbRole;
        private System.Windows.Forms.Label lbName;
    }
}