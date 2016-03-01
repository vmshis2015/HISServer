using System;
using System.Windows.Forms;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frmUpdateMaBenhNhan : Form
    {
        public frmUpdateMaBenhNhan()
        {
            InitializeComponent();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlQuery sqlkt = new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtmabenhnhanmoi.Text);
                //if(sqlkt.GetRecordCount()>0)
                //{
                //    Utility.ShowMsg("Mã bệnh nhân này đang được sử dụng cho bệnh nhân khác! Bạn cần check lại mã mã bệnh nhân khác");
                //    txtmabenhnhanmoi.Focus();
                //    return;
                //}
                var sp = SPs.SpUpdateMaBenhNhan(Utility.sDbnull(txtmabenhnhanmoi.Text), Utility.sDbnull(txtmabenhnhancu.Text));
                sp.Execute();
                Utility.ShowMsg("Bạn đã update mã bệnh nhân thành công!");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
           
        }

        private void txtmalankhammoi_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtmabenhnhanmoi.Text) != "")
            //{
            //    string _maluotkham  = Utility.AutoFullPatientCode(txtmabenhnhanmoi.Text);
            //    txtmabenhnhanmoi.Text = _maluotkham;
            //}
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
