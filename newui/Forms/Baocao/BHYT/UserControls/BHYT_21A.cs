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

using NLog;
using SubSonic;

namespace VNS.HIS.UI.FORMs.BAOCAO.BHYT.UserControls
{
    public partial class BHYT_21A : UserControl
    {
        private DataTable m_dataTH;
       
        private Logger log;
        public BHYT_21A()
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
            dtpFromDate.ValueChanged += new EventHandler(dtpFromDate_ValueChanged);
            dtpToDate.ValueChanged += new EventHandler(dtpToDate_ValueChanged);
            this.KeyDown += new KeyEventHandler(BHYT_21A_KeyDown);
            this.Load += new EventHandler(BHYT_21A_Load);
            
            optChitiet.CheckedChanged += new EventHandler(optChitiet_CheckedChanged);
            optTonghop.CheckedChanged += new EventHandler(optTonghop_CheckedChanged);
            txtTinhthanh._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtTinhthanh__OnSelectionChanged);
        }

        void txtTinhthanh__OnSelectionChanged()
        {
            AutocompleteKCBBD();
        }

       
        void BHYT_21A_Load(object sender, EventArgs e)
        {

        }

      

        void optTonghop_CheckedChanged(object sender, EventArgs e)
        {
           
            ModifyCommands();
           
        }

        void optChitiet_CheckedChanged(object sender, EventArgs e)
        {
          
            ModifyCommands();
           
        }

       
        public void Init()
        {
            log = LogManager.GetCurrentClassLogger();
            AutocompleteDmuc();
            dtpNgayIn.Value = globalVariables.SysDate;
            dtpFromDate.Value = globalVariables.SysDate;
            dtpToDate.Value = globalVariables.SysDate;
            baocaO_TIEUDE1.Init("BHYT_21A");

            DataBinding.BindData(cboObjectType, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(new List<string>{"DV", "BHYT"}),
                                 DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb);
            txtNhomBHYT.Init();
            if (cboObjectType.DataSource != null && cboObjectType.Items.Count > 0)
                cboObjectType.SelectedIndex = 0;
            if (cboTuyen.Items.Count > 0)
                cboTuyen.SelectedIndex = 0;
        }
        private void AutocompleteKCBBD()
        {
            DataTable dtKCBBD = null;
            try
            {
                DataRow[] arrDR = globalVariables.gv_dtDmucNoiKCBBD.Select(DmucNoiKCBBD.MaKcbbdColumn.ColumnName + "='" + Utility.DoTrim(txtTPCode.Text) + "'");
                if (arrDR.Length <= 0)
                {
                    this.txtTinhthanh.AutoCompleteList = null;
                    return;
                }
                dtKCBBD = arrDR.CopyToDataTable();
                if (dtKCBBD == null) return;
                if (!dtKCBBD.Columns.Contains("ShortCut"))
                    dtKCBBD.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in dtKCBBD.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucNoiKCBBD.TenKcbbdColumn.ColumnName].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucNoiKCBBD.TenKcbbdColumn.ColumnName].ToString().Trim());
                    shortcut = dr[DmucNoiKCBBD.MaKcbbdColumn.ColumnName].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in dtKCBBD.AsEnumerable()
                            select p[DmucNoiKCBBD.IdKcbbdColumn.ColumnName].ToString() + "#" + p[DmucNoiKCBBD.MaKcbbdColumn.ColumnName].ToString() + "@" + p[DmucNoiKCBBD.TenKcbbdColumn.ColumnName].ToString() + "@" + p["shortcut"].ToString();
                source = query.ToList();
                this.txtKCBBD.AutoCompleteList = source;
                this.txtKCBBD.TextAlign = HorizontalAlignment.Center;
                this.txtKCBBD.CaseSensitive = false;
                this.txtKCBBD.MinTypedCharacters = 1;

            }
        }
        private void AutocompleteDmuc()
        {
            DataTable dtTinhTP = null;
            try
            {
                dtTinhTP =
     new Select(DmucDiachinh.Columns.TenDiachinh, DmucDiachinh.Columns.MaDiachinh).From(DmucDiachinh.Schema).Where(
         DmucDiachinh.Columns.LoaiDiachinh).IsEqualTo(0).ExecuteDataSet().Tables[0];

                if (dtTinhTP == null) return;
                if (!dtTinhTP.Columns.Contains("ShortCut"))
                    dtTinhTP.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                foreach (DataRow dr in dtTinhTP.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucDiachinh.TenDiachinhColumn.ColumnName].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucDiachinh.TenDiachinhColumn.ColumnName].ToString().Trim());
                    shortcut = dr[DmucDiachinh.MaDiachinhColumn.ColumnName].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = "";
                    string _Nospace = "";
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                        {
                            _space += word + " ";
                            //_Nospace += word;
                        }
                    }
                    shortcut += _space; // +_Nospace;
                    foreach (string word in arrWords)
                    {
                        if (word.Trim() != "")
                            shortcut += word.Substring(0, 1);
                    }
                    dr["ShortCut"] = shortcut;
                }
            }
            catch
            {
            }
            finally
            {
                var source = new List<string>();
                var query = from p in dtTinhTP.AsEnumerable()
                            select p[DmucDiachinh.MaDiachinhColumn.ColumnName].ToString() + "#" + p[DmucDiachinh.MaDiachinhColumn.ColumnName].ToString() + "@" + p[DmucDiachinh.TenDiachinhColumn.ColumnName].ToString() + "@" + p.Field<string>("shortcut").ToString();
                source = query.ToList();
                this.txtTinhthanh.AutoCompleteList = source;
                this.txtTinhthanh.TextAlign = HorizontalAlignment.Center;
                this.txtTinhthanh.CaseSensitive = false;
                this.txtTinhthanh.MinTypedCharacters = 1;

            }
        }
        void BHYT_21A_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void cmdExcel_Click(object sender, EventArgs e)
        {
            gridEXExporter1.GridEX = grdList;
            if (gridEXExporter1.GridEX.RowCount <= 0)
            {
                Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                //grdList.Focus();
                return;
            }
            saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
            saveFileDialog1.FileName = "BHYT-21A.xls";
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

        private void PrintReport(bool view)
        {
            try
            {
                prgBar.Visible = true;
                if (m_dataTH != null)
                {
                    if (m_dataTH.Rows.Count <= 0 || m_dataTH.Columns.Count <= 0)
                        return;
                     string tieude="", reportname = "";
                    ReportDocument crpt = Utility.GetReport("BHYT_21A",ref tieude,ref reportname);
                    if (crpt == null) return;
                    frmPrintPreview objForm = new frmPrintPreview(Utility.DoTrim(baocaO_TIEUDE1.TIEUDE), crpt, true, true);
                    if (m_dataTH.Rows.Count <= 0)
                        return;
                    Utility.UpdateLogotoDatatable(ref m_dataTH);
                    THU_VIEN_CHUNG.CreateXML(m_dataTH, "BHYT_21A.xml");
                    crpt.SetDataSource(m_dataTH);
                    objForm.mv_sReportFileName = Path.GetFileName(reportname);
                    objForm.mv_sReportCode = "BHYT_21A";
                    Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                    Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                    Utility.SetParameterValue(crpt,"ReportCondition", GetReportCondition());
                    Utility.SetParameterValue(crpt,"NTN", Utility.FormatDateTimeWithThanhPho(dtpNgayIn.Value));
                    Utility.SetParameterValue(crpt,"sBangChu", new MoneyByLetter().sMoneyToLetter(SumOfTotal(m_dataTH)));
                    Utility.SetParameterValue(crpt,"sTitleReport", baocaO_TIEUDE1.TIEUDE);
                    objForm.crptViewer.ReportSource = crpt;
                    if (view)
                    {
                        objForm.ShowDialog();
                    }
                    else
                    {
                        crpt.PrintOptions.PaperSize = PaperSize.PaperA4;
                        crpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                        crpt.PrintToPrinter(1, false, 1, 1);

                    }
                    prgBar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi trong qua trinh in bao cao : (0)", ex);
                prgBar.Visible = false;
            }

        }
        private string GetReportCondition()
        {
            string reval = "";
            if(chkDate.Checked)
            {
              
                reval += "Từ ngày " + dtpFromDate.Value.ToString("dd/MM/yyyy") + " đến ngày " + dtpToDate.Value.ToString("dd/MM/yyyy");
                reval += "Nhóm BHYT: " + txtNhomBHYT.myCode == "-1" ? "Tất cả" : txtNhomBHYT.Text + "; Tuyến: " + cboTuyen.Text + " ;";
               
            }
            else
            {
                reval += "Từ ngày " + "01/01/1900 " + "đến ngày " + globalVariables.SysDate.ToString("dd/MM/yyyy");
                reval += "Nhóm BHYT: " + txtNhomBHYT.myCode == "-1" ? "Tất cả" : txtNhomBHYT.Text + "; Tuyến: " + cboTuyen.Text + " ;";
            }
            return reval;
          
        }
        private string SumOfTotal(DataTable dataTable)
        {
            return Utility.sDbnull(dataTable.Compute("SUM(THANH_TIEN)", "1=1"));
        }
    
        void cmdSearch_Click(object sender, EventArgs e)
        {
            
                LoadTH();
           
            ModifyCommands();
        }
        void LoadTH()
        {
            try
            {
                m_dataTH =
                    new BAOCAO_BHYT().BHYT_21A(chkDate.Checked ? dtpFromDate.Value : Convert.ToDateTime("01/01/1900"),
                                             chkDate.Checked ? dtpToDate.Value : globalVariables.SysDate,
                                             "BHYT",
                                             -1,
                                             txtNhomBHYT.myCode,
                                             Utility.Int32Dbnull(cboTuyen.SelectedIndex-1, -1),
                                             Utility.sDbnull(globalVariables.gv_strNoiDKKCBBD, "01"),
                                             globalVariables.gv_strNoicapBHYT, Utility.sDbnull(txtKCBBDCode.Text, -1),
                                             chkKhacMa.Checked ? "KHAC" : "BANG");

                THU_VIEN_CHUNG.CreateXML(m_dataTH, "BHYT21A.xml");
                Utility.SetDataSourceForDataGridEx(grdList, m_dataTH, true, true, "1=1", "");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
     
      

        void ModifyCommands()
        {
            cmdPrint.Enabled = m_dataTH != null && m_dataTH.Rows.Count > 0 ;
            cmdPreview.Enabled = cmdPrint.Enabled;
            cmdExcel.Enabled = cmdPrint.Enabled;
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
