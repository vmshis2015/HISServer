namespace VNS.HIS.UI.DANHMUC
{
    partial class frm_themmoi_noiKCBBD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_themmoi_noiKCBBD));
            this.grpControl = new Janus.Windows.EditControls.UIGroupBox();
            this.cmdClear = new System.Windows.Forms.LinkLabel();
            this.txtSurveyCode = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIntOrder = new Janus.Windows.GridEX.EditControls.IntegerUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtsDesc = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSurveys = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDieaseName = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClinicCode = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtClinic_ID = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdSave = new Janus.Windows.EditControls.UIButton();
            this.cmdExit = new Janus.Windows.EditControls.UIButton();
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.cmdClear);
            this.grpControl.Controls.Add(this.txtSurveyCode);
            this.grpControl.Controls.Add(this.label5);
            this.grpControl.Controls.Add(this.txtIntOrder);
            this.grpControl.Controls.Add(this.label4);
            this.grpControl.Controls.Add(this.txtsDesc);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.cboSurveys);
            this.grpControl.Controls.Add(this.label2);
            this.grpControl.Controls.Add(this.txtDieaseName);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Controls.Add(this.txtClinicCode);
            this.grpControl.Controls.Add(this.txtClinic_ID);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControl.Image = ((System.Drawing.Image)(resources.GetObject("grpControl.Image")));
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(473, 217);
            this.grpControl.TabIndex = 0;
            this.grpControl.Text = "&Thông tin nơi khám chữa bệnh ban đầu";
            // 
            // cmdClear
            // 
            this.cmdClear.AutoSize = true;
            this.cmdClear.Location = new System.Drawing.Point(351, 199);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(85, 15);
            this.cmdClear.TabIndex = 23;
            this.cmdClear.TabStop = true;
            this.cmdClear.Text = "&Thêm mới(F5)";
            this.cmdClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdClear_LinkClicked);
            // 
            // txtSurveyCode
            // 
            this.txtSurveyCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSurveyCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtSurveyCode.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.txtSurveyCode.Location = new System.Drawing.Point(109, 85);
            this.txtSurveyCode.Name = "txtSurveyCode";
            this.txtSurveyCode.Size = new System.Drawing.Size(73, 21);
            this.txtSurveyCode.TabIndex = 2;
            this.txtSurveyCode.TextChanged += new System.EventHandler(this.txtSurveyCode_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 22;
            this.label5.Text = "&Thứ tự";
            // 
            // txtIntOrder
            // 
            this.txtIntOrder.Location = new System.Drawing.Point(109, 115);
            this.txtIntOrder.Maximum = 100000;
            this.txtIntOrder.Name = "txtIntOrder";
            this.txtIntOrder.Size = new System.Drawing.Size(104, 21);
            this.txtIntOrder.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "&Mô tả";
            // 
            // txtsDesc
            // 
            this.txtsDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtsDesc.Location = new System.Drawing.Point(109, 144);
            this.txtsDesc.Multiline = true;
            this.txtsDesc.Name = "txtsDesc";
            this.txtsDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtsDesc.Size = new System.Drawing.Size(327, 48);
            this.txtsDesc.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "&Thành phố";
            // 
            // cboSurveys
            // 
            this.cboSurveys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSurveys.FormattingEnabled = true;
            this.cboSurveys.Location = new System.Drawing.Point(188, 85);
            this.cboSurveys.Name = "cboSurveys";
            this.cboSurveys.Size = new System.Drawing.Size(248, 23);
            this.cboSurveys.TabIndex = 3;
            this.cboSurveys.SelectedIndexChanged += new System.EventHandler(this.cboSurveys_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "&Tên KCBBĐ";
            // 
            // txtDieaseName
            // 
            this.txtDieaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDieaseName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtDieaseName.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.txtDieaseName.Location = new System.Drawing.Point(109, 56);
            this.txtDieaseName.Name = "txtDieaseName";
            this.txtDieaseName.Size = new System.Drawing.Size(327, 21);
            this.txtDieaseName.TabIndex = 1;
            this.txtDieaseName.TextChanged += new System.EventHandler(this.txtDieaseName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "&Mã KCBBĐ";
            // 
            // txtClinicCode
            // 
            this.txtClinicCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClinicCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtClinicCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClinicCode.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight;
            this.txtClinicCode.Location = new System.Drawing.Point(109, 30);
            this.txtClinicCode.MaxLength = 10;
            this.txtClinicCode.Name = "txtClinicCode";
            this.txtClinicCode.Size = new System.Drawing.Size(217, 21);
            this.txtClinicCode.TabIndex = 0;
            this.txtClinicCode.TextChanged += new System.EventHandler(this.txtDieaseCode_TextChanged);
            // 
            // txtClinic_ID
            // 
            this.txtClinic_ID.Location = new System.Drawing.Point(109, 30);
            this.txtClinic_ID.Name = "txtClinic_ID";
            this.txtClinic_ID.Size = new System.Drawing.Size(29, 21);
            this.txtClinic_ID.TabIndex = 0;
            this.txtClinic_ID.TabStop = false;
            this.txtClinic_ID.Visible = false;
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.Location = new System.Drawing.Point(109, 232);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(137, 32);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Text = "&Lưu thông tin ";
            this.cmdSave.ToolTipText = "Lưu lại thông tin ";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(252, 232);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(108, 32);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.ToolTipText = "Thoát Form hiện tại";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frm_themmoi_noiKCBBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 272);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.grpControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_themmoi_noiKCBBD";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin nơi khám chữa bệnh ban đầu";
            this.Load += new System.EventHandler(this.frm_themmoi_noiKCBBD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal Janus.Windows.GridEX.EditControls.EditBox txtClinic_ID;
        private System.Windows.Forms.Label label4;
        private Janus.Windows.GridEX.EditControls.EditBox txtsDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSurveys;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.GridEX.EditControls.EditBox txtDieaseName;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.GridEX.EditControls.EditBox txtClinicCode;
        private Janus.Windows.GridEX.EditControls.IntegerUpDown txtIntOrder;
        private Janus.Windows.EditControls.UIButton cmdSave;
        private Janus.Windows.EditControls.UIButton cmdExit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Janus.Windows.GridEX.EditControls.EditBox txtSurveyCode;
        private Janus.Windows.EditControls.UIGroupBox grpControl;
        private System.Windows.Forms.LinkLabel cmdClear;
    }
}