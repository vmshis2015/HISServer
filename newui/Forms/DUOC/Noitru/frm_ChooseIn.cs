using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using VNS.Properties;
using VNS.HIS.NGHIEPVU.THUOC;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_ChooseIn : Form
    {
        private string thamso = "KHOA";
        public frm_ChooseIn(string sthamso)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.thamso = sthamso;
            dtDenNgay.Value = dtDenNgay.Value = BusinessHelper.GetSysDateTime();
            cauhinh();
        }

        private void cauhinh()
        {
            globalVariables._HisSoTamTraProperties = Utility.GetHisSoTamTraProperties();
            if (globalVariables._HisSoTamTraProperties != null)
            {
                chkInNgay.Checked = globalVariables._HisSoTamTraProperties.isInThangMayin;
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataTable m_DataTimKiem=new DataTable();
        private void cmdInBaoCao_Click(object sender, EventArgs e)
        {

            if (!VietBaIT.CommonLibrary.InvaliDanhMuc.InValiKhoaPhong(Utility.Int32Dbnull(cboKhoa.SelectedValue)))
            {
                Utility.ShowMsg("Mời bạn chọn khoa phòng của mình\n Nếu chưa có liên hệ với quản trị mạng để cấu hình","Thông báo",MessageBoxIcon.Error);
                return;
            }
            string sfileName = AppDomain.CurrentDomain.BaseDirectory + "sotamtra\\sotamtra.xls";
            string sfileNameSave = AppDomain.CurrentDomain.BaseDirectory + "sotamtra\\" + string.Format("{0}_{1}",cboKhoa.Text, dtDenNgay.Value.ToString("yyyyMMddHHmmss")) + ".xls";
            CommonLibrary.ExcelUtlity obj = new CommonLibrary.ExcelUtlity();
            m_DataTimKiem =
             SPs.DuocSoTamCha(dtDenNgay.Value, dtDenNgay.Value,
                 Utility.Int16Dbnull(cboKhoa.SelectedValue, -1), radLinhThuong.Checked ? 0 : 1).GetDataSet().Tables[0];
            if (m_DataTimKiem.Rows.Count <= 0 | m_DataTimKiem.Columns.Count <= 1)
            {
                Utility.ShowMsg("Không tìm thấy kết quả !");
                return;
            }
            Utility.AddColumToDataTable(ref m_DataTimKiem,"STT",typeof(Int32));
            int idx = 1;
            foreach (DataRow drv in m_DataTimKiem.Rows)
            {
                drv["STT"] = idx;
                idx++;
            }
            m_DataTimKiem.AcceptChanges();
            obj.WriteDataTableToExcel_SoTamTra(m_DataTimKiem, "sotamtra", sfileNameSave, radLinhThuong.Checked ? radLinhThuong.Text : radLinhBoSung.Text, Utility.sDbnull(cboKhoa.Text), dtDenNgay.Text);
          //  exportReport(3,)

          
        }
        static public bool exportReport(int type, ReportDocument repd)
        {
            SaveFileDialog f = new SaveFileDialog();
            bool result = false;
            switch (type)
            {
                case 1:

                    f.Filter = "Word file(*.doc)|*.doc";
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        repd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, f.FileName);
                        result = true;
                    }
                    break;
                case 2:

                    f.Filter = "Pdf file(*.pdf)|*.pdf";
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        repd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, f.FileName);
                        result = true;
                    }
                    break;
                case 3:

                    f.Filter = "Excel file(*.xls)|*.xls";
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        repd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, f.FileName);
                        result = true;
                    }
                    break;
                default:
                    MessageBox.Show("Không chọn đúng loại.");
                    break;


            }
            return result;
        }




        private DataTable m_dtKhoaNoiTru = new DataTable();
        private void frm_ChooseIn_Load(object sender, EventArgs e)
        {

           if (!globalVariables.IsAdmin)
            {
                if (thamso.Equals("KHO"))
                {
                    m_dtKhoaNoiTru = LoadDataCommon.CommonBusiness.LayThongTin_KhoaNoiTru();
                    DataBinding.BindDataCombox(cboKhoa, m_dtKhoaNoiTru, LDepartment.Columns.DepartmentId, LDepartment.Columns.DepartmentName, "--Khoa nội trú---");
                }

                else
                {
                    m_dtKhoaNoiTru = LoadDataCommon.CommonBusiness.LayThongTin_KhoaNoiTruTheoKhoa(globalVariables.DepartmentID);
                    DataBinding.BindData(cboKhoa, m_dtKhoaNoiTru, LDepartment.Columns.DepartmentId, LDepartment.Columns.DepartmentName);
                }

            }
            else
            {
                m_dtKhoaNoiTru = LoadDataCommon.CommonBusiness.LayThongTin_KhoaNoiTru();
                DataBinding.BindDataCombox(cboKhoa, m_dtKhoaNoiTru, LDepartment.Columns.DepartmentId, LDepartment.Columns.DepartmentName, "--Khoa nội trú---");
            }
        }

        private void frm_ChooseIn_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.F4)cmdInBaoCao.PerformClick();
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            VietBaIT.HISLink.UI.ControlUtility.CauHinh.frm_CauHinh_TamTra frm=new frm_CauHinh_TamTra();
            frm.HisclsProperties = Utility.GetHisSoTamTraProperties();
            frm.ShowDialog();
        }

        private void chkInNgay_CheckedChanged(object sender, EventArgs e)
        {
            SaveCauHinh();
        }

        private void SaveCauHinh()
        {
            if (globalVariables._HisSoTamTraProperties != null)
            {
                globalVariables._HisSoTamTraProperties.isInThangMayin = chkInNgay.Checked;
                Utility.SaveHisSoTamTraConfig(globalVariables._HisSoTamTraProperties);
            }
        }

        private void frm_ChooseIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCauHinh();
        }
    }
}
