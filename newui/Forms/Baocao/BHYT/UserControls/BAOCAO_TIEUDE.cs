using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;

namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls
{
    public partial class BAOCAO_TIEUDE : UserControl
    {
        public BAOCAO_TIEUDE()
        {
            InitializeComponent();
            txtTieuDe.LostFocus += new EventHandler(txtTieuDe_LostFocus);
            txtTieuDe.GotFocus += new EventHandler(txtTieuDe_GotFocus);
            txtTieuDe.KeyDown += new KeyEventHandler(txtTieuDe_KeyDown);
            cmdSave.Visible = globalVariables.gv_bQuyenSuaTieudeBaocao;
            cmdSave.Click += new EventHandler(cmdSave_Click);
        }
        
        public string Phimtat
        {
            get { return lblPhimtat.Text; }
            set { lblPhimtat.Text = value; }
        }
        public bool showHelp
        {
            get { return lblPhimtat.Visible; }
            set { lblPhimtat.Visible = false; }
        }
       
        public Image PicImg
        {
            get { return pnlImg.BackgroundImage; }
            set { pnlImg.BackgroundImage = value; }
        }
        public Color BackGroundColor
        {
            set
            {
                this.BackColor = value;
                lblPhimtat.BackColor = value;
                txtTieuDe.BackColor = value;
                pnlImg.BackColor = value;
            }
        }
        public Font TitleFont
        {
            get { return txtTieuDe.Font; }
            set { txtTieuDe.Font = value; }
        }
        public Font ShortcutFont
        {
            get { return lblPhimtat.Font; }
            set { lblPhimtat.Font = value; }
        }
        public ContentAlignment ShortcutAlignment
        {
            get { return lblPhimtat.TextAlign; }
            set
            {
                lblPhimtat.TextAlign = value;
            }
        }
        public Point ShortcutLocation
        {
            set { lblPhimtat.Location = value; }
        }

        public string MA_BAOCAO
        {
            get;
            set;
        }
        public bool ShowSaveCommand
        {
            set { cmdSave.Visible = value; }
        }
        public string TIEUDE
        {
            get { return Utility.DoTrim(txtTieuDe.Text); }
            set { txtTieuDe.Text = value; }
        }
        void txtTieuDe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && globalVariables.gv_bQuyenSuaTieudeBaocao)
                THU_VIEN_CHUNG.CapnhatgiatriTieudebaocao(MA_BAOCAO, Utility.DoTrim(txtTieuDe.Text));
        }
        void cmdSave_Click(object sender, EventArgs e)
        {
            THU_VIEN_CHUNG.CapnhatgiatriTieudebaocao(MA_BAOCAO, Utility.DoTrim(txtTieuDe.Text));
        }
        void txtTieuDe_GotFocus(object sender, EventArgs e)
        {
            if (globalVariables.gv_bQuyenSuaTieudeBaocao) txtTieuDe.ReadOnly = false;
        }

        void txtTieuDe_LostFocus(object sender, EventArgs e)
        {
            if (globalVariables.gv_bQuyenSuaTieudeBaocao) txtTieuDe.ReadOnly = true;
        }
        public void Init(string MA_BAOCAO)
        {
            this.MA_BAOCAO = MA_BAOCAO;
            txtTieuDe.Text = THU_VIEN_CHUNG.LaygiatriTieudebaocao(MA_BAOCAO, txtTieuDe.Text, true);
        }
        public void Init()
        {
            txtTieuDe.Text = THU_VIEN_CHUNG.LaygiatriTieudebaocao(MA_BAOCAO, txtTieuDe.Text, true);
        }
    }
}
