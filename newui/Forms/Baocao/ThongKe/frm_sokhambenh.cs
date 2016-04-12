using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Baocao.ThongKe
{
    public partial class frm_sokhambenh : Form
    {
        private readonly string thamso = "";
        public DataTable _dtData = new DataTable();
        private DataTable m_dtKhoathucHien = new DataTable();
        private string reportname = "";
        private string tieude = "";

        public frm_sokhambenh(string sThamSo)
        {
            InitializeComponent();
            Initevents();
            thamso = sThamSo;
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
        }

        private void Initevents()
        {
            KeyDown += frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown;
            cmdExit.Click += cmdExit_Click;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load;
        }

        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                Autocompletenhanvien();
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                             DmucDoituongkcb.Columns.IdDoituongKcb,
                                             DmucDoituongkcb.Columns.TenDoituongKcb, "Chọn đối tượng KCB", true);

                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox(cboKhoa, m_dtKhoathucHien,
                                             DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                             "Chọn khoa KCB", true);
                EnumerableRowCollection<DataRow> query = from khoa in m_dtKhoathucHien.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) ==
                                                             globalVariables.MA_KHOA_THIEN
                                                         select khoa;
                if (query.Any())
                {
                    cboKhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi load chức năng!", ex);
            }
        }

        private void Autocompletenhanvien()
        {
            DataTable _nhanvien = SPs.DmucLaydanhsachNhanvienTiepdon().GetDataSet().Tables[0];
            try
            {
                if (_nhanvien == null) return;
                if (!_nhanvien.Columns.Contains("ShortCut"))
                    _nhanvien.Columns.Add(new DataColumn("ShortCut", typeof (string)));
                foreach (DataRow dr in _nhanvien.Rows)
                {
                    string realName = dr[DmucNhanvien.Columns.TenNhanvien].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucNhanvien.Columns.TenNhanvien].ToString().Trim());
                    string shortcut = dr[DmucNhanvien.Columns.UserName].ToString().Trim();
                    string[] arrWords = realName.ToLower().Split(' ');
                    string _space = arrWords.Where(word => word.Trim() != "").Aggregate("", (current, word) => current + (word + " "));
                    shortcut += _space; // +_Nospace;
                    shortcut = arrWords.Where(word => word.Trim() != "").Aggregate(shortcut, (current, word) => current + word.Substring(0, 1));
                    dr["ShortCut"] = shortcut;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
            finally
            {
                if (_nhanvien != null)
                {
                    EnumerableRowCollection<string> query = from p in _nhanvien.AsEnumerable()
                                                            select
                                                                p[DmucNhanvien.Columns.IdNhanvien] + "#" +
                                                                Utility.sDbnull(p[DmucNhanvien.Columns.UserName], "") + "@" +
                                                                p.Field<string>(DmucNhanvien.Columns.TenNhanvien) + "@" +
                                                                p.Field<string>("shortcut");
                    List<string> source = query.ToList();
                    txtNhanvientiepdon.AutoCompleteList = source;
                }
                txtNhanvientiepdon.TextAlign = HorizontalAlignment.Center;
                txtNhanvientiepdon.CaseSensitive = false;
                txtNhanvientiepdon.MinTypedCharacters = 1;
            }
        }

        /// <summary>
        /// trạng thái của tìm kiếm từ ngày tới ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }

        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin của form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieuXN.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
            //  if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }

        /// <summary>
        /// hàm thực hiên việc thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện việc export excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelUtlity.ExportGridEx(grdList);
                //saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                //saveFileDialog1.FileName = string.Format("{0}.xls",tieude);
                ////saveFileDialog1.ShowDialog();
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    string sPath = saveFileDialog1.FileName;
                //    FileStream fs = new FileStream(sPath, FileMode.Create);
                //    fs.CanWrite.CompareTo(true);
                //    fs.CanRead.CompareTo(true);
                //    gridEXExporter1.Export(fs);
                //    fs.Dispose();
                //}
                //saveFileDialog1.Dispose();
                //saveFileDialog1.Reset();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            _dtData =
                SPs.BaocaoThongkeSokhambenh(dtFromDate.Value, dtToDate.Value,
                                            Utility.Int16Dbnull(cboDoituongKCB.SelectedValue, -1),
                                            Utility.Int16Dbnull(txtNhanvientiepdon.txtMyID, -1),
                                            Utility.sDbnull(cboKhoa.SelectedValue, "KKB"), thamso, -1).GetDataSet().
                    Tables[0];

            Utility.SetDataSourceForDataGridEx(grdList, _dtData, false, true, "1=1", "");
            THU_VIEN_CHUNG.CreateXML(_dtData, "baocao_thongke_sokhambenh.XML");
            if (_dtData.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo",
                                MessageBoxIcon.Information);
                return;
            }
            Utility.UpdateLogotoDatatable(ref _dtData);
            reportname = "baocao_thongke_sokhambenh";

            string Condition =
                string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Khoa KCB :{3} - Người tiếp đón: {4}",
                              dtFromDate.Text, dtToDate.Text,
                              cboDoituongKCB.SelectedIndex >= 0
                                  ? Utility.sDbnull(cboDoituongKCB.Text)
                                  : "Tất cả",
                              cboKhoa.SelectedIndex > 0
                                  ? Utility.sDbnull(cboKhoa.Text)
                                  : "Tất cả", txtNhanvientiepdon.MyCode == "-1" ? "Tất cả" : txtNhanvientiepdon.Text);
            ReportDocument crpt = Utility.GetReport(reportname, ref tieude, ref reportname);
            if (crpt == null) return;

            string StaffName = globalVariables.gv_strTenNhanvien;
            if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;
            try
            {
                var objForm = new frmPrintPreview(tieude, crpt, true, _dtData.Rows.Count > 0);
                //try
                //{
                crpt.SetDataSource(_dtData);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportname;
                crpt.SetParameterValue("StaffName", StaffName);
                crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                crpt.SetParameterValue("Address", globalVariables.Branch_Address);
                crpt.SetParameterValue("FromDateToDate", Condition);
                crpt.SetParameterValue("sTitleReport", tieude);
                crpt.SetParameterValue("sCurrentDate", Utility.FormatDateTimeWithThanhPho(dtNgayInPhieu.Value));
                crpt.SetParameterValue("BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                                          Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }
    }
}