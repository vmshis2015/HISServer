namespace VNS.HIS.UI.THANHTOAN
{
    partial class frm_PhanbotientheoPTTT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_PhanbotientheoPTTT));
            Janus.Windows.GridEX.GridEXLayout grdList_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.cmdAccept = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtPttt = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.pnlInfor = new System.Windows.Forms.Panel();
            this.txtTongtien = new MaskedTextBox.MaskedTextBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.pnlHuyThanhtoan = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNgaythanhtoan = new System.Windows.Forms.Label();
            this.dtPaymentDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSotien = new MaskedTextBox.MaskedTextBox();
            this.grdList = new Janus.Windows.GridEX.GridEX();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlInfor.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlHuyThanhtoan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAccept.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdAccept.Location = new System.Drawing.Point(267, 7);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(140, 44);
            this.cmdAccept.TabIndex = 4;
            this.cmdAccept.Text = "Chấp nhận(Ctrl+S)";
            this.toolTip1.SetToolTip(this.cmdAccept, "Nhấn vào đây để bắt đầu hủy thanh toán cho các mục được chọn trên lưới");
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(20, 20);
            this.cmdExit.Location = new System.Drawing.Point(421, 7);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(140, 44);
            this.cmdExit.TabIndex = 5;
            this.cmdExit.Text = "Thoát(Esc)";
            this.toolTip1.SetToolTip(this.cmdExit, "Nhấn vào đây để hủy bỏ việc phân bổ tiền theo phương thức thanh toán và quay lại " +
        "màn hình chính");
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp nhanh:";
            // 
            // txtPttt
            // 
            this.txtPttt._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtPttt._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPttt.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPttt.AutoCompleteList")));
            this.txtPttt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPttt.CaseSensitive = false;
            this.txtPttt.CompareNoID = true;
            this.txtPttt.DefaultCode = "-1";
            this.txtPttt.DefaultID = "-1";
            this.txtPttt.Drug_ID = null;
            this.txtPttt.ExtraWidth = 0;
            this.txtPttt.FillValueAfterSelect = false;
            this.txtPttt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.txtPttt.LOAI_DANHMUC = "PHUONGTHUCTHANHTOAN";
            this.txtPttt.Location = new System.Drawing.Point(229, 29);
            this.txtPttt.MaxHeight = -1;
            this.txtPttt.MinTypedCharacters = 2;
            this.txtPttt.MyCode = "-1";
            this.txtPttt.MyID = "-1";
            this.txtPttt.Name = "txtPttt";
            this.txtPttt.RaiseEvent = false;
            this.txtPttt.RaiseEventEnter = false;
            this.txtPttt.RaiseEventEnterWhenEmpty = false;
            this.txtPttt.SelectedIndex = -1;
            this.txtPttt.Size = new System.Drawing.Size(276, 26);
            this.txtPttt.splitChar = '@';
            this.txtPttt.splitCharIDAndCode = '#';
            this.txtPttt.TabIndex = 0;
            this.txtPttt.TakeCode = false;
            this.toolTip1.SetToolTip(this.txtPttt, "Nhấn vào đây để xem và bổ sung thêm danh mục dân tộc");
            this.txtPttt.txtMyCode = null;
            this.txtPttt.txtMyCode_Edit = null;
            this.txtPttt.txtMyID = null;
            this.txtPttt.txtMyID_Edit = null;
            this.txtPttt.txtMyName = null;
            this.txtPttt.txtMyName_Edit = null;
            this.txtPttt.txtNext = null;
            // 
            // pnlInfor
            // 
            this.pnlInfor.Controls.Add(this.txtTongtien);
            this.pnlInfor.Controls.Add(this.pnlActions);
            this.pnlInfor.Controls.Add(this.label9);
            this.pnlInfor.Controls.Add(this.lblNgaythanhtoan);
            this.pnlInfor.Controls.Add(this.dtPaymentDate);
            this.pnlInfor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfor.Location = new System.Drawing.Point(0, 469);
            this.pnlInfor.Name = "pnlInfor";
            this.pnlInfor.Size = new System.Drawing.Size(784, 93);
            this.pnlInfor.TabIndex = 8;
            // 
            // txtTongtien
            // 
            this.txtTongtien.BackColor = System.Drawing.Color.White;
            this.txtTongtien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTongtien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongtien.Location = new System.Drawing.Point(533, 7);
            this.txtTongtien.Masked = MaskedTextBox.Mask.Decimal;
            this.txtTongtien.Name = "txtTongtien";
            this.txtTongtien.ReadOnly = true;
            this.txtTongtien.Size = new System.Drawing.Size(220, 22);
            this.txtTongtien.TabIndex = 3;
            this.txtTongtien.TabStop = false;
            this.txtTongtien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.pnlHuyThanhtoan);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 35);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(784, 58);
            this.pnlActions.TabIndex = 0;
            // 
            // pnlHuyThanhtoan
            // 
            this.pnlHuyThanhtoan.Controls.Add(this.cmdAccept);
            this.pnlHuyThanhtoan.Controls.Add(this.cmdExit);
            this.pnlHuyThanhtoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHuyThanhtoan.Location = new System.Drawing.Point(0, 0);
            this.pnlHuyThanhtoan.Name = "pnlHuyThanhtoan";
            this.pnlHuyThanhtoan.Size = new System.Drawing.Size(784, 58);
            this.pnlHuyThanhtoan.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(384, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 24);
            this.label9.TabIndex = 371;
            this.label9.Text = "Tổng tiền";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNgaythanhtoan
            // 
            this.lblNgaythanhtoan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgaythanhtoan.ForeColor = System.Drawing.Color.Navy;
            this.lblNgaythanhtoan.Location = new System.Drawing.Point(3, 7);
            this.lblNgaythanhtoan.Name = "lblNgaythanhtoan";
            this.lblNgaythanhtoan.Size = new System.Drawing.Size(143, 24);
            this.lblNgaythanhtoan.TabIndex = 27;
            this.lblNgaythanhtoan.Text = "Ngày thanh toán:";
            this.lblNgaythanhtoan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtPaymentDate
            // 
            this.dtPaymentDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtPaymentDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtPaymentDate.DropDownCalendar.FirstMonth = new System.DateTime(2014, 4, 1, 0, 0, 0, 0);
            this.dtPaymentDate.DropDownCalendar.Name = "";
            this.dtPaymentDate.Enabled = false;
            this.dtPaymentDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPaymentDate.Location = new System.Drawing.Point(157, 8);
            this.dtPaymentDate.Name = "dtPaymentDate";
            this.dtPaymentDate.ShowUpDown = true;
            this.dtPaymentDate.Size = new System.Drawing.Size(215, 21);
            this.dtPaymentDate.TabIndex = 2;
            this.dtPaymentDate.TabStop = false;
            this.dtPaymentDate.Value = new System.DateTime(2013, 6, 19, 0, 0, 0, 0);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.label1);
            this.uiGroupBox2.Controls.Add(this.txtSotien);
            this.uiGroupBox2.Controls.Add(this.txtPttt);
            this.uiGroupBox2.Controls.Add(this.grdList);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(784, 469);
            this.uiGroupBox2.TabIndex = 9;
            this.uiGroupBox2.Text = "Chi tiết phân bổ tiền theo hình thức thanh toán";
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(511, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 24);
            this.label2.TabIndex = 384;
            this.label2.Text = "Số tiền:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 24);
            this.label1.TabIndex = 383;
            this.label1.Text = "Phương thức thanh toán:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSotien
            // 
            this.txtSotien.BackColor = System.Drawing.Color.White;
            this.txtSotien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSotien.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.txtSotien.Location = new System.Drawing.Point(603, 28);
            this.txtSotien.Masked = MaskedTextBox.Mask.Decimal;
            this.txtSotien.Name = "txtSotien";
            this.txtSotien.Size = new System.Drawing.Size(169, 26);
            this.txtSotien.TabIndex = 1;
            this.txtSotien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grdList
            // 
            this.grdList.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grdList.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><RecordNavigator>Số bả" +
    "n ghi:|/</RecordNavigator></LocalizableData>";
            this.grdList.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            grdList_DesignTimeLayout.LayoutString = resources.GetString("grdList_DesignTimeLayout.LayoutString");
            this.grdList.DesignTimeLayout = grdList_DesignTimeLayout;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdList.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdList.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.FrozenColumns = 10;
            this.grdList.GroupByBoxVisible = false;
            this.grdList.GroupRowFormatStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.grdList.GroupRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.grdList.GroupTotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.grdList.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdList.Location = new System.Drawing.Point(3, 68);
            this.grdList.Name = "grdList";
            this.grdList.RecordNavigator = true;
            this.grdList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.SelectedFormatStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.grdList.Size = new System.Drawing.Size(778, 398);
            this.grdList.TabIndex = 116;
            this.grdList.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.Default;
            this.grdList.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdList.TotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdList.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True;
            this.grdList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed;
            this.grdList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frm_PhanbotientheoPTTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.pnlInfor);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_PhanbotientheoPTTT";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân bổ tiền theo hình thức thanh toán";
            this.pnlInfor.ResumeLayout(false);
            this.pnlInfor.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlHuyThanhtoan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdAccept;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlInfor;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Panel pnlHuyThanhtoan;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblNgaythanhtoan;
        private Janus.Windows.CalendarCombo.CalendarCombo dtPaymentDate;
        private Janus.Windows.GridEX.GridEX grdList;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private MaskedTextBox.MaskedTextBox txtTongtien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MaskedTextBox.MaskedTextBox txtSotien;
        private UCs.AutoCompleteTextbox_Danhmucchung txtPttt;
    }
}