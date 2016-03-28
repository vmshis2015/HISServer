namespace VNS.HIS.UI.NGOAITRU
{
    partial class frm_Quanly_Ketqua_Maukiemnghiem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Quanly_Ketqua_Maukiemnghiem));
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel1 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel2 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel3 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            Janus.Windows.GridEX.GridEXLayout grdDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdNhanmau = new System.Windows.Forms.ToolStripButton();
            this.cmdNhapKQ = new System.Windows.Forms.ToolStripButton();
            this.cmdXacnhanKQ = new System.Windows.Forms.ToolStripButton();
            this.cmdExit = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuMaDVu = new System.Windows.Forms.ToolStripMenuItem();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.optDaXacnhanKQ = new System.Windows.Forms.RadioButton();
            this.optDanhapKQ = new System.Windows.Forms.RadioButton();
            this.optChuacoKQ = new System.Windows.Forms.RadioButton();
            this.optAll = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMauKn = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDichvuKn = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPatientCode = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPatient_ID = new Janus.Windows.GridEX.EditControls.MaskedEditBox();
            this.txtPatientName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtmTo = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtmFrom = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdDetail = new Janus.Windows.GridEX.GridEX();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdNhanmau,
            this.cmdNhapKQ,
            this.cmdXacnhanKQ,
            this.cmdExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1018, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdNhanmau
            // 
            this.cmdNhanmau.Image = ((System.Drawing.Image)(resources.GetObject("cmdNhanmau.Image")));
            this.cmdNhanmau.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNhanmau.Name = "cmdNhanmau";
            this.cmdNhanmau.Size = new System.Drawing.Size(85, 22);
            this.cmdNhanmau.Tag = "0";
            this.cmdNhanmau.Text = "Nhận mẫu";
            // 
            // cmdNhapKQ
            // 
            this.cmdNhapKQ.Image = ((System.Drawing.Image)(resources.GetObject("cmdNhapKQ.Image")));
            this.cmdNhapKQ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNhapKQ.Name = "cmdNhapKQ";
            this.cmdNhapKQ.Size = new System.Drawing.Size(100, 22);
            this.cmdNhapKQ.Text = "Nhập kết quả";
            // 
            // cmdXacnhanKQ
            // 
            this.cmdXacnhanKQ.Image = ((System.Drawing.Image)(resources.GetObject("cmdXacnhanKQ.Image")));
            this.cmdXacnhanKQ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdXacnhanKQ.Name = "cmdXacnhanKQ";
            this.cmdXacnhanKQ.Size = new System.Drawing.Size(121, 22);
            this.cmdXacnhanKQ.Tag = "0";
            this.cmdXacnhanKQ.Text = "Xác nhận kết quả";
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(118, 22);
            this.cmdExit.Text = "Đóng chức năng";
            this.cmdExit.ToolTipText = "Nhấn nút này để thoát khỏi chức năng ";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMaDVu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(280, 26);
            // 
            // mnuMaDVu
            // 
            this.mnuMaDVu.CheckOnClick = true;
            this.mnuMaDVu.Name = "mnuMaDVu";
            this.mnuMaDVu.Size = new System.Drawing.Size(279, 22);
            this.mnuMaDVu.Text = "Đăng ký dịch vụ KCB bằng cách gõ mã";
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiStatusBar1.Location = new System.Drawing.Point(0, 713);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel1.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel1.Key = "";
            uiStatusBarPanel1.ProgressBarValue = 0;
            uiStatusBarPanel1.Text = "Ctrl+N: Nhập kết quả";
            uiStatusBarPanel1.Width = 129;
            uiStatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel2.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel2.Key = "";
            uiStatusBarPanel2.ProgressBarValue = 0;
            uiStatusBarPanel2.Text = "Ctrl+X: Xác nhận kết quả";
            uiStatusBarPanel2.Width = 148;
            uiStatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            uiStatusBarPanel3.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel3.Key = "";
            uiStatusBarPanel3.ProgressBarValue = 0;
            uiStatusBarPanel3.Text = "Esc: Thoát Form";
            uiStatusBarPanel3.Width = 105;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel1,
            uiStatusBarPanel2,
            uiStatusBarPanel3});
            this.uiStatusBar1.Size = new System.Drawing.Size(1018, 27);
            this.uiStatusBar1.TabIndex = 7;
            this.uiStatusBar1.TabStop = false;
            this.uiStatusBar1.VisualStyle = Janus.Windows.UI.VisualStyle.OfficeXP;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.optDaXacnhanKQ);
            this.uiGroupBox1.Controls.Add(this.optDanhapKQ);
            this.uiGroupBox1.Controls.Add(this.optChuacoKQ);
            this.uiGroupBox1.Controls.Add(this.optAll);
            this.uiGroupBox1.Controls.Add(this.label7);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.txtMauKn);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.txtDichvuKn);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.txtPatientCode);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtPatient_ID);
            this.uiGroupBox1.Controls.Add(this.txtPatientName);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.dtmTo);
            this.uiGroupBox1.Controls.Add(this.dtmFrom);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.Label1);
            this.uiGroupBox1.Controls.Add(this.cmdTimKiem);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 25);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1018, 123);
            this.uiGroupBox1.TabIndex = 1;
            // 
            // optDaXacnhanKQ
            // 
            this.optDaXacnhanKQ.AutoSize = true;
            this.optDaXacnhanKQ.Location = new System.Drawing.Point(551, 97);
            this.optDaXacnhanKQ.Name = "optDaXacnhanKQ";
            this.optDaXacnhanKQ.Size = new System.Drawing.Size(136, 19);
            this.optDaXacnhanKQ.TabIndex = 608;
            this.optDaXacnhanKQ.Text = "Đã xác nhận kết quả";
            this.optDaXacnhanKQ.UseVisualStyleBackColor = true;
            // 
            // optDanhapKQ
            // 
            this.optDanhapKQ.AutoSize = true;
            this.optDanhapKQ.Location = new System.Drawing.Point(373, 97);
            this.optDanhapKQ.Name = "optDanhapKQ";
            this.optDanhapKQ.Size = new System.Drawing.Size(115, 19);
            this.optDanhapKQ.TabIndex = 607;
            this.optDanhapKQ.Text = "Đã nhập kết quả";
            this.optDanhapKQ.UseVisualStyleBackColor = true;
            // 
            // optChuacoKQ
            // 
            this.optChuacoKQ.AutoSize = true;
            this.optChuacoKQ.Location = new System.Drawing.Point(222, 96);
            this.optChuacoKQ.Name = "optChuacoKQ";
            this.optChuacoKQ.Size = new System.Drawing.Size(115, 19);
            this.optChuacoKQ.TabIndex = 606;
            this.optChuacoKQ.Text = "Chưa có kết quả";
            this.optChuacoKQ.UseVisualStyleBackColor = true;
            // 
            // optAll
            // 
            this.optAll.AutoSize = true;
            this.optAll.Checked = true;
            this.optAll.Location = new System.Drawing.Point(137, 96);
            this.optAll.Name = "optAll";
            this.optAll.Size = new System.Drawing.Size(58, 19);
            this.optAll.TabIndex = 605;
            this.optAll.TabStop = true;
            this.optAll.Text = "Tất cả";
            this.optAll.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(12, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 17);
            this.label7.TabIndex = 604;
            this.label7.Text = "Trạng thái:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(460, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 17);
            this.label6.TabIndex = 603;
            this.label6.Text = "Mẫu KN";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMauKn
            // 
            this.txtMauKn._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtMauKn._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMauKn._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtMauKn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMauKn.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtMauKn.AutoCompleteList")));
            this.txtMauKn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMauKn.CaseSensitive = false;
            this.txtMauKn.CompareNoID = true;
            this.txtMauKn.DefaultCode = "-1";
            this.txtMauKn.DefaultID = "-1";
            this.txtMauKn.Drug_ID = null;
            this.txtMauKn.ExtraWidth = 0;
            this.txtMauKn.FillValueAfterSelect = false;
            this.txtMauKn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMauKn.Location = new System.Drawing.Point(551, 69);
            this.txtMauKn.MaxHeight = 289;
            this.txtMauKn.MinTypedCharacters = 2;
            this.txtMauKn.MyCode = "-1";
            this.txtMauKn.MyID = "-1";
            this.txtMauKn.MyText = "";
            this.txtMauKn.Name = "txtMauKn";
            this.txtMauKn.RaiseEvent = true;
            this.txtMauKn.RaiseEventEnter = true;
            this.txtMauKn.RaiseEventEnterWhenEmpty = true;
            this.txtMauKn.SelectedIndex = -1;
            this.txtMauKn.Size = new System.Drawing.Size(301, 21);
            this.txtMauKn.splitChar = '@';
            this.txtMauKn.splitCharIDAndCode = '#';
            this.txtMauKn.TabIndex = 602;
            this.txtMauKn.TakeCode = false;
            this.txtMauKn.txtMyCode = null;
            this.txtMauKn.txtMyCode_Edit = null;
            this.txtMauKn.txtMyID = null;
            this.txtMauKn.txtMyID_Edit = null;
            this.txtMauKn.txtMyName = null;
            this.txtMauKn.txtMyName_Edit = null;
            this.txtMauKn.txtNext = null;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(12, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 17);
            this.label5.TabIndex = 601;
            this.label5.Text = "Dịch vụ KN";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDichvuKn
            // 
            this.txtDichvuKn._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtDichvuKn._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDichvuKn._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDichvuKn.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtDichvuKn.AutoCompleteList")));
            this.txtDichvuKn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDichvuKn.CaseSensitive = false;
            this.txtDichvuKn.CompareNoID = true;
            this.txtDichvuKn.DefaultCode = "-1";
            this.txtDichvuKn.DefaultID = "-1";
            this.txtDichvuKn.Drug_ID = null;
            this.txtDichvuKn.ExtraWidth = 0;
            this.txtDichvuKn.FillValueAfterSelect = false;
            this.txtDichvuKn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDichvuKn.Location = new System.Drawing.Point(137, 69);
            this.txtDichvuKn.MaxHeight = 289;
            this.txtDichvuKn.MinTypedCharacters = 2;
            this.txtDichvuKn.MyCode = "-1";
            this.txtDichvuKn.MyID = "-1";
            this.txtDichvuKn.MyText = "";
            this.txtDichvuKn.Name = "txtDichvuKn";
            this.txtDichvuKn.RaiseEvent = true;
            this.txtDichvuKn.RaiseEventEnter = true;
            this.txtDichvuKn.RaiseEventEnterWhenEmpty = true;
            this.txtDichvuKn.SelectedIndex = -1;
            this.txtDichvuKn.Size = new System.Drawing.Size(313, 21);
            this.txtDichvuKn.splitChar = '@';
            this.txtDichvuKn.splitCharIDAndCode = '#';
            this.txtDichvuKn.TabIndex = 600;
            this.txtDichvuKn.TakeCode = false;
            this.txtDichvuKn.txtMyCode = null;
            this.txtDichvuKn.txtMyCode_Edit = null;
            this.txtDichvuKn.txtMyID = null;
            this.txtDichvuKn.txtMyID_Edit = null;
            this.txtDichvuKn.txtMyName = null;
            this.txtDichvuKn.txtMyName_Edit = null;
            this.txtDichvuKn.txtNext = null;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(631, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 518;
            this.label4.Text = "Mã đăng ký";
            // 
            // txtPatientCode
            // 
            this.txtPatientCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientCode.BackColor = System.Drawing.Color.White;
            this.txtPatientCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCode.Location = new System.Drawing.Point(714, 14);
            this.txtPatientCode.Name = "txtPatientCode";
            this.txtPatientCode.Size = new System.Drawing.Size(138, 21);
            this.txtPatientCode.TabIndex = 3;
            this.txtPatientCode.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(457, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 516;
            this.label2.Text = "ID Khách hàng";
            // 
            // txtPatient_ID
            // 
            this.txtPatient_ID.BackColor = System.Drawing.Color.White;
            this.txtPatient_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_ID.Location = new System.Drawing.Point(551, 14);
            this.txtPatient_ID.Name = "txtPatient_ID";
            this.txtPatient_ID.Numeric = true;
            this.txtPatient_ID.Size = new System.Drawing.Size(76, 21);
            this.txtPatient_ID.TabIndex = 2;
            this.txtPatient_ID.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientName.BackColor = System.Drawing.Color.White;
            this.txtPatientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(137, 40);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(715, 23);
            this.txtPatientName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(23, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 511;
            this.label3.Text = "Tên Khách hàng :";
            // 
            // dtmTo
            // 
            this.dtmTo.CustomFormat = "dd/MM/yyyy";
            this.dtmTo.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtmTo.DropDownCalendar.Name = "";
            this.dtmTo.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtmTo.Location = new System.Drawing.Point(320, 15);
            this.dtmTo.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
            this.dtmTo.Name = "dtmTo";
            this.dtmTo.ShowUpDown = true;
            this.dtmTo.Size = new System.Drawing.Size(130, 21);
            this.dtmTo.TabIndex = 1;
            this.dtmTo.Value = new System.DateTime(2011, 10, 20, 0, 0, 0, 0);
            // 
            // dtmFrom
            // 
            this.dtmFrom.CustomFormat = "dd/MM/yyyy";
            this.dtmFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtmFrom.DropDownCalendar.Name = "";
            this.dtmFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard;
            this.dtmFrom.Location = new System.Drawing.Point(137, 15);
            this.dtmFrom.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
            this.dtmFrom.Name = "dtmFrom";
            this.dtmFrom.ShowUpDown = true;
            this.dtmFrom.Size = new System.Drawing.Size(117, 21);
            this.dtmFrom.TabIndex = 0;
            this.dtmFrom.Value = new System.DateTime(2011, 10, 20, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.AutoSize = true;
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(15, 15);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(116, 19);
            this.chkByDate.TabIndex = 241;
            this.chkByDate.Text = "Ngày nhận mẫu:";
            this.chkByDate.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(260, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(59, 15);
            this.Label1.TabIndex = 240;
            this.Label1.Text = "Đến ngày";
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTimKiem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("cmdTimKiem.Image")));
            this.cmdTimKiem.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdTimKiem.Location = new System.Drawing.Point(858, 14);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(148, 51);
            this.cmdTimKiem.TabIndex = 6;
            this.cmdTimKiem.Text = "Tìm kiếm (F3)";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grdDetail);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 148);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1018, 565);
            this.panel2.TabIndex = 548;
            // 
            // grdDetail
            // 
            this.grdDetail.BackColor = System.Drawing.Color.Silver;
            this.grdDetail.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><RecordNavigator>Tổng:" +
    "|Của</RecordNavigator></LocalizableData>";
            this.grdDetail.ContextMenuStrip = this.contextMenuStrip1;
            grdDetail_DesignTimeLayout.LayoutString = resources.GetString("grdDetail_DesignTimeLayout.LayoutString");
            this.grdDetail.DesignTimeLayout = grdDetail_DesignTimeLayout;
            this.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDetail.Font = new System.Drawing.Font("Arial", 9F);
            this.grdDetail.GroupByBoxVisible = false;
            this.grdDetail.GroupRowFormatStyle.BackColor = System.Drawing.Color.White;
            this.grdDetail.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdDetail.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdDetail.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdDetail.Location = new System.Drawing.Point(0, 0);
            this.grdDetail.Name = "grdDetail";
            this.grdDetail.RecordNavigator = true;
            this.grdDetail.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdDetail.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdDetail.Size = new System.Drawing.Size(1018, 565);
            this.grdDetail.TabIndex = 510;
            this.grdDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDetail.TotalRowFormatStyle.BackColor = System.Drawing.Color.Silver;
            this.grdDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // frm_Quanly_Ketqua_Maukiemnghiem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1018, 740);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.uiStatusBar1);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Quanly_Ketqua_Maukiemnghiem";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý kết quả kiểm nghiệm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private Janus.Windows.CalendarCombo.CalendarCombo dtmTo;
        private Janus.Windows.CalendarCombo.CalendarCombo dtmFrom;
        private System.Windows.Forms.CheckBox chkByDate;
        internal System.Windows.Forms.Label Label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatientName;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtPatientCode;
        internal System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.MaskedEditBox txtPatient_ID;
        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuMaDVu;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton cmdNhapKQ;
        private System.Windows.Forms.ToolStripButton cmdXacnhanKQ;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.GridEX.GridEX grdDetail;
        private System.Windows.Forms.Label label6;
        private UCs.AutoCompleteTextbox txtMauKn;
        private System.Windows.Forms.Label label5;
        private UCs.AutoCompleteTextbox txtDichvuKn;
        private System.Windows.Forms.RadioButton optDaXacnhanKQ;
        private System.Windows.Forms.RadioButton optDanhapKQ;
        private System.Windows.Forms.RadioButton optChuacoKQ;
        private System.Windows.Forms.RadioButton optAll;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripButton cmdNhanmau;
    }
}