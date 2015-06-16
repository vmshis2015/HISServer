namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls
{
    partial class BHYT_79A
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BHYT_79A));
            Janus.Windows.GridEX.GridEXLayout grdExcel_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cboObject = new System.Windows.Forms.ComboBox();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.lbFromDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtCreateDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optChitiet = new System.Windows.Forms.RadioButton();
            this.optTonghop = new System.Windows.Forms.RadioButton();
            this.pnlfunctions = new System.Windows.Forms.Panel();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.cmdExcel = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdPreview = new Janus.Windows.EditControls.UIButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grdExcel = new Janus.Windows.GridEX.GridEX();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.pnlHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlfunctions.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.baocaO_TIEUDE1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1024, 35);
            this.pnlHeader.TabIndex = 0;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Top;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "BHYT79ATH";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(1024, 31);
            this.baocaO_TIEUDE1.TabIndex = 0;
            this.baocaO_TIEUDE1.TIEUDE = "BẢNG TỔNG HỢP ĐỀ NGHỊ THANH TOÁN CHI PHÍ KHÁM CHỮA BỆNH NGOẠI TRÚ";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.cboObject);
            this.pnlSearch.Controls.Add(this.cmdSearch);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.dtpToDate);
            this.pnlSearch.Controls.Add(this.lbFromDate);
            this.pnlSearch.Controls.Add(this.dtpFromDate);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.Location = new System.Drawing.Point(0, 35);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1024, 88);
            this.pnlSearch.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(104, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(622, 46);
            this.label4.TabIndex = 46;
            this.label4.Text = resources.GetString("label4.Text");
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboObject
            // 
            this.cboObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboObject.FormattingEnabled = true;
            this.cboObject.Location = new System.Drawing.Point(520, 12);
            this.cboObject.Name = "cboObject";
            this.cboObject.Size = new System.Drawing.Size(206, 23);
            this.cboObject.TabIndex = 2;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageSize = new System.Drawing.Size(0, 0);
            this.cmdSearch.Location = new System.Drawing.Point(901, 7);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(120, 56);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "Tìm kiếm(F3)";
            this.toolTip1.SetToolTip(this.cmdSearch, "Nhấn vào đây để thực hiện lấy dữ liệu báo cáo BHYT.79A");
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(242, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 20);
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
            this.dtpToDate.Location = new System.Drawing.Point(277, 15);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.ShowUpDown = true;
            this.dtpToDate.Size = new System.Drawing.Size(132, 21);
            this.dtpToDate.TabIndex = 1;
            this.dtpToDate.Value = new System.DateTime(2014, 5, 6, 0, 0, 0, 0);
            // 
            // lbFromDate
            // 
            this.lbFromDate.Location = new System.Drawing.Point(12, 15);
            this.lbFromDate.Name = "lbFromDate";
            this.lbFromDate.Size = new System.Drawing.Size(87, 20);
            this.lbFromDate.TabIndex = 44;
            this.lbFromDate.Text = "Từ ngày";
            this.lbFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.dtpFromDate.Location = new System.Drawing.Point(104, 15);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ShowUpDown = true;
            this.dtpFromDate.Size = new System.Drawing.Size(132, 21);
            this.dtpFromDate.TabIndex = 0;
            this.dtpFromDate.Value = new System.DateTime(2014, 5, 6, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(415, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 43;
            this.label1.Text = "Đối tượng KCB:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 51;
            this.label2.Text = "Kiểu in:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(301, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 20);
            this.label5.TabIndex = 50;
            this.label5.Text = "Ngày in báo cáo";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtCreateDate
            // 
            this.dtCreateDate.CustomFormat = "dd/MM/yyyy";
            this.dtCreateDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtCreateDate.DropDownCalendar.Name = "";
            this.dtCreateDate.ForeColor = System.Drawing.Color.Black;
            this.dtCreateDate.Location = new System.Drawing.Point(394, 9);
            this.dtCreateDate.Name = "dtCreateDate";
            this.dtCreateDate.ShowUpDown = true;
            this.dtCreateDate.Size = new System.Drawing.Size(131, 21);
            this.dtCreateDate.TabIndex = 49;
            this.dtCreateDate.Value = new System.DateTime(2014, 5, 6, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optChitiet);
            this.groupBox1.Controls.Add(this.optTonghop);
            this.groupBox1.Location = new System.Drawing.Point(104, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 33);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // optChitiet
            // 
            this.optChitiet.AutoSize = true;
            this.optChitiet.Location = new System.Drawing.Point(105, 11);
            this.optChitiet.Name = "optChitiet";
            this.optChitiet.Size = new System.Drawing.Size(67, 19);
            this.optChitiet.TabIndex = 1;
            this.optChitiet.Text = "Chi Tiết";
            this.optChitiet.UseVisualStyleBackColor = true;
            // 
            // optTonghop
            // 
            this.optTonghop.AutoSize = true;
            this.optTonghop.Checked = true;
            this.optTonghop.Location = new System.Drawing.Point(21, 10);
            this.optTonghop.Name = "optTonghop";
            this.optTonghop.Size = new System.Drawing.Size(78, 19);
            this.optTonghop.TabIndex = 0;
            this.optTonghop.TabStop = true;
            this.optTonghop.Text = "Tổng hợp";
            this.optTonghop.UseVisualStyleBackColor = true;
            // 
            // pnlfunctions
            // 
            this.pnlfunctions.Controls.Add(this.prgBar);
            this.pnlfunctions.Controls.Add(this.label2);
            this.pnlfunctions.Controls.Add(this.label5);
            this.pnlfunctions.Controls.Add(this.cmdExcel);
            this.pnlfunctions.Controls.Add(this.dtCreateDate);
            this.pnlfunctions.Controls.Add(this.cmdPrint);
            this.pnlfunctions.Controls.Add(this.groupBox1);
            this.pnlfunctions.Controls.Add(this.cmdPreview);
            this.pnlfunctions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlfunctions.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlfunctions.Location = new System.Drawing.Point(0, 620);
            this.pnlfunctions.Name = "pnlfunctions";
            this.pnlfunctions.Size = new System.Drawing.Size(1024, 48);
            this.pnlfunctions.TabIndex = 2;
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(3, 31);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(563, 14);
            this.prgBar.TabIndex = 18;
            this.prgBar.Visible = false;
            // 
            // cmdExcel
            // 
            this.cmdExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExcel.Enabled = false;
            this.cmdExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExcel.Image")));
            this.cmdExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExcel.Location = new System.Drawing.Point(891, 6);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(120, 34);
            this.cmdExcel.TabIndex = 17;
            this.cmdExcel.Text = "Excel";
            this.toolTip1.SetToolTip(this.cmdExcel, "Nhấn vào đây để xuất dữ liệu báo cáo ra file excel");
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Enabled = false;
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPrint.Location = new System.Drawing.Point(629, 6);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(120, 34);
            this.cmdPrint.TabIndex = 13;
            this.cmdPrint.Text = "In";
            // 
            // cmdPreview
            // 
            this.cmdPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPreview.Enabled = false;
            this.cmdPreview.Image = ((System.Drawing.Image)(resources.GetObject("cmdPreview.Image")));
            this.cmdPreview.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdPreview.Location = new System.Drawing.Point(755, 6);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(130, 34);
            this.cmdPreview.TabIndex = 12;
            this.cmdPreview.Text = "Xem trước khi in";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdExcel);
            this.pnlMain.Controls.Add(this.grdList);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 123);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1024, 497);
            this.pnlMain.TabIndex = 3;
            // 
            // grdExcel
            // 
            grdExcel_DesignTimeLayout.LayoutString = resources.GetString("grdExcel_DesignTimeLayout.LayoutString");
            this.grdExcel.DesignTimeLayout = grdExcel_DesignTimeLayout;
            this.grdExcel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdExcel.DynamicFiltering = true;
            this.grdExcel.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdExcel.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdExcel.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grdExcel.GroupByBoxVisible = false;
            this.grdExcel.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed;
            this.grdExcel.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdExcel.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdExcel.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdExcel.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdExcel.Location = new System.Drawing.Point(0, 487);
            this.grdExcel.Name = "grdExcel";
            this.grdExcel.RecordNavigator = true;
            this.grdExcel.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdExcel.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdExcel.Size = new System.Drawing.Size(1024, 10);
            this.grdExcel.TabIndex = 273;
            this.grdExcel.TabStop = false;
            this.grdExcel.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdExcel.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdExcel.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdExcel.Visible = false;
            // 
            // grdList
            // 
            this.grdList.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed;
            this.grdList.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdList.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdList.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdList.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaderContent = Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            this.grdList.Size = new System.Drawing.Size(1024, 497);
            this.grdList.TabIndex = 274;
            this.grdList.TabStop = false;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // gridEXExporter1
            // 
            this.gridEXExporter1.GridEX = this.grdExcel;
            // 
            // BHYT_79A
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlfunctions);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlHeader);
            this.Name = "BHYT_79A";
            this.Size = new System.Drawing.Size(1024, 668);
            this.pnlHeader.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlfunctions.ResumeLayout(false);
            this.pnlfunctions.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel pnlfunctions;
        private System.Windows.Forms.Panel pnlMain;
        private Janus.Windows.GridEX.GridEX grdExcel;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optChitiet;
        private System.Windows.Forms.RadioButton optTonghop;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpToDate;
        private System.Windows.Forms.Label lbFromDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.CalendarCombo.CalendarCombo dtCreateDate;
        private Janus.Windows.EditControls.UIButton cmdExcel;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdPreview;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.ComboBox cboObject;
        private BAOCAO_TIEUDE baocaO_TIEUDE1;
        private System.Windows.Forms.Label label4;
    }
}
