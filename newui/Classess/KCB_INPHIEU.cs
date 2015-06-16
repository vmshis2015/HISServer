using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using VNS.Libs;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using Microsoft.VisualBasic;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

namespace VNS.HIS.Classes
{
  public  class KCB_INPHIEU
    {
       static MoneyByLetter _moneyByLetter=new MoneyByLetter();
        public static void INPHIEU_KHAM(string MaDoiTuong,DataTable m_dtReport,string sTitleReport,string KhoGiay)
        {
            Utility.UpdateLogotoDatatable(ref  m_dtReport);
            switch (MaDoiTuong)
            {
                case "DV":
                    InPhieuKCB_DV(m_dtReport, sTitleReport, KhoGiay);
                    break;
                case "BHYT":
                    InPhieuKCB_BHYT(m_dtReport, sTitleReport, KhoGiay);
                    break;
                default:
                    InPhieuKCB_DV(m_dtReport, sTitleReport, KhoGiay);
                    break;

            }
        }
        public static void InPhieuKCB_DV(DataTable m_dtReport, string sTitleReport,string KhoGiay)
        {
            ReportDocument reportDocument=new ReportDocument();
             string tieude="", reportname = "";
            switch (KhoGiay)
            {
                case "A4":
                    reportDocument =Utility.GetReport("tiepdon_PhieuKCB_Dvu_A4",ref tieude,ref reportname);
                    break;
                case "A5":
                    reportDocument = Utility.GetReport("tiepdon_PhieuKCB_Dvu_A5" ,ref tieude,ref reportname);
                    break;

            }
            if (reportDocument == null) return;
            
            var crpt = reportDocument;
            var objForm = new frmPrintPreview(sTitleReport, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            try
            {

                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone + globalVariables.SOMAYLE);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInPhieuKCB;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }
      private string SumOfTotal(DataTable dataTable,string FiledName)
      {
          return Utility.sDbnull(dataTable.AsEnumerable().Sum(c => c.Field<decimal>(FiledName)));
      }
      /// <summary>
      /// HÀM THỰC HIỆN VIỆC IN PHIẾU THÔNG TIN KHÁM BỆNH
      /// </summary>
      /// <param name="m_dtReport"></param>
      /// <param name="sTitleReport"></param>
      /// <param name="KhoGiay"></param>
        public static void InPhieuKCB_BHYT(DataTable m_dtReport, string sTitleReport, string KhoGiay)
        {
            ReportDocument reportDocument = new ReportDocument();
             string tieude="", reportname = "";
            switch (KhoGiay)
            {
                case "A4":
                    reportDocument = Utility.GetReport("tiepdon_PhieuKCB_BHYT_A4", ref tieude, ref reportname);
                    break;
                case "A5":
                    reportDocument = Utility.GetReport("tiepdon_PhieuKCB_BHYT_A5", ref tieude, ref reportname);
                    break;

            }
            if (reportDocument == null) return;
            var crpt = reportDocument;
           // VietBaIT.HISLink.Report_LaoKhoa.Report_LaoKhoa.CRPT_BAOCAO_PHIEUKHAMBENH_BAOHIEMYTE crpt = new CRPT_BAOCAO_PHIEUKHAMBENH_BAOHIEMYTE();
            var objForm = new frmPrintPreview("", crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            try
            {

                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "  PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone+globalVariables.SOMAYLE);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt,"sTitleReport", sTitleReport);
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInPhieuKCB, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInPhieuKCB;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
               
            }
        }

        public static void InphieuChidinhCLS(int id_benhnhan,string ma_luotkham, int v_AssignId, string v_AssignCode,string  nhomincls,int selectedIndex, bool inTach ,ref string mayin)
        {
            try
            {
                mayin = "";
                KcbChidinhcl objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);

                DataTable dt = new KCB_THAMKHAM().KcbThamkhamLaydulieuInphieuCls(id_benhnhan, ma_luotkham, v_AssignCode, nhomincls).Tables[0];
                if (dt == null || dt.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu in. Mời bạn kiểm tra lại");
                    return;
                }
                THU_VIEN_CHUNG.CreateXML(dt,"Thamkham_InphieuCLS.XML");
                Utility.UpdateLogotoDatatable(ref dt);
                Utility.CreateBarcodeData(ref dt, v_AssignCode);

                var crpt = new ReportDocument();
                string KhoGiay = "A5";
                bool inchung = false;
                string tieude = "", reportname = "";
                if (PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4) KhoGiay = "A4";
                if (KhoGiay == "A5")
                    if (inTach && selectedIndex == 0)//Nếu in riêng mà chọn tất
                        crpt = Utility.GetReport("thamkham_InphieuchidinhCLS_RIENG_A5", ref tieude, ref reportname);
                    else
                    {
                        inchung = true;
                        crpt = Utility.GetReport("thamkham_InphieuchidinhCLS_A5", ref tieude, ref reportname);
                    }
                else//Khổ giấy A4
                    if (inTach && selectedIndex == 0)//Nếu in riêng mà chọn tất-->Gọi báo cáo nhóm theo nhóm in
                        crpt = Utility.GetReport("thamkham_InphieuchidinhCLS_RIENG_A4", ref tieude, ref reportname);
                    else
                    {
                        inchung = true;
                        crpt = Utility.GetReport("thamkham_InphieuchidinhCLS_A4", ref tieude, ref reportname);
                    }

                if (crpt == null) return;
                if (inchung)
                {
                    List<string> lstNhominCLS = (from p in dt.AsEnumerable()
                                                 where Utility.DoTrim(Utility.sDbnull(p.Field<string>("nhom_in_cls"))) != ""
                                                 select p.Field<string>("nhom_in_cls")
                                               ).Distinct().ToList<string>();
                    if (lstNhominCLS.Count > 1)
                    {
                        string tenphieuchidinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_TENPHIEU_INCHUNG", "PHIẾU CHỈ ĐỊNH CẬN LÂM SÀNG", true); ;
                        foreach (DataRow dr in dt.Rows)
                            dr["ten_nhominphieucls"] = tenphieuchidinh;
                    }
                }
                var objForm = new frmPrintPreview("IN PHIẾU CHỈ ĐỊNH", crpt, true, true);
                try
                {
                    crpt.SetDataSource(dt);
                    //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên        Bác sĩ chỉ định     ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                    Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                    if (!inTach && selectedIndex == 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                            dr[VKcbChidinhcl.Columns.TenNhominphieucls] = THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEUDE_PHIEUCHIDNHCLS_INCHUNG", "PHIẾU CHỈ ĐỊNH", true);
                    }
                    else
                    {
                        Utility.SetParameterValue(crpt, "TitleReport", tieude);
                    }
                    Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithLocation(globalVariables.SysDate, globalVariables.gv_strDiadiem));
                    objForm.crptViewer.ReportSource = crpt;
                    if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInCLS))
                    {
                        objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                        objForm.ShowDialog();
                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                    }
                    else
                    {
                        objForm.addTrinhKy_OnFormLoad();
                        crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                        mayin = PropertyLib._MayInProperties.TenMayInBienlai;
                        crpt.PrintToPrinter(1, false, 0, 0);
                    }
                }
                catch (Exception ex)
                {
                   // Utility.DefaultNow(this);
                }

            }
            catch
            {
            }
        }
    }
 
}
