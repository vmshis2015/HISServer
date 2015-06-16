namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_dmuc_diachinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_dmuc_diachinh));
            Janus.Windows.GridEX.GridEXLayout grdSurveys_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel5 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel6 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel7 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel8 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            this.sysColor = new System.Windows.Forms.ToolStrip();
            this.cmdNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cboLoaiDiachinh = new System.Windows.Forms.ComboBox();
            this.cboSurvery = new System.Windows.Forms.ComboBox();
            this.gridEXPrintDocument1 = new Janus.Windows.GridEX.GridEXPrintDocument();
            this.grdSurveys = new Janus.Windows.GridEX.GridEX();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.uiStatusBar2 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbldiachinh = new System.Windows.Forms.Label();
            this.cmdRefresh = new System.Windows.Forms.ToolStripButton();
            this.sysColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSurveys)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sysColor
            // 
            this.sysColor.BackColor = System.Drawing.SystemColors.Control;
            this.sysColor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sysColor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdNew,
            this.toolStripSeparator1,
            this.cmdEdit,
            this.toolStripSeparator2,
            this.cmdDelete,
            this.toolStripSeparator4,
            this.cmdPrint,
            this.cmdRefresh,
            this.toolStripButton1});
            this.sysColor.Location = new System.Drawing.Point(0, 0);
            this.sysColor.Name = "sysColor";
            this.sysColor.Size = new System.Drawing.Size(1008, 39);
            this.sysColor.TabIndex = 4;
            this.sysColor.Text = "toolStrip1";
            // 
            // cmdNew
            // 
            this.cmdNew.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdNew.Image")));
            this.cmdNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(144, 36);
            this.cmdNew.Text = "&Thêm mới (Ctrl+N)";
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(106, 36);
            this.cmdEdit.Text = "&Sửa(Ctrl+E)";
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(102, 36);
            this.cmdDelete.Text = "&Xoá(Ctrl+D)";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(141, 36);
            this.cmdPrint.Text = "&In danh sách(F4)";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(99, 36);
            this.toolStripButton1.Text = "Thoát(Esc)";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.uiGroupBox1.Controls.Add(this.lbldiachinh);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.cboLoaiDiachinh);
            this.uiGroupBox1.Controls.Add(this.cboSurvery);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 39);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 61);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "&Thông tin tìm kiếm";
            // 
            // cboLoaiDiachinh
            // 
            this.cboLoaiDiachinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiDiachinh.FormattingEnabled = true;
            this.cboLoaiDiachinh.Items.AddRange(new object[] {
            "Tất cả",
            "Tỉnh/Thành phố",
            "Quận/Huyện",
            "Xã/Phường"});
            this.cboLoaiDiachinh.Location = new System.Drawing.Point(112, 29);
            this.cboLoaiDiachinh.Name = "cboLoaiDiachinh";
            this.cboLoaiDiachinh.Size = new System.Drawing.Size(142, 24);
            this.cboLoaiDiachinh.TabIndex = 13;
            this.cboLoaiDiachinh.SelectedIndexChanged += new System.EventHandler(this.cboLoaiDiachinh_SelectedIndexChanged);
            // 
            // cboSurvery
            // 
            this.cboSurvery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSurvery.Enabled = false;
            this.cboSurvery.FormattingEnabled = true;
            this.cboSurvery.Location = new System.Drawing.Point(410, 29);
            this.cboSurvery.Name = "cboSurvery";
            this.cboSurvery.Size = new System.Drawing.Size(364, 24);
            this.cboSurvery.TabIndex = 0;
            // 
            // gridEXPrintDocument1
            // 
            this.gridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns;
            this.gridEXPrintDocument1.GridEX = this.grdSurveys;
            // 
            // grdSurveys
            // 
            this.grdSurveys.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdSurveys.ColumnAutoResize = true;
            grdSurveys_DesignTimeLayout.LayoutString = resources.GetString("grdSurveys_DesignTimeLayout.LayoutString");
            this.grdSurveys.DesignTimeLayout = grdSurveys_DesignTimeLayout;
            this.grdSurveys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSurveys.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdSurveys.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdSurveys.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdSurveys.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdSurveys.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSurveys.GroupByBoxVisible = false;
            this.grdSurveys.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdSurveys.Location = new System.Drawing.Point(3, 16);
            this.grdSurveys.Name = "grdSurveys";
            this.grdSurveys.RecordNavigator = true;
            this.grdSurveys.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdSurveys.Size = new System.Drawing.Size(1002, 588);
            this.grdSurveys.TabIndex = 13;
            this.grdSurveys.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.gridEXPrintDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // uiStatusBar2
            // 
            this.uiStatusBar2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.uiStatusBar2.Location = new System.Drawing.Point(3, 604);
            this.uiStatusBar2.Name = "uiStatusBar2";
            uiStatusBarPanel5.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel5.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel5.Key = "";
            uiStatusBarPanel5.ProgressBarValue = 0;
            uiStatusBarPanel5.Text = "Ctrl+N: thêm mới";
            uiStatusBarPanel5.Width = 117;
            uiStatusBarPanel6.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel6.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel6.Key = "";
            uiStatusBarPanel6.ProgressBarValue = 0;
            uiStatusBarPanel6.Text = "Ctrl+E: sửa thông tin ";
            uiStatusBarPanel6.Width = 142;
            uiStatusBarPanel7.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel7.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel7.Key = "";
            uiStatusBarPanel7.ProgressBarValue = 0;
            uiStatusBarPanel7.Text = "Ctrl+D: xóa thông tin ";
            uiStatusBarPanel7.Width = 140;
            uiStatusBarPanel8.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel8.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel8.Key = "";
            uiStatusBarPanel8.ProgressBarValue = 0;
            uiStatusBarPanel8.Text = "Esc:Thoát Form hiện tại";
            uiStatusBarPanel8.Width = 155;
            this.uiStatusBar2.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel5,
            uiStatusBarPanel6,
            uiStatusBarPanel7,
            uiStatusBarPanel8});
            this.uiStatusBar2.Size = new System.Drawing.Size(1002, 23);
            this.uiStatusBar2.TabIndex = 12;
            this.uiStatusBar2.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdSurveys);
            this.groupBox1.Controls.Add(this.uiStatusBar2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 630);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin  địa chính";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Loại địa chính";
            // 
            // lbldiachinh
            // 
            this.lbldiachinh.AutoSize = true;
            this.lbldiachinh.Location = new System.Drawing.Point(282, 32);
            this.lbldiachinh.Name = "lbldiachinh";
            this.lbldiachinh.Size = new System.Drawing.Size(117, 16);
            this.lbldiachinh.TabIndex = 15;
            this.lbldiachinh.Text = "Địa chính cấp trên:";
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Font = new System.Drawing.Font("Arial", 9.75F);
            this.cmdRefresh.Image = ((System.Drawing.Image)(resources.GetObject("cmdRefresh.Image")));
            this.cmdRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(88, 36);
            this.cmdRefresh.Text = "Refresh";
            // 
            // frm_dmuc_diachinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.sysColor);
            this.KeyPreview = true;
            this.Name = "frm_dmuc_diachinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin địa chính";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_dmuc_diachinh_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_dmuc_diachinh_KeyDown);
            this.sysColor.ResumeLayout(false);
            this.sysColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSurveys)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip sysColor;
        private System.Windows.Forms.ToolStripButton cmdNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.ComboBox cboSurvery;
        private System.Windows.Forms.ToolStripButton cmdPrint;
        private Janus.Windows.GridEX.GridEXPrintDocument gridEXPrintDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar2;
        private System.Windows.Forms.GroupBox groupBox1;
        private Janus.Windows.GridEX.GridEX grdSurveys;
        private System.Windows.Forms.ComboBox cboLoaiDiachinh;
        private System.Windows.Forms.Label lbldiachinh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton cmdRefresh;
    }
}