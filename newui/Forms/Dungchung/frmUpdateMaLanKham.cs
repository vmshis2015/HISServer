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
using SubSonic;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frmUpdateMaLanKham : Form
    {
        public frmUpdateMaLanKham()
        {
            InitializeComponent();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlQuery sqlkt = new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtmalankhammoi.Text);
                if(sqlkt.GetRecordCount()>0)
                {
                    Utility.ShowMsg("Mã lần khám này đang được sử dụng cho bệnh nhân khác! Bạn cần check lại mã lượt khám khác");
                    return;
                }
                var sp = SPs.SpUpdateMaLuotKham(Utility.sDbnull(txtmalankhammoi.Text), Utility.sDbnull(txtmalankhamcu.Text));
                sp.Execute();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
           
        }

        private void txtmalankhammoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtmalankhammoi.Text) != "")
            {
                string _maluotkham  = Utility.AutoFullPatientCode(txtmalankhammoi.Text);
                txtmalankhammoi.Text = _maluotkham;
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
