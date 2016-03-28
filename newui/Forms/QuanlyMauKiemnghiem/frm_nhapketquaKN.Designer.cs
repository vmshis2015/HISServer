namespace VNS.HIS.UI.Forms.CanLamSang
{
    partial class frm_nhapketquaKN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_nhapketquaKN));
            Janus.Windows.GridEX.GridEXLayout grdKetqua_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.grpThongTinBN = new Janus.Windows.EditControls.UIGroupBox();
            this.txtDiaChi = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdLamSach = new Janus.Windows.EditControls.UIButton();
            this.txtMahoamau = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.txtPatient_Name = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label23 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkSaveAndConfirm = new System.Windows.Forms.CheckBox();
            this.cmdConfirm = new Janus.Windows.EditControls.UIButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grdKetqua = new Janus.Windows.GridEX.GridEX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctxDelCLS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCancelResult = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTinBN)).BeginInit();
            this.grpThongTinBN.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKetqua)).BeginInit();
            this.ctxDelCLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpThongTinBN
            // 
            this.grpThongTinBN.Controls.Add(this.txtDiaChi);
            this.grpThongTinBN.Controls.Add(this.label1);
            this.grpThongTinBN.Controls.Add(this.cmdLamSach);
            this.grpThongTinBN.Controls.Add(this.txtMahoamau);
            this.grpThongTinBN.Controls.Add(this.label15);
            this.grpThongTinBN.Controls.Add(this.cmdSearch);
            this.grpThongTinBN.Controls.Add(this.txtPatient_Name);
            this.grpThongTinBN.Controls.Add(this.label23);
            this.grpThongTinBN.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinBN.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTinBN.Location = new System.Drawing.Point(0, 0);
            this.grpThongTinBN.Name = "grpThongTinBN";
            this.grpThongTinBN.Size = new System.Drawing.Size(784, 74);
            this.grpThongTinBN.TabIndex = 1;
            this.grpThongTinBN.Text = "Thông tin tìm kiếm";
            this.grpThongTinBN.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.VS2005;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Enabled = false;
            this.txtDiaChi.Location = new System.Drawing.Point(106, 44);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.ReadOnly = true;
            this.txtDiaChi.Size = new System.Drawing.Size(662, 21);
            this.txtDiaChi.TabIndex = 30;
            this.txtDiaChi.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 29;
            this.label1.Text = "Địa chỉ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdLamSach
            // 
            this.cmdLamSach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLamSach.Image = ((System.Drawing.Image)(resources.GetObject("cmdLamSach.Image")));
            this.cmdLamSach.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdLamSach.Location = new System.Drawing.Point(890, 18);
            this.cmdLamSach.Name = "cmdLamSach";
            this.cmdLamSach.Size = new System.Drawing.Size(0, 45);
            this.cmdLamSach.TabIndex = 3;
            this.cmdLamSach.Text = "Làm sạch";
            // 
            // txtMahoamau
            // 
            this.txtMahoamau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtMahoamau.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMahoamau.Location = new System.Drawing.Point(106, 20);
            this.txtMahoamau.Name = "txtMahoamau";
            this.txtMahoamau.Size = new System.Drawing.Size(148, 21);
            this.txtMahoamau.TabIndex = 1;
            this.txtMahoamau.TabStop = false;
            this.toolTip1.SetToolTip(this.txtMahoamau, "Ctrl+F(F3)");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 15);
            this.label15.TabIndex = 15;
            this.label15.Text = "Mã hóa mẫu";
            this.toolTip1.SetToolTip(this.label15, "Nhấn F4 để nhập mã lượt khám. Sau khi nhập gõ Enter để tìm kiếm");
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdSearch.Location = new System.Drawing.Point(774, 18);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(0, 45);
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "Tìm kiếm";
            this.toolTip1.SetToolTip(this.cmdSearch, "Tìm kiếm cận lâm sàng Xé nghiệm để nhập kết quả(Ctrl+F hoặc F3)");
            // 
            // txtPatient_Name
            // 
            this.txtPatient_Name.Enabled = false;
            this.txtPatient_Name.Location = new System.Drawing.Point(382, 18);
            this.txtPatient_Name.Name = "txtPatient_Name";
            this.txtPatient_Name.ReadOnly = true;
            this.txtPatient_Name.Size = new System.Drawing.Size(386, 21);
            this.txtPatient_Name.TabIndex = 3;
            this.txtPatient_Name.TabStop = false;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(259, 21);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(117, 18);
            this.label23.TabIndex = 2;
            this.label23.Text = "Tên Khách hàng";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.chkSaveAndConfirm);
            this.panel1.Controls.Add(this.cmdConfirm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 511);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 51);
            this.panel1.TabIndex = 2;
            // 
            // chkSaveAndConfirm
            // 
            this.chkSaveAndConfirm.AutoSize = true;
            this.chkSaveAndConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSaveAndConfirm.Location = new System.Drawing.Point(12, 19);
            this.chkSaveAndConfirm.Name = "chkSaveAndConfirm";
            this.chkSaveAndConfirm.Size = new System.Drawing.Size(296, 20);
            this.chkSaveAndConfirm.TabIndex = 20;
            this.chkSaveAndConfirm.Text = "Tự động xác nhận ngay sau khi nhập kết quả?";
            this.toolTip1.SetToolTip(this.chkSaveAndConfirm, "Tự động xác nhận ngay sau khi nhập kết quả");
            this.chkSaveAndConfirm.UseVisualStyleBackColor = true;
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConfirm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConfirm.Image = ((System.Drawing.Image)(resources.GetObject("cmdConfirm.Image")));
            this.cmdConfirm.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdConfirm.Location = new System.Drawing.Point(550, 10);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Size = new System.Drawing.Size(109, 30);
            this.cmdConfirm.TabIndex = 5;
            this.cmdConfirm.Text = "Xác nhận";
            this.toolTip1.SetToolTip(this.cmdConfirm, "Ctrl+S(hoặc Ctrl+A)");
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grdKetqua);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 74);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(784, 437);
            this.panel3.TabIndex = 4;
            // 
            // grdKetqua
            // 
            this.grdKetqua.AlternatingColors = true;
            this.grdKetqua.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            grdKetqua_DesignTimeLayout.LayoutString = resources.GetString("grdKetqua_DesignTimeLayout.LayoutString");
            this.grdKetqua.DesignTimeLayout = grdKetqua_DesignTimeLayout;
            this.grdKetqua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKetqua.DynamicFiltering = true;
            this.grdKetqua.FilterMode = Janus.Windows.GridEX.FilterMode.Manual;
            this.grdKetqua.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdKetqua.Font = new System.Drawing.Font("Arial", 9F);
            this.grdKetqua.GroupByBoxVisible = false;
            this.grdKetqua.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed;
            this.grdKetqua.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdKetqua.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdKetqua.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdKetqua.Location = new System.Drawing.Point(0, 0);
            this.grdKetqua.Name = "grdKetqua";
            this.grdKetqua.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKetqua.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdKetqua.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            this.grdKetqua.Size = new System.Drawing.Size(784, 437);
            this.grdKetqua.TabIndex = 255;
            this.grdKetqua.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdKetqua.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grdKetqua.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdKetqua.TotalRowFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.grdKetqua.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdKetqua.UseGroupRowSelector = true;
            this.grdKetqua.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            this.grdKetqua.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.grdKetqua_FormattingRow);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp:";
            // 
            // ctxDelCLS
            // 
            this.ctxDelCLS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCancelResult,
            this.toolStripMenuItem1});
            this.ctxDelCLS.Name = "ctxDelCLS";
            this.ctxDelCLS.Size = new System.Drawing.Size(222, 32);
            // 
            // mnuCancelResult
            // 
            this.mnuCancelResult.CheckOnClick = true;
            this.mnuCancelResult.Name = "mnuCancelResult";
            this.mnuCancelResult.Size = new System.Drawing.Size(221, 22);
            this.mnuCancelResult.Tag = "0";
            this.mnuCancelResult.Text = "Hủy kết quả CLS đang chọn";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(218, 6);
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(665, 10);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(109, 30);
            this.cmdExit.TabIndex = 21;
            this.cmdExit.Text = "Thoát";
            this.toolTip1.SetToolTip(this.cmdExit, "Esc");
            // 
            // frm_nhapketquaKN
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpThongTinBN);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_nhapketquaKN";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập kết quả kiểm nghiệm";
            ((System.ComponentModel.ISupportInitialize)(this.grpThongTinBN)).EndInit();
            this.grpThongTinBN.ResumeLayout(false);
            this.grpThongTinBN.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdKetqua)).EndInit();
            this.ctxDelCLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox grpThongTinBN;
        internal Janus.Windows.EditControls.UIButton cmdLamSach;
        internal Janus.Windows.GridEX.EditControls.EditBox txtMahoamau;
        private System.Windows.Forms.Label label15;
        internal Janus.Windows.EditControls.UIButton cmdSearch;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatient_Name;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private Janus.Windows.GridEX.GridEX grdKetqua;
        private System.Windows.Forms.CheckBox chkSaveAndConfirm;
        private Janus.Windows.EditControls.UIButton cmdConfirm;
        private Janus.Windows.GridEX.EditControls.EditBox txtDiaChi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip ctxDelCLS;
        private System.Windows.Forms.ToolStripMenuItem mnuCancelResult;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private Janus.Windows.EditControls.UIButton cmdExit;
    }
}