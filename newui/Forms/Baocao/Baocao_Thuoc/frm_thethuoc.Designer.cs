namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    partial class frm_thethuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_thethuoc));
            Janus.Windows.GridEX.GridEXLayout grdThethuoctutruc_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdListChitiet_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdListKhole_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdThethuoc_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdListKhoChan_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.baocaO_TIEUDE1 = new VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE();
            this.label1 = new System.Windows.Forms.Label();
            this.dtNgayIn = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.cmdBaoCao = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.chkSimple = new Janus.Windows.EditControls.UICheckBox();
            this.chkChanle = new Janus.Windows.EditControls.UICheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLoaikho = new System.Windows.Forms.Label();
            this.chkThekhochitiet = new Janus.Windows.EditControls.UICheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdThethuoctutruc = new Janus.Windows.GridEX.GridEX();
            this.grdListChitiet = new Janus.Windows.GridEX.GridEX();
            this.grdListKhole = new Janus.Windows.GridEX.GridEX();
            this.grdThethuoc = new Janus.Windows.GridEX.GridEX();
            this.grdListKhoChan = new Janus.Windows.GridEX.GridEX();
            this.txtthuoc = new VNS.HIS.UCs.AutoCompleteTextbox_Thuoc();
            this.txtLoaithuoc = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkBiendong = new Janus.Windows.EditControls.UICheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboKho = new Janus.Windows.EditControls.UIComboBox();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.cmdExportToExcel = new Janus.Windows.EditControls.UIButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdThethuoctutruc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListChitiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListKhole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdThethuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListKhoChan)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.baocaO_TIEUDE1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(779, 66);
            this.panel1.TabIndex = 1;
            // 
            // baocaO_TIEUDE1
            // 
            this.baocaO_TIEUDE1.BackColor = System.Drawing.SystemColors.Control;
            this.baocaO_TIEUDE1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baocaO_TIEUDE1.Location = new System.Drawing.Point(0, 0);
            this.baocaO_TIEUDE1.MA_BAOCAO = "THUOC_THETHUOC_KHOCHAN";
            this.baocaO_TIEUDE1.Name = "baocaO_TIEUDE1";
            this.baocaO_TIEUDE1.Phimtat = "Bạn có thể sử dụng phím tắt";
            this.baocaO_TIEUDE1.PicImg = ((System.Drawing.Image)(resources.GetObject("baocaO_TIEUDE1.PicImg")));
            this.baocaO_TIEUDE1.ShortcutAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.baocaO_TIEUDE1.ShortcutFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baocaO_TIEUDE1.showHelp = false;
            this.baocaO_TIEUDE1.Size = new System.Drawing.Size(779, 66);
            this.baocaO_TIEUDE1.TabIndex = 1;
            this.baocaO_TIEUDE1.TIEUDE = "THẺ THUỐC KHO CHẴN";
            this.baocaO_TIEUDE1.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 636);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ngày in ";
            // 
            // dtNgayIn
            // 
            this.dtNgayIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtNgayIn.CustomFormat = "dd/MM/yyyy";
            this.dtNgayIn.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtNgayIn.DropDownCalendar.Name = "";
            this.dtNgayIn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayIn.Location = new System.Drawing.Point(69, 634);
            this.dtNgayIn.Name = "dtNgayIn";
            this.dtNgayIn.ShowUpDown = true;
            this.dtNgayIn.Size = new System.Drawing.Size(134, 21);
            this.dtNgayIn.TabIndex = 13;
            // 
            // cmdBaoCao
            // 
            this.cmdBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBaoCao.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("cmdBaoCao.Image")));
            this.cmdBaoCao.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdBaoCao.Location = new System.Drawing.Point(513, 628);
            this.cmdBaoCao.Name = "cmdBaoCao";
            this.cmdBaoCao.Size = new System.Drawing.Size(124, 30);
            this.cmdBaoCao.TabIndex = 9;
            this.cmdBaoCao.Text = "In báo cáo";
            this.cmdBaoCao.Click += new System.EventHandler(this.cmdBaoCao_Click_1);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(643, 628);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(124, 30);
            this.cmdExit.TabIndex = 11;
            this.cmdExit.Text = "Thoát ";
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.chkSimple);
            this.uiGroupBox1.Controls.Add(this.chkChanle);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.lblLoaikho);
            this.uiGroupBox1.Controls.Add(this.chkThekhochitiet);
            this.uiGroupBox1.Controls.Add(this.panel2);
            this.uiGroupBox1.Controls.Add(this.txtthuoc);
            this.uiGroupBox1.Controls.Add(this.txtLoaithuoc);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.label7);
            this.uiGroupBox1.Controls.Add(this.chkBiendong);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.cboKho);
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 66);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(779, 553);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin báo cáo";
            // 
            // chkSimple
            // 
            this.chkSimple.Location = new System.Drawing.Point(141, 110);
            this.chkSimple.Name = "chkSimple";
            this.chkSimple.Size = new System.Drawing.Size(146, 23);
            this.chkSimple.TabIndex = 5;
            this.chkSimple.Text = "Thẻ xuất nhập tồn?";
            this.toolTip1.SetToolTip(this.chkSimple, "Chọn mục này nếu muốn báo cáo chỉ quan tâm đến 4 yếu tố: Tồn đầu, Nhập, Xuất, Tồn" +
        " cuối. Không chi tiết sâu đến các mức khác");
            // 
            // chkChanle
            // 
            this.chkChanle.Location = new System.Drawing.Point(445, 110);
            this.chkChanle.Name = "chkChanle";
            this.chkChanle.Size = new System.Drawing.Size(188, 23);
            this.chkChanle.TabIndex = 7;
            this.chkChanle.Text = "Báo cáo theo kho chẵn?";
            this.toolTip1.SetToolTip(this.chkChanle, resources.GetString("chkChanle.ToolTip"));
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(336, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 40;
            this.label2.Text = "đến";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLoaikho
            // 
            this.lblLoaikho.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaikho.ForeColor = System.Drawing.Color.Navy;
            this.lblLoaikho.Location = new System.Drawing.Point(601, 27);
            this.lblLoaikho.Name = "lblLoaikho";
            this.lblLoaikho.Size = new System.Drawing.Size(166, 20);
            this.lblLoaikho.TabIndex = 39;
            this.lblLoaikho.Text = "(Kho chẵn)";
            this.lblLoaikho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkThekhochitiet
            // 
            this.chkThekhochitiet.Location = new System.Drawing.Point(293, 110);
            this.chkThekhochitiet.Name = "chkThekhochitiet";
            this.chkThekhochitiet.Size = new System.Drawing.Size(146, 23);
            this.chkThekhochitiet.TabIndex = 6;
            this.chkThekhochitiet.Text = "Thẻ kho chi tiết?";
            this.toolTip1.SetToolTip(this.chkThekhochitiet, "Chọn mục này nếu muốn báo cáo theo mức chi tiết(Thông tư 22 MS: 04D/BV-01). Nếu k" +
        "hông chọn mục này thì sẽ báo cáo theo mức tổng quát");
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.grdThethuoc);
            this.panel2.Controls.Add(this.grdListKhoChan);
            this.panel2.Controls.Add(this.grdThethuoctutruc);
            this.panel2.Controls.Add(this.grdListChitiet);
            this.panel2.Controls.Add(this.grdListKhole);
            this.panel2.Location = new System.Drawing.Point(6, 159);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(769, 387);
            this.panel2.TabIndex = 37;
            // 
            // grdThethuoctutruc
            // 
            this.grdThethuoctutruc.AlternatingColors = true;
            grdThethuoctutruc_DesignTimeLayout.LayoutString = resources.GetString("grdThethuoctutruc_DesignTimeLayout.LayoutString");
            this.grdThethuoctutruc.DesignTimeLayout = grdThethuoctutruc_DesignTimeLayout;
            this.grdThethuoctutruc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThethuoctutruc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdThethuoctutruc.GroupByBoxVisible = false;
            this.grdThethuoctutruc.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdThethuoctutruc.Location = new System.Drawing.Point(0, 0);
            this.grdThethuoctutruc.Name = "grdThethuoctutruc";
            this.grdThethuoctutruc.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThethuoctutruc.Size = new System.Drawing.Size(769, 387);
            this.grdThethuoctutruc.TabIndex = 13;
            this.grdThethuoctutruc.TabStop = false;
            this.grdThethuoctutruc.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThethuoctutruc.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdThethuoctutruc.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdListChitiet
            // 
            this.grdListChitiet.AlternatingColors = true;
            grdListChitiet_DesignTimeLayout.LayoutString = resources.GetString("grdListChitiet_DesignTimeLayout.LayoutString");
            this.grdListChitiet.DesignTimeLayout = grdListChitiet_DesignTimeLayout;
            this.grdListChitiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListChitiet.GroupByBoxVisible = false;
            this.grdListChitiet.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdListChitiet.Location = new System.Drawing.Point(0, 0);
            this.grdListChitiet.Name = "grdListChitiet";
            this.grdListChitiet.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListChitiet.Size = new System.Drawing.Size(769, 387);
            this.grdListChitiet.TabIndex = 12;
            this.grdListChitiet.TabStop = false;
            this.grdListChitiet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListChitiet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdListChitiet.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdListKhole
            // 
            this.grdListKhole.AlternatingColors = true;
            grdListKhole_DesignTimeLayout.LayoutString = resources.GetString("grdListKhole_DesignTimeLayout.LayoutString");
            this.grdListKhole.DesignTimeLayout = grdListKhole_DesignTimeLayout;
            this.grdListKhole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListKhole.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdListKhole.GroupByBoxVisible = false;
            this.grdListKhole.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdListKhole.Location = new System.Drawing.Point(0, 0);
            this.grdListKhole.Name = "grdListKhole";
            this.grdListKhole.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListKhole.Size = new System.Drawing.Size(769, 387);
            this.grdListKhole.TabIndex = 12;
            this.grdListKhole.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListKhole.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdListKhole.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdThethuoc
            // 
            this.grdThethuoc.AlternatingColors = true;
            grdThethuoc_DesignTimeLayout.LayoutString = resources.GetString("grdThethuoc_DesignTimeLayout.LayoutString");
            this.grdThethuoc.DesignTimeLayout = grdThethuoc_DesignTimeLayout;
            this.grdThethuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThethuoc.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdThethuoc.GroupByBoxVisible = false;
            this.grdThethuoc.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdThethuoc.Location = new System.Drawing.Point(0, 0);
            this.grdThethuoc.Name = "grdThethuoc";
            this.grdThethuoc.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThethuoc.Size = new System.Drawing.Size(769, 387);
            this.grdThethuoc.TabIndex = 12;
            this.grdThethuoc.TabStop = false;
            this.grdThethuoc.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdThethuoc.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdThethuoc.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // grdListKhoChan
            // 
            this.grdListKhoChan.AlternatingColors = true;
            grdListKhoChan_DesignTimeLayout.LayoutString = resources.GetString("grdListKhoChan_DesignTimeLayout.LayoutString");
            this.grdListKhoChan.DesignTimeLayout = grdListKhoChan_DesignTimeLayout;
            this.grdListKhoChan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListKhoChan.GroupByBoxVisible = false;
            this.grdListKhoChan.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdListKhoChan.Location = new System.Drawing.Point(0, 0);
            this.grdListKhoChan.Name = "grdListKhoChan";
            this.grdListKhoChan.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListKhoChan.Size = new System.Drawing.Size(769, 387);
            this.grdListKhoChan.TabIndex = 12;
            this.grdListKhoChan.TabStop = false;
            this.grdListKhoChan.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdListKhoChan.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdListKhoChan.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // txtthuoc
            // 
            this.txtthuoc._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
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
            this.txtthuoc.FillValueAfterSelect = false;
            this.txtthuoc.GridView = false;
            this.txtthuoc.Location = new System.Drawing.Point(141, 54);
            this.txtthuoc.MaxHeight = -1;
            this.txtthuoc.MinTypedCharacters = 2;
            this.txtthuoc.MyCode = "-1";
            this.txtthuoc.MyID = "-1";
            this.txtthuoc.Name = "txtthuoc";
            this.txtthuoc.RaiseEvent = false;
            this.txtthuoc.RaiseEventEnter = false;
            this.txtthuoc.RaiseEventEnterWhenEmpty = false;
            this.txtthuoc.SelectedIndex = -1;
            this.txtthuoc.Size = new System.Drawing.Size(454, 21);
            this.txtthuoc.splitChar = '@';
            this.txtthuoc.splitCharIDAndCode = '#';
            this.txtthuoc.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtthuoc, "Chọn thuốc cần báo cáo");
            this.txtthuoc.txtMyCode = null;
            this.txtthuoc.txtMyCode_Edit = null;
            this.txtthuoc.txtMyID = null;
            this.txtthuoc.txtMyID_Edit = null;
            this.txtthuoc.txtMyName = null;
            this.txtthuoc.txtMyName_Edit = null;
            this.txtthuoc.txtNext = null;
            // 
            // txtLoaithuoc
            // 
            this.txtLoaithuoc._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtLoaithuoc._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaithuoc.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLoaithuoc.AutoCompleteList")));
            this.txtLoaithuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoaithuoc.CaseSensitive = false;
            this.txtLoaithuoc.CompareNoID = true;
            this.txtLoaithuoc.DefaultCode = "-1";
            this.txtLoaithuoc.DefaultID = "-1";
            this.txtLoaithuoc.Drug_ID = null;
            this.txtLoaithuoc.ExtraWidth = 0;
            this.txtLoaithuoc.FillValueAfterSelect = false;
            this.txtLoaithuoc.Location = new System.Drawing.Point(754, 60);
            this.txtLoaithuoc.MaxHeight = -1;
            this.txtLoaithuoc.MinTypedCharacters = 2;
            this.txtLoaithuoc.MyCode = "-1";
            this.txtLoaithuoc.MyID = "-1";
            this.txtLoaithuoc.Name = "txtLoaithuoc";
            this.txtLoaithuoc.RaiseEvent = true;
            this.txtLoaithuoc.RaiseEventEnter = true;
            this.txtLoaithuoc.RaiseEventEnterWhenEmpty = true;
            this.txtLoaithuoc.SelectedIndex = -1;
            this.txtLoaithuoc.Size = new System.Drawing.Size(10, 21);
            this.txtLoaithuoc.splitChar = '@';
            this.txtLoaithuoc.splitCharIDAndCode = '#';
            this.txtLoaithuoc.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtLoaithuoc, "Chọn nhóm thuốc(Có thể nhấn phím space(cách) để hiển thị tất cả các nhóm)");
            this.txtLoaithuoc.txtMyCode = null;
            this.txtLoaithuoc.txtMyCode_Edit = null;
            this.txtLoaithuoc.txtMyID = null;
            this.txtLoaithuoc.txtMyID_Edit = null;
            this.txtLoaithuoc.txtMyName = null;
            this.txtLoaithuoc.txtMyName_Edit = null;
            this.txtLoaithuoc.txtNext = null;
            this.txtLoaithuoc.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(5, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 20);
            this.label6.TabIndex = 33;
            this.label6.Text = "Tên thuốc:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(725, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 20);
            this.label7.TabIndex = 32;
            this.label7.Text = "Nhóm thuốc";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Visible = false;
            // 
            // chkBiendong
            // 
            this.chkBiendong.Location = new System.Drawing.Point(141, 134);
            this.chkBiendong.Name = "chkBiendong";
            this.chkBiendong.Size = new System.Drawing.Size(220, 23);
            this.chkBiendong.TabIndex = 8;
            this.chkBiendong.Text = "Chỉ lấy dữ liệu thuốc có biến động?";
            this.toolTip1.SetToolTip(this.chkBiendong, "Nhấn vào đây nếu chỉ muốn lấy các thuốc có biến động: Nhập hoặc Xuất. Không check" +
        " sẽ lấy tất cả các thuốc trong kho");
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Chọn Kho:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboKho
            // 
            this.cboKho.Location = new System.Drawing.Point(141, 27);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(454, 21);
            this.cboKho.TabIndex = 0;
            this.cboKho.Text = "Kho";
            this.toolTip1.SetToolTip(this.cboKho, "Chọn kho để báo cáo");
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy ";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.Location = new System.Drawing.Point(394, 83);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(200, 21);
            this.dtToDate.TabIndex = 4;
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy ";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.Location = new System.Drawing.Point(141, 83);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(177, 21);
            this.dtFromDate.TabIndex = 3;
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Location = new System.Drawing.Point(5, 83);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(130, 23);
            this.chkByDate.TabIndex = 2;
            this.chkByDate.TabStop = false;
            this.chkByDate.Text = "Ngày biến động từ:";
            this.chkByDate.TextAlignment = Janus.Windows.EditControls.TextAlignment.Center;
            // 
            // cmdExportToExcel
            // 
            this.cmdExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportToExcel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportToExcel.Image")));
            this.cmdExportToExcel.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExportToExcel.Location = new System.Drawing.Point(380, 628);
            this.cmdExportToExcel.Name = "cmdExportToExcel";
            this.cmdExportToExcel.Size = new System.Drawing.Size(124, 30);
            this.cmdExportToExcel.TabIndex = 10;
            this.cmdExportToExcel.Text = "Xuất Excel";
            this.cmdExportToExcel.ToolTipText = "Bạn nhấn nút in phiếu để thực hiện in phiếu xét nghiệm cho bệnh nhân";
            this.cmdExportToExcel.Click += new System.EventHandler(this.cmdExportToExcel_Click);
            // 
            // gridEXExporter1
            // 
            this.gridEXExporter1.GridEX = this.grdListKhoChan;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // frm_thethuoc
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(779, 667);
            this.Controls.Add(this.cmdExportToExcel);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtNgayIn);
            this.Controls.Add(this.cmdBaoCao);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_thethuoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thẻ thuốc kho chẵn";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_thethuoc_KeyDown_1);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdThethuoctutruc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListChitiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListKhole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdThethuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListKhoChan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.CalendarCombo.CalendarCombo dtNgayIn;
        private Janus.Windows.EditControls.UIButton cmdBaoCao;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.GridEX.GridEX grdListKhoChan;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox cboKho;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
        private Janus.Windows.EditControls.UIButton cmdExportToExcel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private Janus.Windows.EditControls.UICheckBox chkBiendong;
        private VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls.BAOCAO_TIEUDE baocaO_TIEUDE1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private VNS.HIS.UCs.AutoCompleteTextbox txtLoaithuoc;
        private VNS.HIS.UCs.AutoCompleteTextbox_Thuoc txtthuoc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLoaikho;
        private Janus.Windows.EditControls.UICheckBox chkThekhochitiet;
        private Janus.Windows.GridEX.GridEX grdListChitiet;
        private Janus.Windows.GridEX.GridEX grdListKhole;
        private Janus.Windows.EditControls.UICheckBox chkChanle;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UICheckBox chkSimple;
        private Janus.Windows.GridEX.GridEX grdThethuoc;
        private Janus.Windows.GridEX.GridEX grdThethuoctutruc;
    }
}