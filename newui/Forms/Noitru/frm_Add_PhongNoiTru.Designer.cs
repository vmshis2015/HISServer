namespace VNS.HIS.UI.NOITRU
{
    partial class frm_Add_PhongNoiTru
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Add_PhongNoiTru));
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.chkAutoupdate = new Janus.Windows.EditControls.UICheckBox();
            this.lblSample = new System.Windows.Forms.Label();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCode2 = new MaskedTextBox.MaskedTextBox();
            this.txtCodefrom = new MaskedTextBox.MaskedTextBox();
            this.chkcodefrom = new System.Windows.Forms.CheckBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboKhoaNoiTru = new Janus.Windows.EditControls.UIComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.chkTrangThai = new Janus.Windows.EditControls.UICheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Mo_Ta = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_STT_HTHI = new Janus.Windows.GridEX.EditControls.IntegerUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTEN = new Janus.Windows.GridEX.EditControls.EditBox();
            this.lblmatiento = new System.Windows.Forms.Label();
            this.txtMa = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkthemmoilientuc = new Janus.Windows.EditControls.UICheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.chkthemmoilientuc);
            this.grpControl.Controls.Add(this.chkAutoupdate);
            this.grpControl.Controls.Add(this.lblSample);
            this.grpControl.Controls.Add(this.lblSuffix);
            this.grpControl.Controls.Add(this.label7);
            this.grpControl.Controls.Add(this.txtCode2);
            this.grpControl.Controls.Add(this.txtCodefrom);
            this.grpControl.Controls.Add(this.chkcodefrom);
            this.grpControl.Controls.Add(this.lblMessage);
            this.grpControl.Controls.Add(this.label6);
            this.grpControl.Controls.Add(this.cboKhoaNoiTru);
            this.grpControl.Controls.Add(this.label5);
            this.grpControl.Controls.Add(this.txtID);
            this.grpControl.Controls.Add(this.chkTrangThai);
            this.grpControl.Controls.Add(this.label4);
            this.grpControl.Controls.Add(this.txt_Mo_Ta);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.txt_STT_HTHI);
            this.grpControl.Controls.Add(this.label2);
            this.grpControl.Controls.Add(this.txtTEN);
            this.grpControl.Controls.Add(this.lblmatiento);
            this.grpControl.Controls.Add(this.txtMa);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(580, 315);
            this.grpControl.TabIndex = 0;
            this.grpControl.Text = "Thông tin buồng nội trú";
            // 
            // chkAutoupdate
            // 
            this.chkAutoupdate.Checked = true;
            this.chkAutoupdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoupdate.Location = new System.Drawing.Point(248, 234);
            this.chkAutoupdate.Name = "chkAutoupdate";
            this.chkAutoupdate.Size = new System.Drawing.Size(257, 23);
            this.chkAutoupdate.TabIndex = 9;
            this.chkAutoupdate.TabStop = false;
            this.chkAutoupdate.Text = "Cập nhật nếu trùng mã buồng?";
            this.toolTip1.SetToolTip(this.chkAutoupdate, "Check vào đây nếu muốn khi tạo buồng theo lô gặp buồng nào trùng mã sẽ tự động Up" +
        "date các thông tin còn lại. Bỏ check nếu muốn khi trùng mã không làm gì cà");
            // 
            // lblSample
            // 
            this.lblSample.AutoSize = true;
            this.lblSample.Location = new System.Drawing.Point(212, 59);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(97, 15);
            this.lblSample.TabIndex = 26;
            this.lblSample.Text = "(hậu tố theo mã)";
            // 
            // lblSuffix
            // 
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Location = new System.Drawing.Point(339, 115);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new System.Drawing.Size(46, 15);
            this.lblSuffix.TabIndex = 25;
            this.lblSuffix.Text = "(+  mã)";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(212, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 18);
            this.label7.TabIndex = 24;
            this.label7.Text = "đến";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode2
            // 
            this.txtCode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode2.Enabled = false;
            this.txtCode2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode2.Location = new System.Drawing.Point(248, 86);
            this.txtCode2.Masked = MaskedTextBox.Mask.Digit;
            this.txtCode2.Name = "txtCode2";
            this.txtCode2.Size = new System.Drawing.Size(85, 21);
            this.txtCode2.TabIndex = 3;
            this.txtCode2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCodefrom
            // 
            this.txtCodefrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodefrom.Enabled = false;
            this.txtCodefrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodefrom.Location = new System.Drawing.Point(106, 86);
            this.txtCodefrom.Masked = MaskedTextBox.Mask.Digit;
            this.txtCodefrom.Name = "txtCodefrom";
            this.txtCodefrom.Size = new System.Drawing.Size(100, 21);
            this.txtCodefrom.TabIndex = 2;
            this.txtCodefrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkcodefrom
            // 
            this.chkcodefrom.AutoSize = true;
            this.chkcodefrom.Location = new System.Drawing.Point(43, 85);
            this.chkcodefrom.Name = "chkcodefrom";
            this.chkcodefrom.Size = new System.Drawing.Size(57, 19);
            this.chkcodefrom.TabIndex = 1;
            this.chkcodefrom.TabStop = false;
            this.chkcodefrom.Text = "Mã từ";
            this.chkcodefrom.UseVisualStyleBackColor = true;
            this.chkcodefrom.CheckedChanged += new System.EventHandler(this.chkcodefrom_CheckedChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.Control;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(-3, 288);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(571, 23);
            this.lblMessage.TabIndex = 20;
            this.lblMessage.Text = "lblMessage";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMessage.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "Khoa nội trú";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboKhoaNoiTru
            // 
            this.cboKhoaNoiTru.Location = new System.Drawing.Point(106, 140);
            this.cboKhoaNoiTru.Name = "cboKhoaNoiTru";
            this.cboKhoaNoiTru.Size = new System.Drawing.Size(399, 21);
            this.cboKhoaNoiTru.TabIndex = 5;
            this.cboKhoaNoiTru.Text = "Khoa nội trú";
            this.cboKhoaNoiTru.SelectedIndexChanged += new System.EventHandler(this.cboKhoaNoiTru_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(106, 32);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(100, 21);
            this.txtID.TabIndex = 8;
            this.txtID.Text = "Tự sinh";
            this.txtID.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangThai.Location = new System.Drawing.Point(106, 234);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(104, 23);
            this.chkTrangThai.TabIndex = 8;
            this.chkTrangThai.TabStop = false;
            this.chkTrangThai.Text = "Hiệu lực?";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mô tả thêm";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_Mo_Ta
            // 
            this.txt_Mo_Ta.Location = new System.Drawing.Point(106, 207);
            this.txt_Mo_Ta.Name = "txt_Mo_Ta";
            this.txt_Mo_Ta.Size = new System.Drawing.Size(399, 21);
            this.txt_Mo_Ta.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "STT hiển thị";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_STT_HTHI
            // 
            this.txt_STT_HTHI.Location = new System.Drawing.Point(106, 180);
            this.txt_STT_HTHI.Name = "txt_STT_HTHI";
            this.txt_STT_HTHI.Size = new System.Drawing.Size(100, 21);
            this.txt_STT_HTHI.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tên buồng";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTEN
            // 
            this.txtTEN.Location = new System.Drawing.Point(106, 113);
            this.txtTEN.Name = "txtTEN";
            this.txtTEN.Size = new System.Drawing.Size(227, 21);
            this.txtTEN.TabIndex = 4;
            this.txtTEN.TextChanged += new System.EventHandler(this.txtTEN_TextChanged);
            // 
            // lblmatiento
            // 
            this.lblmatiento.Location = new System.Drawing.Point(6, 60);
            this.lblmatiento.Name = "lblmatiento";
            this.lblmatiento.Size = new System.Drawing.Size(94, 18);
            this.lblmatiento.TabIndex = 1;
            this.lblmatiento.Text = "Mã ";
            this.lblmatiento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMa
            // 
            this.txtMa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa.Location = new System.Drawing.Point(106, 59);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(100, 21);
            this.txtMa.TabIndex = 0;
            this.txtMa.TextChanged += new System.EventHandler(this.txtMa_TextChanged);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdExit.Location = new System.Drawing.Point(285, 321);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(106, 32);
            this.cmdExit.TabIndex = 16;
            this.cmdExit.Text = "Thoát(Esc)";
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdSave.Location = new System.Drawing.Point(166, 321);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(106, 32);
            this.cmdSave.TabIndex = 15;
            this.cmdSave.Text = "Ghi(Ctrl+S)";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // chkthemmoilientuc
            // 
            this.chkthemmoilientuc.Checked = true;
            this.chkthemmoilientuc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkthemmoilientuc.Location = new System.Drawing.Point(106, 262);
            this.chkthemmoilientuc.Name = "chkthemmoilientuc";
            this.chkthemmoilientuc.Size = new System.Drawing.Size(227, 23);
            this.chkthemmoilientuc.TabIndex = 10;
            this.chkthemmoilientuc.TabStop = false;
            this.chkthemmoilientuc.Text = "Thêm mới liên tục?";
            // 
            // frm_Add_PhongNoiTru
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(580, 358);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Add_PhongNoiTru";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin phòng nội trú";
            this.Load += new System.EventHandler(this.frm_Add_DCHUNG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cmdExit;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txt_Mo_Ta;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.GridEX.EditControls.IntegerUpDown txt_STT_HTHI;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtTEN;
        private System.Windows.Forms.Label lblmatiento;
        internal Janus.Windows.GridEX.EditControls.EditBox txtMa;
        private Janus.Windows.EditControls.UICheckBox chkTrangThai;
        private System.Windows.Forms.Label label5;
        internal Janus.Windows.GridEX.EditControls.EditBox txtID;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.EditControls.UIComboBox cboKhoaNoiTru;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.CheckBox chkcodefrom;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.Label label7;
        private MaskedTextBox.MaskedTextBox txtCode2;
        private MaskedTextBox.MaskedTextBox txtCodefrom;
        private System.Windows.Forms.Label lblSample;
        private Janus.Windows.EditControls.UICheckBox chkAutoupdate;
        private System.Windows.Forms.ToolTip toolTip1;
        private Janus.Windows.EditControls.UICheckBox chkthemmoilientuc;
    }
}