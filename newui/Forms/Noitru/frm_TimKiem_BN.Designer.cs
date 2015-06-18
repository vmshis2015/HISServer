
namespace VNS.HIS.UI.NOITRU
{
    partial class frm_TimKiem_BN
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
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_TimKiem_BN));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPatientCode = new System.Windows.Forms.TextBox();
            this.cmdTimKiem = new Janus.Windows.EditControls.UIButton();
            this.dtDenNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.dtTuNgay = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.txtKhoanoitru = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtKhoanoitru);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtPatientName);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtPatientCode);
            this.panel1.Controls.Add(this.cmdTimKiem);
            this.panel1.Controls.Add(this.dtDenNgay);
            this.panel1.Controls.Add(this.chkByDate);
            this.panel1.Controls.Add(this.dtTuNgay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 73);
            this.panel1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(514, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 15);
            this.label8.TabIndex = 22;
            this.label8.Text = "Khoa nội trú";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(226, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 15);
            this.label7.TabIndex = 20;
            this.label7.Text = "Tên BN";
            // 
            // txtPatientName
            // 
            this.txtPatientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPatientName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(280, 41);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(228, 21);
            this.txtPatientName.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Mã lần khám";
            // 
            // txtPatientCode
            // 
            this.txtPatientCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPatientCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCode.Location = new System.Drawing.Point(100, 41);
            this.txtPatientCode.Name = "txtPatientCode";
            this.txtPatientCode.Size = new System.Drawing.Size(118, 21);
            this.txtPatientCode.TabIndex = 17;
            // 
            // cmdTimKiem
            // 
            this.cmdTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTimKiem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTimKiem.Location = new System.Drawing.Point(879, 14);
            this.cmdTimKiem.Name = "cmdTimKiem";
            this.cmdTimKiem.Size = new System.Drawing.Size(126, 47);
            this.cmdTimKiem.TabIndex = 8;
            this.cmdTimKiem.Text = "Tìm kiếm";
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtDenNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtDenNgay.DropDownCalendar.Name = "";
            this.dtDenNgay.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDenNgay.Location = new System.Drawing.Point(308, 15);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.ShowUpDown = true;
            this.dtDenNgay.Size = new System.Drawing.Size(200, 21);
            this.dtDenNgay.TabIndex = 7;
            this.dtDenNgay.Value = new System.DateTime(2014, 9, 16, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkByDate.Location = new System.Drawing.Point(12, 14);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(84, 23);
            this.chkByDate.TabIndex = 6;
            this.chkByDate.Text = "Từ ngày";
            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtTuNgay.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtTuNgay.DropDownCalendar.Name = "";
            this.dtTuNgay.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTuNgay.Location = new System.Drawing.Point(102, 15);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.ShowUpDown = true;
            this.dtTuNgay.Size = new System.Drawing.Size(200, 21);
            this.dtTuNgay.TabIndex = 5;
            this.dtTuNgay.Value = new System.DateTime(2014, 9, 16, 0, 0, 0, 0);
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
            this.grdList.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
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
            this.grdList.Location = new System.Drawing.Point(0, 73);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1008, 657);
            this.grdList.TabIndex = 8;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // txtKhoanoitru
            // 
            this.txtKhoanoitru._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtKhoanoitru._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKhoanoitru._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKhoanoitru.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKhoanoitru.AutoCompleteList")));
            this.txtKhoanoitru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKhoanoitru.CaseSensitive = false;
            this.txtKhoanoitru.CompareNoID = true;
            this.txtKhoanoitru.DefaultCode = "-1";
            this.txtKhoanoitru.DefaultID = "-1";
            this.txtKhoanoitru.Drug_ID = null;
            this.txtKhoanoitru.ExtraWidth = 0;
            this.txtKhoanoitru.FillValueAfterSelect = false;
            this.txtKhoanoitru.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKhoanoitru.Location = new System.Drawing.Point(593, 40);
            this.txtKhoanoitru.MaxHeight = 289;
            this.txtKhoanoitru.MinTypedCharacters = 2;
            this.txtKhoanoitru.MyCode = "-1";
            this.txtKhoanoitru.MyID = "-1";
            this.txtKhoanoitru.Name = "txtKhoanoitru";
            this.txtKhoanoitru.RaiseEvent = true;
            this.txtKhoanoitru.RaiseEventEnter = true;
            this.txtKhoanoitru.RaiseEventEnterWhenEmpty = true;
            this.txtKhoanoitru.SelectedIndex = -1;
            this.txtKhoanoitru.Size = new System.Drawing.Size(280, 21);
            this.txtKhoanoitru.splitChar = '@';
            this.txtKhoanoitru.splitCharIDAndCode = '#';
            this.txtKhoanoitru.TabIndex = 23;
            this.txtKhoanoitru.TakeCode = false;
            this.txtKhoanoitru.txtMyCode = null;
            this.txtKhoanoitru.txtMyCode_Edit = null;
            this.txtKhoanoitru.txtMyID = null;
            this.txtKhoanoitru.txtMyID_Edit = null;
            this.txtKhoanoitru.txtMyName = null;
            this.txtKhoanoitru.txtMyName_Edit = null;
            this.txtKhoanoitru.txtNext = null;
            // 
            // frm_TimKiem_BN
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_TimKiem_BN";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm bệnh nhân nội trú";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_TimKiem_BN_FormClosing);
            this.Load += new System.EventHandler(this.frm_TimKiem_BN_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdTimKiem;
        private Janus.Windows.CalendarCombo.CalendarCombo dtDenNgay;
        private Janus.Windows.CalendarCombo.CalendarCombo dtTuNgay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.GridEX grdList;
        public UCs.AutoCompleteTextbox txtKhoanoitru;
        public Janus.Windows.EditControls.UICheckBox chkByDate;
        public System.Windows.Forms.TextBox txtPatientName;
        public System.Windows.Forms.TextBox txtPatientCode;
    }
}