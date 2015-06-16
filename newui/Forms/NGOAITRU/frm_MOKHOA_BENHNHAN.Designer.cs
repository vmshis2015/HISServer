namespace  VNS.HIS.UI.NGOAITRU
{
    partial class frm_MOKHOA_BENHNHAN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_MOKHOA_BENHNHAN));
            Janus.Windows.UI.StatusBar.UIStatusBarPanel uiStatusBarPanel3 = new Janus.Windows.UI.StatusBar.UIStatusBarPanel();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.uiButton2 = new Janus.Windows.EditControls.UIButton();
            this.cmdMoKhoa = new Janus.Windows.EditControls.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPatientCode = new Janus.Windows.GridEX.EditControls.EditBox();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtTrangThaiBN = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMaLanKham = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTuoiBN = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNgayVaoKham = new System.Windows.Forms.Label();
            this.txtDiaChiBHYT = new System.Windows.Forms.Label();
            this.txtSoBHYT = new System.Windows.Forms.Label();
            this.txtDoiTuong = new System.Windows.Forms.Label();
            this.txtDiaChiBenhNhan = new System.Windows.Forms.Label();
            this.txtTenBenhNhan = new System.Windows.Forms.Label();
            this.txtMaBenhNhan = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uiStatusBar1 = new Janus.Windows.UI.StatusBar.UIStatusBar();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.txtTrangthai = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.uiButton2);
            this.uiGroupBox1.Controls.Add(this.cmdMoKhoa);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.txtPatientCode);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox1.Image")));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(512, 50);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Thông tin mã lần khám";
            // 
            // uiButton2
            // 
            this.uiButton2.Image = ((System.Drawing.Image)(resources.GetObject("uiButton2.Image")));
            this.uiButton2.Location = new System.Drawing.Point(383, 14);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(122, 28);
            this.uiButton2.TabIndex = 3;
            this.uiButton2.Text = "Khóa";
            // 
            // cmdMoKhoa
            // 
            this.cmdMoKhoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoKhoa.Image")));
            this.cmdMoKhoa.Location = new System.Drawing.Point(255, 14);
            this.cmdMoKhoa.Name = "cmdMoKhoa";
            this.cmdMoKhoa.Size = new System.Drawing.Size(122, 28);
            this.cmdMoKhoa.TabIndex = 2;
            this.cmdMoKhoa.Text = "Mở khóa";
            this.cmdMoKhoa.ToolTipText = "Mở lại lần khám cho bệnh nhân. Cẩn thận khi sử dụng chức năng này";
            this.cmdMoKhoa.Click += new System.EventHandler(this.cmdMoKhoa_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã lần khám";
            // 
            // txtPatientCode
            // 
            this.txtPatientCode.Location = new System.Drawing.Point(90, 19);
            this.txtPatientCode.MaxLength = 8;
            this.txtPatientCode.Name = "txtPatientCode";
            this.txtPatientCode.Size = new System.Drawing.Size(159, 20);
            this.txtPatientCode.TabIndex = 0;
            this.txtPatientCode.TextChanged += new System.EventHandler(this.txtPatientCode_TextChanged);
            this.txtPatientCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientCode_KeyDown);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.txtTrangthai);
            this.uiGroupBox2.Controls.Add(this.label12);
            this.uiGroupBox2.Controls.Add(this.txtTrangThaiBN);
            this.uiGroupBox2.Controls.Add(this.label11);
            this.uiGroupBox2.Controls.Add(this.txtMaLanKham);
            this.uiGroupBox2.Controls.Add(this.label10);
            this.uiGroupBox2.Controls.Add(this.txtTuoiBN);
            this.uiGroupBox2.Controls.Add(this.label9);
            this.uiGroupBox2.Controls.Add(this.txtNgayVaoKham);
            this.uiGroupBox2.Controls.Add(this.txtDiaChiBHYT);
            this.uiGroupBox2.Controls.Add(this.txtSoBHYT);
            this.uiGroupBox2.Controls.Add(this.txtDoiTuong);
            this.uiGroupBox2.Controls.Add(this.txtDiaChiBenhNhan);
            this.uiGroupBox2.Controls.Add(this.txtTenBenhNhan);
            this.uiGroupBox2.Controls.Add(this.txtMaBenhNhan);
            this.uiGroupBox2.Controls.Add(this.label8);
            this.uiGroupBox2.Controls.Add(this.label7);
            this.uiGroupBox2.Controls.Add(this.label6);
            this.uiGroupBox2.Controls.Add(this.label5);
            this.uiGroupBox2.Controls.Add(this.label4);
            this.uiGroupBox2.Controls.Add(this.label3);
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.uiStatusBar1);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox2.Image")));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 50);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(512, 219);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Thông tin bệnh nhân";
            // 
            // txtTrangThaiBN
            // 
            this.txtTrangThaiBN.AutoSize = true;
            this.txtTrangThaiBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrangThaiBN.ForeColor = System.Drawing.Color.Red;
            this.txtTrangThaiBN.Location = new System.Drawing.Point(159, 166);
            this.txtTrangThaiBN.Name = "txtTrangThaiBN";
            this.txtTrangThaiBN.Size = new System.Drawing.Size(106, 15);
            this.txtTrangThaiBN.TabIndex = 20;
            this.txtTrangThaiBN.Text = "txtTrangThaiBN";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 167);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(141, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Trạng thái bệnh nhân : ";
            // 
            // txtMaLanKham
            // 
            this.txtMaLanKham.AutoSize = true;
            this.txtMaLanKham.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaLanKham.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtMaLanKham.Location = new System.Drawing.Point(383, 28);
            this.txtMaLanKham.Name = "txtMaLanKham";
            this.txtMaLanKham.Size = new System.Drawing.Size(103, 15);
            this.txtMaLanKham.TabIndex = 18;
            this.txtMaLanKham.Text = "txtMaLanKham";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(280, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Mã Bệnh nhân :";
            // 
            // txtTuoiBN
            // 
            this.txtTuoiBN.AutoSize = true;
            this.txtTuoiBN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTuoiBN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtTuoiBN.Location = new System.Drawing.Point(424, 51);
            this.txtTuoiBN.Name = "txtTuoiBN";
            this.txtTuoiBN.Size = new System.Drawing.Size(69, 15);
            this.txtTuoiBN.TabIndex = 16;
            this.txtTuoiBN.Text = "txtTuoiBN";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(316, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Tuổi bệnh nhân :";
            // 
            // txtNgayVaoKham
            // 
            this.txtNgayVaoKham.AutoSize = true;
            this.txtNgayVaoKham.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNgayVaoKham.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtNgayVaoKham.Location = new System.Drawing.Point(114, 143);
            this.txtNgayVaoKham.Name = "txtNgayVaoKham";
            this.txtNgayVaoKham.Size = new System.Drawing.Size(115, 15);
            this.txtNgayVaoKham.TabIndex = 14;
            this.txtNgayVaoKham.Text = "txtNgayVaoKham";
            // 
            // txtDiaChiBHYT
            // 
            this.txtDiaChiBHYT.AutoSize = true;
            this.txtDiaChiBHYT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiaChiBHYT.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtDiaChiBHYT.Location = new System.Drawing.Point(68, 120);
            this.txtDiaChiBHYT.Name = "txtDiaChiBHYT";
            this.txtDiaChiBHYT.Size = new System.Drawing.Size(100, 15);
            this.txtDiaChiBHYT.TabIndex = 13;
            this.txtDiaChiBHYT.Text = "txtDiaChiBHYT";
            // 
            // txtSoBHYT
            // 
            this.txtSoBHYT.AutoSize = true;
            this.txtSoBHYT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoBHYT.ForeColor = System.Drawing.Color.Red;
            this.txtSoBHYT.Location = new System.Drawing.Point(280, 97);
            this.txtSoBHYT.Name = "txtSoBHYT";
            this.txtSoBHYT.Size = new System.Drawing.Size(74, 15);
            this.txtSoBHYT.TabIndex = 12;
            this.txtSoBHYT.Text = "txtSoBHYT";
            // 
            // txtDoiTuong
            // 
            this.txtDoiTuong.AutoSize = true;
            this.txtDoiTuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoiTuong.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtDoiTuong.Location = new System.Drawing.Point(84, 97);
            this.txtDoiTuong.Name = "txtDoiTuong";
            this.txtDoiTuong.Size = new System.Drawing.Size(84, 15);
            this.txtDoiTuong.TabIndex = 11;
            this.txtDoiTuong.Text = "txtDoiTuong";
            // 
            // txtDiaChiBenhNhan
            // 
            this.txtDiaChiBenhNhan.AutoSize = true;
            this.txtDiaChiBenhNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiaChiBenhNhan.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtDiaChiBenhNhan.Location = new System.Drawing.Point(124, 74);
            this.txtDiaChiBenhNhan.Name = "txtDiaChiBenhNhan";
            this.txtDiaChiBenhNhan.Size = new System.Drawing.Size(132, 15);
            this.txtDiaChiBenhNhan.TabIndex = 10;
            this.txtDiaChiBenhNhan.Text = "txtDiaChiBenhNhan";
            // 
            // txtTenBenhNhan
            // 
            this.txtTenBenhNhan.AutoSize = true;
            this.txtTenBenhNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenBenhNhan.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtTenBenhNhan.Location = new System.Drawing.Point(116, 51);
            this.txtTenBenhNhan.Name = "txtTenBenhNhan";
            this.txtTenBenhNhan.Size = new System.Drawing.Size(113, 15);
            this.txtTenBenhNhan.TabIndex = 9;
            this.txtTenBenhNhan.Text = "txtTenBenhNhan";
            // 
            // txtMaBenhNhan
            // 
            this.txtMaBenhNhan.AutoSize = true;
            this.txtMaBenhNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaBenhNhan.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtMaBenhNhan.Location = new System.Drawing.Point(109, 28);
            this.txtMaBenhNhan.Name = "txtMaBenhNhan";
            this.txtMaBenhNhan.Size = new System.Drawing.Size(109, 15);
            this.txtMaBenhNhan.TabIndex = 8;
            this.txtMaBenhNhan.Text = "txtMaBenhNhan";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Ngày vào khám";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Địa chỉ bệnh nhân";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(209, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Số BHYT :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Địa chỉ :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Đối tượng :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên Bệnh nhân :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã Bệnh nhân :";
            // 
            // uiStatusBar1
            // 
            this.uiStatusBar1.Location = new System.Drawing.Point(3, 193);
            this.uiStatusBar1.Name = "uiStatusBar1";
            uiStatusBarPanel3.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            uiStatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            uiStatusBarPanel3.BorderColor = System.Drawing.Color.Empty;
            uiStatusBarPanel3.FormatStyle.BackColor = System.Drawing.Color.White;
            uiStatusBarPanel3.FormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            uiStatusBarPanel3.FormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            uiStatusBarPanel3.FormatStyle.ForeColor = System.Drawing.Color.Red;
            uiStatusBarPanel3.Key = "";
            uiStatusBarPanel3.ProgressBarValue = 0;
            uiStatusBarPanel3.Text = "Cẩn thận khi sử dụng chức năng mở khóa bệnh nhân";
            uiStatusBarPanel3.Width = 500;
            this.uiStatusBar1.Panels.AddRange(new Janus.Windows.UI.StatusBar.UIStatusBarPanel[] {
            uiStatusBarPanel3});
            this.uiStatusBar1.Size = new System.Drawing.Size(506, 23);
            this.uiStatusBar1.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(279, 167);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Trạng thái :";
            // 
            // txtTrangthai
            // 
            this.txtTrangthai.AutoSize = true;
            this.txtTrangthai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrangthai.ForeColor = System.Drawing.Color.Red;
            this.txtTrangthai.Location = new System.Drawing.Point(358, 166);
            this.txtTrangthai.Name = "txtTrangthai";
            this.txtTrangthai.Size = new System.Drawing.Size(83, 15);
            this.txtTrangthai.TabIndex = 22;
            this.txtTrangthai.Text = "txtTrangthai";
            // 
            // frm_MOKHOA_BENHNHAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 269);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.KeyPreview = true;
            this.Name = "frm_MOKHOA_BENHNHAN";
            this.Text = "MỞ KHÁM BỆNH NHÂN";
            this.Load += new System.EventHandler(this.frm_MOKHOA_BENHNHAN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtPatientCode;
        private Janus.Windows.EditControls.UIButton cmdMoKhoa;
        private Janus.Windows.EditControls.UIButton uiButton2;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.UI.StatusBar.UIStatusBar uiStatusBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label txtMaBenhNhan;
        private System.Windows.Forms.Label txtTenBenhNhan;
        private System.Windows.Forms.Label txtDiaChiBenhNhan;
        private System.Windows.Forms.Label txtNgayVaoKham;
        private System.Windows.Forms.Label txtDiaChiBHYT;
        private System.Windows.Forms.Label txtSoBHYT;
        private System.Windows.Forms.Label txtDoiTuong;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label txtTuoiBN;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label txtMaLanKham;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label txtTrangThaiBN;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label txtTrangthai;
    }
}