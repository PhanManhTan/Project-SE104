using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    partial class Main
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelLeft = new System.Windows.Forms.Panel();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.btnLoaiPhong = new System.Windows.Forms.Button();
            this.btnBaoCaoDoanhThu = new System.Windows.Forms.Button();
            this.btnQuyDinh = new System.Windows.Forms.Button();
            this.btnQuanLyPhong = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelContent.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(375, 1077);
            this.panelLeft.TabIndex = 0;
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.Location = new System.Drawing.Point(0, 0);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(75, 23);
            this.btnTrangChu.TabIndex = 0;
            // 
            // btnLoaiPhong
            // 
            this.btnLoaiPhong.Location = new System.Drawing.Point(0, 0);
            this.btnLoaiPhong.Name = "btnLoaiPhong";
            this.btnLoaiPhong.Size = new System.Drawing.Size(75, 23);
            this.btnLoaiPhong.TabIndex = 0;
            // 
            // btnBaoCaoDoanhThu
            // 
            this.btnBaoCaoDoanhThu.Location = new System.Drawing.Point(0, 0);
            this.btnBaoCaoDoanhThu.Name = "btnBaoCaoDoanhThu";
            this.btnBaoCaoDoanhThu.Size = new System.Drawing.Size(75, 23);
            this.btnBaoCaoDoanhThu.TabIndex = 0;
            // 
            // btnQuyDinh
            // 
            this.btnQuyDinh.Location = new System.Drawing.Point(0, 0);
            this.btnQuyDinh.Name = "btnQuyDinh";
            this.btnQuyDinh.Size = new System.Drawing.Size(75, 23);
            this.btnQuyDinh.TabIndex = 0;
            // 
            // btnQuanLyPhong
            // 
            this.btnQuanLyPhong.Location = new System.Drawing.Point(0, 0);
            this.btnQuanLyPhong.Name = "btnQuanLyPhong";
            this.btnQuanLyPhong.Size = new System.Drawing.Size(75, 23);
            this.btnQuanLyPhong.TabIndex = 0;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Location = new System.Drawing.Point(0, 0);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(75, 23);
            this.btnThanhToan.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.panelContent.Controls.Add(this.panel1);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(375, 0);
            this.panelContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1575, 1077);
            this.panelContent.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1575, 138);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(1389, 49);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 54);
            this.button1.TabIndex = 0;
            this.button1.Text = "ĐĂNG XUẤT";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(1119, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "QUẢN TRỊ VIÊN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(1119, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Chào, Nguyễn Văn A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.label3.Location = new System.Drawing.Point(30, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(573, 70);
            this.label3.TabIndex = 3;
            this.label3.Text = "QUẢN LÝ KHÁCH SẠN";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1950, 1077);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelLeft);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ KHÁCH SẠN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelContent.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelLeft;
        private Button btnTrangChu;
        private Button btnLoaiPhong;
        private Button btnBaoCaoDoanhThu;
        private Button btnQuyDinh;
        private Button btnQuanLyPhong;
        private Button btnThanhToan;

        private Panel panelContent;
        private Panel panel1;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}