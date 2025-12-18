using System.Windows.Forms;

namespace Project
{
    partial class RoomDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnDone = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbMaPhong = new System.Windows.Forms.TextBox();
            this.cbTypeRoom = new System.Windows.Forms.ComboBox();
            this.cbTinhTrang = new System.Windows.Forms.ComboBox();
            this.tbDonGia = new System.Windows.Forms.TextBox();
            this.tbNote = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDone.ForeColor = System.Drawing.Color.White;
            this.btnDone.Location = new System.Drawing.Point(372, 420);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(187, 60);
            this.btnDone.TabIndex = 17;
            this.btnDone.Text = "Xong";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(178, 420);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(187, 60);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Thoát";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(14, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 28);
            this.label1.TabIndex = 8;
            this.label1.Text = "Mã phòng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(14, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 28);
            this.label2.TabIndex = 7;
            this.label2.Text = "Loại phòng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(14, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tình trạng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(14, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 28);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ghi chú";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(14, 360);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 28);
            this.label5.TabIndex = 4;
            this.label5.Text = "Đơn giá";
            // 
            // tbMaPhong
            // 
            this.tbMaPhong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tbMaPhong.Location = new System.Drawing.Point(146, 30);
            this.tbMaPhong.Name = "tbMaPhong";
            this.tbMaPhong.Size = new System.Drawing.Size(412, 34);
            this.tbMaPhong.TabIndex = 0;
            // 
            // cbTypeRoom
            // 
            this.cbTypeRoom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cbTypeRoom.Location = new System.Drawing.Point(146, 90);
            this.cbTypeRoom.Name = "cbTypeRoom";
            this.cbTypeRoom.Size = new System.Drawing.Size(412, 36);
            this.cbTypeRoom.TabIndex = 1;
            this.cbTypeRoom.SelectedIndexChanged += new System.EventHandler(this.cbTypeRoom_SelectedIndexChanged);
            // 
            // cbTinhTrang
            // 
            this.cbTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTinhTrang.Enabled = false;
            this.cbTinhTrang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cbTinhTrang.Location = new System.Drawing.Point(146, 150);
            this.cbTinhTrang.Name = "cbTinhTrang";
            this.cbTinhTrang.Size = new System.Drawing.Size(412, 36);
            this.cbTinhTrang.TabIndex = 2;
            // 
            // tbDonGia
            // 
            this.tbDonGia.BackColor = System.Drawing.SystemColors.Window;
            this.tbDonGia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tbDonGia.ForeColor = System.Drawing.Color.Black;
            this.tbDonGia.Location = new System.Drawing.Point(146, 360);
            this.tbDonGia.Name = "tbDonGia";
            this.tbDonGia.ReadOnly = true;
            this.tbDonGia.Size = new System.Drawing.Size(412, 34);
            this.tbDonGia.TabIndex = 3;
            this.tbDonGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbNote
            // 
            this.tbNote.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tbNote.Location = new System.Drawing.Point(146, 210);
            this.tbNote.Multiline = true;
            this.tbNote.Name = "tbNote";
            this.tbNote.Size = new System.Drawing.Size(412, 130);
            this.tbNote.TabIndex = 4;
            // 
            // DetailRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 500);
            this.Controls.Add(this.tbDonGia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbTinhTrang);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbNote);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbTypeRoom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMaPhong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDone);
            this.Name = "DetailRoom";
            this.Text = "Chi tiết phòng";
            this.Load += new System.EventHandler(this.DetailRoom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbMaPhong;
        private System.Windows.Forms.ComboBox cbTypeRoom;
        private System.Windows.Forms.ComboBox cbTinhTrang; // Thay đổi
        private System.Windows.Forms.TextBox tbDonGia;
        private System.Windows.Forms.TextBox tbNote;
    }
}