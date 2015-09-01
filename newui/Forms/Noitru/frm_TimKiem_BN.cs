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
using System.Collections.Generic;


namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_TimKiem_BN : Form
    {
        public bool m_blnCancel = true;
        public DataTable p_mdtDataTimKiem = new DataTable();
        public string ten_benhnhan { get; set; }
        public string MaLuotkham { get; set; }
        public int IdBenhnhan { get; set; }
        public bool SearchByDate { get{return chkByDate.Checked;} set{chkByDate.Checked=value;} }
        public frm_TimKiem_BN()
        {
            InitializeComponent();
            foreach (Janus.Windows.GridEX.GridEXColumn gridExColumn in grdList.RootTable.Columns)
            {
                gridExColumn.EditType = EditType.NoEdit;
            }
            this.KeyDown += frm_TimKiem_BN_KeyDown;
            dtDenNgay.Value = dtTuNgay.Value = globalVariables.SysDate;
            grdList.DoubleClick+=grdList_DoubleClick;
            grdList.KeyDown += grdList_KeyDown;
            cmdTimKiem.Click+=cmdTimKiem_Click;
            
            CauHinh();
        }

        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utility.isValidGrid(grdList) && e.KeyCode == Keys.Enter)
            {
                AcceptData();
            }
        }

        void frm_TimKiem_BN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) this.Close();
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
                txtKhoanoitru.Init(m_dtKhoaNoiTru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            }
            else
            {
                m_dtKhoaNoiTru = THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1);
                txtKhoanoitru.Init(m_dtKhoaNoiTru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            }
            
            TimKiem();
        }

        private void TimKiem()
        {
            p_mdtDataTimKiem =
                SPs.NoitruTimkiembenhnhan(Utility.Int32Dbnull(txtKhoanoitru.MyID), txtPatientCode.Text, -1,
                    chkByDate.Checked ? dtTuNgay.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                    chkByDate.Checked ? dtDenNgay.Value.ToString("dd/MM/yyyy") : "01/01/1900", txtPatientName.Text,
                    1,-1,0).
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
            if (Utility.isValidGrid(grdList))
            {
                ten_benhnhan = Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan));
                MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                m_blnCancel = false;
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