namespace TestFormLoaiPhong
{
    partial class QuyDinh
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
            this.dataGridView_quydinh = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_quydinh)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_quydinh
            // 
            this.dataGridView_quydinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_quydinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_quydinh.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_quydinh.Name = "dataGridView_quydinh";
            this.dataGridView_quydinh.RowHeadersWidth = 51;
            this.dataGridView_quydinh.RowTemplate.Height = 24;
            this.dataGridView_quydinh.Size = new System.Drawing.Size(931, 536);
            this.dataGridView_quydinh.TabIndex = 0;
            // 
            // QuyDinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 536);
            this.Controls.Add(this.dataGridView_quydinh);
            this.Name = "QuyDinh";
            this.Text = "QuyDinh";
            this.Load += new System.EventHandler(this.QuyDinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_quydinh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_quydinh;
    }
}