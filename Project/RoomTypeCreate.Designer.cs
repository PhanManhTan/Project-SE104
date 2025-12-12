namespace Project
{
    partial class RoomTypeCreate
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
            this.txtMaLP = new System.Windows.Forms.TextBox();
            this.txtTenLP = new System.Windows.Forms.TextBox();
            this.txtDG = new System.Windows.Forms.TextBox();
            this.lbMaLP = new System.Windows.Forms.Label();
            this.lbTenLP = new System.Windows.Forms.Label();
            this.lbDG = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMaLP
            // 
            this.txtMaLP.Location = new System.Drawing.Point(150, 50);
            this.txtMaLP.Name = "txtMaLP";
            this.txtMaLP.Size = new System.Drawing.Size(300, 22);
            this.txtMaLP.TabIndex = 0;
            // 
            // txtTenLP
            // 
            this.txtTenLP.Location = new System.Drawing.Point(150, 100);
            this.txtTenLP.Name = "txtTenLP";
            this.txtTenLP.Size = new System.Drawing.Size(300, 22);
            this.txtTenLP.TabIndex = 1;
            // 
            // txtDG
            // 
            this.txtDG.Location = new System.Drawing.Point(150, 150);
            this.txtDG.Name = "txtDG";
            this.txtDG.Size = new System.Drawing.Size(300, 22);
            this.txtDG.TabIndex = 2;
            // 
            // lbMaLP
            // 
            this.lbMaLP.AutoSize = true;
            this.lbMaLP.Location = new System.Drawing.Point(50, 53);
            this.lbMaLP.Name = "lbMaLP";
            this.lbMaLP.Size = new System.Drawing.Size(92, 16);
            this.lbMaLP.TabIndex = 3;
            this.lbMaLP.Text = "Mã loại phòng";
            // 
            // lbTenLP
            // 
            this.lbTenLP.AutoSize = true;
            this.lbTenLP.Location = new System.Drawing.Point(50, 103);
            this.lbTenLP.Name = "lbTenLP";
            this.lbTenLP.Size = new System.Drawing.Size(97, 16);
            this.lbTenLP.TabIndex = 4;
            this.lbTenLP.Text = "Tên loại phòng";
            // 
            // lbDG
            // 
            this.lbDG.AutoSize = true;
            this.lbDG.Location = new System.Drawing.Point(50, 153);
            this.lbDG.Name = "lbDG";
            this.lbDG.Size = new System.Drawing.Size(53, 16);
            this.lbDG.TabIndex = 5;
            this.lbDG.Text = "Đơn giá";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(150, 220);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(120, 40);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Thêm mới";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(330, 220);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 40);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Xóa trắng";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // RoomTypes_CreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 321);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lbDG);
            this.Controls.Add(this.lbTenLP);
            this.Controls.Add(this.lbMaLP);
            this.Controls.Add(this.txtDG);
            this.Controls.Add(this.txtTenLP);
            this.Controls.Add(this.txtMaLP);
            this.Name = "RoomTypes_CreateForm";
            this.Text = "Thêm loại phòng mới";
            this.Load += new System.EventHandler(this.RoomTypes_CreateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMaLP;
        private System.Windows.Forms.TextBox txtTenLP;
        private System.Windows.Forms.TextBox txtDG;
        private System.Windows.Forms.Label lbMaLP;
        private System.Windows.Forms.Label lbTenLP;
        private System.Windows.Forms.Label lbDG;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnClear;
    }
}