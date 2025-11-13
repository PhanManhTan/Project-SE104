namespace FormPhong
{
    partial class UpdatePhongForm
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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cboLoaiPhong = new System.Windows.Forms.ComboBox();
            this.cboTinhTrang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTinhTrang = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtDG = new System.Windows.Forms.TextBox();
            this.lbDG = new System.Windows.Forms.Label();
            this.txtMaPhong = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbMaLK = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.Window;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpdate.Location = new System.Drawing.Point(501, 384);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(97, 42);
            this.btnUpdate.TabIndex = 51;
            this.btnUpdate.TabStop = false;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            // 
            // cboLoaiPhong
            // 
            this.cboLoaiPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiPhong.FormattingEnabled = true;
            this.cboLoaiPhong.Location = new System.Drawing.Point(202, 129);
            this.cboLoaiPhong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboLoaiPhong.Name = "cboLoaiPhong";
            this.cboLoaiPhong.Size = new System.Drawing.Size(332, 24);
            this.cboLoaiPhong.TabIndex = 49;
            this.cboLoaiPhong.TabStop = false;
            // 
            // cboTinhTrang
            // 
            this.cboTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTinhTrang.FormattingEnabled = true;
            this.cboTinhTrang.Location = new System.Drawing.Point(202, 281);
            this.cboTinhTrang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboTinhTrang.Name = "cboTinhTrang";
            this.cboTinhTrang.Size = new System.Drawing.Size(332, 24);
            this.cboTinhTrang.TabIndex = 50;
            this.cboTinhTrang.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 47;
            this.label1.Text = "Ghi chú";
            // 
            // lbTinhTrang
            // 
            this.lbTinhTrang.AutoSize = true;
            this.lbTinhTrang.Location = new System.Drawing.Point(202, 249);
            this.lbTinhTrang.Name = "lbTinhTrang";
            this.lbTinhTrang.Size = new System.Drawing.Size(107, 16);
            this.lbTinhTrang.TabIndex = 48;
            this.lbTinhTrang.Text = "Tình trạng phòng";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(202, 335);
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(332, 27);
            this.txtGhiChu.TabIndex = 45;
            this.txtGhiChu.TabStop = false;
            // 
            // txtDG
            // 
            this.txtDG.Location = new System.Drawing.Point(202, 207);
            this.txtDG.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDG.Multiline = true;
            this.txtDG.Name = "txtDG";
            this.txtDG.Size = new System.Drawing.Size(332, 27);
            this.txtDG.TabIndex = 46;
            this.txtDG.TabStop = false;
            // 
            // lbDG
            // 
            this.lbDG.AutoSize = true;
            this.lbDG.Location = new System.Drawing.Point(202, 175);
            this.lbDG.Name = "lbDG";
            this.lbDG.Size = new System.Drawing.Size(53, 16);
            this.lbDG.TabIndex = 44;
            this.lbDG.Text = "Đơn giá";
            // 
            // txtMaPhong
            // 
            this.txtMaPhong.Location = new System.Drawing.Point(202, 61);
            this.txtMaPhong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMaPhong.Multiline = true;
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.Size = new System.Drawing.Size(332, 27);
            this.txtMaPhong.TabIndex = 43;
            this.txtMaPhong.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 41;
            this.label2.Text = "Mã phòng";
            // 
            // lbMaLK
            // 
            this.lbMaLK.AutoSize = true;
            this.lbMaLK.Location = new System.Drawing.Point(202, 100);
            this.lbMaLK.Name = "lbMaLK";
            this.lbMaLK.Size = new System.Drawing.Size(92, 16);
            this.lbMaLK.TabIndex = 42;
            this.lbMaLK.Text = "Mã loại phòng";
            // 
            // UpdatePhongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.cboLoaiPhong);
            this.Controls.Add(this.cboTinhTrang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbTinhTrang);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.txtDG);
            this.Controls.Add(this.lbDG);
            this.Controls.Add(this.txtMaPhong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbMaLK);
            this.Name = "UpdatePhongForm";
            this.Text = "UpdatePhongForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cboLoaiPhong;
        private System.Windows.Forms.ComboBox cboTinhTrang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTinhTrang;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.TextBox txtDG;
        private System.Windows.Forms.Label lbDG;
        private System.Windows.Forms.TextBox txtMaPhong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbMaLK;
    }
}