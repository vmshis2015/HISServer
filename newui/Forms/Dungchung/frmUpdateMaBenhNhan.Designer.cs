namespace VNS.HIS.UI.Forms.Dungchung
{
    partial class frmUpdateMaBenhNhan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateMaBenhNhan));
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdThoat = new System.Windows.Forms.Button();
            this.txtmabenhnhancu = new System.Windows.Forms.TextBox();
            this.txtmabenhnhanmoi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUpdate.Location = new System.Drawing.Point(35, 142);
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
            this.cmdThoat.Location = new System.Drawing.Point(176, 142);
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(111, 39);
            this.cmdThoat.TabIndex = 1;
            this.cmdThoat.Text = "Thoát";
            this.cmdThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdThoat.UseVisualStyleBackColor = true;
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // txtmabenhnhancu
            // 
            this.txtmabenhnhancu.Location = new System.Drawing.Point(130, 23);
            this.txtmabenhnhancu.Multiline = true;
            this.txtmabenhnhancu.Name = "txtmabenhnhancu";
            this.txtmabenhnhancu.ReadOnly = true;
            this.txtmabenhnhancu.Size = new System.Drawing.Size(164, 30);
            this.txtmabenhnhancu.TabIndex = 2;
            this.txtmabenhnhancu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtmabenhnhanmoi
            // 
            this.txtmabenhnhanmoi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmabenhnhanmoi.Location = new System.Drawing.Point(130, 59);
            this.txtmabenhnhanmoi.Multiline = true;
            this.txtmabenhnhanmoi.Name = "txtmabenhnhanmoi";
            this.txtmabenhnhanmoi.Size = new System.Drawing.Size(164, 30);
            this.txtmabenhnhanmoi.TabIndex = 3;
            this.txtmabenhnhanmoi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtmabenhnhanmoi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtmalankhammoi_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã bệnh nhân cũ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mã bệnh nhân mới";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(314, 38);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mã bệnh nhân mới";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmUpdateMaBenhNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 193);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtmabenhnhanmoi);
            this.Controls.Add(this.txtmabenhnhancu);
            this.Controls.Add(this.cmdThoat);
            this.Controls.Add(this.cmdUpdate);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdateMaBenhNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Mã bệnh nhân";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdThoat;
        private System.Windows.Forms.TextBox txtmabenhnhanmoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtmabenhnhancu;
        private System.Windows.Forms.Label label3;
    }
}