namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls
{
    partial class BHYT_21A
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BHYT_21A));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtNhomBHYT = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.cboTuyen = new System.Windows.Forms.ComboBox();
            this.txtKCBBDCode = new System.Windows.Forms.TextBox();
            this.cboObjectType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTPCode = new System.Windows.Forms.TextBox();
            this.chkDate = new Janus.Windows.EditControls.UICheckBox();
            this.chkKhacMa = new Janus.Windows.EditControls.UICheckBox();
            this.txtKCBBD = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtTinhthanh = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optChitiet = new System.Windows.Forms.RadioButton();
            this.optTonghop = new System.Windows.Forms.RadioButton();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtpFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.pnlfunctions = new System.Windows.Forms.Panel();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.cmdExcel = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdPreview = new Janus.Windows.EditControls.UIButton();
            this.dtpNgayIn = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label5 = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.pnlHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlfunctions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.baocaO_TIEUDE1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1024, 54);
            this.pnlHeader.TabIndex = 6;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.txtNhomBHYT);
            this.pnlSearch.Controls.Add(this.cboTuyen);
            this.pnlSearch.Controls.Add(this.txtKCBBDCode);
            this.pnlSearch.Controls.Add(this.cboObjectType);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.txtTPCode);
            this.pnlSearch.Controls.Add(this.chkDate);
            this.pnlSearch.Controls.Add(this.chkKhacMa);
            this.pnlSearch.Controls.Add(this.txtKCBBD);
            this.pnlSearch.Controls.Add(this.txtTinhthanh);
            this.pnlSearch.Controls.Add(this.label9);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.groupBox1);
            this.pnlSearch.Controls.Add(this.cmdSearch);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.dtpToDate);
            this.pnlSearch.Controls.Add(this.dtpFromDate);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.Location = new System.Drawing.Point(0, 54);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1024, 81);
            this.pnlSearch.TabIndex = 10;
            // 
            // txtNhomBHYT
            // 
            this.txtNhomBHYT._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtNhomBHYT._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhomBHYT.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNhomBHYT.AutoCompleteList")));
            this.txtNhomBHYT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNhomBHYT.CaseSensitive = false;
            this.txtNhomBHYT.CompareNoID = true;
            this.txtNhomBHYT.DefaultCode = "-1";
            this.txtNhomBHYT.DefaultID = "-1";
            this.txtNhomBHYT.Drug_ID = null;
            this.txtNhomBHYT.ExtraWidth = 100;
            this.txtNhomBHYT.FillValueAfterSelect = false;
            this.txtNhomBHYT.LOAI_DANHMUC = "NHOMBHYT";
            this.txtNhomBHYT.Location = new System.Drawing.Point(83, 46);
            this.txtNhomBHYT.MaxHeight = 300;
            this.txtNhomBHYT.MinTypedCharacters = 2;
            this.txtNhomBHYT.MyCode = "-1";
            this.txtNhomBHYT.MyID = "-1";
            this.txtNhomBHYT.Name = "txtNhomBHYT";
            this.txtNhomBHYT.RaiseEvent = false;
            this.txtNhomBHYT.RaiseEventEnter = false;
            this.txtNhomBHYT.RaiseEventEnterWhenEmpty = false;
            this.txtNhomBHYT.SelectedIndex = -1;
            this.txtNhomBHYT.Size = new System.Drawing.Size(133, 21);
            this.txtNhomBHYT.splitChar = '@';
            this.txtNhomBHYT.splitCharIDAndCode = '#';
            this.txtNhomBHYT.TabIndex = 5;
            this.txtNhomBHYT.TakeCode = false;
            this.txtNhomBHYT.txtMyCode = null;
            this.txtNhomBHYT.txtMyCode_Edit = null;
            this.txtNhomBHYT.txtMyID = null;
            this.txtNhomBHYT.txtMyID_Edit = null;
            this.txtNhomBHYT.txtMyName = null;
            this.txtNhomBHYT.txtMyName_Edit = null;
            this.txtNhomBHYT.txtNext = null;
            // 
            // cboTuyen
            // 
            this.cboTuyen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTuyen.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTuyen.FormattingEnabled = true;
            this.cboTuyen.Items.AddRange(new object[] {
            "Tất cả",
            "Trái tuyến",
            "Đúng tuyến"});
            this.cboTuyen.Location = new System.Drawing.Point(286, 44);
            this.cboTuyen.Name = "cboTuyen";
            this.cboTuyen.Size = new System.Drawing.Size(124, 23);
            this.cboTuyen.TabIndex = 6;
            // 
            // txtKCBBDCode
            // 
            this.txtKCBBDCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKCBBDCode.Location = new System.Drawing.Point(710, 44);
            this.txtKCBBDCode.Name = "txtKCBBDCode";
            this.txtKCBBDCode.Size = new System.Drawing.Size(49, 21);
            this.txtKCBBDCode.TabIndex = 63;
            this.txtKCBBDCode.Text = "-1";
            // 
            // cboObjectType
            // 
            this.cboObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboObjectType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboObjectType.FormattingEnabled = true;
            this.cboObjectType.Location = new System.Drawing.Point(490, 44);
            this.cboObjectType.Name = "cboObjectType";
            this.cboObjectType.Size = new System.Drawing.Size(164, 23);
            this.cboObjectType.TabIndex = 7;
            this.cboObjectType.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(416, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 23);
            this.label1.TabIndex = 84;
            this.label1.Text = "Đối tượng:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // txtTPCode
            // 
            this.txtTPCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTPCode.Location = new System.Drawing.Point(655, 44);
            this.txtTPCode.Name = "txtTPCode";
            this.txtTPCode.Size = new System.Drawing.Size(49, 21);
            this.txtTPCode.TabIndex = 62;
            this.txtTPCode.Text = "-1";
            // 
            // chkDate
            // 
            this.chkDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDate.ForeColor = System.Drawing.Color.Black;
            this.chkDate.Location = new System.Drawing.Point(9, 15);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(68, 23);
            this.chkDate.TabIndex = 0;
            this.chkDate.TabStop = false;
            this.chkDate.Text = "Từ ngày:";
            // 
            // chkKhacMa
            // 
            this.chkKhacMa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKhacMa.ForeColor = System.Drawing.Color.Black;
            this.chkKhacMa.Location = new System.Drawing.Point(660, 16);
            this.chkKhacMa.Name = "chkKhacMa";
            this.chkKhacMa.Size = new System.Drawing.Size(92, 23);
            this.chkKhacMa.TabIndex = 4;
            this.chkKhacMa.TabStop = false;
            this.chkKhacMa.Text = "Khác KCBBĐ:";
            // 
            // txtKCBBD
            // 
            this.txtKCBBD._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtKCBBD._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKCBBD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKCBBD.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKCBBD.AutoCompleteList")));
            this.txtKCBBD.CaseSensitive = false;
            this.txtKCBBD.CompareNoID = true;
            this.txtKCBBD.DefaultCode = "-1";
            this.txtKCBBD.DefaultID = "-1";
            this.txtKCBBD.Drug_ID = null;
            this.txtKCBBD.ExtraWidth = 0;
            this.txtKCBBD.FillValueAfterSelect = false;
            this.txtKCBBD.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKCBBD.Location = new System.Drawing.Point(758, 17);
            this.txtKCBBD.MaxHeight = 289;
            this.txtKCBBD.MinTypedCharacters = 2;
            this.txtKCBBD.MyCode = "-1";
            this.txtKCBBD.MyID = "-1";
            this.txtKCBBD.Name = "txtKCBBD";
            this.txtKCBBD.RaiseEvent = false;
            this.txtKCBBD.RaiseEventEnter = false;
            this.txtKCBBD.RaiseEventEnterWhenEmpty = false;
            this.txtKCBBD.SelectedIndex = -1;
            this.txtKCBBD.Size = new System.Drawing.Size(117, 21);
            this.txtKCBBD.splitChar = '@';
            this.txtKCBBD.splitCharIDAndCode = '#';
            this.txtKCBBD.TabIndex = 4;
            this.txtKCBBD.TakeCode = false;
            this.txtKCBBD.txtMyCode = this.txtKCBBDCode;
            this.txtKCBBD.txtMyCode_Edit = null;
            this.txtKCBBD.txtMyID = null;
            this.txtKCBBD.txtMyID_Edit = null;
            this.txtKCBBD.txtMyName = null;
            this.txtKCBBD.txtMyName_Edit = null;
            this.txtKCBBD.txtNext = null;
            // 
            // txtTinhthanh
            // 
            this.txtTinhthanh._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtTinhthanh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhthanh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTinhthanh.AutoCompleteList")));
            this.txtTinhthanh.CaseSensitive = false;
            this.txtTinhthanh.CompareNoID = true;
            this.txtTinhthanh.DefaultCode = "-1";
            this.txtTinhthanh.DefaultID = "-1";
            this.txtTinhthanh.Drug_ID = null;
            this.txtTinhthanh.ExtraWidth = 200;
            this.txtTinhthanh.FillValueAfterSelect = false;
            this.txtTinhthanh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhthanh.Location = new System.Drawing.Point(490, 18);
            this.txtTinhthanh.MaxHeight = 289;
            this.txtTinhthanh.MinTypedCharacters = 2;
            this.txtTinhthanh.MyCode = "-1";
            this.txtTinhthanh.MyID = "-1";
            this.txtTinhthanh.Name = "txtTinhthanh";
            this.txtTinhthanh.RaiseEvent = true;
            this.txtTinhthanh.RaiseEventEnter = false;
            this.txtTinhthanh.RaiseEventEnterWhenEmpty = false;
            this.txtTinhthanh.SelectedIndex = -1;
            this.txtTinhthanh.Size = new System.Drawing.Size(164, 21);
            this.txtTinhthanh.splitChar = '@';
            this.txtTinhthanh.splitCharIDAndCode = '#';
            this.txtTinhthanh.TabIndex = 3;
            this.txtTinhthanh.TakeCode = false;
            this.txtTinhthanh.txtMyCode = this.txtTPCode;
            this.txtTinhthanh.txtMyCode_Edit = null;
            this.txtTinhthanh.txtMyID = null;
            this.txtTinhthanh.txtMyID_Edit = null;
            this.txtTinhthanh.txtMyName = null;
            this.txtTinhthanh.txtMyName_Edit = null;
            this.txtTinhthanh.txtNext = null;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(416, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 23);
            this.label9.TabIndex = 58;
            this.label9.Text = "Tỉnh/T.Phố:";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(239, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 23);
            this.label8.TabIndex = 57;
            this.label8.Text = "Tuyến";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.Location = new System.Drawing.Point(29, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 23);
            this.label4.TabIndex = 53;
            this.label4.Text = "Nhóm";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optChitiet);
            this.groupBox1.Controls.Add(this.optTonghop);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(814, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(10, 38);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // optChitiet
            // 
            this.optChitiet.AutoSize = true;
            this.optChitiet.Location = new System.Drawing.Point(108, 12);
            this.optChitiet.Name = "optChitiet";
            this.optChitiet.Size = new System.Drawing.Size(63, 19);
            this.optChitiet.TabIndex = 1;
            this.optChitiet.Text = "Chi tiết";
            this.optChitiet.UseVisualStyleBackColor = true;
            // 
            // optTonghop
            // 
            this.optTonghop.AutoSize = true;
            this.optTonghop.Checked = true;
            this.optTonghop.Location = new System.Drawing.Point(24, 12);
            this.optTonghop.Name = "optTonghop";
            this.optTonghop.Size = new System.Drawing.Size(78, 19);
            this.optTonghop.TabIndex = 0;
            this.optTonghop.TabStop = true;
            this.optTonghop.Text = "Tổng hợp";
            this.optTonghop.UseVisualStyleBackColor = true;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageSize = new System.Drawing.Size(0, 0);
            this.cmdSearch.Location = new System.Drawing.Point(881, 18);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(140, 55);
            this.cmdSearch.TabIndex = 8;
            this.cmdSearch.Text = "Tìm kiếm(F3)";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(245, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 23);
            this.label3.TabIndex = 45;
            this.label3.Text = "đến";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpToDate.DropDownCalendar.Name = "";
            this.dtpToDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            this.dtpToDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Location = new System.Drawing.Point(286, 18);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.ShowUpDown = true;
            this.dtpToDate.Size = new System.Drawing.Size(124, 21);
            this.dtpToDate.TabIndex = 2;
            this.dtpToDate.Value = new System.DateTime(2014, 5, 5, 0, 0, 0, 0);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpFromDate.DropDownCalendar.FirstDayOfWeek = Janus.Windows.CalendarCombo.CalendarDayOfWeek.Sunday;
            this.dtpFromDate.DropDownCalendar.Name = "";
            this.dtpFromDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007;
            this.dtpFromDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Location = new System.Drawing.Point(83, 16);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ShowUpDown = true;
            this.dtpFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtpFromDate.TabIndex = 1;
            this.dtpFromDate.Value = new System.DateTime(2014, 5, 5, 0, 0, 0, 0);
            // 
            // pnlfunctions
            // 
            this.pnlfunctions.Controls.Add(this.prgBar);
            this.pnlfunctions.Controls.Add(this.cmdExcel);
            this.pnlfunctions.Controls.Add(this.cmdPrint);
            this.pnlfunctions.Controls.Add(this.cmdPreview);
            this.pnlfunctions.Controls.Add(this.dtpNgayIn);
            this.pnlfunctions.Controls.Add(this.label5);
            this.pnlfunctions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlfunctions.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlfunctions.Location = new System.Drawing.Point(0, 513);
            this.pnlfunctions.Name = "pnlfunctions";
            this.pnlfunctions.Size = new System.Drawing.Size(1024, 55);
            this.pnlfunctions.TabIndex = 11;
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(9, 27);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(466, 23);
            this.prgBar.TabIndex = 18;
            this.prgBar.Visible = false;
            // 
            // cmdExcel
            // 
            this.cmdExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExcel.Enabled = false;
            this.cmdExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExcel.Image")));
            this.cmdExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExcel.Location = new System.Drawing.Point(869, 7);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(140, 39);
            this.cmdExcel.TabIndex = 17;
            this.cmdExcel.Text = "Excel";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Enabled = false;
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(564, 7);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(140, 39);
            this.cmdPrint.TabIndex = 13;
            this.cmdPrint.Text = "In";
            // 
            // cmdPreview
            // 
            this.cmdPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPreview.Enabled = false;
            this.cmdPreview.Image = ((System.Drawing.Image)(resources.GetObject("cmdPreview.Image")));
            this.cmdPreview.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPreview.Location = new System.Drawing.Point(711, 7);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(152, 39);
            this.cmdPreview.TabIndex = 12;
            this.cmdPreview.Text = "Xem trước khi in";
            // 
            // dtpNgayIn
            // 
            this.dtpNgayIn.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayIn.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgayIn.DropDownCalendar.Name = "";
            this.dtpNgayIn.ForeColor = System.Drawing.Color.Black;
            this.dtpNgayIn.Location = new System.Drawing.Point(112, 6);
            this.dtpNgayIn.Name = "dtpNgayIn";
            this.dtpNgayIn.ShowUpDown = true;
            this.dtpNgayIn.Size = new System.Drawing.Size(126, 21);
            this.dtpNgayIn.TabIndex = 49;
            this.dtpNgayIn.Value = new System.DateTime(2014, 5, 5, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(5, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 23);
            this.label5.TabIndex = 50;
            this.label5.Text = "Ngày in:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdList
            // 
            this.grdList.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.AllowDrop = true;
            this.grdList.AlternatingColors = true;
            this.grdList.AutoEdit = true;
            this.grdList.CardColumnHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.ColumnAutoResize = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdList.HeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.False;
            this.grdList.Location = new System.Drawing.Point(0, 135);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.Size = new System.Drawing.Size(1024, 378);
            this.grdList.TabIndex = 49;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "BHYT21A";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1024, 43);
            this.baocaO_TIEUDE1.TabIndex = 4;
            this.baocaO_TIEUDE1.TIEUDE = "THỐNG KÊ TÌNH HÌNH DVKT SỬ DỤNG";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // BHYT_21A
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.pnlfunctions);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlHeader);
            this.Name = "BHYT_21A";
            this.Size = new System.Drawing.Size(1024, 568);
            this.pnlHeader.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlfunctions.ResumeLayout(false);
            this.pnlfunctions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlSearch;
        public VNS.HIS.UCs.AutoCompleteTextbox txtKCBBD;
        private System.Windows.Forms.TextBox txtKCBBDCode;
        public VNS.HIS.UCs.AutoCompleteTextbox txtTinhthanh;
        private System.Windows.Forms.TextBox txtTPCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optChitiet;
        private System.Windows.Forms.RadioButton optTonghop;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private System.Windows.Forms.Label label3;
        public Janus.Windows.CalendarCombo.CalendarCombo dtpToDate;
        public Janus.Windows.CalendarCombo.CalendarCombo dtpFromDate;
        private System.Windows.Forms.Panel pnlfunctions;
        private System.Windows.Forms.ProgressBar prgBar;
        private Janus.Windows.EditControls.UIButton cmdExcel;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdPreview;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayIn;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private Janus.Windows.EditControls.UICheckBox chkKhacMa;
        private Janus.Windows.EditControls.UICheckBox chkDate;
        private System.Windows.Forms.ComboBox cboObjectType;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cboTuyen;
        private BAOCAO_TIEUDE baocaO_TIEUDE1;
        private UCs.AutoCompleteTextbox_Danhmucchung txtNhomBHYT;
    }
}
