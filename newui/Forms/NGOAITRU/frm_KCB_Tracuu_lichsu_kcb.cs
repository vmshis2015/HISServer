using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.Tab;
using Microsoft.VisualBasic;
using NLog;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.UI.QMS;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.BENH_AN;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.NOITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.Cauhinh;

namespace VNS.HIS.UI.NGOAITRU
{
    /// <summary>
    /// 06/11/2013 3h57
    /// </summary>
    public partial class frm_KCB_Tracuu_lichsu_kcb : Form
    {
        KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        KCB_KEDONTHUOC _KCB_KEDONTHUOC = new KCB_KEDONTHUOC();
        KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath,
                                                         "SplitterDistanceThamKham.txt");

        private readonly MoneyByLetter MoneyByLetter = new MoneyByLetter();
        private  DataTable dt_ICD_PHU = new DataTable();
        private readonly Logger log;
        private readonly AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionBenhChinh = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionBenhPhu = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionKetLuan = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollection_MaLanKham = new AutoCompleteStringCollection();

        private readonly string strSaveandprintPath = Application.StartupPath +
                                                      @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";

        private bool AllowTextChanged;
        bool _buttonClick = false;
        private int Distance = 488;
        private int Exam_ID = -1;
        public KcbLuotkham objLuotkham=null;
        public KcbDanhsachBenhnhan objBenhnhan=null;
        KcbDangkyKcb objkcbdangky = null;
        private bool Selected;
        private string TEN_BENHPHU = "";
        private bool hasMorethanOne = true;
        private string _rowFilter = "1=1";
        private bool b_Hasloaded;
        private DataSet ds = new DataSet();
        private DataTable dt_ICD = new DataTable();
        private bool hasLoaded;
       
        private bool isLike = true;
        private DataTable m_dtAssignDetail;
        private DataTable m_dtDataVTYT = new DataTable();
        private DataTable m_dtDoctorAssign;
        private DataTable m_dtKhoaNoiTru = new DataTable();
        private DataTable m_dtPresDetail = new DataTable();
        private DataTable m_dtDonthuocChitiet_View = new DataTable();
        
        private DataTable m_dtReport = new DataTable();
        private DataTable m_hdt = new DataTable();
        private DataTable m_kl;
        private string m_strDefaultLazerPrinterName = "";

        /// <summary>
        /// hàm thực hiện việc chọn bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string malankham = "";

        private DataTable m_dtDanhsachbenhnhanthamkham = new DataTable();
        private frm_DanhSachKham objSoKham;
        private GridEXRow row_Selected;
        private bool trieuchung;


        public frm_KCB_Tracuu_lichsu_kcb()
        {
            InitializeComponent();
            KeyPreview = true;
           
            log = LogManager.GetCurrentClassLogger();
           
            
           
            dtInput_Date.Value =
                dtpCreatedDate.Value=dtNgayNhapVien.Value = globalVariables.SysDate;

            dtFromDate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;

          
            txtIdKhoaNoiTru.Visible = txtPatientDept_ID.Visible = globalVariables.IsAdmin;
            InitEvents();
        }
        void InitEvents()
        {
            FormClosing += frm_LAOKHOA_Add_TIEPDON_BN_FormClosing;
            Load+=new EventHandler(frm_KCB_Tracuu_lichsu_kcb_Load);
            KeyDown+=new KeyEventHandler(frm_KCB_Tracuu_lichsu_kcb_KeyDown);
            cmdSearch.Click+=new EventHandler(cmdSearch_Click);
            txtmaluotkham.KeyDown += new KeyEventHandler(txtPatient_Code_KeyDown);
            txtPatient_Code.TextChanged+=new EventHandler(txtPatient_Code_TextChanged);
            txtMaBenhChinh.TextChanged+=new EventHandler(txtMaBenhChinh_TextChanged);
            txtMaBenhphu.TextChanged+=new EventHandler(txtMaBenhphu_TextChanged);
            mnuDelDrug.Click+=new EventHandler(mnuDelDrug_Click);
            mnuDeleteCLS.Click+=new EventHandler(mnuDeleteCLS_Click);
            cmdAddMaBenhPhu.Click+=new EventHandler(cmdAddMaBenhPhu_Click);
            grd_ICD.ColumnButtonClick+=new ColumnActionEventHandler(grd_ICD_ColumnButtonClick);
            grdList.SelectionChanged+=new EventHandler(grdList_SelectionChanged);
            grdRegExam.SelectionChanged += new EventHandler(grdRegExam_SelectionChanged);
            grdLuotkham.SelectionChanged += new EventHandler(grdLuotkham_SelectionChanged);
        }

        void grdLuotkham_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdLuotkham))
                {
                    ClearControl();
                    return;
                }
                else
                {
                    string maluotkham = Utility.sDbnull(grdLuotkham.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                    DataTable dtData = _KCB_THAMKHAM.KcbLichsuKcbTimkiemphongkham(maluotkham);
                    Utility.SetDataSourceForDataGridEx_Basic(grdRegExam, dtData, true, true, "", KcbDangkyKcb.Columns.NgayDangky);
                    if (grdRegExam.GetDataRows().Length <= 1)
                        grbRegs.Height = 0;
                    else
                        grbRegs.Height = 100;
                    grdRegExam.MoveFirst();
                }
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void grdRegExam_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam))
                {
                    ClearControl();
                    return;
                }
                HienthithongtinBN();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       

        void cmdThamkhamConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._ThamKhamProperties);
            frm.ShowDialog();
           
        }
      
        void grdList_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

      

       

        void grdList_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

       

        void grdList_DoubleClick(object sender, EventArgs e)
        {
           
        }
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }



        private void frm_LAOKHOA_Add_TIEPDON_BN_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
     
       
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin của thăm khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchPatient();
        }

        /// <summary>
        /// Hàm thực hiện load lên Khoa nội trú
        /// </summary>
        private void InitData()
        {
            try
            {
                m_dtKhoaNoiTru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI",0);
            }
            catch (Exception)
            {
            }
        }
        void UpdateGroup()
        {
            try
            {
                if (!Utility.isValidGrid(grdLuotkham)) return;
                var counts =((DataView) grdLuotkham.DataSource).Table.AsEnumerable().GroupBy(x => x.Field<string>("ma_doituong_kcb"))
                    .Select(g => new { g.Key, Count = g.Count() });
                if (counts.Count() >= 2)
                {
                    if (grdLuotkham.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = "Nhóm đối tượng KCB: ";
                        grdLuotkham.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdLuotkham.RootTable.Columns["ma_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    grdLuotkham.RootTable.Groups.Clear();
                }
                grdLuotkham.UpdateData();
                grdLuotkham.Refresh();
            }
            catch
            {
            }
        }
        private void SearchPatient()
        {
            try
            {
                ClearControl();
                malankham = "";
                objkcbdangky = null;
                objBenhnhan = null;
                objLuotkham = null;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Clear();
                if (m_dtAssignDetail != null) m_dtAssignDetail.Clear();
                if (m_dtPresDetail != null) m_dtPresDetail.Clear();
                DateTime dt_FormDate = dtFromDate.Value;
                DateTime dt_ToDate = dtToDate.Value;
                m_dtDanhsachbenhnhanthamkham = _KCB_THAMKHAM.KcbLichsuKcbTimkiemBenhnhan(!chkByDate.Checked ? "01/01/1900" : dt_FormDate.ToString("dd/MM/yyyy"), 
                    !chkByDate.Checked ? "01/01/1900" : dt_ToDate.ToString("dd/MM/yyyy"),
                    Utility.DoTrim(txtmaluotkham.Text), 
                    Utility.Int32Dbnull(txtIdBenhnhan.Text,-1),
                    Utility.DoTrim(txtTenBN.Text),Utility.DoTrim(txtTheBHYT.Text),Utility.Int32Dbnull(txtBacsikham.MyID,-1));
                Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtDanhsachbenhnhanthamkham, true, true, "", KcbDanhsachBenhnhan.Columns.TenBenhnhan); 
                if (!dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.MaBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.MaBenh, typeof(string));
                }
                if (!dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.TenBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.TenBenh, typeof(string));
                }
                grd_ICD.DataSource = dt_ICD_PHU;

                grdList.MoveFirst();
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }    
        private void BindDoctorAssignInfo()
        {
            try
            {
                m_dtDoctorAssign = THU_VIEN_CHUNG.LaydanhsachBacsi(-1,-1);
                DataBinding.BindDataCombox(cboDoctorAssign, m_dtDoctorAssign, DmucNhanvien.Columns.IdNhanvien,
                                           DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ chỉ định---", true);
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    if (cboDoctorAssign.Items.Count > 0)
                        cboDoctorAssign.SelectedIndex = 0;
                }
                else
                {
                    if (cboDoctorAssign.Items.Count > 0)
                        cboDoctorAssign.SelectedIndex = Utility.GetSelectedIndex(cboDoctorAssign,
                                                                                 globalVariables.gv_intIDNhanvien.ToString());
                }
            }
            catch (Exception exception)
            {
                // throw;
            }
           
        }

       

        private void GetDataChiDinh()
        {
            ds =
                _KCB_THAMKHAM.LaythongtinCLSVaThuoc(Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                         Utility.sDbnull(malankham, ""),
                                                         Utility.Int32Dbnull(txtExam_ID.Text));
            m_dtAssignDetail = ds.Tables[0];
            m_dtPresDetail = ds.Tables[1];

            Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtAssignDetail, false, true, "",
                                               "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

            m_dtDonthuocChitiet_View = m_dtPresDetail.Clone();
            foreach (DataRow dr in m_dtPresDetail.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = m_dtDonthuocChitiet_View
                    .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                if (drview.Length <= 0)
                {
                    m_dtDonthuocChitiet_View.ImportRow(dr);
                }
                else
                {

                    drview[0][KcbDonthuocChitiet.Columns.SoLuong] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) + Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                    drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                    drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                    drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) + Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                    drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                    drview[0]["TT_BN_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong]) * Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                    drview[0][KcbDonthuocChitiet.Columns.SttIn] = Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0), Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                    m_dtDonthuocChitiet_View.AcceptChanges();
                }
            }

            Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDonthuocChitiet_View, false, true, "", KcbDonthuocChitiet.Columns.SttIn);
           
        }
       
        private void Get_DanhmucChung()
        {
           
        }

        private void frm_KCB_Tracuu_lichsu_kcb_Load(object sender, EventArgs e)
        {
            try
            {
                AllowTextChanged = false;
                Get_DanhmucChung();

             

                Load_DSach_ICD();
                LoadPhongkhamngoaitru();
                txtBacsikham.Init(THU_VIEN_CHUNG.LaydanhsachBacsi(-1, -1));
                BindDoctorAssignInfo();
                SearchPatient();
               
                AllowTextChanged = true;

                hasLoaded = true;

                InitData();
                ClearControl();
            }
            catch
            {
            }
            finally
            {
                txtPatient_Code.Focus();
                txtPatient_Code.Select();
                

            }
        }

        private void LoadTrangTraiIn()
        {
            try
            {
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy trạng thái in");
            }
        }

        private void Load_DSach_ICD()
        {
            try
            {
                dt_ICD = _KCB_THAMKHAM.LaydanhsachBenh();
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy danh sách ICD");
            }
        }

        private void LoadPhongkhamngoaitru()
        {
          
           
        }
        DataTable m_ExamTypeRelationList = new DataTable();
    
        private void cboDoctorAssign_TextChanged(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// hàm thực hiện việc mã bệnh chính
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaBenhChinh_TextChanged(object sender, EventArgs e)
        {
          


            try
            {
                hasMorethanOne = true;
                DataRow[] arrDr;
                if (isLike)
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh+ " like '" +
                                                               Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                               "%'");
                else
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh+ " = '" +
                                                               Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                               "'");
                if (!string.IsNullOrEmpty(txtMaBenhChinh.Text))
                {
                    if (arrDr.GetLength(0) == 1)
                    {
                        hasMorethanOne = false;
                        txtMaBenhChinh.Text = arrDr[0][DmucBenh.Columns.MaBenh].ToString();
                        txtTenBenhChinh.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.TenBenh], "");
                        //  txtDisease_ID.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.DiseaseId], "-1");
                    }
                    else
                    {
                        //txtDisease_ID.Text = "-1";
                        txtTenBenhChinh.Text = "";
                    }
                }
                else
                {
                    //  txtDisease_ID.Text = "-1";

                    txtTenBenhChinh.Text = "";
                    //cmdSearchBenhChinh.PerformClick();
                }
            }
            catch
            {
            }
            finally
            {
               
            }
        }

        /// <summary>
        /// hàm thực hiện việc mã bệnh phụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaBenhphu_TextChanged(object sender, EventArgs e)
        {
          
            hasMorethanOne = true;
            DataRow[] arrDr;
            if (isLike)
                arrDr =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh+ " like '" +
                                                           Utility.sDbnull(txtMaBenhphu.Text, "") +
                                                           "%'");
            else
                arrDr =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh+ " = '" +
                                                           Utility.sDbnull(txtMaBenhphu.Text, "") +
                                                           "'");
            if (!string.IsNullOrEmpty(txtMaBenhphu.Text))
            {
                if (arrDr.GetLength(0) == 1)
                {
                    hasMorethanOne = false;
                    txtMaBenhphu.Text = arrDr[0][DmucBenh.Columns.MaBenh].ToString();
                    txtTenBenhPhu.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.TenBenh], "");
                    TEN_BENHPHU = txtTenBenhPhu.Text;
                    //  txtDisease_ID.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.DiseaseId], "-1");
                }
                else
                {
                    //txtDisease_ID.Text = "-1";
                    txtTenBenhPhu.Text = "";
                    TEN_BENHPHU = "";
                }
            }
            else
            {
                //  txtDisease_ID.Text = "-1";

                txtMaBenhphu.Text = "";
                TEN_BENHPHU = "";
                //cmdSearchBenhChinh.PerformClick();
            }
        }

     

        private void ClearControl()
        {
            try
            {
                txtReg_ID.Text = "";
                txtPatientDept_ID.Clear();
                txtIdKhoaNoiTru.Clear();
                txtKhoaNoiTru.Clear();
              
                foreach (Control control in pnlThongtinBNKCB.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }
                
                foreach (Control control in pnlKetluan.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }

                foreach (Control control in pnlother.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                    
                }
                nmrSongayDT.Value = 0;
               
            }
            catch (Exception)
            {
            }
        }

        private void getResult()
        {
            try
            {
               
            }
            catch
            {
                uiGroupBox6.Width = 0;
            }
        }





        KcbChandoanKetluan _KcbChandoanKetluan = null;
        bool isRoom = false;
        private bool isNhapVien = false;
        /// <summary>
        /// Lấy về thông tin bệnh nhâ nội trú
        /// </summary>
        private void GetData()
        {
            try
            {
                // Utility.SetMsg(lblMsg, "", false);
                string PatientCode = Utility.sDbnull(grdLuotkham.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                malankham = PatientCode;
                int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                objLuotkham = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(PatientCode)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Patient_ID).ExecuteSingle<KcbLuotkham>();

                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);

                isRoom = false;
                if (objLuotkham != null)
                {
                    ClearControl();
                    txt_idchidinhphongkham.Text = Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));

                    objkcbdangky = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
                    DmucNhanvien objStaff = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                    string TenNhanvien = objLuotkham.NguoiKetthuc;
                    if (objStaff != null)
                        TenNhanvien = objStaff.TenNhanvien;
                    if (objkcbdangky != null)
                    {
                        DataTable m_dtThongTin = _KCB_THAMKHAM.LayThongtinBenhnhanKCB(objLuotkham.MaLuotkham,
                                                                          Utility.Int32Dbnull(objLuotkham.IdBenhnhan,
                                                                                              -1),
                                                                          Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
                        if (m_dtThongTin.Rows.Count > 0)
                        {
                            DataRow dr = m_dtThongTin.Rows[0];
                            if (dr != null)
                            {

                                dtInput_Date.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayTiepdon]);
                                txtExam_ID.Text = Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));

                                txtKhoaDieuTri.Text = Utility.sDbnull(grdLuotkham.GetValue("ten_khoanoitru"));
                                txtBuong.Text = Utility.sDbnull(grdLuotkham.GetValue("ten_buong"));
                                txtGiuong.Text = Utility.sDbnull(grdLuotkham.GetValue("ten_giuong"));

                                txtTrangthaiNgoaitru.Text = Utility.sDbnull(grdLuotkham.GetValue("trangthai_ngoaitru"))=="0"?"Đang khám":"Đã khám xong";
                                txtTrangthaiNoitru.Text = Utility.sDbnull(grdLuotkham.GetValue("ten_trangthai_noitru"));
                                
                                Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text, -1);
                                txtGioitinh.Text =
                                    Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.GioiTinh), "");
                                txt_idchidinhphongkham.Text = Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));
                                lblSOkham.Text = Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.SttKham));
                                txtPatient_Name.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                                txtPatient_ID.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                                txtPatient_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaLuotkham], "");
                                barcode.Data = malankham;
                                txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                                txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");

                                txtObjectType_Name.Text = Utility.sDbnull(dr[DmucDoituongkcb.Columns.TenDoituongKcb], "");
                                txtSoBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MatheBhyt], "");
                                txtBHTT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.PtramBhyt], "0");
                                txtNgheNghiep.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.NgheNghiep], "");
                                txtHanTheBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], "");
                                dtpNgayhethanBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], globalVariables.SysDate.ToString("dd/MM/yyyy"));
                                txtTuoi.Text = Utility.sDbnull(Utility.Int32Dbnull(globalVariables.SysDate.Year) -
                                                               objBenhnhan.NgaySinh.Value.Year);
                                //ThongBaoBenhAn(txtPatient_ID.Text);

                                if (objkcbdangky != null)
                                {
                                    txtReg_ID.Text = Utility.sDbnull(objkcbdangky.IdKham);
                                    dtpCreatedDate.Value = Convert.ToDateTime(objkcbdangky.NgayDangky);
                                    txtDepartment_ID.Text = Utility.sDbnull(objkcbdangky.IdPhongkham);
                                    DmucKhoaphong _department = new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).IsEqualTo(objkcbdangky.IdPhongkham).ExecuteSingle<DmucKhoaphong>();
                                    if (_department != null)
                                    {
                                        txtPhongkham.Text = Utility.sDbnull(_department.TenKhoaphong);
                                    }
                                    txtTenDvuKham.Text = Utility.sDbnull(objkcbdangky.TenDichvuKcb);
                                    txtNguoiTiepNhan.Text = Utility.sDbnull(objkcbdangky.NguoiTao);
                                    try
                                    {
                                        cboDoctorAssign.SelectedIndex =
                                                           Utility.GetSelectedIndex(cboDoctorAssign,
                                                                                    Utility.sDbnull(
                                                                                        objkcbdangky.IdBacsikham, -1));


                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                        .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objkcbdangky.IdKham).ExecuteSingle
                                        <KcbChandoanKetluan>();
                                if (_KcbChandoanKetluan != null)
                                {
                                    txtKet_Luan._Text = Utility.sDbnull(_KcbChandoanKetluan.Ketluan);
                                    txtHuongdieutri._Text = _KcbChandoanKetluan.HuongDieutri;
                                    nmrSongayDT.Value = Utility.DecimaltoDbnull(_KcbChandoanKetluan.SongayDieutri, 0);
                                    txtHa.Text = Utility.sDbnull(_KcbChandoanKetluan.Huyetap);
                                    txtMach.Text = Utility.sDbnull(_KcbChandoanKetluan.Mach);
                                    txtNhipTim.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhiptim);
                                    txtNhipTho.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhiptho);
                                    txtNhietDo.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhietdo);
                                    txtCannang.Text = Utility.sDbnull(_KcbChandoanKetluan.Cannang);
                                    txtChieucao.Text = Utility.sDbnull(_KcbChandoanKetluan.Chieucao);
                                    if (!string.IsNullOrEmpty(Utility.sDbnull(_KcbChandoanKetluan.Nhommau)) &&
                                        Utility.sDbnull(_KcbChandoanKetluan.Nhommau) != "-1")
                                        txtNhommau._Text = Utility.sDbnull(_KcbChandoanKetluan.Nhommau);


                                    AllowTextChanged = true;
                                    isLike = false;
                                    txtChanDoan._Text = Utility.sDbnull(_KcbChandoanKetluan.Chandoan);
                                    txtChanDoanKemTheo.Text = Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo);
                                    txtMaBenhChinh.Text = Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh);
                                    string dataString = Utility.sDbnull(_KcbChandoanKetluan.MabenhPhu, "");
                                    isLike = true;
                                    AllowTextChanged = false;
                                    dt_ICD_PHU.Clear();
                                    if (!string.IsNullOrEmpty(dataString))
                                    {
                                        string[] rows = dataString.Split(',');
                                        foreach (string row in rows)
                                        {
                                            if (!string.IsNullOrEmpty(row))
                                            {
                                                DataRow newDr = dt_ICD_PHU.NewRow();
                                                newDr[DmucBenh.Columns.MaBenh] = row;
                                                newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                                                dt_ICD_PHU.Rows.Add(newDr);
                                                dt_ICD_PHU.AcceptChanges();
                                            }
                                        }
                                        grd_ICD.DataSource = dt_ICD_PHU;
                                    }
                                }

                                GetDataChiDinh();
                            }
                        }

                    }
                    else
                    {
                        ClearControl();

                    }
                }

            }
            catch
            {
            }
            finally
            {
                KiemTraDaInPhoiBHYT();
                getResult();
            }
        }
        void LoadBenh()
        {
            try
            {
                    AllowTextChanged = true;
                    isLike = false;
                    txtChanDoan._Text = Utility.sDbnull(_KcbChandoanKetluan.Chandoan);
                    txtChanDoanKemTheo.Text = Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo);
                    txtMaBenhChinh.Text = Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh);
                    string dataString = Utility.sDbnull(_KcbChandoanKetluan.MabenhPhu, "");
                    isLike = true;
                    AllowTextChanged = false;
                    dt_ICD_PHU.Clear();
                    if (!string.IsNullOrEmpty(dataString))
                    {
                        string[] rows = dataString.Split(',');
                        foreach (string row in rows)
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                DataRow newDr = dt_ICD_PHU.NewRow();
                                newDr[DmucBenh.Columns.MaBenh] = row;
                                newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                                dt_ICD_PHU.Rows.Add(newDr);
                                dt_ICD_PHU.AcceptChanges();
                            }
                        }
                        grd_ICD.DataSource = dt_ICD_PHU;
                    }
                
            }
            catch
            {
            }
        }
        void ModifyByLockStatus(byte lockstatus)
        {
           
        }
        private void KiemTraDaInPhoiBHYT()
        {
            lblMessage.Visible = objLuotkham != null && objLuotkham.MaDoituongKcb == "BHYT";
            if (objLuotkham != null && objLuotkham.MaDoituongKcb == "BHYT")
            {
                SqlQuery sqlQuery = new Select().From(KcbPhieuDct.Schema)
                    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtPatient_Code.Text))
                    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text))
                    .And(KcbPhieuDct.Columns.LoaiThanhtoan).IsEqualTo(Utility.Int32Dbnull(KieuThanhToan.NgoaiTru));
                if (sqlQuery.GetRecordCount() > 0)
                {
                   
                    var objPhieuDct = sqlQuery.ExecuteSingle<KcbPhieuDct>();
                    if (objPhieuDct != null)
                    {
                        Utility.SetMsg(lblMessage,
                            string.Format("Đã in phôi bởi {0}, vào lúc: {1}", objPhieuDct.NguoiTao,
                                objPhieuDct.NgayTao), true);
                       
                        
                    }
                }
                else
                {
                    
                    lblMessage.Visible = false;
                }
            }//Đối tượng dịch vụ sẽ luôn hiển thị nút lưu
            
        }
        private string GetTenBenh(string MaBenh)
        {
            string TenBenh = "";
            DataRow[] arrMaBenh = globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh+ "='{0}'", MaBenh));
            if (arrMaBenh.GetLength(0) > 0) TenBenh = Utility.sDbnull(arrMaBenh[0][DmucBenh.Columns.TenBenh], "");
            return TenBenh;
        }


        void HienthithongtinBN()
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam))
                {
                    return;
                    ClearControl();
                }
                AllowTextChanged = false;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                GetData();
                ModifyByLockStatus(objLuotkham.Locked);
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg(exception.ToString());
                //throw;
            }
            finally
            {

                AllowTextChanged = true;
            }
        }
        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "cmdCHONBN")
                {
                    _buttonClick = true;
                    Janus.Windows.GridEX.GridEXColumn gridExColumn = grdList.RootTable.Columns[KcbDangkyKcb.Columns.SttKham];
                    grdList.Col = gridExColumn.Position;
                    HienthithongtinBN();
                }
            }
            catch (Exception exception)
            {
               
            }
            
        }

        /// <summary>
        /// hàm thực hiện viecj dóng form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        void Unlock()
        {
            try
            {
                if (objLuotkham == null)
                    return;
                //Kiểm tra nếu đã in phôi thì cần hủy in phôi
                KcbPhieuDct _item = new Select().From(KcbPhieuDct.Schema)
                    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbPhieuDct>();
                if (_item != null)
                {
                    Utility.ShowMsg("Bệnh nhân này thuộc đối tượng BHYT đã được in phôi. Bạn cần liên hệ bộ phận thanh toán hủy in phôi để mở khóa bệnh nhân");
                    return;
                }
                new Update(KcbLuotkham.Schema)
                                   .Set(KcbLuotkham.Columns.Locked).EqualTo(0)
                                   .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(string.Empty)
                                   .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(
                                       objLuotkham.MaLuotkham)
                                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                objLuotkham.Locked = 0;
                ModifyByLockStatus(objLuotkham.Locked);
             
                GetData();
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện việc dùng phím tắt in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_Tracuu_lichsu_kcb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if ((ActiveControl!=null && ActiveControl.Name == grdList.Name) || (this.tabPageChanDoan.ActiveControl != null && this.tabPageChanDoan.ActiveControl.Name == txtMaBenhphu.Name))
                    return;
                else
                    SendKeys.Send("{TAB}");
            if (e.Control && e.KeyCode == Keys.P)
            {
               
            }

            if (e.Control & e.KeyCode==Keys.F) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.Control && e.KeyCode == Keys.U)
                Unlock();
            if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
           
            if (e.KeyCode == Keys.F1)
            {
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                txtMach.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                tabDiagInfo.SelectedTab = tabPageChiDinhCLS;
               
            }
            if (e.KeyCode == Keys.F6)
            {
                txtPatient_Code.SelectAll();
                txtPatient_Code.Focus();
                return;
            }
            if (e.KeyCode == Keys.F3)
            {
               
            }
            if (e.KeyCode == Keys.F4)
            {
               
            }
          
            if (e.KeyCode == Keys.A && e.Control)
            {
             
            }
            if (e.Control && e.KeyCode == Keys.N)
            {
               
            }

        }

        private void txtSoKham_LostFocus(object sender, EventArgs e)
        {
            
        }

        private void txtSoKham_Click(object sender, EventArgs e)
        {
        }

     

        private void txtSoKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmdSearch.PerformClick();
        }
        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
           
        }
        private void label22_Click(object sender, EventArgs e)
        {
        }
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    ClearControl();
                    return;
                }
                else
                {
                    int idbenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                    DataTable dtData = _KCB_THAMKHAM.KcbLichsuKcbLuotkham(idbenhnhan);
                    Utility.SetDataSourceForDataGridEx_Basic(grdLuotkham, dtData, true, true, "", KcbLuotkham.Columns.NgayTiepdon);
                    UpdateGroup();
                    grdLuotkham.MoveFirst();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void txtPatient_Code_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter)
                {
                    string _patient_Code = Utility.AutoFullPatientCode(txtmaluotkham.Text);
                    ClearControl();
                    txtmaluotkham.Text = _patient_Code;
                    dtPatient = _KCB_THAMKHAM.TimkiemBenhnhan(txtmaluotkham.Text,
                                                   -1,(byte)2, 0);

                    string PatientCodeFilter = globalVariables.SysDate.Year.ToString().Substring(2, 2) +
                                               txtPatient_Code.Text.PadLeft(6, '0');
                    DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + PatientCodeFilter + "'");
                    if (arrPatients.GetLength(0) <= 0)
                    {
                        if (dtPatient.Rows.Count > 1)
                        {
                            var frm = new frm_DSACH_BN_TKIEM();
                            frm.MaLuotkham = txtmaluotkham.Text;
                            frm.dtPatient = dtPatient;
                            frm.ShowDialog();
                            if (!frm.has_Cancel)
                            {
                                txtmaluotkham.Text = frm.MaLuotkham;
                            }
                        }
                    }
                    else
                    {
                        txtmaluotkham.Text = PatientCodeFilter;
                    }
                    DataTable dt_Patient = _KCB_THAMKHAM.TimkiemThongtinBenhnhansaukhigoMaBN(txtmaluotkham.Text,
                                                              -1,globalVariables.MA_KHOA_THIEN);
                    grdList.DataSource = null;
                    grdList.DataSource = dt_Patient;
                    if (dt_Patient.Rows.Count > 0)
                    {
                        grdList.MoveToRowIndex(0);
                        grdList.CurrentRow.BeginEdit();
                        grdList.CurrentRow.Cells["MAUSAC"].Value = 1;
                        grdList.CurrentRow.EndEdit();
                        AllowTextChanged = false;
                        if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                        GetData();
                        txtPatient_Code.SelectAll();
                    }
                    else
                    {
                        string sPatientTemp = txtPatient_Code.Text;
                        ClearControl();

                        txtmaluotkham.Text = sPatientTemp;
                        txtmaluotkham.SelectAll();
                    }
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
            }
            finally
            {
                AllowTextChanged = true;
            }
        }

        private bool InValiMaBenh(string mabenh)
        {
            EnumerableRowCollection<DataRow> query = from benh in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                                     where
                                                         Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                         Utility.sDbnull(mabenh)
                                                     select benh;
            if (query.Any()) return true;
            else return false;
        }

        private void txtTenBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void DSACH_ICD(EditBox tEditBox, string LOAITIMKIEM, int CP)
        {
            try
            {
                Selected = false;
                string sFillter = "";
                if (LOAITIMKIEM.ToUpper() == DmucChung.Columns.Ten)
                {
                    sFillter = " Disease_Name like '%" + tEditBox.Text + "%' OR FirstChar LIKE '%" + tEditBox.Text +
                               "%'";
                }
                else if (LOAITIMKIEM == DmucChung.Columns.Ma)
                {
                    sFillter = DmucBenh.Columns.MaBenh+ " LIKE '%" + tEditBox.Text + "%'";
                }
                DataRow[] dataRows;
                dataRows = dt_ICD.Select(sFillter);
                if (dataRows.Length == 1)
                {
                    if (CP == 0)
                    {
                        txtMaBenhChinh.Text = "";
                        txtMaBenhChinh.Text = Utility.sDbnull(dataRows[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                        txtMaBenhChinh.Focus();
                    }
                    else if (CP == 1)
                    {
                        txtMaBenhphu.Text = Utility.sDbnull(dataRows[0][DmucBenh.Columns.MaBenh], "");
                        hasMorethanOne = false;
                        txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                        txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                        Selected = false;
                    }
                }
                else if (dataRows.Length > 1)
                {
                    var frmDanhSachIcd = new frm_DanhSach_ICD(CP);
                    frmDanhSachIcd.dt_ICD = dataRows.CopyToDataTable();
                    frmDanhSachIcd.ShowDialog();
                    if (!frmDanhSachIcd.has_Cancel)
                    {
                        List<GridEXRow> lstSelectedRows = frmDanhSachIcd.lstSelectedRows;
                        if (CP == 0)
                        {
                            isLike = false;
                            txtMaBenhChinh.Text = "";
                            txtMaBenhChinh.Text =
                                Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                            hasMorethanOne = false;
                            txtMaBenhChinh_TextChanged(txtMaBenhChinh, new EventArgs());
                            txtMaBenhChinh_KeyDown(txtMaBenhChinh, new KeyEventArgs(Keys.Enter));
                            Selected = false;
                        }
                        else if (CP == 1)
                        {
                            if (lstSelectedRows.Count == 1)
                            {
                                isLike = false;
                                txtMaBenhphu.Text = "";
                                txtMaBenhphu.Text =
                                    Utility.sDbnull(lstSelectedRows[0].Cells[DmucBenh.Columns.MaBenh].Value, "");
                                hasMorethanOne = false;
                                txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                                txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                Selected = false;
                            }
                            else
                            {
                                foreach (GridEXRow row in lstSelectedRows)
                                {
                                    isLike = false;
                                    txtMaBenhphu.Text = "";
                                    txtMaBenhphu.Text =
                                        Utility.sDbnull(row.Cells[DmucBenh.Columns.MaBenh].Value, "");
                                    hasMorethanOne = false;
                                    txtMaBenhphu_TextChanged(txtMaBenhphu, new EventArgs());
                                    txtMaBenhphu_KeyDown(txtMaBenhphu, new KeyEventArgs(Keys.Enter));
                                    Selected = false;
                                }
                                hasMorethanOne = true;
                            }
                        }
                        tEditBox.Focus();
                    }
                    else
                    {
                        hasMorethanOne = true;
                        tEditBox.Focus();
                    }
                }
                else
                {
                    hasMorethanOne = true;
                    tEditBox.SelectAll();
                }
            }
            catch
            {
            }
            finally
            {
                isLike = true;
            }
        }

        private void txtMaBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void cmdAddMaBenhPhu_Click(object sender, EventArgs e)
        {
            
        }

       

        private void txtTenBenhPhu_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void grd_ICD_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
           
        }

       

        private void txtTenBenhPhu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTenBenhPhu.TextLength <= 0)
                {
                    Selected = false;
                    txtMaBenhphu.ResetText();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý sự kiện");
            }
        }

        private void txtTenBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMaBenhChinh.TextLength <= 0)
                {
                    txtMaBenhChinh.ResetText();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xử lý sự kiện");
            }
        }

      

        private void tabDiagInfo_SelectedTabChanged(object sender, TabEventArgs e)
        {
        }

       

       

        private void txtMaBenhphu_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

      

        private void radChuaKham_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chkChonPhieu_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cmdCreateNewPresTuTuc_Click(object sender, EventArgs e)
        {
        }

        private void radDaKham_CheckedChanged(object sender, EventArgs e)
        {
            cmdSearch_Click(cmdSearch, e);
        }

        private void mnuDelDrug_Click(object sender, EventArgs e)
        {
           
        }

        private void mnuDeleteCLS_Click(object sender, EventArgs e)
        {
          
        }

      

       


        private void Try2CreateFolder()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(strSaveandprintPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strSaveandprintPath));
            }
            catch
            {
            }
        }

     

        private void cmdBenhAnNgoaiTru_Click(object sender, EventArgs e)
        {
           
        }

       

       

        private void cmdHistory_Click_1(object sender, EventArgs e)
        {
        }

        private void GetChanDoanChinhPhu(string ICD_chinh, string IDC_Phu, ref string ICD_chinh_Name,
                                         ref string ICD_chinh_Code, ref string ICD_Phu_Name, ref string ICD_Phu_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_chinh_Name += _item.TenBenh + ";";
                    ICD_chinh_Code += _item.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Phu_Name += _item.TenBenh + ";";
                    ICD_Phu_Code += _item.MaBenh + ";";
                }
            }
            catch
            {
            }
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties( PropertyLib._HISQMSProperties);
            frm.ShowDialog();
            
        }

        private void txtNhietDo_Click(object sender, EventArgs e)
        {
        }

       

       

     

       

        private void TimKiemKhoaNoiTru()
        {
            
        }

        private void txtDeparmentID_TextChanged(object sender, EventArgs e)
        {
            EnumerableRowCollection<string> query = from khoa in m_dtKhoaNoiTru.AsEnumerable()
                                                    let y = Utility.sDbnull(khoa[DmucKhoaphong.Columns.TenKhoaphong])
                                                    where
                                                        Utility.Int32Dbnull(khoa[DmucKhoaphong.Columns.IdKhoaphong]) ==
                                                        Utility.Int32Dbnull(txtIdKhoaNoiTru.Text)
                                                    select y;
            if (query.Any())
            {
                txtKhoaNoiTru.Text = Utility.sDbnull(query.FirstOrDefault());
            }
            else
            {
                txtKhoaNoiTru.Text = string.Empty;
            }
        }

      



    }
}