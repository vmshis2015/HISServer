using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;
using VNS.HIS.NGHIEPVU;
using VNS.HIS.DAL;

using VNS.Libs;


using Microsoft.VisualBasic;


using VNS.HIS.BusRule.Classes;
using reports.Thanhtoan;

namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_PHIEUTHU_KHAM : Form
    {
        private readonly string sAccountName = "DETMAY";
        private  DataTable m_dtReportPhieuThu=new DataTable();
        public decimal v_DiscountRate;
        public int Reg_ID = -1;
        private readonly MoneyByLetter sMoneyByLetter = new MoneyByLetter();
        private ActionResult actionResult = ActionResult.Error;
        private action em_Action = action.Insert;
        //private DataTable m_dtPaymentDetail = new DataTable();
        public DataTable _dtPayment = new DataTable();
        public DataTable _dtPaymentDetail = new DataTable();
        public int ObjectTypeId = -1;
        private NLog.Logger log;
        public int v_ObjectType_Id = -1;
        public frm_PHIEUTHU_KHAM()
        {
            InitializeComponent();
            this.txtTieuDe.LostFocus+=new EventHandler(txtTieuDe_LostFocus);
            dtCreateDate.Value = BusinessHelper.GetSysDateTime();
            BusinessHelper.GetTieuDeBaoCao(this.Name, txtTieuDe.Text);
            //LInsuranceObject.Columns.so
        }
        private void txtTieuDe_LostFocus(object sender, EventArgs eventArgs)
        {
            BusinessHelper.UpdateTieuDe(this.Name,txtTieuDe.Text);
        }
        private void uiGroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void grdPaymentDetail_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataTable m_dtPaymentDetail=new DataTable();
        private int status = 0;
        private void LayThongTinPaymentDetail(string sAccountName)
        {
            m_dtPaymentDetail =
                SPs.KcbThanhtoanLaythongtinchitietTheoid(Utility.Int32Dbnull(txtPayment_ID.Text, -1)).GetDataSet().Tables[0];
            if (!m_dtPaymentDetail.Columns.Contains("Thu_Tu_In"))
                m_dtPaymentDetail.Columns.Add("Thu_Tu_In", typeof(int));
            int idx = 1;
            foreach (DataRow drv in m_dtPaymentDetail.Rows)
            {

                drv["PT"] = Utility.Int32Dbnull(drv["Quantity"], 0) *
                            Utility.DecimaltoDbnull(drv[TPaymentDetail.Columns.SurchargePrice], 0);
                if (sAccountName != "DETMAY")
                {
                    drv["Thu_Tu_In"] = idx;
                    idx++;
                }
            }
            m_dtPaymentDetail.AcceptChanges();
            Utility.SetDataSourceForDataGridEx(grdPaymentDetail, m_dtPaymentDetail, false, true, "1=1", "");
        }
        private void GetData()
        {
            string ReasonBy = "";

            string sAccountName = THU_VIEN_CHUNG.LayMaDviLamViec();
            LayThongTinPaymentDetail(sAccountName);
            //grdPaymentDetail.DataSource = m_dtPaymentDetail;


            SqlQuery sqlQuery = new Select().From(TPhieuthu.Schema)
                .Where(TPhieuthu.Columns.PaymentId).IsEqualTo(Utility.Int32Dbnull(txtPayment_ID.Text)).And(
                    TPhieuthu.Columns.LoaiPhieu).IsEqualTo(status);

            if (sqlQuery.GetRecordCount() <= 0)
            {
                TPayment objPayment = TPayment.FetchByID(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
                if (objPayment != null)
                {
                    dtCreateDate.Value = Convert.ToDateTime(objPayment.PaymentDate);
                    txtPayment_ID.Text = Utility.sDbnull(objPayment.PaymentId, "-1");
                    txtMA_PTHU.Text = BusinessHelper.GetMaPhieuThu(dtCreateDate.Value,0);
                    Janus.Windows.GridEX.GridEXColumn gridExColumn = grdPaymentDetail.RootTable.Columns["TONG"];
                    txtSO_TIEN.Text = Utility.sDbnull(grdPaymentDetail.GetTotal(gridExColumn, Janus.Windows.GridEX.AggregateFunction.Sum));
                    txtSLUONG_CTU_GOC.Text = "1";
                    TPatientInfo objPatientInfo = TPatientInfo.FetchByID(objPayment.PatientId);
                    if (objPatientInfo != null)
                    {
                        txtNGUOI_NOP.Text = objPatientInfo.PatientName;
                        label9.Text = "Người nhận";
                    }
                    if (status == 0)
                    {
                        switch (sAccountName)
                        {
                            case "YHOCHAIQUAN":
                                ReasonBy = BusinessHelper.GetLyDo_PhieuThu(m_dtPaymentDetail);
                                txtLDO_NOP.Text = ReasonBy;
                                break;
                            case "KYDONG":
                                ReasonBy = BusinessHelper.GetLyDo_PhieuThu(m_dtPaymentDetail);
                                txtLDO_NOP.Text = ReasonBy;
                                break;
                            case "DETMAY":
                                LObjectType objectType = LObjectType.FetchByID(v_ObjectType_Id);
                                if (objectType.ObjectTypeType == 0)
                                {
                                    txtLDO_NOP.Text = string.Format("Bệnh nhân cùng chi trả :{0} %", (100 - v_DiscountRate));
                                }
                                else
                                {
                                    ReasonBy = BusinessHelper.GetLyDo_PhieuThu(m_dtPaymentDetail);
                                    txtLDO_NOP.Text = ReasonBy;
                                }

                                break;
                            default:
                                ReasonBy = BusinessHelper.GetLyDo_PhieuThu(m_dtPaymentDetail);
                                txtLDO_NOP.Text = ReasonBy;
                                break;
                        }
                    }
                    if (status == 1)
                    {
                        ReasonBy = "Trả lại tiền cho bệnh nhân";
                        txtLDO_NOP.Text = ReasonBy;
                    }

                }
            }
            else
            {
                var objPhieuthu = sqlQuery.ExecuteSingle<TPhieuthu>();
                if (objPhieuthu != null)
                {
                    txtSLUONG_CTU_GOC.Text = Utility.sDbnull(objPhieuthu.SluongCtuGoc, 1);
                    txtMA_PTHU.Text = Utility.sDbnull(objPhieuthu.MaPthu, "");
                    txtNGUOI_NOP.Text = Utility.sDbnull(objPhieuthu.NguoiNop);
                    txtSO_TIEN.Text = Utility.sDbnull(objPhieuthu.SoTien);
                    txtTKHOAN_CO.Text = Utility.sDbnull(objPhieuthu.TkhoanCo, "");
                    txtTKHOAN_NO.Text = Utility.sDbnull(objPhieuthu.TkhoanNo, "");
                    txtLDO_NOP.Text = objPhieuthu.LdoNop;
                    dtCreateDate.Value = Convert.ToDateTime(objPhieuthu.NgayThien);
                }
            }
        }

        private void frm_PHIEUTHU_KHAM_Load(object sender, EventArgs e)
        {
            GetData();
        }
        private void InPhieuDichVu()
        {
            switch (sAccountName)
            {
                case "YHOCHAIQUAN":
                    YHHQ_PrintPhieuThu("PHIẾU THU");
                    break;
                case "DETMAY":
                    DM_PrintPhieuThu("PHIẾU THU");
                    break;
                case "KYDONG":
                    YHHQ_PrintPhieuThu("PHIẾU THU");
                    break;
                default:
                    YHHQ_PrintPhieuThu("PHIẾU THU");
                    break;
            }
        }
        /// <summary>
        /// hàm thực hiện việc in phieus thu của dệt may
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void DM_PrintPhieuThu(string sTitleReport)
        {
            Utility.WaitNow(this);
            var crpt = new crpt_PhieuThu();
            var objForm = new frmPrintPreview("", crpt, true, true);
            //try
            //{
            crpt.SetDataSource(m_dtReportPhieuThu);
            crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
            crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
            crpt.SetParameterValue("DateTime", Utility.FormatDateTime(dtCreateDate.Value));
            crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
            crpt.SetParameterValue("sTitleReport", sTitleReport);
            crpt.SetParameterValue("CharacterMoney", sMoneyByLetter.sMoneyToLetter(txtSO_TIEN.Text));
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            //}
            //catch (Exception ex)
            //{
            //    Utility.DefaultNow(this);
            //}
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu thu của y học hải quân
        /// </summary>
        /// <param name="sTitleReport"></param>
        private void YHHQ_PrintPhieuThu(string sTitleReport)
        {
            var crpt = new ReportDocument();
            Utility.WaitNow(this);
           
            crpt = new crpt_PhieuThu();
           
            var objForm = new frmPrintPreview("", crpt, true, true);
            //try
            //{
            crpt.SetDataSource(m_dtReportPhieuThu);
            // crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
            crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
            crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
            crpt.SetParameterValue("DateTime", Utility.FormatDateTime(dtCreateDate.Value));
            crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
            crpt.SetParameterValue("sTitleReport", sTitleReport);
            crpt.SetParameterValue("CharacterMoney", sMoneyByLetter.sMoneyToLetter(txtSO_TIEN.Text));
            DataRow dataRow = m_dtReportPhieuThu.Rows[0];
            //if (status != 0)
            //crpt.SetParameterValue("NguoiChi", BusinessHelper.GetStaffByUserName(dataRow["NGUOI_NOP"].ToString()));
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();
            Utility.DefaultNow(this);
            //}
            //catch (Exception ex)
            //{
            //    Utility.DefaultNow(this);
            //}
        }
        private TPhieuthu CreatePhieuThu()
        {
            var objPhieuThu = new TPhieuthu();
            objPhieuThu.PaymentId = Utility.Int32Dbnull(txtPayment_ID.Text, -1);
            objPhieuThu.MaPthu = Utility.sDbnull(txtMA_PTHU.Text, "");
            objPhieuThu.SluongCtuGoc = Utility.Int16Dbnull(txtSLUONG_CTU_GOC.Text);
            objPhieuThu.LoaiPhieu = Convert.ToByte(status);
            objPhieuThu.NgayThien = dtCreateDate.Value;
            objPhieuThu.SoTien = Utility.DecimaltoDbnull(txtSO_TIEN.Text, 0);
            objPhieuThu.NguoiNop = Utility.sDbnull(txtNGUOI_NOP.Text, "");
            objPhieuThu.TkhoanCo = Utility.sDbnull(txtTKHOAN_CO.Text, "");
            objPhieuThu.TkhoanNo = Utility.sDbnull(txtTKHOAN_NO.Text, "");
            objPhieuThu.LdoNop = Utility.sDbnull(txtLDO_NOP.Text, "");
            return objPhieuThu;
        }
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            actionResult = new KCB_THANHTOAN().UpdateDataPhieuThu(CreatePhieuThu());
            switch (actionResult)
            {
                case ActionResult.Success:
                    SqlQuery q = new Select().From(TPhieuthu.Schema)
                        .Where(TPhieuthu.Columns.MaPthu).IsEqualTo(Utility.sDbnull(txtMA_PTHU.Text, "")).And(
                            TPhieuthu.Columns.PaymentId).IsEqualTo(Utility.Int32Dbnull(txtPayment_ID.Text, -1));
                    m_dtReportPhieuThu = q.ExecuteDataSet().Tables[0];
                    if (m_dtReportPhieuThu.Rows.Count <= 0)
                    {
                        Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                        return;
                    }
                    InPhieuDichVu();
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình lập phiếu thu", "Thông báo lỗi", MessageBoxIcon.Warning);
                    break;
            }
        }
        private void Kydong_InPhieuKhambenh(DataTable m_dtReport, string sTitleReport)
        {
            Utility.UpdateLogotoDatatable(ref m_dtReport);
            Utility.WaitNow(this);
            crpt_PhieuKCB crpt = new crpt_PhieuKCB();
            var objForm = new frmPrintPreview("", crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
            try
            {
                //foreach (DataRow row in m_dtReport.Rows)
                //{
                //    row["Department_Name"] = row["Department_Name"].ToString().Replace("Khám", "").ToString();
                //    row["Department_Name"] = "Khám " + row["Department_Name"].ToString();
                //}
                m_dtReport.AcceptChanges();
                crpt.SetDataSource(m_dtReport);
                crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "                                                                           PHÒNG TIẾP ĐÓN   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                crpt.SetParameterValue("Telephone", globalVariables.Branch_Phone);
                crpt.SetParameterValue("Address", globalVariables.Branch_Address);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                //  frmPrintPreview objForm = new frmPrintPreview("", crpt, true, strPatientCode == null ? false : true);
                //  crpt.SetParameterValue("TongTien", Total.ToString());
                //crpt.SetParameterValue("characterMoney", MoneyByLetter.sMoneyToLetter(Total.ToString()));
                //crpt.SetParameterValue("TongTien","10000");
                //crpt.SetParameterValue("Staff_Name", BusinessHelper.GetNameByUserName(globalVariables.UserName));
                crpt.SetParameterValue("CurrentDate", Utility.FormatDateTime(BusinessHelper.GetSysDateTime()));
                crpt.SetParameterValue("sTitleReport", sTitleReport);

                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }


        /// <summary>
        /// hàm thực hiên việc in phiếu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieu_Click(object sender, EventArgs e)
        {
           
            DataTable v_dtData = SPs.KydongSearchRegExamPringting(Reg_ID).GetDataSet().Tables[0];
            // v_dtData = SPs.RptAssignInfoPatient(txtPatientCode.Text).GetDataSet().Tables[0];
            v_dtData.Columns.Add(new DataColumn("BarCode", typeof(byte[])));
            v_dtData.Columns.Add("logo", typeof(byte[]));
            string path = Application.StartupPath;
        
            if (v_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo");
                return;
            }
            Utility.UpdateLogotoDatatable(ref v_dtData);
            Kydong_InPhieuKhambenh(v_dtData, txtTieuDe.Text);
        }

        private void frm_PHIEUTHU_KHAM_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F4)cmdPrint.PerformClick();
            if(e.KeyCode==Keys.F5)cmdInPhieu.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }
    }
}
