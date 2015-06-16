namespace VNS.HIS.UI.NOITRU
{
    partial class frm_Quanlytamung
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Quanlytamung));
            Janus.Windows.GridEX.GridEXLayout grdTamung_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.label11 = new System.Windows.Forms.Label();
            this.cboKhoaChuyenDen = new Janus.Windows.EditControls.UIComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPatientName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPatientCode = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.grdTamung = new Janus.Windows.GridEX.GridEX();
            this.lblTongtien = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkPrintPreview = new System.Windows.Forms.CheckBox();
            this.chkSaveAndPrint = new System.Windows.Forms.CheckBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.cmdSua = new Janus.Windows.EditControls.UIButton();
            this.cmdthemmoi = new Janus.Windows.EditControls.UIButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpNgaythu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdxoa = new Janus.Windows.EditControls.UIButton();
            this.cmdGhi = new Janus.Windows.EditControls.UIButton();
            this.cmdIn = new Janus.Windows.EditControls.UIButton();
            this.cmdHuy = new Janus.Windows.EditControls.UIButton();
            this.label7 = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.txtNguoithu = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtLydo = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.txtSotien = new MaskedTextBox.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTamung)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmdTimKiem);
            this.uiGroupBox1.Controls.Add(this.label11);
            this.uiGroupBox1.Controls.Add(this.cboKhoaChuyenDen);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtPatientName);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.txtPatientCode);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F);
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1018, 68);
            this.uiGroupBox1.TabIndex = 5;
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("cmdTimKiem.Image")));
            this.cmdTimKiem.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdTimKiem.Location = new System.Drawing.Point(827, 11);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(181, 43);
            this.cmdTimKiem.TabIndex = 18;
            this.cmdTimKiem.Text = "Tìm kiếm(F3)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(390, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "Khoa nội trú:";
            // 
            // cboKhoaChuyenDen
            // 
            this.cboKhoaChuyenDen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKhoaChuyenDen.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboKhoaChuyenDen.Location = new System.Drawing.Point(472, 42);
            this.cboKhoaChuyenDen.Name = "cboKhoaChuyenDen";
            this.cboKhoaChuyenDen.Size = new System.Drawing.Size(349, 21);
            this.cboKhoaChuyenDen.TabIndex = 5;
            this.cboKhoaChuyenDen.Text = "Khoa nội trú";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tên bệnh nhân:";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientName.Location = new System.Drawing.Point(472, 12);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(349, 21);
            this.txtPatientName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã lượt khám:";
            // 
            // txtPatientCode
            // 
            this.txtPatientCode.BackColor = System.Drawing.Color.White;
            this.txtPatientCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCode.Location = new System.Drawing.Point(99, 12);
            this.txtPatientCode.Name = "txtPatientCode";
            this.txtPatientCode.Size = new System.Drawing.Size(133, 26);
            this.txtPatientCode.TabIndex = 0;
            this.txtPatientCode.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtPatientCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientCode_KeyDown);
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Enabled = false;
            this.dtToDate.Location = new System.Drawing.Point(239, 42);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(133, 21);
            this.dtToDate.TabIndex = 3;
            // 
            // chkByDate
            // 
            this.chkByDate.Location = new System.Drawing.Point(9, 40);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(83, 27);
            this.chkByDate.TabIndex = 2;
            this.chkByDate.Text = "Từ ngày:";
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Enabled = false;
            this.dtFromDate.Location = new System.Drawing.Point(99, 42);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtFromDate.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdConfig);
            this.panel1.Controls.Add(this.grdTamung);
            this.panel1.Controls.Add(this.lblTongtien);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(556, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 472);
            this.panel1.TabIndex = 31;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdConfig.Location = new System.Drawing.Point(424, 449);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(34, 24);
            this.cmdConfig.TabIndex = 31;
            this.cmdConfig.TabStop = false;
            this.cmdConfig.Visible = false;
            // 
            // grdTamung
            // 
            this.grdTamung.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdTamung.BackColor = System.Drawing.Color.Silver;
            this.grdTamung.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin tiền tạm ứng</FilterRowInfoText></LocalizableData>";
            grdTamung_DesignTimeLayout.LayoutString = resources.GetString("grdTamung_DesignTimeLayout.LayoutString");
            this.grdTamung.DesignTimeLayout = grdTamung_DesignTimeLayout;
            this.grdTamung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTamung.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdTamung.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdTamung.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdTamung.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdTamung.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdTamung.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdTamung.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdTamung.Font = new System.Drawing.Font("Arial", 8.5F);
            this.grdTamung.GroupByBoxVisible = false;
            this.grdTamung.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdTamung.Location = new System.Drawing.Point(0, 168);
            this.grdTamung.Name = "grdTamung";
            this.grdTamung.RecordNavigator = true;
            this.grdTamung.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdTamung.Size = new System.Drawing.Size(462, 280);
            this.grdTamung.TabIndex = 37;
            this.grdTamung.TabStop = false;
            this.grdTamung.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdTamung.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdTamung.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // lblTongtien
            // 
            this.lblTongtien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTongtien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongtien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTongtien.Location = new System.Drawing.Point(0, 448);
            this.lblTongtien.Name = "lblTongtien";
            this.lblTongtien.Size = new System.Drawing.Size(462, 24);
            this.lblTongtien.TabIndex = 36;
            this.lblTongtien.Text = "Tổng tiền:";
            this.lblTongtien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkPrintPreview);
            this.panel2.Controls.Add(this.chkSaveAndPrint);
            this.panel2.Controls.Add(this.lblMsg);
            this.panel2.Controls.Add(this.cmdSua);
            this.panel2.Controls.Add(this.cmdthemmoi);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtNguoithu);
            this.panel2.Controls.Add(this.txtLydo);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtSotien);
            this.panel2.Controls.Add(this.dtpNgaythu);
            this.panel2.Controls.Add(this.txtID);
            this.panel2.Controls.Add(this.cmdIn);
            this.panel2.Controls.Add(this.cmdHuy);
            this.panel2.Controls.Add(this.cmdxoa);
            this.panel2.Controls.Add(this.cmdGhi);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 168);
            this.panel2.TabIndex = 9;
            // 
            // chkPrintPreview
            // 
            this.chkPrintPreview.AutoSize = true;
            this.chkPrintPreview.Location = new System.Drawing.Point(190, 87);
            this.chkPrintPreview.Name = "chkPrintPreview";
            this.chkPrintPreview.Size = new System.Drawing.Size(122, 19);
            this.chkPrintPreview.TabIndex = 12;
            this.chkPrintPreview.TabStop = false;
            this.chkPrintPreview.Text = "Xem trước khi in?";
            this.chkPrintPreview.UseVisualStyleBackColor = true;
            // 
            // chkSaveAndPrint
            // 
            this.chkSaveAndPrint.AutoSize = true;
            this.chkSaveAndPrint.Location = new System.Drawing.Point(78, 89);
            this.chkSaveAndPrint.Name = "chkSaveAndPrint";
            this.chkSaveAndPrint.Size = new System.Drawing.Size(83, 19);
            this.chkSaveAndPrint.TabIndex = 11;
            this.chkSaveAndPrint.TabStop = false;
            this.chkSaveAndPrint.Text = "Lưu và In?";
            this.chkSaveAndPrint.UseVisualStyleBackColor = true;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(6, 109);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(446, 20);
            this.lblMsg.TabIndex = 35;
            this.lblMsg.Text = "Msg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdSua
            // 
            this.cmdSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSua.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSua.Image = ((System.Drawing.Image)(resources.GetObject("cmdSua.Image")));
            this.cmdSua.Location = new System.Drawing.Point(210, 136);
            this.cmdSua.Name = "cmdSua";
            this.cmdSua.Size = new System.Drawing.Size(76, 26);
            this.cmdSua.TabIndex = 12;
            this.cmdSua.Text = "Sửa";
            // 
            // cmdthemmoi
            // 
            this.cmdthemmoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdthemmoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdthemmoi.Image = ((System.Drawing.Image)(resources.GetObject("cmdthemmoi.Image")));
            this.cmdthemmoi.Location = new System.Drawing.Point(126, 136);
            this.cmdthemmoi.Name = "cmdthemmoi";
            this.cmdthemmoi.Size = new System.Drawing.Size(76, 26);
            this.cmdthemmoi.TabIndex = 11;
            this.cmdthemmoi.Text = "Thêm";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(217, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Số tiền";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 26;
            this.label3.Text = "Ngày thu";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(0, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Lý do";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(4, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Người thu";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpNgaythu
            // 
            this.dtpNgaythu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpNgaythu.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaythu.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaythu.DropDownCalendar.Name = "";
            this.dtpNgaythu.Enabled = false;
            this.dtpNgaythu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaythu.Location = new System.Drawing.Point(78, 13);
            this.dtpNgaythu.Name = "dtpNgaythu";
            this.dtpNgaythu.ShowUpDown = true;
            this.dtpNgaythu.Size = new System.Drawing.Size(133, 21);
            this.dtpNgaythu.TabIndex = 7;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(6, 16);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(31, 21);
            this.txtID.TabIndex = 33;
            this.txtID.TabStop = false;
            this.txtID.Visible = false;
            // 
            // cmdxoa
            // 
            this.cmdxoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdxoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdxoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdxoa.Image")));
            this.cmdxoa.Location = new System.Drawing.Point(294, 136);
            this.cmdxoa.Name = "cmdxoa";
            this.cmdxoa.Size = new System.Drawing.Size(76, 26);
            this.cmdxoa.TabIndex = 13;
            this.cmdxoa.Text = "Xóa";
            // 
            // cmdGhi
            // 
            this.cmdGhi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGhi.Enabled = false;
            this.cmdGhi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGhi.Image = ((System.Drawing.Image)(resources.GetObject("cmdGhi.Image")));
            this.cmdGhi.Location = new System.Drawing.Point(294, 136);
            this.cmdGhi.Name = "cmdGhi";
            this.cmdGhi.Size = new System.Drawing.Size(76, 26);
            this.cmdGhi.TabIndex = 13;
            this.cmdGhi.Text = "Ghi";
            // 
            // cmdIn
            // 
            this.cmdIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdIn.Image")));
            this.cmdIn.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdIn.Location = new System.Drawing.Point(376, 136);
            this.cmdIn.Name = "cmdIn";
            this.cmdIn.Size = new System.Drawing.Size(76, 26);
            this.cmdIn.TabIndex = 14;
            this.cmdIn.Text = "In phiếu";
            // 
            // cmdHuy
            // 
            this.cmdHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHuy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHuy.Image = ((System.Drawing.Image)(resources.GetObject("cmdHuy.Image")));
            this.cmdHuy.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdHuy.Location = new System.Drawing.Point(376, 136);
            this.cmdHuy.Name = "cmdHuy";
            this.cmdHuy.Size = new System.Drawing.Size(76, 26);
            this.cmdHuy.TabIndex = 16;
            this.cmdHuy.Text = "Hủy";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(0, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(556, 20);
            this.label7.TabIndex = 36;
            this.label7.Text = "Chú ý: Bệnh nhân Dịch vụ phải đóng hết tiền ngoại trú mới được nộp Tạm ứng";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.BackColor = System.Drawing.Color.Silver;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdList.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdList.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 88);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(556, 452);
            this.grdList.TabIndex = 37;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // txtNguoithu
            // 
            this.txtNguoithu._backcolor = System.Drawing.SystemColors.Control;
            this.txtNguoithu._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoithu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNguoithu.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtNguoithu.AutoCompleteList")));
            this.txtNguoithu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNguoithu.CaseSensitive = false;
            this.txtNguoithu.CompareNoID = true;
            this.txtNguoithu.DefaultCode = "-1";
            this.txtNguoithu.DefaultID = "-1";
            this.txtNguoithu.Drug_ID = null;
            this.txtNguoithu.ExtraWidth = 0;
            this.txtNguoithu.FillValueAfterSelect = false;
            this.txtNguoithu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoithu.Location = new System.Drawing.Point(78, 62);
            this.txtNguoithu.MaxHeight = 289;
            this.txtNguoithu.MinTypedCharacters = 2;
            this.txtNguoithu.MyCode = "-1";
            this.txtNguoithu.MyID = "-1";
            this.txtNguoithu.Name = "txtNguoithu";
            this.txtNguoithu.RaiseEvent = true;
            this.txtNguoithu.RaiseEventEnter = true;
            this.txtNguoithu.RaiseEventEnterWhenEmpty = true;
            this.txtNguoithu.SelectedIndex = -1;
            this.txtNguoithu.Size = new System.Drawing.Size(374, 21);
            this.txtNguoithu.splitChar = '@';
            this.txtNguoithu.splitCharIDAndCode = '#';
            this.txtNguoithu.TabIndex = 10;
            this.txtNguoithu.TakeCode = false;
            this.txtNguoithu.txtMyCode = null;
            this.txtNguoithu.txtMyCode_Edit = null;
            this.txtNguoithu.txtMyID = null;
            this.txtNguoithu.txtMyID_Edit = null;
            this.txtNguoithu.txtMyName = null;
            this.txtNguoithu.txtMyName_Edit = null;
            this.txtNguoithu.txtNext = this.cmdGhi;
            // 
            // txtLydo
            // 
            this.txtLydo._backcolor = System.Drawing.SystemColors.Control;
            this.txtLydo._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLydo.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLydo.AutoCompleteList")));
            this.txtLydo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydo.CaseSensitive = false;
            this.txtLydo.CompareNoID = true;
            this.txtLydo.DefaultCode = "\"\"";
            this.txtLydo.DefaultID = "-1";
            this.txtLydo.Drug_ID = null;
            this.txtLydo.ExtraWidth = 0;
            this.txtLydo.FillValueAfterSelect = false;
            this.txtLydo.LOAI_DANHMUC = "LYDOTAMUNG";
            this.txtLydo.Location = new System.Drawing.Point(78, 37);
            this.txtLydo.MaxHeight = 300;
            this.txtLydo.MinTypedCharacters = 2;
            this.txtLydo.MyCode = "-1";
            this.txtLydo.MyID = "-1";
            this.txtLydo.Name = "txtLydo";
            this.txtLydo.RaiseEvent = false;
            this.txtLydo.RaiseEventEnter = false;
            this.txtLydo.RaiseEventEnterWhenEmpty = false;
            this.txtLydo.SelectedIndex = -1;
            this.txtLydo.Size = new System.Drawing.Size(374, 21);
            this.txtLydo.splitChar = '@';
            this.txtLydo.splitCharIDAndCode = '#';
            this.txtLydo.TabIndex = 9;
            this.txtLydo.TakeCode = false;
            this.txtLydo.txtMyCode = null;
            this.txtLydo.txtMyCode_Edit = null;
            this.txtLydo.txtMyID = null;
            this.txtLydo.txtMyID_Edit = null;
            this.txtLydo.txtMyName = null;
            this.txtLydo.txtMyName_Edit = null;
            this.txtLydo.txtNext = this.txtNguoithu;
            // 
            // txtSotien
            // 
            this.txtSotien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSotien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSotien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSotien.Location = new System.Drawing.Point(279, 13);
            this.txtSotien.Masked = MaskedTextBox.Mask.Digit;
            this.txtSotien.Name = "txtSotien";
            this.txtSotien.Size = new System.Drawing.Size(173, 21);
            this.txtSotien.TabIndex = 8;
            this.txtSotien.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frm_Quanlytamung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 540);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.uiGroupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Quanlytamung";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý nộp tiền tạm ứng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Quanlytamung_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Quanlytamung_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTamung)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatientName;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatientCode;
        private System.Windows.Forms.Label label11;
        private Janus.Windows.EditControls.UIComboBox cboKhoaChuyenDen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.EditControls.UIButton cmdSua;
        private Janus.Windows.EditControls.UIButton cmdthemmoi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private UCs.AutoCompleteTextbox txtNguoithu;
        private UCs.AutoCompleteTextbox_Danhmucchung txtLydo;
        private System.Windows.Forms.Label label5;
        private MaskedTextBox.MaskedTextBox txtSotien;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaythu;
        private Janus.Windows.EditControls.UIButton cmdxoa;
        private Janus.Windows.EditControls.UIButton cmdGhi;
        private Janus.Windows.EditControls.UIButton cmdIn;
        private Janus.Windows.EditControls.UIButton cmdHuy;
        private Janus.Windows.GridEX.EditControls.EditBox txtID;
        private Janus.Windows.GridEX.GridEX grdTamung;
        private System.Windows.Forms.Label lblTongtien;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.CheckBox chkSaveAndPrint;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private Janus.Windows.EditControls.UIButton cmdConfig;
        private System.Windows.Forms.CheckBox chkPrintPreview;
        
    }
}