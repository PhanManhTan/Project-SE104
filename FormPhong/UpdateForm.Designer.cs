namespace FormPhong
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
            this.txtMaLP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbMaLK
            // 
            this.lbMaLK.AutoSize = true;
            this.lbMaLK.Location = new System.Drawing.Point(90, -21);
            this.lbMaLK.Name = "lbMaLK";
            this.lbMaLK.Size = new System.Drawing.Size(92, 16);
            this.lbMaLK.TabIndex = 34;
            this.lbMaLK.Text = "Mã loại phòng";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.Window;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpdate.Location = new System.Drawing.Point(335, 301);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(97, 42);
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
            this.cboTTSD.Location = new System.Drawing.Point(36, 198);
            this.cboTTSD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboTTSD.Name = "cboTTSD";
            this.cboTTSD.Size = new System.Drawing.Size(332, 24);
            this.cboTTSD.TabIndex = 40;
            this.cboTTSD.TabStop = false;
            // 
            // lbTTSD
            // 
            this.lbTTSD.AutoSize = true;
            this.lbTTSD.Location = new System.Drawing.Point(36, 166);
            this.lbTTSD.Name = "lbTTSD";
            this.lbTTSD.Size = new System.Drawing.Size(117, 16);
            this.lbTTSD.TabIndex = 39;
            this.lbTTSD.Text = "Trạng thái sử dụng";
            // 
            // txtDG
            // 
            this.txtDG.Location = new System.Drawing.Point(36, 124);
            this.txtDG.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDG.Multiline = true;
            this.txtDG.Name = "txtDG";
            this.txtDG.Size = new System.Drawing.Size(332, 27);
            this.txtDG.TabIndex = 38;
            this.txtDG.TabStop = false;
            // 
            // lbDG
            // 
            this.lbDG.AutoSize = true;
            this.lbDG.Location = new System.Drawing.Point(36, 92);
            this.lbDG.Name = "lbDG";
            this.lbDG.Size = new System.Drawing.Size(53, 16);
            this.lbDG.TabIndex = 37;
            this.lbDG.Text = "Đơn giá";
            // 
            // txtMaLP
            // 
            this.txtMaLP.Location = new System.Drawing.Point(36, 50);
            this.txtMaLP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMaLP.Multiline = true;
            this.txtMaLP.Name = "txtMaLP";
            this.txtMaLP.Size = new System.Drawing.Size(332, 27);
            this.txtMaLP.TabIndex = 36;
            this.txtMaLP.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "Mã loại phòng";
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 360);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.cboTTSD);
            this.Controls.Add(this.lbTTSD);
            this.Controls.Add(this.txtDG);
            this.Controls.Add(this.lbDG);
            this.Controls.Add(this.txtMaLP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbMaLK);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.TextBox txtMaLP;
        private System.Windows.Forms.Label label1;
    }
}