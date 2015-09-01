using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.HIS.DAL;
using VNS.Libs;
using VNS.Properties;
namespace VNS.UI.QMS
{
    public partial class frm_ScreenSoKham : Form
    {
        
        public string TenBenhVien { get; set; }
        public string TenQuay { get; set; }

        public frm_ScreenSoKham()
        {
            InitializeComponent();
           // this.SetScreenToFirstNonPrimary();
            CauHinh();
            this.BackColor = lblSoKham.BackColor;
            cmdConfig.Click+=new EventHandler(cmdConfig_Click);
        }
        public void SetQMSValue(string sokham,int IsUuTien)
        {
            Utility.SetMsg(lblSoKham,sokham,true);
            grpThongTin.Text = Utility.DoTrim(PropertyLib._HISQMSProperties.TenLoaiQMS) == "" ? (IsUuTien == 1 ? "SỐ ƯU TIÊN" : "SỐ THƯỜNG") : Utility.DoTrim(PropertyLib._HISQMSProperties.TenLoaiQMS);
        }
        /// <summary>
        /// hàm thực hiện việc cấu hình
        /// </summary>
        private void CauHinh()
        {
            try
            {
                this.Text = Utility.sDbnull(PropertyLib._HISQMSProperties.TenBenhVien);
                Utility.SetMsg(lblTenDonVi, Utility.sDbnull(PropertyLib._HISQMSProperties.TenBenhVien,globalVariables.Branch_Name), true);
                Utility.SetMsg(lblQuaySo, Utility.sDbnull(PropertyLib._HISQMSProperties.TenQuay, ""), true);
                //// new Font("Microsoft Sans Serif", 14)
                Font font = new Font("Times New Roman", PropertyLib._HISQMSProperties.FontSize, FontStyle.Bold);

                lblSoKham.Font = font;
                font = new Font("Times New Roman", PropertyLib._HISQMSProperties.LoaiBNFontSize, FontStyle.Bold);
                grpThongTin.FormatStyle.Font = font;
                if (PropertyLib._HISQMSProperties.TestMode)
                {
                    lblSoKham.Font = new Font("Times New Roman", 50, FontStyle.Bold);
                    grpThongTin.FormatStyle.Font = new Font("Times New Roman", 30, FontStyle.Bold);
                }
            }
            catch (Exception)
            {
            }
        }
        private void frm_ScreenSoKham_Load(object sender, EventArgs e)
        {
            
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties( PropertyLib._HISQMSProperties);
            frm.TopMost = true;
            frm.ShowDialog();
            CauHinh();
        }

        private void PassData(string sokham)
        {
            Utility.SetMsg(lblSoKham, sokham,true);
        }

        private void lblSoKham_Click(object sender, EventArgs e)
        {

        }
    }
}
