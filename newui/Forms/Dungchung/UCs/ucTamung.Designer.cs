namespace VNS.HIS.UCs.Noitru
{
    partial class ucTamung
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTamung));
            Janus.Windows.GridEX.GridEXLayout grdTamung_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkPrintPreview = new System.Windows.Forms.CheckBox();
            this.chkSaveAndPrint = new System.Windows.Forms.CheckBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.cmdSua = new Janus.Windows.EditControls.UIButton();
            this.cmdthemmoi = new Janus.Windows.EditControls.UIButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNguoithu = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdGhi = new Janus.Windows.EditControls.UIButton();
            this.txtLydo = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSotien = new MaskedTextBox.MaskedTextBox();
            this.dtpNgaythu = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.txtID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdIn = new Janus.Windows.EditControls.UIButton();
            this.cmdHuy = new Janus.Windows.EditControls.UIButton();
            this.cmdxoa = new Janus.Windows.EditControls.UIButton();
            this.lblTongtien = new System.Windows.Forms.Label();
            this.cmdConfig = new Janus.Windows.EditControls.UIButton();
            this.grdTamung = new Janus.Windows.GridEX.GridEX();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTamung)).BeginInit();
            this.SuspendLayout();
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
            this.panel2.Size = new System.Drawing.Size(466, 163);
            this.panel2.TabIndex = 10;
            // 
            // chkPrintPreview
            // 
            this.chkPrintPreview.AutoSize = true;
            this.chkPrintPreview.Location = new System.Drawing.Point(190, 87);
            this.chkPrintPreview.Name = "chkPrintPreview";
            this.chkPrintPreview.Size = new System.Drawing.Size(108, 17);
            this.chkPrintPreview.TabIndex = 12;
            this.chkPrintPreview.TabStop = false;
            this.chkPrintPreview.Text = "Xem trước khi in?";
            this.chkPrintPreview.UseVisualStyleBackColor = true;
            // 
            // chkSaveAndPrint
            // 
            this.chkSaveAndPrint.AutoSize = true;
            this.chkSaveAndPrint.Location = new System.Drawing.Point(82, 87);
            this.chkSaveAndPrint.Name = "chkSaveAndPrint";
            this.chkSaveAndPrint.Size = new System.Drawing.Size(77, 17);
            this.chkSaveAndPrint.TabIndex = 11;
            this.chkSaveAndPrint.TabStop = false;
            this.chkSaveAndPrint.Text = "Lưu và In?";
            this.chkSaveAndPrint.UseVisualStyleBackColor = true;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(6, 105);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(450, 20);
            this.lblMsg.TabIndex = 35;
            this.lblMsg.Text = "Msg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdSua
            // 
            this.cmdSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSua.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSua.Image = ((System.Drawing.Image)(resources.GetObject("cmdSua.Image")));
            this.cmdSua.Location = new System.Drawing.Point(214, 130);
            this.cmdSua.Name = "cmdSua";
            this.cmdSua.Size = new System.Drawing.Size(76, 26);
            this.cmdSua.TabIndex = 12;
            this.cmdSua.Text = "Sửa";
            // 
            // cmdthemmoi
            // 
            this.cmdthemmoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdthemmoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdthemmoi.Image = ((System.Drawing.Image)(resources.GetObject("cmdthemmoi.Image")));
            this.cmdthemmoi.Location = new System.Drawing.Point(130, 130);
            this.cmdthemmoi.Name = "cmdthemmoi";
            this.cmdthemmoi.Size = new System.Drawing.Size(76, 26);
            this.cmdthemmoi.TabIndex = 11;
            this.cmdthemmoi.Text = "Thêm";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(221, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Số tiền";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 26;
            this.label3.Text = "Ngày thu";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(4, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Lý do";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNguoithu
            // 
            this.txtNguoithu._backcolor = System.Drawing.SystemColors.Control;
            this.txtNguoithu._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoithu._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNguoithu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtNguoithu.Location = new System.Drawing.Point(82, 56);
            this.txtNguoithu.MaxHeight = 289;
            this.txtNguoithu.MinTypedCharacters = 2;
            this.txtNguoithu.MyCode = "-1";
            this.txtNguoithu.MyID = "-1";
            this.txtNguoithu.MyText = "";
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
            // cmdGhi
            // 
            this.cmdGhi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGhi.Enabled = false;
            this.cmdGhi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGhi.Image = ((System.Drawing.Image)(resources.GetObject("cmdGhi.Image")));
            this.cmdGhi.Location = new System.Drawing.Point(298, 130);
            this.cmdGhi.Name = "cmdGhi";
            this.cmdGhi.Size = new System.Drawing.Size(76, 26);
            this.cmdGhi.TabIndex = 13;
            this.cmdGhi.Text = "Ghi";
            // 
            // txtLydo
            // 
            this.txtLydo._backcolor = System.Drawing.SystemColors.Control;
            this.txtLydo._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtLydo.Location = new System.Drawing.Point(82, 31);
            this.txtLydo.MaxHeight = 300;
            this.txtLydo.MinTypedCharacters = 2;
            this.txtLydo.MyCode = "-1";
            this.txtLydo.MyID = "-1";
            this.txtLydo.Name = "txtLydo";
            this.txtLydo.RaiseEvent = false;
            this.txtLydo.RaiseEventEnter = false;
            this.txtLydo.RaiseEventEnterWhenEmpty = false;
            this.txtLydo.SelectedIndex = -1;
            this.txtLydo.Size = new System.Drawing.Size(374, 20);
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
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(8, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Người thu";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSotien
            // 
            this.txtSotien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSotien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSotien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSotien.Location = new System.Drawing.Point(283, 7);
            this.txtSotien.Masked = MaskedTextBox.Mask.Digit;
            this.txtSotien.Name = "txtSotien";
            this.txtSotien.Size = new System.Drawing.Size(173, 21);
            this.txtSotien.TabIndex = 8;
            this.txtSotien.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtpNgaythu
            // 
            this.dtpNgaythu.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaythu.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtpNgaythu.DropDownCalendar.Name = "";
            this.dtpNgaythu.Enabled = false;
            this.dtpNgaythu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaythu.Location = new System.Drawing.Point(82, 7);
            this.dtpNgaythu.Name = "dtpNgaythu";
            this.dtpNgaythu.ShowUpDown = true;
            this.dtpNgaythu.Size = new System.Drawing.Size(133, 21);
            this.dtpNgaythu.TabIndex = 7;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(6, 16);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(31, 20);
            this.txtID.TabIndex = 33;
            this.txtID.TabStop = false;
            this.txtID.Visible = false;
            // 
            // cmdIn
            // 
            this.cmdIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdIn.Image")));
            this.cmdIn.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdIn.Location = new System.Drawing.Point(380, 130);
            this.cmdIn.Name = "cmdIn";
            this.cmdIn.Size = new System.Drawing.Size(76, 26);
            this.cmdIn.TabIndex = 14;
            this.cmdIn.Text = "In phiếu";
            // 
            // cmdHuy
            // 
            this.cmdHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHuy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHuy.Image = ((System.Drawing.Image)(resources.GetObject("cmdHuy.Image")));
            this.cmdHuy.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdHuy.Location = new System.Drawing.Point(380, 130);
            this.cmdHuy.Name = "cmdHuy";
            this.cmdHuy.Size = new System.Drawing.Size(76, 26);
            this.cmdHuy.TabIndex = 16;
            this.cmdHuy.Text = "Hủy";
            // 
            // cmdxoa
            // 
            this.cmdxoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdxoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdxoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdxoa.Image")));
            this.cmdxoa.Location = new System.Drawing.Point(298, 130);
            this.cmdxoa.Name = "cmdxoa";
            this.cmdxoa.Size = new System.Drawing.Size(76, 26);
            this.cmdxoa.TabIndex = 13;
            this.cmdxoa.Text = "Xóa";
            // 
            // lblTongtien
            // 
            this.lblTongtien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTongtien.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongtien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTongtien.Location = new System.Drawing.Point(0, 528);
            this.lblTongtien.Name = "lblTongtien";
            this.lblTongtien.Size = new System.Drawing.Size(466, 24);
            this.lblTongtien.TabIndex = 37;
            this.lblTongtien.Text = "Tổng tiền:";
            this.lblTongtien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdConfig
            // 
            this.cmdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfig.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfig.Image")));
            this.cmdConfig.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdConfig.Location = new System.Drawing.Point(429, 528);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(34, 24);
            this.cmdConfig.TabIndex = 38;
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
            this.grdTamung.Location = new System.Drawing.Point(0, 163);
            this.grdTamung.Name = "grdTamung";
            this.grdTamung.RecordNavigator = true;
            this.grdTamung.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdTamung.Size = new System.Drawing.Size(466, 365);
            this.grdTamung.TabIndex = 39;
            this.grdTamung.TabStop = false;
            this.grdTamung.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdTamung.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdTamung.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // ucTamung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdTamung);
            this.Controls.Add(this.cmdConfig);
            this.Controls.Add(this.lblTongtien);
            this.Controls.Add(this.panel2);
            this.Name = "ucTamung";
            this.Size = new System.Drawing.Size(466, 552);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTamung)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkPrintPreview;
        private System.Windows.Forms.CheckBox chkSaveAndPrint;
        private System.Windows.Forms.Label lblMsg;
        private Janus.Windows.EditControls.UIButton cmdSua;
        private Janus.Windows.EditControls.UIButton cmdthemmoi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private HIS.UCs.AutoCompleteTextbox txtNguoithu;
        private Janus.Windows.EditControls.UIButton cmdGhi;
        private HIS.UCs.AutoCompleteTextbox_Danhmucchung txtLydo;
        private System.Windows.Forms.Label label5;
        private MaskedTextBox.MaskedTextBox txtSotien;
        private Janus.Windows.CalendarCombo.CalendarCombo dtpNgaythu;
        private Janus.Windows.GridEX.EditControls.EditBox txtID;
        private Janus.Windows.EditControls.UIButton cmdIn;
        private Janus.Windows.EditControls.UIButton cmdHuy;
        private Janus.Windows.EditControls.UIButton cmdxoa;
        private System.Windows.Forms.Label lblTongtien;
        private Janus.Windows.EditControls.UIButton cmdConfig;
        private Janus.Windows.GridEX.GridEX grdTamung;
    }
}
