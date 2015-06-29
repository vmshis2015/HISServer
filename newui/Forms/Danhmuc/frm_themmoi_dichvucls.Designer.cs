namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_themmoi_dichvucls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_dichvucls));
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.nmrDongia = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.chkTrangthai = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPhongthuchien = new Janus.Windows.EditControls.UIComboBox();
            this.txtchidan = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDepartment = new Janus.Windows.EditControls.UIComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbonhombaocao = new System.Windows.Forms.ComboBox();
            this.cboNhomin = new System.Windows.Forms.ComboBox();
            this.chkHaveDetail = new System.Windows.Forms.CheckBox();
            this.chkHighTech = new System.Windows.Forms.CheckBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtServiceCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cboServiceType = new System.Windows.Forms.ComboBox();
            this.txtServiceOrder = new System.Windows.Forms.NumericUpDown();
            this.lblIntOrder = new System.Windows.Forms.Label();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.lblServiceType = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.cmdThoat = new System.Windows.Forms.Button();
            this.cmdGhi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDongia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServiceOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.lblMsg);
            this.grpControl.Controls.Add(this.nmrDongia);
            this.grpControl.Controls.Add(this.label5);
            this.grpControl.Controls.Add(this.chkTrangthai);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.cboPhongthuchien);
            this.grpControl.Controls.Add(this.txtchidan);
            this.grpControl.Controls.Add(this.label7);
            this.grpControl.Controls.Add(this.label6);
            this.grpControl.Controls.Add(this.label4);
            this.grpControl.Controls.Add(this.cboDepartment);
            this.grpControl.Controls.Add(this.label2);
            this.grpControl.Controls.Add(this.cbonhombaocao);
            this.grpControl.Controls.Add(this.cboNhomin);
            this.grpControl.Controls.Add(this.chkHaveDetail);
            this.grpControl.Controls.Add(this.chkHighTech);
            this.grpControl.Controls.Add(this.txtDesc);
            this.grpControl.Controls.Add(this.txtServiceCode);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Controls.Add(this.lblDescription);
            this.grpControl.Controls.Add(this.cboServiceType);
            this.grpControl.Controls.Add(this.txtServiceOrder);
            this.grpControl.Controls.Add(this.lblIntOrder);
            this.grpControl.Controls.Add(this.lblServiceName);
            this.grpControl.Controls.Add(this.lblServiceType);
            this.grpControl.Controls.Add(this.txtServiceName);
            this.grpControl.Controls.Add(this.txtID);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Image = ((System.Drawing.Image)(resources.GetObject("grpControl.Image")));
            this.grpControl.ImageSize = new System.Drawing.Size(24, 24);
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(590, 392);
            this.grpControl.TabIndex = 0;
            this.grpControl.Text = "&Thông tin dịch vụ";
            // 
            // lblMsg
            // 
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(3, 366);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(584, 23);
            this.lblMsg.TabIndex = 110;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmrDongia
            // 
            this.nmrDongia.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmrDongia.Location = new System.Drawing.Point(377, 108);
            this.nmrDongia.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nmrDongia.Name = "nmrDongia";
            this.nmrDongia.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nmrDongia.Size = new System.Drawing.Size(182, 22);
            this.nmrDongia.TabIndex = 4;
            this.nmrDongia.ThousandsSeparator = true;
            this.nmrDongia.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(236, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 23);
            this.label5.TabIndex = 109;
            this.label5.Text = "Đơn giá:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTrangthai
            // 
            this.chkTrangthai.AutoSize = true;
            this.chkTrangthai.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTrangthai.Location = new System.Drawing.Point(132, 349);
            this.chkTrangthai.Name = "chkTrangthai";
            this.chkTrangthai.Size = new System.Drawing.Size(91, 20);
            this.chkTrangthai.TabIndex = 11;
            this.chkTrangthai.TabStop = false;
            this.chkTrangthai.Text = "Trạng thái?";
            this.chkTrangthai.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 23);
            this.label3.TabIndex = 106;
            this.label3.Text = "Phòng thực hiện";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPhongthuchien
            // 
            this.cboPhongthuchien.BorderStyle = Janus.Windows.UI.BorderStyle.Flat;
            this.cboPhongthuchien.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboPhongthuchien.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPhongthuchien.Location = new System.Drawing.Point(136, 221);
            this.cboPhongthuchien.Name = "cboPhongthuchien";
            this.cboPhongthuchien.Size = new System.Drawing.Size(418, 22);
            this.cboPhongthuchien.TabIndex = 8;
            this.cboPhongthuchien.Text = "Khoa thực hiện";
            // 
            // txtchidan
            // 
            this.txtchidan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtchidan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchidan.Location = new System.Drawing.Point(136, 257);
            this.txtchidan.Multiline = true;
            this.txtchidan.Name = "txtchidan";
            this.txtchidan.Size = new System.Drawing.Size(418, 34);
            this.txtchidan.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(71, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 23);
            this.label7.TabIndex = 103;
            this.label7.Text = "Chỉ dẫn:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(2, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 23);
            this.label6.TabIndex = 102;
            this.label6.Text = "Nhóm in";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 23);
            this.label4.TabIndex = 100;
            this.label4.Text = "Khoa thực hiện";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDepartment
            // 
            this.cboDepartment.BorderStyle = Janus.Windows.UI.BorderStyle.Flat;
            this.cboDepartment.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            this.cboDepartment.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.Location = new System.Drawing.Point(137, 193);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(418, 22);
            this.cboDepartment.TabIndex = 7;
            this.cboDepartment.Text = "Khoa thực hiện";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(2, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 23);
            this.label2.TabIndex = 98;
            this.label2.Text = "Nhóm báo cáo";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbonhombaocao
            // 
            this.cbonhombaocao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbonhombaocao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbonhombaocao.FormattingEnabled = true;
            this.cbonhombaocao.Location = new System.Drawing.Point(136, 164);
            this.cbonhombaocao.Name = "cbonhombaocao";
            this.cbonhombaocao.Size = new System.Drawing.Size(418, 24);
            this.cbonhombaocao.TabIndex = 6;
            // 
            // cboNhomin
            // 
            this.cboNhomin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhomin.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNhomin.FormattingEnabled = true;
            this.cboNhomin.Items.AddRange(new object[] {
            "Phiếu xét nghiệm",
            "Phiếu X Quang",
            "Phiếu siêu âm",
            "Phiếu điện tim",
            "Phiếu nội soi",
            "Phiếu điện não đồ"});
            this.cboNhomin.Location = new System.Drawing.Point(136, 135);
            this.cboNhomin.Name = "cboNhomin";
            this.cboNhomin.Size = new System.Drawing.Size(418, 24);
            this.cboNhomin.TabIndex = 5;
            // 
            // chkHaveDetail
            // 
            this.chkHaveDetail.AutoSize = true;
            this.chkHaveDetail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHaveDetail.Location = new System.Drawing.Point(244, 348);
            this.chkHaveDetail.Name = "chkHaveDetail";
            this.chkHaveDetail.Size = new System.Drawing.Size(121, 20);
            this.chkHaveDetail.TabIndex = 12;
            this.chkHaveDetail.TabStop = false;
            this.chkHaveDetail.Text = "Hiển thị chi tiết?";
            this.chkHaveDetail.UseVisualStyleBackColor = true;
            // 
            // chkHighTech
            // 
            this.chkHighTech.AutoSize = true;
            this.chkHighTech.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHighTech.ForeColor = System.Drawing.Color.Navy;
            this.chkHighTech.Location = new System.Drawing.Point(371, 349);
            this.chkHighTech.Name = "chkHighTech";
            this.chkHighTech.Size = new System.Drawing.Size(168, 20);
            this.chkHighTech.TabIndex = 13;
            this.chkHighTech.TabStop = false;
            this.chkHighTech.Text = "Là dịch vụ kỹ thuật cao?";
            this.chkHighTech.UseVisualStyleBackColor = true;
            // 
            // txtDesc
            // 
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.Location = new System.Drawing.Point(136, 301);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(418, 34);
            this.txtDesc.TabIndex = 10;
            // 
            // txtServiceCode
            // 
            this.txtServiceCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceCode.Location = new System.Drawing.Point(136, 27);
            this.txtServiceCode.Name = "txtServiceCode";
            this.txtServiceCode.Size = new System.Drawing.Size(137, 22);
            this.txtServiceCode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 23);
            this.label1.TabIndex = 93;
            this.label1.Text = "Mã dịch vụ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(2, 300);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(125, 23);
            this.lblDescription.TabIndex = 91;
            this.lblDescription.Text = "Ghi chú";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboServiceType
            // 
            this.cboServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServiceType.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboServiceType.FormattingEnabled = true;
            this.cboServiceType.Location = new System.Drawing.Point(137, 78);
            this.cboServiceType.Name = "cboServiceType";
            this.cboServiceType.Size = new System.Drawing.Size(422, 24);
            this.cboServiceType.TabIndex = 2;
            // 
            // txtServiceOrder
            // 
            this.txtServiceOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceOrder.Location = new System.Drawing.Point(136, 107);
            this.txtServiceOrder.Name = "txtServiceOrder";
            this.txtServiceOrder.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtServiceOrder.Size = new System.Drawing.Size(94, 22);
            this.txtServiceOrder.TabIndex = 3;
            this.txtServiceOrder.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // lblIntOrder
            // 
            this.lblIntOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntOrder.Location = new System.Drawing.Point(6, 110);
            this.lblIntOrder.Name = "lblIntOrder";
            this.lblIntOrder.Size = new System.Drawing.Size(125, 23);
            this.lblIntOrder.TabIndex = 89;
            this.lblIntOrder.Text = "STT hiển thị:";
            this.lblIntOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblServiceName
            // 
            this.lblServiceName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceName.ForeColor = System.Drawing.Color.Red;
            this.lblServiceName.Location = new System.Drawing.Point(6, 54);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(125, 23);
            this.lblServiceName.TabIndex = 87;
            this.lblServiceName.Text = "Tên Dịch Vụ";
            this.lblServiceName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblServiceType
            // 
            this.lblServiceType.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceType.ForeColor = System.Drawing.Color.Red;
            this.lblServiceType.Location = new System.Drawing.Point(6, 82);
            this.lblServiceType.Name = "lblServiceType";
            this.lblServiceType.Size = new System.Drawing.Size(125, 23);
            this.lblServiceType.TabIndex = 88;
            this.lblServiceType.Text = "Loại Dịch Vụ";
            this.lblServiceType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServiceName
            // 
            this.txtServiceName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceName.Location = new System.Drawing.Point(137, 52);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(422, 22);
            this.txtServiceName.TabIndex = 1;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(137, 27);
            this.txtID.MaxLength = 1;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(79, 22);
            this.txtID.TabIndex = 94;
            this.txtID.TabStop = false;
            this.txtID.Visible = false;
            // 
            // cmdThoat
            // 
            this.cmdThoat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdThoat.Location = new System.Drawing.Point(303, 399);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(120, 35);
            this.cmdThoat.TabIndex = 15;
            this.cmdThoat.Text = "Thoát(Esc)";
            this.cmdThoat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdThoat.UseVisualStyleBackColor = true;
            // 
            // cmdGhi
            // 
            this.cmdGhi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGhi.Image = ((System.Drawing.Image)(resources.GetObject("cmdGhi.Image")));
            this.cmdGhi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdGhi.Location = new System.Drawing.Point(165, 399);
            this.cmdGhi.Name = "cmdGhi";
            this.cmdGhi.Size = new System.Drawing.Size(117, 35);
            this.cmdGhi.TabIndex = 14;
            this.cmdGhi.Text = "Lưu(Ctrl+S)";
            this.cmdGhi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdGhi.UseVisualStyleBackColor = true;
            this.cmdGhi.Click += new System.EventHandler(this.cmdGhi_Click_1);
            // 
            // frm_themmoi_dichvucls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 442);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.cmdThoat);
            this.Controls.Add(this.cmdGhi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_dichvucls";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THÔNG TIN DỊCH VỤ";
            this.Load += new System.EventHandler(this.frm_themmoi_dichvucls_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_themmoi_dichvucls_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frm_themmoi_dichvucls_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDongia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServiceOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.ComboBox cboNhomin;
        private System.Windows.Forms.CheckBox chkHaveDetail;
        private System.Windows.Forms.CheckBox chkHighTech;
        internal System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtServiceCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ComboBox cboServiceType;
        private System.Windows.Forms.NumericUpDown txtServiceOrder;
        private System.Windows.Forms.Label lblIntOrder;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.Label lblServiceType;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Button cmdThoat;
        private System.Windows.Forms.Button cmdGhi;
        private System.Windows.Forms.ComboBox cbonhombaocao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.EditControls.UIComboBox cboDepartment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtchidan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox cboPhongthuchien;
        private System.Windows.Forms.CheckBox chkTrangthai;
        private System.Windows.Forms.NumericUpDown nmrDongia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMsg;
    }
}