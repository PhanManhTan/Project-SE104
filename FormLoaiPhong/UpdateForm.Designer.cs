namespace FormLoaiPhong
{
    partial class UpdateForm
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
            this.lbMaLK = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cboTTSD = new System.Windows.Forms.ComboBox();
            this.lbTTSD = new System.Windows.Forms.Label();
            this.txtDG = new System.Windows.Forms.TextBox();
            this.lbDG = new System.Windows.Forms.Label();
            this.txtMaLK = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbMaLK
            // 
            this.lbMaLK.AutoSize = true;
            this.lbMaLK.Location = new System.Drawing.Point(101, -26);
            this.lbMaLK.Name = "lbMaLK";
            this.lbMaLK.Size = new System.Drawing.Size(108, 20);
            this.lbMaLK.TabIndex = 34;
            this.lbMaLK.Text = "Mã loại phòng";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.Window;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpdate.Location = new System.Drawing.Point(377, 376);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(109, 52);
            this.btnUpdate.TabIndex = 41;
            this.btnUpdate.TabStop = false;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cboTTSD
            // 
            this.cboTTSD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTTSD.FormattingEnabled = true;
            this.cboTTSD.Location = new System.Drawing.Point(41, 248);
            this.cboTTSD.Name = "cboTTSD";
            this.cboTTSD.Size = new System.Drawing.Size(373, 28);
            this.cboTTSD.TabIndex = 40;
            this.cboTTSD.TabStop = false;
            // 
            // lbTTSD
            // 
            this.lbTTSD.AutoSize = true;
            this.lbTTSD.Location = new System.Drawing.Point(41, 208);
            this.lbTTSD.Name = "lbTTSD";
            this.lbTTSD.Size = new System.Drawing.Size(141, 20);
            this.lbTTSD.TabIndex = 39;
            this.lbTTSD.Text = "Trạng thái sử dụng";
            // 
            // txtDG
            // 
            this.txtDG.Location = new System.Drawing.Point(41, 155);
            this.txtDG.Multiline = true;
            this.txtDG.Name = "txtDG";
            this.txtDG.Size = new System.Drawing.Size(373, 33);
            this.txtDG.TabIndex = 38;
            this.txtDG.TabStop = false;
            // 
            // lbDG
            // 
            this.lbDG.AutoSize = true;
            this.lbDG.Location = new System.Drawing.Point(41, 115);
            this.lbDG.Name = "lbDG";
            this.lbDG.Size = new System.Drawing.Size(64, 20);
            this.lbDG.TabIndex = 37;
            this.lbDG.Text = "Đơn giá";
            // 
            // txtMaLK
            // 
            this.txtMaLK.Location = new System.Drawing.Point(41, 62);
            this.txtMaLK.Multiline = true;
            this.txtMaLK.Name = "txtMaLK";
            this.txtMaLK.Size = new System.Drawing.Size(373, 33);
            this.txtMaLK.TabIndex = 36;
            this.txtMaLK.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "Mã loại phòng";
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 450);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.cboTTSD);
            this.Controls.Add(this.lbTTSD);
            this.Controls.Add(this.txtDG);
            this.Controls.Add(this.lbDG);
            this.Controls.Add(this.txtMaLK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbMaLK);
            this.Name = "UpdateForm";
            this.Text = "UpdateForm";
            this.Load += new System.EventHandler(this.UpdateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbMaLK;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cboTTSD;
        private System.Windows.Forms.Label lbTTSD;
        private System.Windows.Forms.TextBox txtDG;
        private System.Windows.Forms.Label lbDG;
        private System.Windows.Forms.TextBox txtMaLK;
        private System.Windows.Forms.Label label1;
    }
}