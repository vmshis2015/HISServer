namespace  VNS.HIS.UI.THANHTOAN
{
    partial class frm_Danhsach_benhnhan_inphoi_BHYT
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
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Danhsach_benhnhan_inphoi_BHYT));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox3 = new Janus.Windows.EditControls.UIGroupBox();
            this.radChuaduyet = new Janus.Windows.EditControls.UIRadioButton();
            this.radDaduyet = new Janus.Windows.EditControls.UIRadioButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.radNoiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.radNgoaiTru = new Janus.Windows.EditControls.UIRadioButton();
            this.lblChuaKetThuc = new System.Windows.Forms.Label();
            this.lblDaKetThuc = new System.Windows.Forms.Label();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.chkChuaKetThuc = new Janus.Windows.EditControls.UICheckBox();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaLanKham = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.uiGroupBox4 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdExportXML = new Janus.Windows.EditControls.UIButton();
            this.cmdXuatExcel = new Janus.Windows.EditControls.UIButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).BeginInit();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 660);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 660);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdList);
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox4);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 660);
            this.splitContainer1.SplitterDistance = 589;
            this.splitContainer1.TabIndex = 0;
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin tìm kiếm trên lưới</FilterRowInfoText></LocalizableData>";
            this.grdList.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(0, 104);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1008, 485);
            this.grdList.TabIndex = 5;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmdConfig);
            this.uiGroupBox1.Controls.Add(this.uiGroupBox3);
            this.uiGroupBox1.Controls.Add(this.uiGroupBox2);
            this.uiGroupBox1.Controls.Add(this.lblChuaKetThuc);
            this.uiGroupBox1.Controls.Add(this.lblDaKetThuc);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.chkChuaKetThuc);
            this.uiGroupBox1.Controls.Add(this.cmdTimKiem);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtMaLanKham);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 104);
            this.uiGroupBox1.TabIndex = 4;
            this.uiGroupBox1.Text = "&Thông tin tìm kiếm";
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdConfig.Location = new System.Drawing.Point(962, 9);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(43, 34);
            this.cmdConfig.TabIndex = 31;
            this.cmdConfig.TabStop = false;
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.radChuaduyet);
            this.uiGroupBox3.Controls.Add(this.radDaduyet);
            this.uiGroupBox3.Location = new System.Drawing.Point(497, 56);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Size = new System.Drawing.Size(230, 42);
            this.uiGroupBox3.TabIndex = 18;
            this.uiGroupBox3.Text = "Trạng thái";
            // 
            // radChuaduyet
            // 
            this.radChuaduyet.Checked = true;
            this.radChuaduyet.Location = new System.Drawing.Point(22, 13);
            this.radChuaduyet.Name = "radChuaduyet";
            this.radChuaduyet.Size = new System.Drawing.Size(94, 23);
            this.radChuaduyet.TabIndex = 15;
            this.radChuaduyet.TabStop = true;
            this.radChuaduyet.Text = "Chưa duyệt";
            this.radChuaduyet.CheckedChanged += new System.EventHandler(this.radChuaduyet_CheckedChanged);
            // 
            // radDaduyet
            // 
            this.radDaduyet.Location = new System.Drawing.Point(122, 13);
            this.radDaduyet.Name = "radDaduyet";
            this.radDaduyet.Size = new System.Drawing.Size(72, 23);
            this.radDaduyet.TabIndex = 16;
            this.radDaduyet.Text = "Đã duyệt";
            this.radDaduyet.CheckedChanged += new System.EventHandler(this.radDaduyet_CheckedChanged);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.radNoiTru);
            this.uiGroupBox2.Controls.Add(this.radNgoaiTru);
            this.uiGroupBox2.Location = new System.Drawing.Point(497, 12);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(230, 44);
            this.uiGroupBox2.TabIndex = 17;
            this.uiGroupBox2.Text = "Tình trạng";
            // 
            // radNoiTru
            // 
            this.radNoiTru.Location = new System.Drawing.Point(122, 17);
            this.radNoiTru.Name = "radNoiTru";
            this.radNoiTru.Size = new System.Drawing.Size(72, 23);
            this.radNoiTru.TabIndex = 11;
            this.radNoiTru.Text = "Nội trú";
            this.radNoiTru.CheckedChanged += new System.EventHandler(this.radNoiTru_CheckedChanged);
            // 
            // radNgoaiTru
            // 
            this.radNgoaiTru.Checked = true;
            this.radNgoaiTru.Location = new System.Drawing.Point(22, 17);
            this.radNgoaiTru.Name = "radNgoaiTru";
            this.radNgoaiTru.Size = new System.Drawing.Size(72, 23);
            this.radNgoaiTru.TabIndex = 10;
            this.radNgoaiTru.TabStop = true;
            this.radNgoaiTru.Text = "Ngoại trú";
            this.radNgoaiTru.CheckedChanged += new System.EventHandler(this.radNgoaiTru_CheckedChanged);
            // 
            // lblChuaKetThuc
            // 
            this.lblChuaKetThuc.AutoSize = true;
            this.lblChuaKetThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChuaKetThuc.ForeColor = System.Drawing.Color.Navy;
            this.lblChuaKetThuc.Location = new System.Drawing.Point(248, 83);
            this.lblChuaKetThuc.Name = "lblChuaKetThuc";
            this.lblChuaKetThuc.Size = new System.Drawing.Size(94, 15);
            this.lblChuaKetThuc.TabIndex = 14;
            this.lblChuaKetThuc.Text = "lblChuaKetThuc";
            this.lblChuaKetThuc.Visible = false;
            // 
            // lblDaKetThuc
            // 
            this.lblDaKetThuc.AutoSize = true;
            this.lblDaKetThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaKetThuc.ForeColor = System.Drawing.Color.Navy;
            this.lblDaKetThuc.Location = new System.Drawing.Point(83, 83);
            this.lblDaKetThuc.Name = "lblDaKetThuc";
            this.lblDaKetThuc.Size = new System.Drawing.Size(81, 15);
            this.lblDaKetThuc.TabIndex = 13;
            this.lblDaKetThuc.Text = "lblDaKetThuc";
            this.lblDaKetThuc.Visible = false;
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Location = new System.Drawing.Point(257, 54);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(159, 21);
            this.dtToDate.TabIndex = 7;
            this.dtToDate.Value = new System.DateTime(2015, 11, 2, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(15, 53);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(60, 23);
            this.chkByDate.TabIndex = 6;
            this.chkByDate.Text = "Từ ngày";
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // chkChuaKetThuc
            // 
            this.chkChuaKetThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkChuaKetThuc.Location = new System.Drawing.Point(774, 68);
            this.chkChuaKetThuc.Name = "chkChuaKetThuc";
            this.chkChuaKetThuc.Size = new System.Drawing.Size(115, 23);
            this.chkChuaKetThuc.TabIndex = 5;
            this.chkChuaKetThuc.Text = "Chưa kết thúc";
            this.chkChuaKetThuc.Visible = false;
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Location = new System.Drawing.Point(774, 17);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(131, 35);
            this.cmdTimKiem.TabIndex = 4;
            this.cmdTimKiem.Text = "Tìm kiếm";
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mã lần khám";
            // 
            // txtMaLanKham
            // 
            this.txtMaLanKham.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtMaLanKham.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaLanKham.Location = new System.Drawing.Point(92, 19);
            this.txtMaLanKham.Name = "txtMaLanKham";
            this.txtMaLanKham.Size = new System.Drawing.Size(159, 26);
            this.txtMaLanKham.TabIndex = 2;
            this.txtMaLanKham.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            this.txtMaLanKham.TextChanged += new System.EventHandler(this.txtMaLanKham_TextChanged);
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Location = new System.Drawing.Point(92, 54);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(159, 21);
            this.dtFromDate.TabIndex = 0;
            this.dtFromDate.Value = new System.DateTime(2015, 11, 2, 0, 0, 0, 0);
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.cmdExportXML);
            this.uiGroupBox4.Controls.Add(this.cmdXuatExcel);
            this.uiGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Size = new System.Drawing.Size(1008, 67);
            this.uiGroupBox4.TabIndex = 0;
            this.uiGroupBox4.Text = "Chức năng";
            // 
            // cmdExportXML
            // 
            this.cmdExportXML.Location = new System.Drawing.Point(423, 19);
            this.cmdExportXML.Name = "cmdExportXML";
            this.cmdExportXML.Size = new System.Drawing.Size(131, 35);
            this.cmdExportXML.TabIndex = 14;
            this.cmdExportXML.Text = "Xuất XML";
            this.cmdExportXML.Click += new System.EventHandler(this.cmdExportXML_Click);
            // 
            // cmdXuatExcel
            // 
            this.cmdXuatExcel.Location = new System.Drawing.Point(560, 19);
            this.cmdXuatExcel.Name = "cmdXuatExcel";
            this.cmdXuatExcel.Size = new System.Drawing.Size(131, 35);
            this.cmdXuatExcel.TabIndex = 13;
            this.cmdXuatExcel.Text = "Xuất Excel";
            this.cmdXuatExcel.Click += new System.EventHandler(this.cmdXuatExcel_Click);
            // 
            // frm_Danhsach_benhnhan_inphoi_BHYT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 682);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Danhsach_benhnhan_inphoi_BHYT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách thông tin phôi bảo hiểm y tế";
            this.Load += new System.EventHandler(this.frm_Danhsach_benhnhan_inphoi_BHYT_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Danhsach_benhnhan_inphoi_BHYT_KeyDown);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox3)).EndInit();
            this.uiGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox3;
        private Janus.Windows.EditControls.UIRadioButton radChuaduyet;
        private Janus.Windows.EditControls.UIRadioButton radDaduyet;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIRadioButton radNoiTru;
        private Janus.Windows.EditControls.UIRadioButton radNgoaiTru;
        private System.Windows.Forms.Label lblChuaKetThuc;
        private System.Windows.Forms.Label lblDaKetThuc;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.EditControls.UICheckBox chkChuaKetThuc;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaLanKham;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox4;
        private Janus.Windows.EditControls.UIButton cmdXuatExcel;
        private Janus.Windows.EditControls.UIButton cmdExportXML;
        private Janus.Windows.EditControls.UIButton cmdConfig;
    }
}