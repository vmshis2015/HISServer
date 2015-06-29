namespace VNS.HIS.UI.THUOC
{
    partial class frm_themmoi_NhaptraKholeVeKhochan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_NhaptraKholeVeKhochan));
            Janus.Windows.GridEX.GridEXLayout grdKhoXuat_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdPhieuXuatChiTiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.txtLydotra = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.chkIsHetHan = new Janus.Windows.EditControls.UICheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdAddDetail = new Janus.Windows.EditControls.UIButton();
            this.txtSoluongdutru = new MaskedTextBox.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtthuoc = new VNS.HIS.UCs.AutoCompleteTextbox_Thuoc();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblKhoTra = new System.Windows.Forms.Label();
            this.cboKhoTra = new Janus.Windows.EditControls.UIComboBox();
            this.txtIDPhieuNhapKho = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblLinh = new System.Windows.Forms.Label();
            this.cboKhoLinh = new Janus.Windows.EditControls.UIComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboNhanVien = new Janus.Windows.EditControls.UIComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaPhieu = new Janus.Windows.GridEX.EditControls.EditBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdKhoXuat = new Janus.Windows.GridEX.GridEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdPrevius = new Janus.Windows.EditControls.UIButton();
            this.cmdNext = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox4 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdPhieuXuatChiTiet = new Janus.Windows.GridEX.GridEX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdInPhieuNhap = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKhoXuat)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPhieuXuatChiTiet)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpControl
            // 
            this.grpControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grpControl.Controls.Add(this.txtLydotra);
            this.grpControl.Controls.Add(this.chkIsHetHan);
            this.grpControl.Controls.Add(this.groupBox1);
            this.grpControl.Controls.Add(this.cmdAddDetail);
            this.grpControl.Controls.Add(this.txtSoluongdutru);
            this.grpControl.Controls.Add(this.label10);
            this.grpControl.Controls.Add(this.label7);
            this.grpControl.Controls.Add(this.txtthuoc);
            this.grpControl.Controls.Add(this.label2);
            this.grpControl.Controls.Add(this.lblMsg);
            this.grpControl.Controls.Add(this.lblKhoTra);
            this.grpControl.Controls.Add(this.cboKhoTra);
            this.grpControl.Controls.Add(this.txtIDPhieuNhapKho);
            this.grpControl.Controls.Add(this.lblLinh);
            this.grpControl.Controls.Add(this.cboKhoLinh);
            this.grpControl.Controls.Add(this.label8);
            this.grpControl.Controls.Add(this.cboNhanVien);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.dtNgayNhap);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Controls.Add(this.txtMaPhieu);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(1008, 170);
            this.grpControl.TabIndex = 1;
            this.grpControl.Text = "Thông tin phiếu trả";
            // 
            // txtLydotra
            // 
            this.txtLydotra._backcolor = System.Drawing.SystemColors.Info;
            this.txtLydotra._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydotra.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLydotra.AutoCompleteList")));
            this.txtLydotra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydotra.CaseSensitive = false;
            this.txtLydotra.CompareNoID = true;
            this.txtLydotra.DefaultCode = "-1";
            this.txtLydotra.DefaultID = "-1";
            this.txtLydotra.Drug_ID = null;
            this.txtLydotra.ExtraWidth = 0;
            this.txtLydotra.FillValueAfterSelect = false;
            this.txtLydotra.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydotra.LOAI_DANHMUC = "LYDONHAPTRA";
            this.txtLydotra.Location = new System.Drawing.Point(112, 77);
            this.txtLydotra.MaxHeight = 300;
            this.txtLydotra.MinTypedCharacters = 2;
            this.txtLydotra.MyCode = "-1";
            this.txtLydotra.MyID = "-1";
            this.txtLydotra.Name = "txtLydotra";
            this.txtLydotra.RaiseEvent = false;
            this.txtLydotra.RaiseEventEnter = false;
            this.txtLydotra.RaiseEventEnterWhenEmpty = false;
            this.txtLydotra.SelectedIndex = -1;
            this.txtLydotra.Size = new System.Drawing.Size(884, 21);
            this.txtLydotra.splitChar = '@';
            this.txtLydotra.splitCharIDAndCode = '#';
            this.txtLydotra.TabIndex = 483;
            this.txtLydotra.TakeCode = false;
            this.txtLydotra.txtMyCode = null;
            this.txtLydotra.txtMyCode_Edit = null;
            this.txtLydotra.txtMyID = null;
            this.txtLydotra.txtMyID_Edit = null;
            this.txtLydotra.txtMyName = null;
            this.txtLydotra.txtMyName_Edit = null;
            this.txtLydotra.txtNext = null;
            // 
            // chkIsHetHan
            // 
            this.chkIsHetHan.Location = new System.Drawing.Point(112, 144);
            this.chkIsHetHan.Name = "chkIsHetHan";
            this.chkIsHetHan.Size = new System.Drawing.Size(121, 23);
            this.chkIsHetHan.TabIndex = 3;
            this.chkIsHetHan.Text = "Bỏ thuốc hết hạn";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(-4, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1020, 2);
            this.groupBox1.TabIndex = 482;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // cmdAddDetail
            // 
            this.cmdAddDetail.Enabled = false;
            this.cmdAddDetail.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddDetail.Image")));
            this.cmdAddDetail.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdAddDetail.Location = new System.Drawing.Point(953, 115);
            this.cmdAddDetail.Name = "cmdAddDetail";
            this.cmdAddDetail.Size = new System.Drawing.Size(43, 30);
            this.cmdAddDetail.TabIndex = 7;
            // 
            // txtSoluongdutru
            // 
            this.txtSoluongdutru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoluongdutru.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoluongdutru.Location = new System.Drawing.Point(698, 122);
            this.txtSoluongdutru.Masked = MaskedTextBox.Mask.Digit;
            this.txtSoluongdutru.Name = "txtSoluongdutru";
            this.txtSoluongdutru.Size = new System.Drawing.Size(249, 21);
            this.txtSoluongdutru.TabIndex = 6;
            this.txtSoluongdutru.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(628, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 20);
            this.label10.TabIndex = 71;
            this.label10.Text = "S.L hủy:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(36, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 70;
            this.label7.Text = "Tên thuốc:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtthuoc
            // 
            this.txtthuoc._backcolor = System.Drawing.SystemColors.Control;
            this.txtthuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtthuoc.AllowedSelectPrice = false;
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
            this.txtthuoc.Font = new System.Drawing.Font("Arial", 9F);
            this.txtthuoc.GridView = false;
            this.txtthuoc.Location = new System.Drawing.Point(112, 122);
            this.txtthuoc.MaxHeight = 300;
            this.txtthuoc.MinTypedCharacters = 2;
            this.txtthuoc.MyCode = "-1";
            this.txtthuoc.MyID = "-1";
            this.txtthuoc.Name = "txtthuoc";
            this.txtthuoc.RaiseEvent = true;
            this.txtthuoc.RaiseEventEnter = true;
            this.txtthuoc.RaiseEventEnterWhenEmpty = true;
            this.txtthuoc.SelectedIndex = -1;
            this.txtthuoc.Size = new System.Drawing.Size(488, 21);
            this.txtthuoc.splitChar = '@';
            this.txtthuoc.splitCharIDAndCode = '#';
            this.txtthuoc.TabIndex = 5;
            this.txtthuoc.txtMyCode = null;
            this.txtthuoc.txtMyCode_Edit = null;
            this.txtthuoc.txtMyID = null;
            this.txtthuoc.txtMyID_Edit = null;
            this.txtthuoc.txtMyName = null;
            this.txtthuoc.txtMyName_Edit = null;
            this.txtthuoc.txtNext = null;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 46;
            this.label2.Text = "Mô tả thêm:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(3, 151);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(1002, 16);
            this.lblMsg.TabIndex = 45;
            this.lblMsg.Text = "Thông báo:";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKhoTra
            // 
            this.lblKhoTra.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhoTra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblKhoTra.Location = new System.Drawing.Point(6, 53);
            this.lblKhoTra.Name = "lblKhoTra";
            this.lblKhoTra.Size = new System.Drawing.Size(97, 16);
            this.lblKhoTra.TabIndex = 44;
            this.lblKhoTra.Text = "Kho trả";
            this.lblKhoTra.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboKhoTra
            // 
            this.cboKhoTra.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoTra.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboKhoTra.Location = new System.Drawing.Point(112, 49);
            this.cboKhoTra.MaxDropDownItems = 15;
            this.cboKhoTra.Name = "cboKhoTra";
            this.cboKhoTra.Size = new System.Drawing.Size(488, 22);
            this.cboKhoTra.TabIndex = 2;
            this.cboKhoTra.Text = "Kho xuất";
            // 
            // txtIDPhieuNhapKho
            // 
            this.txtIDPhieuNhapKho.Enabled = false;
            this.txtIDPhieuNhapKho.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDPhieuNhapKho.Location = new System.Drawing.Point(112, 20);
            this.txtIDPhieuNhapKho.Name = "txtIDPhieuNhapKho";
            this.txtIDPhieuNhapKho.Size = new System.Drawing.Size(57, 22);
            this.txtIDPhieuNhapKho.TabIndex = 42;
            // 
            // lblLinh
            // 
            this.lblLinh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLinh.Location = new System.Drawing.Point(611, 49);
            this.lblLinh.Name = "lblLinh";
            this.lblLinh.Size = new System.Drawing.Size(81, 16);
            this.lblLinh.TabIndex = 40;
            this.lblLinh.Text = "Kho lĩnh";
            this.lblLinh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboKhoLinh
            // 
            this.cboKhoLinh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKhoLinh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoLinh.ItemsFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.cboKhoLinh.Location = new System.Drawing.Point(698, 46);
            this.cboKhoLinh.MaxDropDownItems = 15;
            this.cboKhoLinh.Name = "cboKhoLinh";
            this.cboKhoLinh.Size = new System.Drawing.Size(298, 22);
            this.cboKhoLinh.TabIndex = 3;
            this.cboKhoLinh.Text = "Kho nhập";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(611, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 16);
            this.label8.TabIndex = 39;
            this.label8.Text = "Nhân viên";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboNhanVien.Enabled = false;
            this.cboNhanVien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNhanVien.Location = new System.Drawing.Point(698, 19);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(298, 22);
            this.cboNhanVien.TabIndex = 1;
            this.cboNhanVien.Text = "Nhân viên";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(346, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 31;
            this.label3.Text = "Ngày nhập";
            // 
            // dtNgayNhap
            // 
            this.dtNgayNhap.CustomFormat = "dd/MM/yyyy";
            this.dtNgayNhap.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayNhap.Location = new System.Drawing.Point(418, 21);
            this.dtNgayNhap.Name = "dtNgayNhap";
            this.dtNgayNhap.Size = new System.Drawing.Size(182, 22);
            this.dtNgayNhap.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Mã phiếu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.Enabled = false;
            this.txtMaPhieu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaPhieu.Location = new System.Drawing.Point(170, 20);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Size = new System.Drawing.Size(170, 22);
            this.txtMaPhieu.TabIndex = 23;
            this.txtMaPhieu.Text = "Tự sinh.....";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 170);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uiGroupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiGroupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 560);
            this.splitContainer1.SplitterDistance = 567;
            this.splitContainer1.TabIndex = 69;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdKhoXuat);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(518, 560);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "Thông tin thuốc, vật tư trong kho trả";
            // 
            // grdKhoXuat
            // 
            this.grdKhoXuat.AlternatingColors = true;
            this.grdKhoXuat.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            grdKhoXuat_DesignTimeLayout.LayoutString = resources.GetString("grdKhoXuat_DesignTimeLayout.LayoutString");
            this.grdKhoXuat.DesignTimeLayout = grdKhoXuat_DesignTimeLayout;
            this.grdKhoXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKhoXuat.DynamicFiltering = true;
            this.grdKhoXuat.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdKhoXuat.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdKhoXuat.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdKhoXuat.FlatBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.grdKhoXuat.FocusCellFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.grdKhoXuat.FocusCellFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdKhoXuat.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid;
            this.grdKhoXuat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdKhoXuat.FrozenColumns = 3;
            this.grdKhoXuat.GroupByBoxVisible = false;
            this.grdKhoXuat.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdKhoXuat.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdKhoXuat.Location = new System.Drawing.Point(3, 18);
            this.grdKhoXuat.Name = "grdKhoXuat";
            this.grdKhoXuat.RecordNavigator = true;
            this.grdKhoXuat.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKhoXuat.Size = new System.Drawing.Size(512, 539);
            this.grdKhoXuat.TabIndex = 3;
            this.grdKhoXuat.TabStop = false;
            this.grdKhoXuat.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKhoXuat.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdKhoXuat.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdPrevius);
            this.panel2.Controls.Add(this.cmdNext);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(518, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(49, 560);
            this.panel2.TabIndex = 0;
            // 
            // cmdPrevius
            // 
            this.cmdPrevius.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrevius.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrevius.Image")));
            this.cmdPrevius.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdPrevius.Location = new System.Drawing.Point(5, 202);
            this.cmdPrevius.Name = "cmdPrevius";
            this.cmdPrevius.Size = new System.Drawing.Size(38, 80);
            this.cmdPrevius.TabIndex = 14;
            this.cmdPrevius.TabStop = false;
            // 
            // cmdNext
            // 
            this.cmdNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNext.Image = ((System.Drawing.Image)(resources.GetObject("cmdNext.Image")));
            this.cmdNext.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdNext.Location = new System.Drawing.Point(5, 114);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(38, 82);
            this.cmdNext.TabIndex = 13;
            this.cmdNext.TabStop = false;
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.grdPhieuXuatChiTiet);
            this.uiGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Size = new System.Drawing.Size(437, 511);
            this.uiGroupBox4.TabIndex = 69;
            this.uiGroupBox4.Text = "Chi tiết phiếu trả";
            // 
            // grdPhieuXuatChiTiet
            // 
            this.grdPhieuXuatChiTiet.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdPhieuXuatChiTiet.AlternatingColors = true;
            this.grdPhieuXuatChiTiet.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            grdPhieuXuatChiTiet_DesignTimeLayout.LayoutString = resources.GetString("grdPhieuXuatChiTiet_DesignTimeLayout.LayoutString");
            this.grdPhieuXuatChiTiet.DesignTimeLayout = grdPhieuXuatChiTiet_DesignTimeLayout;
            this.grdPhieuXuatChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPhieuXuatChiTiet.DynamicFiltering = true;
            this.grdPhieuXuatChiTiet.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdPhieuXuatChiTiet.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPhieuXuatChiTiet.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdPhieuXuatChiTiet.FrozenColumns = 1;
            this.grdPhieuXuatChiTiet.GroupByBoxVisible = false;
            this.grdPhieuXuatChiTiet.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdPhieuXuatChiTiet.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPhieuXuatChiTiet.Location = new System.Drawing.Point(3, 18);
            this.grdPhieuXuatChiTiet.Name = "grdPhieuXuatChiTiet";
            this.grdPhieuXuatChiTiet.RecordNavigator = true;
            this.grdPhieuXuatChiTiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPhieuXuatChiTiet.Size = new System.Drawing.Size(431, 490);
            this.grdPhieuXuatChiTiet.TabIndex = 1;
            this.grdPhieuXuatChiTiet.TabStop = false;
            this.grdPhieuXuatChiTiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPhieuXuatChiTiet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdPhieuXuatChiTiet.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdInPhieuNhap);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 511);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 49);
            this.panel1.TabIndex = 70;
            // 
            // cmdInPhieuNhap
            // 
            this.cmdInPhieuNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInPhieuNhap.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInPhieuNhap.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieuNhap.Image")));
            this.cmdInPhieuNhap.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieuNhap.Location = new System.Drawing.Point(80, 7);
            this.cmdInPhieuNhap.Name = "cmdInPhieuNhap";
            this.cmdInPhieuNhap.Size = new System.Drawing.Size(110, 33);
            this.cmdInPhieuNhap.TabIndex = 11;
            this.cmdInPhieuNhap.TabStop = false;
            this.cmdInPhieuNhap.Text = "In phiếu";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(205, 7);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(110, 33);
            this.cmdSave.TabIndex = 10;
            this.cmdSave.TabStop = false;
            this.cmdSave.Text = "Lưu lại";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(324, 7);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(110, 33);
            this.cmdExit.TabIndex = 12;
            this.cmdExit.TabStop = false;
            this.cmdExit.Text = "Thoát Form";
            // 
            // frm_themmoi_NhaptraKholeVeKhochan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.grpControl);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_NhaptraKholeVeKhochan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu trả thuốc từ kho lẻ về kho chẵn";
            this.Load += new System.EventHandler(this.frm_AddXuatKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKhoXuat)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox4)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPhieuXuatChiTiet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.Label lblKhoTra;
        private Janus.Windows.EditControls.UIComboBox cboKhoTra;
        internal Janus.Windows.GridEX.EditControls.EditBox txtIDPhieuNhapKho;
        private System.Windows.Forms.Label lblLinh;
        private Janus.Windows.EditControls.UIComboBox cboKhoLinh;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.EditControls.UIComboBox cboNhanVien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtNgayNhap;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtMaPhieu;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox4;
        private System.Windows.Forms.Panel panel1;
        private Janus.Windows.EditControls.UIButton cmdInPhieuNhap;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Panel panel2;
        private Janus.Windows.EditControls.UIButton cmdPrevius;
        private Janus.Windows.EditControls.UIButton cmdNext;
        private Janus.Windows.GridEX.GridEX grdKhoXuat;
        private Janus.Windows.GridEX.GridEX grdPhieuXuatChiTiet;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIButton cmdAddDetail;
        private MaskedTextBox.MaskedTextBox txtSoluongdutru;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private UCs.AutoCompleteTextbox_Thuoc txtthuoc;
        private System.Windows.Forms.GroupBox groupBox1;
        private Janus.Windows.EditControls.UICheckBox chkIsHetHan;
        private UCs.AutoCompleteTextbox_Danhmucchung txtLydotra;
    }
}