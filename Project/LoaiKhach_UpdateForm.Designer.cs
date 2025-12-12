namespace Project
{
    partial class LoaiKhach_UpdateForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
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
            this.btnCancel.Location = new System.Drawing.Point(203, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 52);
            this.btnCancel.TabIndex = 56;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.Window;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpdate.Location = new System.Drawing.Point(318, 250);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(109, 52);
            this.btnUpdate.TabIndex = 55;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtTenLK
            // 
            this.txtTenLK.Location = new System.Drawing.Point(54, 180);
            this.txtTenLK.Multiline = true;
            this.txtTenLK.Name = "txtTenLK";
            this.txtTenLK.Size = new System.Drawing.Size(373, 33);
            this.txtTenLK.TabIndex = 52;
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
            this.ClientSize = new System.Drawing.Size(478, 330);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtTenLK);
            this.Controls.Add(this.lbDG);
            this.Controls.Add(this.txtMaLK);
            this.Controls.Add(this.lbMaLK);
            this.Name = "LoaiKhach_UpdateForm";
            this.Text = "Cập nhật loại khách";
            this.Load += new System.EventHandler(this.LoaiKhach_UpdateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtTenLK;
        private System.Windows.Forms.Label lbDG;
        private System.Windows.Forms.TextBox txtMaLK;
        private System.Windows.Forms.Label lbMaLK;
    }
}