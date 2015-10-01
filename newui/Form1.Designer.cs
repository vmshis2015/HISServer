namespace newui
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.uiTab1 = new Janus.Windows.UI.Tab.UITab();
            this.tabPhieuDieuTri = new Janus.Windows.UI.Tab.UITabPage();
            this.uiTabPage3 = new Janus.Windows.UI.Tab.UITabPage();
            this.txtDongia = new MaskedTextBox.MaskedTextBox();
            this.txtSoluong = new MaskedTextBox.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.txtDonvitinh = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.chkNgaytiem = new Janus.Windows.EditControls.UICheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).BeginInit();
            this.uiTab1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // uiTab1
            // 
            this.uiTab1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTab1.Location = new System.Drawing.Point(40, 87);
            this.uiTab1.Name = "uiTab1";
            this.uiTab1.Size = new System.Drawing.Size(625, 278);
            this.uiTab1.TabIndex = 20;
            this.uiTab1.TabPages.AddRange(new Janus.Windows.UI.Tab.UITabPage[] {
            this.tabPhieuDieuTri,
            this.uiTabPage3});
            this.uiTab1.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.VS2005;
            // 
            // tabPhieuDieuTri
            // 
            this.tabPhieuDieuTri.Location = new System.Drawing.Point(1, 22);
            this.tabPhieuDieuTri.Name = "tabPhieuDieuTri";
            this.tabPhieuDieuTri.Size = new System.Drawing.Size(623, 255);
            this.tabPhieuDieuTri.TabStop = true;
            this.tabPhieuDieuTri.Text = "Thông tin phiếu điều trị";
            // 
            // uiTabPage3
            // 
            this.uiTabPage3.Location = new System.Drawing.Point(1, 22);
            this.uiTabPage3.Name = "uiTabPage3";
            this.uiTabPage3.Size = new System.Drawing.Size(809, 452);
            this.uiTabPage3.TabStop = true;
            this.uiTabPage3.Text = "Thông tin buồng gường";
            // 
            // txtDongia
            // 
            this.txtDongia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDongia.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDongia.Location = new System.Drawing.Point(92, 39);
            this.txtDongia.Masked = MaskedTextBox.Mask.Decimal;
            this.txtDongia.Name = "txtDongia";
            this.txtDongia.Size = new System.Drawing.Size(96, 21);
            this.txtDongia.TabIndex = 22;
            this.txtDongia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSoluong
            // 
            this.txtSoluong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoluong.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoluong.Location = new System.Drawing.Point(92, 12);
            this.txtSoluong.Masked = MaskedTextBox.Mask.Digit;
            this.txtSoluong.Name = "txtSoluong";
            this.txtSoluong.Size = new System.Drawing.Size(125, 21);
            this.txtSoluong.TabIndex = 21;
            this.txtSoluong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Int";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Decimal";
            // 
            // grdList
            // 
            this.grdList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdList.BackColor = System.Drawing.Color.Silver;
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin bệnh nhân</FilterRowInfoText></LocalizableData>";
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.DynamicFiltering = true;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FocusCellDisplayMode = Janus.Windows.GridEX.FocusCellDisplayMode.UseSelectedFormatStyle;
            this.grdList.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdList.FocusCellFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdList.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F);
            this.grdList.FrozenColumns = 3;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(600, 210);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdList.Size = new System.Drawing.Size(270, 191);
            this.grdList.TabIndex = 26;
            this.grdList.TabStop = false;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // txtDonvitinh
            // 
            this.txtDonvitinh._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtDonvitinh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonvitinh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtDonvitinh.AutoCompleteList")));
            this.txtDonvitinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDonvitinh.CaseSensitive = false;
            this.txtDonvitinh.CompareNoID = true;
            this.txtDonvitinh.DefaultCode = "-1";
            this.txtDonvitinh.DefaultID = "-1";
            this.txtDonvitinh.Drug_ID = null;
            this.txtDonvitinh.ExtraWidth = 0;
            this.txtDonvitinh.FillValueAfterSelect = false;
            this.txtDonvitinh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonvitinh.LOAI_DANHMUC = "DONVITINH";
            this.txtDonvitinh.Location = new System.Drawing.Point(255, 12);
            this.txtDonvitinh.MaxHeight = -1;
            this.txtDonvitinh.MinTypedCharacters = 2;
            this.txtDonvitinh.MyCode = "-1";
            this.txtDonvitinh.MyID = "-1";
            this.txtDonvitinh.Name = "txtDonvitinh";
            this.txtDonvitinh.RaiseEvent = false;
            this.txtDonvitinh.RaiseEventEnter = false;
            this.txtDonvitinh.RaiseEventEnterWhenEmpty = false;
            this.txtDonvitinh.SelectedIndex = -1;
            this.txtDonvitinh.Size = new System.Drawing.Size(127, 21);
            this.txtDonvitinh.splitChar = '@';
            this.txtDonvitinh.splitCharIDAndCode = '#';
            this.txtDonvitinh.TabIndex = 27;
            this.txtDonvitinh.TakeCode = false;
            this.txtDonvitinh.txtMyCode = null;
            this.txtDonvitinh.txtMyCode_Edit = null;
            this.txtDonvitinh.txtMyID = null;
            this.txtDonvitinh.txtMyID_Edit = null;
            this.txtDonvitinh.txtMyName = null;
            this.txtDonvitinh.txtMyName_Edit = null;
            this.txtDonvitinh.txtNext = null;
            // 
            // chkNgaytiem
            // 
            this.chkNgaytiem.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkNgaytiem.Enabled = false;
            this.chkNgaytiem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNgaytiem.Location = new System.Drawing.Point(134, 227);
            this.chkNgaytiem.Name = "chkNgaytiem";
            this.chkNgaytiem.Size = new System.Drawing.Size(89, 23);
            this.chkNgaytiem.TabIndex = 584;
            this.chkNgaytiem.Text = "Ngày tiêm:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 475);
            this.Controls.Add(this.chkNgaytiem);
            this.Controls.Add(this.txtDonvitinh);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDongia);
            this.Controls.Add(this.txtSoluong);
            this.Controls.Add(this.uiTab1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.uiTab1)).EndInit();
            this.uiTab1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private Janus.Windows.UI.Tab.UITab uiTab1;
        private Janus.Windows.UI.Tab.UITabPage tabPhieuDieuTri;
        private Janus.Windows.UI.Tab.UITabPage uiTabPage3;
        private MaskedTextBox.MaskedTextBox txtDongia;
        private MaskedTextBox.MaskedTextBox txtSoluong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.GridEX grdList;
        private VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung txtDonvitinh;
        private Janus.Windows.EditControls.UICheckBox chkNgaytiem;

    }
}

