namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_themmoi_phongban
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_phongban));
            this.cmdThoat = new System.Windows.Forms.Button();
            this.cmdGhi = new System.Windows.Forms.Button();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtTIEN_TAM_UNG = new MaskedTextBox.MaskedTextBox();
            this.txtDeptFee = new MaskedTextBox.MaskedTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.optKhac = new Janus.Windows.EditControls.UIRadioButton();
            this.optNgoaitru = new Janus.Windows.EditControls.UIRadioButton();
            this.optNoitru = new Janus.Windows.EditControls.UIRadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optChuyenmon = new Janus.Windows.EditControls.UIRadioButton();
            this.optChucnang = new Janus.Windows.EditControls.UIRadioButton();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtDonvitinh = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optKhoa = new Janus.Windows.EditControls.UIRadioButton();
            this.optPhong = new Janus.Windows.EditControls.UIRadioButton();
            this.txtMaphongXepStt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPhong_Thien = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkKhoaCapCuu = new Janus.Windows.EditControls.UICheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkParent = new Janus.Windows.EditControls.UICheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDepartment_Code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtintOrder = new System.Windows.Forms.NumericUpDown();
            this.cboParent_ID = new System.Windows.Forms.ComboBox();
            this.lblintOrder = new System.Windows.Forms.Label();
            this.txtDepartment_Name = new System.Windows.Forms.TextBox();
            this.lblDepartment_Name = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtintOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdThoat
            // 
            this.cmdThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdThoat.Location = new System.Drawing.Point(317, 429);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(116, 31);
            this.cmdThoat.TabIndex = 16;
            this.cmdThoat.Text = "Thoát(Esc)";
            this.cmdThoat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdThoat.UseVisualStyleBackColor = true;
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // cmdGhi
            // 
            this.cmdGhi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGhi.Image = ((System.Drawing.Image)(resources.GetObject("cmdGhi.Image")));
            this.cmdGhi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdGhi.Location = new System.Drawing.Point(158, 428);
            this.cmdGhi.Name = "cmdGhi";
            this.cmdGhi.Size = new System.Drawing.Size(129, 31);
            this.cmdGhi.TabIndex = 15;
            this.cmdGhi.Text = "Lưu(Ctrl+S)";
            this.cmdGhi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdGhi.UseVisualStyleBackColor = true;
            this.cmdGhi.Click += new System.EventHandler(this.cmdGhi_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txtTIEN_TAM_UNG);
            this.uiGroupBox1.Controls.Add(this.txtDeptFee);
            this.uiGroupBox1.Controls.Add(this.groupBox3);
            this.uiGroupBox1.Controls.Add(this.groupBox1);
            this.uiGroupBox1.Controls.Add(this.lblMsg);
            this.uiGroupBox1.Controls.Add(this.txtDonvitinh);
            this.uiGroupBox1.Controls.Add(this.groupBox2);
            this.uiGroupBox1.Controls.Add(this.txtMaphongXepStt);
            this.uiGroupBox1.Controls.Add(this.label9);
            this.uiGroupBox1.Controls.Add(this.txtPhong_Thien);
            this.uiGroupBox1.Controls.Add(this.label8);
            this.uiGroupBox1.Controls.Add(this.label7);
            this.uiGroupBox1.Controls.Add(this.chkKhoaCapCuu);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.chkParent);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.txtDepartment_Code);
            this.uiGroupBox1.Controls.Add(this.label4);
            this.uiGroupBox1.Controls.Add(this.label3);
            this.uiGroupBox1.Controls.Add(this.txtDesc);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.txtID);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtintOrder);
            this.uiGroupBox1.Controls.Add(this.cboParent_ID);
            this.uiGroupBox1.Controls.Add(this.lblintOrder);
            this.uiGroupBox1.Controls.Add(this.txtDepartment_Name);
            this.uiGroupBox1.Controls.Add(this.lblDepartment_Name);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(625, 422);
            this.uiGroupBox1.TabIndex = 3;
            this.uiGroupBox1.Text = "Thông tin khoa phòng";
            // 
            // txtTIEN_TAM_UNG
            // 
            this.txtTIEN_TAM_UNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTIEN_TAM_UNG.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTIEN_TAM_UNG.Location = new System.Drawing.Point(594, 29);
            this.txtTIEN_TAM_UNG.Masked = MaskedTextBox.Mask.Decimal;
            this.txtTIEN_TAM_UNG.Name = "txtTIEN_TAM_UNG";
            this.txtTIEN_TAM_UNG.Size = new System.Drawing.Size(10, 21);
            this.txtTIEN_TAM_UNG.TabIndex = 7;
            this.txtTIEN_TAM_UNG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTIEN_TAM_UNG.Visible = false;
            // 
            // txtDeptFee
            // 
            this.txtDeptFee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeptFee.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeptFee.Location = new System.Drawing.Point(610, 5);
            this.txtDeptFee.Masked = MaskedTextBox.Mask.Decimal;
            this.txtDeptFee.Name = "txtDeptFee";
            this.txtDeptFee.Size = new System.Drawing.Size(10, 21);
            this.txtDeptFee.TabIndex = 6;
            this.txtDeptFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDeptFee.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.optKhac);
            this.groupBox3.Controls.Add(this.optNgoaitru);
            this.groupBox3.Controls.Add(this.optNoitru);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(158, 139);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(308, 45);
            this.groupBox3.TabIndex = 82;
            this.groupBox3.TabStop = false;
            // 
            // optKhac
            // 
            this.optKhac.Location = new System.Drawing.Point(206, 16);
            this.optKhac.Name = "optKhac";
            this.optKhac.Size = new System.Drawing.Size(98, 23);
            this.optKhac.TabIndex = 9;
            this.optKhac.Text = "Khác?";
            // 
            // optNgoaitru
            // 
            this.optNgoaitru.Checked = true;
            this.optNgoaitru.Location = new System.Drawing.Point(10, 18);
            this.optNgoaitru.Name = "optNgoaitru";
            this.optNgoaitru.Size = new System.Drawing.Size(97, 23);
            this.optNgoaitru.TabIndex = 9;
            this.optNgoaitru.TabStop = true;
            this.optNgoaitru.Text = "Ngoại trú?";
            // 
            // optNoitru
            // 
            this.optNoitru.Location = new System.Drawing.Point(113, 18);
            this.optNoitru.Name = "optNoitru";
            this.optNoitru.Size = new System.Drawing.Size(98, 23);
            this.optNoitru.TabIndex = 9;
            this.optNoitru.Text = "Nội trú?";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optChuyenmon);
            this.groupBox1.Controls.Add(this.optChucnang);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(392, 189);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 48);
            this.groupBox1.TabIndex = 81;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Phân loại";
            // 
            // optChuyenmon
            // 
            this.optChuyenmon.Checked = true;
            this.optChuyenmon.Location = new System.Drawing.Point(10, 18);
            this.optChuyenmon.Name = "optChuyenmon";
            this.optChuyenmon.Size = new System.Drawing.Size(97, 23);
            this.optChuyenmon.TabIndex = 12;
            this.optChuyenmon.TabStop = true;
            this.optChuyenmon.Text = "Chuyên môn";
            this.toolTip1.SetToolTip(this.optChuyenmon, "Chọn mục này nếu đó là các khoa phòng liên quan đến việc khám chữa bệnh trực tiếp" +
        " cho bệnh nhân");
            // 
            // optChucnang
            // 
            this.optChucnang.Location = new System.Drawing.Point(113, 18);
            this.optChucnang.Name = "optChucnang";
            this.optChucnang.Size = new System.Drawing.Size(98, 23);
            this.optChucnang.TabIndex = 12;
            this.optChucnang.Text = "Chức năng?";
            this.toolTip1.SetToolTip(this.optChucnang, "Chọn mục này nếu đó là các phòng chức năng. Ví dụ: Phòng tài chính, khoa Dược,..." +
        "");
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(3, 394);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(619, 25);
            this.lblMsg.TabIndex = 80;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDonvitinh
            // 
            this.txtDonvitinh._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtDonvitinh._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonvitinh.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtDonvitinh.AutoCompleteList")));
            this.txtDonvitinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDonvitinh.CaseSensitive = false;
            this.txtDonvitinh.CompareNoID = true;
            this.txtDonvitinh.DefaultCode = "-1";
            this.txtDonvitinh.DefaultID = "-1";
            this.txtDonvitinh.Drug_ID = null;
            this.txtDonvitinh.ExtraWidth = 0;
            this.txtDonvitinh.FillValueAfterSelect = false;
            this.txtDonvitinh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonvitinh.LOAI_DANHMUC = "DONVITINH";
            this.txtDonvitinh.Location = new System.Drawing.Point(158, 278);
            this.txtDonvitinh.MaxHeight = -1;
            this.txtDonvitinh.MinTypedCharacters = 2;
            this.txtDonvitinh.MyCode = "-1";
            this.txtDonvitinh.MyID = "-1";
            this.txtDonvitinh.Name = "txtDonvitinh";
            this.txtDonvitinh.RaiseEvent = false;
            this.txtDonvitinh.RaiseEventEnter = false;
            this.txtDonvitinh.RaiseEventEnterWhenEmpty = false;
            this.txtDonvitinh.SelectedIndex = -1;
            this.txtDonvitinh.Size = new System.Drawing.Size(452, 22);
            this.txtDonvitinh.splitChar = '@';
            this.txtDonvitinh.splitCharIDAndCode = '#';
            this.txtDonvitinh.TabIndex = 14;
            this.txtDonvitinh.TakeCode = false;
            this.txtDonvitinh.txtMyCode = null;
            this.txtDonvitinh.txtMyCode_Edit = null;
            this.txtDonvitinh.txtMyID = null;
            this.txtDonvitinh.txtMyID_Edit = null;
            this.txtDonvitinh.txtMyName = null;
            this.txtDonvitinh.txtMyName_Edit = null;
            this.txtDonvitinh.txtNext = null;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optKhoa);
            this.groupBox2.Controls.Add(this.optPhong);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(158, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 48);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kiểu khoa-phòng";
            // 
            // optKhoa
            // 
            this.optKhoa.Checked = true;
            this.optKhoa.Location = new System.Drawing.Point(10, 18);
            this.optKhoa.Name = "optKhoa";
            this.optKhoa.Size = new System.Drawing.Size(97, 23);
            this.optKhoa.TabIndex = 11;
            this.optKhoa.TabStop = true;
            this.optKhoa.Text = "Là Khoa?";
            // 
            // optPhong
            // 
            this.optPhong.Location = new System.Drawing.Point(113, 18);
            this.optPhong.Name = "optPhong";
            this.optPhong.Size = new System.Drawing.Size(98, 23);
            this.optPhong.TabIndex = 11;
            this.optPhong.Text = "Là phòng?";
            // 
            // txtMaphongXepStt
            // 
            this.txtMaphongXepStt.BackColor = System.Drawing.Color.White;
            this.txtMaphongXepStt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaphongXepStt.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaphongXepStt.Location = new System.Drawing.Point(158, 306);
            this.txtMaphongXepStt.MaxLength = 99999;
            this.txtMaphongXepStt.Name = "txtMaphongXepStt";
            this.txtMaphongXepStt.Size = new System.Drawing.Size(215, 22);
            this.txtMaphongXepStt.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 309);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 16);
            this.label9.TabIndex = 76;
            this.label9.Text = "Mã phòng xếp STT";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPhong_Thien
            // 
            this.txtPhong_Thien.BackColor = System.Drawing.Color.White;
            this.txtPhong_Thien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhong_Thien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhong_Thien.Location = new System.Drawing.Point(451, 4);
            this.txtPhong_Thien.MaxLength = 99999;
            this.txtPhong_Thien.Name = "txtPhong_Thien";
            this.txtPhong_Thien.Size = new System.Drawing.Size(10, 22);
            this.txtPhong_Thien.TabIndex = 2;
            this.txtPhong_Thien.Visible = false;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(303, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 16);
            this.label8.TabIndex = 74;
            this.label8.Text = "Nơi thực hiện";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 285);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 13);
            this.label7.TabIndex = 69;
            this.label7.Text = "&Đơn vị tính";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkKhoaCapCuu
            // 
            this.chkKhoaCapCuu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKhoaCapCuu.ForeColor = System.Drawing.Color.Red;
            this.chkKhoaCapCuu.Location = new System.Drawing.Point(484, 155);
            this.chkKhoaCapCuu.Name = "chkKhoaCapCuu";
            this.chkKhoaCapCuu.Size = new System.Drawing.Size(123, 23);
            this.chkKhoaCapCuu.TabIndex = 10;
            this.chkKhoaCapCuu.Text = "Khoa cấp cứu?";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(502, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 16);
            this.label6.TabIndex = 65;
            this.label6.Text = "&Tiền tạm ứng";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // chkParent
            // 
            this.chkParent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkParent.Location = new System.Drawing.Point(10, 89);
            this.chkParent.Name = "chkParent";
            this.chkParent.Size = new System.Drawing.Size(143, 23);
            this.chkParent.TabIndex = 4;
            this.chkParent.Text = "Khoa/phòng cấp trên:";
            this.chkParent.CheckedChanged += new System.EventHandler(this.chkParent_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(10, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 16);
            this.label5.TabIndex = 64;
            this.label5.Text = "Mã phòng";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDepartment_Code
            // 
            this.txtDepartment_Code.BackColor = System.Drawing.Color.White;
            this.txtDepartment_Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDepartment_Code.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDepartment_Code.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartment_Code.Location = new System.Drawing.Point(158, 30);
            this.txtDepartment_Code.Name = "txtDepartment_Code";
            this.txtDepartment_Code.Size = new System.Drawing.Size(129, 22);
            this.txtDepartment_Code.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(464, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 16);
            this.label4.TabIndex = 63;
            this.label4.Text = "Đơn giá";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 16);
            this.label3.TabIndex = 62;
            this.label3.Text = "Mô tả thêm";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDesc
            // 
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.Location = new System.Drawing.Point(158, 243);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(452, 29);
            this.txtDesc.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(10, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 16);
            this.label1.TabIndex = 61;
            this.label1.Text = "Nội trú/ngoại trú";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(158, 30);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(133, 22);
            this.txtID.TabIndex = 49;
            this.txtID.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(263, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 60;
            this.label2.Text = "Tự sinh";
            // 
            // txtintOrder
            // 
            this.txtintOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtintOrder.Location = new System.Drawing.Point(158, 118);
            this.txtintOrder.Name = "txtintOrder";
            this.txtintOrder.Size = new System.Drawing.Size(94, 22);
            this.txtintOrder.TabIndex = 8;
            this.txtintOrder.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cboParent_ID
            // 
            this.cboParent_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParent_ID.Enabled = false;
            this.cboParent_ID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboParent_ID.FormattingEnabled = true;
            this.cboParent_ID.Items.AddRange(new object[] {
            "01",
            "02",
            "03"});
            this.cboParent_ID.Location = new System.Drawing.Point(158, 87);
            this.cboParent_ID.Name = "cboParent_ID";
            this.cboParent_ID.Size = new System.Drawing.Size(449, 24);
            this.cboParent_ID.TabIndex = 5;
            this.cboParent_ID.SelectedIndexChanged += new System.EventHandler(this.cboParent_ID_SelectedIndexChanged);
            // 
            // lblintOrder
            // 
            this.lblintOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblintOrder.ForeColor = System.Drawing.Color.Red;
            this.lblintOrder.Location = new System.Drawing.Point(10, 125);
            this.lblintOrder.Name = "lblintOrder";
            this.lblintOrder.Size = new System.Drawing.Size(143, 16);
            this.lblintOrder.TabIndex = 54;
            this.lblintOrder.Text = "Số thứ tự";
            this.lblintOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDepartment_Name
            // 
            this.txtDepartment_Name.BackColor = System.Drawing.Color.White;
            this.txtDepartment_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDepartment_Name.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartment_Name.Location = new System.Drawing.Point(158, 59);
            this.txtDepartment_Name.MaxLength = 99999;
            this.txtDepartment_Name.Name = "txtDepartment_Name";
            this.txtDepartment_Name.Size = new System.Drawing.Size(449, 22);
            this.txtDepartment_Name.TabIndex = 1;
            // 
            // lblDepartment_Name
            // 
            this.lblDepartment_Name.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment_Name.ForeColor = System.Drawing.Color.Red;
            this.lblDepartment_Name.Location = new System.Drawing.Point(10, 63);
            this.lblDepartment_Name.Name = "lblDepartment_Name";
            this.lblDepartment_Name.Size = new System.Drawing.Size(143, 16);
            this.lblDepartment_Name.TabIndex = 48;
            this.lblDepartment_Name.Text = "Tên khoa, phòng";
            this.lblDepartment_Name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // frm_themmoi_phongban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 466);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.cmdThoat);
            this.Controls.Add(this.cmdGhi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_phongban";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THÔNG TIN KHOA(PHÒNG)";
            this.Load += new System.EventHandler(this.frm_themmoi_phongban_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtintOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdThoat;
        private System.Windows.Forms.Button cmdGhi;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label6;
        private Janus.Windows.EditControls.UICheckBox chkParent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDepartment_Code;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDesc;
       // private System.Windows.Forms.RadioButton radChucnang;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtintOrder;
        private System.Windows.Forms.ComboBox cboParent_ID;
        private System.Windows.Forms.Label lblintOrder;
        private System.Windows.Forms.TextBox txtDepartment_Name;
        private System.Windows.Forms.Label lblDepartment_Name;
        private Janus.Windows.EditControls.UICheckBox chkKhoaCapCuu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMaphongXepStt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPhong_Thien;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private Janus.Windows.EditControls.UIRadioButton optKhoa;
        private Janus.Windows.EditControls.UIRadioButton optPhong;
        private VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung txtDonvitinh;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.GroupBox groupBox3;
        private Janus.Windows.EditControls.UIRadioButton optNgoaitru;
        private Janus.Windows.EditControls.UIRadioButton optNoitru;
        private System.Windows.Forms.GroupBox groupBox1;
        private Janus.Windows.EditControls.UIRadioButton optChuyenmon;
        private Janus.Windows.EditControls.UIRadioButton optChucnang;
        private Janus.Windows.EditControls.UIRadioButton optKhac;
        private System.Windows.Forms.ToolTip toolTip1;
        private MaskedTextBox.MaskedTextBox txtTIEN_TAM_UNG;
        private MaskedTextBox.MaskedTextBox txtDeptFee;
    }
}