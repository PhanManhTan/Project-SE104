namespace Project
{
    partial class RoomTypeUpdate
    {
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtMaLP = new System.Windows.Forms.TextBox();
            this.txtTenLP = new System.Windows.Forms.TextBox();
            this.txtDG = new System.Windows.Forms.TextBox();
            this.lbMaLP = new System.Windows.Forms.Label();
            this.lbTenLP = new System.Windows.Forms.Label();
            this.lbDG = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMaLP
            // 
            this.txtMaLP.Location = new System.Drawing.Point(150, 50);
            this.txtMaLP.Name = "txtMaLP";
            this.txtMaLP.Size = new System.Drawing.Size(300, 27);
            this.txtMaLP.TabIndex = 0;
            // 
            // txtTenLP
            // 
            this.txtTenLP.Location = new System.Drawing.Point(150, 100);
            this.txtTenLP.Name = "txtTenLP";
            this.txtTenLP.Size = new System.Drawing.Size(300, 27);
            this.txtTenLP.TabIndex = 1;
            // 
            // txtDG
            // 
            this.txtDG.Location = new System.Drawing.Point(150, 150);
            this.txtDG.Name = "txtDG";
            this.txtDG.Size = new System.Drawing.Size(300, 27);
            this.txtDG.TabIndex = 2;
            // 
            // lbMaLP
            // 
            this.lbMaLP.AutoSize = true;
            this.lbMaLP.Location = new System.Drawing.Point(50, 53);
            this.lbMaLP.Name = "lbMaLP";
            this.lbMaLP.Text = "Mã loại phòng";
            // 
            // lbTenLP
            // 
            this.lbTenLP.AutoSize = true;
            this.lbTenLP.Location = new System.Drawing.Point(50, 103);
            this.lbTenLP.Name = "lbTenLP";
            this.lbTenLP.Text = "Tên loại phòng";
            // 
            // lbDG
            // 
            this.lbDG.AutoSize = true;
            this.lbDG.Location = new System.Drawing.Point(50, 153);
            this.lbDG.Name = "lbDG";
            this.lbDG.Text = "Đơn giá";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(150, 220);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 40);
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(330, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // RoomTypeUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 321);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lbDG);
            this.Controls.Add(this.lbTenLP);
            this.Controls.Add(this.lbMaLP);
            this.Controls.Add(this.txtDG);
            this.Controls.Add(this.txtTenLP);
            this.Controls.Add(this.txtMaLP);
            this.Name = "RoomTypeUpdate";
            this.Text = "Cập nhật loại phòng";
            this.Load += new System.EventHandler(this.RoomTypes_UpdateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtMaLP;
        private System.Windows.Forms.TextBox txtTenLP;
        private System.Windows.Forms.TextBox txtDG;
        private System.Windows.Forms.Label lbMaLP;
        private System.Windows.Forms.Label lbTenLP;
        private System.Windows.Forms.Label lbDG;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
    }
}