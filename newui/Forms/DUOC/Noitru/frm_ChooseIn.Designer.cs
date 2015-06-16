namespace VNS.HIS.UI.THUOC
{
    partial class frm_ChooseIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChooseIn));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdCauHinh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboKhoa = new Janus.Windows.EditControls.UIComboBox();
            this.radLinhBoSung = new System.Windows.Forms.RadioButton();
            this.radLinhThuong = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtDenNgay = new System.Windows.Forms.DateTimePicker();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdInBaoCao = new System.Windows.Forms.Button();
            this.chkInNgay = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.chkInNgay);
            this.uiGroupBox1.Controls.Add(this.cmdCauHinh);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.cboKhoa);
            this.uiGroupBox1.Controls.Add(this.radLinhBoSung);
            this.uiGroupBox1.Controls.Add(this.radLinhThuong);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.dtDenNgay);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(420, 196);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin ";
            // 
            // cmdCauHinh
            // 
            this.cmdCauHinh.Image = ((System.Drawing.Image)(resources.GetObject("cmdCauHinh.Image")));
            this.cmdCauHinh.Location = new System.Drawing.Point(390, 0);
            this.cmdCauHinh.Name = "cmdCauHinh";
            this.cmdCauHinh.Size = new System.Drawing.Size(30, 23);
            this.cmdCauHinh.TabIndex = 3;
            this.cmdCauHinh.UseVisualStyleBackColor = true;
            this.cmdCauHinh.Click += new System.EventHandler(this.cmdCauHinh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 18);
            this.label2.TabIndex = 37;
            this.label2.Text = "(Bạn chọn điều kiện để lập phiếu)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 36;
            this.label6.Text = "&Khoa:";
            // 
            // cboKhoa
            // 
            this.cboKhoa.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboKhoa.Location = new System.Drawing.Point(78, 123);
            this.cboKhoa.MaxDropDownItems = 30;
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.Size = new System.Drawing.Size(298, 21);
            this.cboKhoa.TabIndex = 35;
            this.cboKhoa.Text = "   ---Chọn khoa phòng---";
            // 
            // radLinhBoSung
            // 
            this.radLinhBoSung.AutoSize = true;
            this.radLinhBoSung.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLinhBoSung.Location = new System.Drawing.Point(188, 86);
            this.radLinhBoSung.Name = "radLinhBoSung";
            this.radLinhBoSung.Size = new System.Drawing.Size(101, 17);
            this.radLinhBoSung.TabIndex = 32;
            this.radLinhBoSung.Text = "&Lĩnh bổ sung";
            this.radLinhBoSung.UseVisualStyleBackColor = true;
            // 
            // radLinhThuong
            // 
            this.radLinhThuong.AutoSize = true;
            this.radLinhThuong.Checked = true;
            this.radLinhThuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLinhThuong.Location = new System.Drawing.Point(78, 86);
            this.radLinhThuong.Name = "radLinhThuong";
            this.radLinhThuong.Size = new System.Drawing.Size(95, 17);
            this.radLinhThuong.TabIndex = 31;
            this.radLinhThuong.TabStop = true;
            this.radLinhThuong.Text = "Lĩnh thường";
            this.radLinhThuong.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Ngày lập";
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDenNgay.Location = new System.Drawing.Point(78, 49);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Size = new System.Drawing.Size(298, 21);
            this.dtDenNgay.TabIndex = 1;
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExit.Location = new System.Drawing.Point(217, 204);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(120, 29);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "&Thoát (Esc)";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdInBaoCao
            // 
            this.cmdInBaoCao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("cmdInBaoCao.Image")));
            this.cmdInBaoCao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdInBaoCao.Location = new System.Drawing.Point(78, 203);
            this.cmdInBaoCao.Name = "cmdInBaoCao";
            this.cmdInBaoCao.Size = new System.Drawing.Size(133, 29);
            this.cmdInBaoCao.TabIndex = 2;
            this.cmdInBaoCao.Text = "&In báo cáo(F4)";
            this.cmdInBaoCao.UseVisualStyleBackColor = true;
            this.cmdInBaoCao.Click += new System.EventHandler(this.cmdInBaoCao_Click);
            // 
            // chkInNgay
            // 
            this.chkInNgay.AutoSize = true;
            this.chkInNgay.Location = new System.Drawing.Point(78, 160);
            this.chkInNgay.Name = "chkInNgay";
            this.chkInNgay.Size = new System.Drawing.Size(123, 19);
            this.chkInNgay.TabIndex = 38;
            this.chkInNgay.Text = "&In thẳng ra máy in";
            this.chkInNgay.UseVisualStyleBackColor = true;
            this.chkInNgay.CheckedChanged += new System.EventHandler(this.chkInNgay_CheckedChanged);
            // 
            // frm_ChooseIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 242);
            this.Controls.Add(this.cmdInBaoCao);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.uiGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ChooseIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu tam tra";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_ChooseIn_FormClosing);
            this.Load += new System.EventHandler(this.frm_ChooseIn_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ChooseIn_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdInBaoCao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtDenNgay;
        private System.Windows.Forms.RadioButton radLinhBoSung;
        private System.Windows.Forms.RadioButton radLinhThuong;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.EditControls.UIComboBox cboKhoa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdCauHinh;
        private System.Windows.Forms.CheckBox chkInNgay;
    }
}