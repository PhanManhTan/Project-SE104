using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project
{
    partial class PaymentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblDiaChiValue = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblTenKhach = new System.Windows.Forms.Label();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lineHeader = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dgvBill = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTotalTitle);
            this.panelHeader.Controls.Add(this.lblTotalAmount);
            this.panelHeader.Controls.Add(this.lblDiaChiValue);
            this.panelHeader.Controls.Add(this.lblDiaChi);
            this.panelHeader.Controls.Add(this.lblTenKhach);
            this.panelHeader.Controls.Add(this.lblKhachHang);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lineHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(27, 16, 27, 8);
            this.panelHeader.Size = new System.Drawing.Size(1028, 130);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalTitle.Location = new System.Drawing.Point(711, 60);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(151, 28);
            this.lblTotalTitle.TabIndex = 0;
            this.lblTotalTitle.Text = "TỔNG TRỊ GIÁ:";
            this.lblTotalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalAmount.Location = new System.Drawing.Point(711, 80);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(0, 32);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiaChiValue
            // 
            this.lblDiaChiValue.AutoSize = true;
            this.lblDiaChiValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiaChiValue.Location = new System.Drawing.Point(99, 84);
            this.lblDiaChiValue.Name = "lblDiaChiValue";
            this.lblDiaChiValue.Size = new System.Drawing.Size(0, 23);
            this.lblDiaChiValue.TabIndex = 2;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiaChi.Location = new System.Drawing.Point(27, 84);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(66, 23);
            this.lblDiaChi.TabIndex = 3;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // lblTenKhach
            // 
            this.lblTenKhach.AutoSize = true;
            this.lblTenKhach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenKhach.Location = new System.Drawing.Point(213, 60);
            this.lblTenKhach.Name = "lblTenKhach";
            this.lblTenKhach.Size = new System.Drawing.Size(0, 23);
            this.lblTenKhach.TabIndex = 4;
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKhachHang.Location = new System.Drawing.Point(27, 60);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(187, 23);
            this.lblKhachHang.TabIndex = 5;
            this.lblKhachHang.Text = "Khách hàng / Cơ quan:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Navy;
            this.lblTitle.Location = new System.Drawing.Point(27, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(372, 41);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "HÓA ĐƠN THANH TOÁN";
            // 
            // lineHeader
            // 
            this.lineHeader.BackColor = System.Drawing.Color.Black;
            this.lineHeader.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lineHeader.Location = new System.Drawing.Point(27, 121);
            this.lineHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lineHeader.Name = "lineHeader";
            this.lineHeader.Size = new System.Drawing.Size(974, 1);
            this.lineHeader.TabIndex = 7;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelButtons.Controls.Add(this.button1);
            this.panelButtons.Controls.Add(this.button2);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 394);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(0, 16, 27, 24);
            this.panelButtons.Size = new System.Drawing.Size(1028, 80);
            this.panelButtons.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(852, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 48);
            this.button1.TabIndex = 13;
            this.button1.Text = "Thanh toán";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(679, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(166, 48);
            this.button2.TabIndex = 12;
            this.button2.Text = "Thoát";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvBill
            // 
            this.dgvBill.BackgroundColor = System.Drawing.Color.White;
            this.dgvBill.ColumnHeadersHeight = 34;
            this.dgvBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBill.Location = new System.Drawing.Point(0, 130);
            this.dgvBill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvBill.Name = "dgvBill";
            this.dgvBill.RowHeadersWidth = 62;
            this.dgvBill.Size = new System.Drawing.Size(1028, 264);
            this.dgvBill.TabIndex = 2;
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 474);
            this.Controls.Add(this.dgvBill);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelHeader);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thanh toán";
            this.Load += new System.EventHandler(this.PaymentForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.Label lblTenKhach;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblDiaChiValue;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Panel lineHeader;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.DataGridView dgvBill;
        private Button button1;
        private Button button2;
    }
}