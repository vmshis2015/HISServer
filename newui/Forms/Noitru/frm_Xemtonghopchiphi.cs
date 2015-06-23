using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Xemtonghopchiphi : Form
    {
        public KcbLuotkham objLuotkham = null;
        DataTable m_dtChiPhiThanhtoan = null;
        bool Khoanoitrutonghop = true;
        bool mv_blnHasloaded = false;
        string idkhoanoitru = "-1";
        public frm_Xemtonghopchiphi(bool Khoanoitrutonghop,string idkhoanoitru)
        {
            InitializeComponent();
            this.idkhoanoitru = idkhoanoitru;
            this.Khoanoitrutonghop = Khoanoitrutonghop;
            pnlCachthuchienthidulieu.Visible = !Khoanoitrutonghop;
            optAll.Checked = true;
            InitEvents();
        }
        void InitEvents()
        {
            this.Load += frm_Xemtonghopchiphi_Load;
            cmdExit.Click += cmdExit_Click;
            cmdRefresh.Click += cmdRefresh_Click;
            cmdPrint.Click += cmdPrint_Click;
            optAll.CheckedChanged += optAll_CheckedChanged;
            optNoitru.CheckedChanged += optAll_CheckedChanged;
            optNgoaitru.CheckedChanged += optAll_CheckedChanged;
        }
        void optAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!mv_blnHasloaded) return;
                string RowFilter = "1=1";
                PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Tatca;
                if (optNoitru.Checked)
                {
                    PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Noitru;
                    RowFilter = "noi_tru=1";
                }
                if (optNgoaitru.Checked)
                {
                    PropertyLib._ThanhtoanProperties.CachhienthidulieuNoitru = DisplayType.Ngoaitru;
                    RowFilter = "noi_tru=0";
                }
                m_dtChiPhiThanhtoan.DefaultView.RowFilter = RowFilter;
                m_dtChiPhiThanhtoan.AcceptChanges();
                PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
            }
            catch (Exception ex)
            {


            }
        }
        void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                DataTable dtData = SPs.NoitruTonghopChiphiRavien(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan,Utility.Bool2byte(!Khoanoitrutonghop),idkhoanoitru).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(dtData, "noitru_tonghopchiphiravien.XML");
                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                foreach (DataRow drv in dtData.Rows)
                {
                    if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "1"//Chi phí KCB
                        || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "0"//Phí KCB kèm theo
                        || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "4"//Buồng giường
                        || drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "8"//Gói dịch vụ
                        )
                    {

                        drv["ten_loaidichvu"] = string.Empty;
                        drv["STT"] = 1;
                        drv["id_loaidichvu"] = -1;
                    }
                    else if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "2")
                    {
                        string ma_loaidichvu = Utility.sDbnull(drv["id_loaidichvu"], -1);
                        //drv["id_loaidichvu"]-->Được xác định trong câu truy vấn

                        DmucChung objService = THU_VIEN_CHUNG.LaydoituongDmucChung("LOAIDICHVUCLS", ma_loaidichvu);
                        if (objService != null)
                        {
                            drv["ten_loaidichvu"] = Utility.sDbnull(objService.Ten);
                            drv["STT"] = Utility.sDbnull(objService.SttHthi);
                        }

                    }
                    else if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "3")
                    {
                        int Drug_ID = Utility.Int32Dbnull(drv["id_dichvu"], -1);
                        DmucThuoc objDrug = DmucThuoc.FetchByID(Drug_ID);
                        if (objDrug != null)
                        {
                            if (objDrug.KieuThuocvattu == "THUOC")
                            {
                                drv["id_loaidichvu"] = 1;
                                drv["STT"] = 1;
                                drv["ten_loaidichvu"] = "Thuốc và dịch truyền";
                            }
                            else
                            {
                                drv["id_loaidichvu"] = 2;
                                drv["STT"] = 2;
                                drv["ten_loaidichvu"] = "Vật tư y tế ";
                            }
                        }
                    }
                    if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "5")
                    {
                        drv["id_loaidichvu"] = 1;
                        drv["STT"] = 1;
                        drv["ten_loaidichvu"] = "Chi phí thêm  ";
                    }
                }
                THU_VIEN_CHUNG.Sapxepthutuin(ref dtData, true);
                dtData.DefaultView.Sort = "stt_in ,stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu";
                dtData.AcceptChanges();

                Utility.UpdateLogotoDatatable(ref dtData);
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                string tieude = "", reportname = "";
                ReportDocument crpt = Utility.GetReport(Khoanoitrutonghop ? "noitru_tonghopchiphiravien_theokhoa" : "noitru_tonghopchiphiravien", ref tieude, ref reportname);
                if (crpt == null) return;
                frmPrintPreview objForm = new frmPrintPreview(baocaO_TIEUDE1.TIEUDE, crpt, true, dtData.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(dtData);
                objForm.crptViewer.ReportSource = crpt;
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = Khoanoitrutonghop ? "noitru_tonghopchiphiravien_theokhoa" : "noitru_tonghopchiphiravien";
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sTitleReport", baocaO_TIEUDE1.TIEUDE);
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(DateTime.Now));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());

                objForm.ShowDialog();

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }

        void cmdRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void frm_Xemtonghopchiphi_Load(object sender, EventArgs e)
        {
            baocaO_TIEUDE1.Init(Khoanoitrutonghop ? "noitru_tonghopchiphiravien_theokhoa" : "noitru_tonghopchiphiravien");
            LoadData();
            mv_blnHasloaded = true;
        }
        void LoadData()
        {
            try
            {
                m_dtChiPhiThanhtoan =
                   SPs.NoitruTonghopChiphiRavien(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, Utility.Bool2byte(!Khoanoitrutonghop), idkhoanoitru).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdThongTinChuaThanhToan, m_dtChiPhiThanhtoan, false, true, "trangthai_huy=0" + (PropertyLib._ThanhtoanProperties.Hienthidichvuchuathanhtoan ? " and trangthai_thanhtoan=0" : ""), "");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }
    }
}
