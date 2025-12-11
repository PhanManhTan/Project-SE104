namespace QuanLyKhachSan
{
    partial class LoaiKhach_UpdateForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cboTTSD = new System.Windows.Forms.ComboBox();
            this.lbTTSD = new System.Windows.Forms.Label();
            this.txtTenLK = new System.Windows.Forms.TextBox();
            this.lbDG = new System.Windows.Forms.Label();
            this.txtMaLK = new System.Windows.Forms.TextBox();
            this.lbMaLK = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Window;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(203, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 52);
            this.btnCancel.TabIndex = 56;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.Window;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpdate.Location = new System.Drawing.Point(318, 340);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(109, 52);
            this.btnUpdate.TabIndex = 55;
            this.btnUpdate.TabStop = false;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cboTTSD
            // 
            this.cboTTSD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTTSD.FormattingEnabled = true;
            this.cboTTSD.Location = new System.Drawing.Point(54, 280);
            this.cboTTSD.Name = "cboTTSD";
            this.cboTTSD.Size = new System.Drawing.Size(373, 28);
            this.cboTTSD.TabIndex = 54;
            this.cboTTSD.TabStop = false;
            // 
            // lbTTSD
            // 
            this.lbTTSD.AutoSize = true;
            this.lbTTSD.Location = new System.Drawing.Point(54, 240);
            this.lbTTSD.Name = "lbTTSD";
            this.lbTTSD.Size = new System.Drawing.Size(141, 20);
            this.lbTTSD.TabIndex = 53;
            this.lbTTSD.Text = "Trạng thái sử dụng";
            // 
            // txtTenLK
            // 
            this.txtTenLK.Location = new System.Drawing.Point(54, 180);
            this.txtTenLK.Multiline = true;
            this.txtTenLK.Name = "txtTenLK";
            this.txtTenLK.Size = new System.Drawing.Size(373, 33);
            this.txtTenLK.TabIndex = 52;
            this.txtTenLK.TabStop = false;
            // 
            // lbDG
            // 
            this.lbDG.AutoSize = true;
            this.lbDG.Location = new System.Drawing.Point(54, 140);
            this.lbDG.Name = "lbDG";
            this.lbDG.Size = new System.Drawing.Size(111, 20);
            this.lbDG.TabIndex = 51;
            this.lbDG.Text = "Tên loại khách";
            // 
            // txtMaLK
            // 
            this.txtMaLK.Location = new System.Drawing.Point(54, 80);
            this.txtMaLK.Multiline = true;
            this.txtMaLK.Name = "txtMaLK";
            this.txtMaLK.Size = new System.Drawing.Size(373, 33);
            this.txtMaLK.TabIndex = 50;
            this.txtMaLK.TabStop = false;
            // 
            // lbMaLK
            // 
            this.lbMaLK.AutoSize = true;
            this.lbMaLK.Location = new System.Drawing.Point(54, 40);
            this.lbMaLK.Name = "lbMaLK";
            this.lbMaLK.Size = new System.Drawing.Size(106, 20);
            this.lbMaLK.TabIndex = 49;
            this.lbMaLK.Text = "Mã loại khách";
            // 
            // LoaiKhach_UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 424);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.cboTTSD);
            this.Controls.Add(this.lbTTSD);
            this.Controls.Add(this.txtTenLK);
            this.Controls.Add(this.lbDG);
            this.Controls.Add(this.txtMaLK);
            this.Controls.Add(this.lbMaLK);
            this.Name = "LoaiKhach_UpdateForm";
            this.Text = "LoaiPhong_UpdateForm";
            this.Load += new System.EventHandler(this.LoaiKhach_UpdateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cboTTSD;
        private System.Windows.Forms.Label lbTTSD;
        private System.Windows.Forms.TextBox txtTenLK;
        private System.Windows.Forms.Label lbDG;
        private System.Windows.Forms.TextBox txtMaLK;
        private System.Windows.Forms.Label lbMaLK;
    }
}