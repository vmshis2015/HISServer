using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Libs.AppUI;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using CrystalDecisions.Shared;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;


namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls
{
    public partial class BHYT_79A : UserControl
    {
        private DataTable mdtReport;
        public BHYT_79A()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            cmdSearch.Click += new EventHandler(cmdSearch_Click);
            cmdPrint.Click += new EventHandler(cmdPrint_Click);
            cmdPreview.Click += new EventHandler(cmdPreview_Click);
            cmdExcel.Click += new EventHandler(cmdExcel_Click);
            dtpFromDate.ValueChanged+=new EventHandler(dtpFromDate_ValueChanged);
            dtpToDate.ValueChanged+=new EventHandler(dtpToDate_ValueChanged);
            this.KeyDown += new KeyEventHandler(BHYT_79A_KeyDown);
            this.Load += new EventHandler(BHYT_79A_Load);
           
            optChitiet.CheckedChanged += new EventHandler(optChitiet_CheckedChanged);
            optTonghop.CheckedChanged += new EventHandler(optTonghop_CheckedChanged);
        }

     

        void BHYT_79A_Load(object sender, EventArgs e)
        {
            
        }

       

        void optTonghop_CheckedChanged(object sender, EventArgs e)
        {
            baocaO_TIEUDE1.Init("BHYT_79A_TH");
        }

        void optChitiet_CheckedChanged(object sender, EventArgs e)
        {
            baocaO_TIEUDE1.Init("BHYT_79A_CT");
        }
        public void Init()
        {
            dtCreateDate.Value = globalVariables.SysDate;
            dtpFromDate.Value = globalVariables.SysDate;
            dtpToDate.Value = globalVariables.SysDate;
            baocaO_TIEUDE1.Init("BHYT_79A_TH");
            DataBinding.BindData(cboObject, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(new List<string>(){ "BHYT"}),
                                 DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb);
            if (cboObject.Items.Count > 0) cboObject.SelectedIndex = 0;
        }
        void BHYT_79A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void cmdExcel_Click(object sender, EventArgs e)
        {
            gridEXExporter1.GridEX = grdExcel;
            //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
            if (gridEXExporter1.GridEX.RowCount <= 0)
            {
                Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                //grdList.Focus();
                return;
            }
            saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
            saveFileDialog1.FileName = "BaoCaoBHYT-79a.xls";
            //saveFileDialog1.ShowDialog();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sPath = saveFileDialog1.FileName;
                FileStream fs = new FileStream(sPath, FileMode.Create);
                fs.CanWrite.CompareTo(true);
                fs.CanRead.CompareTo(true);
                gridEXExporter1.Export(fs);
                fs.Dispose();
            }
            saveFileDialog1.Dispose();
            saveFileDialog1.Reset();
        }

        void cmdPreview_Click(object sender, EventArgs e)
        {
            PrintReport(true);
        }

        void cmdPrint_Click(object sender, EventArgs e)
        {
            PrintReport(false);
        }
        void PrintReport(bool view)
        {
            try
            {
                if (mdtReport != null)
                {
                    if (mdtReport.Rows.Count <= 0 || mdtReport.Columns.Count <= 0)
                        return;
                    //Báo cáo chi tiết
                     string tieude="", reportname = "";
                    if (optChitiet.Checked == true)
                    {

                        ReportDocument crpt = Utility.GetReport("BHYT_79A_CT" ,ref tieude,ref reportname);
                        if (crpt == null) return;

                        frmPrintPreview objForm =
                            new frmPrintPreview(
                               baocaO_TIEUDE1.txtTieuDe.Text, crpt,
                                true, mdtReport.Rows.Count <= 0 ? false : true);
                        Utility.UpdateLogotoDatatable(ref mdtReport);
                        crpt.SetDataSource(mdtReport);
                       
                        objForm.mv_sReportFileName = Path.GetFileName(reportname);
                        objForm.mv_sReportCode = "BHYT_79A_CT";
                        Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                        Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
                        Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(crpt,"FromDateToDate", dtpFromDate.Value.ToString("dd/MM/yyyy") + " ĐẾN NGÀY " +
                                               dtpToDate.Value.ToString("dd/MM/yyyy"));
                        Utility.SetParameterValue(crpt,"sTitleReport", baocaO_TIEUDE1.txtTieuDe.Text);
                        Utility.SetParameterValue(crpt,"NTN", Utility.FormatDateTimeWithThanhPho(dtCreateDate.Value));
                        Utility.SetParameterValue(crpt,"TongTien", ChuyenDoiSoThanhChu());
                        Utility.SetParameterValue(crpt, "txtTrinhky",
                                                           Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                              globalVariables.SysDate));
                        objForm.crptViewer.ReportSource = crpt;
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, view))
                        {
                            objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 1);
                            objForm.ShowDialog();
                        }
                        else
                        {
                            crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            crpt.PrintToPrinter(1, false, 0, 0);
                        }
                        Utility.FreeMemory(crpt);

                    }
                    //Báo cáo tổng hợp
                    else if (optTonghop.Checked == true)
                    {
                        ReportDocument crpt = Utility.GetReport("BHYT_79A_TH",ref tieude,ref reportname);
                        if (crpt == null) return;
                        frmPrintPreview objForm =
                            new frmPrintPreview(
                                baocaO_TIEUDE1.txtTieuDe.Text, crpt, true, mdtReport.Rows.Count <= 0 ? false : true);
                        Utility.UpdateLogotoDatatable(ref mdtReport);
                        crpt.SetDataSource(mdtReport);
                       
                        objForm.mv_sReportFileName = Path.GetFileName(reportname);
                        objForm.mv_sReportCode = "BHYT_79A_TH";
                        Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                        Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                        Utility.SetParameterValue(crpt,"Telephone", globalVariables.Branch_Phone);
                        Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                        Utility.SetParameterValue(crpt,"FromDateToDate", dtpFromDate.Value.ToString("dd/MM/yyyy") + " ĐẾN NGÀY " +
                                               dtpToDate.Value.ToString("dd/MM/yyyy"));
                        Utility.SetParameterValue(crpt,"sTitleReport", baocaO_TIEUDE1.txtTieuDe.Text);
                        Utility.SetParameterValue(crpt,"NTN", Utility.FormatDateTimeWithThanhPho(dtCreateDate.Value));
                        Utility.SetParameterValue(crpt,"TongTien", ChuyenDoiSoThanhChu());
                        Utility.SetParameterValue(crpt, "txtTrinhky",
                                                           Utility.getTrinhky(objForm.mv_sReportFileName,
                                                                              globalVariables.SysDate));
                        objForm.crptViewer.ReportSource = crpt;
                        if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, view))
                        {
                            objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 1);
                            objForm.ShowDialog();
                        }
                        else
                        {
                            crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                            crpt.PrintToPrinter(1, false, 0, 0);
                        }
                        Utility.FreeMemory(crpt);
                    }
                    else
                    {
                        MessageBox.Show("Chọn lựa không chính xác", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        string ChuyenDoiSoThanhChu()
        {
            decimal Tongtien = 0;

            var query = from tongtien in mdtReport.AsEnumerable()
                        let x =
                            Utility.DecimaltoDbnull(tongtien["BHCT"], 0) + Utility.DecimaltoDbnull(tongtien["QDS"], 0)
                        select x;

            Tongtien = query.Sum();
            return new MoneyByLetter().sMoneyToLetter(Tongtien.ToString());
        }
        void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }
        void SearchData()
        {
            try
            {
                mdtReport = new BAOCAO_BHYT().BHYT_79A(dtpFromDate.Value, dtpToDate.Value, "BHYT");
                //THU_VIEN_CHUNG.CreateXML(mdtReport, @"Xml4Reports\BHYT_79A.xml");
                ModifyCommands();
                ProcessData();
                grdList.DataSource = mdtReport;
                grdExcel.DataSource = mdtReport;
            }
            catch (Exception ex)
            {
                 Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }
        void ModifyCommands()
        {
            cmdPrint.Enabled = mdtReport != null && mdtReport.Rows.Count > 0;
            cmdPreview.Enabled = cmdPrint.Enabled;
            cmdExcel.Enabled = cmdPrint.Enabled;
        }
        void ProcessData()
        {
            try
            {
                Utility.ResetProgressBar(prgBar, mdtReport.Rows.Count, true);
                if (!mdtReport.Columns.Contains("NamQT")) mdtReport.Columns.Add("NamQT", typeof(string), dtpToDate.Value.Year.ToString());
                if (!mdtReport.Columns.Contains("ThangQT")) mdtReport.Columns.Add("ThangQT", typeof(string), dtpToDate.Value.Month.ToString());
                if (!mdtReport.Columns.Contains("LoaiKCB")) mdtReport.Columns.Add("LoaiKCB", typeof(string));
                if (!mdtReport.Columns.Contains("NoiTT")) mdtReport.Columns.Add("NoiTT", typeof(string));
                if (!mdtReport.Columns.Contains("DoiTuong")) mdtReport.Columns.Add("DoiTuong", typeof(string));
                if (!mdtReport.Columns.Contains("Tuyen")) mdtReport.Columns.Add("Tuyen", typeof(int));
                foreach (DataRow row in mdtReport.Rows)
                {
                    if (row["dung_tuyen"].ToString() == "1")
                    {
                        row["ten_dung_tuyen"] = "I. Đúng Tuyến";
                    }
                    else if (row["dung_tuyen"].ToString() == "0")
                    {
                        row["ten_dung_tuyen"] = "II. Trái Tuyến";
                    }
                    else
                    {
                        row["ten_dung_tuyen"] = "III. Dịch Vụ";
                    }
                    if (row[KcbLuotkham.Columns.MaNoicapBhyt].ToString() == globalVariables.gv_strNoicapBHYT)
                    {
                        if (row[KcbLuotkham.Columns.MaKcbbd].ToString() == globalVariables.gv_strNoiDKKCBBD)
                        {
                            row["DoiTuong"] = globalVariables.gv_strTendoituongNoiTinhKCBBD;
                            row["Tuyen"] = 1;
                        }
                        else
                        {
                            row["DoiTuong"] = globalVariables.gv_strTendoituongNoitinhKhongKCBBD;
                            row["Tuyen"] = 2;
                        }
                    }
                    else
                    {
                        row["DoiTuong"] = globalVariables.gv_strTendoituongNgoaitinh;
                        row["Tuyen"] = 3;
                    }
                    if (string.IsNullOrEmpty(row["ten_nhombhyt"].ToString()))
                    {
                        row["ten_nhombhyt"] = "Dịch vụ";
                        row["Tuyen"] = 0;
                    }
                    row["NoiTT"] = "CSKCB";
                    row["LoaiKCB"] = "Ngoại";
                    UIAction.SetValue4Prg(prgBar, 1);
                }
                mdtReport.AcceptChanges();
            }
            catch
            {
            }
        }
        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                dtpToDate.Value = dtpFromDate.Value;
            }
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                dtpFromDate.Value = dtpToDate.Value;
            }
        }
    }
}
