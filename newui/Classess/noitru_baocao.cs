using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.HIS.UI.BaoCao;
using Microsoft.VisualBasic;


namespace VNS.HIS.UI.Baocao
{
  public class noitru_baocao
    {
      public static void Inphieunhapvien(DataTable m_dtReport, string sTitleReport, DateTime NgayIn)
        {

            string tieude = "", reportname = "";
            var crpt = Utility.GetReport("noitru_phieunhapvien", ref tieude, ref reportname);
            if (crpt == null) return;
            THU_VIEN_CHUNG.CreateXML(m_dtReport, "noitru_phieunhapvien.xml");
            MoneyByLetter _moneyByLetter = new MoneyByLetter();
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
           // string tinhtong = TinhTong(m_dtReport);
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            try
            {

                crpt.SetDataSource(m_dtReport);
               

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_phieunhapvien";
                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"sCurrentDate", Utility.FormatDateTimeWithThanhPho(NgayIn));
                Utility.SetParameterValue(crpt,"sTitleReport", tieude);
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }
     

        private static string TinhTong(DataTable dataTable)
        {
            string sumoftotal = Utility.sDbnull(dataTable.AsEnumerable().Sum(
                c => c.Field<decimal>(TPhieuNhapxuatthuocChitiet.ThanhTienColumn.ColumnName)));
            return sumoftotal;
        }
        private static string TinhTong(DataTable dataTable,string ColName)
        {
            string sumoftotal = Utility.sDbnull(dataTable.AsEnumerable().Sum(
                c => c.Field<decimal>(ColName)));
            return sumoftotal;
        }
    }
}
