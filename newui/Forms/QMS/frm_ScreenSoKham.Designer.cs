namespace VNS.UI.QMS
{
    partial class frm_ScreenSoKham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ScreenSoKham));
            this.grpThongTin = new Janus.Windows.EditControls.UIGroupBox();
            this.lblQuaySo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTenDonVi = new System.Windows.Forms.Label();
            this.cmdConfig = new System.Windows.Forms.Button();
            this.lblSoKham = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTin)).BeginInit();
            this.grpThongTin.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpThongTin
            // 
            this.grpThongTin.BackColor = System.Drawing.Color.Black;
            this.grpThongTin.BorderColor = System.Drawing.Color.Black;
            this.grpThongTin.Controls.Add(this.lblQuaySo);
            this.grpThongTin.Controls.Add(this.panel1);
            this.grpThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpThongTin.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTin.ForeColor = System.Drawing.Color.Red;
            this.grpThongTin.FormatStyle.BackColor = System.Drawing.Color.Black;
            this.grpThongTin.FormatStyle.Font = new System.Drawing.Font("Times New Roman", 55F, System.Drawing.FontStyle.Bold);
            this.grpThongTin.FormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.grpThongTin.FormatStyle.ForeColor = System.Drawing.Color.Yellow;
            this.grpThongTin.FrameStyle = Janus.Windows.EditControls.FrameStyle.None;
            this.grpThongTin.Location = new System.Drawing.Point(0, 0);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(732, 611);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.Text = "Thông tin số khám";
            this.grpThongTin.TextAlignment = Janus.Windows.EditControls.TextAlignment.Center;
            this.grpThongTin.UseCompatibleTextRendering = true;
            this.grpThongTin.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.VS2005;
            // 
            // lblQuaySo
            // 
            this.lblQuaySo.AutoSize = true;
            this.lblQuaySo.BackColor = System.Drawing.Color.Black;
            this.lblQuaySo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblQuaySo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuaySo.ForeColor = System.Drawing.Color.White;
            this.lblQuaySo.Location = new System.Drawing.Point(0, 88);
            this.lblQuaySo.Name = "lblQuaySo";
            this.lblQuaySo.Size = new System.Drawing.Size(99, 24);
            this.lblQuaySo.TabIndex = 3;
            this.lblQuaySo.Text = "Quầy số :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.lblTenDonVi);
            this.panel1.Controls.Add(this.cmdConfig);
            this.panel1.Controls.Add(this.lblSoKham);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(732, 520);
            this.panel1.TabIndex = 1;
            // 
            // lblTenDonVi
            // 
            this.lblTenDonVi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTenDonVi.AutoSize = true;
            this.lblTenDonVi.BackColor = System.Drawing.Color.Black;
            this.lblTenDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTenDonVi.ForeColor = System.Drawing.Color.White;
            this.lblTenDonVi.Location = new System.Drawing.Point(3, 494);
            this.lblTenDonVi.Name = "lblTenDonVi";
            this.lblTenDonVi.Size = new System.Drawing.Size(357, 24);
            this.lblTenDonVi.TabIndex = 2;
            this.lblTenDonVi.Text = "BỆNH VIỆN NỘI TIẾT TRUNG ƯƠNG";
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdConfig.BackgroundImage")));
            this.cmdConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdConfig.Location = new System.Drawing.Point(690, 481);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(40, 40);
            this.cmdConfig.TabIndex = 1;
            this.cmdConfig.UseVisualStyleBackColor = true;
            // 
            // lblSoKham
            // 
            this.lblSoKham.BackColor = System.Drawing.Color.Black;
            this.lblSoKham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoKham.Font = new System.Drawing.Font("Times New Roman", 300F, System.Drawing.FontStyle.Bold);
            this.lblSoKham.ForeColor = System.Drawing.Color.Red;
            this.lblSoKham.Location = new System.Drawing.Point(0, 0);
            this.lblSoKham.Name = "lblSoKham";
            this.lblSoKham.Size = new System.Drawing.Size(732, 520);
            this.lblSoKham.TabIndex = 0;
            this.lblSoKham.Text = "1";
            this.lblSoKham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_ScreenSoKham
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(732, 611);
            this.Controls.Add(this.grpThongTin);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ScreenSoKham";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quầy số";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_ScreenSoKham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTin)).EndInit();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox grpThongTin;
        private System.Windows.Forms.Label lblQuaySo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTenDonVi;
        private System.Windows.Forms.Button cmdConfig;
        private System.Windows.Forms.Label lblSoKham;

    }
}