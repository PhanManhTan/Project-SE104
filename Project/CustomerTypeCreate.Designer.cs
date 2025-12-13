namespace Project
{
    partial class CustomerTypeCreate
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
            this.btnCreate = new System.Windows.Forms.Button();
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
            this.btnCancel.Location = new System.Drawing.Point(202, 250); // Đã chỉnh lại vị trí lên cao
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 52);
            this.btnCancel.TabIndex = 64;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.SystemColors.Window;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCreate.Location = new System.Drawing.Point(317, 250); // Đã chỉnh lại vị trí lên cao
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(109, 52);
            this.btnCreate.TabIndex = 63;
            this.btnCreate.Text = "Tạo";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtTenLK
            // 
            this.txtTenLK.Location = new System.Drawing.Point(53, 186);
            this.txtTenLK.Multiline = true;
            this.txtTenLK.Name = "txtTenLK";
            this.txtTenLK.Size = new System.Drawing.Size(373, 33);
            this.txtTenLK.TabIndex = 60;
            // 
            // lbDG
            // 
            this.lbDG.AutoSize = true;
            this.lbDG.Location = new System.Drawing.Point(53, 146);
            this.lbDG.Name = "lbDG";
            this.lbDG.Size = new System.Drawing.Size(111, 20);
            this.lbDG.TabIndex = 59;
            this.lbDG.Text = "Tên loại khách";
            // 
            // txtMaLK
            // 
            this.txtMaLK.Location = new System.Drawing.Point(53, 86);
            this.txtMaLK.Multiline = true;
            this.txtMaLK.Name = "txtMaLK";
            this.txtMaLK.Size = new System.Drawing.Size(373, 33);
            this.txtMaLK.TabIndex = 58;
            // 
            // lbMaLK
            // 
            this.lbMaLK.AutoSize = true;
            this.lbMaLK.Location = new System.Drawing.Point(53, 46);
            this.lbMaLK.Name = "lbMaLK";
            this.lbMaLK.Size = new System.Drawing.Size(106, 20);
            this.lbMaLK.TabIndex = 57;
            this.lbMaLK.Text = "Mã loại khách";
            // 
            // LoaiKhach_CreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 330); // Thu nhỏ chiều cao Form lại
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtTenLK);
            this.Controls.Add(this.lbDG);
            this.Controls.Add(this.txtMaLK);
            this.Controls.Add(this.lbMaLK);
            this.Name = "LoaiKhach_CreateForm";
            this.Text = "Thêm loại khách mới";
            this.Load += new System.EventHandler(this.LoaiKhach_CreateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtTenLK;
        private System.Windows.Forms.Label lbDG;
        private System.Windows.Forms.TextBox txtMaLK;
        private System.Windows.Forms.Label lbMaLK;
    }
}