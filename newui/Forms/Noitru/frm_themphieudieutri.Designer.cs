namespace VNS.HIS.UI.NOITRU
{
    partial class frm_themphieudieutri
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themphieudieutri));
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel2 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            this.grpThongTinDieuTri = new Janus.Windows.EditControls.UIGroupBox();
            this.txtDieuduongtheodoi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtBstheodoi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.chkPhieuBoSung = new Janus.Windows.EditControls.UICheckBox();
            this.dtNgayLapPhieu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboBacSy = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboKhoaNoiTru = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtGioLapPhieu = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTreat_ID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTinDieuTri)).BeginInit();
            this.grpThongTinDieuTri.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpThongTinDieuTri
            // 
            resources.ApplyResources(this.grpThongTinDieuTri, "grpThongTinDieuTri");
            this.grpThongTinDieuTri.Controls.Add(this.txtDieuduongtheodoi);
            this.grpThongTinDieuTri.Controls.Add(this.txtBstheodoi);
            this.grpThongTinDieuTri.Controls.Add(this.chkPhieuBoSung);
            this.grpThongTinDieuTri.Controls.Add(this.dtNgayLapPhieu);
            this.grpThongTinDieuTri.Controls.Add(this.lblMsg);
            this.grpThongTinDieuTri.Controls.Add(this.label7);
            this.grpThongTinDieuTri.Controls.Add(this.cboBacSy);
            this.grpThongTinDieuTri.Controls.Add(this.label6);
            this.grpThongTinDieuTri.Controls.Add(this.cboKhoaNoiTru);
            this.grpThongTinDieuTri.Controls.Add(this.label5);
            this.grpThongTinDieuTri.Controls.Add(this.label4);
            this.grpThongTinDieuTri.Controls.Add(this.dtGioLapPhieu);
            this.grpThongTinDieuTri.Controls.Add(this.label3);
            this.grpThongTinDieuTri.Controls.Add(this.label2);
            this.grpThongTinDieuTri.Controls.Add(this.label1);
            this.grpThongTinDieuTri.Controls.Add(this.txtTreat_ID);
            this.grpThongTinDieuTri.Name = "grpThongTinDieuTri";
            // 
            // txtDieuduongtheodoi
            // 
            resources.ApplyResources(this.txtDieuduongtheodoi, "txtDieuduongtheodoi");
            this.txtDieuduongtheodoi.Multiline = true;
            this.txtDieuduongtheodoi.Name = "txtDieuduongtheodoi";
            this.txtDieuduongtheodoi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // txtBstheodoi
            // 
            resources.ApplyResources(this.txtBstheodoi, "txtBstheodoi");
            this.txtBstheodoi.Multiline = true;
            this.txtBstheodoi.Name = "txtBstheodoi";
            this.txtBstheodoi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // chkPhieuBoSung
            // 
            resources.ApplyResources(this.chkPhieuBoSung, "chkPhieuBoSung");
            this.chkPhieuBoSung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.chkPhieuBoSung.Name = "chkPhieuBoSung";
            // 
            // dtNgayLapPhieu
            // 
            resources.ApplyResources(this.dtNgayLapPhieu, "dtNgayLapPhieu");
            // 
            // 
            // 
            this.dtNgayLapPhieu.DropDownCalendar.Name = "";
            this.dtNgayLapPhieu.Name = "dtNgayLapPhieu";
            this.dtNgayLapPhieu.ShowUpDown = true;
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblMsg, "lblMsg");
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Name = "lblMsg";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cboBacSy
            // 
            this.cboBacSy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboBacSy, "cboBacSy");
            this.cboBacSy.FormattingEnabled = true;
            this.cboBacSy.Name = "cboBacSy";
            this.cboBacSy.SelectedIndexChanged += new System.EventHandler(this.cboBacSy_SelectedIndexChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // cboKhoaNoiTru
            // 
            this.cboKhoaNoiTru.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboKhoaNoiTru, "cboKhoaNoiTru");
            this.cboKhoaNoiTru.FormattingEnabled = true;
            this.cboKhoaNoiTru.Name = "cboKhoaNoiTru";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dtGioLapPhieu
            // 
            resources.ApplyResources(this.dtGioLapPhieu, "dtGioLapPhieu");
            this.dtGioLapPhieu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtGioLapPhieu.Name = "dtGioLapPhieu";
            this.dtGioLapPhieu.ShowUpDown = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtTreat_ID
            // 
            resources.ApplyResources(this.txtTreat_ID, "txtTreat_ID");
            this.txtTreat_ID.Name = "txtTreat_ID";
            // 
            // uiStatusBar1
            // 
            resources.ApplyResources(this.uiStatusBar1, "uiStatusBar1");
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            resources.ApplyResources(uiStatusBarPanel1, "uiStatusBarPanel1");
            uiStatusBarPanel2.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel2.Key = "";
            resources.ApplyResources(uiStatusBarPanel2, "uiStatusBarPanel2");
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2});
            this.uiStatusBar1.TabStop = false;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // cmdSave
            // 
            resources.ApplyResources(this.cmdSave, "cmdSave");
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            resources.ApplyResources(this.cmdExit, "cmdExit");
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // frm_themphieudieutri
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.uiStatusBar1);
            this.Controls.Add(this.grpThongTinDieuTri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themphieudieutri";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frm_themphieudieutri_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_themphieudieutri_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTinDieuTri)).EndInit();
            this.grpThongTinDieuTri.ResumeLayout(false);
            this.grpThongTinDieuTri.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox grpThongTinDieuTri;
        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtGioLapPhieu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboBacSy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboKhoaNoiTru;
        internal Janus.Windows.GridEX.EditControls.EditBox txtTreat_ID;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayLapPhieu;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UICheckBox chkPhieuBoSung;
        internal Janus.Windows.GridEX.EditControls.EditBox txtDieuduongtheodoi;
        internal Janus.Windows.GridEX.EditControls.EditBox txtBstheodoi;
    }
}