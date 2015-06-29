namespace VNS.UCs
{
    partial class ucDynamicParam
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtValue = new RicherTextBox.RicherTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(257, 340);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Đối tượng:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtValue);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 340);
            this.panel1.TabIndex = 4;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Trợ giúp";
            // 
            // txtValue
            // 
            this.txtValue.AlignCenterVisible = true;
            this.txtValue.AlignLeftVisible = true;
            this.txtValue.AlignRightVisible = true;
            this.txtValue.AutoScroll = true;
            this.txtValue.BoldVisible = true;
            this.txtValue.BulletsVisible = false;
            this.txtValue.ChooseFontVisible = true;
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValue.FindReplaceVisible = true;
            this.txtValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.FontColorVisible = true;
            this.txtValue.FontFamilyVisible = true;
            this.txtValue.FontSizeVisible = true;
            this.txtValue.GroupAlignmentVisible = true;
            this.txtValue.GroupBoldUnderlineItalicVisible = true;
            this.txtValue.GroupFontColorVisible = true;
            this.txtValue.GroupFontNameAndSizeVisible = true;
            this.txtValue.GroupIndentationAndBulletsVisible = false;
            this.txtValue.GroupInsertVisible = false;
            this.txtValue.GroupSaveAndLoadVisible = true;
            this.txtValue.GroupZoomVisible = false;
            this.txtValue.INDENT = 10;
            this.txtValue.IndentVisible = true;
            this.txtValue.InsertPictureVisible = false;
            this.txtValue.ItalicVisible = true;
            this.txtValue.LoadVisible = true;
            this.txtValue.Location = new System.Drawing.Point(257, 0);
            this.txtValue.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtValue.Name = "txtValue";
            this.txtValue.OutdentVisible = false;
            this.txtValue.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset204 Microsoft" +
    " Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs18\\par\r\n}\r\n";
            this.txtValue.SaveVisible = true;
            this.txtValue.SeparatorAlignVisible = true;
            this.txtValue.SeparatorBoldUnderlineItalicVisible = true;
            this.txtValue.SeparatorFontColorVisible = true;
            this.txtValue.SeparatorFontVisible = true;
            this.txtValue.SeparatorIndentAndBulletsVisible = false;
            this.txtValue.SeparatorInsertVisible = false;
            this.txtValue.SeparatorSaveLoadVisible = true;
            this.txtValue.Size = new System.Drawing.Size(526, 340);
            this.txtValue.TabIndex = 0;
            this.txtValue.ToolStripVisible = true;
            this.txtValue.UnderlineVisible = true;
            this.txtValue.WordWrapVisible = true;
            this.txtValue.ZoomFactorTextVisible = false;
            this.txtValue.ZoomInVisible = false;
            this.txtValue.ZoomOutVisible = false;
            // 
            // ucDynamicParam
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Name = "ucDynamicParam";
            this.Size = new System.Drawing.Size(783, 340);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Label lblName;
        public RicherTextBox.RicherTextBox txtValue;

    }
}
