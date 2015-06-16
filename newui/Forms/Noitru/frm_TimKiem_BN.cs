using System;
using System.Data;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;


namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_TimKiem_BN : Form
    {
        public TrangthaiNoitru _TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        public bool b_Cancel = false;
        public DataTable p_mdtDataTimKiem = new DataTable();
        public string SoHSBA { get; set; }
        public string ten_benhnhan { get; set; }
        public string MaLuotkham { get; set; }
        public int IdBenhnhan { get; set; }
        public frm_TimKiem_BN()
        {
            InitializeComponent();
            foreach (Janus.Windows.GridEX.GridEXColumn gridExColumn in grdList.RootTable.Columns)
            {
                gridExColumn.EditType = EditType.NoEdit;
            }
            dtDenNgay.Value = dtTuNgay.Value = globalVariables.SysDate;
            CauHinh();
        }

        private void CauHinh()
        {
           
        }
        private  DataTable m_dtKhoaNoiTru=new DataTable();
        /// <summary>
        /// hàm thực hiện việc load thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_TimKiem_BN_Load(object sender, EventArgs e)
        {
            txtPatientCode.Text = MaLuotkham;
            txtPatientName.Text = ten_benhnhan;
             if (globalVariables.IsAdmin)
            {
                m_dtKhoaNoiTru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                DataBinding.BindDataCombobox(cboKhoaNoiTru, m_dtKhoaNoiTru, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,"Chọn khoa",true);
            }
            else
            {
                //if (!globalVariables._HinhPhanBuongGiuongProperties.IsKeHoachTongHop)
                //{
                //    m_dtKhoaNoiTru = LoadDataCommon.CommonBusiness.LayThongTin_KhoaNoiTruTheoKhoa(globalVariables.DepartmentID);
                //    DataBinding.BindDataCombobox(cboKhoaNoiTru, m_dtKhoaNoiTru, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa", true);
                //}
                //else
                //{
                    m_dtKhoaNoiTru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
                    DataBinding.BindDataCombobox(cboKhoaNoiTru, m_dtKhoaNoiTru, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "Chọn khoa", true);
                //}
               
            }
            
            TimKiem();
        }

        private void TimKiem()
        {

           
            p_mdtDataTimKiem =
                SPs.NoitruTimkiembenhnhan(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue), txtPatientCode.Text,-1,
                    chkByDate.Checked ? dtTuNgay.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                    chkByDate.Checked ? dtDenNgay.Value.ToString("dd/MM/yyyy") : "01/01/1900", txtPatientName.Text,
                    (int?) _TrangthaiNoitru,-1).
                    GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, p_mdtDataTimKiem, true, true, "1=1", "");
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            AcceptData();
        }
        /// <summary>
        /// hàm thực hiện việc chấp nhận thông t ncuar bệnh nhân
        /// </summary>
        private void AcceptData()
        {
            if (grdList.CurrentRow != null && grdList.CurrentRow.RowType == RowType.Record)
            {
                SoHSBA = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                if (_TrangthaiNoitru == TrangthaiNoitru.ChoRaVien)
                {
                    SqlQuery sqlQuery = new Select().From<KcbLuotkham>()
                        .Where(KcbLuotkham.Columns.MaLuotkham)
                        .IsEqualTo(MaLuotkham)
                        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(IdBenhnhan)
                        .And(KcbLuotkham.Columns.TrangthaiNoitru).IsEqualTo(3);
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Bệnh nhân đã hoàn tất thủ tục ra viện \n Mời bạn xem lại hoặc hủy thanh toán","Thông báo",MessageBoxIcon.Error);
                        return;
                    }
                    sqlQuery = new Select().From<KcbThanhtoan>()
                        .Where(KcbThanhtoan.Columns.MaLuotkham)
                        .IsEqualTo(MaLuotkham)
                        .And(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(IdBenhnhan)
                        .And(KcbThanhtoan.Columns.KieuThanhtoan).IsEqualTo(1);
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Bệnh nhân đã thanh toán \n Mời bạn xem lại hoặc hủy thanh toán","Thông báo",MessageBoxIcon.Error);
                        return;
                    }
                }
                b_Cancel = true;
                
                Close();
            }
        }

        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtDenNgay.Enabled = dtTuNgay.Enabled = chkByDate.Checked;
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void frm_TimKiem_BN_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}