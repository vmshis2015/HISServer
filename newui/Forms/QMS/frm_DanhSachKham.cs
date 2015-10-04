using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using SortOrder = Janus.Windows.GridEX.SortOrder;
using VNS.Properties;

namespace VNS.UI.QMS
{
    public partial class frm_DanhSachKham : Form
    {
        public frm_DanhSachKham()
        {
            InitializeComponent();
            
            timer1.Tick += new System.EventHandler(OnTimerEvent);
            CauHinh();
        }
        public void OnTimerEvent(object source, EventArgs e)
        {
            LoadDanhSach();
        }
        
        private void CauHinh()
        {
            
            if (PropertyLib._HISQMSProperties!=null)
            {

                if (PropertyLib._HISQMSProperties.ThoiGianTuDongLay <= 0) PropertyLib._HISQMSProperties.ThoiGianTuDongLay = 1000;

                timer1.Interval = PropertyLib._HISQMSProperties.ThoiGianTuDongLay;
                timer1.Enabled = true;
                timer1.Start();
                
            }
            else
            {
                timer1.Enabled = true;
                timer1.Start();
                timer1.Interval =1000;
            }
           
        }
        private DataTable m_dtDanhSachChoKham=new DataTable();
        private void LoadDanhSach()
        {
            try
            {
                m_dtDanhSachChoKham =
                    SPs.QmsLaydanhsachbenhnhanchokham(PropertyLib._HISQMSProperties.MaPhongKham,
                                                      globalVariables.MA_KHOA_THIEN,
                                                      PropertyLib._HISQMSProperties.SoLuongHienThi).GetDataSet().Tables[
                                                          0];
                Utility.SetDataSourceForDataGridEx(grdList, m_dtDanhSachChoKham, false, true, "1=1", "");
                Janus.Windows.GridEX.GridEXColumn gridExColumn = grdList.RootTable.Columns["STT"];
                Utility.SetGridEXSortKey(grdList, gridExColumn,SortOrder.Ascending);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
                
            }
           
        }
        private void frm_DanhSachKham_Load(object sender, EventArgs e)
        {
           // LoadDanhSach();
        }
        /// <summary>
        /// hàm thực hiện viec
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Properties frm = new frm_Properties( PropertyLib._HISQMSProperties);
                frm.ShowDialog();
                CauHinh();
            }
            catch (Exception)
            {
                
            }
          
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
        }
       
    }
}
