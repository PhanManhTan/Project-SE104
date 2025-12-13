using System.Windows.Forms;

namespace Project
{
    partial class DetailRoom
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
            this.label1 = new System.Windows.Forms.Label(); // Mã phòng
            this.label2 = new System.Windows.Forms.Label(); // Loại phòng
            this.label3 = new System.Windows.Forms.Label(); // Tình trạng
            this.label4 = new System.Windows.Forms.Label(); // Ghi chú
            this.label5 = new System.Windows.Forms.Label(); // Đơn giá
            this.tbMaPhong = new System.Windows.Forms.TextBox();
            this.cbTypeRoom = new System.Windows.Forms.ComboBox();
            this.cbTinhTrang = new System.Windows.Forms.ComboBox(); // Thay TextBox bằng ComboBox
            this.tbDonGia = new System.Windows.Forms.TextBox();     // Đơn giá bình thường
            this.tbNote = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            // btnDone
            this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDone.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDone.ForeColor = System.Drawing.Color.White;
            this.btnDone.Location = new System.Drawing.Point(331, 420);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(166, 60);
            this.btnDone.TabIndex = 17;
            this.btnDone.Text = "Xong";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);

            // btnClose
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(158, 420);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 60);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Thoát";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // label1 - Mã phòng
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Text = "Mã phòng";

            // label2 - Loại phòng
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(12, 90);
            this.label2.Name = "label2";
            this.label2.Text = "Loại phòng";

            // label3 - Tình trạng
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(12, 150);
            this.label3.Name = "label3";
            this.label3.Text = "Tình trạng";

            // label4 - Ghi chú
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(12, 210);
            this.label4.Name = "label4";
            this.label4.Text = "Ghi chú";

            // label5 - Đơn giá
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(12, 360);
            this.label5.Name = "label5";
            this.label5.Text = "Đơn giá (đ)";

            // tbMaPhong
            this.tbMaPhong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.tbMaPhong.Location = new System.Drawing.Point(130, 25);
            this.tbMaPhong.Name = "tbMaPhong";
            this.tbMaPhong.Size = new System.Drawing.Size(367, 34);
            this.tbMaPhong.TabIndex = 0;

            // cbTypeRoom
            this.cbTypeRoom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbTypeRoom.Location = new System.Drawing.Point(130, 85);
            this.cbTypeRoom.Name = "cbTypeRoom";
            this.cbTypeRoom.Size = new System.Drawing.Size(367, 30);
            this.cbTypeRoom.TabIndex = 1;
            this.cbTypeRoom.SelectedIndexChanged += new System.EventHandler(this.cbTypeRoom_SelectedIndexChanged);

            // cbTinhTrang (ComboBox, disable)
            this.cbTinhTrang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbTinhTrang.Location = new System.Drawing.Point(130, 145);
            this.cbTinhTrang.Name = "cbTinhTrang";
            this.cbTinhTrang.Enabled = false; // Disable
            this.cbTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbTinhTrang.Size = new System.Drawing.Size(367, 30);
            this.cbTinhTrang.TabIndex = 2;

            // tbDonGia (TextBox bình thường, disable)
            this.tbDonGia.Font = new System.Drawing.Font("Segoe UI", 10F); // Chữ bình thường
            this.tbDonGia.ForeColor = System.Drawing.Color.Black;
            this.tbDonGia.Location = new System.Drawing.Point(130, 355);
            this.tbDonGia.Name = "tbDonGia";
            this.tbDonGia.ReadOnly = true;
            this.tbDonGia.BackColor = System.Drawing.SystemColors.Window;
            this.tbDonGia.Size = new System.Drawing.Size(367, 30);
            this.tbDonGia.TabIndex = 3;
            this.tbDonGia.TextAlign = HorizontalAlignment.Right;

            // tbNote
            this.tbNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbNote.Location = new System.Drawing.Point(130, 210);
            this.tbNote.Multiline = true;
            this.tbNote.Name = "tbNote";
            this.tbNote.Size = new System.Drawing.Size(367, 130);
            this.tbNote.TabIndex = 4;

            // DetailRoom
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 500);
            this.Controls.Add(this.tbDonGia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbTinhTrang); // Thay tbTinhTrang
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