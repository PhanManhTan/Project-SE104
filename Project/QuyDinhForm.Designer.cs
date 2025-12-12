namespace Project
{
    partial class QuyDinhForm
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
            this.grpQuyDinh = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numHeSo = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numPhuThu = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numMaxGuest = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpQuyDinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeSo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhuThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxGuest)).BeginInit();
            this.SuspendLayout();
            // 
            // grpQuyDinh
            // 
            this.grpQuyDinh.Controls.Add(this.btnSave);
            this.grpQuyDinh.Controls.Add(this.btnClose);
            this.grpQuyDinh.Controls.Add(this.label4);
            this.grpQuyDinh.Controls.Add(this.label3);
            this.grpQuyDinh.Controls.Add(this.numHeSo);
            this.grpQuyDinh.Controls.Add(this.label2);
            this.grpQuyDinh.Controls.Add(this.numPhuThu);
            this.grpQuyDinh.Controls.Add(this.label1);
            this.grpQuyDinh.Controls.Add(this.numMaxGuest);
            this.grpQuyDinh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuyDinh.Location = new System.Drawing.Point(12, 1);
            this.grpQuyDinh.Name = "grpQuyDinh";
            this.grpQuyDinh.Size = new System.Drawing.Size(1153, 694);
            this.grpQuyDinh.TabIndex = 0;
            this.grpQuyDinh.TabStop = false;
            this.grpQuyDinh.Text = "Thông tin quy định";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(633, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 28);
            this.label4.TabIndex = 6;
            this.label4.Text = "%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hệ số khách nước ngoài:";
            // 
            // numHeSo
            // 
            this.numHeSo.DecimalPlaces = 2;
            this.numHeSo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numHeSo.Location = new System.Drawing.Point(527, 286);
            this.numHeSo.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numHeSo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHeSo.Name = "numHeSo";
            this.numHeSo.Size = new System.Drawing.Size(100, 37);
            this.numHeSo.TabIndex = 4;
            this.numHeSo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numHeSo.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tỷ lệ phụ thu (%):";
            // 
            // numPhuThu
            // 
            this.numPhuThu.Location = new System.Drawing.Point(527, 193);
            this.numPhuThu.Name = "numPhuThu";
            this.numPhuThu.Size = new System.Drawing.Size(100, 37);
            this.numPhuThu.TabIndex = 2;
            this.numPhuThu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Số khách tối đa trong phòng:";
            // 
            // numMaxGuest
            // 
            this.numMaxGuest.Location = new System.Drawing.Point(527, 111);
            this.numMaxGuest.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numMaxGuest.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxGuest.Name = "numMaxGuest";
            this.numMaxGuest.Size = new System.Drawing.Size(100, 37);
            this.numMaxGuest.TabIndex = 0;
            this.numMaxGuest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMaxGuest.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(179, 374);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(208, 45);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu Quy Định";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(468, 374);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 45);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Thoát";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // QuyDinhForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1169, 700);
            this.Controls.Add(this.grpQuyDinh);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuyDinhForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thay Đổi Quy Định";
            this.Load += new System.EventHandler(this.QuyDinhForm_Load);
            this.grpQuyDinh.ResumeLayout(false);
            this.grpQuyDinh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeSo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhuThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxGuest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpQuyDinh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numMaxGuest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numHeSo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numPhuThu;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label4;
    }
}