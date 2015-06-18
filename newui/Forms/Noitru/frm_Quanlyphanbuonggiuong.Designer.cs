namespace VNS.HIS.UI.NOITRU
{
    partial class frm_Quanlyphanbuonggiuong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Quanlyphanbuonggiuong));
            Janus.Windows.GridEX.GridEXLayout grdBuongGiuong_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdThemMoiBN = new System.Windows.Forms.ToolStripButton();
            this.cmdSuaThongTinBN = new System.Windows.Forms.ToolStripButton();
            this.cmdNhapvien = new System.Windows.Forms.ToolStripButton();
            this.cmdPhanGiuong = new System.Windows.Forms.ToolStripButton();
            this.cmdHuyphangiuong = new System.Windows.Forms.ToolStripButton();
            this.cmdChuyenKhoa = new System.Windows.Forms.ToolStripButton();
            this.cmdHuychuyenkhoa = new System.Windows.Forms.ToolStripButton();
            this.cmdChuyenGiuong = new System.Windows.Forms.ToolStripButton();
            this.cmdConfig = new System.Windows.Forms.ToolStripButton();
            this.cmdLichSu = new System.Windows.Forms.ToolStripButton();
            this.cmdExit = new System.Windows.Forms.ToolStripButton();
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
            this.grdBuongGiuong = new Janus.Windows.GridEX.GridEX();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuongGiuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdThemMoiBN,
            this.cmdSuaThongTinBN,
            this.cmdNhapvien,
            this.cmdPhanGiuong,
            this.cmdHuyphangiuong,
            this.cmdChuyenKhoa,
            this.cmdHuychuyenkhoa,
            this.cmdChuyenGiuong,
            this.cmdConfig,
            this.cmdLichSu,
            this.cmdExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 39);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdThemMoiBN
            // 
            this.cmdThemMoiBN.BackColor = System.Drawing.SystemColors.Control;
            this.cmdThemMoiBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmdThemMoiBN.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemMoiBN.Image")));
            this.cmdThemMoiBN.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdThemMoiBN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdThemMoiBN.Name = "cmdThemMoiBN";
            this.cmdThemMoiBN.Size = new System.Drawing.Size(91, 36);
            this.cmdThemMoiBN.Text = "Thêm mới";
            this.cmdThemMoiBN.Visible = false;
            this.cmdThemMoiBN.Click += new System.EventHandler(this.cmdThemMoiBN_Click);
            // 
            // cmdSuaThongTinBN
            // 
            this.cmdSuaThongTinBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmdSuaThongTinBN.Image = ((System.Drawing.Image)(resources.GetObject("cmdSuaThongTinBN.Image")));
            this.cmdSuaThongTinBN.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdSuaThongTinBN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSuaThongTinBN.Name = "cmdSuaThongTinBN";
            this.cmdSuaThongTinBN.Size = new System.Drawing.Size(107, 36);
            this.cmdSuaThongTinBN.Text = "Sửa thông tin";
            this.cmdSuaThongTinBN.Visible = false;
            this.cmdSuaThongTinBN.Click += new System.EventHandler(this.cmdSuaThongTinBN_Click);
            // 
            // cmdNhapvien
            // 
            this.cmdNhapvien.Image = ((System.Drawing.Image)(resources.GetObject("cmdNhapvien.Image")));
            this.cmdNhapvien.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNhapvien.Name = "cmdNhapvien";
            this.cmdNhapvien.Size = new System.Drawing.Size(90, 36);
            this.cmdNhapvien.Text = "Nhập viện";
            this.cmdNhapvien.Visible = false;
            this.cmdNhapvien.Click += new System.EventHandler(this.cmdNhapvien_Click);
            // 
            // cmdPhanGiuong
            // 
            this.cmdPhanGiuong.Image = ((System.Drawing.Image)(resources.GetObject("cmdPhanGiuong.Image")));
            this.cmdPhanGiuong.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdPhanGiuong.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdPhanGiuong.Name = "cmdPhanGiuong";
            this.cmdPhanGiuong.Size = new System.Drawing.Size(107, 36);
            this.cmdPhanGiuong.Text = "Phân giường";
            this.cmdPhanGiuong.ToolTipText = "Phân giường nằm cho Bệnh nhân vừa nhập viện hoặc vừa chuyển Khoa nội trú";
            this.cmdPhanGiuong.Click += new System.EventHandler(this.cmdPhanGiuong_Click);
            // 
            // cmdHuyphangiuong
            // 
            this.cmdHuyphangiuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmdHuyphangiuong.Image = ((System.Drawing.Image)(resources.GetObject("cmdHuyphangiuong.Image")));
            this.cmdHuyphangiuong.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdHuyphangiuong.Name = "cmdHuyphangiuong";
            this.cmdHuyphangiuong.Size = new System.Drawing.Size(128, 36);
            this.cmdHuyphangiuong.Text = "Hủy phân giường";
            this.cmdHuyphangiuong.ToolTipText = "Hủy phân buồng giường";
            this.cmdHuyphangiuong.Click += new System.EventHandler(this.cmdHuyphangiuong_Click);
            // 
            // cmdChuyenKhoa
            // 
            this.cmdChuyenKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmdChuyenKhoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdChuyenKhoa.Image")));
            this.cmdChuyenKhoa.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdChuyenKhoa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdChuyenKhoa.Name = "cmdChuyenKhoa";
            this.cmdChuyenKhoa.Size = new System.Drawing.Size(114, 36);
            this.cmdChuyenKhoa.Text = "Chuyển khoa";
            this.cmdChuyenKhoa.ToolTipText = "Chuyển khoa cho các Bệnh nhân đang nằm ở khoa khác";
            this.cmdChuyenKhoa.Click += new System.EventHandler(this.cmdChuyenKhoa_Click);
            // 
            // cmdHuychuyenkhoa
            // 
            this.cmdHuychuyenkhoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdHuychuyenkhoa.Image")));
            this.cmdHuychuyenkhoa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdHuychuyenkhoa.Name = "cmdHuychuyenkhoa";
            this.cmdHuychuyenkhoa.Size = new System.Drawing.Size(128, 36);
            this.cmdHuychuyenkhoa.Text = "Hủy chuyển khoa";
            this.cmdHuychuyenkhoa.ToolTipText = "Hủy chuyển khoa nội trú để quay về khoa cũ";
            this.cmdHuychuyenkhoa.Click += new System.EventHandler(this.cmdHuychuyenkhoa_Click);
            // 
            // cmdChuyenGiuong
            // 
            this.cmdChuyenGiuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmdChuyenGiuong.Image = ((System.Drawing.Image)(resources.GetObject("cmdChuyenGiuong.Image")));
            this.cmdChuyenGiuong.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdChuyenGiuong.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdChuyenGiuong.Name = "cmdChuyenGiuong";
            this.cmdChuyenGiuong.Size = new System.Drawing.Size(125, 36);
            this.cmdChuyenGiuong.Text = "Chuyển giường";
            this.cmdChuyenGiuong.ToolTipText = "Chuyển giường cùng khoa nội trú";
            this.cmdChuyenGiuong.Click += new System.EventHandler(this.cmdChuyenGiuong_Click);
            // 
            // cmdConfig
            // 
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(85, 36);
            this.cmdConfig.Text = "Cấu hình";
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            // 
            // cmdLichSu
            // 
            this.cmdLichSu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmdLichSu.Image = ((System.Drawing.Image)(resources.GetObject("cmdLichSu.Image")));
            this.cmdLichSu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdLichSu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdLichSu.Name = "cmdLichSu";
            this.cmdLichSu.Size = new System.Drawing.Size(74, 36);
            this.cmdLichSu.Text = "Lịch sử";
            this.cmdLichSu.Click += new System.EventHandler(this.cmdLichSu_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(74, 36);
            this.cmdExit.Text = "Thoát";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
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
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 39);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 99);
            this.uiGroupBox1.TabIndex = 5;
            this.uiGroupBox1.Text = "&Thông tin tìm kiếm";
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Location = new System.Drawing.Point(849, 22);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(147, 59);
            this.cmdTimKiem.TabIndex = 18;
            this.cmdTimKiem.Text = "Tìm kiếm(F3)";
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(373, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "Khoa nội trú:";
            // 
            // cboKhoaChuyenDen
            // 
            this.cboKhoaChuyenDen.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboKhoaChuyenDen.Location = new System.Drawing.Point(472, 58);
            this.cboKhoaChuyenDen.Name = "cboKhoaChuyenDen";
            this.cboKhoaChuyenDen.Size = new System.Drawing.Size(349, 21);
            this.cboKhoaChuyenDen.TabIndex = 16;
            this.cboKhoaChuyenDen.Text = "Khoa nội trú";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tên bệnh nhân:";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Location = new System.Drawing.Point(472, 28);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(349, 21);
            this.txtPatientName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã lượt khám:";
            // 
            // txtPatientCode
            // 
            this.txtPatientCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPatientCode.ButtonFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtPatientCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtPatientCode.Location = new System.Drawing.Point(99, 28);
            this.txtPatientCode.Name = "txtPatientCode";
            this.txtPatientCode.Size = new System.Drawing.Size(133, 21);
            this.txtPatientCode.TabIndex = 3;
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
            this.dtToDate.Location = new System.Drawing.Point(239, 58);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(133, 21);
            this.dtToDate.TabIndex = 2;
            this.dtToDate.Value = new System.DateTime(2015, 6, 12, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Location = new System.Drawing.Point(9, 57);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(83, 27);
            this.chkByDate.TabIndex = 1;
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
            this.dtFromDate.Location = new System.Drawing.Point(99, 58);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(133, 21);
            this.dtFromDate.TabIndex = 0;
            this.dtFromDate.Value = new System.DateTime(2015, 6, 12, 0, 0, 0, 0);
            // 
            // grdBuongGiuong
            // 
            this.grdBuongGiuong.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdBuongGiuong.BackColor = System.Drawing.Color.Silver;
            this.grdBuongGiuong.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            grdBuongGiuong_DesignTimeLayout.LayoutString = resources.GetString("grdBuongGiuong_DesignTimeLayout.LayoutString");
            this.grdBuongGiuong.DesignTimeLayout = grdBuongGiuong_DesignTimeLayout;
            this.grdBuongGiuong.Dock = System.Windows.Forms.DockStyle.Right;
            this.grdBuongGiuong.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdBuongGiuong.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdBuongGiuong.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdBuongGiuong.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdBuongGiuong.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdBuongGiuong.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdBuongGiuong.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdBuongGiuong.Font = new System.Drawing.Font("Arial", 8.5F);
            this.grdBuongGiuong.GroupByBoxVisible = false;
            this.grdBuongGiuong.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdBuongGiuong.Location = new System.Drawing.Point(597, 138);
            this.grdBuongGiuong.Name = "grdBuongGiuong";
            this.grdBuongGiuong.RecordNavigator = true;
            this.grdBuongGiuong.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdBuongGiuong.Size = new System.Drawing.Size(411, 592);
            this.grdBuongGiuong.TabIndex = 6;
            this.grdBuongGiuong.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdBuongGiuong.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdBuongGiuong.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
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
            this.grdList.Location = new System.Drawing.Point(0, 138);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(597, 592);
            this.grdList.TabIndex = 7;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // frm_Quanlyphanbuonggiuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.grdBuongGiuong);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Quanlyphanbuonggiuong";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý thông tin phân buồng giường";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Quanlyphanbuonggiuong_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Quanlyphanbuonggiuong_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuongGiuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdThemMoiBN;
        private System.Windows.Forms.ToolStripButton cmdSuaThongTinBN;
        private System.Windows.Forms.ToolStripButton cmdHuyphangiuong;
        private System.Windows.Forms.ToolStripButton cmdExit;
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
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private System.Windows.Forms.ToolStripButton cmdChuyenKhoa;
        private System.Windows.Forms.ToolStripButton cmdChuyenGiuong;
        private System.Windows.Forms.ToolStripButton cmdPhanGiuong;
        private System.Windows.Forms.ToolStripButton cmdLichSu;
        private System.Windows.Forms.ToolStripButton cmdHuychuyenkhoa;
        private System.Windows.Forms.ToolStripButton cmdNhapvien;
        private System.Windows.Forms.ToolStripButton cmdConfig;
        private Janus.Windows.GridEX.GridEX grdBuongGiuong;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.ToolTip toolTip1;
        
    }
}