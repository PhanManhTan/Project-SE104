using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    partial class Rental
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMaPhongText = new System.Windows.Forms.Label();
            this.lblMaPhong = new System.Windows.Forms.Label();
            this.lblNgayThue = new System.Windows.Forms.Label();
            this.dtpNgayThue = new System.Windows.Forms.DateTimePicker();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.clbKhachHang = new System.Windows.Forms.CheckedListBox();
            this.btnLapPhieu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(261, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐẶT PHÒNG";
            // 
            // lblMaPhongText
            // 
            this.lblMaPhongText.AutoSize = true;
            this.lblMaPhongText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMaPhongText.Location = new System.Drawing.Point(30, 110);
            this.lblMaPhongText.Name = "lblMaPhongText";
            this.lblMaPhongText.Size = new System.Drawing.Size(138, 32);
            this.lblMaPhongText.TabIndex = 7;
            this.lblMaPhongText.Text = "Mã phòng:";
            // 
            // lblMaPhong
            // 
            this.lblMaPhong.AutoSize = true;
            this.lblMaPhong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMaPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblMaPhong.Location = new System.Drawing.Point(150, 107);
            this.lblMaPhong.Name = "lblMaPhong";
            this.lblMaPhong.Size = new System.Drawing.Size(82, 38);
            this.lblMaPhong.TabIndex = 6;
            this.lblMaPhong.Text = "P101";
            // 
            // lblNgayThue
            // 
            this.lblNgayThue.AutoSize = true;
            this.lblNgayThue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNgayThue.Location = new System.Drawing.Point(30, 160);
            this.lblNgayThue.Name = "lblNgayThue";
            this.lblNgayThue.Size = new System.Drawing.Size(118, 30);
            this.lblNgayThue.TabIndex = 5;
            this.lblNgayThue.Text = "Ngày thuê:";
            // 
            // dtpNgayThue
            // 
            this.dtpNgayThue.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayThue.Location = new System.Drawing.Point(150, 157);
            this.dtpNgayThue.Name = "dtpNgayThue";
            this.dtpNgayThue.Size = new System.Drawing.Size(200, 34);
            this.dtpNgayThue.TabIndex = 4;
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKhachHang.Location = new System.Drawing.Point(30, 210);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(366, 30);
            this.lblKhachHang.TabIndex = 3;
            this.lblKhachHang.Text = "Chọn khách hàng (tối đa 3 người):";
            // 
            // clbKhachHang
            // 
            this.clbKhachHang.CheckOnClick = true;
            this.clbKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.clbKhachHang.Location = new System.Drawing.Point(30, 245);
            this.clbKhachHang.Name = "clbKhachHang";
            this.clbKhachHang.Size = new System.Drawing.Size(430, 221);
            this.clbKhachHang.TabIndex = 2;
            // 
            // btnLapPhieu
            // 
            this.btnLapPhieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLapPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapPhieu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLapPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLapPhieu.Location = new System.Drawing.Point(80, 520);
            this.btnLapPhieu.Name = "btnLapPhieu";
            this.btnLapPhieu.Size = new System.Drawing.Size(200, 50);
            this.btnLapPhieu.TabIndex = 1;
            this.btnLapPhieu.Text = "Lập phiếu thuê";
            this.btnLapPhieu.UseVisualStyleBackColor = false;
            this.btnLapPhieu.Click += new System.EventHandler(this.btnLapPhieu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(310, 520);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(120, 50);
            this.btnHuy.TabIndex = 0;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 80);
            this.panel1.TabIndex = 8;
            // 
            // Rental
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(478, 564);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLapPhieu);
            this.Controls.Add(this.clbKhachHang);
            this.Controls.Add(this.lblKhachHang);
            this.Controls.Add(this.dtpNgayThue);
            this.Controls.Add(this.lblNgayThue);
            this.Controls.Add(this.lblMaPhong);
            this.Controls.Add(this.lblMaPhongText);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Rental";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lập phiếu thuê phòng";
            this.Load += new System.EventHandler(this.RentalForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaPhongText;
        private System.Windows.Forms.Label lblMaPhong;
        private System.Windows.Forms.Label lblNgayThue;
        private System.Windows.Forms.DateTimePicker dtpNgayThue;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.CheckedListBox clbKhachHang;
        private System.Windows.Forms.Button btnLapPhieu;
        private System.Windows.Forms.Button btnHuy;
    }
}