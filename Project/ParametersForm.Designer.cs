namespace Project
{
    partial class ParametersForm
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
            this.numMaxGuest = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numPhuThu = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numHeSo = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDone = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxGuest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhuThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeSo)).BeginInit();
            this.SuspendLayout();
            // 
            // numMaxGuest
            // 
            this.numMaxGuest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxGuest.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numMaxGuest.Location = new System.Drawing.Point(629, 173);
            this.numMaxGuest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.numMaxGuest.Size = new System.Drawing.Size(135, 37);
            this.numMaxGuest.TabIndex = 1;
            this.numMaxGuest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMaxGuest.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label1.Location = new System.Drawing.Point(228, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Số khách tối đa trong phòng:";
            // 
            // numPhuThu
            // 
            this.numPhuThu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numPhuThu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numPhuThu.Location = new System.Drawing.Point(629, 260);
            this.numPhuThu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numPhuThu.Name = "numPhuThu";
            this.numPhuThu.Size = new System.Drawing.Size(135, 37);
            this.numPhuThu.TabIndex = 3;
            this.numPhuThu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label2.Location = new System.Drawing.Point(228, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tỷ lệ phụ thu (%):";
            // 
            // numHeSo
            // 
            this.numHeSo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numHeSo.DecimalPlaces = 2;
            this.numHeSo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numHeSo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numHeSo.Location = new System.Drawing.Point(629, 347);
            this.numHeSo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.numHeSo.Size = new System.Drawing.Size(135, 37);
            this.numHeSo.TabIndex = 5;
            this.numHeSo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numHeSo.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label3.Location = new System.Drawing.Point(228, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 30);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hệ số khách nước ngoài:";
            // 
            // btnDone
            // 
            this.btnDone.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDone.ForeColor = System.Drawing.Color.White;
            this.btnDone.Location = new System.Drawing.Point(406, 504);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(200, 70);
            this.btnDone.TabIndex = 21;
            this.btnDone.Text = "Lưu";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Parameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1012, 750);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numHeSo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numPhuThu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numMaxGuest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1012, 750);
            this.Name = "Parameter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thay Đổi Quy Định";
            this.Load += new System.EventHandler(this.Parameter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxGuest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhuThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeSo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numMaxGuest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numPhuThu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numHeSo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDone;
    }
}