namespace Lab15_SoftwareQuality
{
    partial class Form1
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
            this.cboKhoa = new System.Windows.Forms.ComboBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnXemTheoDiem = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cboKhoa
            // 
            this.cboKhoa.FormattingEnabled = true;
            this.cboKhoa.Location = new System.Drawing.Point(179, 12);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.Size = new System.Drawing.Size(239, 24);
            this.cboKhoa.TabIndex = 0;
            this.cboKhoa.SelectedIndexChanged += new System.EventHandler(this.Form1_Load);
            this.cboKhoa.Click += new System.EventHandler(this.Form1_Load);
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(179, 61);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(100, 22);
            this.txtMin.TabIndex = 1;
            this.txtMin.Click += new System.EventHandler(this.Form1_Load);
            this.txtMin.TextChanged += new System.EventHandler(this.Form1_Load);
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(318, 61);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(100, 22);
            this.txtMax.TabIndex = 2;
            this.txtMax.Click += new System.EventHandler(this.Form1_Load);
            this.txtMax.TextChanged += new System.EventHandler(this.Form1_Load);
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(28, 584);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(157, 23);
            this.btnXem.TabIndex = 3;
            this.btnXem.Text = "Xem theo Khoa";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.TextChanged += new System.EventHandler(this.btnXem_Click);
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnXemTheoDiem
            // 
            this.btnXemTheoDiem.Location = new System.Drawing.Point(279, 584);
            this.btnXemTheoDiem.Name = "btnXemTheoDiem";
            this.btnXemTheoDiem.Size = new System.Drawing.Size(139, 23);
            this.btnXemTheoDiem.TabIndex = 4;
            this.btnXemTheoDiem.Text = "Xem theo Điểm";
            this.btnXemTheoDiem.UseVisualStyleBackColor = true;
            this.btnXemTheoDiem.TextChanged += new System.EventHandler(this.btnXemTheoDiem_Click);
            this.btnXemTheoDiem.Click += new System.EventHandler(this.btnXemTheoDiem_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Location = new System.Drawing.Point(528, 584);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(115, 23);
            this.btnXuatExcel.TabIndex = 5;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = true;
            this.btnXuatExcel.TextChanged += new System.EventHandler(this.btnXuatExcel_Click);
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(28, 114);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(999, 414);
            this.crystalReportViewer1.TabIndex = 6;
            this.crystalReportViewer1.Load += new System.EventHandler(this.Form1_Load);
            this.crystalReportViewer1.Click += new System.EventHandler(this.Form1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 665);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnXemTheoDiem);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.txtMax);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.cboKhoa);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboKhoa;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnXemTheoDiem;
        private System.Windows.Forms.Button btnXuatExcel;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
    }
}

