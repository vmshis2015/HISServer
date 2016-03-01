namespace VNS.HIS.UI.Forms.Dungchung
{
    partial class frmUpdateMaBenhAn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateMaBenhAn));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem9 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem10 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem11 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem12 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem13 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem14 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem15 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem16 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdThoat = new System.Windows.Forms.Button();
            this.txtmalankham = new System.Windows.Forms.TextBox();
            this.txtmabenhanmoi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboloaibenhan = new Janus.Windows.EditControls.UIComboBox();
            this.SuspendLayout();
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUpdate.Location = new System.Drawing.Point(37, 140);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(109, 39);
            this.cmdUpdate.TabIndex = 0;
            this.cmdUpdate.Text = "Chấp nhận";
            this.cmdUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdThoat
            // 
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.Location = new System.Drawing.Point(173, 140);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(111, 39);
            this.cmdThoat.TabIndex = 1;
            this.cmdThoat.Text = "Thoát";
            this.cmdThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdThoat.UseVisualStyleBackColor = true;
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // txtmalankham
            // 
            this.txtmalankham.Location = new System.Drawing.Point(112, 23);
            this.txtmalankham.Multiline = true;
            this.txtmalankham.Name = "txtmalankham";
            this.txtmalankham.ReadOnly = true;
            this.txtmalankham.Size = new System.Drawing.Size(172, 30);
            this.txtmalankham.TabIndex = 2;
            this.txtmalankham.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtmabenhanmoi
            // 
            this.txtmabenhanmoi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmabenhanmoi.Location = new System.Drawing.Point(112, 104);
            this.txtmabenhanmoi.Multiline = true;
            this.txtmabenhanmoi.Name = "txtmabenhanmoi";
            this.txtmabenhanmoi.Size = new System.Drawing.Size(172, 30);
            this.txtmabenhanmoi.TabIndex = 3;
            this.txtmabenhanmoi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã lần khám";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mã bệnh án mới";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Loại bệnh án";
            // 
            // cboloaibenhan
            // 
            uiComboBoxItem9.FormatStyle.Alpha = 0;
            uiComboBoxItem9.IsSeparator = false;
            uiComboBoxItem9.Text = "Chọn bệnh án";
            uiComboBoxItem9.Value = "-1";
            uiComboBoxItem10.FormatStyle.Alpha = 0;
            uiComboBoxItem10.IsSeparator = false;
            uiComboBoxItem10.Text = "Bệnh án Đái tháo đường";
            uiComboBoxItem10.Value = "DTD";
            uiComboBoxItem11.FormatStyle.Alpha = 0;
            uiComboBoxItem11.IsSeparator = false;
            uiComboBoxItem11.Text = "Bệnh án Tăng huyết áp";
            uiComboBoxItem11.Value = "THA";
            uiComboBoxItem12.FormatStyle.Alpha = 0;
            uiComboBoxItem12.IsSeparator = false;
            uiComboBoxItem12.Text = "Bệnh án Basedow";
            uiComboBoxItem12.Value = "BAS";
            uiComboBoxItem13.FormatStyle.Alpha = 0;
            uiComboBoxItem13.IsSeparator = false;
            uiComboBoxItem13.Text = "Bệnh án COP";
            uiComboBoxItem13.Value = "COP";
            uiComboBoxItem14.FormatStyle.Alpha = 0;
            uiComboBoxItem14.IsSeparator = false;
            uiComboBoxItem14.Text = "Bệnh án Viêm gan B";
            uiComboBoxItem14.Value = "VGB";
            uiComboBoxItem15.FormatStyle.Alpha = 0;
            uiComboBoxItem15.IsSeparator = false;
            uiComboBoxItem15.Text = "Bệnh án Tai mũi họng";
            uiComboBoxItem15.Value = "TMH";
            uiComboBoxItem16.FormatStyle.Alpha = 0;
            uiComboBoxItem16.IsSeparator = false;
            uiComboBoxItem16.Text = "Bệnh án Răng hàm mặt";
            uiComboBoxItem16.Value = "RHM";
            this.cboloaibenhan.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem9,
            uiComboBoxItem10,
            uiComboBoxItem11,
            uiComboBoxItem12,
            uiComboBoxItem13,
            uiComboBoxItem14,
            uiComboBoxItem15,
            uiComboBoxItem16});
            this.cboloaibenhan.Location = new System.Drawing.Point(112, 65);
            this.cboloaibenhan.Name = "cboloaibenhan";
            this.cboloaibenhan.Size = new System.Drawing.Size(172, 23);
            this.cboloaibenhan.TabIndex = 8;
            this.cboloaibenhan.Text = "Chọn bệnh án";
            // 
            // frmUpdateMaBenhAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 201);
            this.Controls.Add(this.cboloaibenhan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtmabenhanmoi);
            this.Controls.Add(this.txtmalankham);
            this.Controls.Add(this.cmdThoat);
            this.Controls.Add(this.cmdUpdate);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdateMaBenhAn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Mã Bệnh Án";
            this.Load += new System.EventHandler(this.frmUpdateMaBenhAn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdThoat;
        private System.Windows.Forms.TextBox txtmabenhanmoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtmalankham;
        private System.Windows.Forms.Label label3;
        private Janus.Windows.EditControls.UIComboBox cboloaibenhan;
    }
}