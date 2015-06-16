namespace VNS.HIS.UI.THUOC
{
    partial class frm_UpdateSoLuongTon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_UpdateSoLuongTon));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdDieuchinh_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdCauHinh = new Janus.Windows.EditControls.UIButton();
            this.chkTamdung = new Janus.Windows.EditControls.UICheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboKho = new Janus.Windows.EditControls.UIComboBox();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.pnlDieuchinh = new System.Windows.Forms.Panel();
            this.optUutien = new System.Windows.Forms.RadioButton();
            this.optExpireDate = new System.Windows.Forms.RadioButton();
            this.optLIFO = new System.Windows.Forms.RadioButton();
            this.optFIFO = new System.Windows.Forms.RadioButton();
            this.grdDieuchinh = new Janus.Windows.GridEX.GridEX();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.lnkNgayhethan = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdDown = new Janus.Windows.EditControls.UIButton();
            this.cmdUp = new Janus.Windows.EditControls.UIButton();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkAutoupdate = new Janus.Windows.EditControls.UICheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.pnlDieuchinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDieuchinh)).BeginInit();
            this.pnlNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(363, 694);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(135, 31);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.Text = "&Lưu thông tin ";
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(509, 694);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(115, 31);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "&Thoát Form";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmdCauHinh);
            this.uiGroupBox1.Controls.Add(this.chkTamdung);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.cboKho);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(1008, 61);
            this.uiGroupBox1.TabIndex = 2;
            // 
            // cmdCauHinh
            // 
            this.cmdCauHinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCauHinh.Image = ((System.Drawing.Image)(resources.GetObject("cmdCauHinh.Image")));
            this.cmdCauHinh.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdCauHinh.Location = new System.Drawing.Point(963, 12);
            this.cmdCauHinh.Name = "cmdCauHinh";
            this.cmdCauHinh.Size = new System.Drawing.Size(39, 33);
            this.cmdCauHinh.TabIndex = 462;
            // 
            // chkTamdung
            // 
            this.chkTamdung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTamdung.Checked = true;
            this.chkTamdung.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTamdung.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTamdung.Location = new System.Drawing.Point(616, 18);
            this.chkTamdung.Name = "chkTamdung";
            this.chkTamdung.Size = new System.Drawing.Size(228, 23);
            this.chkTamdung.TabIndex = 5;
            this.chkTamdung.Text = "Tạm dừng lấy dữ liệu tự động?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chọn kho thuốc cần xem";
            // 
            // cboKho
            // 
            this.cboKho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKho.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboKho.Location = new System.Drawing.Point(172, 19);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(421, 22);
            this.cboKho.TabIndex = 0;
            this.cboKho.Text = "Kho thuốc";
            this.cboKho.SelectedIndexChanged += new System.EventHandler(this.cboKho_SelectedIndexChanged);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Controls.Add(this.pnlDieuchinh);
            this.uiGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 61);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(1008, 627);
            this.uiGroupBox2.TabIndex = 3;
            this.uiGroupBox2.Text = "&Thông tin số lượng tồn";
            // 
            // grdList
            // 
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdList.GroupByBoxVisible = false;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 19);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.Size = new System.Drawing.Size(1002, 332);
            this.grdList.TabIndex = 2;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // pnlDieuchinh
            // 
            this.pnlDieuchinh.Controls.Add(this.optUutien);
            this.pnlDieuchinh.Controls.Add(this.optExpireDate);
            this.pnlDieuchinh.Controls.Add(this.optLIFO);
            this.pnlDieuchinh.Controls.Add(this.optFIFO);
            this.pnlDieuchinh.Controls.Add(this.grdDieuchinh);
            this.pnlDieuchinh.Controls.Add(this.pnlNav);
            this.pnlDieuchinh.Controls.Add(this.vbLine1);
            this.pnlDieuchinh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDieuchinh.Location = new System.Drawing.Point(3, 351);
            this.pnlDieuchinh.Name = "pnlDieuchinh";
            this.pnlDieuchinh.Size = new System.Drawing.Size(1002, 273);
            this.pnlDieuchinh.TabIndex = 1;
            // 
            // optUutien
            // 
            this.optUutien.AutoSize = true;
            this.optUutien.Checked = true;
            this.optUutien.Location = new System.Drawing.Point(647, 28);
            this.optUutien.Name = "optUutien";
            this.optUutien.Size = new System.Drawing.Size(172, 21);
            this.optUutien.TabIndex = 10;
            this.optUutien.TabStop = true;
            this.optUutien.Text = "Theo mức ưu tiên bán?";
            this.toolTip1.SetToolTip(this.optUutien, "Chọn mục này nếu muốn xuất thuốc theo thứ tự bán, sau đó đến ngày hết hạn gần nhấ" +
        "t");
            this.optUutien.UseVisualStyleBackColor = true;
            // 
            // optExpireDate
            // 
            this.optExpireDate.AutoSize = true;
            this.optExpireDate.Location = new System.Drawing.Point(382, 28);
            this.optExpireDate.Name = "optExpireDate";
            this.optExpireDate.Size = new System.Drawing.Size(245, 21);
            this.optExpireDate.TabIndex = 9;
            this.optExpireDate.TabStop = true;
            this.optExpireDate.Text = "Ngày hết hạn gần nhất xuất trước?";
            this.toolTip1.SetToolTip(this.optExpireDate, "Chọn mục này nếu muốn trừ các thuốc gần hết hạn trước, sau đó đến nhập trước xuất" +
        " trước");
            this.optExpireDate.UseVisualStyleBackColor = true;
            // 
            // optLIFO
            // 
            this.optLIFO.AutoSize = true;
            this.optLIFO.Location = new System.Drawing.Point(197, 28);
            this.optLIFO.Name = "optLIFO";
            this.optLIFO.Size = new System.Drawing.Size(161, 21);
            this.optLIFO.TabIndex = 8;
            this.optLIFO.TabStop = true;
            this.optLIFO.Text = "Nhập trước xuất sau?";
            this.toolTip1.SetToolTip(this.optLIFO, "Chọn mục này nếu muốn xuất thuốc nhập sau xuất trước, kế đến là ngày hết hạn");
            this.optLIFO.UseVisualStyleBackColor = true;
            // 
            // optFIFO
            // 
            this.optFIFO.AutoSize = true;
            this.optFIFO.Location = new System.Drawing.Point(9, 28);
            this.optFIFO.Name = "optFIFO";
            this.optFIFO.Size = new System.Drawing.Size(170, 21);
            this.optFIFO.TabIndex = 7;
            this.optFIFO.TabStop = true;
            this.optFIFO.Text = "Nhập trước xuất trước?";
            this.toolTip1.SetToolTip(this.optFIFO, "Chọn mục này nếu muốn xuất các thuốc nhập trước, kế đến là ngày hết hạn");
            this.optFIFO.UseVisualStyleBackColor = true;
            // 
            // grdDieuchinh
            // 
            this.grdDieuchinh.AllowColumnDrag = false;
            this.grdDieuchinh.AutomaticSort = false;
            this.grdDieuchinh.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
    " thông tin cập nhập số lượng tồn</FilterRowInfoText></LocalizableData>";
            grdDieuchinh_DesignTimeLayout.LayoutString = resources.GetString("grdDieuchinh_DesignTimeLayout.LayoutString");
            this.grdDieuchinh.DesignTimeLayout = grdDieuchinh_DesignTimeLayout;
            this.grdDieuchinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDieuchinh.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdDieuchinh.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdDieuchinh.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdDieuchinh.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdDieuchinh.Font = new System.Drawing.Font("Arial", 9.75F);
            this.grdDieuchinh.GroupByBoxVisible = false;
            this.grdDieuchinh.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdDieuchinh.Location = new System.Drawing.Point(0, 64);
            this.grdDieuchinh.Name = "grdDieuchinh";
            this.grdDieuchinh.RecordNavigator = true;
            this.grdDieuchinh.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDieuchinh.Size = new System.Drawing.Size(841, 209);
            this.grdDieuchinh.TabIndex = 6;
            this.grdDieuchinh.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdDieuchinh.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdDieuchinh.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.lnkNgayhethan);
            this.pnlNav.Controls.Add(this.label2);
            this.pnlNav.Controls.Add(this.cmdDown);
            this.pnlNav.Controls.Add(this.cmdUp);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlNav.Location = new System.Drawing.Point(841, 64);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(161, 209);
            this.pnlNav.TabIndex = 5;
            // 
            // lnkNgayhethan
            // 
            this.lnkNgayhethan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lnkNgayhethan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkNgayhethan.Location = new System.Drawing.Point(0, 63);
            this.lnkNgayhethan.Name = "lnkNgayhethan";
            this.lnkNgayhethan.Size = new System.Drawing.Size(161, 55);
            this.lnkNgayhethan.TabIndex = 4;
            this.lnkNgayhethan.TabStop = true;
            this.lnkNgayhethan.Text = "Sắp xếp ưu tiên bán theo ngày hết hạn gần nhất?";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(0, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 91);
            this.label2.TabIndex = 3;
            this.label2.Text = "Chú ý: Nếu 2 thuốc có cùng số thứ tự và ngày hết hạn khác nhau thì ưu tiên lấy ng" +
    "ày hết hạn gần nhất để bán trước";
            // 
            // cmdDown
            // 
            this.cmdDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdDown.Image")));
            this.cmdDown.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdDown.Location = new System.Drawing.Point(84, 3);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Size = new System.Drawing.Size(74, 60);
            this.cmdDown.TabIndex = 2;
            // 
            // cmdUp
            // 
            this.cmdUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdUp.Image")));
            this.cmdUp.ImageSize = new System.Drawing.Size(32, 32);
            this.cmdUp.Location = new System.Drawing.Point(6, 3);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Size = new System.Drawing.Size(74, 60);
            this.cmdUp.TabIndex = 1;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.Black;
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.vbLine1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(0, 0);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(1002, 64);
            this.vbLine1.TabIndex = 4;
            this.vbLine1.YourText = "Điều chỉnh độ ưu tiên xuất thuốc trong kho";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkAutoupdate
            // 
            this.chkAutoupdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAutoupdate.Checked = true;
            this.chkAutoupdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoupdate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoupdate.Location = new System.Drawing.Point(3, 695);
            this.chkAutoupdate.Name = "chkAutoupdate";
            this.chkAutoupdate.Size = new System.Drawing.Size(279, 23);
            this.chkAutoupdate.TabIndex = 6;
            this.chkAutoupdate.Text = "Tự động cập nhật không cần nhấn nút Lưu?";
            this.toolTip1.SetToolTip(this.chkAutoupdate, "Nếu chọn mục này thì dữ liệu sẽ được cập nhật ngay sau khi thay đổi giá trị thay " +
        "vì phải nhấn nút Lưu thông tin");
            // 
            // frm_UpdateSoLuongTon
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.chkAutoupdate);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.KeyPreview = true;
            this.Name = "frm_UpdateSoLuongTon";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem và điều chỉnh ưu tiên bán thuốc trong kho";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_UpdateSoLuongTon_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_UpdateSoLuongTon_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.pnlDieuchinh.ResumeLayout(false);
            this.pnlDieuchinh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDieuchinh)).EndInit();
            this.pnlNav.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIComboBox cboKho;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlDieuchinh;
        private Janus.Windows.EditControls.UICheckBox chkTamdung;
        private Janus.Windows.GridEX.GridEX grdDieuchinh;
        private System.Windows.Forms.Panel pnlNav;
        private Janus.Windows.EditControls.UIButton cmdDown;
        private Janus.Windows.EditControls.UIButton cmdUp;
        private VNS.UCs.VBLine vbLine1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkNgayhethan;
        private Janus.Windows.GridEX.GridEX grdList;
        private Janus.Windows.EditControls.UICheckBox chkAutoupdate;
        private Janus.Windows.EditControls.UIButton cmdCauHinh;
        private System.Windows.Forms.RadioButton optUutien;
        private System.Windows.Forms.RadioButton optExpireDate;
        private System.Windows.Forms.RadioButton optLIFO;
        private System.Windows.Forms.RadioButton optFIFO;
    }
}