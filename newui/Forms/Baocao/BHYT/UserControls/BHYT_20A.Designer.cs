namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls
{
    partial class BHYT_20A
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BHYT_20A));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtNhomBHYT = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.cboTuyen = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optChitiet = new System.Windows.Forms.RadioButton();
            this.txtKCBBDCode = new System.Windows.Forms.TextBox();
            this.optTonghop = new System.Windows.Forms.RadioButton();
            this.txtdrugtype_id = new System.Windows.Forms.TextBox();
            this.txtTPCode = new System.Windows.Forms.TextBox();
            this.txtDrugtypeCode = new System.Windows.Forms.TextBox();
            this.txtDrugID = new System.Windows.Forms.TextBox();
            this.txtDrugCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboObjectType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTinhthanh = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKCBBD = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkKhacMa = new Janus.Windows.EditControls.UICheckBox();
            this.txtLoaithuoc = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtthuoc = new VNS.HIS.UCs.AutoCompleteTextbox_Thuoc();
            this.chkDate = new Janus.Windows.EditControls.UICheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtpFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.pnlfunctions = new System.Windows.Forms.Panel();
            this.dtpNgayIn = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label5 = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.cmdExcel = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdPreview = new Janus.Windows.EditControls.UIButton();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
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
            this.pnlHeader.Size = new System.Drawing.Size(1024, 37);
            this.pnlHeader.TabIndex = 5;
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
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1024, 33);
            this.baocaO_TIEUDE1.TabIndex = 1;
            this.baocaO_TIEUDE1.TIEUDE = "THỐNG KÊ TÌNH HÌNH DVKT SỬ DỤNG";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.txtNhomBHYT);
            this.pnlSearch.Controls.Add(this.cboTuyen);
            this.pnlSearch.Controls.Add(this.groupBox1);
            this.pnlSearch.Controls.Add(this.label9);
            this.pnlSearch.Controls.Add(this.txtTinhthanh);
            this.pnlSearch.Controls.Add(this.label6);
            this.pnlSearch.Controls.Add(this.txtKCBBD);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.chkKhacMa);
            this.pnlSearch.Controls.Add(this.txtLoaithuoc);
            this.pnlSearch.Controls.Add(this.txtthuoc);
            this.pnlSearch.Controls.Add(this.chkDate);
            this.pnlSearch.Controls.Add(this.label8);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.cmdSearch);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.dtpToDate);
            this.pnlSearch.Controls.Add(this.dtpFromDate);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.Location = new System.Drawing.Point(0, 37);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1024, 61);
            this.pnlSearch.TabIndex = 9;
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
            this.txtNhomBHYT.Location = new System.Drawing.Point(83, 32);
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
            this.cboTuyen.FormattingEnabled = true;
            this.cboTuyen.Items.AddRange(new object[] {
            "Tất cả",
            "Trái tuyến",
            "Đúng tuyến"});
            this.cboTuyen.Location = new System.Drawing.Point(286, 29);
            this.cboTuyen.Name = "cboTuyen";
            this.cboTuyen.Size = new System.Drawing.Size(124, 23);
            this.cboTuyen.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optChitiet);
            this.groupBox1.Controls.Add(this.txtKCBBDCode);
            this.groupBox1.Controls.Add(this.optTonghop);
            this.groupBox1.Controls.Add(this.txtdrugtype_id);
            this.groupBox1.Controls.Add(this.txtTPCode);
            this.groupBox1.Controls.Add(this.txtDrugtypeCode);
            this.groupBox1.Controls.Add(this.txtDrugID);
            this.groupBox1.Controls.Add(this.txtDrugCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboObjectType);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(670, -15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(10, 45);
            this.groupBox1.TabIndex = 93;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // optChitiet
            // 
            this.optChitiet.AutoSize = true;
            this.optChitiet.Location = new System.Drawing.Point(108, 13);
            this.optChitiet.Name = "optChitiet";
            this.optChitiet.Size = new System.Drawing.Size(63, 19);
            this.optChitiet.TabIndex = 1;
            this.optChitiet.Text = "Chi tiết";
            this.optChitiet.UseVisualStyleBackColor = true;
            // 
            // txtKCBBDCode
            // 
            this.txtKCBBDCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKCBBDCode.Location = new System.Drawing.Point(449, 11);
            this.txtKCBBDCode.Name = "txtKCBBDCode";
            this.txtKCBBDCode.Size = new System.Drawing.Size(49, 21);
            this.txtKCBBDCode.TabIndex = 99;
            this.txtKCBBDCode.Text = "-1";
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
            // txtdrugtype_id
            // 
            this.txtdrugtype_id.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdrugtype_id.Location = new System.Drawing.Point(177, 13);
            this.txtdrugtype_id.Name = "txtdrugtype_id";
            this.txtdrugtype_id.Size = new System.Drawing.Size(49, 21);
            this.txtdrugtype_id.TabIndex = 111;
            this.txtdrugtype_id.Text = "-1";
            // 
            // txtTPCode
            // 
            this.txtTPCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTPCode.Location = new System.Drawing.Point(394, 11);
            this.txtTPCode.Name = "txtTPCode";
            this.txtTPCode.Size = new System.Drawing.Size(49, 21);
            this.txtTPCode.TabIndex = 98;
            this.txtTPCode.Text = "-1";
            // 
            // txtDrugtypeCode
            // 
            this.txtDrugtypeCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDrugtypeCode.Location = new System.Drawing.Point(232, 13);
            this.txtDrugtypeCode.Name = "txtDrugtypeCode";
            this.txtDrugtypeCode.Size = new System.Drawing.Size(49, 21);
            this.txtDrugtypeCode.TabIndex = 108;
            this.txtDrugtypeCode.Text = "-1";
            // 
            // txtDrugID
            // 
            this.txtDrugID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDrugID.Location = new System.Drawing.Point(284, 13);
            this.txtDrugID.Name = "txtDrugID";
            this.txtDrugID.Size = new System.Drawing.Size(49, 21);
            this.txtDrugID.TabIndex = 109;
            this.txtDrugID.Text = "-1";
            // 
            // txtDrugCode
            // 
            this.txtDrugCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDrugCode.Location = new System.Drawing.Point(339, 12);
            this.txtDrugCode.Name = "txtDrugCode";
            this.txtDrugCode.Size = new System.Drawing.Size(49, 21);
            this.txtDrugCode.TabIndex = 110;
            this.txtDrugCode.Text = "-1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(440, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 23);
            this.label1.TabIndex = 102;
            this.label1.Text = "Đối tượng:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // cboObjectType
            // 
            this.cboObjectType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboObjectType.FormattingEnabled = true;
            this.cboObjectType.Location = new System.Drawing.Point(514, 19);
            this.cboObjectType.Name = "cboObjectType";
            this.cboObjectType.Size = new System.Drawing.Size(27, 23);
            this.cboObjectType.TabIndex = 0;
            this.cboObjectType.Visible = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(413, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 23);
            this.label9.TabIndex = 97;
            this.label9.Text = "Tỉnh/T.Phố:";
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
            this.txtTinhthanh.Location = new System.Drawing.Point(490, 31);
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
            this.txtTinhthanh.TabIndex = 7;
            this.txtTinhthanh.TakeCode = false;
            this.txtTinhthanh.txtMyCode = this.txtTPCode;
            this.txtTinhthanh.txtMyCode_Edit = null;
            this.txtTinhthanh.txtMyID = null;
            this.txtTinhthanh.txtMyID_Edit = null;
            this.txtTinhthanh.txtMyName = null;
            this.txtTinhthanh.txtMyName_Edit = null;
            this.txtTinhthanh.txtNext = null;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(655, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 23);
            this.label6.TabIndex = 107;
            this.label6.Text = "Thuốc:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.txtKCBBD.Location = new System.Drawing.Point(756, 31);
            this.txtKCBBD.MaxHeight = 289;
            this.txtKCBBD.MinTypedCharacters = 2;
            this.txtKCBBD.MyCode = "-1";
            this.txtKCBBD.MyID = "-1";
            this.txtKCBBD.Name = "txtKCBBD";
            this.txtKCBBD.RaiseEvent = false;
            this.txtKCBBD.RaiseEventEnter = false;
            this.txtKCBBD.RaiseEventEnterWhenEmpty = false;
            this.txtKCBBD.SelectedIndex = -1;
            this.txtKCBBD.Size = new System.Drawing.Size(110, 21);
            this.txtKCBBD.splitChar = '@';
            this.txtKCBBD.splitCharIDAndCode = '#';
            this.txtKCBBD.TabIndex = 8;
            this.txtKCBBD.TakeCode = false;
            this.txtKCBBD.txtMyCode = this.txtKCBBDCode;
            this.txtKCBBD.txtMyCode_Edit = null;
            this.txtKCBBD.txtMyID = null;
            this.txtKCBBD.txtMyID_Edit = null;
            this.txtKCBBD.txtMyName = null;
            this.txtKCBBD.txtMyName_Edit = null;
            this.txtKCBBD.txtNext = null;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(416, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 23);
            this.label2.TabIndex = 106;
            this.label2.Text = "Loại thuốc";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkKhacMa
            // 
            this.chkKhacMa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKhacMa.ForeColor = System.Drawing.Color.Black;
            this.chkKhacMa.Location = new System.Drawing.Point(658, 32);
            this.chkKhacMa.Name = "chkKhacMa";
            this.chkKhacMa.Size = new System.Drawing.Size(92, 23);
            this.chkKhacMa.TabIndex = 8;
            this.chkKhacMa.Text = "Khác KCBBĐ:";
            // 
            // txtLoaithuoc
            // 
            this.txtLoaithuoc._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtLoaithuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaithuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLoaithuoc.AutoCompleteList")));
            this.txtLoaithuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoaithuoc.CaseSensitive = false;
            this.txtLoaithuoc.CompareNoID = true;
            this.txtLoaithuoc.DefaultCode = "-1";
            this.txtLoaithuoc.DefaultID = "-1";
            this.txtLoaithuoc.Drug_ID = null;
            this.txtLoaithuoc.ExtraWidth = 100;
            this.txtLoaithuoc.FillValueAfterSelect = false;
            this.txtLoaithuoc.Location = new System.Drawing.Point(490, 7);
            this.txtLoaithuoc.MaxHeight = 300;
            this.txtLoaithuoc.MinTypedCharacters = 2;
            this.txtLoaithuoc.MyCode = "-1";
            this.txtLoaithuoc.MyID = "-1";
            this.txtLoaithuoc.Name = "txtLoaithuoc";
            this.txtLoaithuoc.RaiseEvent = true;
            this.txtLoaithuoc.RaiseEventEnter = true;
            this.txtLoaithuoc.RaiseEventEnterWhenEmpty = true;
            this.txtLoaithuoc.SelectedIndex = -1;
            this.txtLoaithuoc.Size = new System.Drawing.Size(164, 21);
            this.txtLoaithuoc.splitChar = '@';
            this.txtLoaithuoc.splitCharIDAndCode = '#';
            this.txtLoaithuoc.TabIndex = 3;
            this.txtLoaithuoc.TakeCode = false;
            this.txtLoaithuoc.txtMyCode = this.txtDrugtypeCode;
            this.txtLoaithuoc.txtMyCode_Edit = null;
            this.txtLoaithuoc.txtMyID = this.txtdrugtype_id;
            this.txtLoaithuoc.txtMyID_Edit = null;
            this.txtLoaithuoc.txtMyName = null;
            this.txtLoaithuoc.txtMyName_Edit = null;
            this.txtLoaithuoc.txtNext = null;
            // 
            // txtthuoc
            // 
            this.txtthuoc._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtthuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthuoc.AllowedSelectPrice = false;
            this.txtthuoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtthuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtthuoc.AutoCompleteList")));
            this.txtthuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtthuoc.CaseSensitive = false;
            this.txtthuoc.CompareNoID = true;
            this.txtthuoc.DefaultCode = "-1";
            this.txtthuoc.DefaultID = "-1";
            this.txtthuoc.Drug_ID = null;
            this.txtthuoc.ExtraWidth = 0;
            this.txtthuoc.ExtraWidth_Pre = 0;
            this.txtthuoc.FillValueAfterSelect = true;
            this.txtthuoc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthuoc.GridView = false;
            this.txtthuoc.Location = new System.Drawing.Point(756, 7);
            this.txtthuoc.MaxHeight = 300;
            this.txtthuoc.MinTypedCharacters = 2;
            this.txtthuoc.MyCode = "-1";
            this.txtthuoc.MyID = "-1";
            this.txtthuoc.Name = "txtthuoc";
            this.txtthuoc.RaiseEvent = false;
            this.txtthuoc.RaiseEventEnter = false;
            this.txtthuoc.RaiseEventEnterWhenEmpty = false;
            this.txtthuoc.SelectedIndex = -1;
            this.txtthuoc.Size = new System.Drawing.Size(110, 21);
            this.txtthuoc.splitChar = '@';
            this.txtthuoc.splitCharIDAndCode = '#';
            this.txtthuoc.TabIndex = 4;
            this.txtthuoc.txtMyCode = this.txtDrugCode;
            this.txtthuoc.txtMyCode_Edit = null;
            this.txtthuoc.txtMyID = this.txtDrugID;
            this.txtthuoc.txtMyID_Edit = null;
            this.txtthuoc.txtMyName = null;
            this.txtthuoc.txtMyName_Edit = null;
            this.txtthuoc.txtNext = null;
            // 
            // chkDate
            // 
            this.chkDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDate.ForeColor = System.Drawing.Color.Black;
            this.chkDate.Location = new System.Drawing.Point(9, 6);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(68, 23);
            this.chkDate.TabIndex = 1;
            this.chkDate.TabStop = false;
            this.chkDate.Text = "Từ ngày:";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(239, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 23);
            this.label8.TabIndex = 96;
            this.label8.Text = "Tuyến";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.Location = new System.Drawing.Point(29, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 23);
            this.label4.TabIndex = 95;
            this.label4.Text = "Nhóm";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageSize = new System.Drawing.Size(0, 0);
            this.cmdSearch.Location = new System.Drawing.Point(881, 6);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(140, 46);
            this.cmdSearch.TabIndex = 9;
            this.cmdSearch.Text = "Tìm kiếm(F3)";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(245, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 23);
            this.label3.TabIndex = 94;
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
            this.dtpToDate.Location = new System.Drawing.Point(286, 7);
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
            this.dtpFromDate.Location = new System.Drawing.Point(83, 7);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ShowUpDown = true;
            this.dtpFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtpFromDate.TabIndex = 1;
            this.dtpFromDate.Value = new System.DateTime(2014, 5, 5, 0, 0, 0, 0);
            // 
            // pnlfunctions
            // 
            this.pnlfunctions.Controls.Add(this.dtpNgayIn);
            this.pnlfunctions.Controls.Add(this.label5);
            this.pnlfunctions.Controls.Add(this.prgBar);
            this.pnlfunctions.Controls.Add(this.cmdExcel);
            this.pnlfunctions.Controls.Add(this.cmdPrint);
            this.pnlfunctions.Controls.Add(this.cmdPreview);
            this.pnlfunctions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlfunctions.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlfunctions.Location = new System.Drawing.Point(0, 545);
            this.pnlfunctions.Name = "pnlfunctions";
            this.pnlfunctions.Size = new System.Drawing.Size(1024, 55);
            this.pnlfunctions.TabIndex = 10;
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
            this.dtpNgayIn.Location = new System.Drawing.Point(113, 3);
            this.dtpNgayIn.Name = "dtpNgayIn";
            this.dtpNgayIn.ShowUpDown = true;
            this.dtpNgayIn.Size = new System.Drawing.Size(126, 21);
            this.dtpNgayIn.TabIndex = 51;
            this.dtpNgayIn.Value = new System.DateTime(2014, 5, 5, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 23);
            this.label5.TabIndex = 52;
            this.label5.Text = "Ngày in:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(9, 30);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(466, 20);
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
            // grdList
            // 
            this.grdList.AlternatingColors = true;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.Location = new System.Drawing.Point(0, 98);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1024, 447);
            this.grdList.TabIndex = 273;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // BHYT_20A
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.pnlfunctions);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlHeader);
            this.Name = "BHYT_20A";
            this.Size = new System.Drawing.Size(1024, 600);
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
        private System.Windows.Forms.Panel pnlfunctions;
        private System.Windows.Forms.ProgressBar prgBar;
        private Janus.Windows.EditControls.UIButton cmdExcel;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdPreview;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.TextBox txtKCBBDCode;
        private System.Windows.Forms.ComboBox cboObjectType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTPCode;
        private Janus.Windows.EditControls.UICheckBox chkDate;
        private Janus.Windows.EditControls.UICheckBox chkKhacMa;
        public VNS.HIS.UCs.AutoCompleteTextbox txtKCBBD;
        public VNS.HIS.UCs.AutoCompleteTextbox txtTinhthanh;
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
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgayIn;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox txtDrugCode;
        private System.Windows.Forms.TextBox txtDrugID;
        private System.Windows.Forms.TextBox txtDrugtypeCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private VNS.HIS.UCs.AutoCompleteTextbox txtLoaithuoc;
        private VNS.HIS.UCs.AutoCompleteTextbox_Thuoc txtthuoc;
        private System.Windows.Forms.TextBox txtdrugtype_id;
        public System.Windows.Forms.ComboBox cboTuyen;
        private BAOCAO_TIEUDE baocaO_TIEUDE1;
        private UCs.AutoCompleteTextbox_Danhmucchung txtNhomBHYT;
    }
}
