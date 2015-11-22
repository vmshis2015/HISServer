using VNS.HIS.UCs;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    partial class frm_ChuyenPhongkham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ChuyenPhongkham));
            this.cboChonphongkham = new Janus.Windows.EditControls.UIButton();
            this.cboChonkieukham = new Janus.Windows.EditControls.UIButton();
            this.label21 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtKieuKham = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.txtPhongkham = new VNS.HIS.UCs.AutoCompleteTextbox();
            this.cmdChuyen = new Janus.Windows.EditControls.UIButton();
            this.vbLine8 = new VNS.UCs.VBLine();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vbLine1 = new VNS.UCs.VBLine();
            this.cmdClose = new Janus.Windows.EditControls.UIButton();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhonghientai = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLydo = new VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung();
            this.radkhamchuyenkhoa = new System.Windows.Forms.RadioButton();
            this.radkhambenhpham = new System.Windows.Forms.RadioButton();
            this.cmdInPhieukhamchuyenkhoa = new Janus.Windows.EditControls.UIButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboChonphongkham
            // 
            this.cboChonphongkham.Font = new System.Drawing.Font("Arial", 9F);
            this.cboChonphongkham.Image = ((System.Drawing.Image)(resources.GetObject("cboChonphongkham.Image")));
            this.cboChonphongkham.Location = new System.Drawing.Point(510, 182);
            this.cboChonphongkham.Name = "cboChonphongkham";
            this.cboChonphongkham.Size = new System.Drawing.Size(29, 21);
            this.cboChonphongkham.TabIndex = 552;
            this.cboChonphongkham.TabStop = false;
            this.cboChonphongkham.Visible = false;
            // 
            // cboChonkieukham
            // 
            this.cboChonkieukham.Font = new System.Drawing.Font("Arial", 9F);
            this.cboChonkieukham.Image = ((System.Drawing.Image)(resources.GetObject("cboChonkieukham.Image")));
            this.cboChonkieukham.Location = new System.Drawing.Point(510, 135);
            this.cboChonkieukham.Name = "cboChonkieukham";
            this.cboChonkieukham.Size = new System.Drawing.Size(29, 21);
            this.cboChonkieukham.TabIndex = 551;
            this.cboChonkieukham.TabStop = false;
            this.cboChonkieukham.Visible = false;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(11, 182);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(101, 21);
            this.label21.TabIndex = 550;
            this.label21.Text = "Chuyển tới:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Navy;
            this.label27.Location = new System.Drawing.Point(19, 135);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(91, 21);
            this.label27.TabIndex = 549;
            this.label27.Text = "Dịch vụ khám:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKieuKham
            // 
            this.txtKieuKham._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtKieuKham._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKieuKham._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtKieuKham.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtKieuKham.AutoCompleteList")));
            this.txtKieuKham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKieuKham.CaseSensitive = false;
            this.txtKieuKham.CompareNoID = true;
            this.txtKieuKham.DefaultCode = "-1";
            this.txtKieuKham.DefaultID = "-1";
            this.txtKieuKham.Drug_ID = null;
            this.txtKieuKham.ExtraWidth = 0;
            this.txtKieuKham.FillValueAfterSelect = false;
            this.txtKieuKham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKieuKham.Location = new System.Drawing.Point(115, 135);
            this.txtKieuKham.MaxHeight = 289;
            this.txtKieuKham.MaxLength = 100;
            this.txtKieuKham.MinTypedCharacters = 2;
            this.txtKieuKham.MyCode = "-1";
            this.txtKieuKham.MyID = "-1";
            this.txtKieuKham.MyText = "";
            this.txtKieuKham.Name = "txtKieuKham";
            this.txtKieuKham.RaiseEvent = true;
            this.txtKieuKham.RaiseEventEnter = false;
            this.txtKieuKham.RaiseEventEnterWhenEmpty = true;
            this.txtKieuKham.SelectedIndex = -1;
            this.txtKieuKham.Size = new System.Drawing.Size(389, 21);
            this.txtKieuKham.splitChar = '@';
            this.txtKieuKham.splitCharIDAndCode = '#';
            this.txtKieuKham.TabIndex = 0;
            this.txtKieuKham.TabStop = false;
            this.txtKieuKham.TakeCode = false;
            this.txtKieuKham.txtMyCode = null;
            this.txtKieuKham.txtMyCode_Edit = null;
            this.txtKieuKham.txtMyID = null;
            this.txtKieuKham.txtMyID_Edit = null;
            this.txtKieuKham.txtMyName = null;
            this.txtKieuKham.txtMyName_Edit = null;
            this.txtKieuKham.txtNext = null;
            // 
            // txtPhongkham
            // 
            this.txtPhongkham._backcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtPhongkham._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhongkham._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhongkham.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtPhongkham.AutoCompleteList")));
            this.txtPhongkham.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhongkham.CaseSensitive = false;
            this.txtPhongkham.CompareNoID = true;
            this.txtPhongkham.DefaultCode = "-1";
            this.txtPhongkham.DefaultID = "-1";
            this.txtPhongkham.Drug_ID = null;
            this.txtPhongkham.ExtraWidth = 0;
            this.txtPhongkham.FillValueAfterSelect = false;
            this.txtPhongkham.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhongkham.Location = new System.Drawing.Point(115, 182);
            this.txtPhongkham.MaxHeight = 289;
            this.txtPhongkham.MaxLength = 100;
            this.txtPhongkham.MinTypedCharacters = 2;
            this.txtPhongkham.MyCode = "-1";
            this.txtPhongkham.MyID = "-1";
            this.txtPhongkham.MyText = "";
            this.txtPhongkham.Name = "txtPhongkham";
            this.txtPhongkham.RaiseEvent = true;
            this.txtPhongkham.RaiseEventEnter = false;
            this.txtPhongkham.RaiseEventEnterWhenEmpty = true;
            this.txtPhongkham.SelectedIndex = -1;
            this.txtPhongkham.Size = new System.Drawing.Size(389, 21);
            this.txtPhongkham.splitChar = '@';
            this.txtPhongkham.splitCharIDAndCode = '#';
            this.txtPhongkham.TabIndex = 1;
            this.txtPhongkham.TakeCode = false;
            this.txtPhongkham.txtMyCode = null;
            this.txtPhongkham.txtMyCode_Edit = null;
            this.txtPhongkham.txtMyID = null;
            this.txtPhongkham.txtMyID_Edit = null;
            this.txtPhongkham.txtMyName = null;
            this.txtPhongkham.txtMyName_Edit = null;
            this.txtPhongkham.txtNext = null;
            // 
            // cmdChuyen
            // 
            this.cmdChuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChuyen.Image = ((System.Drawing.Image)(resources.GetObject("cmdChuyen.Image")));
            this.cmdChuyen.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdChuyen.Location = new System.Drawing.Point(240, 320);
            this.cmdChuyen.Name = "cmdChuyen";
            this.cmdChuyen.Size = new System.Drawing.Size(141, 35);
            this.cmdChuyen.TabIndex = 2;
            this.cmdChuyen.Text = "Chuyển (Ctrl+S)";
            // 
            // vbLine8
            // 
            this.vbLine8._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine8.BackColor = System.Drawing.Color.Transparent;
            this.vbLine8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine8.FontText = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine8.Location = new System.Drawing.Point(0, 98);
            this.vbLine8.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine8.Name = "vbLine8";
            this.vbLine8.Size = new System.Drawing.Size(571, 22);
            this.vbLine8.TabIndex = 545;
            this.vbLine8.TabStop = false;
            this.vbLine8.YourText = "Chọn phòng khám cần chuyển";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 91);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(140, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(432, 51);
            this.label2.TabIndex = 542;
            this.label2.Text = "Chỉ được chọn các phòng khám hoặc kiểu khám có giá tương đương....Nếu muốn chuyển" +
    " dịch vụ khám khác, cần hướng dẫn bệnh nhân quay lại quầy tiếp đón để thực hiện." +
    "";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(91, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 21);
            this.label1.TabIndex = 541;
            this.label1.Text = "Chuyển phòng khám";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(59, 56);
            this.panel2.TabIndex = 0;
            // 
            // vbLine1
            // 
            this.vbLine1._FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vbLine1.BackColor = System.Drawing.Color.Transparent;
            this.vbLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.FontText = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.vbLine1.Location = new System.Drawing.Point(0, 291);
            this.vbLine1.Margin = new System.Windows.Forms.Padding(4);
            this.vbLine1.Name = "vbLine1";
            this.vbLine1.Size = new System.Drawing.Size(571, 22);
            this.vbLine1.TabIndex = 554;
            this.vbLine1.TabStop = false;
            this.vbLine1.YourText = "Chọn hành động";
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdClose.Location = new System.Drawing.Point(402, 320);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(141, 35);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Thoát (Esc)";
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(115, 265);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(389, 37);
            this.lblMsg.TabIndex = 555;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(11, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 21);
            this.label3.TabIndex = 557;
            this.label3.Text = "Lý do chuyển:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPhonghientai
            // 
            this.txtPhonghientai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhonghientai.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhonghientai.Location = new System.Drawing.Point(115, 159);
            this.txtPhonghientai.MaxLength = 255;
            this.txtPhonghientai.Name = "txtPhonghientai";
            this.txtPhonghientai.Size = new System.Drawing.Size(389, 21);
            this.txtPhonghientai.TabIndex = 558;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(12, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 21);
            this.label4.TabIndex = 559;
            this.label4.Text = "Phòng hiện tại:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLydo
            // 
            this.txtLydo._backcolor = System.Drawing.Color.WhiteSmoke;
            this.txtLydo._Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLydo.AutoCompleteList = ((System.Collections.Generic.List<string>)(resources.GetObject("txtLydo.AutoCompleteList")));
            this.txtLydo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLydo.CaseSensitive = false;
            this.txtLydo.CompareNoID = true;
            this.txtLydo.DefaultCode = "-1";
            this.txtLydo.DefaultID = "-1";
            this.txtLydo.Drug_ID = null;
            this.txtLydo.ExtraWidth = 0;
            this.txtLydo.FillValueAfterSelect = false;
            this.txtLydo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLydo.LOAI_DANHMUC = "LYDOCHUYENPHONGKHAM";
            this.txtLydo.Location = new System.Drawing.Point(115, 208);
            this.txtLydo.MaxHeight = 150;
            this.txtLydo.MinTypedCharacters = 2;
            this.txtLydo.MyCode = "-1";
            this.txtLydo.MyID = "-1";
            this.txtLydo.Name = "txtLydo";
            this.txtLydo.RaiseEvent = false;
            this.txtLydo.RaiseEventEnter = false;
            this.txtLydo.RaiseEventEnterWhenEmpty = false;
            this.txtLydo.SelectedIndex = -1;
            this.txtLydo.Size = new System.Drawing.Size(389, 21);
            this.txtLydo.splitChar = '@';
            this.txtLydo.splitCharIDAndCode = '#';
            this.txtLydo.TabIndex = 2;
            this.txtLydo.TakeCode = false;
            this.txtLydo.txtMyCode = null;
            this.txtLydo.txtMyCode_Edit = null;
            this.txtLydo.txtMyID = null;
            this.txtLydo.txtMyID_Edit = null;
            this.txtLydo.txtMyName = null;
            this.txtLydo.txtMyName_Edit = null;
            this.txtLydo.txtNext = null;
            // 
            // radkhamchuyenkhoa
            // 
            this.radkhamchuyenkhoa.AutoSize = true;
            this.radkhamchuyenkhoa.Checked = true;
            this.radkhamchuyenkhoa.Location = new System.Drawing.Point(144, 245);
            this.radkhamchuyenkhoa.Name = "radkhamchuyenkhoa";
            this.radkhamchuyenkhoa.Size = new System.Drawing.Size(155, 17);
            this.radkhamchuyenkhoa.TabIndex = 560;
            this.radkhamchuyenkhoa.TabStop = true;
            this.radkhamchuyenkhoa.Text = "Chuyển khám chuyên khoa";
            this.radkhamchuyenkhoa.UseVisualStyleBackColor = true;
            // 
            // radkhambenhpham
            // 
            this.radkhambenhpham.AutoSize = true;
            this.radkhambenhpham.Location = new System.Drawing.Point(303, 245);
            this.radkhambenhpham.Name = "radkhambenhpham";
            this.radkhambenhpham.Size = new System.Drawing.Size(171, 17);
            this.radkhambenhpham.TabIndex = 561;
            this.radkhambenhpham.Text = "Chuyển xét nghiệm bệnh phẩm";
            this.radkhambenhpham.UseVisualStyleBackColor = true;
            // 
            // cmdInPhieukhamchuyenkhoa
            // 
            this.cmdInPhieukhamchuyenkhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInPhieukhamchuyenkhoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdInPhieukhamchuyenkhoa.Image")));
            this.cmdInPhieukhamchuyenkhoa.ImageSize = new System.Drawing.Size(24, 24);
            this.cmdInPhieukhamchuyenkhoa.Location = new System.Drawing.Point(78, 320);
            this.cmdInPhieukhamchuyenkhoa.Name = "cmdInPhieukhamchuyenkhoa";
            this.cmdInPhieukhamchuyenkhoa.Size = new System.Drawing.Size(141, 35);
            this.cmdInPhieukhamchuyenkhoa.TabIndex = 562;
            this.cmdInPhieukhamchuyenkhoa.Text = "In phiếu chuyên khoa (F4)";
            this.cmdInPhieukhamchuyenkhoa.Click += new System.EventHandler(this.cmdInPhieukhamchuyenkhoa_Click);
            // 
            // frm_ChuyenPhongkham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.cmdInPhieukhamchuyenkhoa);
            this.Controls.Add(this.radkhambenhpham);
            this.Controls.Add(this.radkhamchuyenkhoa);
            this.Controls.Add(this.txtLydo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPhonghientai);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.vbLine1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cboChonphongkham);
            this.Controls.Add(this.cboChonkieukham);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.txtKieuKham);
            this.Controls.Add(this.txtPhongkham);
            this.Controls.Add(this.cmdChuyen);
            this.Controls.Add(this.vbLine8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frm_ChuyenPhongkham";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyển phòng khám";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Janus.Windows.EditControls.UIButton cboChonphongkham;
        private Janus.Windows.EditControls.UIButton cboChonkieukham;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label27;
        private Janus.Windows.EditControls.UIButton cmdChuyen;
        private VNS.UCs.VBLine vbLine8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private VNS.UCs.VBLine vbLine1;
        private Janus.Windows.EditControls.UIButton cmdClose;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label3;
        public VNS.HIS.UCs.AutoCompleteTextbox txtKieuKham;
        public VNS.HIS.UCs.AutoCompleteTextbox txtPhongkham;
        public System.Windows.Forms.TextBox txtPhonghientai;
        private System.Windows.Forms.Label label4;
        private AutoCompleteTextbox_Danhmucchung txtLydo;
        private System.Windows.Forms.RadioButton radkhamchuyenkhoa;
        private System.Windows.Forms.RadioButton radkhambenhpham;
        private Janus.Windows.EditControls.UIButton cmdInPhieukhamchuyenkhoa;
    }
}