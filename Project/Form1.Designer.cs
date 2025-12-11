namespace Project
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnQuanLyPhong = new System.Windows.Forms.Button();
            this.btnQuyDinh = new System.Windows.Forms.Button();
            this.btnBaoCaoDoanhThu = new System.Windows.Forms.Button();
            this.btnLoaiPhong = new System.Windows.Forms.Button();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.ColumnSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLoaiPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.panelMenu.Controls.Add(this.btnThanhToan);
            this.panelMenu.Controls.Add(this.btnQuanLyPhong);
            this.panelMenu.Controls.Add(this.btnQuyDinh);
            this.panelMenu.Controls.Add(this.btnBaoCaoDoanhThu);
            this.panelMenu.Controls.Add(this.btnLoaiPhong);
            this.panelMenu.Controls.Add(this.btnTrangChu);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 894);
            this.panelMenu.TabIndex = 0;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThanhToan.FlatAppearance.BorderSize = 0;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnThanhToan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnThanhToan.Location = new System.Drawing.Point(0, 350);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnThanhToan.Size = new System.Drawing.Size(220, 70);
            this.btnThanhToan.TabIndex = 5;
            this.btnThanhToan.Text = "THANH TOÁN";
            this.btnThanhToan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThanhToan.UseVisualStyleBackColor = true;
            // 
            // btnQuanLyPhong
            // 
            this.btnQuanLyPhong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnQuanLyPhong.FlatAppearance.BorderSize = 0;
            this.btnQuanLyPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyPhong.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnQuanLyPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnQuanLyPhong.Location = new System.Drawing.Point(0, 280);
            this.btnQuanLyPhong.Name = "btnQuanLyPhong";
            this.btnQuanLyPhong.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnQuanLyPhong.Size = new System.Drawing.Size(220, 70);
            this.btnQuanLyPhong.TabIndex = 4;
            this.btnQuanLyPhong.Text = "QUẢN LÝ PHÒNG";
            this.btnQuanLyPhong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuanLyPhong.UseVisualStyleBackColor = true;
            // 
            // btnQuyDinh
            // 
            this.btnQuyDinh.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnQuyDinh.FlatAppearance.BorderSize = 0;
            this.btnQuyDinh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuyDinh.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnQuyDinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnQuyDinh.Location = new System.Drawing.Point(0, 210);
            this.btnQuyDinh.Name = "btnQuyDinh";
            this.btnQuyDinh.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnQuyDinh.Size = new System.Drawing.Size(220, 70);
            this.btnQuyDinh.TabIndex = 3;
            this.btnQuyDinh.Text = "QUY ĐỊNH";
            this.btnQuyDinh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuyDinh.UseVisualStyleBackColor = true;
            // 
            // btnBaoCaoDoanhThu
            // 
            this.btnBaoCaoDoanhThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCaoDoanhThu.FlatAppearance.BorderSize = 0;
            this.btnBaoCaoDoanhThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoDoanhThu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnBaoCaoDoanhThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBaoCaoDoanhThu.Location = new System.Drawing.Point(0, 140);
            this.btnBaoCaoDoanhThu.Name = "btnBaoCaoDoanhThu";
            this.btnBaoCaoDoanhThu.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnBaoCaoDoanhThu.Size = new System.Drawing.Size(220, 70);
            this.btnBaoCaoDoanhThu.TabIndex = 2;
            this.btnBaoCaoDoanhThu.Text = "BÁO CÁO DOANH THU";
            this.btnBaoCaoDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCaoDoanhThu.UseVisualStyleBackColor = true;
            // 
            // btnLoaiPhong
            // 
            this.btnLoaiPhong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLoaiPhong.FlatAppearance.BorderSize = 0;
            this.btnLoaiPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoaiPhong.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnLoaiPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnLoaiPhong.Location = new System.Drawing.Point(0, 70);
            this.btnLoaiPhong.Name = "btnLoaiPhong";
            this.btnLoaiPhong.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnLoaiPhong.Size = new System.Drawing.Size(220, 70);
            this.btnLoaiPhong.TabIndex = 1;
            this.btnLoaiPhong.Text = "LOẠI PHÒNG";
            this.btnLoaiPhong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoaiPhong.UseVisualStyleBackColor = true;
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTrangChu.FlatAppearance.BorderSize = 0;
            this.btnTrangChu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrangChu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnTrangChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnTrangChu.Location = new System.Drawing.Point(0, 0);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnTrangChu.Size = new System.Drawing.Size(220, 70);
            this.btnTrangChu.TabIndex = 0;
            this.btnTrangChu.Text = "TRANG CHỦ";
            this.btnTrangChu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrangChu.UseVisualStyleBackColor = true;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.dgvPhong);
            this.panelContent.Controls.Add(this.txtTimKiem);
            this.panelContent.Controls.Add(this.lblTitle);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(220, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1209, 894);
            this.panelContent.TabIndex = 1;
            // 
            // dgvPhong
            // 
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.AllowUserToDeleteRows = false;
            this.dgvPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSTT,
            this.ColumnMaPhong,
            this.ColumnLoaiPhong,
            this.ColumnGia,
            this.ColumnGhiChu,
            this.ColumnTrangThai});
            this.dgvPhong.EnableHeadersVisualStyles = false;
            this.dgvPhong.Location = new System.Drawing.Point(0, 164);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.RowHeadersVisible = false;
            this.dgvPhong.RowHeadersWidth = 62;
            this.dgvPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhong.Size = new System.Drawing.Size(1209, 727);
            this.dgvPhong.TabIndex = 2;
            this.dgvPhong.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhong_CellContentClick);
            // 
            // ColumnSTT
            // 
            this.ColumnSTT.HeaderText = "STT";
            this.ColumnSTT.MinimumWidth = 8;
            this.ColumnSTT.Name = "ColumnSTT";
            this.ColumnSTT.ReadOnly = true;
            this.ColumnSTT.Width = 60;
            // 
            // ColumnMaPhong
            // 
            this.ColumnMaPhong.HeaderText = "MÃ PHÒNG";
            this.ColumnMaPhong.MinimumWidth = 8;
            this.ColumnMaPhong.Name = "ColumnMaPhong";
            this.ColumnMaPhong.ReadOnly = true;
            this.ColumnMaPhong.Width = 120;
            // 
            // ColumnLoaiPhong
            // 
            this.ColumnLoaiPhong.HeaderText = "LOẠI PHÒNG";
            this.ColumnLoaiPhong.MinimumWidth = 8;
            this.ColumnLoaiPhong.Name = "ColumnLoaiPhong";
            this.ColumnLoaiPhong.ReadOnly = true;
            this.ColumnLoaiPhong.Width = 150;
            // 
            // ColumnGia
            // 
            dataGridViewCellStyle2.Format = "N0";
            this.ColumnGia.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnGia.HeaderText = "GIÁ";
            this.ColumnGia.MinimumWidth = 8;
            this.ColumnGia.Name = "ColumnGia";
            this.ColumnGia.ReadOnly = true;
            this.ColumnGia.Width = 150;
            // 
            // ColumnGhiChu
            // 
            this.ColumnGhiChu.HeaderText = "GHI CHÚ";
            this.ColumnGhiChu.MinimumWidth = 8;
            this.ColumnGhiChu.Name = "ColumnGhiChu";
            this.ColumnGhiChu.ReadOnly = true;
            this.ColumnGhiChu.Width = 150;
            // 
            // ColumnTrangThai
            // 
            this.ColumnTrangThai.HeaderText = "TRẠNG THÁI";
            this.ColumnTrangThai.MinimumWidth = 8;
            this.ColumnTrangThai.Name = "ColumnTrangThai";
            this.ColumnTrangThai.ReadOnly = true;
            this.ColumnTrangThai.Width = 150;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            this.txtTimKiem.Location = new System.Drawing.Point(30, 110);
            this.txtTimKiem.Multiline = true;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(500, 35);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.Text = "TÌM KIẾM";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(530, 65);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ KHÁCH SẠN";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1429, 894);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ KHÁCH SẠN";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnTrangChu;
        private System.Windows.Forms.Button btnLoaiPhong;
        private System.Windows.Forms.Button btnBaoCaoDoanhThu;
        private System.Windows.Forms.Button btnQuyDinh;
        private System.Windows.Forms.Button btnQuanLyPhong;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLoaiPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGhiChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTrangThai;
    }
}