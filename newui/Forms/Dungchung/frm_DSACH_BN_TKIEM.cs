using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.Libs;
using VNS.HIS.DAL;
using SubSonic;
using VNS.HIS.BusRule.Classes;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_DSACH_BN_TKIEM : Form
    {
        public string MaLuotkham = "";
        public long IdBenhnhan = -1;
        public bool has_Cancel = true;
        public int DepartmentId = -1;
        public bool AutoSearch = false;
        public frm_DSACH_BN_TKIEM()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(frm_DSACH_BN_TKIEM_KeyDown);
            grdPatient.DoubleClick += new EventHandler(grdPatient_DoubleClick);
            grdPatient.KeyDown+=new KeyEventHandler(grdPatient_KeyDown);
            chkByDate.CheckedChanged+=new EventHandler(chkByDate_CheckedChanged);
            cmdTimKiem.Click += new EventHandler(cmdTimKiem_Click);
            txtPatientCode.KeyDown += new KeyEventHandler(txtPatientCode_KeyDown);
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        public void FillAndSearchData(bool theongay,string IdBenhnhan,string MaLuotkham,string TenBenhnhan,string CMT,string Dienthoai,string IdDoituongKCB)
        {
            chkByDate.Checked = theongay;
            txtPatient_ID.Text = IdBenhnhan;
            txtPatientCode.Text = MaLuotkham;
            txtPatientName.Text = TenBenhnhan;
            txtCMT.Text = CMT;
            txtDienthoai.Text = Dienthoai;
            cboObjectType.SelectedIndex = Utility.GetSelectedIndex(cboObjectType, IdDoituongKCB);
            TimKiemThongTin(true);
        }
        void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatientCode.Text.Trim()) != "")
                {
                    string _ID = txtPatient_ID.Text.Trim();
                    string _Name = txtPatientName.Text.Trim();
                    int _Idx = cboObjectType.SelectedIndex;
                    string patient_ID = Utility.GetYY(DateTime.Now) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtPatientCode.Text, 0), "000000");
                    txtPatient_ID.Clear();
                    txtPatientName.Clear();
                    cboObjectType.SelectedIndex = -1;
                    txtPatientCode.Text = patient_ID;
                    radTatCa.Checked = true;
                    TimKiemThongTin(false);
                    cboObjectType.SelectedIndex = _Idx;
                    txtPatientName.Text = _Name;
                    txtPatient_ID.Text = _ID;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
                //throw;
            }
        }

        void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin(true);
        }
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtmTo.Enabled = dtmFrom.Enabled = chkByDate.Checked;
        }
        void TimKiemThongTin(bool theongay)
        {
            try
            {
                int Hos_status = -1;
                if (radNgoaiTru.Checked) Hos_status = 0;
                if (radNoiTru.Checked) Hos_status = 1;
              DataTable  m_dtPatient = new KCB_DANGKY().KcbTiepdonTimkiemBenhnhan(theongay ? (chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                    theongay ? (chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                                                     Utility.Int32Dbnull(cboObjectType.SelectedValue, -1), Hos_status,
                                                     Utility.sDbnull(txtPatientName.Text),
                                                     Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                     Utility.sDbnull(txtPatientCode.Text),
                                                     Utility.sDbnull(txtCMT.Text), 
                                                     Utility.sDbnull(txtDienthoai.Text),globalVariables.MA_KHOA_THIEN,0,(byte)cboTrangthainoitru.SelectedValue,"ALL");
              Utility.SetDataSourceForDataGridEx(grdPatient, m_dtPatient, true, true, "1=1", KcbDanhsachBenhnhan.Columns.IdBenhnhan + " desc");
              grdPatient.MoveFirst();
              Utility.focusCell(grdPatient, KcbDanhsachBenhnhan.Columns.TenBenhnhan);
            }
            catch
            {
            }
            finally
            {
               // ModifyCommand();
            }
        }
        void grdPatient_DoubleClick(object sender, EventArgs e)
        {
            grdPatient_KeyDown(grdPatient, new KeyEventArgs(Keys.Enter));
        }

        void frm_DSACH_BN_TKIEM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                has_Cancel = true;
                this.Close();
            }
        }
        public DataTable dtPatient;
        private void frm_DSACH_BN_TKIEM_Load(object sender, EventArgs e)
        {
            try
            {
                dtmFrom.Value = DateTime.Now;
                dtmTo.Value = DateTime.Now;
                cboTrangthainoitru.SelectedIndex = 0;
                if (!AutoSearch)
                {
                    Utility.SetDataSourceForDataGridEx(grdPatient, dtPatient, true, true, "1=1", KcbDanhsachBenhnhan.Columns.IdBenhnhan + " desc");
                    grdPatient.MoveFirst();
                    Utility.focusCell(grdPatient, KcbDanhsachBenhnhan.Columns.TenBenhnhan);
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình load thông tin bệnh nhân");
            }
        }

        private void grdPatient_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPatient)) return;
                if (e.KeyCode == Keys.Enter)
                {
                    MaLuotkham = Utility.sDbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value, "");
                    IdBenhnhan = Utility.Int64Dbnull(grdPatient.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value, -1);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    has_Cancel = false;
                    Close();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    Close();
                }

            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình chọn bệnh nhân");

            }
        }
    }
}
