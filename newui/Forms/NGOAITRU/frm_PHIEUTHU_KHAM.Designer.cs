namespace VNS.HIS.UI.NGOAITRU
{
    partial class frm_PHIEUTHU_KHAM
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
            Janus.Windows.GridEX.GridEXLayout grdPaymentDetail_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PHIEUTHU_KHAM));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdPaymentDetail = new Janus.Windows.GridEX.GridEX();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTieuDe = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLDO_NOP = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNGUOI_NOP = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTKHOAN_CO = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTKHOAN_NO = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSLUONG_CTU_GOC = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSO_TIEN = new Janus.Windows.GridEX.EditControls.EditBox();
            this.dtCreateDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPayment_ID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMA_PTHU = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdInPhieu = new Janus.Windows.EditControls.UIButton();
            this.cmdPrint = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.grdPaymentDetail);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(932, 398);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "&Thông tin phiếu thu";
            // 
            // grdPaymentDetail
            // 
            this.grdPaymentDetail.AlternatingColors = true;
            this.grdPaymentDetail.BackColor = System.Drawing.Color.Silver;
            grdPaymentDetail_DesignTimeLayout.LayoutString = resources.GetString("grdPaymentDetail_DesignTimeLayout.LayoutString");
            this.grdPaymentDetail.DesignTimeLayout = grdPaymentDetail_DesignTimeLayout;
            this.grdPaymentDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPaymentDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdPaymentDetail.GroupByBoxFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdPaymentDetail.GroupByBoxVisible = false;
            this.grdPaymentDetail.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPaymentDetail.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Navy;
            this.grdPaymentDetail.GroupTotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdPaymentDetail.GroupTotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdPaymentDetail.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdPaymentDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdPaymentDetail.Location = new System.Drawing.Point(3, 16);
            this.grdPaymentDetail.Name = "grdPaymentDetail";
            this.grdPaymentDetail.Size = new System.Drawing.Size(926, 379);
            this.grdPaymentDetail.TabIndex = 5;
            this.grdPaymentDetail.TabStop = false;
            this.grdPaymentDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdPaymentDetail.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdPaymentDetail.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdPaymentDetail.TotalRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            this.grdPaymentDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdPaymentDetail.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.grdPaymentDetail_FormattingRow);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.label10);
            this.uiGroupBox2.Controls.Add(this.txtTieuDe);
            this.uiGroupBox2.Controls.Add(this.label4);
            this.uiGroupBox2.Controls.Add(this.txtLDO_NOP);
            this.uiGroupBox2.Controls.Add(this.label9);
            this.uiGroupBox2.Controls.Add(this.txtNGUOI_NOP);
            this.uiGroupBox2.Controls.Add(this.label8);
            this.uiGroupBox2.Controls.Add(this.txtTKHOAN_CO);
            this.uiGroupBox2.Controls.Add(this.label7);
            this.uiGroupBox2.Controls.Add(this.txtTKHOAN_NO);
            this.uiGroupBox2.Controls.Add(this.label6);
            this.uiGroupBox2.Controls.Add(this.txtSLUONG_CTU_GOC);
            this.uiGroupBox2.Controls.Add(this.label5);
            this.uiGroupBox2.Controls.Add(this.txtSO_TIEN);
            this.uiGroupBox2.Controls.Add(this.dtCreateDate);
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.txtPayment_ID);
            this.uiGroupBox2.Controls.Add(this.label3);
            this.uiGroupBox2.Controls.Add(this.txtMA_PTHU);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 398);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(932, 188);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "&Thông tin phiếu thu";
            this.uiGroupBox2.Click += new System.EventHandler(this.uiGroupBox2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Navy;
            this.label10.Location = new System.Drawing.Point(23, 162);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 510;
            this.label10.Text = "&Tiêu đề";
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTieuDe.ForeColor = System.Drawing.Color.Navy;
            this.txtTieuDe.Location = new System.Drawing.Point(143, 162);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(249, 23);
            this.txtTieuDe.TabIndex = 510;
            this.txtTieuDe.Text = "PHIẾU KHÁM BỆNH";
            this.txtTieuDe.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(23, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 509;
            this.label4.Text = "&Lý do thu";
            // 
            // txtLDO_NOP
            // 
            this.txtLDO_NOP.Location = new System.Drawing.Point(143, 135);
            this.txtLDO_NOP.MaxLength = 255;
            this.txtLDO_NOP.Name = "txtLDO_NOP";
            this.txtLDO_NOP.Size = new System.Drawing.Size(562, 20);
            this.txtLDO_NOP.TabIndex = 494;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Navy;
            this.label9.Location = new System.Drawing.Point(23, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 508;
            this.label9.Text = "&Người nộp";
            // 
            // txtNGUOI_NOP
            // 
            this.txtNGUOI_NOP.Location = new System.Drawing.Point(143, 107);
            this.txtNGUOI_NOP.Name = "txtNGUOI_NOP";
            this.txtNGUOI_NOP.Size = new System.Drawing.Size(216, 20);
            this.txtNGUOI_NOP.TabIndex = 492;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(380, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 507;
            this.label8.Text = "&Tài khoản có";
            // 
            // txtTKHOAN_CO
            // 
            this.txtTKHOAN_CO.Location = new System.Drawing.Point(483, 111);
            this.txtTKHOAN_CO.Name = "txtTKHOAN_CO";
            this.txtTKHOAN_CO.Size = new System.Drawing.Size(222, 20);
            this.txtTKHOAN_CO.TabIndex = 498;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(380, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 506;
            this.label7.Text = "&Tài khoản nợ";
            // 
            // txtTKHOAN_NO
            // 
            this.txtTKHOAN_NO.Location = new System.Drawing.Point(483, 84);
            this.txtTKHOAN_NO.Name = "txtTKHOAN_NO";
            this.txtTKHOAN_NO.Size = new System.Drawing.Size(222, 20);
            this.txtTKHOAN_NO.TabIndex = 497;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(380, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 505;
            this.label6.Text = "&Số lượng CT gốc";
            // 
            // txtSLUONG_CTU_GOC
            // 
            this.txtSLUONG_CTU_GOC.Location = new System.Drawing.Point(483, 57);
            this.txtSLUONG_CTU_GOC.Name = "txtSLUONG_CTU_GOC";
            this.txtSLUONG_CTU_GOC.Size = new System.Drawing.Size(222, 20);
            this.txtSLUONG_CTU_GOC.TabIndex = 496;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(380, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 504;
            this.label5.Text = "&Số tiền";
            // 
            // txtSO_TIEN
            // 
            this.txtSO_TIEN.Enabled = false;
            this.txtSO_TIEN.Location = new System.Drawing.Point(483, 30);
            this.txtSO_TIEN.Name = "txtSO_TIEN";
            this.txtSO_TIEN.Size = new System.Drawing.Size(222, 20);
            this.txtSO_TIEN.TabIndex = 495;
            // 
            // dtCreateDate
            // 
            this.dtCreateDate.CustomFormat = "dd/MM/yyyy";
            this.dtCreateDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtCreateDate.DropDownCalendar.Name = "";
            this.dtCreateDate.Location = new System.Drawing.Point(143, 28);
            this.dtCreateDate.Name = "dtCreateDate";
            this.dtCreateDate.ShowUpDown = true;
            this.dtCreateDate.Size = new System.Drawing.Size(216, 20);
            this.dtCreateDate.TabIndex = 503;
            this.dtCreateDate.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(22, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 502;
            this.label2.Text = "&Ngày thực hiện";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(23, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 501;
            this.label1.Text = "&Mã thanh toán";
            // 
            // txtPayment_ID
            // 
            this.txtPayment_ID.Enabled = false;
            this.txtPayment_ID.Location = new System.Drawing.Point(143, 82);
            this.txtPayment_ID.Name = "txtPayment_ID";
            this.txtPayment_ID.Size = new System.Drawing.Size(216, 20);
            this.txtPayment_ID.TabIndex = 500;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(23, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 499;
            this.label3.Text = "&Mã phiếu thu";
            // 
            // txtMA_PTHU
            // 
            this.txtMA_PTHU.Enabled = false;
            this.txtMA_PTHU.Location = new System.Drawing.Point(143, 55);
            this.txtMA_PTHU.Name = "txtMA_PTHU";
            this.txtMA_PTHU.Size = new System.Drawing.Size(216, 20);
            this.txtMA_PTHU.TabIndex = 493;
            // 
            // cmdInPhieu
            // 
            this.cmdInPhieu.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieu.Image")));
            this.cmdInPhieu.Location = new System.Drawing.Point(247, 606);
            this.cmdInPhieu.Name = "cmdInPhieu";
            this.cmdInPhieu.Size = new System.Drawing.Size(114, 30);
            this.cmdInPhieu.TabIndex = 6;
            this.cmdInPhieu.Text = "&In phiếu(F4)";
            this.cmdInPhieu.ToolTipText = "In phiếu thông tin của phần phiếu của phần thanh toán";
            this.cmdInPhieu.Click += new System.EventHandler(this.cmdInPhieu_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.Location = new System.Drawing.Point(367, 606);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(114, 30);
            this.cmdPrint.TabIndex = 4;
            this.cmdPrint.Text = "&In hóa đơn (F5)";
            this.cmdPrint.ToolTipText = "In thông tin hóa đơn phiếu thu";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(487, 606);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(106, 30);
            this.cmdExit.TabIndex = 5;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // frm_PHIEUTHU_KHAM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 643);
            this.Controls.Add(this.cmdInPhieu);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_PHIEUTHU_KHAM";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu thu khám bệnh";
            this.Load += new System.EventHandler(this.frm_PHIEUTHU_KHAM_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_PHIEUTHU_KHAM_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtLDO_NOP;
        private System.Windows.Forms.Label label9;
        private Janus.Windows.GridEX.EditControls.EditBox txtNGUOI_NOP;
        private System.Windows.Forms.Label label8;
        private Janus.Windows.GridEX.EditControls.EditBox txtTKHOAN_CO;
        private System.Windows.Forms.Label label7;
        private Janus.Windows.GridEX.EditControls.EditBox txtTKHOAN_NO;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.GridEX.EditControls.EditBox txtSLUONG_CTU_GOC;
        private System.Windows.Forms.Label label5;
        private Janus.Windows.GridEX.EditControls.EditBox txtSO_TIEN;
        private Janus.Windows.CalendarCombo.CalendarCombo dtCreateDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal Janus.Windows.GridEX.EditControls.EditBox txtPayment_ID;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.EditBox txtMA_PTHU;
        public Janus.Windows.GridEX.GridEX grdPaymentDetail;
        private Janus.Windows.EditControls.UIButton cmdInPhieu;
        private Janus.Windows.EditControls.UIButton cmdPrint;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label label10;
        private Janus.Windows.GridEX.EditControls.EditBox txtTieuDe;
    }
}