namespace FormLoaiPhong
{
    partial class CreateForm
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
            this.txtDG = new System.Windows.Forms.TextBox();
            this.lbDG = new System.Windows.Forms.Label();
            this.txtMaLK = new System.Windows.Forms.TextBox();
            this.lbMaLK = new System.Windows.Forms.Label();
            this.cboTTSD = new System.Windows.Forms.ComboBox();
            this.lbTTSD = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDG
            // 
            this.txtDG.Location = new System.Drawing.Point(108, 196);
            this.txtDG.Multiline = true;
            this.txtDG.Name = "txtDG";
            this.txtDG.Size = new System.Drawing.Size(373, 33);
            this.txtDG.TabIndex = 29;
            this.txtDG.TabStop = false;
            // 
            // lbDG
            // 
            this.lbDG.AutoSize = true;
            this.lbDG.Location = new System.Drawing.Point(108, 156);
            this.lbDG.Name = "lbDG";
            this.lbDG.Size = new System.Drawing.Size(64, 20);
            this.lbDG.TabIndex = 28;
            this.lbDG.Text = "Đơn giá";
            // 
            // txtMaLK
            // 
            this.txtMaLK.Location = new System.Drawing.Point(108, 103);
            this.txtMaLK.Multiline = true;
            this.txtMaLK.Name = "txtMaLK";
            this.txtMaLK.Size = new System.Drawing.Size(373, 33);
            this.txtMaLK.TabIndex = 27;
            this.txtMaLK.TabStop = false;
            // 
            // lbMaLK
            // 
            this.lbMaLK.AutoSize = true;
            this.lbMaLK.Location = new System.Drawing.Point(108, 63);
            this.lbMaLK.Name = "lbMaLK";
            this.lbMaLK.Size = new System.Drawing.Size(108, 20);
            this.lbMaLK.TabIndex = 26;
            this.lbMaLK.Text = "Mã loại phòng";
            // 
            // cboTTSD
            // 
            this.cboTTSD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTTSD.FormattingEnabled = true;
            this.cboTTSD.Location = new System.Drawing.Point(108, 289);
            this.cboTTSD.Name = "cboTTSD";
            this.cboTTSD.Size = new System.Drawing.Size(373, 28);
            this.cboTTSD.TabIndex = 32;
            this.cboTTSD.TabStop = false;
            // 
            // lbTTSD
            // 
            this.lbTTSD.AutoSize = true;
            this.lbTTSD.Location = new System.Drawing.Point(108, 249);
            this.lbTTSD.Name = "lbTTSD";
            this.lbTTSD.Size = new System.Drawing.Size(141, 20);
            this.lbTTSD.TabIndex = 30;
            this.lbTTSD.Text = "Trạng thái sử dụng";
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.SystemColors.Window;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCreate.Location = new System.Drawing.Point(444, 417);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(109, 52);
            this.btnCreate.TabIndex = 33;
            this.btnCreate.TabStop = false;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // CreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 496);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cboTTSD);
            this.Controls.Add(this.lbTTSD);
            this.Controls.Add(this.txtDG);
            this.Controls.Add(this.lbDG);
            this.Controls.Add(this.txtMaLK);
            this.Controls.Add(this.lbMaLK);
            this.Name = "CreateForm";
            this.Load += new System.EventHandler(this.CreateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDG;
        private System.Windows.Forms.Label lbDG;
        private System.Windows.Forms.TextBox txtMaLK;
        private System.Windows.Forms.Label lbMaLK;
        private System.Windows.Forms.ComboBox cboTTSD;
        private System.Windows.Forms.Label lbTTSD;
        private System.Windows.Forms.Button btnCreate;
    }
}