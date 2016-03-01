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
    public partial class frmUpdateMaBenhAn : Form
    {
        public frmUpdateMaBenhAn()
        {
            InitializeComponent();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(cboloaibenhan.SelectedIndex <= 0)
                {
                    Utility.ShowMsg("Bạn phải chọn loại bệnh án");
                    cboloaibenhan.Focus();
                    return;
                }
                SqlQuery sqlkt = new Select().From(KcbBenhAn.Schema).Where(KcbBenhAn.Columns.SoBenhAn).IsEqualTo(txtmabenhanmoi.Text).And(KcbBenhAn.Columns.LoaiBa).IsEqualTo(cboloaibenhan.SelectedValue);
                if(sqlkt.GetRecordCount()>0)
                {
                    Utility.ShowMsg("Mã lần khám này đang được sử dụng cho bệnh nhân khác! Bạn cần check lại mã lượt khám khác");
                    txtmabenhanmoi.Focus();
                    return;
                }
                var sp = SPs.SpUpdateMaBenhAn(Utility.sDbnull(txtmalankham.Text), Utility.sDbnull(txtmabenhanmoi.Text));
                sp.Execute();
                Utility.ShowMsg("Bạn update số bệnh án thành công!");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
           
        }


        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmUpdateMaBenhAn_Load(object sender, EventArgs e)
        {
            cboloaibenhan.SelectedIndex = 0;
        }
    }
}
