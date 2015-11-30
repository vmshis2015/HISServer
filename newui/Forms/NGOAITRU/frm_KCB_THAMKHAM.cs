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
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.Tab;
using NLog;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VNS.HIS.DAL;
using VNS.HIS.UCs;
using VNS.HIS.UI.BENH_AN;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.Forms.CanLamSang;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.NOITRU;
using VNS.Libs;
using VNS.Properties;
using VNS.UCs;
using VNS.UI.QMS;

namespace VNS.HIS.UI.NGOAITRU
{
    /// <summary>
    /// 06/11/2013 3h57
    /// </summary>
    public partial class frm_KCB_THAMKHAM : Form
    {
        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath,
                                                         "SplitterDistanceThamKham.txt");

        private readonly MoneyByLetter MoneyByLetter = new MoneyByLetter();
        private readonly KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        private readonly KCB_KEDONTHUOC _KCB_KEDONTHUOC = new KCB_KEDONTHUOC();
        private readonly KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        private readonly Logger log;

        private readonly List<string> lstKQCochitietColumns = new List<string>
                                                                  {"ten_chitietdichvu", "Ket_qua", "bt_nam", "bt_nu"};

        private readonly List<string> lstKQKhongchitietColumns = new List<string> {"Ket_qua", "bt_nam", "bt_nu"};

        private readonly List<string> lstResultColumns = new List<string>
                                                             {
                                                                 "ten_chitietdichvu",
                                                                 "ketqua_cls",
                                                                 "binhthuong_nam",
                                                                 "binhthuong_nu"
                                                             };

        private readonly AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionBenhChinh = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionBenhPhu = new AutoCompleteStringCollection();
        private readonly AutoCompleteStringCollection namesCollectionKetLuan = new AutoCompleteStringCollection();

        private readonly AutoCompleteStringCollection namesCollection_m_strMaLuotkham =
            new AutoCompleteStringCollection();

        private readonly string strSaveandprintPath = Application.StartupPath +
                                                      @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";

        private int Distance = 488;
        private string TEN_BENHPHU = "";
        private DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
        private KcbChandoanKetluan _KcbChandoanKetluan;
        private bool _buttonClick;
        private DataSet ds = new DataSet();
        private DataTable dt_ICD = new DataTable();
        private DataTable dt_ICD_PHU = new DataTable();
        private bool hasLoaded;
        private bool hasMorethanOne = true;

        private bool isLike = true;
        private bool isNhapVien = false;
        private bool isRoom;
        private List<string> lstVisibleColumns = new List<string>();
        private DataTable m_dtAssignDetail;
        private DataTable m_dtDanhsachbenhnhanthamkham = new DataTable();
        private DataTable m_dtDoctorAssign;
        private DataTable m_dtDonthuocChitiet_View = new DataTable();
        private DataTable m_dtKhoaNoiTru = new DataTable();
        private DataTable m_dtPresDetail = new DataTable();

        private DataTable m_dtReport = new DataTable();
        private DataTable m_dtVTTH = new DataTable();
        private DataTable m_dtVTTHChitiet_View = new DataTable();
        private DataTable m_hdt = new DataTable();
        private DataTable m_kl;
        private string m_strDefaultLazerPrinterName = "";

        /// <summary>
        /// hàm thực hiện việc chọn bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string m_strMaLuotkham = "";

        public KcbDanhsachBenhnhan objBenhnhan = null;
        public KcbLuotkham objLuotkham = null;

        private frm_DanhSachKham objSoKham;
        private KcbDangkyKcb objkcbdangky;
        private GridEXRow row_Selected;
        private bool trieuchung;

        string Args = "ALL";
        public frm_KCB_THAMKHAM(string sthamso)
        {
            InitializeComponent();
            KeyPreview = true;
            Args = sthamso;
            log = LogManager.GetCurrentClassLogger();


            dtInput_Date.Value =
                dtpCreatedDate.Value = globalVariables.SysDate;

            dtFromDate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;

            LoadLaserPrinters();
            CauHinhQMS();
            CauHinhThamKham();
            // txtIdKhoaNoiTru.Visible = globalVariables.IsAdmin;
            InitEvents();
        }

        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }

        private void InitEvents()
        {
            FormClosing += frm_LAOKHOA_Add_TIEPDON_BN_FormClosing;
            Load += frm_KCB_THAMKHAM_Load;
            KeyDown += frm_KCB_THAMKHAM_KeyDown;

            txtSoKham.LostFocus += txtSoKham_LostFocus;
            txtSoKham.Click += txtSoKham_Click;
            txtSoKham.KeyDown += txtSoKham_KeyDown;

            cmdSearch.Click += cmdSearch_Click;
            radChuaKham.CheckedChanged += radChuaKham_CheckedChanged;
            radDaKham.CheckedChanged += radDaKham_CheckedChanged;

            txtPatient_Code.KeyDown += txtPatient_Code_KeyDown;
            txtPatient_Code.TextChanged += txtPatient_Code_TextChanged;

            grdList.ColumnButtonClick += grdList_ColumnButtonClick;
            grdList.KeyDown += grdList_KeyDown;
            grdList.SelectionChanged += grdList_SelectionChanged;
            grdList.DoubleClick += grdList_DoubleClick;
            grdList.MouseClick += grdList_MouseClick;

            grdPresDetail.UpdatingCell += grdPresDetail_UpdatingCell;

            txtMaBenhChinh.GotFocus += txtMaBenhChinh_GotFocus;
            txtMaBenhChinh.KeyDown += txtMaBenhChinh_KeyDown;
            txtMaBenhChinh.TextChanged += txtMaBenhChinh_TextChanged;
            txtMach.LostFocus += txtMach_LostFocus;
            txtNhipTim.LostFocus += txtNhipTim_LostFocus;
            txtMaBenhphu.GotFocus += txtMaBenhphu_GotFocus;
            txtMaBenhphu.KeyDown += txtMaBenhphu_KeyDown;
            txtMaBenhphu.TextChanged += txtMaBenhphu_TextChanged;

            cmdSave.Click += cmdSave_Click;
            cmdInTTDieuTri.Click += cmdInTTDieuTri_Click;
            cmdNhapVien.Click += cmdNhapVien_Click;
            cmdHuyNhapVien.Click += cmdHuyNhapVien_Click;
            cmdCauHinh.Click += cmdCauHinh_Click;


            grdAssignDetail.CellUpdated += grdAssignDetail_CellUpdated;
            grdAssignDetail.SelectionChanged += grdAssignDetail_SelectionChanged;
            grdAssignDetail.MouseDoubleClick += grdAssignDetail_MouseDoubleClick;
            cmdInsertAssign.Click += cmdInsertAssign_Click;
            cmdUpdate.Click += cmdUpdate_Click;
            cmdDelteAssign.Click += cmdDelteAssign_Click;
            cboLaserPrinters.SelectedIndexChanged += cboLaserPrinters_SelectedIndexChanged;
            cmdPrintAssign.Click += cmdPrintAssign_Click;

            cboA4Donthuoc.SelectedIndexChanged += cboA4_SelectedIndexChanged;
            cboPrintPreviewDonthuoc.SelectedIndexChanged += cboPrintPreview_SelectedIndexChanged;
            cmdCreateNewPres.Click += cmdCreateNewPres_Click;
            cmdUpdatePres.Click += cmdUpdatePres_Click;
            cmdDeletePres.Click += cmdDeletePres_Click;
            cmdPrintPres.Click += cmdPrintPres_Click;

            mnuDelDrug.Click += mnuDelDrug_Click;
            mnuDelVTTH.Click += mnuDelVTTH_Click;
            mnuDeleteCLS.Click += mnuDeleteCLS_Click;

            cmdSearchBenhChinh.Click += cmdSearchBenhChinh_Click;
            cmdSearchBenhPhu.Click += cmdSearchBenhPhu_Click;
            cmdAddMaBenhPhu.Click += cmdAddMaBenhPhu_Click;

            cmdThamkhamConfig.Click += cmdThamkhamConfig_Click;
            chkAutocollapse.CheckedChanged += chkAutocollapse_CheckedChanged;

            grd_ICD.ColumnButtonClick += grd_ICD_ColumnButtonClick;
            chkHienthichitiet.CheckedChanged += chkHienthichitiet_CheckedChanged;

            cboChonBenhAn.SelectedIndexChanged += cboChonBenhAn_SelectedIndexChanged;

            cboA4Cls.SelectedIndexChanged += cboA4Cls_SelectedIndexChanged;
            cboPrintPreviewCLS.SelectedIndexChanged += cboPrintPreviewCLS_SelectedIndexChanged;

            cboA4Tomtatdieutringoaitru.SelectedIndexChanged += cboA4Tomtatdieutringoaitru_SelectedIndexChanged;
            cboPrintPreviewTomtatdieutringoaitru.SelectedIndexChanged +=
                cboPrintPreviewTomtatdieutringoaitru_SelectedIndexChanged;

            cmdUnlock.Click += cmdUnlock_Click;
            cmdChuyenPhong.Click += cmdChuyenPhong_Click;

            txtChanDoan._OnShowData += txtChanDoan__OnShowData;
            txtKet_Luan._OnShowData += txtKet_Luan__OnShowData;
            txtHuongdieutri._OnShowData += txtHuongdieutri__OnShowData;
            txtNhommau._OnShowData += txtNhommau__OnShowData;
            txtNhanxet._OnShowData += txtNhanxet__OnShowData;

            txtNhommau._OnSaveAs += txtNhommau__OnSaveAs;
            txtKet_Luan._OnSaveAs += txtKet_Luan__OnSaveAs;
            txtHuongdieutri._OnSaveAs += txtHuongdieutri__OnSaveAs;
            txtChanDoan._OnSaveAs += txtChanDoan__OnSaveAs;
            txtNhanxet._OnSaveAs += txtNhanxet__OnSaveAs;

            cmdThemphieuVT.Click += cmdThemphieuVT_Click;
            cmdSuaphieuVT.Click += cmdSuaphieuVT_Click;
            cmdXoaphieuVT.Click += cmdXoaphieuVT_Click;
            cmdInphieuVT.Click += cmdInphieuVT_Click;
            mnuShowResult.Click += mnuShowResult_Click;
            lnkSize.Click += lnkSize_Click;
            cmdLuuChandoan.Click += cmdLuuChandoan_Click;
            txtMauKQ._OnEnterMe += txtMauKQ__OnEnterMe;
        }

        void txtMauKQ__OnEnterMe()
        {
            try
            {
                if (!Utility.isValidGrid(grdAssignDetail)) return;
                int IdChitietdichvu =
                       Utility.Int32Dbnull(
                           Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);

                int IdChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                FillDynamicValues(IdChitietdichvu, IdChitietchidinh);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        private void grdAssignDetail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowResult();
        }

        private void cmdLuuChandoan_Click(object sender, EventArgs e)
        {
          ActionResult act=  _KCB_THAMKHAM.LuuHoibenhvaChandoan(TaoDulieuChandoanKetluan(), null, false);
          if (act == ActionResult.Success)
          {
              _KcbChandoanKetluan.IsNew = false;
              _KcbChandoanKetluan.MarkOld();
          }
        }

        private void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if (!Utility.isValidGrid(grdPresDetail)) return;
            if (e.Column.Key == "stt_in")
            {
                long IdChitietdonthuoc =
                    Utility.Int64Dbnull(
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, 0);
                if (IdChitietdonthuoc > -1)
                    new KCB_KEDONTHUOC().Capnhatchidanchitiet(IdChitietdonthuoc, KcbDonthuocChitiet.Columns.SttIn,
                                                              e.Value.ToString());
                grdPresDetail.UpdateData();
            }
            if (e.Column.Key == "mota_them_chitiet")
            {
                long IdChitietdonthuoc =
                    Utility.Int64Dbnull(
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, 0);
                if (IdChitietdonthuoc > -1)
                    new KCB_KEDONTHUOC().Capnhatchidanchitiet(IdChitietdonthuoc, KcbDonthuocChitiet.Columns.MotaThem,
                                                              e.Value.ToString());
                grdPresDetail.UpdateData();
            }
        }

        private void lnkSize_Click(object sender, EventArgs e)
        {
            var _Properties = new frm_Properties(PropertyLib._DynamicInputProperties);
            if (_Properties.ShowDialog() == DialogResult.OK)
            {
                ShowResult();
            }
        }

        private void mnuShowResult_Click(object sender, EventArgs e)
        {
            mnuShowResult.Tag = mnuShowResult.Checked ? "1" : "0";
            if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
            {
                Utility.ShowColumns(grdAssignDetail, mnuShowResult.Checked ? lstResultColumns : lstVisibleColumns);
            }
            else
                grdAssignDetail_SelectionChanged(grdAssignDetail, e);
        }

        private void txtChanDoan__OnSaveAs()
        {
            if (Utility.DoTrim(txtChanDoan.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtChanDoan.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtChanDoan.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChanDoan.myCode;
                txtChanDoan.Init();
                txtChanDoan.SetCode(oldCode);
                txtChanDoan.Focus();
            }
        }

        private void txtNhanxet__OnSaveAs()
        {
            if (Utility.DoTrim(txtNhanxet.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhanxet.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtNhanxet.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNhanxet.myCode;
                txtNhanxet.Init();
                txtNhanxet.SetCode(oldCode);
                txtNhanxet.Focus();
            }
        }

        private void txtHuongdieutri__OnSaveAs()
        {
            if (Utility.DoTrim(txtHuongdieutri.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtHuongdieutri.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtHuongdieutri.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtHuongdieutri.myCode;
                txtHuongdieutri.Init();
                txtHuongdieutri.SetCode(oldCode);
                txtHuongdieutri.Focus();
            }
        }

        private void txtKet_Luan__OnSaveAs()
        {
            if (Utility.DoTrim(txtKet_Luan.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtKet_Luan.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtKet_Luan.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKet_Luan.myCode;
                txtKet_Luan.Init();
                txtKet_Luan.SetCode(oldCode);
                txtKet_Luan.Focus();
            }
        }

        private void txtNhommau__OnSaveAs()
        {
            if (Utility.DoTrim(txtNhommau.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhommau.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtNhommau.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNhommau.myCode;
                txtNhommau.Init();
                txtNhommau.SetCode(oldCode);
                txtNhommau.Focus();
            }
        }

        private void txtNhanxet__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhanxet.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNhanxet.myCode;
                txtNhanxet.Init();
                txtNhanxet.SetCode(oldCode);
                txtNhanxet.Focus();
            }
        }

        private void txtNhommau__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhommau.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNhommau.myCode;
                txtNhommau.Init();
                txtNhommau.SetCode(oldCode);
                txtNhommau.Focus();
            }
        }

        private void txtHuongdieutri__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtHuongdieutri.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtHuongdieutri.myCode;
                txtHuongdieutri.Init();
                txtHuongdieutri.SetCode(oldCode);
                txtHuongdieutri.Focus();
            }
        }

        private void txtKet_Luan__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtKet_Luan.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKet_Luan.myCode;
                txtKet_Luan.Init();
                txtKet_Luan.SetCode(oldCode);
                txtKet_Luan.Focus();
            }
        }

        private void txtChanDoan__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtChanDoan.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChanDoan.myCode;
                txtChanDoan.Init();
                txtChanDoan.SetCode(oldCode);
                txtChanDoan.Focus();
            }
        }

        private void cmdChuyenPhong_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (objkcbdangky == null)
                {
                    Utility.ShowMsg("Bạn cần chọn bệnh nhân trước khi thực hiện chuyển phòng khám", "");
                    return;
                }
                //Kiểm tra nếu BN chưa kết thúc hoặc bác sĩ chưa thăm khám mới được phép chuyển phòng
                //SqlQuery sqlQuery = new Select().From(KcbDonthuoc.Schema)
                //    .Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objkcbdangky.IdKham);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Bác sĩ đã kê đơn thuốc cho lần khám này nên không thể chuyển phòng. Đề nghị hủy đơn thuốc trước khi chuyển phòng khám", "");
                //    return;
                //}
                //sqlQuery = new Select().From(KcbChidinhcl.Schema)
                //    .Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(objkcbdangky.IdKham);
                //if (sqlQuery.GetRecordCount() > 0)
                //{
                //    Utility.ShowMsg("Bác sĩ đã chỉ định cận lâm sàng cho lần khám này nên không thể chuyển phòng. Đề nghị hủy chỉ định cận lâm sàng trước khi chuyển phòng khám", "");
                //    return;
                //}
                var _ChuyenPhongkham = new frm_ChuyenPhongkham();
                _ChuyenPhongkham.objDangkyKcb_Cu = objkcbdangky;
                _ChuyenPhongkham.MA_DTUONG = objkcbdangky.MaDoituongkcb;
                _ChuyenPhongkham.dongia = objkcbdangky.DonGia;
                _ChuyenPhongkham.txtPhonghientai.Text = txtPhongkham.Text;

                _ChuyenPhongkham.ShowDialog();
                if (!_ChuyenPhongkham.m_blnCancel)
                {
                    Utility.SetMsg(lblMsg, "Chuyển phòng khám thành công. Mời bạn chọn bệnh nhân khác để thăm khám",
                                   false);
                    DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + objkcbdangky.IdKham.ToString());
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["ten_phongkham"] = _ChuyenPhongkham.txtPhongkham.Text;
                        arrDr[0]["id_phongkham"] = _ChuyenPhongkham._DmucDichvukcb.IdPhongkham;
                        m_dtDanhsachbenhnhanthamkham.AcceptChanges();
                    }
                    string filter = "";
                    filter = "id_phongkham=" + Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1).ToString();
                    if (Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1) > -1)
                    {
                        m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
                        m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = filter;
                    }
                    else //BN vẫn hiển thị như vậy-->Cần load lại dữ liệu
                    {
                        GetData();
                    }
                    txtPatient_Code.Focus();
                    txtPatient_Code.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void UpdateDatatable()
        {
        }

        private void cmdUnlock_Click(object sender, EventArgs e)
        {
            Unlock();
        }

        private void cboChonBenhAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmdInTTDieuTri.Visible)
            {
                return;
            }
            if (cboChonBenhAn.SelectedIndex == 6)
            {
                BenhAnThuong();
                //   BenhAnDaithaoduong();
            }
            if (cboChonBenhAn.SelectedIndex == 7)
            {
                BenhAnThuong();
               // BenhAnTG();
            }
            if (cboChonBenhAn.SelectedIndex == 1 || 
                cboChonBenhAn.SelectedIndex == 2 ||
                cboChonBenhAn.SelectedIndex == 3 ||
                cboChonBenhAn.SelectedIndex == 4 ||
                cboChonBenhAn.SelectedIndex == 5 )
            {
                BenhAnThuong();
            }
        }

        private void BenhAnThuong()
        {
            try
            {
                var frm = new Frm_BenhAnThuong();
                switch (cboChonBenhAn.SelectedIndex)
                {
                    case 1: frm.loaibenhan = Utility.GetLoaiBenhAn(LoaiBenhAn.BA_DaiThaoDuong);
                    break;
                    case 2: frm.loaibenhan = Utility.GetLoaiBenhAn(LoaiBenhAn.BA_Basedow);
                    break;
                    case 3: frm.loaibenhan = Utility.GetLoaiBenhAn(LoaiBenhAn.BA_TangHuyetAp);
                    break;
                    case 4: frm.loaibenhan = Utility.GetLoaiBenhAn(LoaiBenhAn.BA_ViemGanB);
                    break;
                    case 5: frm.loaibenhan = Utility.GetLoaiBenhAn(LoaiBenhAn.BA_Cop);
                    break;
                    case 6: frm.loaibenhan = Utility.GetLoaiBenhAn(LoaiBenhAn.BA_TaiMuiHong);
                    break;
                    case 7: frm.loaibenhan = Utility.GetLoaiBenhAn(LoaiBenhAn.BA_RangHamMat);
                    break;
                }
                frm.txtMaBN.Text = txtPatient_ID.Text;
                frm.txtMaLanKham.Text = m_strMaLuotkham;
                frm.txtHuyetApTu.Text = Utility.sDbnull(txtHa.Text);
                frm.txtNhietDo.Text = Utility.sDbnull(txtNhietDo.Text);
                frm.txtNhipTho.Text = Utility.sDbnull(txtNhipTho.Text);
                frm.txtMach.Text = Utility.sDbnull(txtNhipTim.Text);
                string ICD_Name = "";
                string ICD_Code = "";
                string ICD_chinh_Name = "";
                string ICD_chinh_Code = "";
                string ICD_Phu_Name = "";
                string ICD_Phu_Code = "";
                DataSet dsData =
                    new Select("*").From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(
                        Utility.Int32Dbnull(txtExam_ID.Text.Trim())).ExecuteDataSet();
                DataTable v_dtData = dsData.Tables[0];
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                {
                    GetChanDoan(Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhChinh], ""),
                                Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhPhu], ""),
                                ref ICD_Name, ref ICD_Code);
                }
                frm.txtkbChanDoanRaVien.Text = ICD_Name;
                frm.txtkbMa.Text = ICD_Code;

                if (v_dtData != null && v_dtData.Rows.Count > 0)
                {
                    GetChanDoanChinhPhu(Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhChinh], ""),
                                        Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhPhu], ""),
                                        ref ICD_chinh_Name,
                                        ref ICD_chinh_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                }
                SqlQuery SoKham =
                    new Select().From(KcbBenhAn.Schema).
                        Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text));
                //Đã Tồn tại thông tin bệnh án ngoại trú. Sửa
                if (SoKham.GetRecordCount() > 0)
                {
                    //Execute()
                    frm.em_Action = action.Update;
                }
                    //chưa tồn tại thông tin ngoại tru. Them
                else
                {
                    frm.em_Action = action.Insert;
                }
                frm.ShowDialog();
                cboChonBenhAn.SelectedIndex = 0;
                ThongBaoBenhAn(txtPatient_ID.Text);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void BenhAnTG()
        {
            try
            {
                var frm = new frm_BENHAN_NGOAITRU_TG();
                frm.txtMaBN.Text = txtPatient_ID.Text;
                frm.txtMaLanKham.Text = m_strMaLuotkham;

                frm.txtHuyetApTu.Text = Utility.sDbnull(txtHa.Text);
                frm.txtNhietDo.Text = Utility.sDbnull(txtNhietDo.Text);
                frm.txtNhipTho.Text = Utility.sDbnull(txtNhipTho.Text);
                frm.txtMach.Text = Utility.sDbnull(txtNhipTim.Text);
                string ICD_Name = "";
                string ICD_Code = "";
                string ICD_chinh_Name = "";
                string ICD_chinh_Code = "";
                string ICD_Phu_Name = "";
                string ICD_Phu_Code = "";
                DataSet dsData =
                    new Select("*").From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(
                        Utility.Int32Dbnull(txtExam_ID.Text.Trim())).ExecuteDataSet();
                DataTable v_dtData = dsData.Tables[0];
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                {
                    GetChanDoan(Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhChinh], ""),
                                Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhPhu], ""),
                                ref ICD_Name, ref ICD_Code);
                }
                frm.txtkbChanDoanRaVien.Text = ICD_Name;
                frm.txtkbMa.Text = ICD_Code;

                if (v_dtData != null && v_dtData.Rows.Count > 0)
                {
                    GetChanDoanChinhPhu(Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhChinh], ""),
                                        Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhPhu], ""),
                                        ref ICD_chinh_Name,
                                        ref ICD_chinh_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                }

                //frm.txtBenhChinh.Text = ICD_chinh_Name + ICD_chinh_Code;
                // frm.txtBenhPhu.Text = ICD_Phu_Name + ICD_Phu_Code;
                SqlQuery SoKham =
                    new Select().From(KcbBenhAn.Schema).
                        Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text));
                //Đã Tồn tại thông tin bệnh án ngoại trú. Sửa
                if (SoKham.GetRecordCount() > 0)
                {
                    //Execute()
                    frm.em_Action = action.Update;
                }
                    //chưa tồn tại thông tin ngoại tru. Them
                else
                {
                    frm.em_Action = action.Insert;
                }
                frm.ShowDialog();

                cboChonBenhAn.SelectedIndex = 0;
                ThongBaoBenhAn(txtPatient_ID.Text);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void BenhAnDaithaoduong()
        {
            try
            {
                var frm = new frm_BENHAN_NGOAITRU();
                frm.uc_bant_11.txtMaBN.Text = txtPatient_ID.Text;
                frm.uc_bant_11.txtMaLanKham.Text = m_strMaLuotkham;
                frm.loaibenhan = Utility.GetLoaiBenhAn(LoaiBenhAn.BA_DaiThaoDuong);
                frm.uc_bant_41.txtHuyetApTu.Text = Utility.sDbnull(txtHa.Text);
                frm.uc_bant_41.txtNhietDo.Text = Utility.sDbnull(txtNhietDo.Text);
                frm.uc_bant_41.txtNhipTho.Text = Utility.sDbnull(txtNhipTho.Text);
                frm.uc_bant_41.txtMach.Text = Utility.sDbnull(txtNhipTim.Text);
                string ICD_Name = "";
                string ICD_Code = "";
                string ICD_chinh_Name = "";
                string ICD_chinh_Code = "";
                string ICD_Phu_Name = "";
                string ICD_Phu_Code = "";
                DataSet dsData =
                    new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(
                        Utility.Int32Dbnull(txtExam_ID.Text.Trim())).ExecuteDataSet();
                DataTable v_dtData = dsData.Tables[0];
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                {
                    GetChanDoan(Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhChinh], ""),
                                Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhPhu], ""),
                                ref ICD_Name, ref ICD_Code);
                    GetChanDoanChinhPhu(Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhChinh], ""),
                                        Utility.sDbnull(v_dtData.Rows[0][KcbChandoanKetluan.Columns.MabenhPhu], ""),
                                        ref ICD_chinh_Name,
                                        ref ICD_chinh_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                }
                frm.uc_bant_41.txtkbChanDoanRaVien.Text = ICD_Name;
                frm.uc_bant_41.txtkbMa.Text = ICD_Code;
                frm.uc_bant_41.txtBenhChinh.Text = ICD_chinh_Name + ICD_chinh_Code;
                frm.uc_bant_41.txtBenhPhu.Text = ICD_Phu_Name + ICD_Phu_Code;
                var _KcbBenhAn =
                    new Select().From(KcbBenhAn.Schema).
                        Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.sDbnull(txtPatient_ID.Text)).ExecuteSingle
                        <KcbBenhAn>();
                //Đã Tồn tại thông tin bệnh án ngoại trú. Sửa
                if (_KcbBenhAn != null)
                {
                    frm.uc_bant_11.txtID_BA.Text = Utility.sDbnull(_KcbBenhAn.Id, "");
                    frm.uc_bant_11.txtMaBenhAn.Text = Utility.sDbnull(_KcbBenhAn.SoBenhAn, -1);
                    frm.m_enAct = action.Update;
                }
                else
                {
                    frm.m_enAct = action.Insert;
                }
                frm.ShowDialog();
                cboChonBenhAn.SelectedIndex = 0;
                ThongBaoBenhAn(txtPatient_ID.Text);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void ThongBaoBenhAn(string IdBnhan)
        {
            SqlQuery SoKham =
                new Select().From(KcbBenhAn.Schema).
                    Where(KcbBenhAn.Columns.IdBnhan).IsEqualTo(Utility.sDbnull(IdBnhan));
            //Đã Tồn tại thông tin bệnh án ngoại trú.
            if (SoKham.GetRecordCount() > 0)
            {
                lblBANgoaitru.ForeColor = Color.Black;
                var objBenhAnNgoaiTru = SoKham.ExecuteSingle<KcbBenhAn>();
                lblBANgoaitru.Text = "Đã có số lưu Bệnh Án ngoại trú, Người Tạo:" +
                                     Utility.sDbnull(objBenhAnNgoaiTru.NguoiTao);
                cboChonBenhAn.SelectedIndex = 0;
            }
                //chưa tồn tại thông tin ngoại tru. 
            else
            {
                lblBANgoaitru.ForeColor = Color.Red;
                ;
                lblBANgoaitru.Text = "BN Chưa có số lưu Bệnh Án ngoại trú";
                cboChonBenhAn.SelectedIndex = 0;
            }
        }

        private void chkHienthichitiet_CheckedChanged(object sender, EventArgs e)
        {
            grdAssignDetail.GroupMode = chkHienthichitiet.Checked ? GroupMode.Expanded : GroupMode.Collapsed;
            grdAssignDetail.Refresh();
            Application.DoEvents();
        }

        private void chkAutocollapse_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._ThamKhamProperties.TudongthugonCLS = chkAutocollapse.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
            if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                grdAssignDetail.GroupMode = GroupMode.Collapsed;
        }


        private void cmdThamkhamConfig_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._ThamKhamProperties);
            frm.ShowDialog();
            CauHinhThamKham();
        }

        private void CauHinhThamKham()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                cboA4Donthuoc.Text = PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewDonthuoc.SelectedIndex = PropertyLib._MayInProperties.PreviewInDonthuoc ? 0 : 1;

                cboA4Cls.Text = PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewCLS.SelectedIndex = PropertyLib._MayInProperties.PreviewInCLS ? 0 : 1;

                cboA4Tomtatdieutringoaitru.Text = PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru ==
                                                  Papersize.A4
                                                      ? "A4"
                                                      : "A5";
                cboPrintPreviewTomtatdieutringoaitru.SelectedIndex =
                    PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru ? 0 : 1;


                cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                chkAutocollapse.Checked = PropertyLib._ThamKhamProperties.TudongthugonCLS;
                cmdPrintPres.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
                grdList.Height = PropertyLib._ThamKhamProperties.ChieucaoluoidanhsachBenhnhan <= 0
                                     ? 0
                                     : PropertyLib._ThamKhamProperties.ChieucaoluoidanhsachBenhnhan;
                grpSearch.Height = PropertyLib._ThamKhamProperties.AntimkiemNangcao ? 0 : 145;
                if (uiTabKqCls.Width > 0)
                    uiTabKqCls.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
            }
        }

        private void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                HienthithongtinBN();
        }

        private void grdList_MouseClick(object sender, MouseEventArgs e)
        {
            if (PropertyLib._ThamKhamProperties.ViewAfterClick && !_buttonClick)
                HienthithongtinBN();
            _buttonClick = false;
        }

        private void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = cboPrintPreviewDonthuoc.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = cboA4Donthuoc.SelectedIndex == 0
                                                                ? Papersize.A4
                                                                : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboPrintPreviewCLS_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInCLS = cboPrintPreviewCLS.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboA4Cls_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInCLS = cboA4Cls.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboPrintPreviewTomtatdieutringoaitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru =
                cboPrintPreviewTomtatdieutringoaitru.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void cboA4Tomtatdieutringoaitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru = cboA4Tomtatdieutringoaitru.SelectedIndex == 0
                                                                             ? Papersize.A4
                                                                             : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            HienthithongtinBN();
        }


        private void showOnMonitor2()
        {
            try
            {
                Screen[] sc;
                sc = Screen.AllScreens;
                IEnumerable<Screen> query = from mh in Screen.AllScreens
                                            select mh;
                //get all the screen width and heights

                if (query.Count() >= 2)
                {
                    objSoKham = new frm_DanhSachKham();
                    if (!CheckOpened(objSoKham.Name))
                    {
                        //SetParameterValueCallback += objSoKham.SetParamValueCallbackFn;
                        objSoKham.FormBorderStyle = FormBorderStyle.None;
                        objSoKham.Left = sc[1].Bounds.Width;
                        objSoKham.Top = sc[1].Bounds.Height;
                        objSoKham.StartPosition = FormStartPosition.CenterScreen;
                        objSoKham.Location = sc[1].Bounds.Location;
                        var p = new Point(sc[1].Bounds.Location.X, sc[1].Bounds.Location.Y);
                        objSoKham.Location = p;
                        objSoKham.WindowState = FormWindowState.Maximized;
                        objSoKham.Show();
                        //b_HasScreenmonitor = true;
                        // txtSoKham_TextChanged(txtSoKham, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        // private bool b_HasSecondMonitor=false;

        private void ShowScreen()
        {
            try
            {
                if (PropertyLib._HISQMSProperties != null)
                {
                    if (PropertyLib._HISQMSProperties.IsQMS)
                    {
                        showOnMonitor2();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void frm_LAOKHOA_Add_TIEPDON_BN_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Utility.FreeLockObject(m_strMaLuotkham);
                if (objSoKham != null && !(CheckOpened(objSoKham.Name))) objSoKham.Close();
            }
            catch (Exception exception)
            {
            }
        }

        private void CauHinhQMS()
        {
            if (PropertyLib._HISQMSProperties.IsQMS)
            {
                ShowScreen();
            }
        }

        private void AddShortCut_KETLUAN()
        {
            try
            {
                if (m_kl == null) return;
                if (!m_kl.Columns.Contains("ShortCut")) m_kl.Columns.Add(new DataColumn("ShortCut", typeof (string)));
                foreach (DataRow dr in m_kl.Rows)
                {
                    string shortcut = "";
                    string realName = dr[DmucChung.Columns.Ten].ToString().Trim() + " " +
                                      Utility.Bodau(dr[DmucChung.Columns.Ten].ToString().Trim());
                    shortcut = dr[DmucChung.Columns.Ma].ToString().Trim();
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
        }


        private void txtMaBenhphu_GotFocus(object sender, EventArgs e)
        {
            txtMaBenhphu_TextChanged(txtMaBenhphu, e);
        }

        private void txtMaBenhChinh_GotFocus(object sender, EventArgs e)
        {
            //txtMaBenhChinh_TextChanged(txtMaBenhChinh, e);
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
                m_dtKhoaNoiTru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void SearchPatient()
        {
            try
            {
                ClearControl();
                m_strMaLuotkham = "";
                objkcbdangky = null;
                objBenhnhan = null;
                objLuotkham = null;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Clear();
                if (m_dtAssignDetail != null) m_dtAssignDetail.Clear();
                if (m_dtPresDetail != null) m_dtPresDetail.Clear();
                DateTime dt_FormDate = dtFromDate.Value;
                DateTime dt_ToDate = dtToDate.Value;
                int Status = -1;
                int SoKham = -1;
                if (!string.IsNullOrEmpty(txtSoKham.Text))
                {
                    SoKham = Utility.Int32Dbnull(txtSoKham.Text, -1);
                    Status = -1;
                }
                else
                {
                    Status = radChuaKham.Checked ? 0 : 1;
                }

                m_dtDanhsachbenhnhanthamkham =
                    _KCB_THAMKHAM.LayDSachBnhanThamkham(
                        !chkByDate.Checked ? globalVariables.SysDate.AddDays(-7) : dt_FormDate,
                        !chkByDate.Checked ? globalVariables.SysDate : dt_ToDate, txtTenBN.Text, Status,
                        SoKham,
                        Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1), this.Args.Split('-')[0],
                        globalVariables.MA_KHOA_THIEN);

                if (!m_dtDanhsachbenhnhanthamkham.Columns.Contains("MAUSAC"))
                    m_dtDanhsachbenhnhanthamkham.Columns.Add("MAUSAC", typeof (int));


                Utility.SetDataSourceForDataGridEx_Basic(grdList, m_dtDanhsachbenhnhanthamkham, true, true, "",
                                                         KcbDangkyKcb.Columns.SttKham); //"locked=0", "");

                if (!dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.MaBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.MaBenh, typeof (string));
                }
                if (!dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.TenBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.TenBenh, typeof (string));
                }
                grd_ICD.DataSource = dt_ICD_PHU;
                cmdUnlock.Visible = false;
                ModifyCommmands();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void AddAutoComplete_m_strMaLuotkham()
        {
            try
            {
                DataTable dt_m_strMaLuotkham =
                    new Select(KcbDangkyKcb.MaLuotkhamColumn.ColumnName).From(KcbDangkyKcb.Schema).Distinct().
                        Where(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1).
                        ExecuteDataSet().Tables[0];
                foreach (DataRow dr in dt_m_strMaLuotkham.Rows)
                {
                    namesCollection_m_strMaLuotkham.Add(dr[KcbLuotkham.Columns.MaLuotkham].ToString());
                }
                txtPatient_Code.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtPatient_Code.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtPatient_Code.AutoCompleteCustomSource = namesCollection_m_strMaLuotkham;
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy danh sách mã lần khám");
            }
        }

        private void LaydanhsachbacsiChidinh()
        {
            try
            {
                m_dtDoctorAssign = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, 0);
                txtBacsi.Init(m_dtDoctorAssign,
                              new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }


        /// <summary>
        /// hàm thực hiện trạng thái của nút
        /// </summary>
        private void ModifyCommmands()
        {
            try
            {
                cmdPrintPres.Enabled = !string.IsNullOrEmpty(m_strMaLuotkham);
                cmdSave.Enabled = !string.IsNullOrEmpty(m_strMaLuotkham);
                cmdPrintPres.Enabled = Utility.isValidGrid(grdPresDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                cmdPrintAssign.Enabled = Utility.isValidGrid(grdAssignDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                chkIntach.Enabled = cmdPrintAssign.Enabled;
                cboServicePrint.Enabled = cmdPrintAssign.Enabled;
                tabDiagInfo.Enabled = objLuotkham != null && !string.IsNullOrEmpty(m_strMaLuotkham);
                cmdPrintPres.Enabled =
                    cmdDeletePres.Enabled =
                    cmdUpdatePres.Enabled = Utility.isValidGrid(grdPresDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);

                cmdInphieuVT.Enabled =
                    cmdXoaphieuVT.Enabled =
                    cmdSuaphieuVT.Enabled = Utility.isValidGrid(grdVTTH) && !string.IsNullOrEmpty(m_strMaLuotkham);

                cmdUpdate.Enabled =
                    cmdDelteAssign.Enabled =
                    Utility.isValidGrid(grdAssignDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);
                cmdConfirm.Enabled = Utility.isValidGrid(grdAssignDetail) && !string.IsNullOrEmpty(m_strMaLuotkham);
                
                chkDaThucHien.Visible = chkDaThucHien.Checked;
                if (objLuotkham != null)
                {
                    if (objLuotkham.Locked == 1 || objLuotkham.TrangthaiNoitru >= 1)
                    {
                        cmdInsertAssign.Enabled = cmdUpdate.Enabled = cmdDelteAssign.Enabled =
                                                                      cmdCreateNewPres.Enabled =
                                                                      cmdUpdatePres.Enabled = cmdDeletePres.Enabled =
                                                                                              cmdThemphieuVT.Enabled =
                                                                                              cmdSuaphieuVT.Enabled =
                                                                                              cmdXoaphieuVT.Enabled =
                                                                                              false;
                    }
                    else
                    {
                        cmdInsertAssign.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                        cmdCreateNewPres.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                        cmdThemphieuVT.Enabled = true && !string.IsNullOrEmpty(m_strMaLuotkham);
                    }
                    ctxDelCLS.Enabled = cmdUpdate.Enabled;
                    ctxDelDrug.Enabled = cmdUpdatePres.Enabled;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        /// <summary>
        /// Hàm kiểm tra bệnh nhân nội trú đã được nhập viện chưa?
        /// </summary>
        private bool InVali()
        {
            //SqlQuery sqlQuery = new Select().From(TPatientDept.Schema)
            //    .Where(TPatientDept.Columns.NoiTru).IsEqualTo(1)
            //    .And(TPatientDept.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //    .And(TPatientDept.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //    .AndExpression(TPatientDept.Columns.RoomId).IsGreaterThan(-1).Or(TPatientDept.Columns.RoomId).IsNotNull()
            //    .CloseExpression();
            //if (sqlQuery.GetRecordCount() > 0)
            //{
            //    Utility.ShowMsg("Bệnh nhân đã được phân buồng giường, Bạn không thể sửa thông tin ", "Thông báo",
            //                    MessageBoxIcon.Warning);
            //    //Utility.SetMsgError(errorProvider1, cboKhoaNoiTru,
            //    //                        "Bệnh nhân đã được phân buồng giường, Bạn không thể sửa thông tin ");
            //    return false;
            //}


            return true;
        }
        private void HienThiChuyenCan()
        {
            int id_chidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
            if (m_dtAssignDetail.Select("trangthai_chitiet >2 and id_chidinh = '"+id_chidinh+"'").Length > 0)
            {
                cmdConfirm.Enabled = false;
                cmdConfirm.Text = "Đã được thực hiện";
                cmdConfirm.Tag = 3;
            }
            if (m_dtAssignDetail.Select("trangthai_chuyencls in(1,2) and id_chidinh = '" + id_chidinh + "'").Length > 0)
            {
                cmdConfirm.Enabled = Utility.Coquyen("coquyenhuyketnoihislis");
                cmdConfirm.Text = "Hủy chuyển CLS";
                cmdConfirm.Tag = 2;
            }
            if (m_dtAssignDetail.Select("trangthai_chuyencls=0 and id_chidinh = '" + id_chidinh + "'").Length > 0)
            {
                cmdConfirm.Enabled = true;
                cmdConfirm.Text = "Chuyển ClS";
                cmdConfirm.Tag = 1;
            }
        }
        private void Laythongtinchidinhngoaitru()
        {
            ds =
                _KCB_THAMKHAM.LaythongtinCLSVaThuoc(Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                    Utility.sDbnull(m_strMaLuotkham, ""),
                                                    Utility.Int32Dbnull(txtExam_ID.Text));
            m_dtAssignDetail = ds.Tables[0];
            m_dtPresDetail = ds.Tables[1];
            m_dtVTTH = ds.Tables[2];
            Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtAssignDetail, false, true, "",
                                               "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
            grdAssignDetail.MoveFirst();
            HienThiChuyenCan();
            m_dtDonthuocChitiet_View = m_dtPresDetail.Clone();
            foreach (DataRow dr in m_dtPresDetail.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = m_dtDonthuocChitiet_View
                        .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                                + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                if (drview.Length <= 0)
                {
                    m_dtDonthuocChitiet_View.ImportRow(dr);
                }
                else
                {
                    drview[0][KcbDonthuocChitiet.Columns.SoLuong] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) +
                        Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                                   Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                    drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                      (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                    drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                    drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                         (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                          Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                    drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                             Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                    drview[0]["TT_BN_KHONG_PHUTHU"] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                        Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                    drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                        Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                                 Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                    m_dtDonthuocChitiet_View.AcceptChanges();
                }
            }

            Utility.SetDataSourceForDataGridEx(grdPresDetail, m_dtDonthuocChitiet_View, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);

            m_dtVTTHChitiet_View = m_dtVTTH.Clone();
            foreach (DataRow dr in m_dtVTTH.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = m_dtVTTHChitiet_View
                        .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                                + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                if (drview.Length <= 0)
                {
                    m_dtVTTHChitiet_View.ImportRow(dr);
                }
                else
                {
                    drview[0][KcbDonthuocChitiet.Columns.SoLuong] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) +
                        Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                                   Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                    drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                      (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                    drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                    drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                         (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                          Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                    drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                             Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                    drview[0]["TT_BN_KHONG_PHUTHU"] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                        Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                    drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                        Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                                 Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                    m_dtVTTHChitiet_View.AcceptChanges();
                }
            }
            //Old-->Utility.SetDataSourceForDataGridEx
            Utility.SetDataSourceForDataGridEx(grdVTTH, m_dtVTTHChitiet_View, false, true, "",
                                               KcbDonthuocChitiet.Columns.SttIn);
        }

        private void Get_DanhmucChung()
        {
            txtTrieuChungBD.Init();
            txtChanDoan.Init();
            txtHuongdieutri.Init();
            txtKet_Luan.Init();
            txtNhommau.Init();
            txtNhanxet.Init();
        }

        private void frm_KCB_THAMKHAM_Load(object sender, EventArgs e)
        {
            try
            {
                Get_DanhmucChung();
                lstVisibleColumns = Utility.GetVisibleColumns(grdAssignDetail);
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
                DataBinding.BindDataCombox(cboServicePrint, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten,
                                           "Tất cả", true);
                Load_DSach_ICD();
                LoadPhongkhamngoaitru();
                LaydanhsachbacsiChidinh();
                SearchPatient();

                if (cboServicePrint.Items.Count > 0) cboServicePrint.SelectedIndex = 0;
                cmdNhapVien.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU", "0", true) == "1";
                cmdConfirm.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CHOPHEPBACSY_CHUYENCAN","0",true) == "1";
                cmdHuyNhapVien.Visible = cmdNhapVien.Visible;

                hasLoaded = true;

                InitData();
                ClearControl();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyCommmands();
                txtPatient_Code.Focus();
                txtPatient_Code.Select();
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
            DataBinding.BindDataCombox(cboPhongKhamNgoaiTru,
                                       THU_VIEN_CHUNG.DmucLaydanhsachCacphongkhamTheoBacsi(globalVariables.UserName,
                                                                                           globalVariables.idKhoatheoMay,
                                                                                           Utility.Bool2byte(
                                                                                               globalVariables.IsAdmin),
                                                                                           0),
                                       DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                       "---Chọn phòng khám---", false);
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
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                             Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                             "%'");
                else
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
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
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                cboChonBenhAn.Visible = Utility.DoTrim(txtMaBenhChinh.Text) != "" &&
                                        THU_VIEN_CHUNG.Laygiatrithamsohethong("BENH_AN", "0", false) == "1" &&
                                        (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU", "ALL", false) ==
                                         "ALL" ||
                                         THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU", "ALL", false)
                                             .Contains(Utility.DoTrim(txtMaBenhChinh.Text)));
                lblBenhan.Visible = cboChonBenhAn.Visible;
                setChanDoan();
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
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                         Utility.sDbnull(txtMaBenhphu.Text, "") +
                                                         "%'");
            else
                arrDr =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
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

        /// <summary>
        /// hàm thực hiện việc tìm kiếm bệnh phụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearchBenhPhu_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frm_QuickSearchDiseases();
                frm.DiseaseType_ID = -1;
                frm.ShowDialog();
                if (frm.m_blnCancel)
                {
                    //  m_blnGetAuxDiseasesCodeFromList = true;
                    txtMaBenhphu.Text = frm.v_DiseasesCode;
                    txtMaBenhphu.Focus();
                    txtMaBenhphu.SelectAll();
                    //cboDeaisetype_ID.SelectedIndex = Utility.GetSelectedIndex(cboDeaisetype_ID,
                    //  frm.DiseaseType_ID.ToString());
                    //  lstAuxDiseasesTip.Visible = false;
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        /// <summary>
        /// hàm thực hiện viecj tim fkieems bệnh chính
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearchBenhChinh_Click(object sender, EventArgs e)
        {
            ShowDiseaseList();
        }

        /// <summary>
        /// hàm thực hiện hsow thông tin cua bệnh
        /// </summary>
        private void ShowDiseaseList()
        {
            try
            {
                var frm = new frm_QuickSearchDiseases();
                frm.DiseaseType_ID = -1;
                frm.ShowDialog();
                if (frm.m_blnCancel)
                {
                    txtMaBenhChinh.Text = frm.v_DiseasesCode;
                    txtMaBenhChinh.Focus();
                    txtMaBenhChinh.SelectAll();
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
        }

        private void ClearControl()
        {
            try
            {
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                grdAssignDetail.DataSource = null;
                grdPresDetail.DataSource = null;
                txtReg_ID.Text = "";
                // txtPatientDept_ID.Clear();
                //  txtIdKhoaNoiTru.Clear();
                //  txtKhoaNoiTru.Clear();
                cmdNhapVien.Enabled = false;
                cmdHuyNhapVien.Enabled = false;
                txtTenDvuKham.Clear();
                txtPhongkham.Clear();
                foreach (Control control in pnlPatientInfor.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is AutoCompleteTextbox)
                    {
                        ((AutoCompleteTextbox) control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox) (control)).Clear();
                    }
                }


                foreach (Control control in pnlKetluan.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is AutoCompleteTextbox)
                    {
                        ((AutoCompleteTextbox) control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox) (control)).Clear();
                    }
                }

                foreach (Control control in pnlother.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is AutoCompleteTextbox)
                    {
                        ((AutoCompleteTextbox) control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox) (control)).Clear();
                    }
                }
                txtSongaydieutri.Text = "0";
            }
            catch (Exception)
            {
            }
        }

        private void getResult()
        {
            try
            {
                //QueryCommand cmd = KcbKetquaCl.CreateQuery().BuildCommand();
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandSql = "Select *," +
                //                 "(select TOP 1 id_dichvu from dmuc_dichvucls_chitiet where id_chitietdichvu=p.Loai_XN ) as id_dichvu," +
                //                 "(select TOP 1 IntOrder from dmuc_dichvucls_chitiet where id_chitietdichvu=p.Loai_XN ) as sIntOrder," +
                //                 "(select TOP 1 ten_dichvu from dmuc_dichvucls where id_dichvu in(select TOP 1 id_dichvu from dmuc_dichvucls_chitiet where id_chitietdichvu=p.Loai_XN ) ) as ten_dichvu," +
                //                 "(select TOP 1 intOrder from dmuc_dichvucls where id_dichvu in(select TOP 1 id_dichvu from dmuc_dichvucls_chitiet where id_chitietdichvu=p.Loai_XN ) ) as stt_hthi_dichvu " +
                //                 "from kcb_ketqua_cls p " +
                //                 "where ID_CHI_DINH in(Select id_chidinh  from kcb_chidinhcls where ma_luotkham='" +
                //                 m_strMaLuotkham.Trim() + "') order by stt_hthi_dichvu,sIntOrder,Ten_KQ";
                //DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                //uiGroupBox6.Width = temdt != null && temdt.Rows.Count > 0 ? 300 : 0;
                //Utility.SetDataSourceForDataGridEx(grdKetQua, temdt, true, true, "", "");
            }
            catch
            {
            }
        }


        /// <summary>
        /// Lấy về thông tin bệnh nhâ nội trú
        /// </summary>
        private void GetData()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (!Utility.isValidGrid(grdList))
                {
                    return;
                }
                bool KHAMCHEO_CACKHOA = THU_VIEN_CHUNG.Laygiatrithamsohethong("KHAMCHEO_CACKHOA", "0", true) == "1";

                // Utility.SetMsg(lblMsg, "", false);
                string PatientCode = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                m_strMaLuotkham = PatientCode;
                if (!Utility.CheckLockObject(m_strMaLuotkham, "Thăm khám", "TK"))
                    return;

                int Id_Benhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                objLuotkham = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(PatientCode)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Id_Benhnhan).ExecuteSingle<KcbLuotkham>();

                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);

                isRoom = false;
                if (objLuotkham != null)
                {
                    string ten_khoaphong = Utility.sDbnull(grdList.GetValue("ten_khoaphong"), "");

                    cmdChuyenPhong.Visible = objLuotkham.TrangthaiNoitru == 0;
                    int hien_thi = Utility.Int32Dbnull(grdList.GetValue("hien_thi"), 0);
                    if (hien_thi == 0)
                    {
                        Utility.ShowMsg("Bệnh nhân " + objBenhnhan.TenBenhnhan +
                                        " CHƯA NỘP TIỀN KHÁM trong khi thuộc đối tượng khám chữa bệnh CẦN THANH TOÁN TRƯỚC KHI VÀO PHÒNG KHÁM.\nYêu cầu Bệnh nhân đi NỘP TIỀN KHÁM TRƯỚC");
                        objLuotkham = null;
                        objBenhnhan = null;
                        m_strMaLuotkham = "";
                        return;
                    }
                    if (!KHAMCHEO_CACKHOA && globalVariables.MA_KHOA_THIEN != objLuotkham.MaKhoaThuchien)
                    {
                        Utility.ShowMsg("Bệnh nhân này được tiếp đón và chỉ định khám cho khoa " + ten_khoaphong +
                                        ". Trong khi máy bạn đang cấu hình khám chữa bệnh cho khoa " +
                                        globalVariablesPrivate.objKhoaphong.TenKhoaphong +
                                        "\nHệ thống không cho phép khám chéo giữa các khoa. Đề nghị liên hệ Bộ phận IT trong đơn vị để được trợ giúp");
                        objLuotkham = null;
                        objBenhnhan = null;
                        m_strMaLuotkham = "";
                        return;
                    }
                    ClearControl();
                    lblSongaydieutri.ForeColor = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb)
                                                     ? lblKetluan.ForeColor
                                                     : lblBenhphu.ForeColor;
                    lblBenhchinh.ForeColor = lblSongaydieutri.ForeColor;
                    lblBANoitru.Text = Utility.Int32Dbnull(objLuotkham.SoBenhAn, -1) <= 0
                                           ? ""
                                           : "Số B.A Nội trú: " + objLuotkham.SoBenhAn;
                    txt_idchidinhphongkham.Text = Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.IdKham));

                    objkcbdangky = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
                    var objStaff =
                        new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(
                            Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                    string TenNhanvien = objLuotkham.NguoiKetthuc;
                    if (objStaff != null)
                        TenNhanvien = objStaff.TenNhanvien;
                    pnlCLS.Enabled = true;
                    pnlDonthuoc.Enabled = true;
                    pnlVTTH.Enabled = true;
                    if (objkcbdangky != null)
                    {
                        cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && objLuotkham.Locked.ToString() == "1";
                        cmdUnlock.Enabled = cmdUnlock.Visible &&
                                            (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                             objLuotkham.NguoiKetthuc == globalVariables.UserName);
                        if (!cmdUnlock.Enabled)
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " +
                                                TenNhanvien + "(" + objLuotkham.NguoiKetthuc +
                                                " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                        else
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                        cmdNhapVien.Enabled = objkcbdangky.TrangThai == 1;
                        cmdHuyNhapVien.Enabled = objLuotkham.TrangthaiNoitru >= 1;
                        cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                        cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";

                        DataTable m_dtThongTin = _KCB_THAMKHAM.LayThongtinBenhnhanKCB(objLuotkham.MaLuotkham,
                                                                                      Utility.Int32Dbnull(
                                                                                          objLuotkham.IdBenhnhan,
                                                                                          -1),
                                                                                      Utility.Int32Dbnull(
                                                                                          txt_idchidinhphongkham.Text));

                        if (m_dtThongTin.Rows.Count > 0)
                        {
                            DataRow dr = m_dtThongTin.Rows[0];
                            if (dr != null)
                            {
                                dtInput_Date.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayTiepdon]);
                                txtExam_ID.Text = Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.IdKham));


                                txtGioitinh.Text =
                                    Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.GioiTinh), "");
                                txt_idchidinhphongkham.Text =
                                    Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.IdKham));
                                lblSOkham.Text = Utility.sDbnull(grdList.GetValue(KcbDangkyKcb.Columns.SttKham));
                                txtPatient_Name.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                                txtPatient_ID.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                                txtPatient_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaLuotkham], "");
                                barcode.Data = m_strMaLuotkham;
                                txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                                txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");
                                txtTrieuChungBD._Text = Utility.sDbnull(dr[KcbLuotkham.Columns.TrieuChung], "");
                                txtObjectType_Name.Text = Utility.sDbnull(dr[DmucDoituongkcb.Columns.TenDoituongKcb], "");
                                txtSoBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MatheBhyt], "");
                                txtBHTT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.PtramBhyt], "0");
                                txtMaBenhAn.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.SoBenhAn], "");
                                //txtNgheNghiep.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.NgheNghiep], "");
                                txtHanTheBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], "");
                                dtpNgayhethanBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt],
                                                                         globalVariables.SysDate.ToString("dd/MM/yyyy"));
                                txtTuoi.Text = Utility.sDbnull(Utility.Int32Dbnull(globalVariables.SysDate.Year) -
                                                               objBenhnhan.NgaySinh.Value.Year);
                                ThongBaoBenhAn(txtPatient_ID.Text);

                                if (objkcbdangky != null)
                                {
                                    // txtSTTKhamBenh.Text = Utility.sDbnull(objkcbdangky.SoKham);
                                    txtReg_ID.Text = Utility.sDbnull(objkcbdangky.IdKham);
                                    dtpCreatedDate.Value = Convert.ToDateTime(objkcbdangky.NgayDangky);
                                    txtDepartment_ID.Text = Utility.sDbnull(objkcbdangky.IdPhongkham);
                                    var _department =
                                        new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).
                                            IsEqualTo(objkcbdangky.IdPhongkham).ExecuteSingle<DmucKhoaphong>();
                                    if (_department != null)
                                    {
                                        txtPhongkham.Text = Utility.sDbnull(_department.TenKhoaphong);
                                    }
                                    txtTenDvuKham.Text = Utility.sDbnull(objkcbdangky.TenDichvuKcb);
                                    txtNguoiTiepNhan.Text = Utility.sDbnull(objkcbdangky.NguoiTao);
                                    if (Utility.Int16Dbnull(objkcbdangky.IdBacsikham, -1) != -1)
                                    {
                                        txtBacsi.SetId(Utility.sDbnull(objkcbdangky.IdBacsikham, -1));
                                    }
                                    else
                                    {
                                        txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                                    }

                                    chkDaThucHien.Checked = Utility.Int32Dbnull(objkcbdangky.TrangThai) == 1;
                                }
                                _KcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                    .Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objkcbdangky.IdKham).
                                    ExecuteSingle
                                    <KcbChandoanKetluan>();
                                if (_KcbChandoanKetluan != null)
                                {
                                    txtKet_Luan._Text = Utility.sDbnull(_KcbChandoanKetluan.Ketluan);
                                    // txtHuongdieutri.SetCode(_KcbChandoanKetluan.HuongDieutri);
                                    txtHuongdieutri._Text = _KcbChandoanKetluan.HuongDieutri;
                                    txtSongaydieutri.Text = Utility.sDbnull(_KcbChandoanKetluan.SongayDieutri, "0");
                                    txtHa.Text = Utility.sDbnull(_KcbChandoanKetluan.Huyetap);
                                    txtTrieuChungBD._Text = Utility.sDbnull(_KcbChandoanKetluan.TrieuchungBandau);
                                    txtMach.Text = Utility.sDbnull(_KcbChandoanKetluan.Mach);
                                    txtNhipTim.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhiptim);
                                    txtNhipTho.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhiptho);
                                    txtNhietDo.Text = Utility.sDbnull(_KcbChandoanKetluan.Nhietdo);
                                    txtCannang.Text = Utility.sDbnull(_KcbChandoanKetluan.Cannang);
                                    txtChieucao.Text = Utility.sDbnull(_KcbChandoanKetluan.Chieucao);
                                    txtNhanxet.Text = Utility.sDbnull(_KcbChandoanKetluan.NhanXet);
                                    if (!string.IsNullOrEmpty(Utility.sDbnull(_KcbChandoanKetluan.Nhommau)) &&
                                        Utility.sDbnull(_KcbChandoanKetluan.Nhommau) != "-1")
                                        txtNhommau._Text = Utility.sDbnull(_KcbChandoanKetluan.Nhommau);


                                    isLike = false;
                                    txtChanDoan._Text = Utility.sDbnull(_KcbChandoanKetluan.Chandoan);
                                    txtChanDoanKemTheo.Text = Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo);
                                    txtMaBenhChinh.Text = Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh);
                                    string dataString = Utility.sDbnull(_KcbChandoanKetluan.MabenhPhu, "");
                                    isLike = true;
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
                                Laythongtinchidinhngoaitru();
                            }
                        }
                        //cmdKETTHUC.Visible = objLuotkham.Locked == 0 && objkcbdangky.TrangThai > 0;
                        cmdInTTDieuTri.Visible =
                            cmdInphieuhen.Visible = objkcbdangky.TrangThai != 0 && objLuotkham.TrangthaiNoitru == 0;
                        cboChonBenhAn.Visible = objkcbdangky.TrangThai != 0 &&
                                                THU_VIEN_CHUNG.Laygiatrithamsohethong("BENH_AN", "0", true) == "1" &&
                                                objLuotkham.TrangthaiNoitru == 0;
                        lblBenhan.Visible = cboChonBenhAn.Visible;
                        // cmdBenhAnNgoaiTru.Visible = objkcbdangky.TrangThai != 0;
                        cmdKETTHUC.Enabled = true;
                    }
                    else
                    {
                        ClearControl();
                        cmdKETTHUC.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyCommmands();
                KiemTraDaInPhoiBHYT();
                getResult();
            }
        }

        private void LoadBenh()
        {
            try
            {
                isLike = false;
                txtChanDoan._Text = Utility.sDbnull(_KcbChandoanKetluan.Chandoan);
                txtChanDoanKemTheo.Text = Utility.sDbnull(_KcbChandoanKetluan.ChandoanKemtheo);
                txtMaBenhChinh.Text = Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh);
                string dataString = Utility.sDbnull(_KcbChandoanKetluan.MabenhPhu, "");
                isLike = true;
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

        private void ModifyByLockStatus(byte lockstatus)
        {
            cmdCreateNewPres.Enabled = !Utility.isTrue(lockstatus);
            cmdUpdatePres.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdDeletePres.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdPrintPres.Enabled = grdPresDetail.RowCount > 0 && !string.IsNullOrEmpty(m_strMaLuotkham);
            ctxDelDrug.Enabled = cmdUpdatePres.Enabled;

            cmdInsertAssign.Enabled = !Utility.isTrue(lockstatus);
            cmdUpdate.Enabled = grdAssignDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdDelteAssign.Enabled = grdAssignDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdPrintAssign.Enabled = grdAssignDetail.RowCount > 0 && !string.IsNullOrEmpty(m_strMaLuotkham);
            chkIntach.Enabled = cmdPrintAssign.Enabled;
            cboServicePrint.Enabled = cmdPrintAssign.Enabled;
            ctxDelCLS.Enabled = cmdUpdate.Enabled;
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
                        cmdSave.Enabled = false;
                        grd_ICD.Enabled = false;
                        cmdChuyenPhong.Enabled = false;
                        cmdLuuChandoan.Enabled = false;
                        toolTip1.SetToolTip(cmdSave, "Bệnh nhân đã kết thúc nên bạn không thể sửa thông tin được nữa");
                    }
                }
                else
                {
                    grd_ICD.Enabled = true;
                    cmdLuuChandoan.Enabled = true;
                    cmdChuyenPhong.Enabled = true;
                    cmdSave.Enabled = true;
                    toolTip1.SetToolTip(cmdSave, "Nhấn vào đây để kết thúc khám cho Bệnh nhân(Phím tắt Ctrl+S)");
                    lblMessage.Visible = false;
                }
            } //Đối tượng dịch vụ sẽ luôn hiển thị nút lưu
            else
            {
                grd_ICD.Enabled = true;
                cmdSave.Enabled = true;
                cmdLuuChandoan.Enabled = true;
                cmdChuyenPhong.Enabled = true;
            }
        }

        private string GetTenBenh(string MaBenh)
        {
            string TenBenh = "";
            DataRow[] arrMaBenh =
                globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", MaBenh));
            if (arrMaBenh.GetLength(0) > 0) TenBenh = Utility.sDbnull(arrMaBenh[0][DmucBenh.Columns.TenBenh], "");
            return TenBenh;
        }


        private void HienthithongtinBN()
        {
            try
            {
                Utility.FreeLockObject(m_strMaLuotkham);
                if (!Utility.isValidGrid(grdList))
                {
                    ClearControl();
                    return;
                }
                cmdInphieuhen.Visible = false;
                cmdInTTDieuTri.Visible = false;
                cboChonBenhAn.Visible = false;
                lblBenhan.Visible = cboChonBenhAn.Visible;
                //  cmdBenhAnNgoaiTru.Visible = false;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                GetData();
                //ModifyByLockStatus(objLuotkham.Locked);
                //  GetData_Noitru();
                foreach (GridEXRow row in grdList.GetDataRows())
                {
                    row.BeginEdit();
                    row.Cells["MAUSAC"].Value = 0;
                    row.EndEdit();
                }
                grdList.CurrentRow.Cells["MAUSAC"].Value = 1;
                grdList.Refresh();
                if (Utility.Int32Dbnull(grdList.CurrentRow.Cells["MAUSAC"].Value, 0) == 1)
                {
                    grdList.SelectedFormatStyle.ForeColor = Color.White;
                    grdList.SelectedFormatStyle.BackColor = Color.SteelBlue;
                }
                txtMach.Focus();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg(exception.ToString());
                //throw;
            }
            finally
            {
                setChanDoan();
            }
        }

        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "cmdCHONBN")
                {
                    Utility.FreeLockObject(m_strMaLuotkham);
                    _buttonClick = true;
                    GridEXColumn gridExColumn = grdList.RootTable.Columns[KcbDangkyKcb.Columns.SttKham];
                    grdList.Col = gridExColumn.Position;
                    HienthithongtinBN();
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        private void Unlock()
        {
            try
            {
                if (objLuotkham == null)
                    return;
                //Kiểm tra nếu đã in phôi thì cần hủy in phôi
                var _item = new Select().From(KcbPhieuDct.Schema)
                    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<KcbPhieuDct>();
                if (_item != null)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân này thuộc đối tượng BHYT đã được in phôi. Bạn cần liên hệ bộ phận thanh toán hủy in phôi để mở khóa bệnh nhân");
                    return;
                }
                new Update(KcbLuotkham.Schema)
                    .Set(KcbLuotkham.Columns.Locked).EqualTo(0)
                    .Set(KcbLuotkham.Columns.TrangthaiNgoaitru).EqualTo(0)
                    .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(string.Empty)
                    .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(
                        objLuotkham.MaLuotkham)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                objLuotkham.Locked = 0;
                objLuotkham.TrangthaiNgoaitru = 0;
                //ModifyByLockStatus(objLuotkham.Locked);
                cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && objLuotkham.Locked.ToString() == "1";
                cmdUnlock.Enabled = cmdUnlock.Visible &&
                                    (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                     objLuotkham.NguoiKetthuc == globalVariables.UserName);
                if (!cmdUnlock.Enabled)
                    toolTip1.SetToolTip(cmdUnlock,
                                        "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ Quản trị hệ thống để được mở khóa");
                else
                    toolTip1.SetToolTip(cmdUnlock,
                                        "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                GetData();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện việc dùng phím tắt in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_THAMKHAM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if ((ActiveControl != null && ActiveControl.Name == grdList.Name) ||
                    (tabPageChanDoan.ActiveControl != null && tabPageChanDoan.ActiveControl.Name == txtMaBenhphu.Name))
                    return;
                else
                    SendKeys.Send("{TAB}");
            if (e.Control && e.KeyCode == Keys.P)
            {
                if (tabDiagInfo.SelectedTab == tabPageChiDinhCLS)
                    cmdPrintAssign_Click(cmdPrintAssign, new EventArgs());
                if (tabDiagInfo.SelectedTab == tabPageChanDoan)
                    cmdInTTDieuTri_Click(cmdInTTDieuTri, new EventArgs());
                else
                    cmdPrintPres_Click(cmdPrintPres, new EventArgs());
            }

            if (e.Control & e.KeyCode == Keys.F) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.Control && e.KeyCode == Keys.U)
                Unlock();
            if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl)
                Utility.ShowMsg(ActiveControl.Name);
            if (e.KeyCode == Keys.F6)
            {
                txtPatient_Code.SelectAll();
                txtPatient_Code.Focus();
                return;
            }
            if (e.KeyCode == Keys.F1)
            {
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                txtMach.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                tabDiagInfo.SelectedTab = tabPageChiDinhCLS;
                if (grdAssignDetail.RowCount <= 0)
                {
                    cmdInsertAssign.Focus();
                    cmdInsertAssign_Click(cmdInsertAssign, new EventArgs());
                }
                else
                {
                    cmdUpdate.Focus();
                    cmdUpdate_Click(cmdUpdate, new EventArgs());
                }
            }

            if (e.KeyCode == Keys.F3)
            {
                tabDiagInfo.SelectedTab = tabPageChidinhThuoc;
                if (grdPresDetail.RowCount <= 0)
                {
                    cmdCreateNewPres.Focus();
                    cmdCreateNewPres_Click(cmdCreateNewPres, new EventArgs());
                }
                else
                {
                    cmdUpdatePres.Focus();
                    cmdUpdatePres_Click(cmdUpdatePres, new EventArgs());
                }
            }
            if (e.KeyCode == Keys.F4)
            {
                if (tabDiagInfo.SelectedTab == tabPageChidinhThuoc) cmdPrintPres.PerformClick();
                if (tabDiagInfo.SelectedTab == tabPageChiDinhCLS) cmdPrintAssign.PerformClick();
            }
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();

            if (e.Control && e.KeyCode == Keys.N)
            {
                if (tabDiagInfo.SelectedTab == tabPageChidinhThuoc)
                    cmdCreateNewPres_Click(cmdCreateNewPres, new EventArgs());
                else
                    cmdInsertAssign_Click(cmdInsertAssign, new EventArgs());
            }
        }

        private void txtSoKham_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoKham.Text))
            {
                chkByDate.Checked = false;
                cmdSearch.PerformClick();
            }
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
            ModifyCommmands();
            HienThiChuyenCan();
            ShowResult();
        }

        private void ShowResult()
        {
            try
            {
                Int16 trangthai_chitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.TrangthaiChitiet), 0);
                Int16 CoChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);

                int IdChitietdichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                int IdDichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);

                int IdChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                int IdChidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);

                string maloai_dichvuCLS =
                    Utility.sDbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                string MaChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh),
                                    "XN");
                string MaBenhpham =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham),
                                    "XN");
                if (trangthai_chitiet <= 3)
                    //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
                {
                    uiTabKqCls.Width = 0;
                    Application.DoEvents();
                }
                else
                {
                    if (maloai_dichvuCLS == "XN")
                        pnlXN.BringToFront();
                    else
                        pnlXN.SendToBack();
                    if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
                    {
                        if (CoChitiet == 1 || maloai_dichvuCLS != "XN")
                            uiTabKqCls.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                        else
                            uiTabKqCls.Width = 0;
                    }
                    else
                    {
                        uiTabKqCls.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                    }
                    if (CoChitiet == 1)
                        Utility.ShowColumns(grdKetqua, lstKQCochitietColumns);
                    else
                        Utility.ShowColumns(grdKetqua, lstKQKhongchitietColumns);
                    //Lấy dữ liệu CLS
                    if (maloai_dichvuCLS == "XN")
                    {
                        DataTable dt =
                            SPs.ClsTimkiemketquaXNChitiet(objLuotkham.MaLuotkham, MaChidinh, MaBenhpham, IdChidinh,
                                                          CoChitiet, IdDichvu, IdChitietdichvu).GetDataSet().Tables[0];
                        Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1",
                                                                 "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

                        Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
                    }
                    else //XQ,SA,DT,NS
                    {
                        DataTable dtMauQK = clsHinhanh.HinhanhLaydanhsachMauKQtheoDichvuCLS(IdChitietdichvu);
                        txtMauKQ.Init(dtMauQK, new List<string>() { QheDichvuMauketqua.Columns.MaMauKQ, QheDichvuMauketqua.Columns.MaMauKQ, DmucChung.Columns.Ten });
                        txtMauKQ__OnEnterMe();

                        FillDynamicValues(IdChitietdichvu, IdChitietchidinh);
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        private void FillDynamicValues(int IdDichvuChitiet, int idchidinhchitiet)
        {
            try
            {
                pnlDynamicValues.Controls.Clear();

                DataTable dtData = clsHinhanh.GetDynamicFieldsValues(IdDichvuChitiet, txtMauKQ.myCode, "", "", -1, idchidinhchitiet);
                var lnkViewImage = new LinkLabel();
                lnkViewImage.Text = "Xem hình ảnh";
                lnkViewImage.Tag = idchidinhchitiet;
                lnkViewImage.Click += lnkViewImage_Click;
                pnlDynamicValues.Controls.Add(lnkViewImage);
                foreach (DataRow dr in dtData.Select("1=1", "Stt_hthi"))
                {
                    dr[DynamicValue.Columns.IdChidinhchitiet] = Utility.Int32Dbnull(idchidinhchitiet);
                    var _ucTextSysparam = new ucDynamicParam(dr, true);
                    _ucTextSysparam._ReadOnly = true;
                    _ucTextSysparam.onlyView = true;
                    _ucTextSysparam.TabStop = true;
                    _ucTextSysparam.TabIndex = 10 + Utility.Int32Dbnull(dr[DynamicField.Columns.Stt], 0);
                    _ucTextSysparam.Init();
                    if (Utility.Byte2Bool(dr[DynamicField.Columns.Rtxt]))
                    {
                        _ucTextSysparam.Size = PropertyLib._DynamicInputProperties.RtfDynamicSize;
                        _ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.RtfTextSize;
                        _ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.RtfLabelSize;
                    }
                    else
                    {
                        _ucTextSysparam.Size = PropertyLib._DynamicInputProperties.DynamicSize;
                        _ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.TextSize;
                        _ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.LabelSize;
                    }
                    pnlDynamicValues.Controls.Add(_ucTextSysparam);
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        private void lnkViewImage_Click(object sender, EventArgs e)
        {
            try
            {
                int idchidinhchitiet = Utility.Int32Dbnull(((LinkLabel) sender).Tag);
                KcbChidinhclsChitiet objChitiet = KcbChidinhclsChitiet.FetchByID(idchidinhchitiet);
                if (objChitiet != null)
                {
                    if (Utility.sDbnull(objChitiet.ImgPath1, "") != "" ||
                        Utility.sDbnull(objChitiet.ImgPath2, "") != "" | Utility.sDbnull(objChitiet.ImgPath3, "") != "" ||
                        Utility.sDbnull(objChitiet.ImgPath4, "") != "")
                    {
                        var _ViewImages = new frm_ViewImages(objChitiet);
                        _ViewImages.ShowDialog();
                    }
                    else
                    {
                        ((LinkLabel) sender).Text = "Chưa có ảnh";
                    }
                }
                else
                {
                    ((LinkLabel) sender).Text = "Lỗi: Không lấy được chi tiết";
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {
        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Utility.Int32Dbnull(grdList.CurrentRow.Cells["MAUSAC"].Value, 0) == 1)
            //    {
            //        //grdList.SelectedFormatStyle.FontSize = 13;
            //        grdList.SelectedFormatStyle.ForeColor = Color.White;
            //        grdList.SelectedFormatStyle.BackColor = Color.SteelBlue;
            //    }
            //    else
            //    {
            //        //grdList.SelectedFormatStyle.FontSize = 9;
            //        grdList.SelectedFormatStyle.ForeColor = Color.Black;
            //        grdList.SelectedFormatStyle.BackColor = SystemColors.ControlLight;
            //    }
            //}
            //catch
            //{
            //}
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
                    Utility.FreeLockObject(m_strMaLuotkham);
                    string _patient_Code = Utility.AutoFullPatientCode(txtPatient_Code.Text);
                    ClearControl();
                    txtPatient_Code.Text = _patient_Code;
                    if (grdList.RowCount > 0 && PropertyLib._ThamKhamProperties.Timtrenluoi)
                    {
                        DataRow[] arrData_tempt = null;
                        arrData_tempt =
                            m_dtDanhsachbenhnhanthamkham.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code +
                                                                "'");
                        if (arrData_tempt.Length > 0)
                        {
                            string _status = radChuaKham.Checked ? "0" : "1";
                            string regStatus = Utility.sDbnull(arrData_tempt[0][KcbDangkyKcb.Columns.TrangThai], "0");
                            if (_status != regStatus)
                            {
                                if (regStatus == "1") radDaKham.Checked = true;
                                else
                                    radChuaKham.Checked = true;
                            }
                            Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, _patient_Code);
                            if (Utility.isValidGrid(grdList)) grdList_DoubleClick(grdList, new EventArgs());
                            return;
                        }
                    }

                    dtPatient = _KCB_THAMKHAM.TimkiemBenhnhan(txtPatient_Code.Text,
                                                              Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1),
                                                              0, 0);

                    DataRow[] arrPatients = dtPatient.Select(KcbLuotkham.Columns.MaLuotkham + "='" + _patient_Code + "'");
                    if (arrPatients.GetLength(0) <= 0)
                    {
                        if (dtPatient.Rows.Count > 1)
                        {
                            var frm = new frm_DSACH_BN_TKIEM();
                            frm.MaLuotkham = txtPatient_Code.Text;
                            frm.dtPatient = dtPatient;
                            frm.ShowDialog();
                            if (!frm.has_Cancel)
                            {
                                txtPatient_Code.Text = frm.MaLuotkham;
                            }
                        }
                    }
                    else
                    {
                        txtPatient_Code.Text = _patient_Code;
                    }
                    DataTable dt_Patient = _KCB_THAMKHAM.TimkiemThongtinBenhnhansaukhigoMaBN
                        (txtPatient_Code.Text, Utility.Int32Dbnull(cboPhongKhamNgoaiTru.SelectedValue, -1),
                         globalVariables.MA_KHOA_THIEN);

                    grdList.DataSource = null;
                    grdList.DataSource = dt_Patient;
                    if (dt_Patient.Rows.Count > 0)
                    {
                        grdList.MoveToRowIndex(0);
                        grdList.CurrentRow.BeginEdit();
                        grdList.CurrentRow.Cells["MAUSAC"].Value = 1;
                        grdList.CurrentRow.EndEdit();
                        if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                        GetData();
                        txtPatient_Code.SelectAll();
                    }
                    else
                    {
                        string sPatientTemp = txtPatient_Code.Text;
                        m_strMaLuotkham = "";
                        objLuotkham = null;
                        objkcbdangky = null;
                        objBenhnhan = null;
                        ClearControl();

                        txtPatient_Code.Text = sPatientTemp;
                        txtPatient_Code.SelectAll();
                        //Utility.SetMsg(lblMsg, "Không tìm thấy bệnh nhân có mã lần khám đang chọn",true);
                    }
                    txtMach.SelectAll();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
            }
            finally
            {
                ModifyCommmands();
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
            if (e.KeyCode == Keys.Enter)
            {
                if (!InValiMaBenh(txtMaBenhChinh.Text))
                {
                    DSACH_ICD(txtTenBenhChinh, DmucChung.Columns.Ten, 0);
                    txtMaBenhphu.Focus();
                    //hasMorethanOne = false;
                }
                else
                    txtMaBenhphu.Focus();
            }
        }

        private void DSACH_ICD(EditBox tEditBox, string LOAITIMKIEM, int CP)
        {
            try
            {
                string sFillter = "";
                if (LOAITIMKIEM.ToUpper() == DmucChung.Columns.Ten)
                {
                    sFillter = " Disease_Name like '%" + tEditBox.Text + "%' OR FirstChar LIKE '%" + tEditBox.Text +
                               "%'";
                }
                else if (LOAITIMKIEM == DmucChung.Columns.Ma)
                {
                    sFillter = DmucBenh.Columns.MaBenh + " LIKE '%" + tEditBox.Text + "%'";
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
            //if (e.KeyCode == Keys.Enter )
            //{
            //    if (!InValiMaBenh(txtMaBenhChinh.Text))
            //    {
            //        DSACH_ICD(txtMaBenhChinh, DmucChung.Columns.Ma, 0);
            //        hasMorethanOne = false;
            //         txtMaBenhphu.Focus();
            //    }
            //    else
            //        txtMaBenhphu.Focus();
            //}
            if (e.KeyCode == Keys.Enter)
            {
                if (hasMorethanOne)
                {
                    DSACH_ICD(txtMaBenhChinh, DmucChung.Columns.Ma, 0);
                    hasMorethanOne = false;
                    //txtMaBenhphu.Focus();
                }
                //else
                //    txtMaBenhphu.Focus();
            }
        }

        private void cmdAddMaBenhPhu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaBenhphu.Text.TrimStart().TrimEnd() == "" || txtTenBenhPhu.Text.TrimStart().TrimEnd() == "")
                    return;
                //int record = dt_ICD.Select(string.Format(DmucBenh.Columns.MaBenh+ " ='{0}'", txtMaBenhphu.Text)).GetLength(0);
                EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                             txtMaBenhphu.Text
                                                         select benh;


                if (!query.Any())
                {
                    AddMaBenh(txtMaBenhphu.Text, TEN_BENHPHU);
                    txtMaBenhphu.ResetText();
                    txtTenBenhPhu.ResetText();
                    txtMaBenhphu.Focus();
                    txtMaBenhphu.SelectAll();
                }
                else
                {
                    txtMaBenhphu.ResetText();
                    txtTenBenhPhu.ResetText();
                    txtMaBenhphu.Focus();
                    txtMaBenhphu.SelectAll();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình thêm thông tin vào lưới");
            }
            finally
            {
                setChanDoan();
            }
        }

        private void AddMaBenh(string MaBenh, string TenBenh)
        {
            //DataRow[] arrDr = dt_ICD_PHU.Select(string.Format("MA_ICD='{0}'", MaBenh));
            EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                     where Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == MaBenh
                                                     select benh;
            if (!query.Any())
            {
                DataRow drv = dt_ICD_PHU.NewRow();
                drv[DmucBenh.Columns.MaBenh] = MaBenh;
                EnumerableRowCollection<string> query1 = from benh in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) ==
                                                             MaBenh
                                                         select Utility.sDbnull(benh[DmucBenh.Columns.TenBenh]);
                if (query1.Any())
                {
                    drv[DmucBenh.Columns.TenBenh] = Utility.sDbnull(query1.FirstOrDefault());
                }

                dt_ICD_PHU.Rows.Add(drv);
                dt_ICD_PHU.AcceptChanges();
                grd_ICD.AutoSizeColumns();
            }
        }

        private void txtTenBenhPhu_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{

            //    if (InValiMaBenh(txtMaBenhphu.Text))
            //        cmdAddMaBenhPhu.PerformClick();
            //    else
            //    {
            //        DSACH_ICD(txtTenBenhPhu, DmucChung.Columns.Ten, 1);
            //    }
            //}
            if (e.KeyCode == Keys.Enter)
            {
                if (hasMorethanOne)
                {
                    DSACH_ICD(txtTenBenhPhu, DmucChung.Columns.Ten, 1);
                    txtTenBenhPhu.Focus();
                }
                else
                    txtTenBenhPhu.Focus();
            }
        }

        private void grd_ICD_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA")
                {
                    grd_ICD.CurrentRow.Delete();
                    dt_ICD_PHU.AcceptChanges();
                    grd_ICD.Refetch();
                    grd_ICD.AutoSizeColumns();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin Mã ICD");
                throw;
            }
            finally
            {
                setChanDoan();
            }
        }

        private void cmdKETTHUC_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắn chắn muốn kết thúc lần khám của bệnh nhân không", "Xác nhận",
                                           true))
                {
                    int record = -1;
                    record = new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.Locked).EqualTo(1)
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtPatient_Code.Text).Execute();
                    if (record > 0)
                    {
                        DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                        if (arrDr.Length > 0)
                            arrDr[0]["Locked"] = 1;
                        Utility.ShowMsg("Đã cập nhật thông tin thành công");
                        tabDiagInfo.Enabled = false;
                        cmdInTTDieuTri.Visible = true;
                        cmdInphieuhen.Visible = true;
                        // cmdBenhAnNgoaiTru.Visible = true;
                    }
                    else
                    {
                        Utility.ShowMsg("Chưa lưu được thông tin vào cơ sở dữ liệu");
                    }
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lưu thông tin");
            }
            finally
            {
                ModifyCommmands();
            }
        }

        private void txtTenBenhPhu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTenBenhPhu.TextLength <= 0)
                {
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

        private void cmdInTTDieuTri_Click(object sender, EventArgs e)
        {
            if (grdPresDetail.GetDataRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn cần kê đơn thuốc cho bệnh nhân trước khi thực hiện in tóm tắt điều trị ngoại trú",
                                "Thông báo");
                tabDiagInfo.SelectedTab = tabPageChidinhThuoc;
                return;
            }
            if (IN_TTAT_DTRI_NGOAITRU())
            {
                try
                {
                    DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                    if (arrDr.Length > 0 && objLuotkham.MaDoituongKcb == "DV")
                    {
                        arrDr[0]["Locked"] = 1;
                        objLuotkham.Locked = 1;
                        objLuotkham.NguoiKetthuc = globalVariables.UserName;
                        objLuotkham.NgayKetthuc = globalVariables.SysDate;
                        objLuotkham.Locked = 1;
                    }
                    ActionResult actionResult =
                        _KCB_THAMKHAM.LockExamInfo(objLuotkham);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            ModifyByLockStatus(objLuotkham.Locked);
                            IEnumerable<GridEXRow> query = from kham in grdList.GetDataRows()
                                                           where
                                                               kham.RowType == RowType.Record &&
                                                               Utility.Int32Dbnull(
                                                                   kham.Cells[KcbDangkyKcb.Columns.IdKham].Value) ==
                                                               Utility.Int32Dbnull(txt_idchidinhphongkham.Text)
                                                           select kham;
                            if (query.Count() > 0)
                            {
                                GridEXRow gridExRow = query.FirstOrDefault();
                                gridExRow.Cells[KcbLuotkham.Columns.Locked].Value = (byte?) 1;
                                gridExRow.Cells[KcbLuotkham.Columns.NguoiKetthuc].Value = globalVariables.UserName;
                                gridExRow.Cells[KcbLuotkham.Columns.NgayKetthuc].Value = globalVariables.SysDate;
                                grdList.UpdateData();
                                m_dtDanhsachbenhnhanthamkham.AcceptChanges();
                                grdList.Refetch();
                                Utility.GotoNewRowJanus(grdList, KcbDangkyKcb.Columns.IdKham,
                                                        Utility.sDbnull(txt_idchidinhphongkham.Text));
                                var objStaff =
                                    new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(
                                        Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                                string TenNhanvien = objLuotkham.NguoiKetthuc;
                                if (objStaff != null)
                                    TenNhanvien = objStaff.TenNhanvien;
                                cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 &&
                                                    objLuotkham.Locked.ToString() == "1";
                                cmdUnlock.Enabled = cmdUnlock.Visible &&
                                                    (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                                     objLuotkham.NguoiKetthuc == globalVariables.UserName);
                                if (!cmdUnlock.Enabled)
                                    toolTip1.SetToolTip(cmdUnlock,
                                                        "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " +
                                                        TenNhanvien + "(" + objLuotkham.NguoiKetthuc +
                                                        " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                                else
                                    toolTip1.SetToolTip(cmdUnlock,
                                                        "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                            }
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá lưu thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                }
            }
        }

        private void GetChanDoan(string ICD_chinh, string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                if (ICD_Name.Trim() != "") ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
                if (ICD_Code.Trim() != "") ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
            }
            catch
            {
            }
        }

        private DataTable getChitietCLS()
        {
            try
            {
                var sub_dtData = new DataTable("Temp");
                sub_dtData.Columns.AddRange(new[]
                                                {
                                                    new DataColumn("LOAI_CLS", typeof (string)),
                                                    new DataColumn("KETQUA", typeof (string)),
                                                    new DataColumn("STT", typeof (string))
                                                });

                DataTable temdt = SPs.KcbThamkhamLayketquacls(m_strMaLuotkham).GetDataSet().Tables[0];
                if (!temdt.Columns.Contains("id_dichvu_temp"))
                    temdt.Columns.Add(new DataColumn("id_dichvu_temp", typeof (string)));
                var lstid_dichvu = new List<string>();
                foreach (DataRow dr in temdt.Rows)
                {
                    string service_ID = Utility.sDbnull(dr["id_dichvu"], "");
                    if (service_ID.Trim() == "") service_ID = "0";
                    dr["id_dichvu_temp"] = service_ID;
                    if (!lstid_dichvu.Contains(service_ID)) lstid_dichvu.Add(service_ID);
                }
                string reval = "";
                string NhomCLS = "";
                int STT = 0;
                foreach (string service_ID in lstid_dichvu)
                {
                    STT++;
                    DataRow[] arrChitiet = temdt.Select("id_dichvu_temp='" + service_ID + "'");
                    NhomCLS = "";
                    string kq = "";
                    foreach (DataRow drchitiet in arrChitiet)
                    {
                        if (NhomCLS == "") NhomCLS = Utility.sDbnull(drchitiet["ten_dichvu"], "");
                        kq += Utility.sDbnull(drchitiet["Ten_KQ"], "") + ":" + Utility.sDbnull(drchitiet["Ket_Qua"], "") +
                              " ; ";
                    }
                    if (kq.Length > 0) kq = kq.Substring(0, kq.Length - 2);
                    DataRow newDR = sub_dtData.NewRow();

                    if (service_ID == "0") NhomCLS = "#";
                    newDR["STT"] = STT.ToString();
                    newDR["LOAI_CLS"] = NhomCLS;
                    newDR["KETQUA"] = kq;
                    sub_dtData.Rows.Add(newDR);
                }
                return sub_dtData;
            }
            catch
            {
                return new DataTable();
            }
        }

        public static ReportDocument GetReport(string fileName) //, ref string ErrMsg)
        {
            try
            {
                var crpt = new ReportDocument();
                if (File.Exists(fileName))
                {
                    crpt.Load(fileName);
                }
                else
                {
                    return null;
                }
                return crpt;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi nạp báo cáo " + fileName + "-->\n" + ex.Message);
                //ErrMsg = ex.Message;
                return null;
            }
        }

        private bool IN_TTAT_DTRI_NGOAITRU()
        {
            try
            {
                string ICD_Name = "";
                string ICD_Code = "";
                DataSet dsData = _KCB_THAMKHAM.LaythongtinInphieuTtatDtriNgoaitru(objkcbdangky.IdKham);
                THU_VIEN_CHUNG.CreateXML(dsData);
                string NGAY_KEDON = "";
                string[] arrDate =
                    Utility.sDbnull(
                        dsData.Tables[2].Rows.Count > 0
                            ? dsData.Tables[2].Rows[0]["NGAY_KEDON"]
                            : globalVariables.SysDate.ToString("dd/MM/yyyy"),
                        globalVariables.SysDate.ToString("dd/MM/yyyy")).Split('/');
                NGAY_KEDON = "Ngày " + arrDate[0] + " tháng " + arrDate[1] + " năm " + arrDate[2];
                DataTable v_dtData = dsData.Tables[0];
                DataTable sub_dtData = getChitietCLS();
                THU_VIEN_CHUNG.CreateXML(sub_dtData, "sub_report.xml");
                // new DataTable("Temp");
                if (v_dtData != null && v_dtData.Rows.Count > 0)
                    GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["ICD_CHINH"], ""),
                                Utility.sDbnull(v_dtData.Rows[0]["ICD_PHU"], ""), ref ICD_Name, ref ICD_Code);
                foreach (DataRow dr in v_dtData.Rows)
                {
                    dr["chan_doan"] = Utility.sDbnull(dr["chan_doan"]).Trim() == ""
                                          ? ICD_Name
                                          : Utility.sDbnull(dr["chan_doan"]) + ";" + ICD_Name;
                    //dr[DmucBenh.Columns.MaBenh] = ICD_Code;
                    dr["ma_icd"] = ICD_Code;
                }
                v_dtData.AcceptChanges();
                v_dtData.AcceptChanges();
                Utility.UpdateLogotoDatatable(ref v_dtData);
                string tieude = "", reportname = "",
                reportcode = "";
                reportcode = "thamkham_InPhieutomtatdieutringoaitru_A4";
                ReportDocument crpt = Utility.GetReport("thamkham_InPhieutomtatdieutringoaitru_A4", ref tieude,
                                                        ref reportname);
                if (PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru == Papersize.A5)
                {
                    reportcode = "thamkham_InPhieutomtatdieutringoaitru_A4";
                    crpt = Utility.GetReport("thamkham_InPhieutomtatdieutringoaitru_A5", ref tieude, ref reportname);
                }
                    
                if (crpt == null) return false;
                var objForm = new frmPrintPreview("PHIẾU TÓM TẮT ĐIỀU TRỊ NGOẠI TRÚ", crpt, true, true);
                crpt.SetDataSource(v_dtData);
                crpt.Subreports[0].SetDataSource(sub_dtData);
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportcode;
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "NGAY_KEDON", NGAY_KEDON);
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
               
                //In ngay
                if (cboPrintPreviewTomtatdieutringoaitru.SelectedValue.ToString() == "1")
                    objForm.addTrinhKy_OnFormLoad();
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                           PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0, 1);
                    objForm.ShowDialog();
                }
                else
                {
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    int soluongin = 1;
                    string UserPrintNumberFile = Application.StartupPath + @"\UserPrintNumber\" + globalVariables.UserName + "_" + objForm.mv_sReportFileName + ".txt";
                    soluongin = File.Exists(UserPrintNumberFile) ? Utility.Int32Dbnull(File.ReadAllText(UserPrintNumberFile)) : 1;
                    crpt.PrintToPrinter(soluongin, false, 0, 0);
                }

                List<int> lstKho =
                    dsData.Tables[2].AsEnumerable().Select(c => c.Field<int>("id_kho")).Distinct().ToList();
                foreach (int s in lstKho)
                {
                    DataTable dt = v_dtData.Clone();
                    dt = dsData.Tables[2].Select("id_kho = " + s, "stt_in").CopyToDataTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["chan_doan"] = Utility.sDbnull(dr["chan_doan"]).Trim() == ""
                                              ? ICD_Name
                                              : Utility.sDbnull(dr["chan_doan"]) + ";" + ICD_Name;
                        //dr[DmucBenh.Columns.MaBenh] = ICD_Code;
                        dr["ma_icd"] = ICD_Code;
                    }
                    dt.AcceptChanges();
                    Utility.UpdateLogotoDatatable(ref dt);
                    string reportcode1 = "";
                    reportcode1 = "thamkham_Inphieulinhthuocngoaitru_A4";
                    ReportDocument crpt1 = Utility.GetReport("thamkham_Inphieulinhthuocngoaitru_A4", ref tieude,
                                                             ref reportname);

                    if (PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru == Papersize.A5)
                    {
                        crpt1 = Utility.GetReport("thamkham_Inphieulinhthuocngoaitru_A5", ref tieude, ref reportname);
                        reportcode1 = "thamkham_Inphieulinhthuocngoaitru_A5";
                    }
                      
                    if (crpt1 == null) return false;
                    
                    var objForm1 = new frmPrintPreview("PHIẾU LĨNH THUỐC NGOẠI TRÚ", crpt1, true, true);
                    crpt1.SetDataSource(dt);
                    objForm1.mv_sReportFileName = Path.GetFileName(reportname);
                    objForm1.mv_sReportCode =reportcode1 ;
                    crpt1.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                    crpt1.SetParameterValue("BranchName", globalVariables.Branch_Name);
                    crpt1.SetParameterValue("NGAY_KEDON", NGAY_KEDON);
                    Utility.SetParameterValue(crpt1, "txtTrinhky", Utility.getTrinhky(objForm1.mv_sReportFileName, globalVariables.SysDate));
                    objForm1.crptViewer.ReportSource = crpt1;
                    if (cboPrintPreviewTomtatdieutringoaitru.SelectedValue.ToString() == "1")
                        objForm1.addTrinhKy_OnFormLoad();
                    if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                               PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru))
                    {
                        objForm1.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0, 2);
                        objForm1.ShowDialog();
                        Utility.DefaultNow(this);
                    }
                    else
                    {
                        crpt1.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                        crpt1.PrintToPrinter(1, false, 0, 0);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }
        }

        private void tabDiagInfo_SelectedTabChanged(object sender, TabEventArgs e)
        {
        }

        private void cmdNhapVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham.TrangthaiNoitru > 1)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân đã được điều trị nội trú nên bạn chỉ có thể xem và không được phép sửa các thông tin thăm khám");
                    return;
                }
                var frm = new frm_Nhapvien();
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text, -1);

                frm.objLuotkham = objLuotkham;
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    objLuotkham.IdKhoanoitru = Utility.Int16Dbnull(frm.objLuotkham.IdKhoanoitru);
                    objLuotkham.SoBenhAn = Utility.sDbnull(frm.objLuotkham.SoBenhAn);
                    objLuotkham.TrangthaiNoitru = frm.objLuotkham.TrangthaiNoitru;
                    objLuotkham.NgayNhapvien = frm.objLuotkham.NgayNhapvien;
                    objLuotkham.MotaNhapvien = frm.objLuotkham.MotaNhapvien;
                    DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                    if (arrDr.Length > 0)
                        arrDr[0]["trangthai_noitru"] = objLuotkham.TrangthaiNoitru;
                    cmdNhapVien.Enabled = true;
                    cmdHuyNhapVien.Enabled = true;
                    cmdChuyenPhong.Visible = objLuotkham.TrangthaiNoitru == 0;
                    cmdInTTDieuTri.Visible = objLuotkham.TrangthaiNoitru == 0;
                    cmdInphieuhen.Visible = objLuotkham.TrangthaiNoitru == 0;
                    cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                    cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
                    cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && objLuotkham.Locked.ToString() == "1";
                    cmdUnlock.Enabled = cmdUnlock.Visible &&
                                        (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                         objLuotkham.NguoiKetthuc == globalVariables.UserName);
                    if (!cmdUnlock.Enabled)
                        toolTip1.SetToolTip(cmdUnlock,
                                            "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ Quản trị hệ thống");
                    else
                        toolTip1.SetToolTip(cmdUnlock,
                                            "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                }
            }
            catch (Exception)
            {
                // throw;
            }
            finally
            {
                ModifyCommmands();
            }
        }


        private bool IsValidHuyNhapVien()
        {
            SqlQuery query = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(KcbLuotkham.Columns.TrangthaiNoitru).IsLessThanOrEqualTo(0);
            if (query.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg, "Bệnh nhân ngoại trú,Bạn không thể thực hiện chức năng này", true);
                // cmdCancelNhapVien.Focus();
                return false;
            }
            var objNoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
                .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteAsCollection
                <NoitruPhanbuonggiuongCollection>();
            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count > 1)
            {
                Utility.SetMsg(lblMsg,
                               "Bệnh nhân đã chuyển khoa hoặc phân buồng giường, Bạn không thể hủy thông tin nhập viện",
                               true);
                return false;
            }


            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count == 1 &&
                Utility.Int32Dbnull(objNoitruPhanbuonggiuong[0].IdBuong, -1) > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã phân buồng giường,Bạn không thể xóa thông tin ", "Thông báo",
                                MessageBoxIcon.Warning);
                return false;
            }

            SqlQuery sqlQuery2 = new Select().From(NoitruPhieudieutri.Schema)
                .Where(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(m_strMaLuotkham, ""));
            if (sqlQuery2.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg,
                               "Bệnh nhân đã có phiếu điều trị, Bạn không thể xóa hoặc hủy nhập viện được,yêu cầu xem lại",
                               true);
                return false;
            }
            var _NoitruTamung = new Select().From(NoitruTamung.Schema)
                .Where(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteSingle<NoitruTamung>();
            if (_NoitruTamung != null)
            {
                Utility.ShowMsg("Bệnh nhân này đã đóng tiền tạm ứng , Bạn không thể hủy nhập viện", "Thông báo",
                                MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void cmdHuyNhapVien_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (!IsValidHuyNhapVien()) return;
                if (
                    Utility.AcceptQuestion(
                        "Bạn có muốn hủy thông tin nhập viện của bệnh nhân này không,Bệnh nhân sẽ quay lại trạng thái ngoại trú",
                        "Thông báo", true))
                {
                    if (new noitru_nhapvien().Huynhapvien(objLuotkham) == ActionResult.Success)
                    {
                        DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                        if (arrDr.Length > 0)
                            arrDr[0]["trangthai_noitru"] = 0;

                        objLuotkham.TrangthaiNoitru = 0;
                        objLuotkham.NgayNhapvien = null;
                        objLuotkham.MotaNhapvien = "";
                        cmdNhapVien.Enabled = true;
                        cmdHuyNhapVien.Enabled = false;
                        objLuotkham.SoBenhAn = string.Empty;
                        objLuotkham.IdKhoanoitru = -1;
                        cmdChuyenPhong.Visible = objLuotkham.TrangthaiNoitru == 0;
                        cmdInTTDieuTri.Visible = objLuotkham.TrangthaiNoitru == 0;
                        cmdInphieuhen.Visible = objLuotkham.TrangthaiNoitru == 0;
                        var objStaff =
                            new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(
                                Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                        string TenNhanvien = objLuotkham.NguoiKetthuc;
                        if (objStaff != null)
                            TenNhanvien = objStaff.TenNhanvien;
                        cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && objLuotkham.Locked.ToString() == "1";
                        cmdUnlock.Enabled = cmdUnlock.Visible &&
                                            (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                             objLuotkham.NguoiKetthuc == globalVariables.UserName);
                        if (!cmdUnlock.Enabled)
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " +
                                                TenNhanvien + "(" + objLuotkham.NguoiKetthuc +
                                                " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                        else
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");

                        cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                        cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
                        Utility.SetMsg(lblMsg, "Bệnh nhân đã quay lại trạng thái ngoại trú", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommmands();
            }
        }

        private void txtMaBenhphu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (hasMorethanOne)
                    {
                        DSACH_ICD(txtMaBenhphu, DmucChung.Columns.Ma, 1);
                        txtMaBenhphu.SelectAll();
                    }
                    else
                    {
                        cmdAddMaBenhPhu_Click(cmdAddMaBenhPhu, new EventArgs());
                        txtMaBenhphu.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        private void radChuaKham_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int Status = radChuaKham.Checked ? 0 : 1;
                m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
                m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "trang_thai=" + Status;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
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
            if (!IsValidDeleteSelectedDrug()) return;
            PerformActionDeleteSelectedDrug();
            ModifyCommmands();
        }

        private void mnuDeleteCLS_Click(object sender, EventArgs e)
        {
            if (!IsValidSelectedAssignDetail()) return;
            PerforActionDeleteSelectedCLS();
            ModifyCommmands();
        }

        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveDefaultPrinter();
        }

        private void LoadLaserPrinters()
        {
            if (string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInBienlai))
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.GetDefaultPrinter();
                m_strDefaultLazerPrinterName = Utility.sDbnull(PropertyLib._MayInProperties.TenMayInBienlai);
            }
            if (PropertyLib._ThamKhamProperties != null)
            {
                try
                {
                    //khoi tao may in
                    String pkInstalledPrinters;
                    cboLaserPrinters.Items.Clear();
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                        cboLaserPrinters.Items.Add(pkInstalledPrinters);
                    }
                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }
                finally
                {
                    m_strDefaultLazerPrinterName = Utility.sDbnull(PropertyLib._MayInProperties.TenMayInBienlai);

                    cboLaserPrinters.Text = m_strDefaultLazerPrinterName;
                }
            }
        }

        private void SaveDefaultPrinter()
        {
            try
            {
                PropertyLib._MayInProperties.TenMayInBienlai = Utility.sDbnull(cboLaserPrinters.Text);
                PropertyLib.SaveProperty(PropertyLib._MayInProperties);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }

        private void Try2CreateFolder()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(strSaveandprintPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strSaveandprintPath));
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
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
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
            var frm = new frm_Properties(PropertyLib._HISQMSProperties);
            frm.ShowDialog();
            CauHinhQMS();
        }

        private void txtNhietDo_Click(object sender, EventArgs e)
        {
        }

        private void txtKhoaNoiTru_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (!string.IsNullOrEmpty(txtKhoaNoiTru.Text))
            //    {
            //        DataRow query = (from khoa in m_dtKhoaNoiTru.AsEnumerable().Cast<DataRow>()
            //                         let y = Utility.sDbnull(khoa[DmucKhoaphong.Columns.TenKhoaphong])
            //                         let z = Utility.sDbnull(khoa[DmucKhoaphong.Columns.IdKhoaphong])
            //                         where
            //                             Utility.Int32Dbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) ==
            //                             Utility.Int32Dbnull(txtKhoaNoiTru.Text)
            //                         select khoa).FirstOrDefault();
            //        if (query != null)
            //        {
            //            txtKhoaNoiTru.Text = Utility.sDbnull(query[DmucKhoaphong.Columns.TenKhoaphong]);
            //            txtIdKhoaNoiTru.Text = Utility.sDbnull(query[DmucKhoaphong.Columns.IdKhoaphong]);
            //            cmdSave.Focus();
            //        }
            //        else
            //        {
            //            TimKiemKhoaNoiTru();
            //        }
            //    }
            //    else
            //    {
            //        TimKiemKhoaNoiTru();
            //    }
            //}
            //if (e.KeyCode == Keys.F3)
            //{
            //    TimKiemKhoaNoiTru();
            //}
        }

        private void TimKiemKhoaNoiTru()
        {
        }

        private void txtDeparmentID_TextChanged(object sender, EventArgs e)
        {
            //    EnumerableRowCollection<string> query = from khoa in m_dtKhoaNoiTru.AsEnumerable()
            //                                            let y = Utility.sDbnull(khoa[DmucKhoaphong.Columns.TenKhoaphong])
            //                                            where
            //                                                Utility.Int32Dbnull(khoa[DmucKhoaphong.Columns.IdKhoaphong]) ==
            //                                                Utility.Int32Dbnull(txtIdKhoaNoiTru.Text)
            //                                            select y;
            //    if (query.Any())
            //    {
            //        txtKhoaNoiTru.Text = Utility.sDbnull(query.FirstOrDefault());
            //    }
            //    else
            //    {
            //        txtKhoaNoiTru.Text = string.Empty;
            //    }
        }

        private void nmrSongayDT_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            //{
            //    dtNgayNhapVien.Focus();
            //}
        }

        private void dtNgayNhapVien_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            //{
            //    txtKhoaNoiTru.Focus();
            //    txtKhoaNoiTru.SelectAll();

            //}
        }

        private void cmdTimKiemKhoaNoiTru_Click(object sender, EventArgs e)
        {
            TimKiemKhoaNoiTru();
        }

        private void cmdInphieuhen_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtphienhen =
                    SPs.KcbThamkhamInphieuhenBenhnhan(m_strMaLuotkham, Utility.Int16Dbnull(txtPatient_ID.Text, -1)).
                        GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(dtphienhen, "thamkham_inphieuhen_benhnhan.xml");
                KCB_INPHIEU.INPHIEU_HEN(dtphienhen, "PHIẾU HẸN KHÁM");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        #region "chỉ định cận lâm sàng"

        private DataTable m_dtReportAssignInfo;

        /// <summary>
        /// hàm thực hiện việc update thông tin của cell update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                //if (e.Column.Key == "Ghi_Chu")
                //{
                //    string GhiChu = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.MotaThem), "");

                //    new Update(KcbChidinhclsChitiet.Schema)
                //        .Set(KcbChidinhclsChitiet.Columns.MotaThem).EqualTo(GhiChu)
                //        .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                //        .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                //        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(
                //            Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChitietchidinh))).Execute
                //        ();
                //    grdAssignDetail.CurrentRow.BeginEdit();
                //    grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NgaySua].Value = globalVariables.SysDate;
                //    grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiSua].Value = globalVariables.UserName;
                //    grdAssignDetail.CurrentRow.EndEdit();
                //}
            }
            catch (Exception exception)
            {
            }
        }

        /// <summary>
        /// hàm thực hiện việc xóa thông tin chỉ định cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelteAssign_Click(object sender, EventArgs e)
        {
            if (!IsValidCheckedAssignDetails()) return;
            PerforActionDeleteAssign();
            ModifyCommmands();
        }

        private bool IsValidCheckedAssignDetails()
        {
            bool b_Cancel = false;
            if (grdAssignDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định CLS", "Thông báo",
                                MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suaphieuchidinhcls") ||
                    Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                    globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg(
                        "Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                    return false;
                }
            }
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdSave.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int id_chidinhchitiet =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                        -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(id_chidinhchitiet)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
               
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            b_Cancel = false;
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int id_chidinhchitiet =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                        -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(id_chidinhchitiet)
                    .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg(
                    "Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }

        /// <summary>
        /// hàm thực hiện viễ xóa thông tin chỉ định
        /// </summary>
        private void PerforActionDeleteAssign()
        {
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int id_chidinhchitiet =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                        -1);
                int id_chidinh = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                                     -1);
                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(id_chidinhchitiet);
                gridExRow.Delete();
                m_dtAssignDetail.AcceptChanges();
            }
        }

        private void PerforActionDeleteSelectedCLS()
        {
            try
            {
                int id_chidinhchitiet =
                    Utility.Int32Dbnull(
                        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                        -1);
                int id_chidinh =
                    Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                        -1);
                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(id_chidinhchitiet);
                grdAssignDetail.CurrentRow.Delete();
                m_dtAssignDetail.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Bạn nên xóa CLS bằng cách chọn và nhấn nút Xóa CLS-->" + ex.Message);
            }
        }

        private bool IsValidUpdateChidinh()
        {
            if (!Utility.isValidGrid(grdAssignDetail)) return false;
            int id_chidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(id_chidinh).And(
                    KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsGreaterThanOrEqualTo(1)
                    .Or(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1)
                    .And(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(id_chidinh);
           
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu này đã thanh toán hoặc chuyển đã cận \n Mời bạn thêm phiếu mới để thực hiện", "Thông báo");
                cmdInsertAssign.Focus();
                return false;
            }

            SqlQuery sqlQueryKq = new Select().From(KcbKetquaCl.Schema)
                .Where(KcbKetquaCl.Columns.MaChidinh).IsEqualTo(id_chidinh);

            if (sqlQueryKq.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu này đã có kết quả, Mời bạn Thêm phiếu mới để thực hiện", "Thông báo");
                cmdInsertAssign.Focus();
                return false;
            }


            return true;
        }

        /// <summary>
        /// hàm thực hiện viêc jsu thôn gtin chỉ dịnh cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidUpdateChidinh()) return;
                var frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.HosStatus = 0;
                frm.ObjRegExam = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text, -1);
                frm.objLuotkham = objLuotkham; // CreatePatientExam();
                frm.objBenhnhan = objBenhnhan;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = Utility.sDbnull(
                    grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    Laythongtinchidinhngoaitru();
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg("Lỗi trong quá trình sửa phiếu :" + e);
                }
                //throw;
            }
            finally
            {
                ModifyCommmands();
            }
        }

        private bool CheckPatientSelected()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg(
                    "Bạn phải chọn Bệnh nhân trước khi thực hiện các công việc chỉ định Thăm khám, CLS, Kê đơn");
                return false;
            }
            if (objkcbdangky == null)
            {
                Utility.ShowMsg(
                    "Bệnh nhân bạn chọn chưa đăng ký phòng khám nên không được phép thăm khám. Mời bạn kiểm tra lại");
                return false;
            }
            return true;
        }

        /// <summary>
        /// hàm thực hiện việc thêm mới thông itn 
        /// của phần chính định
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsertAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckPatientSelected()) return;
                if (Utility.Coquyen("quyen_suaphieuchidinhcls") ||
                    Utility.Int32Dbnull(objkcbdangky.IdBacsikham, -1) <= 0 ||
                    objkcbdangky.IdBacsikham == globalVariables.gv_intIDNhanvien)
                {
                }
                else
                {
                    Utility.ShowMsg(
                        string.Format(
                            "Bệnh nhân này đã được khám bởi Bác sĩ khác nên bạn không được phép thêm phiếu chỉ định dịch vụ "));
                    return;
                }
                if (!cmdInsertAssign.Enabled) return;
                var frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.txtAssign_ID.Text = "-100";
                frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text, -1);
                frm.objLuotkham = objLuotkham; // CreatePatientExam();
                frm.objBenhnhan = objBenhnhan;
                frm.ObjRegExam = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                frm.m_eAction = action.Insert;
                frm.txtAssign_ID.Text = "-1";
                frm.HosStatus = 0;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    Laythongtinchidinhngoaitru();
                    if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                        grdAssignDetail.GroupMode = GroupMode.Collapsed;
                    Utility.GotoNewRowJanus(grdAssignDetail, KcbChidinhclsChitiet.Columns.IdChidinh,
                                            frm.txtAssign_ID.Text);
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                ModifyCommmands();
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        /// <summary>
        /// hàm thực hiện việc in phiếu chỉ định cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrintAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string mayin = "";
                int v_AssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh),-1);
                string service_Code = "";
                string v_AssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                List<string> nhomcls = new List<string>();
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetDataRows())
                {
                    if (!nhomcls.Contains(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value))) 
                        nhomcls.Add(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value));
                }
                string nhomincls = "ALL";
                if (cboServicePrint.SelectedIndex > 0)
                {
                    nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");
                    switch (cboServicePrint.SelectedIndex)
                    {
                        case 1:
                            service_Code = "";
                            break;
                    }
                }
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTACHTOANBO_CLS", "0", true) == "1" && chkIntach.Checked && cboServicePrint.SelectedIndex <= 0)
                {
                    KCB_INPHIEU.InTachToanBoPhieuCLS((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, v_AssignId,
                                             v_AssignCode, nhomcls, cboServicePrint.SelectedIndex, chkIntach.Checked,
                                             ref mayin);
                }
                else
                {
                    KCB_INPHIEU.InphieuChidinhCLS((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, v_AssignId,
                                             v_AssignCode, nhomincls, cboServicePrint.SelectedIndex, chkIntach.Checked,
                                             ref mayin);
                }
               
                if (mayin != "") cboLaserPrinters.Text = mayin;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        private decimal GetTotalDatatable(DataTable dataTable, string FiledName, string Filer)
        {
            return Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + FiledName + ")", Filer), 0);
        }


        private void GetPatienInfoAddreport(ref DataTable dtReportPhieuXetNghiem)
        {
            // Kiểm tra và cộng cột
            // Kiểm tra và cộng cột
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.DiaChi))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.DiaChi, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.NamSinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.NamSinh, typeof (int));


            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.TenBenhnhan))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.TenBenhnhan, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.GioiTinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.GioiTinh, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Tuoi"))
                dtReportPhieuXetNghiem.Columns.Add("Tuoi", typeof (int));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.GioiTinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.GioiTinh, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Rank_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Rank_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Position_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Position_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Unit_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Unit_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("chan_doan"))
                dtReportPhieuXetNghiem.Columns.Add("chan_doan", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(DmucDoituongkcb.Columns.TenDoituongKcb))
                dtReportPhieuXetNghiem.Columns.Add(DmucDoituongkcb.Columns.TenDoituongKcb, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("mathe_bhyt"))
                dtReportPhieuXetNghiem.Columns.Add("mathe_bhyt", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Barcode"))
                dtReportPhieuXetNghiem.Columns.Add("Barcode", typeof (byte[]));
            byte[] byteArray = Utility.GenerateBarCode(barcode);
            foreach (DataRow dr in dtReportPhieuXetNghiem.Rows)
            {
                dr["Barcode"] = byteArray;
                dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = txtPatient_Name.Text;

                dr[KcbLuotkham.Columns.DiaChi] = txtDiaChi.Text;
                dr[KcbDanhsachBenhnhan.Columns.NamSinh] = objBenhnhan.NgaySinh.Value.Year.ToString();

                dr["Tuoi"] = globalVariables.SysDate.Year - objBenhnhan.NgaySinh.Value.Year;
                dr["gioi_tinh"] = txtGioitinh.Text;
                dr["gioi_tinh"] = txtGioitinh.Text;
                // dr["Rank_Name"] = txtRank.Text;
                //dr["Position_Name"] = txtPosition.Text;
                //dr["Unit_Name"] = unitName;
                dr["chan_doan"] = txtChanDoan.Text;
                dr[DmucDoituongkcb.Columns.TenDoituongKcb] = txtObjectType_Name.Text;
                dr["mathe_bhyt"] = txtSoBHYT.Text;
            }
            dtReportPhieuXetNghiem.AcceptChanges();
        }

        #endregion

        #region "khởi tạo các sụ kienj thông tin của thuốc"

        private bool ExistsDonThuoc()
        {
            try
            {
                string _kenhieudon = THU_VIEN_CHUNG.Laygiatrithamsohethong("KE_NHIEU_DON", "N", true);
                var lstPres =
                    new Select()
                        .From(KcbDonthuoc.Schema)
                        .Where(KcbDonthuoc.MaLuotkhamColumn).IsEqualTo(Utility.sDbnull(objLuotkham.MaLuotkham))
                        .And(KcbDonthuoc.IdBenhnhanColumn).IsEqualTo(Utility.sDbnull(objLuotkham.IdBenhnhan)).
                        ExecuteAsCollection<KcbDonthuocCollection>();

                IEnumerable<KcbDonthuoc> lstPres1 = from p in lstPres
                                                    where p.IdKham == objkcbdangky.IdKham
                                                    select p;
                if (objLuotkham.MaDoituongKcb == "BHYT")
                {
                    if (_kenhieudon == "Y" && lstPres1.Count() <= 0) //Được phép kê mỗi phòng khám 1 đơn thuốc
                        return false;
                    if (_kenhieudon == "N" && lstPres.Count > 0 && lstPres1.Count() <= 0)
                        //Cảnh báo ko được phép kê đơn tiếp
                    {
                        Utility.ShowMsg(
                            "Chú ý: Bệnh nhân này thuộc đối tượng BHYT và đã được kê đơn thuốc tại phòng khám khác. Bạn cần trao đổi với Quản trị hệ thống để được cấu hình kê đơn thuốc tại nhiều phòng khác khác nhau với đối tượng BHYT này",
                            "Thông báo");
                        return false;
                    }
                }
                else
                    //Bệnh nhân dịch vụ-->cho phép kê 1 đơn nếu đơn chưa thanh toán và nhiều đơn nếu các đơn trước đã thanh toán
                {
                    if (lstPres1.Count() > 0)
                        if (lstPres1.FirstOrDefault().TrangthaiThanhtoan == 0) //Chưa thanh toán-->Cần sửa đơn
                            return true;
                        else //Đã thanh toán-->Cho phép thêm đơn mới
                            return false;
                    return false;
                }
                return lstPres.Count > 0;
                //Tạm thời rem lại do vẫn có BN kê được >1 đơn thuốc
                //var query = from thuoc in grdPresDetail.GetDataRows().AsEnumerable()
                //                    select thuoc;
                //if (query.Any()) return true;
                //else return false;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi kiểm tra số lượng đơn thuốc của lần khám\n" + ex.Message);
                return false;
            }
        }

        private void cmdCreateNewPres_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdCreateNewPres.Enabled) return;
            if (Utility.Coquyen("quyen_suadonthuoc") || Utility.Int32Dbnull(objkcbdangky.IdBacsikham, -1) <= 0 ||
                objkcbdangky.IdBacsikham == globalVariables.gv_intIDNhanvien)
            {
            }
            else
            {
                Utility.ShowMsg(
                    string.Format(
                        "Bệnh nhân này đã được khám bởi Bác sĩ khác nên bạn không được phép thêm đơn thuốc cho Bệnh nhân"));
                return;
            }

            if (!ExistsDonThuoc())
            {
                ThemMoiDonThuoc();
            }
            else
            {
                UpdateDonThuoc();
            }
        }

        private void ThemMoiDonThuoc()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                var frm = new frm_KCB_KE_DONTHUOC("THUOC");
                frm.em_Action = action.Insert;
                frm.objLuotkham = objLuotkham;
                frm._KcbChandoanKetluan = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.Text;
                frm._Chandoan = txtChanDoan.Text;
                frm.dt_ICD = dt_ICD;
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objRegExam = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.Text = frm._MabenhChinh;
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbChandoanKetluan != null) _KcbChandoanKetluan = frm._KcbChandoanKetluan;
                    Laythongtinchidinhngoaitru();

                    Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                ModifyCommmands();
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        private void setChanDoan()
        {
            return;
            //string _value = txtTenBenhChinh.Text.Trim() + ";";
            //try
            //{
            //    foreach (DataRow dr in dt_ICD_PHU.Rows)
            //        _value += dr[DmucBenh.Columns.TenBenh].ToString() + ";";
            //    txtChanDoan.Text = _value.Substring(0, _value.Length - 1);
            //}
            //catch
            //{
            //}
        }

        /// <summary>
        /// hàm thực hiện việc kê đơnt huốc theo đối tượng
        /// </summary>
        private void KeDonThuocTheoDoiTuong()
        {
            //KcbLuotkham objPatientExam = CreatePatientExam();
            //if (objPatientExam != null)
            //{
            //    var frm = new frm_DM_CreateNewPres_V2();
            //    frm.Text = string.Format("Kê đơn thuốc  ngoại trú  theo đối tượng -{0}-{1}-{2}", txtTenBN.Text,
            //                             Utility.sDbnull(radNam.Checked ? "Nam" : "Nữ"),
            //                             Utility.sDbnull(txtYear_Of_Birth.Text));
            //    frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
            //    frm.em_CallAction = CallAction.FromMenu;
            //    frm.MaDoituongKcb = Utility.sDbnull(objPatientExam.MaDoituongKcb, "");
            //    frm.objPatientExam = CreatePatientExam();
            //    frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text, -1);
            //    frm.em_CallAction = CallAction.FromMenu;
            //    frm.radTheoDoiTuong.Checked = true;
            //    frm.radTrongGoi.Visible = false;
            //    frm.txtPres_ID.Text = "-1";
            //    frm.em_Action = action.Insert;
            //    frm.TrangThai = 0;
            //    frm.PreType = 0;
            //    frm.ShowDialog();
            //    if (frm.b_Cancel)
            //    {
            //        Laythongtinchidinhngoaitru();
            //        Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc,
            //                                Utility.sDbnull(frm.txtPres_ID.Text));
            //        ModifyCommmands();
            //    }
            //}
        }

        /// <summary>
        /// ham thực hiện việc update thông tin của thuốc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdatePres_Click(object sender, EventArgs e)
        {
            if (!cmdUpdatePres.Enabled) return;
            UpdateDonThuoc();
        }

        private bool Donthuoc_DangXacnhan(int pres_id)
        {
            var _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(pres_id).And(
                    KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null) return true;
            return false;
        }

        private void UpdateDonThuoc()
        {
            try
            {
                if (grdPresDetail.CurrentRow != null && grdPresDetail.CurrentRow.RowType == RowType.Record)
                {
                    if (objLuotkham != null)
                    {
                        int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                        if (Donthuoc_DangXacnhan(Pres_ID))
                        {
                            Utility.ShowMsg(
                                "Đơn thuốc này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát thuốc tại phòng Dược");
                            return;
                        }
                        var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                            .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                            .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                            .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        if (v_collect.Count > 0)
                        {
                            Utility.ShowMsg(
                                "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại đơn thuốc Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp thuốc để hủy xác nhận đơn thuốc tại kho thuốc");
                            return;
                        }
                        KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                        if (objPrescription != null)
                        {
                            var frm = new frm_KCB_KE_DONTHUOC("THUOC");
                            frm.em_Action = action.Update;
                            frm._KcbChandoanKetluan = _KcbChandoanKetluan;
                            frm._MabenhChinh = txtMaBenhChinh.Text;
                            frm._Chandoan = txtChanDoan.Text;
                            frm.dt_ICD = dt_ICD;
                            frm.dt_ICD_PHU = dt_ICD_PHU;
                            frm.noitru = 0;
                            frm.objLuotkham = objLuotkham;
                            frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                            frm.objRegExam = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txtReg_ID.Text));
                            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                            frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                            frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                txtMaBenhChinh.Text = frm._MabenhChinh;
                                txtChanDoan._Text = frm._Chandoan;
                                dt_ICD_PHU = frm.dt_ICD_PHU;
                                if (frm._KcbChandoanKetluan != null) _KcbChandoanKetluan = frm._KcbChandoanKetluan;
                                Laythongtinchidinhngoaitru();
                                Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                        Utility.sDbnull(frm.txtPres_ID.Text));
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
            }
        }

        private List<int> GetIdChitiet(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr =
                m_dtPresDetail.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " +
                                      KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                                      + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
                IEnumerable<string> p1 = (from q in arrDr.AsEnumerable()
                                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                s = string.Join(",", p1.ToArray());
                IEnumerable<int> p = (from q in arrDr.AsEnumerable()
                                      select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                return p.ToList();
            }
            return new List<int>();
        }

        private void deletefromDatatable(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                DataRow[] p = (from q in m_dtPresDetail.Select("1=1").AsEnumerable()
                               where
                                   lstIdChitietDonthuoc.Contains(
                                       Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                               select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtPresDetail.Rows.Remove(p[i]);
                m_dtPresDetail.AcceptChanges();
            }
            catch
            {
            }
        }

        private void PerformActionDeletePres()
        {
            string s = "";
            var lstIdchitiet = new List<int>();
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                List<int> _temp = GetIdChitiet(IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdPresDetail.UpdateData();
            }
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            deletefromDatatable(lstIdchitiet);
            m_dtPresDetail.AcceptChanges();
        }

        private void PerformActionDeletePres_old()
        {
            string s = "";
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                int Pres_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                                  -1);
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                                       -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(v_intIDDonthuoc);
                gridExRow.Delete();
                grdPresDetail.UpdateData();
                m_dtPresDetail.AcceptChanges();
            }
        }

        private void PerformActionDeleteSelectedDrug()
        {
            try
            {
                int Pres_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                        -1);
                int v_IdChitietdonthuoc =
                    Utility.Int32Dbnull(
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                        -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(v_IdChitietdonthuoc);
                grdPresDetail.CurrentRow.Delete();
                grdPresDetail.UpdateData();
                m_dtPresDetail.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message + "-->" +
                                "Bạn nên dùng chức năng xóa thuốc bằng cách chọn thuốc và sử dụng nút xóa thuốc");
            }
        }

        private bool KiemtraThuocTruockhixoa()
        {
            bool b_Cancel = false;
            if (grdPresDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }

            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suadonthuoc") ||
                    Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                    globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg(
                        "Trong các thuốc bạn chọn xóa, có một số thuốc được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các thuốc do chính bạn kê để thực hiện xóa");
                    return false;
                }
            }
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int v_IdChitietdonthuoc =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int v_intIDThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                    KcbDonthuocChitiet _KcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(v_IdChitietdonthuoc);
                    if (_KcbDonthuocChitiet != null &&
                        (Utility.Byte2Bool(_KcbDonthuocChitiet.TrangthaiThanhtoan) ||
                         Utility.Byte2Bool(_KcbDonthuocChitiet.TrangThai)))
                    {
                        b_Cancel = true;
                        break;
                    }
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg(
                    "Một số thuốc bạn chọn đã thanh toán hoặc đã phát thuốc cho Bệnh nhân nên bạn không được phép xóa. Mời bạn kiểm tra lại ",
                    "Thông báo",
                    MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

        private bool IsValidSelectedAssignDetail()
        {
            bool b_Cancel = false;
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdSave.Focus();
                return false;
            }

            if (grdAssignDetail.RowCount <= 0 || grdAssignDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định CLS", "Thông báo",
                                MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }

            if (Utility.Coquyen("quyen_suaphieuchidinhcls") ||
                Utility.sDbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                globalVariables.UserName)
            {
            }
            else
            {
                Utility.ShowMsg(
                    "Chỉ định đang chọn xóa được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                return false;
            }

            int id_chidinhchitiet =
                Utility.Int32Dbnull(
                    grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                    -1);
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(id_chidinhchitiet)
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                b_Cancel = true;
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }

            id_chidinhchitiet =
                Utility.Int32Dbnull(
                    grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                    -1);
            sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(id_chidinhchitiet)
                .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                b_Cancel = true;
            }

            if (b_Cancel)
            {
                Utility.ShowMsg(
                    "Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }


        private bool IsValidDeleteSelectedDrug()
        {
            bool b_Cancel = false;
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdSave.Focus();
                return false;
            }
            if (grdPresDetail.RowCount <= 0 || grdPresDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một thuốc để xóa ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            if (Utility.Coquyen("quyen_suadonthuoc") ||
                Utility.sDbnull(grdPresDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                globalVariables.UserName)
            {
            }
            else
            {
                Utility.ShowMsg(
                    "Thuốc đang chọn xóa được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các thuốc do chính bạn kê để thực hiện xóa");
                return false;
            }

            if (grdPresDetail.CurrentRow.RowType == RowType.Record)
            {
                int v_IdChitietdonthuoc =
                    Utility.Int32Dbnull(
                        grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                        -1);
                KcbDonthuocChitiet _KcbDonthuocChitiet = KcbDonthuocChitiet.FetchByID(v_IdChitietdonthuoc);
                if (_KcbDonthuocChitiet != null &&
                    (Utility.Byte2Bool(_KcbDonthuocChitiet.TrangthaiThanhtoan) ||
                     Utility.Byte2Bool(_KcbDonthuocChitiet.TrangThai)))
                {
                    b_Cancel = true;
                }
            }

            if (b_Cancel)
            {
                Utility.ShowMsg(
                    "Một số thuốc bạn chọn đã thanh toán hoặc đã phát thuốc cho Bệnh nhân nên bạn không được phép xóa. Mời bạn kiểm tra lại ",
                    "Thông báo",
                    MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

        private void cmdDeletePres_Click(object sender, EventArgs e)
        {
            if (!KiemtraThuocTruockhixoa()) return;
            PerformActionDeletePres();
            ModifyCommmands();
        }

        /// <summary>
        /// ham thực hiện việc in phiếu thuôc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrintPres_Click(object sender, EventArgs e)
        {
            try
            {
                //frm_YHHQ_IN_DONTHUOC frm = new frm_YHHQ_IN_DONTHUOC();
                //frm.TrongGoi = 0;
                //frm.Exam_ID = Utility.Int32Dbnull(txtExam_ID.Text);
                //frm.Text = string.Format("In đơn thuốc  ngoại trú  theo đối tượng -{0}-{1}-{2}", txtTenBN.Text,
                //     Utility.sDbnull(radNam.Checked ? "Nam" : "Nữ"), Utility.sDbnull(txtYear_Of_Birth.Text));
                //frm.CallActionInDonThuoc = CallActionInDonThuoc.Exam_ID;
                //frm.ShowDialog();
                int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                PrintPres(Pres_ID, "");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                // throw;
            }
        }

        /// <summary>
        /// hàm thực hiện việc in đơn thuốc
        /// </summary>
        /// <param name="PresID"></param>
        private void PrintPres(int PresID, string forcedTitle)
        {
            DataTable v_dtData = _KCB_KEDONTHUOC.LaythongtinDonthuoc_In(PresID);
            Utility.AddColumToDataTable(ref v_dtData, "BarCode", typeof (byte[]));
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            //barcode.Data = Utility.sDbnull(Pres_ID);
            byte[] Barcode = Utility.GenerateBarCode(barcode);
            string ICD_Name = "";
            string ICD_Code = "";
            if (v_dtData != null && v_dtData.Rows.Count > 0)
                GetChanDoan(Utility.sDbnull(v_dtData.Rows[0]["mabenh_chinh"], ""),
                            Utility.sDbnull(v_dtData.Rows[0]["mabenh_phu"], ""), ref ICD_Name, ref ICD_Code);

            foreach (DataRow drv in v_dtData.Rows)
            {
                drv["BarCode"] = Barcode;
                drv["chan_doan"] = Utility.sDbnull(drv["chan_doan"]).Trim() == ""
                                       ? ICD_Name
                                       : Utility.sDbnull(drv["chan_doan"]) + ";" + ICD_Name;
                drv["ma_icd"] = ICD_Code;
            }
            THU_VIEN_CHUNG.CreateXML(v_dtData, "thamkham_InDonthuocA4.xml");
            v_dtData.AcceptChanges();
            // log.Info("Thuc hien in don thuoc");
            Utility.UpdateLogotoDatatable(ref v_dtData);
            string KhoGiay = "A5";
            if (PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4) KhoGiay = "A4";
            var reportDocument = new ReportDocument();
            string tieude = "", reportname = "", reportCode = "";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonthuocA4";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA4", ref tieude, ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5", ref tieude, ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            Utility.WaitNow(this);
            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", "ĐƠN THUỐC");
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai,
                                           PropertyLib._MayInProperties.PreviewInDonthuoc))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                    cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }

        #endregion

        #region "Xử lý tác vụ của phần lưu thông tin "

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }

        private string GetDanhsachBenhphu()
        {
            var sMaICDPHU = new StringBuilder("");
            try
            {
                int recordRow = 0;

                if (dt_ICD.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_ICD_PHU.Rows)
                    {
                        if (recordRow > 0)
                            sMaICDPHU.Append(",");
                        sMaICDPHU.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                        recordRow++;
                    }
                }
                return sMaICDPHU.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// HÀM KHƠI TẠO PHẦN CHỈ ĐỊNH CHUẨN ĐOÁN
        /// </summary>
        /// <returns></returns>
        private KcbChandoanKetluan TaoDulieuChandoanKetluan()
        {
            try
            {
                if (_KcbChandoanKetluan == null)
                {
                    _KcbChandoanKetluan = new KcbChandoanKetluan();
                    _KcbChandoanKetluan.IsNew = true;
                }
                else
                {
                    _KcbChandoanKetluan.IsNew = false;
                    _KcbChandoanKetluan.MarkOld();
                }
                _KcbChandoanKetluan.IdKham = Utility.Int64Dbnull(txtExam_ID.Text, -1);
                _KcbChandoanKetluan.MaLuotkham = Utility.sDbnull(m_strMaLuotkham, "");
                _KcbChandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(txtPatient_ID.Text, "-1");
                _KcbChandoanKetluan.MabenhChinh = Utility.sDbnull(txtMaBenhChinh.Text, "");
                _KcbChandoanKetluan.Nhommau = txtNhommau.Text;
                _KcbChandoanKetluan.Nhietdo = Utility.sDbnull(txtNhietDo.Text);
                _KcbChandoanKetluan.TrieuchungBandau = txtTrieuChungBD.Text;
                _KcbChandoanKetluan.Huyetap = txtHa.Text;
                _KcbChandoanKetluan.Mach = txtMach.Text;
                _KcbChandoanKetluan.Nhiptim = Utility.sDbnull(txtNhipTim.Text);
                _KcbChandoanKetluan.Nhiptho = Utility.sDbnull(txtNhipTho.Text);
                _KcbChandoanKetluan.Chieucao = Utility.sDbnull(txtChieucao.Text);
                _KcbChandoanKetluan.Cannang = Utility.sDbnull(txtCannang.Text);
                _KcbChandoanKetluan.HuongDieutri = txtHuongdieutri.Text; //.myCode.Trim();
                _KcbChandoanKetluan.SongayDieutri = (Int16) Utility.DecimaltoDbnull(txtSongaydieutri.Text, 0);
                _KcbChandoanKetluan.Ketluan = Utility.sDbnull(txtKet_Luan.Text, "");
                _KcbChandoanKetluan.NhanXet = Utility.sDbnull(txtNhanxet.Text, "");
                if (Utility.Int16Dbnull(txtBacsi.MyID, -1) > 0)
                    _KcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID);
                else
                {
                    _KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                string sMaICDPHU = GetDanhsachBenhphu();
                _KcbChandoanKetluan.MabenhPhu = Utility.sDbnull(sMaICDPHU, "");
                if (objkcbdangky != null)
                {
                    _KcbChandoanKetluan.IdKhoanoitru = Utility.Int32Dbnull(objkcbdangky.IdKhoakcb, -1);
                    _KcbChandoanKetluan.IdPhongkham = Utility.Int32Dbnull(objkcbdangky.IdPhongkham, -1);
                    DmucKhoaphong objDepartment =
                        DmucKhoaphong.FetchByID(Utility.Int32Dbnull(objkcbdangky.IdPhongkham, -1));
                    if (objDepartment != null)
                    {
                        _KcbChandoanKetluan.TenPhongkham = Utility.sDbnull(objDepartment.TenKhoaphong, "");
                    }
                }
                else
                {
                    _KcbChandoanKetluan.IdKhoanoitru = globalVariables.idKhoatheoMay;
                    _KcbChandoanKetluan.IdPhongkham = globalVariables.idKhoatheoMay;
                }
                _KcbChandoanKetluan.IdKham = Utility.Int32Dbnull(txt_idchidinhphongkham.Text, -1);
                _KcbChandoanKetluan.NgayTao = dtpCreatedDate.Value;
                _KcbChandoanKetluan.NguoiTao = globalVariables.UserName;
                _KcbChandoanKetluan.NgayChandoan = dtpCreatedDate.Value;
                _KcbChandoanKetluan.Ketluan = Utility.sDbnull(txtKet_Luan.Text);
                _KcbChandoanKetluan.Chandoan = Utility.ReplaceString(txtChanDoan.Text);
                _KcbChandoanKetluan.ChandoanKemtheo = Utility.sDbnull(txtChanDoanKemTheo.Text);
                _KcbChandoanKetluan.IdPhieudieutri = -1;
                _KcbChandoanKetluan.Noitru = 0;
                return _KcbChandoanKetluan;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi đóng gói dữ liệu vào Entity
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            if (Utility.Int32Dbnull(txtBacsi.MyID, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn bác sĩ khám trước khi kết thúc khám ngoại trú cho Bệnh nhân", true);
                txtBacsi.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPatient_Code.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn Bệnh nhân để thực hiện thăm khám", true);
                txtPatient_Code.Focus();
                return false;
            }
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 2)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdSave.Focus();
                return false;
            }
            if (objLuotkham != null &&
                ((Utility.sDbnull(objLuotkham.MatheBhyt).Trim() != "" &&
                  THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_BATNHAPSONGAYDIEUTRI_BHYT", "0", false) == "1")
                 ||
                 (Utility.sDbnull(objLuotkham.MatheBhyt).Trim() == "" &&
                  THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THAMKHAM_BATNHAPSONGAYDIEUTRI_DV", "0", false) == "1")))
            {
                if (Utility.DecimaltoDbnull(txtSongaydieutri.Text) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập ngày điều trị phải lớn hơn 0", true);
                    txtSongaydieutri.Focus();
                    return false;
                }
            }

            if (objLuotkham != null && objLuotkham.MaDoituongKcb == "BHYT" && Utility.DoTrim(txtMaBenhChinh.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập Mã bệnh chính cho đối tượng BHYT trước khi kết thúc khám", true);
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                txtMaBenhChinh.Focus();
                return false;
            }
            if (Utility.DoTrim(txtKet_Luan.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập kết quả khám cho bệnh nhân", true);
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                txtKet_Luan.Focus();
                return false;
            }
            if (Utility.DoTrim(txtHuongdieutri.myCode) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập hướng điều trị cho bệnh nhân", true);
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                txtHuongdieutri.Focus();
                return false;
            }
            chkDaThucHien.Checked = true;
            chkDaThucHien.Visible = chkDaThucHien.Checked;

            return true;
        }

        /// <summary>
        /// Thực hiện thao tác Insert,Update,Delete tới CSDL theo m_enAction
        /// </summary>
        private void PerformAction()
        {
            try
            {
                //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                if (!IsValidData())
                {
                    return;
                }
                TimeSpan songaychothuoc = Convert.ToDateTime(objLuotkham.NgayketthucBhyt).Subtract(globalVariables.SysDate);
                int songay =  Utility.Int32Dbnull(songaychothuoc.TotalDays);
                if(Utility.Int32Dbnull(songay)<=Utility.Int32Dbnull(txtSongaydieutri.Text))
                {
                    if(!Utility.AcceptQuestion(string.Format("Số ngày cho thuốc vượt quá hạn thẻ BHYT của bệnh nhân {0}. \n Có đồng ý tiếp tục kết thúc không?",objBenhnhan.TenBenhnhan),"Cảnh Báo",true))
                    {
                        return;
                    }
                }
                objkcbdangky.TrangThai = (byte?) (chkDaThucHien.Checked ? 1 : 0);
                DataRow[] arrDr = m_dtDanhsachbenhnhanthamkham.Select("id_kham=" + txtReg_ID.Text);
                if (arrDr.Length > 0)
                {
                    arrDr[0]["trang_thai"] = chkDaThucHien.Checked ? 1 : 0;
                }
                objkcbdangky.IdBacsikham = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                objLuotkham.TrieuChung = txtTrieuChungBD.Text;
                if (!THU_VIEN_CHUNG.IsBaoHiem((byte) objLuotkham.IdLoaidoituongKcb))
                    //Đối tượng dịch vụ được khóa ngay sau khi kết thúc khám
                {
                    objLuotkham.NguoiKetthuc = chkDaThucHien.Checked ? globalVariables.UserName : "";
                    if (chkDaThucHien.Checked)
                        objLuotkham.NgayKetthuc = globalVariables.SysDate;
                    else
                        objLuotkham.NgayKetthuc = null;
                    objLuotkham.Locked = chkDaThucHien.Checked ? (byte) 1 : (byte) 0;
                    objLuotkham.TrangthaiNgoaitru = objLuotkham.Locked;
                }
                else
                {
                    objLuotkham.NguoiKetthuc = chkDaThucHien.Checked ? globalVariables.UserName : "";
                    objLuotkham.NgayKetthuc = globalVariables.SysDate;
                }
                objLuotkham.SongayDieutri = Utility.Int32Dbnull(txtSongaydieutri.Text, 0);
                objLuotkham.HuongDieutri = Utility.sDbnull(txtHuongdieutri.Text,"");
                objLuotkham.KetLuan = Utility.sDbnull(txtKet_Luan.Text);
                ActionResult actionResult =
                    _KCB_THAMKHAM.UpdateExamInfo(
                        TaoDulieuChandoanKetluan(), objkcbdangky, objLuotkham);
                switch (actionResult)
                {
                    case ActionResult.Success:

                        IEnumerable<GridEXRow> query = from kham in grdList.GetDataRows()
                                                       where
                                                           kham.RowType == RowType.Record &&
                                                           Utility.Int32Dbnull(
                                                               kham.Cells[KcbDangkyKcb.Columns.IdKham].Value) ==
                                                           Utility.Int32Dbnull(txt_idchidinhphongkham.Text)
                                                       select kham;
                        if (query.Count() > 0)
                        {
                            GridEXRow gridExRow = query.FirstOrDefault();
                            //gridExRow.BeginEdit();
                            gridExRow.Cells[KcbDangkyKcb.Columns.TrangThai].Value =
                                (byte?) (chkDaThucHien.Checked ? 1 : 0);
                            gridExRow.Cells[KcbLuotkham.Columns.NguoiKetthuc].Value = globalVariables.UserName;
                            gridExRow.Cells[KcbLuotkham.Columns.NgayKetthuc].Value = globalVariables.SysDate;
                            //gridExRow.EndEdit();
                            grdList.UpdateData();
                            Utility.GotoNewRowJanus(grdList, KcbDangkyKcb.Columns.IdKham,
                                                    Utility.sDbnull(txt_idchidinhphongkham.Text));
                        }
                        cmdInTTDieuTri.Visible = true;
                        cmdInphieuhen.Visible = true;
                        cboChonBenhAn.Visible = true &&
                                                THU_VIEN_CHUNG.Laygiatrithamsohethong("BENH_AN", "0", false) == "1" &&
                                                (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU",
                                                                                       "ALL", false) == "ALL" ||
                                                 THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_ICD_BENH_AN_NGOAI_TRU",
                                                                                       "ALL", false).Contains(
                                                                                           Utility.DoTrim(
                                                                                               txtMaBenhChinh.Text)));
                        lblBenhan.Visible = cboChonBenhAn.Visible;
                        chkDaThucHien.Checked = true;

                        //Tự động ẩn BN về tab đã khám
                        int Status = radChuaKham.Checked ? 0 : 1;
                        if (Status == 0)
                        {
                            m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "1=1";
                            m_dtDanhsachbenhnhanthamkham.DefaultView.RowFilter = "trang_thai=" + Status;
                        }

                        var objStaff =
                            new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(
                                Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                        string TenNhanvien = objLuotkham.NguoiKetthuc;
                        if (objStaff != null)
                            TenNhanvien = objStaff.TenNhanvien;
                        //Tự động bật tính năng nhập viện nội trú nếu hướng điều trị chọn là Nội trú và Bệnh nhân chưa nhập viện
                        if (cmdNhapVien.Visible && objLuotkham.TrangthaiNoitru == 0 &&
                            txtHuongdieutri.myCode.ToUpper() ==
                            THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MAHUONGDIEUTRI_NOITRU", false).ToUpper())
                        {
                            cmdNhapVien_Click(cmdNhapVien, new EventArgs());
                            ;
                        }
                        cmdUnlock.Visible = objLuotkham.TrangthaiNoitru == 0 && objLuotkham.Locked.ToString() == "1";
                        cmdUnlock.Enabled = cmdUnlock.Visible &&
                                            (Utility.Coquyen("quyen_mokhoa_tatca") ||
                                             objLuotkham.NguoiKetthuc == globalVariables.UserName);
                        if (!cmdUnlock.Enabled)
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " +
                                                TenNhanvien + "(" + objLuotkham.NguoiKetthuc +
                                                " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                        else
                            toolTip1.SetToolTip(cmdUnlock,
                                                "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá lưu thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                        break;
                }
                ModifyCommmands();
                cmdNhapVien.Enabled = objkcbdangky.TrangThai == 1;
                cmdHuyNhapVien.Enabled = objLuotkham.TrangthaiNoitru >= 1;
                cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }

        #endregion

        #region VTTH

        private void mnuDelVTTH_Click(object sender, EventArgs e)
        {
            if (!IsValidDeleteSelectedVTTH()) return;
            PerformActionDeleteSelectedVTTH();
            ModifyCommmands();
        }

        private void cmdXoaphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!IsValidVTTH_delete()) return;
            PerformActionDeleteVTTH();
            ModifyCommmands();
        }

        private void cmdInphieuVT_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdVTTH)) return;
            int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            PrintPres(Pres_ID, "PHIẾU VẬT TƯ NGOÀI GÓI");
        }

        private void cmdSuaphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdSuaphieuVT.Enabled) return;
            SuaphieuVattu();
        }

        private void cmdThemphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdThemphieuVT.Enabled) return;
            if (Utility.Coquyen("quyen_suadonthuoc") || Utility.Int32Dbnull(objkcbdangky.IdBacsikham, -1) <= 0 ||
                objkcbdangky.IdBacsikham == globalVariables.gv_intIDNhanvien)
            {
            }
            else
            {
                Utility.ShowMsg(
                    string.Format(
                        "Bệnh nhân này đã được khám bởi Bác sĩ khác nên bạn không được phép thêm phiếu vật tư cho Bệnh nhân"));
                return;
            }

            ThemphieuVattu();
        }

        private void ThemphieuVattu()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                var frm = new frm_KCB_KE_DONTHUOC("VT");
                frm.em_Action = action.Insert;
                frm.objLuotkham = objLuotkham;
                frm._KcbChandoanKetluan = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.Text;
                frm._Chandoan = txtChanDoan.Text;
                frm.dt_ICD = dt_ICD;
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objPhieudieutriNoitru = null;
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 0;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.Text = frm._MabenhChinh;
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbChandoanKetluan != null) _KcbChandoanKetluan = frm._KcbChandoanKetluan;
                    Laythongtinchidinhngoaitru();
                    Utility.GotoNewRowJanus(grdVTTH, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                }
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                ModifyCommmands();
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        private void SuaphieuVattu()
        {
            try
            {
                if (!CheckPatientSelected()) return;
                if (!Utility.isValidGrid(grdVTTH)) return;

                KcbLuotkham objPatientExam = objLuotkham;
                if (objPatientExam != null)
                {
                    int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                    if (Donthuoc_DangXacnhan(Pres_ID))
                    {
                        Utility.ShowMsg(
                            "Phiếu vật tư này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát vật tư tại phòng vật tư");
                        return;
                    }
                    var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                        .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                        .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                        .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                    if (v_collect.Count > 0)
                    {
                        Utility.ShowMsg(
                            "Phiếu vật tư bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Phiếu vật tư Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư để hủy xác nhận Phiếu vật tư tại kho vật tư");
                        return;
                    }
                    KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                    if (objPrescription != null)
                    {
                        var frm = new frm_KCB_KE_DONTHUOC("VT");
                        frm.em_Action = action.Update;
                        frm._KcbChandoanKetluan = _KcbChandoanKetluan;
                        frm._MabenhChinh = txtMaBenhChinh.Text;
                        frm._Chandoan = txtChanDoan.Text;
                        frm.dt_ICD = dt_ICD;
                        frm.dt_ICD_PHU = dt_ICD_PHU;
                        frm.noitru = 0;
                        frm.objLuotkham = objLuotkham;
                        frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                        frm.objPhieudieutriNoitru = null;
                        frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                        frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                        frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                        frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            txtMaBenhChinh.Text = frm._MabenhChinh;
                            txtChanDoan._Text = frm._Chandoan;
                            dt_ICD_PHU = frm.dt_ICD_PHU;
                            if (frm._KcbChandoanKetluan != null) _KcbChandoanKetluan = frm._KcbChandoanKetluan;
                            Laythongtinchidinhngoaitru();
                            Utility.GotoNewRowJanus(grdVTTH, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                    Utility.sDbnull(frm.txtPres_ID.Text));
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
            }
        }

        private void PerformActionDeleteVTTH()
        {
            string s = "";
            int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            if (Donthuoc_DangXacnhan(Pres_ID))
            {
                Utility.ShowMsg(
                    "Phiếu vật tư này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị quay lại hỏi bộ phận cấp phát vật tư tại phòng vật tư");
                return;
            }
            var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
            if (v_collect.Count > 0)
            {
                Utility.ShowMsg(
                    "Phiếu vật tư bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Phiếu vật tư Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư để hủy xác nhận Phiếu vật tư tại kho vật tư");
                return;
            }
            var lstIdchitiet = new List<int>();
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                List<int> _temp = GetIdChitietVTTH(IdDonthuoc, id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdVTTH.UpdateData();
            }
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            XoaVTTHKhoiBangDulieu(lstIdchitiet);
            m_dtVTTH.AcceptChanges();
        }

        private void XoaVTTHKhoiBangDulieu(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                DataRow[] p = (from q in m_dtVTTH.Select("1=1").AsEnumerable()
                               where
                                   lstIdChitietDonthuoc.Contains(
                                       Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                               select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtVTTH.Rows.Remove(p[i]);
                m_dtVTTH.AcceptChanges();
            }
            catch
            {
            }
        }

        private List<int> GetIdChitietVTTH(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr =
                m_dtVTTH.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " +
                                KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                                + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
                IEnumerable<string> p1 = (from q in arrDr.AsEnumerable()
                                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                s = string.Join(",", p1.ToArray());
                IEnumerable<int> p = (from q in arrDr.AsEnumerable()
                                      select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).
                    Distinct();
                return p.ToList();
            }
            return new List<int>();
        }

        private bool IsValidVTTH_delete()
        {
            bool b_Cancel = false;
            if (grdVTTH.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin Vật tư tiêu hao ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (Utility.Coquyen("quyen_suadonthuoc") ||
                    Utility.sDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                    globalVariables.UserName)
                {
                }
                else
                {
                    Utility.ShowMsg(
                        "Trong các VTTH bạn chọn xóa, có một số VTTH được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các VTTH do chính bạn kê để thực hiện xóa");
                    return false;
                }
            }
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdSave.Focus();
                return false;
            }
            KcbDonthuocChitiet objKcbDonthuocChitiet = null;
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int v_intIDDonthuoc =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int v_intIDThuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                    objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(v_intIDDonthuoc)
                        .ExecuteSingle<KcbDonthuocChitiet>();
                    if (objKcbDonthuocChitiet != null)
                    {
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangthaiThanhtoan))
                        {
                            Utility.ShowMsg("Bản ghi đã thanh toán, Bạn không thể xóa thông tin được ", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                        if (Utility.Byte2Bool(objKcbDonthuocChitiet.TrangThai))
                        {
                            Utility.ShowMsg("Bản ghi đã xác nhận, Bạn không thể xóa thông tin được ", "Thông báo",
                                            MessageBoxIcon.Warning);
                            b_Cancel = true;
                            break;
                        }
                    }
                }
            }
            if (b_Cancel)
            {
                grdVTTH.Focus();
                return false;
            }
            return true;
        }

        private bool IsValidDeleteSelectedVTTH()
        {
            bool b_Cancel = false;
            if (grdVTTH.RowCount <= 0 || grdVTTH.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một VTTH để xóa ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            if (Utility.Coquyen("quyen_suadonthuoc") ||
                Utility.sDbnull(grdVTTH.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
                globalVariables.UserName)
            {
            }
            else
            {
                Utility.ShowMsg(
                    "VTTH đang chọn xóa được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các VTTH do chính bạn kê để thực hiện xóa");
                return false;
            }
            KcbLuotkham _item = Utility.getKcbLuotkham(Utility.Int64Dbnull(txtPatient_ID.Text, 0), m_strMaLuotkham);
            if (_item == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }

            if (_item != null && Utility.Int32Dbnull(_item.TrangthaiNoitru, -1) >= 1)
            {
                Utility.ShowMsg(
                    "Bệnh nhân đã được điều trị nội trú. Bạn chỉ có thể xem thông tin và không được phép làm các công việc liên quan đến ngoại trú",
                    "Thông báo");
                cmdSave.Focus();
                return false;
            }
            if (grdVTTH.CurrentRow.RowType == RowType.Record)
            {
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(v_intIDDonthuoc)
                    .And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                }
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Vật tư tiêu hao đang chọn xóa đã được thanh toán, Bạn không thể xóa thông tin được ",
                                "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            if (grdVTTH.CurrentRow.RowType == RowType.Record)
            {
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(v_intIDDonthuoc)
                    .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                }
            }

            if (b_Cancel)
            {
                Utility.ShowMsg(
                    "Vật tư tiêu hao đang chọn xóa đã được được xác nhận nên bạn không thể xóa thông tin được ",
                    "Thông báo",
                    MessageBoxIcon.Warning);
                grdVTTH.Focus();
                return false;
            }
            return true;
        }

        private void PerformActionDeleteSelectedVTTH()
        {
            try
            {
                int Pres_ID =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                        -1);
                int v_intIDDonthuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int v_intIDThuoc =
                    Utility.Int32Dbnull(grdVTTH.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                        -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(v_intIDDonthuoc);
                grdVTTH.CurrentRow.Delete();
                grdVTTH.UpdateData();
                m_dtVTTH.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message + "-->" +
                                "Bạn nên dùng chức năng xóa thuốc bằng cách chọn thuốc và sử dụng nút xóa thuốc");
            }
        }

        #endregion

        private void txtNhietDo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string original = (sender as Janus.Windows.UI.).Text;
            //if (!char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
            //if (e.KeyChar == '.')
            //{
            //    if (original.Contains('.'))
            //        e.Handled = true;
            //    else if (!(original.Contains('.')))
            //        e.Handled = false;

            //}
            //else if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
            //{
            //    e.Handled = false;
            //}

        }

        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
               
                List<string> lstIdchidinhchitiet = new List<string>();
                if (objLuotkham != null)
                {
                    // cmdConform = 1 thì là chuyển cận
                    if (cmdConfirm.Tag.ToString() == "1")
                    {
                        int id_chidinh =
                            Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                        DataRow[] arrDr = m_dtAssignDetail.Select("trangthai_chuyencls=0 and id_chidinh = '" + id_chidinh + "' ");
                        if (arrDr.Length == 0)
                        {
                            Utility.SetMsg(lblMsg, string.Format("Các chỉ định CLS đã được chuyển hết"), false);
                            Utility.DefaultNow(this);
                            return;
                        }
                        else
                        {
                            foreach (DataRow dr in arrDr)
                            {
                                dr["trangthai_chuyencls"] = 1;
                            }
                            m_dtAssignDetail.AcceptChanges();
                            int result = new Update(KcbChidinhclsChitiet.Schema)
                                .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                .Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(1)
                                .Where(KcbChidinhclsChitiet.Columns.TrangThai).IsEqualTo(0)
                                .And(KcbChidinhclsChitiet.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                .And(KcbChidinhclsChitiet.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                .And(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(id_chidinh)
                                .Execute();
                            Utility.SetMsg(lblMsg,
                                           string.Format("Bạn vừa chuyển CLS thành công {0} dịch vụ", result.ToString()),
                                           false);
                            if(THU_VIEN_CHUNG.Laygiatrithamsohethong("CHOPHEP_BACSY_CHUYENKETNOI_HISLIS", "0", true)=="1")
                            {
                                #region Hàm bác sỹ  thực hiện đẩy kết nối his - lis
                                DataSet dsData =
                               SPs.HisLisLaydulieuchuyensangLis(dtInput_Date.Value.ToString("dd/MM/yyyy"),
                                                                objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).
                                   GetDataSet();
                                DataTable dt2LIS = dsData.Tables[1].Copy();
                                List<long> lstIchidinh = (from q in grdAssignDetail.GetDataRows()
                                                          select
                                                              Utility.Int64Dbnull(
                                                                  q.Cells[KcbChidinhcl.Columns.IdChidinh].Value, 0)).ToList
                                    <long>();
                                List<DataRow> lstData2Send = (from p in dsData.Tables[0].AsEnumerable()
                                                              where
                                                                  lstIchidinh.Contains(
                                                                      Utility.Int64Dbnull(
                                                                          p[KcbChidinhclsChitiet.Columns.IdChidinh]))
                                                                  && Utility.Int64Dbnull(p["trang_thai"], 0) == 1
                                                              select p).ToList<DataRow>();
                                List<DataRow> lstData2Send_real = (from p in dsData.Tables[1].AsEnumerable()
                                                                   where lstIchidinh.Contains(Utility.Int64Dbnull(p[KcbChidinhclsChitiet.Columns.IdChidinh]))
                                                                   && Utility.Int64Dbnull(p["trang_thai"], 0) == 1
                                                                   select p).ToList<DataRow>();
                                if (lstData2Send.Any())
                                {
                                    dt2LIS = lstData2Send_real.CopyToDataTable();
                                    lstIdchidinhchitiet = (from p in lstData2Send
                                                           select
                                                               Utility.sDbnull(
                                                                   p[KcbChidinhclsChitiet.Columns.IdChitietchidinh], 0)).
                                        Distinct().ToList<string>();
                                    int recoder =
                                        VMS.HIS.HLC.ASTM.RocheCommunication.WriteOrderMessage(
                                            THU_VIEN_CHUNG.Laygiatrithamsohethong("ASTM_ORDERS_FOLDER",
                                                                                  @"\\192.168.1.254\Orders", false), dt2LIS);
                                    if (recoder == 0) //Thành công
                                    {
                                        SPs.HisLisCapnhatdulieuchuyensangLis(
                                            string.Join(",", lstIdchidinhchitiet.ToArray()), 2, 1).Execute();
                                        dsData.Tables[0].AsEnumerable()
                                            .Where(
                                                c =>
                                                lstIdchidinhchitiet.Contains(
                                                    Utility.sDbnull(
                                                        c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                            .ToList<DataRow>()
                                            .ForEach(c1 =>
                                            {
                                                c1["trang_thai"] = 2;
                                                //   c1["ten_trangthai"] = "Đang thực hiện";
                                            });
                                        dsData.Tables[1].AsEnumerable()
                                            .Where(
                                                c =>
                                                lstIdchidinhchitiet.Contains(
                                                    Utility.sDbnull(
                                                        c.Field<long>(KcbChidinhclsChitiet.Columns.IdChitietchidinh))))
                                            .ToList<DataRow>()
                                            .ForEach(c1 =>
                                            {
                                                c1["trang_thai"] = 2;
                                                // c1["ten_trangthai"] = "Đang thực hiện";
                                            });
                                        dsData.AcceptChanges();
                                        Utility.SetMsg(lblMsg,
                                                       string.Format(
                                                           "Các dữ liệu dịch vụ cận lâm sàng của Bệnh nhân đã được gửi thành công sang LIS"),
                                                       false);
                                    }
                                }
                                #endregion
                            }
                           
                        }

                    }
                    else
                    {
                        if(cmdConfirm.Tag.ToString() == "2")
                        {
                            foreach (GridEXRow row in grdAssignDetail.GetCheckedRows())
                            {
                                int id_chidinh =
                                    Utility.Int32Dbnull(row.Cells[KcbChidinhclsChitiet.Columns.IdChidinh], -1);
                                bool hasFound = false;
                                Utility.WaitNow(this);
                                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                                    .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(
                                        new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema).Where(
                                            KcbChidinhcl.Columns.MaLuotkham)
                                            .IsEqualTo(txtPatient_Code.Text)
                                            .And(KcbChidinhcl.Columns.IdBenhnhan)
                                            .IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text)))
                                    .And(KcbChidinhclsChitiet.Columns.TrangThai).In(1, 2)
                                    .And(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(
                                        Utility.Int32Dbnull(id_chidinh));
                                hasFound = sqlQuery.GetRecordCount() > 0;
                                if (sqlQuery.GetRecordCount() <= 0)
                                {
                                    Utility.SetMsg(lblMsg, string.Format("Không có chỉ định CLS có thể hủy chuyển"),
                                                   false);
                                    Utility.DefaultNow(this);
                                    return;
                                }
                                DataRow[] arrDr =
                                    m_dtAssignDetail.Select("trangthai_chuyencls in (1,2) and id_chidinh = '" +
                                                            id_chidinh + "'");
                                foreach (DataRow dr in arrDr)
                                {
                                    dr["trangthai_chuyencls"] = 0;
                                }
                                m_dtAssignDetail.AcceptChanges();
                                int result = new Update(KcbChidinhclsChitiet.Schema)
                                    .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                    .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                    .Set(KcbChidinhclsChitiet.Columns.TrangThai).EqualTo(0)
                                    .Where(KcbChidinhclsChitiet.Columns.TrangThai).In(1, 2)
                                    .And(KcbChidinhclsChitiet.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                    .And(KcbChidinhclsChitiet.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                    .And(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(
                                        Utility.Int32Dbnull(id_chidinh))
                                    .Execute();

                                Utility.SetMsg(lblMsg,
                                               string.Format("Bạn vừa hủy chuyển CLS thành công {0} dịch vụ",
                                                             result.ToString()),
                                               false);
                            }
                          
                        }
                        if (cmdConfirm.Tag.ToString() == "3")
                        {

                        }

                    }
                }
                ModifyCommmands();
                HienThiChuyenCan();
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
                Utility.DefaultNow(this);
            }
            
          
        }

        private void txtNhipTim_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMach.Text))
            {
                txtMach.Text = txtNhipTim.Text;
            }
        }
        private void txtMach_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNhipTim.Text))
            {
                txtNhipTim.Text = txtMach.Text;
            }
        }
    }
}