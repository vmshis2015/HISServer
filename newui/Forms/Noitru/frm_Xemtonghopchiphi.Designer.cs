namespace VNS.HIS.UI.NOITRU
{
    partial class frm_Xemtonghopchiphi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Xemtonghopchiphi));
            Janus.Windows.GridEX.GridEXLayout grdThongTinChuaThanhToan_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel10 = new System.Windows.Forms.Panel();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.optNgoaitru = new System.Windows.Forms.RadioButton();
            this.optNoitru = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.optAll = new System.Windows.Forms.RadioButton();
            this.cmdRefresh = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.grdThongTinChuaThanhToan = new Janus.Windows.GridEX.GridEX();
            this.pnlCachthuchienthidulieu = new System.Windows.Forms.Panel();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdThongTinChuaThanhToan)).BeginInit();
            this.pnlCachthuchienthidulieu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.pnlCachthuchienthidulieu);
            this.panel10.Controls.Add(this.cmdPrint);
            this.panel10.Controls.Add(this.cmdRefresh);
            this.panel10.Controls.Add(this.cmdExit);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(0, 680);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1008, 50);
            this.panel10.TabIndex = 475;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button;
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdPrint.Location = new System.Drawing.Point(672, 12);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Custom;
            this.cmdPrint.Office2007CustomColor = System.Drawing.Color.WhiteSmoke;
            this.cmdPrint.Size = new System.Drawing.Size(107, 27);
            this.cmdPrint.TabIndex = 280;
            this.cmdPrint.Text = "In phiếu";
            // 
            // optNgoaitru
            // 
            this.optNgoaitru.AutoSize = true;
            this.optNgoaitru.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optNgoaitru.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.optNgoaitru.Location = new System.Drawing.Point(349, 17);
            this.optNgoaitru.Name = "optNgoaitru";
            this.optNgoaitru.Size = new System.Drawing.Size(76, 19);
            this.optNgoaitru.TabIndex = 279;
            this.optNgoaitru.Text = "Ngoại trú";
            this.optNgoaitru.UseVisualStyleBackColor = true;
            // 
            // optNoitru
            // 
            this.optNoitru.AutoSize = true;
            this.optNoitru.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optNoitru.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.optNoitru.Location = new System.Drawing.Point(250, 17);
            this.optNoitru.Name = "optNoitru";
            this.optNoitru.Size = new System.Drawing.Size(62, 19);
            this.optNoitru.TabIndex = 278;
            this.optNoitru.Text = "Nội trú";
            this.optNoitru.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 23);
            this.label7.TabIndex = 277;
            this.label7.Text = "Dữ liệu hiển thị";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // optAll
            // 
            this.optAll.AutoSize = true;
            this.optAll.Checked = true;
            this.optAll.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.optAll.Location = new System.Drawing.Point(146, 17);
            this.optAll.Name = "optAll";
            this.optAll.Size = new System.Drawing.Size(60, 19);
            this.optAll.TabIndex = 276;
            this.optAll.TabStop = true;
            this.optAll.Text = "Tất cả";
            this.optAll.UseVisualStyleBackColor = true;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRefresh.Enabled = false;
            this.cmdRefresh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRefresh.Image = ((System.Drawing.Image)(resources.GetObject("cmdRefresh.Image")));
            this.cmdRefresh.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdRefresh.Location = new System.Drawing.Point(785, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(107, 27);
            this.cmdRefresh.TabIndex = 18;
            this.cmdRefresh.Text = "Refresh(F5)";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(898, 12);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(107, 27);
            this.cmdExit.TabIndex = 21;
            this.cmdExit.Text = "Thoát";
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "noitru_tonghopchiphiravien";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1008, 30);
            this.baocaO_TIEUDE1.TabIndex = 477;
            this.baocaO_TIEUDE1.TIEUDE = "TỔNG HỢP CHI PHÍ RA VIỆN";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdThongTinChuaThanhToan
            // 
            this.grdThongTinChuaThanhToan.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grdThongTinChuaThanhToan.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><RecordNavigator>Số bả" +
    "n ghi:|/</RecordNavigator></LocalizableData>";
            this.grdThongTinChuaThanhToan.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            grdThongTinChuaThanhToan_DesignTimeLayout.LayoutString = resources.GetString("grdThongTinChuaThanhToan_DesignTimeLayout.LayoutString");
            this.grdThongTinChuaThanhToan.DesignTimeLayout = grdThongTinChuaThanhToan_DesignTimeLayout;
            this.grdThongTinChuaThanhToan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThongTinChuaThanhToan.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdThongTinChuaThanhToan.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdThongTinChuaThanhToan.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdThongTinChuaThanhToan.Font = new System.Drawing.Font("Arial", 9F);
            this.grdThongTinChuaThanhToan.FrozenColumns = 10;
            this.grdThongTinChuaThanhToan.GroupByBoxVisible = false;
            this.grdThongTinChuaThanhToan.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdThongTinChuaThanhToan.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdThongTinChuaThanhToan.GroupTotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdThongTinChuaThanhToan.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdThongTinChuaThanhToan.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdThongTinChuaThanhToan.Location = new System.Drawing.Point(0, 30);
            this.grdThongTinChuaThanhToan.Name = "grdThongTinChuaThanhToan";
            this.grdThongTinChuaThanhToan.RecordNavigator = true;
            this.grdThongTinChuaThanhToan.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThongTinChuaThanhToan.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdThongTinChuaThanhToan.Size = new System.Drawing.Size(1008, 650);
            this.grdThongTinChuaThanhToan.TabIndex = 478;
            this.grdThongTinChuaThanhToan.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.Default;
            this.grdThongTinChuaThanhToan.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThongTinChuaThanhToan.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdThongTinChuaThanhToan.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdThongTinChuaThanhToan.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdThongTinChuaThanhToan.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // pnlCachthuchienthidulieu
            // 
            this.pnlCachthuchienthidulieu.Controls.Add(this.label7);
            this.pnlCachthuchienthidulieu.Controls.Add(this.optAll);
            this.pnlCachthuchienthidulieu.Controls.Add(this.optNgoaitru);
            this.pnlCachthuchienthidulieu.Controls.Add(this.optNoitru);
            this.pnlCachthuchienthidulieu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCachthuchienthidulieu.Location = new System.Drawing.Point(0, 0);
            this.pnlCachthuchienthidulieu.Name = "pnlCachthuchienthidulieu";
            this.pnlCachthuchienthidulieu.Size = new System.Drawing.Size(434, 50);
            this.pnlCachthuchienthidulieu.TabIndex = 281;
            // 
            // frm_Xemtonghopchiphi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.grdThongTinChuaThanhToan);
            this.Controls.Add(this.baocaO_TIEUDE1);
            this.Controls.Add(this.panel10);
            this.KeyPreview = true;
            this.Name = "frm_Xemtonghopchiphi";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem thông tin tổng hợp chi phí Bệnh nhân";
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdThongTinChuaThanhToan)).EndInit();
            this.pnlCachthuchienthidulieu.ResumeLayout(false);
            this.pnlCachthuchienthidulieu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel10;
        private Janus.Windows.EditControls.UIButton cmdRefresh;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.RadioButton optNgoaitru;
        private System.Windows.Forms.RadioButton optNoitru;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton optAll;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private Janus.Windows.GridEX.GridEX grdThongTinChuaThanhToan;
        private System.Windows.Forms.Panel pnlCachthuchienthidulieu;
    }
}