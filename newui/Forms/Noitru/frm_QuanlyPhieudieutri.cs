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
using VNS.Libs.AppUI;
using VNS.UCs;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.Forms.Cauhinh;

namespace VNS.HIS.UI.NOITRU
{
    /// <summary>
    /// 06/11/2013 3h57
    /// </summary>
    public partial class frm_QuanlyPhieudieutri : Form
    {
        KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        KCB_KEDONTHUOC _KCB_KEDONTHUOC = new KCB_KEDONTHUOC();
        KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
        private readonly string FileName = string.Format("{0}/{1}", Application.StartupPath,
                                                         "SplitterDistanceThamKham.txt");

        private readonly MoneyByLetter MoneyByLetter = new MoneyByLetter();
       
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
       
        public KcbLuotkham objLuotkham=null;
        public KcbDanhsachBenhnhan objBenhnhan=null;
        NoitruPhieudieutri objPhieudieutri = null;
        private bool Selected;
        private string TEN_BENHPHU = "";
        private bool hasMorethanOne = true;
        private string _rowFilter = "1=1";
        private bool b_Hasloaded;
        private DataSet ds = new DataSet();
        private DataTable dt_ICD_PHU = new DataTable();
        
        private bool m_blnHasLoaded;
       
        private bool isLike = true;
        private DataTable m_dtAssignDetail;
        private DataTable m_dtGoidichvu;
        private DataTable m_dtChandoanKCB=new DataTable();
        private DataTable m_dtDataVTYT = new DataTable();
        private DataTable m_dtChedoDinhduong = new DataTable();
        private DataTable m_dtDoctorAssign;
      
        private DataTable m_dtDonthuoc = new DataTable();
        private DataTable m_dtVTTH = new DataTable();
        private DataTable m_dtVTTH_tronggoi = new DataTable();
        private DataTable m_dtDonthuocChitiet_View = new DataTable();
        private DataTable m_dtVTTHChitiet_View = new DataTable();
        
        private DataTable m_dtReport = new DataTable();
        private DataTable m_hdt = new DataTable();
        private DataTable m_kl;
        private string m_strDefaultLazerPrinterName = "";
        action m_enActChandoan = action.FirstOrFinished;
        /// <summary>
        /// hàm thực hiện việc chọn bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string malankham = "";

        private DataTable m_dtPatients = new DataTable();
        private frm_DanhSachKham objSoKham;
        private GridEXRow row_Selected;
        private bool trieuchung;
        List<string> lstVisibleColumns = new List<string>();
        List<string> lstResultColumns = new List<string>() { "ten_chitietdichvu", "ketqua_cls", "binhthuong_nam", "binhthuong_nu" };

        List<string> lstKQKhongchitietColumns = new List<string>() { "Ket_qua", "bt_nam", "bt_nu" };
        List<string> lstKQCochitietColumns = new List<string>() { "ten_chitietdichvu", "Ket_qua", "bt_nam", "bt_nu" };


        public frm_QuanlyPhieudieutri()
        {
            InitializeComponent();
            KeyPreview = true;
           
            log = LogManager.GetCurrentClassLogger();
           
            
           
            dtInput_Date.Value =
                dtpCreatedDate.Value=dtpNgaylapphieu.Value = globalVariables.SysDate;

            dtFromDate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;

            LoadLaserPrinters();
          
            CauHinhThamKham();
            Cauhinh();
            InitEvents();
        }
        void InitEvents()
        {
            FormClosing += frm_LAOKHOA_Add_TIEPDON_BN_FormClosing;
            Load+=new EventHandler(frm_QuanlyPhieudieutri_Load);
            KeyDown+=new KeyEventHandler(frm_QuanlyPhieudieutri_KeyDown);
           
            cmdSearch.Click+=new EventHandler(cmdSearch_Click);
            txtPatient_Code.KeyDown+=new KeyEventHandler(txtPatient_Code_KeyDown);
            grdPatientList.ColumnButtonClick+=new ColumnActionEventHandler(grdPatientList_ColumnButtonClick);
            grdPatientList.KeyDown += new KeyEventHandler(grdPatientList_KeyDown);
            grdPatientList.DoubleClick += new EventHandler(grdPatientList_DoubleClick);
            grdPatientList.MouseClick += new MouseEventHandler(grdPatientList_MouseClick);

            grdGoidichvu.SelectionChanged += grdGoidichvu_SelectionChanged;
            grdGoidichvu.DoubleClick += grdGoidichvu_DoubleClick;
            
            grdAssignDetail.CellUpdated += grdAssignDetail_CellUpdated;
            grdAssignDetail.SelectionChanged+=new EventHandler(grdAssignDetail_SelectionChanged);
            
            cmdInsertAssign.Click+=new EventHandler(cmdInsertAssign_Click);
            cmdUpdate.Click+=new EventHandler(cmdUpdate_Click);
            cmdDelteAssign.Click+=new EventHandler(cmdDelteAssign_Click);
            cboLaserPrinters.SelectedIndexChanged+=new EventHandler(cboLaserPrinters_SelectedIndexChanged);
            cmdPrintAssign.Click+=new EventHandler(cmdPrintAssign_Click);

            cboA4Donthuoc.SelectedIndexChanged += new EventHandler(cboA4_SelectedIndexChanged);
            cboPrintPreviewDonthuoc.SelectedIndexChanged += new EventHandler(cboPrintPreview_SelectedIndexChanged);
            cmdCreateNewPres.Click+=new EventHandler(cmdCreateNewPres_Click);
            cmdUpdatePres.Click+=new EventHandler(cmdUpdatePres_Click);
            cmdDeletePres.Click+=new EventHandler(cmdDeletePres_Click);
            cmdPrintPres.Click+=new EventHandler(cmdPrintPres_Click);

            mnuDelDrug.Click+=new EventHandler(mnuDelDrug_Click);
            mnuDeleteCLS.Click+=new EventHandler(mnuDeleteCLS_Click);

            
            cmdThamkhamConfig.Click += new EventHandler(cmdThamkhamConfig_Click);
            cmdNoitruConfig.Click += cmdNoitruConfig_Click;
            chkAutocollapse.CheckedChanged += new EventHandler(chkAutocollapse_CheckedChanged);

            grd_ICD.ColumnButtonClick+=new ColumnActionEventHandler(grd_ICD_ColumnButtonClick);
            chkHienthichitiet.CheckedChanged += new EventHandler(chkHienthichitiet_CheckedChanged);

           
            cboA4Cls.SelectedIndexChanged += new EventHandler(cboA4Cls_SelectedIndexChanged);
            cboPrintPreviewCLS.SelectedIndexChanged += new EventHandler(cboPrintPreviewCLS_SelectedIndexChanged);

            cboA4Tomtatdieutringoaitru.SelectedIndexChanged += new EventHandler(cboA4Tomtatdieutringoaitru_SelectedIndexChanged);
            cboPrintPreviewTomtatdieutringoaitru.SelectedIndexChanged += new EventHandler(cboPrintPreviewTomtatdieutringoaitru_SelectedIndexChanged);

            txtChanDoan._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtChanDoan__OnShowData);
          
            txtNhommau._OnShowData += new UCs.AutoCompleteTextbox_Danhmucchung.OnShowData(txtNhommau__OnShowData);

            txtNhommau._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtNhommau__OnSaveAs);
          
            txtChanDoan._OnSaveAs += new UCs.AutoCompleteTextbox_Danhmucchung.OnSaveAs(txtChanDoan__OnSaveAs);

            grdPhieudieutri.ColumnButtonClick += new ColumnActionEventHandler(grdPhieudieutri_ColumnButtonClick);
            grdPhieudieutri.SelectionChanged += new EventHandler(grdPhieudieutri_SelectionChanged);

            cmdthemphieudieutri.Click += new EventHandler(cmdthemphieudieutri_Click);
            cmdSuaphieudieutri.Click += new EventHandler(cmdSuaphieudieutri_Click);
            cmdxoaphieudieutri.Click += new EventHandler(cmdxoaphieudieutri_Click);
            cmdInphieudieutri.Click += new EventHandler(cmdInphieudieutri_Click);
            cmdSaochep.Click += new EventHandler(cmdSaochep_Click);
            cmdCauHinh.Click += new EventHandler(cmdCauHinh_Click);

            cmdThemgoiDV.Click += cmdThemgoiDV_Click;
            cmdSuagoiDV.Click += cmdSuagoiDV_Click;
            cmdXoagoiDV.Click += cmdXoagoiDV_Click;
            cmdIngoiDV.Click += cmdIngoiDV_Click;

            cmdThemphieuVT.Click += cmdThemphieuVT_Click;
            cmdSuaphieuVT.Click += cmdSuaphieuVT_Click;
            cmdXoaphieuVT.Click += cmdXoaphieuVT_Click;
            cmdInphieuVT.Click += cmdInphieuVT_Click;

            cmdThemphieuVT_tronggoi.Click += cmdThemphieuVT_tronggoi_Click;
            cmdSuaphieuVT_tronggoi.Click += cmdSuaphieuVT_tronggoi_Click;
            cmdXoaphieuVT_tronggoi.Click += cmdXoaphieuVT_tronggoi_Click;
            cmdInphieuVT_tronggoi.Click += cmdInphieuVT_tronggoi_Click;

            grdPresDetail.SelectionChanged += grdPresDetail_SelectionChanged;

            cmdThemchandoan.Click += cmdThemchandoan_Click;
            cmdSuachandoan.Click += cmdSuachandoan_Click;
            cmdXoachandoan.Click += cmdXoachandoan_Click;
            cmdHuychandoan.Click += cmdHuychandoan_Click;
            cmdGhichandoan.Click += cmdGhichandoan_Click;
            grdChandoan.SelectionChanged += grdChandoan_SelectionChanged;

            cmdChuyengoi.Click += cmdChuyengoi_Click;
            lnkSize.Click += lnkSize_Click;
            cmdXoaDinhduong.Click += cmdXoaDinhduong_Click;
            grdChedoDinhduong.ColumnButtonClick += grdChedoDinhduong_ColumnButtonClick;
            cmdAdd.Click += cmdAdd_Click;
            txtSongay.KeyDown += txtSongay_KeyDown;
            dtpNgaylapphieu.KeyDown += dtpNgaylapphieu_KeyDown;
            chkViewAll.CheckedChanged += chkViewAll_CheckedChanged;
            txtHoly._OnShowData += txtHoly__OnShowData;
            txtChedodinhduong._OnShowData += txtChedodinhduong__OnShowData;
            grdBuongGiuong.MouseDoubleClick += grdBuongGiuong_MouseDoubleClick;
            txtMaluotkham.KeyDown += txtMaluotkham_KeyDown;

           
            grdPresDetail.UpdatingCell += grdPresDetail_UpdatingCell;
            txtMaBenhphu._OnEnterMe += txtMaBenhphu__OnEnterMe;
            grdChandoan.ColumnButtonClick += grdChandoan_ColumnButtonClick;
        }

        void grdChandoan_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "cmdXoa")
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa chẩn đoán đang chọn?", "Xác nhận xóa", true))
                {
                    if (grdChandoan.GetCheckedRows().Length <= 0)
                    {
                        grdChandoan.CurrentRow.IsChecked = true;
                    }
                    XoaChandoan();
                    ModifyCommmands();
                }
            }
        }

        void txtMaBenhphu__OnEnterMe()
        {
            if (txtMaBenhphu.MyCode != "-1")
            {
                EnumerableRowCollection<DataRow> query = from benh in dt_ICD_PHU.AsEnumerable()
                                                         where Utility.sDbnull(benh[DmucBenh.Columns.MaBenh]) == txtMaBenhphu.MyCode
                                                         select benh;


                if (!query.Any())
                    AddMaBenh(txtMaBenhphu.MyCode, txtMaBenhphu.Text);
            }

            txtMaBenhphu.SetCode("-1");
            txtMaBenhphu.Focus();
            txtMaBenhphu.SelectAll();
        }
        void Modify_Thuoctralai()
        {
            try
            {
                bool _dacapphat = false;
                if (grdPresDetail.GetDataRows().Length > 0)
                {
                    _dacapphat = m_dtDonthuoc.Select(TPhieuCapphatChitiet.Columns.IdChitiet + ">0").Length > 0;
                }
                grdPresDetail.RootTable.Columns[TPhieuCapphatChitiet.Columns.ThucLinh].Visible = _dacapphat && grdPresDetail.GetDataRows().Length > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_THUCLINH_PHATTHUOC_BENHNHAN", "0", false) == "1" && THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC", "0", false) == "1";
                grdPresDetail.RootTable.Columns[TPhieuCapphatChitiet.Columns.SoLuongtralai].Visible = _dacapphat && grdPresDetail.GetDataRows().Length > 0 && !grdPresDetail.RootTable.Columns[TPhieuCapphatChitiet.Columns.ThucLinh].Visible   && THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC", "0", false) == "1";
            }
            catch (Exception ex)
            {
            }
        }
        void grdPresDetail_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdPresDetail)) return;
                int TrangthaiTralai = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdPresDetail, TPhieuCapphatChitiet.Columns.TrangthaiTralai), 0);
                int IdPhieutralai = Utility.Int32Dbnull(Utility.getValueOfGridCell(grdPresDetail, TPhieuCapphatChitiet.Columns.IdPhieutralai), 0);
                long IdChitiet = Utility.Int64Dbnull(Utility.getValueOfGridCell(grdPresDetail, TPhieuCapphatChitiet.Columns.IdChitiet), 0);
                int soluong = Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.SoLuong].Value, -1);
                int soluongtralai = 0;
                int thuclinh = soluong;
                if (IdChitiet <= 0)
                {
                    Utility.ShowMsg("Chi tiết này chưa được tổng hợp lĩnh thuốc nội trú nên bạn không thể sửa lại số lượng thực lĩnh(hoặc số lượng trả lại). Đề nghị kiểm tra lại");
                    return;
                }
                if (TrangthaiTralai == 1)
                {
                    Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                    e.Cancel = true;
                    return;
                }
                if (TrangthaiTralai == 2)
                {
                    Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa và đã trả lại kho thuốc nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                    e.Cancel = true;
                    return;
                }
                if (IdPhieutralai > 0)
                {
                    Utility.ShowMsg("Chi tiết này đã được tổng hợp thành phiếu trả thuốc thừa nên bạn không thể thay đổi lại thông tin số lượng thực lĩnh(hoặc số lượng trả lại) nữa\nMời bạn kiểm tra lại");
                    e.Cancel = true;
                    return;
                }
                int soluongsua = Utility.Int32Dbnull(e.Value, 0);
                if (soluongsua > soluong)
                {
                    Utility.ShowMsg(string.Format("Số lượng thực lĩnh (hoặc trả lại) phải nhỏ hơn hoặc bằng số lượng kê {0}", soluong.ToString()));
                    e.Cancel = true;
                    return;
                }
                grdPresDetail.CurrentRow.BeginEdit();
                if (e.Column.Key == TPhieuCapphatChitiet.Columns.SoLuongtralai)
                {
                    soluongtralai = soluongsua;
                    thuclinh = soluong - soluongsua;
                    grdPresDetail.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.ThucLinh].Value = soluong - soluongsua;
                }
                else
                {
                    thuclinh = soluongsua;
                    soluongtralai = soluong - soluongsua;
                    grdPresDetail.CurrentRow.Cells[TPhieuCapphatChitiet.Columns.SoLuongtralai].Value = soluong - soluongsua;
                }
                grdPresDetail.CurrentRow.EndEdit();
                grdPresDetail.Refetch();
                CapphatThuocKhoa.CapnhatThuclinh(
                    IdChitiet
                    , thuclinh
                    , soluongtralai
                    );
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

       

        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaluotkham.Text) != "")
            {
                txtPatient_Code.Text = txtMaluotkham.Text;
                txtPatient_Code_KeyDown(txtPatient_Code, e);
            }
        }

        void grdBuongGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!Utility.isValidGrid(grdBuongGiuong))
            {
                return;
            }
            if (Utility.Int32Dbnull(Utility.getValueOfGridCell(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.IdKhoanoitru.ToString()), 0) !=idKhoaNoitru)
            {
                Utility.ShowMsg(string.Format( "Khoa lập phiếu điều trị {0} khác so với khoa tìm kiếm {1} nên bạn không thể lập phiếu điều trị. Đề nghị chọn lại",Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru"),tenkhoanoitru));
                return;
            }
            if (Utility.Int32Dbnull(Utility.getValueOfGridCell(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.Id.ToString()), 0) != Utility.Int32Dbnull(objLuotkham.IdRavien, 0))
            {
                string khoabuonggiuong = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru") + " - " + Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_buong") + " - " + Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_giuong");
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn lập phiếu điều trị cho thời điểm Bệnh nhân nằm tại " + khoabuonggiuong + " đang chọn hay không?", "Cảnh báo", true))
                    return;
            }
            GetNoitruPhanbuonggiuong();
            LaydanhsachPhieudieutri();
            uiTabPhieudieutri.SelectedIndex = 0;
            
        }
        void GetNoitruPhanbuonggiuong()
        {
            objNoitruPhanbuonggiuong = null;
            if (!Utility.isValidGrid(grdBuongGiuong))
            {
                txtKhoanoitru_lapphieu.Clear();
                txtBuong_lapphieu.Clear();
                txtGiuong_lapphieu.Clear();
                return;
            }
            objNoitruPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(Utility.getValueOfGridCell(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.Id.ToString()), 0));
            txtKhoanoitru_lapphieu.Text = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru");
            txtBuong_lapphieu.Text = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_buong");
            txtGiuong_lapphieu.Text = Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_giuong");
        }
        void txtChedodinhduong__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtChedodinhduong.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChedodinhduong.myCode;
                txtChedodinhduong.Init();
                txtChedodinhduong.SetCode(oldCode);
                txtChedodinhduong.Focus();
            }
        }
       
        void txtHoly__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtHoly.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtHoly.myCode;
                txtHoly.Init();
                txtHoly.SetCode(oldCode);
                txtHoly.Focus();
            }   
        }

        void dtpNgaylapphieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LaydanhsachPhieudieutri();
            }
        }

        void chkViewAll_CheckedChanged(object sender, EventArgs e)
        {
            LaydanhsachPhieudieutri();
        }

        void txtSongay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PropertyLib._NoitruProperties.Songayhienthi = Utility.Int32Dbnull(txtSongay.Text, 0);
                PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
                LaydanhsachPhieudieutri();
            }
        }

        void cmdAdd_Click(object sender, EventArgs e)
        {
             if (txtHoly.myCode == "-1")
             {
                 txtHoly.Focus();
                 return;
             }
             if (txtChedodinhduong.myCode == "-1")
             {
                 txtChedodinhduong.Focus();
                 return;
             }
             if (m_dtChedoDinhduong.Select(NoitruPhieudinhduong.Columns.MaDinhduong + "='" + txtChedodinhduong.myCode + "'").Length <= 0)
             {
                 NoitruPhieudinhduong _newItem = new NoitruPhieudinhduong();
                 _newItem.IdPhieudieutri = objPhieudieutri.IdPhieudieutri;
                 _newItem.MaHoly = txtHoly.myCode;
                 _newItem.MaDinhduong = txtChedodinhduong.myCode;
                 _newItem.NgayTao = globalVariables.SysDate;
                 _newItem.NguoiTao = globalVariables.UserName;
                 _newItem.NgayLap = objPhieudieutri.NgayDieutri.Value;
                 _newItem.IsNew = true;
                 _newItem.Save();
                 DataRow newDr = m_dtChedoDinhduong.NewRow();
                 Utility.FromObjectToDatarow(_newItem, ref newDr);
                 newDr["ten_holy"] = txtHoly.Text;
                 newDr["ten_dinhduong"] = txtChedodinhduong.Text;
                 m_dtChedoDinhduong.Rows.Add(newDr);
                 m_dtChedoDinhduong.AcceptChanges();
                 Utility.GotoNewRowJanus(grdChedoDinhduong, NoitruPhieudinhduong.Columns.Id, _newItem.Id.ToString());

                
             }
             ModifyCommmands();
             txtChedodinhduong.SetCode("-1");
             txtChedodinhduong.Focus();
        }

        void grdChedoDinhduong_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "cmdXoa")
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa chế độ dinh dưỡng đang chọn?", "Xác nhận xóa", true))
                {
                    if (grdChedoDinhduong.GetCheckedRows().Length <= 0)
                    {
                        grdChedoDinhduong.CurrentRow.IsChecked = true;
                    }
                    XoaDinhduong(grdChedoDinhduong.CurrentRow);
                    ModifyCommmands();
                }
            }
        }

        void cmdXoaDinhduong_Click(object sender, EventArgs e)
        {
            int _checkedCount = grdChedoDinhduong.GetCheckedRows().Length;
            if (!Utility.isValidGrid(grdChedoDinhduong) && _checkedCount <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một dòng chế độ dinh dưỡng cần xóa");
                return;
            }
           
            if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa các chế độ dinh dưỡng đang chọn?", "Xác nhận xóa", true))
            {
                if (grdChedoDinhduong.GetCheckedRows().Length <= 0)
                {
                    grdChedoDinhduong.CurrentRow.IsChecked = true;
                }
                XoaDinhduong();
                ModifyCommmands();
            }
        }

        void lnkSize_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._DynamicInputProperties);
            if (_Properties.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ShowResult();
            }
        }

        void mnuShowResult_Click(object sender, EventArgs e)
        {
            mnuShowResult.Tag = mnuShowResult.Checked ? "1" : "0";
            if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
            {
                if (mnuShowResult.Checked)
                    Utility.ShowColumns(grdAssignDetail, lstResultColumns);
                else
                    Utility.ShowColumns(grdAssignDetail, lstVisibleColumns);
            }
            else
                grdAssignDetail_SelectionChanged(grdAssignDetail, e);
        }

        void cmdChuyengoi_Click(object sender, EventArgs e)
        {
            frm_chuyenVTTHvaotronggoiDV _chuyenVTTHvaotronggoiDV = new frm_chuyenVTTHvaotronggoiDV();
            _chuyenVTTHvaotronggoiDV.objLuotkham = objLuotkham;
            _chuyenVTTHvaotronggoiDV.ShowDialog();
            if (!_chuyenVTTHvaotronggoiDV.m_blnCancel)
            {
                LaythongtinPhieudieutri();
            }
        }
        void grdChandoan_SelectionChanged(object sender, EventArgs e)
        {
            string NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI", "1", false);
            if (objLuotkham == null || !Utility.isValidGrid(grdChandoan) || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "1" && objPhieudieutri == null)))
            {
                ClearChandoan();
                EnableChandoan(false);
                ModifyCommmands();
                return;
            }
            else
            {
                txtIdChandoan.Text = Utility.getValueOfGridCell(grdChandoan, KcbChandoanKetluan.Columns.IdChandoan).ToString();
                KcbChandoanKetluan objKcbChandoanKetluan = KcbChandoanKetluan.FetchByID(Utility.Int32Dbnull(txtIdChandoan.Text, -1));
                if (objKcbChandoanKetluan != null)
                {
                    txtMach.Text = objKcbChandoanKetluan.Mach;
                    txtNhietDo.Text = objKcbChandoanKetluan.Nhietdo;
                    txtHa.Text = objKcbChandoanKetluan.Huyetap;
                    txtNhipTho.Text = objKcbChandoanKetluan.Nhiptho;
                    txtNhipTim.Text = objKcbChandoanKetluan.Nhiptim;
                    txtChanDoan._Text = objKcbChandoanKetluan.Chandoan;
                    txtChanDoanKemTheo._Text = objKcbChandoanKetluan.ChandoanKemtheo;
                    dtpNgaychandoan.Value = objKcbChandoanKetluan.NgayChandoan;
                    txtMaBenhChinh.SetCode( objKcbChandoanKetluan.MabenhChinh);
                    dt_ICD_PHU.Clear();
                    string dataString = objKcbChandoanKetluan.MabenhPhu;
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
                    cmdXoachandoan.Enabled = cmdSuachandoan.Enabled = Utility.Byte2Bool(objKcbChandoanKetluan.Noitru);
                }
                else
                {
                    ClearChandoan();
                    EnableChandoan(false);
                }
            }
        }
        void ClearChandoan()
        {
            txtChanDoan.ResetText();
            txtChanDoanKemTheo.ResetText();
            txtMach.Clear();
            txtNhietDo.Clear();
            txtHa.Clear();
            txtNhipTho.Clear();
            txtNhipTim.Clear();
            dtpNgaychandoan.Value = globalVariables.SysDate;
            txtMaBenhphu.Clear();
            txtMaBenhChinh.Clear();
            if (dt_ICD_PHU != null) dt_ICD_PHU.Clear();
        }
        void EnableChandoan(bool _enable)
        {


            txtMach.Enabled = _enable;
            txtNhietDo.Enabled = _enable;
            txtChanDoan.Enabled = _enable;
            txtChanDoanKemTheo.Enabled = _enable;
            txtHa.Enabled = _enable;
            txtNhipTim.Enabled = _enable;
            txtNhipTho.Enabled = _enable;
            dtpNgaychandoan.Enabled = _enable;
            txtMaBenhphu.Enabled = _enable;
            txtMaBenhChinh.Enabled = _enable;
            
        }
        bool isValidChandoan()
        {
            if (objLuotkham != null &&  Utility.DoTrim(txtMaBenhChinh.MyCode) == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập ít nhất Mã bệnh chính để tạo dữ liệu chẩn đoán", true);
                txtMaBenhChinh.Focus();
                return false;
            }
            return true;
            
        }
        void cmdGhichandoan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidChandoan()) return;
                KcbChandoanKetluan objKcbChandoanKetluan = new KcbChandoanKetluan();
                if (m_enActChandoan == action.Update)
                {
                    objKcbChandoanKetluan = KcbChandoanKetluan.FetchByID(Utility.Int32Dbnull(txtIdChandoan.Text, -1));
                    objKcbChandoanKetluan.MarkOld();
                    objKcbChandoanKetluan.IsNew = false;
                }
                else
                {
                    objKcbChandoanKetluan = new KcbChandoanKetluan();
                    objKcbChandoanKetluan.IsNew = true;
                }
                objKcbChandoanKetluan.MaLuotkham = objLuotkham.MaLuotkham;
                objKcbChandoanKetluan.IdBenhnhan = objLuotkham.IdBenhnhan;
                objKcbChandoanKetluan.MabenhChinh = Utility.sDbnull(txtMaBenhChinh.MyCode, "");
                objKcbChandoanKetluan.Nhommau = txtNhommau.Text;
                objKcbChandoanKetluan.Nhietdo = Utility.sDbnull(txtNhietDo.Text);
                objKcbChandoanKetluan.Huyetap = txtHa.Text;
                objKcbChandoanKetluan.Mach = txtMach.Text;
                objKcbChandoanKetluan.Nhiptim = Utility.sDbnull(txtNhipTim.Text);
                objKcbChandoanKetluan.Nhiptho = Utility.sDbnull(txtNhipTho.Text);
                objKcbChandoanKetluan.Chieucao = Utility.sDbnull(txtChieucao.Text);
                objKcbChandoanKetluan.Cannang = Utility.sDbnull(txtCannang.Text);
                objKcbChandoanKetluan.HuongDieutri = "";
                objKcbChandoanKetluan.SongayDieutri = 0;

                if (cboBSDieutri.SelectedIndex > 0)
                    objKcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(cboBSDieutri.SelectedValue, -1);
                else
                {
                    objKcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                }
                string sMaICDPHU = GetDanhsachBenhphu();
                objKcbChandoanKetluan.MabenhPhu = Utility.sDbnull(sMaICDPHU.ToString(), "");
                objKcbChandoanKetluan.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                objKcbChandoanKetluan.IdBuong = objLuotkham.IdBuong;
                objKcbChandoanKetluan.IdGiuong = objLuotkham.IdGiuong;
                objKcbChandoanKetluan.IdBuonggiuong = objLuotkham.IdRavien;

                objKcbChandoanKetluan.IdKham = objPhieudieutri == null ? -1 : objPhieudieutri.IdPhieudieutri;
                objKcbChandoanKetluan.NgayTao = dtpCreatedDate.Value;
                objKcbChandoanKetluan.NguoiTao = globalVariables.UserName;
                objKcbChandoanKetluan.NgayChandoan = dtpNgaychandoan.Value;
                objKcbChandoanKetluan.Ketluan = "";
                objKcbChandoanKetluan.Chandoan = Utility.ReplaceString(txtChanDoan.Text);
                objKcbChandoanKetluan.ChandoanKemtheo = Utility.sDbnull(txtChanDoanKemTheo.Text);
                objKcbChandoanKetluan.IdPhieudieutri = objPhieudieutri == null ? -1 : objPhieudieutri.IdPhieudieutri;
                objKcbChandoanKetluan.Noitru = 1;
                objKcbChandoanKetluan.Save();
                DataRow[] arrDr = m_dtChandoanKCB.Select(KcbChandoanKetluan.Columns.IdChandoan + "=" + objKcbChandoanKetluan.IdChandoan.ToString());
                if (arrDr.Length > 0)
                {
                    Utility.FromObjectToDatarow(objKcbChandoanKetluan, ref arrDr[0]);
                    arrDr[0]["sNgay_chandoan"] = dtpNgaychandoan.Text;
                    Utility.GotoNewRowJanus(grdChandoan, KcbChandoanKetluan.Columns.IdChandoan, objKcbChandoanKetluan.IdChandoan.ToString());
                    m_dtChandoanKCB.AcceptChanges();
                }
                else
                {
                    DataRow newDr = m_dtChandoanKCB.NewRow();
                    Utility.FromObjectToDatarow(objKcbChandoanKetluan, ref newDr);
                    newDr["sNgay_chandoan"] = dtpNgaychandoan.Text;
                    m_dtChandoanKCB.Rows.Add(newDr);
                    m_dtChandoanKCB.AcceptChanges();
                    Utility.GotoNewRowJanus(grdChandoan, KcbChandoanKetluan.Columns.IdChandoan, objKcbChandoanKetluan.IdChandoan.ToString());
                }
                EnableChandoan(false);
                cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = false;
                ModifyCommmands();
                grdChandoan_SelectionChanged(grdChandoan, e);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
       
        void cmdHuychandoan_Click(object sender, EventArgs e)
        {
            m_enActChandoan = action.FirstOrFinished;
            EnableChandoan(false);
            ModifyCommmands();
            grdChandoan_SelectionChanged(grdChandoan, e);
            cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = false;
            cmdHuychandoan.SendToBack(); 
        }

        void cmdXoachandoan_Click(object sender, EventArgs e)
        {
            string NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI", "1", false);
            if(objLuotkham!=null && !Utility.isValidGrid(grdChandoan) && (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null)))
            {
                Utility.ShowMsg("Bạn phải chọn chẩn đoán trên lưới trước khi thực hiện xóa.");
                return;
            }
            if (grdChandoan.GetCheckedRows().Length <= 0)
            {
                grdChandoan.CurrentRow.IsChecked = true;
            }
            XoaChandoan();
            ModifyCommmands(); 
        }

        void cmdSuachandoan_Click(object sender, EventArgs e)
        {
          cmdThemchandoan.Enabled=  cmdSuachandoan.Enabled = cmdXoachandoan.Enabled = false;
          cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = true;
          cmdHuychandoan.BringToFront();
          EnableChandoan(true);
          m_enActChandoan = action.Update;
          txtMach.Focus();
        }

        void cmdThemchandoan_Click(object sender, EventArgs e)
        {
            cmdThemchandoan.Enabled = cmdSuachandoan.Enabled = cmdXoachandoan.Enabled = false;
            cmdGhichandoan.Enabled = cmdHuychandoan.Enabled = true;
            cmdHuychandoan.BringToFront();
            ClearChandoan();
            EnableChandoan(true);
            m_enActChandoan = action.Insert;
            dtpNgaychandoan.Value = globalVariables.SysDate;
            txtMach.Focus();
        }

        void grdGoidichvu_DoubleClick(object sender, EventArgs e)
        {
            grdGoidichvu_SelectionChanged(grdGoidichvu,e);
        }

        void cmdInphieuVT_tronggoi_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdVTTH_tronggoi)) return;
            int Pres_ID = Utility.Int32Dbnull(grdVTTH_tronggoi.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            PrintPres(Pres_ID,"PHIẾU VẬT TƯ TRONG GÓI");
        }

        void cmdXoaphieuVT_tronggoi_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!IsValidVTTH_delete_trongoi()) return;
            PerformActionDeleteVTTH_tronggoi();
            ModifyCommmands(); 
        }

        void cmdSuaphieuVT_tronggoi_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdSuaphieuVT_tronggoi.Enabled) return;
            SuaphieuVattu_tronggoi(); 
        }

        void cmdThemphieuVT_tronggoi_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdThemphieuVT_tronggoi.Enabled) return;
            //if (grdVTTH_tronggoi.GetDataRows().Length <= 0 || !CanUpdateVTTH())
            //{
                ThemphieuVattu_tronggoi();
            //}
            //else
            //{
            //    SuaphieuVattu_tronggoi();
            //}
        }
        /// <summary>
        /// Kiểm tra xem còn phiếu VTTH nào chưa cấp phát
        /// </summary>
        /// <returns></returns>
        bool CanUpdateVTTH()
        {
            //Lấy về phiếu VTTH trong gói chưa được cấp phát vật tư
            var q = from p in m_dtVTTH_tronggoi.AsEnumerable()
                    where Utility.Int32Dbnull(p[KcbDonthuocChitiet.Columns.TrangThai], 0) == 0
                    select p;
            return q.Count() > 0;
        }
        void grdGoidichvu_SelectionChanged(object sender, EventArgs e)
        {
            LayVTTHtronggoi();
            ModifyCommmands();
        }
        void LayVTTHtronggoi()
        {
            try
            {
                if (Utility.isValidGrid(grdGoidichvu))
                {
                    m_dtVTTH_tronggoi =
                     _KCB_THAMKHAM.NoitruLaythongtinVTTHTrongoi(Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                              Utility.sDbnull(malankham, ""),
                                                              Utility.Int32Dbnull(txtIdPhieudieutri.Text), Utility.Int32Dbnull(Utility.getValueOfGridCell(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChitietchidinh))).Tables[0];

                    Utility.SetDataSourceForDataGridEx(grdVTTH_tronggoi, m_dtVTTH_tronggoi, false, true, "", KcbDonthuocChitiet.Columns.SttIn);
                }
                else
                {
                    if (m_dtVTTH_tronggoi != null) m_dtVTTH_tronggoi.Clear();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void cmdNoitruConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._NoitruProperties);
            frm.ShowDialog();
            
        }

        void grdPresDetail_SelectionChanged(object sender, EventArgs e)
        {
            ModifyCommmands();
        }

        void cmdXoaphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!IsValidVTTH_delete()) return;
            PerformActionDeleteVTTH();
            ModifyCommmands(); 
        }

        void cmdInphieuVT_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdVTTH)) return;
            int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            PrintPres(Pres_ID,"PHIẾU VẬT TƯ NGOÀI GÓI");
        }

        void cmdSuaphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdSuaphieuVT.Enabled) return;
            SuaphieuVattu(); 
        }

        void cmdThemphieuVT_Click(object sender, EventArgs e)
        {
            if (!CheckPatientSelected()) return;
            if (!cmdThemphieuVT.Enabled) return;
            //if (!ExistsDonThuoc())
            //{
                ThemphieuVattu();
            //}
            //else
            //{
               // SuaphieuVattu();
            //}
        }
       
        void cmdIngoiDV_Click(object sender, EventArgs e)
        {
            try
            {
                string mayin = "";
                int v_AssignId = Utility.Int32Dbnull(grdGoidichvu.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                string v_AssignCode = Utility.sDbnull(grdGoidichvu.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                string nhomincls = "ALL";
                
                //if (cboServicePrint.SelectedIndex > 0)
                //{
                //    nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");

                //}

                KCB_INPHIEU.InphieuChidinhCLS((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, v_AssignId, v_AssignCode, nhomincls, 0, chkintachgoidichvu.Checked, ref mayin);
                if (mayin != "") cboLaserPrinters.Text = mayin;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void cmdXoagoiDV_Click(object sender, EventArgs e)
        {
            if (!IsValidGoidichvu()) return;
            XoaGoidichvu();
            ModifyCommmands();
        }

        void cmdSuagoiDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckPatientSelected()) return;
                if (!InValiUpdateChiDinh()) return;
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("GOI", 1);
                frm.HosStatus = 1;
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = Utility.sDbnull(grdGoidichvu.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    ModifyCommmands();
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
        }

        void cmdThemgoiDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckPatientSelected()) return;
                if (!cmdInsertAssign.Enabled) return;
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("GOI", 1);
                frm.txtAssign_ID.Text = "-100";
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.m_eAction = action.Insert;
                frm.txtAssign_ID.Text = "-1";
                frm.HosStatus = 1;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {

                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                        grdGoidichvu.GroupMode = GroupMode.Collapsed;
                    Utility.GotoNewRowJanus(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChidinh, frm.txtAssign_ID.Text);
                    ModifyCommmands();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }

        void cmdCauHinh_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._NoitruProperties);
            _Properties.ShowDialog();
            Cauhinh();
        }
        void Cauhinh()
        {
            txtSongay.Text = PropertyLib._NoitruProperties.Songayhienthi.ToString();
            string _val=THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHIKE_VTTH_THEOGOI","0",false);
            pnlVTTHTronggoi.Height = _val == "1" ? 50 : 0;
            pnlVTTHTronggoi.Visible = _val == "1";
        }
        void cmdSaochep_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn phải chọn bệnh nhân trước khi thực hiện sao chép phiếu điều trị");
                return;
            }
            if (Utility.Int32Dbnull(objLuotkham.IdKhoanoitru, 0) != idKhoaNoitru)
            {
                Utility.ShowMsg(string.Format("Khoa nội trú hiện tại của Bệnh nhân đang khác khoa tìm kiếm nên bạn không được phép sao chép phiếu điều trị.\nBạn có thể chọn vào thời điểm nằm nội trú tại khoa "+tenkhoanoitru+"Sau đó nhấn đúp chuột để thêm mới(hoặc sửa) phiếu điều trị cho khoảng thời gian đó.", Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru"), tenkhoanoitru));
                return;
            }
            frm_Saochep_phieudieutri _Saochep_phieudieutri = new frm_Saochep_phieudieutri();
            _Saochep_phieudieutri.objLuotkham = objLuotkham;
            _Saochep_phieudieutri._OnCopyComplete += new frm_Saochep_phieudieutri.OnCopyComplete(_Saochep_phieudieutri__OnCopyComplete);
            _Saochep_phieudieutri.ShowDialog();
        }

        void _Saochep_phieudieutri__OnCopyComplete()
        {
            HienthithongtinBN();
        }

        void cmdInphieudieutri_Click(object sender, EventArgs e)
        {
            frm_InPhieudieutri _InPhieudieutri = new frm_InPhieudieutri();
            _InPhieudieutri.objLuotkham = this.objLuotkham;
            _InPhieudieutri.ShowDialog();
        }

        void cmdxoaphieudieutri_Click(object sender, EventArgs e)
        {
            XoaPhieudieutri();
        }

        void cmdSuaphieudieutri_Click(object sender, EventArgs e)
        {
            SuaPhieudieutri();
        }

        void cmdthemphieudieutri_Click(object sender, EventArgs e)
        {
            Themphieudieutri();
        }
        bool IsValidCommon()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn Bệnh nhân!");
                return false;
            }
            if (Utility.Byte2Bool( objNoitruPhanbuonggiuong.TrangthaiChotkhoa ))
            {
                Utility.ShowMsg("Thời điểm nằm buồng giường bạn đang chọn đã được chốt khoa nên bạn không thể thao tác");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để ra viện nên bạn không thể thao tác");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú để ra viện nên bạn không thể thao tácn");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã kết thúc điều trị nội trú(Đã thanh toán xong) nên bạn không thể thao tác");
                return false;
            }
            return true;
        }
        void Themphieudieutri()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                //Kiểm tra lần nhập viện hoặc chuyển khoa gần nhất phải được phân buồng giường trước khi ra viện
                if (objNoitruPhanbuonggiuong == null)
                {
                    uiTabPhieudieutri.SelectedTab = uiTabPhieudieutri.TabPages["Buonggiuong"];
                    Utility.ShowMsg("Bạn cần chọn Thông tin Khoa-Buồng-Giường(Thời điểm) lập phiếu điều trị cho Bệnh nhân");
                    return;
                }
                bool isValid = Utility.Int16Dbnull(objNoitruPhanbuonggiuong.IdBuong, 0) > 0 && Utility.Int16Dbnull(objNoitruPhanbuonggiuong.IdBuong, 0) > 0;
                if (!isValid)
                {
                    Utility.ShowMsg("Hệ thống phát hiện Bệnh nhân chưa được phân buồng giường cho lần chuyển khoa gần nhất nên bạn không thể lập phiếu điều trị được.");
                    return;
                }
                if (Utility.Int32Dbnull(Utility.getValueOfGridCell(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.IdKhoanoitru.ToString()), 0) != idKhoaNoitru)
                {
                    Utility.ShowMsg(string.Format("Khoa lập phiếu điều trị {0} khác so với khoa tìm kiếm {1} nên bạn không thể lập phiếu điều trị. Đề nghị chọn lại", Utility.GetValueFromGridColumn(grdBuongGiuong, "ten_khoanoitru"), tenkhoanoitru));
                    return;
                }

                if (!IsValidCommon()) return;
                if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && !new noitru_TamungHoanung().DathanhtoanhetNgoaitru(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham))
                {
                    Utility.SetMsg(lblMsg, "Bệnh nhân Dịch vụ chưa thanh toán hết tiền ngoại trú nên không được phép lập phiếu điều trị", true);
                    return ;
                }
                frm_themphieudieutri frm = new frm_themphieudieutri();
                frm.txtTreat_ID.Text = "-1";
                frm.grdList = grdPhieudieutri;
                frm.em_Action = action.Insert;
                frm.p_TreatMent = m_dtPhieudieutri;
                frm.objBuongGiuong = objNoitruPhanbuonggiuong;
                frm.objLuotkham = objLuotkham;
                frm.objPhieudieutri = new NoitruPhieudieutri();
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    if (uiTabPhieudieutri.SelectedTab != tabPagePhieuDieuTri)
                        uiTabPhieudieutri.SelectedTab = tabPagePhieuDieuTri;
                    Utility.GotoNewRowJanus(grdPhieudieutri, NoitruPhieudieutri.Columns.IdPhieudieutri,
                                            Utility.sDbnull(frm.txtTreat_ID.Text, -1));
                    //Thêm các đơn thuốc sao chép
                    //if (frm.lstPresID.Count > 0)
                    //{
                    //    KcbDonthuocCollection lstpres = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).In(frm.lstPresID).ExecuteAsCollection<KcbDonthuocCollection>();
                    //    if (lstpres.Count > 0)
                    //        if (new noitru_phieudieutri().SaochepDonthuoc(_CurIdPhieudieutri, objLuotkham, lstpres, frm.objPhieudieutri.NgayDieutri.Value) == ActionResult.Success)
                    //        {
                    //            grdPhieudieutri_SelectionChanged(grdPhieudieutri, new EventArgs());
                    //        }
                    //        else
                    //        {
                    //            Utility.ShowMsg("Lỗi khi sao chép đơn thuốc. Liên hệ VSS để được trợ giúp!");
                    //        }
                    //}
                }
                ModifyCommandPhieudieutri();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi trong quá trình :{0}", exception));
                }
            }
        }
        private void SuaPhieudieutri()
        {
            try
            {
                if (!CheckPatientSelected()) return;
                int ID = Utility.Int32Dbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1);
                
                frm_themphieudieutri frm = new frm_themphieudieutri();
                frm.objBuongGiuong = null;//Tự động nạp tại form load
                frm.objLuotkham = objLuotkham;
                frm.grdList = grdPhieudieutri;
                frm.em_Action = action.Update;
                frm.p_TreatMent = m_dtPhieudieutri;
                frm.txtTreat_ID.Text = ID.ToString();
                frm.objPhieudieutri = NoitruPhieudieutri.FetchByID(ID);
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    //if (frm.lstPresID.Count > 0)
                    //{
                    //    KcbDonthuocCollection lstpres = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).In(frm.lstPresID).ExecuteAsCollection<KcbDonthuocCollection>();
                    //    if (lstpres.Count > 0)
                    //        if (new noitru_phieudieutri().SaochepDonthuoc(_CurIdPhieudieutri, objLuotkham, lstpres, frm.objPhieudieutri.NgayDieutri.Value) == ActionResult.Success)
                    //        {
                    //            grdPhieudieutri_SelectionChanged(grdPhieudieutri, new EventArgs());
                    //        }
                    //        else
                    //        {
                    //            Utility.ShowMsg("Lỗi khi sao chép đơn thuốc. Liên hệ VSS để được trợ giúp!");
                    //        }
                    //}
                }
                ModifyCommandPhieudieutri();
            }
            catch (Exception)
            {
                //throw;
            }
        }
        private void XoaPhieudieutri()
        {


            try
            {
                List<int> lstIdPhieudieutri = new List<int>();
                if (grdPhieudieutri.GetCheckedRows().Length < 0)
                {
                    if (Utility.isValidGrid(grdPhieudieutri))
                    {
                        Utility.ShowMsg("Bạn phải thực hiện chọn một phiếu điều trị để xóa");
                        grdPhieudieutri.Focus();
                        return;
                    }
                }
                foreach (GridEXRow gridExRow in grdPhieudieutri.GetCheckedRows())
                {
                    int TreatId = Utility.Int32Dbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1);
                    if (!IsValidBeforeDelete(TreatId))
                    {
                        return;
                    }
                }
                lstIdPhieudieutri = (from p in grdPhieudieutri.GetCheckedRows()
                                     select Utility.Int32Dbnull(p.Cells[NoitruPhieudieutri.Columns.IdPhieudieutri].Value, -1)).ToList<int>();
                string _question = grdPhieudieutri.GetCheckedRows().Length > 0 ? "Bạn có chắc chắn muốn xóa các phiếu điều trị đang chọn hay không?"
                    : string.Format("Bạn có chắc chắn muốn xóa phiếu điều trị của ngày {0} hay không?", Utility.sDbnull(grdPhieudieutri.GetValue("sngay_dieutri")));
                if (Utility.AcceptQuestion(_question,
                                           "Thông báo", true))
                {
                    ActionResult actionResult = new noitru_phieudieutri().Xoaphieudieutri(lstIdPhieudieutri);
                    if (grdPhieudieutri.GetCheckedRows().Length > 0)
                    {
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                foreach (GridEXRow gridExRow in grdPhieudieutri.GetCheckedRows())
                                {
                                    gridExRow.Delete();
                                    grdPhieudieutri.UpdateData();
                                    grdPhieudieutri.Refresh();
                                    m_dtPhieudieutri.AcceptChanges();
                                }
                                Utility.SetMsg(lblMsg, "Xóa phiếu điều trị thành công", false);
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình xóa thông tin", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.IsNotUserName:
                                Utility.ShowMsg("Phiếu chỉ định này không phải của bạn, Bạn không có quyền xóa",
                                                "Thông báo", MessageBoxIcon.Warning);
                                break;
                        }

                    }
                    else//Xóa dòng hiện tại
                    {
                        XoaPhieuDieuTri(Utility.Int32Dbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1));
                    }
                }
            }
            catch (Exception)
            {
                // Utility.ShowMsg("Lỗi trong quá trình xóa thông tin ","Thông báo lỗi",MessageBoxIcon.Error);
            }
            finally
            {
                grdPhieudieutri_SelectionChanged(grdPhieudieutri, new EventArgs());
                ModifyCommandPhieudieutri();
                ModifyCommmands();
            }
        }
      
        void ModifyCommandPhieudieutri()
        {
            cmdthemphieudieutri.Enabled = Utility.isValidGrid(grdPatientList) && objLuotkham !=null && objLuotkham.TrangthaiNoitru <= 2;
            cmdSuaphieudieutri.Enabled = Utility.isValidGrid(grdPhieudieutri) && objPhieudieutri != null && objLuotkham != null && objLuotkham.TrangthaiNoitru <= 2;
            cmdxoaphieudieutri.Enabled = Utility.isValidGrid(grdPhieudieutri) && objPhieudieutri != null && objLuotkham != null && objLuotkham.TrangthaiNoitru <= 2;
            cmdInphieudieutri.Enabled = Utility.isValidGrid(grdPhieudieutri) && objPhieudieutri != null && objLuotkham != null;// && objLuotkham.TrangthaiNoitru <= 2;
            cmdSaochep.Enabled = Utility.isValidGrid(grdPatientList) && objLuotkham != null && objLuotkham.TrangthaiNoitru <= 2;
        }
        void grdPhieudieutri_SelectionChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
            if (PropertyLib._NoitruProperties.ViewOnClick && !_buttonClick) 
                Selectionchanged();
            _buttonClick = false;
        }
        
        void Selectionchanged()
        {
            try
            {
                if (Utility.isValidGrid(grdPhieudieutri))
                {
                    _CurIdPhieudieutri = Utility.Int32Dbnull(Utility.sDbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1), -1);
                    txtIdPhieudieutri.Text = _CurIdPhieudieutri.ToString();
                    objPhieudieutri = NoitruPhieudieutri.FetchByID(_CurIdPhieudieutri);
                    if (objPhieudieutri != null)
                    {
                        dtpNgaylapphieu.Value = objPhieudieutri.NgayDieutri.Value;
                        cboBSDieutri.SelectedIndex = Utility.GetSelectedIndex(cboBSDieutri, Utility.sDbnull(objPhieudieutri.IdBacsi, "-1"));
                        BuildContextMenu();
                        LaythongtinPhieudieutri();
                    }
                    else
                    {
                        txtIdPhieudieutri.Text = "-1";
                        grdPresDetail.DataSource = null;
                        grdAssignDetail.DataSource = null;
                        grdVTTH.DataSource = null;
                    }
                }
                else
                {
                    txtIdPhieudieutri.Text = "-1";
                    grdPresDetail.DataSource = null;
                    grdAssignDetail.DataSource = null;
                    grdVTTH.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return;
            }
            finally
            {
                ModifyCommandPhieudieutri();
                ModifyCommmands();
            }
        }
        int _CurIdPhieudieutri = -1;
       
        int date2Int(string date)
        {
            string[] arrdate = date.Split('/');
            return Utility.Int32Dbnull(arrdate[2] + arrdate[1] + arrdate[0], 0);
        }
        void BuildContextMenu()
        {
            ctxPhieudieutri.Items.Clear();


            if (!Utility.isValidGrid(grdPhieudieutri))
            {
                ToolStripMenuItem newItem = new ToolStripMenuItem("Bạn cần chọn phiếu điều trị trước khi thực hiện sao chép");
                ctxPhieudieutri.Items.Add(newItem);
                return;
            }
            _CurIdPhieudieutri = Utility.Int32Dbnull(grdPhieudieutri.CurrentRow.Cells[NoitruPhieudieutri.Columns.IdPhieudieutri].Value.ToString());
            string _date = grdPhieudieutri.CurrentRow.Cells["sngay_dieutri"].Value.ToString();
            int _intDate = date2Int(_date);
            DataRow[] arrDR = m_dtPhieudieutri.Select("1=1", "sngay_dieutri DESC");
            //foreach (GridEXRow row in grdPhieudieutri.GetDataRows())
            int idx = 1;
            foreach (DataRow row in arrDR)
            {
                string _datetempt = row["sngay_dieutri"].ToString();
                int _intDatetempt = date2Int(_datetempt);
                int _IdPhieudieutri = Utility.Int32Dbnull(row[NoitruPhieudieutri.Columns.IdPhieudieutri].ToString(), -1);
                if (_IdPhieudieutri != _CurIdPhieudieutri &&
                    (PropertyLib._NoitruProperties.Songaysaochep == 0 || idx <= PropertyLib._NoitruProperties.Songaysaochep) &&
                    ((!PropertyLib._NoitruProperties.Saochepngaytruocdo)
                    || (PropertyLib._NoitruProperties.Saochepngaytruocdo && _intDatetempt < _intDate)
                    ))
                {
                    idx += 1;
                    string _text = "Sao chép đơn thuốc của phiếu điều trị ngày:" + row["sngay_dieutri"].ToString();

                    ToolStripMenuItem newItem = new ToolStripMenuItem(_text);
                    newItem.Tag = _IdPhieudieutri + "!" + _date;
                    ctxPhieudieutri.Items.Add(newItem);
                    newItem.Click += new EventHandler(newItem_Click);
                }
            }
        }
        void newItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selectedItem = sender as ToolStripMenuItem;
            string[] arrVals = selectedItem.Tag.ToString().Split('!');
            int selectedtTreatID = Utility.Int32Dbnull(arrVals[0], -1);
            if (selectedtTreatID != -1)
            {
                KcbDonthuocCollection lstPres = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdPhieudieutriColumn).IsEqualTo(selectedtTreatID).ExecuteAsCollection<KcbDonthuocCollection>();
                if (lstPres.Count <= 0)
                {
                    Utility.ShowMsg("Phiếu điều trị bạn chọn để sao chép không có đơn thuốc. Mời bạn kiểm tra lại");
                    return;
                }
                string[] arrDate = arrVals[1].Split('/');
                DateTime pres_date = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                if (new noitru_phieudieutri().SaochepDonthuoc(_CurIdPhieudieutri, objLuotkham, lstPres, pres_date) == ActionResult.Success)
                    Utility.ShowMsg("Đã sao chép đơn thuốc thành công. Nhấn OK để kết thúc");
                else
                    Utility.ShowMsg("Lỗi khi sao chép đơn thuốc. Liên hệ VSS để được trợ giúp!");
                Selectionchanged();
            }
        }
        void grdPhieudieutri_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "colView")
            {
                _buttonClick = true;
                Selectionchanged();
            }
            if (e.Column.Key == "colDelete")
            {
                if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa thông tin phiếu điều trị", "Thông báo", true))
                {
                XoaPhieuDieuTri(Utility.Int32Dbnull(grdPhieudieutri.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1));
                }
            }
        }

        private void XoaPhieuDieuTri(int IdPhieudieutri)
        {
            try
            {
                if (!IsValidBeforeDelete(IdPhieudieutri)) return;

                ActionResult act = new noitru_phieudieutri().Xoaphieudieutri(new List<int>() { IdPhieudieutri });
                switch (act)
                {
                    case ActionResult.Success:
                        grdPhieudieutri.CurrentRow.Delete();
                        grdPhieudieutri.UpdateData();
                        grdPhieudieutri.Refresh();
                        m_dtPhieudieutri.AcceptChanges();
                        Utility.SetMsg(lblMsg, "Xóa phiếu điều trị thành công", false);
                        if (!Utility.isValidGrid(grdPhieudieutri))
                        {
                            _CurIdPhieudieutri = -1;
                            objPhieudieutri = null;
                        }
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình xóa thông tin", "Thông báo", MessageBoxIcon.Error);
                        break;
                }

            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.CatchException(exception);
                }
            }
            finally
            {
                grdPhieudieutri_SelectionChanged(grdPhieudieutri, new EventArgs());
                ModifyCommandPhieudieutri();
                ModifyCommmands();
            }
        }
        private bool IsValidBeforeDelete(int Treat_ID)
        {
            if (!globalVariables.IsAdmin)
            {
                if(globalVariables.UserName!=Utility.GetValueFromGridColumn(grdPhieudieutri,NoitruPhieudieutri.Columns.NguoiTao))
                {
                    Utility.ShowMsg("Phiếu điều trị không phải của bạn tạo,Bạn không có quyền xóa thông tin ");
                    return false;
                }


            }
            int reval = -1;
            //Nhập lại kho thanh lý
            StoredProcedure sp = SPs.NoitruKiemtraXoaphieudieutri(Treat_ID, reval);
            sp.Execute();
            reval = Utility.Int32Dbnull(sp.OutputValues[0], -1);
            switch (reval)
            {
                case -1:
                    break;
                case 1:

                     Utility.ShowMsg(string.Format("Đơn thuốc thuộc phiếu điều trị Id={0} đã được tổng hợp(hoặc đã lĩnh thuốc) nên bạn không thể xóa. Đề nghị kiểm tra lại!",Treat_ID.ToString()));
                    return false;
                case 2:
                    Utility.ShowMsg(string.Format("Đơn thuốc thuộc phiếu điều trị Id={0} đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại!", Treat_ID.ToString()));
                    return false;
                case 3:
                    Utility.ShowMsg(string.Format("Một số chỉ dịnh Cận lâm sàng thuộc phiếu điều trị Id={0} đã được chuyển cận(hoặc có kết quả) nên bạn không thể xóa. Đề nghị kiểm tra lại!", Treat_ID.ToString()));
                    return false;
                case 4:
                    Utility.ShowMsg(string.Format("Một số chỉ dịnh Cận lâm sàng thuộc phiếu điều trị Id={0} đã được thanh toán. Đề nghị kiểm tra lại!", Treat_ID.ToString()));
                    return false;

            }
            return true;
           
        }
        NoitruPhanbuonggiuong objNoitruPhanbuonggiuong = null;
       
       
        void txtChanDoan__OnSaveAs()
        {
            if (Utility.DoTrim(txtChanDoan.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtChanDoan.LOAI_DANHMUC);
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

       

        void txtNhommau__OnSaveAs()
        {
            if (Utility.DoTrim(txtNhommau.Text)=="") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhommau.LOAI_DANHMUC);
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

        void txtNhommau__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNhommau.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNhommau.myCode;
                txtNhommau.Init();
                txtNhommau.SetCode(oldCode);
                txtNhommau.Focus();
            } 
        }

       

        void txtChanDoan__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtChanDoan.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChanDoan.myCode;
                txtChanDoan.Init();
                txtChanDoan.SetCode(oldCode);
                txtChanDoan.Focus();
            } 
        }

      

        void UpdateDatatable()
        {
        }

        void cmdUnlock_Click(object sender, EventArgs e)
        {
            Unlock();
        }
      
        void chkHienthichitiet_CheckedChanged(object sender, EventArgs e)
        {
           
            grdAssignDetail.GroupMode = chkHienthichitiet.Checked ? GroupMode.Expanded : GroupMode.Collapsed;
            grdAssignDetail.Refresh();
            Application.DoEvents();
        }

        void chkAutocollapse_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._ThamKhamProperties.TudongthugonCLS = chkAutocollapse.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThamKhamProperties);
            if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                grdAssignDetail.GroupMode = GroupMode.Collapsed;
        }

       
        void cmdThamkhamConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._ThamKhamProperties);
            frm.ShowDialog();
            CauHinhThamKham();
        }
        private void CauHinhThamKham()
        {
            if (PropertyLib._ThamKhamProperties!=null)
            {
                cboA4Donthuoc.Text = PropertyLib._MayInProperties.CoGiayInDonthuoc == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewDonthuoc.SelectedIndex = PropertyLib._MayInProperties.PreviewInDonthuoc ? 0 : 1;

                cboA4Cls.Text = PropertyLib._MayInProperties.CoGiayInCLS == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewCLS.SelectedIndex = PropertyLib._MayInProperties.PreviewInCLS ? 0 : 1;

                cboA4Tomtatdieutringoaitru.Text = PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru == Papersize.A4 ? "A4" : "A5";
                cboPrintPreviewTomtatdieutringoaitru.SelectedIndex = PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru ? 0 : 1;


                cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                chkAutocollapse.Checked = PropertyLib._ThamKhamProperties.TudongthugonCLS;
                cmdPrintPres.Visible = PropertyLib._ThamKhamProperties.ChophepIndonthuoc;
            }   
        }
        void grdPatientList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
                HienthithongtinBN();
        }

        void grdPatientList_MouseClick(object sender, MouseEventArgs e)
        {
            if (PropertyLib._ThamKhamProperties.ViewAfterClick && !_buttonClick )
                HienthithongtinBN();
            _buttonClick = false;
        }

        void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInDonthuoc = cboPrintPreviewDonthuoc.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInDonthuoc = cboA4Donthuoc.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }
        void cboPrintPreviewCLS_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInCLS = cboPrintPreviewCLS.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cboA4Cls_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInCLS = cboA4Cls.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }
        void cboPrintPreviewTomtatdieutringoaitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewInTomtatDieutriNgoaitru = cboPrintPreviewTomtatdieutringoaitru.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cboA4Tomtatdieutringoaitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.CoGiayInTomtatDieutriNgoaitru = cboA4Tomtatdieutringoaitru.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void grdPatientList_DoubleClick(object sender, EventArgs e)
        {
            HienthithongtinBN();
        }
        private int SplitterDistance
        {
            get { return Distance; }
            set { Distance = value; }
        }



        private void frm_LAOKHOA_Add_TIEPDON_BN_FormClosing(object sender, FormClosingEventArgs e)
        {
            
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
                    string realName = dr[DmucChung.Columns.Ten].ToString().Trim() + " " + Utility.Bodau(dr[DmucChung.Columns.Ten].ToString().Trim());
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
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin của thăm khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if (!chkByDate.Checked)
                if (Utility.AcceptQuestion("Bạn đang tìm kiếm không theo điều kiện thời gian!\nViệc tìm kiếm sẽ mất nhiều thời gian nếu dữ liệu đã có nhiều.\nBạn có muốn dừng việc tìm kiếm để chọn lại khoảng thời gian tìm kiếm hay không?","Xác nhận",true))
                    return;
            if (Utility.Int32Dbnull(cboKhoanoitru.SelectedValue, -1) < 0)
            {
                Utility.ShowMsg("Bạn cần chọn khoa nội trú để tìm bệnh nhân cần lập phiếu điều trị");
                cboKhoanoitru.Focus();
                return;
            }
            SearchPatient();
        }

        
        private void SearchPatient()
        {
            try
            {
                
                ClearControl();
                malankham = "";
                objPhieudieutri = null;
                objBenhnhan = null;
                objLuotkham = null;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Clear();
                if (m_dtAssignDetail != null) m_dtAssignDetail.Clear();
                if (m_dtDonthuoc != null) m_dtDonthuoc.Clear();
                DateTime dt_FormDate = dtFromDate.Value;
                DateTime dt_ToDate = dtToDate.Value;
                int Status = -1;
                int SoKham = -1;


                m_dtPatients = _KCB_THAMKHAM.NoitruTimkiembenhnhan(!chkByDate.Checked ? "01/01/1900" : dt_FormDate.ToString("dd/MM/yyyy"), !chkByDate.Checked ? "01/01/1900" : dt_ToDate.ToString("dd/MM/yyyy"), txtTenBN.Text, Utility.Int16Dbnull(-1), Utility.DoTrim(txtMaluotkham.Text),
                                                          Utility.Int32Dbnull(cboKhoanoitru.SelectedValue, -1),
                                                          -1,chkChuyenkhoa.Checked?1:0);

                if (!m_dtPatients.Columns.Contains("MAUSAC"))
                    m_dtPatients.Columns.Add("MAUSAC", typeof(int));

                Utility.SetDataSourceForDataGridEx_Basic(grdPatientList, m_dtPatients, true, true, "", KcbDanhsachBenhnhan.Columns.TenBenhnhan); //"locked=0", "");
                if (grdPatientList.GetDataRows().Length == 1)
                {
                    grdPatientList.MoveFirst();
                }
                if (!dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.MaBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.MaBenh, typeof(string));
                }
                if (!dt_ICD_PHU.Columns.Contains(DmucBenh.Columns.TenBenh))
                {
                    dt_ICD_PHU.Columns.Add(DmucBenh.Columns.TenBenh, typeof(string));
                }
                grd_ICD.DataSource = dt_ICD_PHU;

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommmands();
                ModifyCommandPhieudieutri();
            }
        }
        
   
        private void BindDoctorAssignInfo()
        {
            try
            {
                m_dtDoctorAssign = THU_VIEN_CHUNG.LaydanhsachBacsi(-1,1);
                DataBinding.BindDataCombox(cboBSDieutri, m_dtDoctorAssign, DmucNhanvien.Columns.IdNhanvien,
                                           DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ chỉ định---", true);
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    if (cboBSDieutri.Items.Count > 0)
                        cboBSDieutri.SelectedIndex = 0;
                }
                else
                {
                    if (cboBSDieutri.Items.Count > 0)
                        cboBSDieutri.SelectedIndex = Utility.GetSelectedIndex(cboBSDieutri,
                                                                                 globalVariables.gv_intIDNhanvien.ToString());
                }
            }
            catch (Exception exception)
            {
                // throw;
            }
           
        }

       

        /// <summary>
        /// hàm thực hiện trạng thái của nút
        /// </summary>
        private void ModifyCommmands()
        {
            string NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI","1", false);
            string NOITRU_HIENTHI_GOIDICHVU_THEOPHIEUDIEUTRI = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_GOIDICHVU_THEOPHIEUDIEUTRI","1", false);
            string NOITRU_HIENTHI_PHIEUVTTH_THEOPHIEUDIEUTRI = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_PHIEUVTTH_THEOPHIEUDIEUTRI","1", false);
            try
            {
                cmdXoaDinhduong.Enabled = grdChedoDinhduong.GetDataRows().Length > 0;

                cmdPrintPres.Enabled = objLuotkham != null && grdPresDetail.RowCount > 0 && objPhieudieutri != null;
                cmdInphieuVT.Enabled = objLuotkham != null && grdVTTH.RowCount > 0 && objPhieudieutri != null;
                cmdPrintAssign.Enabled = objLuotkham != null && grdAssignDetail.RowCount > 0 && objPhieudieutri != null;
                cmdIngoiDV.Enabled = objLuotkham != null && grdGoidichvu.RowCount > 0 && objPhieudieutri != null;

                chkintachgoidichvu.Enabled = objLuotkham != null && cmdIngoiDV.Enabled;
                chkIntach.Enabled = objLuotkham != null && cmdPrintAssign.Enabled;
                cboServicePrint.Enabled = objLuotkham != null && cmdPrintAssign.Enabled;
                tabDiagInfo.Enabled = objLuotkham != null && objPhieudieutri != null;

                cmdDeletePres.Enabled =
                cmdUpdatePres.Enabled = objLuotkham != null && grdPresDetail.RowCount > 0 && objPhieudieutri != null && IsValidCommon();
                cmdUpdate.Enabled = cmdDelteAssign.Enabled = objLuotkham != null && grdAssignDetail.RowCount > 0 && objPhieudieutri != null && IsValidCommon();

                cmdSuagoiDV.Enabled = cmdXoagoiDV.Enabled = objLuotkham != null && IsValidCommon() && Utility.isValidGrid(grdGoidichvu) && (NOITRU_HIENTHI_GOIDICHVU_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_GOIDICHVU_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null));
                cmdSuaphieuVT_tronggoi.Enabled = cmdXoaphieuVT_tronggoi.Enabled
                    = cmdInphieuVT_tronggoi.Enabled = objLuotkham != null && IsValidCommon() && Utility.isValidGrid(grdVTTH_tronggoi) && Utility.isValidGrid(grdGoidichvu) && (NOITRU_HIENTHI_GOIDICHVU_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_GOIDICHVU_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null));

                cmdXoachandoan.Enabled =
                cmdSuachandoan.Enabled = objLuotkham != null && IsValidCommon() && Utility.isValidGrid(grdChandoan) && (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null));

                cmdXoaphieuVT.Enabled =
                  cmdSuaphieuVT.Enabled = objLuotkham != null && IsValidCommon() && Utility.isValidGrid(grdVTTH) && (NOITRU_HIENTHI_PHIEUVTTH_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_PHIEUVTTH_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null));

                //0=Ngoại trú;1=Nội trú;2=Đã điều trị(Lập phiếu);3=Đã tổng hợp chờ ra viện;4=Ra viện
                if (objLuotkham.TrangthaiNoitru > 3)
                {
                    cmdThemchandoan.Enabled = cmdInsertAssign.Enabled = cmdUpdate.Enabled = cmdDelteAssign.Enabled = cmdThemgoiDV.Enabled = cmdSuagoiDV.Enabled = cmdXoagoiDV.Enabled =
                                                                    cmdCreateNewPres.Enabled =
                                                                    cmdUpdatePres.Enabled = cmdDeletePres.Enabled =
                                                                    cmdThemphieuVT.Enabled = cmdSuaphieuVT.Enabled = cmdXoaphieuVT.Enabled = false;
                }
                else
                {
                    cmdThemchandoan.Enabled = objLuotkham != null && (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_CHANDOANKCB_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null));
                    cmdInsertAssign.Enabled = objLuotkham != null && IsValidCommon() && objPhieudieutri != null;
                    cmdThemgoiDV.Enabled = objLuotkham != null && IsValidCommon() && (NOITRU_HIENTHI_GOIDICHVU_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_GOIDICHVU_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null));
                    cmdThemphieuVT_tronggoi.Enabled = objLuotkham != null && IsValidCommon() && Utility.isValidGrid(grdGoidichvu);
                    cmdThemphieuVT.Enabled = objLuotkham != null && IsValidCommon() && (NOITRU_HIENTHI_PHIEUVTTH_THEOPHIEUDIEUTRI == "0" || (NOITRU_HIENTHI_PHIEUVTTH_THEOPHIEUDIEUTRI == "1" && objPhieudieutri != null));
                    cmdCreateNewPres.Enabled = objLuotkham != null && IsValidCommon() && objPhieudieutri != null;
                }
            }
            catch (Exception exception)
            {
            }
            finally
            {
                Modify_Thuoctralai();
            }
        }

        private void LaythongtinPhieudieutri()
        {
            ds =
                _KCB_THAMKHAM.NoitruLaythongtinclsThuocTheophieudieutri(Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                         Utility.sDbnull(malankham, ""),
                                                         Utility.Int32Dbnull(txtIdPhieudieutri.Text),0,"-1");
            m_dtAssignDetail = ds.Tables[0];
            m_dtDonthuoc = ds.Tables[1];
            m_dtVTTH = ds.Tables[2];
            m_dtGoidichvu = ds.Tables[3];
            m_dtChandoanKCB = ds.Tables[4];
            m_dtChedoDinhduong = ds.Tables[5];
            Utility.SetDataSourceForDataGridEx_Basic(grdChedoDinhduong, m_dtChedoDinhduong, false, true, "", DmucChung.Columns.SttHthi);
            Utility.SetDataSourceForDataGridEx_Basic(grdChandoan, m_dtChandoanKCB, false, true, "","");
            Utility.SetDataSourceForDataGridEx_Basic(grdAssignDetail, m_dtAssignDetail, false, true, "",
                                               "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");
            Utility.SetDataSourceForDataGridEx_Basic(grdGoidichvu, m_dtGoidichvu, false, true, "",
                                              "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

            m_dtDonthuocChitiet_View = m_dtDonthuoc.Clone();
            foreach (DataRow dr in m_dtDonthuoc.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = m_dtDonthuocChitiet_View
                    .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1")
                    + "AND " + KcbDonthuocChitiet.Columns.BnhanChitra + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.BnhanChitra], "-1")
                    + "AND " + KcbDonthuocChitiet.Columns.BhytChitra + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.BhytChitra], "-1")
                    + "AND " + KcbDonthuocChitiet.Columns.PhuThu + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.PhuThu], "-1")
                    + "AND " + KcbDonthuocChitiet.Columns.TuTuc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.TuTuc], "-1")
                    + "AND " + KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdDonthuoc], "-1")
                    );
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

            Utility.SetDataSourceForDataGridEx_Basic(grdPresDetail, m_dtDonthuocChitiet_View, false, true, "", KcbDonthuocChitiet.Columns.SttIn);

            m_dtVTTHChitiet_View = m_dtVTTH.Clone();
            foreach (DataRow dr in m_dtVTTH.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = m_dtVTTHChitiet_View
                    .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                if (drview.Length <= 0)
                {
                    m_dtVTTHChitiet_View.ImportRow(dr);
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
                    m_dtVTTHChitiet_View.AcceptChanges();
                }
            }
            //Old-->Utility.SetDataSourceForDataGridEx
            Utility.SetDataSourceForDataGridEx_Basic(grdVTTH, m_dtVTTHChitiet_View, false, true, "", KcbDonthuocChitiet.Columns.SttIn);

            ModifyCommmands();
        }
       
        private void Get_DanhmucChung()
        {
            txtChanDoan.Init();
            txtNhommau.Init();
            txtChedodinhduong.Init();
            txtHoly.Init();
            txtMaBenhChinh.Init(globalVariables.gv_dtDmucBenh, new List<string>() { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
            txtMaBenhphu.Init(globalVariables.gv_dtDmucBenh, new List<string>() { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
        }

        private void frm_QuanlyPhieudieutri_Load(object sender, EventArgs e)
        {
            try
            {
                AllowTextChanged = false;
                ClearControl();
                lstVisibleColumns = Utility.GetVisibleColumns(grdAssignDetail);
                Get_DanhmucChung();
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
                DataBinding.BindDataCombox(cboServicePrint, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Tất cả", true);
               
                LoadPhongkhamNoitru();
                BindDoctorAssignInfo();
                if (cboServicePrint.Items.Count > 0) cboServicePrint.SelectedIndex = 0;
                cboKhoanoitru.SelectedIndex = Utility.GetSelectedIndex(cboKhoanoitru, globalVariablesPrivate.objKhoaphong.IdKhoaphong.ToString());
                //cboKhoanoitru.Enabled = globalVariables.IsAdmin || cboKhoanoitru.Items.Count > 0;// THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_LAPPHIEUDIETRI_CHONKHOANOITRU", "0", false) == "1";
                if (cboKhoanoitru.SelectedIndex != 0 && cboKhoanoitru.Items.Count == 1) cboKhoanoitru.SelectedIndex = 0;
                AllowTextChanged = true;
                m_blnHasLoaded = true;
               
            }
            catch
            {
            }
            finally
            {
                ModifyCommmands();
                m_blnHasLoaded = true;
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

       
        private void LoadPhongkhamNoitru()
        {
            DataBinding.BindDataCombox(cboKhoanoitru,
                                                 THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName,Utility.Bool2byte( globalVariables.IsAdmin),(byte)1),
                                                 DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                 "---Chọn khoa nội trú---",false,true);
           
        }
        DataTable m_ExamTypeRelationList = new DataTable();
        private void LoadExamTypeRelation()
        {
            bool oldStatus = AllowTextChanged;
            try
            {
                //cboKieuKham.DataSource = null;
                ////Khởi tạo danh mục Loại khám
                //string objecttype_code = "DV";
                //DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(ObjectTypeId);
                //if (objectType != null)
                //{
                //    objecttype_code = Utility.sDbnull(objectType.MaDoituongKcb);
                //}
                //VNS.HIS.UCs.Libs.AutocompleteKieuKham(objLuotkham.MaDoituongKcb, txtKieuKham);
                //VNS.HIS.UCs.Libs.AutocompletePhongKham(objLuotkham.MaDoituongKcb, txtACPhongkham);
               
                //m_ExamTypeRelationList = THU_VIEN_CHUNG.LayDsach_Dvu_KCB(objecttype_code);
                
                //m_ExamTypeRelationList.AcceptChanges();
                //cboKieuKham.DataSource = m_ExamTypeRelationList;
                //cboKieuKham.DataMember = DmucDichvukcb.Columns.IdDichvukcb;
                //cboKieuKham.ValueMember = DmucDichvukcb.Columns.IdDichvukcb;
                //cboKieuKham.DisplayMember = DmucDichvukcb.Columns.TenDichvukcb;
                //cboKieuKham.Visible = globalVariables.UserName == "ADMIN";
                //if (m_ExamTypeRelationList == null || m_ExamTypeRelationList.Columns.Count <= 0) return;
                //AllowTextChanged = true;
                //if (m_ExamTypeRelationList.Rows.Count == 1 && m_enAction != action.Update)
                //{
                //    cboKieuKham.SelectedIndex = 0;

                //}
                AllowTextChanged = oldStatus;
            }
            catch
            {
                AllowTextChanged = oldStatus;
            }
        }
        private void cboDoctorAssign_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboBSDieutri.Text))
                {
                    cboBSDieutri.DroppedDown = true;
                }
                else
                {
                    cboBSDieutri.DroppedDown = false;
                }
            }
            catch (Exception)
            {
                // throw;
            }
        }

       
        private KcbLuotkham CreatePatientExam()
        {
            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(malankham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text));
            var objPatientExam = sqlQuery.ExecuteSingle<KcbLuotkham>();
            return objPatientExam;
        }

        void ClearControls(Control parent)
        {
            foreach (Control control in parent.Controls)
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
        }
        private void ClearControl()
        {
            try
            {
                grd_ICD.DataSource = null;
                grdAssignDetail.DataSource = null;
                grdPresDetail.DataSource = null;
                grdVTTH.DataSource = null;
                grdPhieudieutri.DataSource = null;
                grdBuongGiuong.DataSource = null;
                grdChedoDinhduong.DataSource = null;
                grdTamung.DataSource = null;
                grdGoidichvu.DataSource = null;
                grdVTTH_tronggoi.DataSource = null;
                txtIdPhieudieutri.Text = "-1";
                txtGiuong.Clear();
                txtBuong.Clear();
                txtKhoanoitru.Clear();
                ClearControls(pnlPatientInfor);
                ClearControls(pnlThongtinBNKCB);
                ClearControls(pnlKetluan);
                ClearControls(pnlother);
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
                //                 malankham.Trim() + "') order by stt_hthi_dichvu,sIntOrder,Ten_KQ";
                //DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                //uiGroupBox6.Width = temdt != null && temdt.Rows.Count > 0 ? 300 : 0;
                //Utility.SetDataSourceForDataGridEx(grdKetQua, temdt, true, true, "", "");
            }
            catch
            {
            }
        }




        int idKhoaNoitru = -1;
        string tenkhoanoitru = "";
        KcbChandoanKetluan _KcbChandoanKetluan = null;
        private bool isNhapVien = false;
        /// <summary>
        /// Lấy về thông tin bệnh nhâ nội trú
        /// </summary>
        private void GetData()
        {
            try
            {
                idKhoaNoitru = Utility.Int32Dbnull(cboKhoanoitru.SelectedValue,-1);
                tenkhoanoitru = cboKhoanoitru.Text;
                objPhieudieutri = null;
               // Utility.SetMsg(lblMsg, "", false);
                string PatientCode = Utility.sDbnull(grdPatientList.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                malankham = PatientCode;
                int Patient_ID = Utility.Int32Dbnull(grdPatientList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                objLuotkham = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(PatientCode)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Patient_ID).ExecuteSingle<KcbLuotkham>();
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
                if (objLuotkham != null)
                {
                    ClearControl();
                    DmucNhanvien objStaff = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                    string TenNhanvien = objLuotkham.NguoiKetthuc;
                    if (objStaff != null)
                        TenNhanvien = objStaff.TenNhanvien;
                    pnlCLS.Enabled = true;
                    pnlDonthuoc.Enabled = true;
                    pnlGoiDV.Enabled = true;
                    pnlVTTH.Enabled = true;
                    DataTable m_dtThongTin = _KCB_THAMKHAM.NoitruLaythongtinBenhnhan(objLuotkham.MaLuotkham,
                                                                          Utility.Int32Dbnull(objLuotkham.IdBenhnhan,-1));
                               
                        if (m_dtThongTin.Rows.Count > 0)
                        {
                            DataRow dr = m_dtThongTin.Rows[0];
                            if (dr != null)
                            {
                               
                                dtInput_Date.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayTiepdon]);
                               
                                txtGioitinh.Text =
                                    Utility.sDbnull(grdPatientList.GetValue(KcbDanhsachBenhnhan.Columns.GioiTinh), "");
                                txtPatient_Name.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                                txtPatient_ID.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                                txtPatient_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaLuotkham], "");
                                barcode.Data = malankham;
                                txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                                txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");
                               
                                txtObjectType_Name.Text = Utility.sDbnull(dr[DmucDoituongkcb.Columns.TenDoituongKcb], "");
                                txtSoBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MatheBhyt], "");
                                txtBHTT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.PtramBhytGoc], "0");
                                txtNgheNghiep.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.NgheNghiep], "");
                                txtHanTheBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], "");
                                dtpNgayhethanBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], globalVariables.SysDate.ToString("dd/MM/yyyy"));
                                txtTuoi.Text = Utility.sDbnull(Utility.Int32Dbnull(globalVariables.SysDate.Year) -
                                                               objBenhnhan.NgaySinh.Value.Year);
                                txtKhoanoitru.Text = Utility.sDbnull(dr["ten_khoanoitru"], "");
                                txtBuong.Text = Utility.sDbnull(dr["ten_buong"], "");
                                txtGiuong.Text = Utility.sDbnull(dr["ten_giuong"], "");
                                LayLichsuBuongGiuong();
                                LaydanhsachPhieudieutri();
                                LayLichsuTamung();
                                TinhtoanTongchiphi();
                            }
                        }
                    

                }
                ModifyCommmands();
            }
            catch
            {
            }
            finally
            {
                
                getResult();
            }
        }
        DataTable m_dtTamung = null;
        DataTable m_dtPhieudieutri = null;
        DataTable m_dtBuongGiuong = null;
        void LayLichsuTamung()
        {
            try
            {

                m_dtTamung = new KCB_THAMKHAM().NoitruTimkiemlichsuNoptientamung(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, 0, -1,(byte)1);
                Utility.SetDataSourceForDataGridEx_Basic(grdTamung, m_dtTamung, false, true, "1=1", NoitruTamung.Columns.NgayTamung + " desc");
                grdTamung.MoveFirst();
            }
            catch (Exception ex)
            {

            }
            finally
            {
               
            }
        }
       
        void LayLichsuBuongGiuong()
        {
            try
            {
                if (m_dtBuongGiuong != null) m_dtBuongGiuong.Rows.Clear();
                m_dtBuongGiuong = _KCB_THAMKHAM.NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan,"-1");
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, m_dtBuongGiuong, false, true, "1=1", NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
               //Tự động dò tới khoa hiện tại
                Utility.GotoNewRowJanus(grdBuongGiuong, NoitruPhanbuonggiuong.Columns.Id, Utility.Int32Dbnull(objLuotkham.IdRavien, 0).ToString());
                GetNoitruPhanbuonggiuong();

            }
            catch (Exception ex)
            {

            }
        }
        void LaydanhsachPhieudieutri()
        {
            try
            {
                string IdKhoanoitru = Utility.sDbnull(objLuotkham.IdKhoanoitru, "-1");
                if(objNoitruPhanbuonggiuong!=null)
                    IdKhoanoitru = Utility.sDbnull(objNoitruPhanbuonggiuong.IdKhoanoitru, "-1");
                bool IsAdmin = Utility.Coquyen("quyen_xemphieudieutricuabacsinoitrukhac")
                    || THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CHOPHEPXEM_PHIEUDIEUTRI_CUABACSIKHAC", "0", false) == "1";
                m_dtPhieudieutri = _KCB_THAMKHAM.NoitruTimkiemphieudieutriTheoluotkham(Utility.Bool2byte(IsAdmin),chkViewAll.Checked?"01/01/1900": dtpNgaylapphieu.Value.ToString("dd/MM/yyyy"),
                    objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan, IdKhoanoitru, Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtSongay.Text, -1)));
                Utility.SetDataSourceForDataGridEx_Basic(grdPhieudieutri,m_dtPhieudieutri,false,true,"1=1",NoitruPhieudieutri.Columns.NgayDieutri+" desc");
                grdPhieudieutri.MoveFirst();
            }
            catch (Exception ex)
            {
                
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
                    txtMaBenhChinh.SetCode( Utility.sDbnull(_KcbChandoanKetluan.MabenhChinh));
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
            cmdCreateNewPres.Enabled = !Utility.isTrue(lockstatus);
            cmdUpdatePres.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdDeletePres.Enabled = grdPresDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdPrintPres.Enabled = grdPresDetail.RowCount > 0 && !string.IsNullOrEmpty(malankham);
            ctxDelDrug.Enabled = cmdUpdatePres.Enabled;
           
            cmdInsertAssign.Enabled = !Utility.isTrue(lockstatus);
            cmdUpdate.Enabled = grdAssignDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdDelteAssign.Enabled = grdAssignDetail.RowCount > 0 && !Utility.isTrue(lockstatus);
            cmdPrintAssign.Enabled = grdAssignDetail.RowCount > 0 && !string.IsNullOrEmpty(malankham);
            chkIntach.Enabled = cmdPrintAssign.Enabled;
            cboServicePrint.Enabled = cmdPrintAssign.Enabled;
            ctxDelCLS.Enabled = cmdUpdate.Enabled;
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
                if (!Utility.isValidGrid(grdPatientList))
                {
                    ClearControl();
                    return;

                }
                AllowTextChanged = false;
                if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                GetData();
                ModifyByLockStatus(objLuotkham.Locked);
               
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
                ModifyCommmands();
                ModifyCommandPhieudieutri();
                setChanDoan();
                AllowTextChanged = true;
            }
        }
        private void grdPatientList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "colChon")
                {
                    //_buttonClick = true;
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
        private void frm_QuanlyPhieudieutri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if ((ActiveControl!=null && ActiveControl.Name == grdPatientList.Name) || (this.TabPageChanDoan.ActiveControl != null && this.TabPageChanDoan.ActiveControl.Name == txtMaBenhphu.Name))
                    return;
                else
                    SendKeys.Send("{TAB}");
            if (e.Control && e.KeyCode == Keys.P)
            {
                if (tabDiagInfo.SelectedTab == tabPageChiDinhCLS)
                    cmdPrintAssign_Click(cmdPrintAssign, new EventArgs());
                else
                    cmdPrintPres_Click(cmdPrintPres, new EventArgs());
            }

            if (e.Control & e.KeyCode==Keys.F) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.Control && e.KeyCode == Keys.U)
                Unlock();
            if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            //Keyvalue=49-->1
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.F6)
            {
                txtPatient_Code.SelectAll();
                txtPatient_Code.Focus();
                return;
            }
            if (e.KeyCode == Keys.F1)
            {
                Utility.Showhelps(this.GetType().Assembly.ManifestModule.Name.Replace(".DLL","").Replace(".dll",""),this.Name);
            }
            if (e.Control && e.KeyValue == 49)
            {
                tabDiagInfo.SelectedTab = TabPageChanDoan;
                txtMach.Focus();
            }
            if (e.Control && e.KeyValue == 50)
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

            if (e.Control && e.KeyValue == 51)
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
            if (e.Control && e.KeyValue == 52)
            {
                if (tabDiagInfo.SelectedTab == tabPageChidinhThuoc) cmdPrintPres.PerformClick();
                if (tabDiagInfo.SelectedTab == tabPageChiDinhCLS) cmdPrintAssign.PerformClick();
            }
            
            if (e.Control && e.KeyCode == Keys.N)
            {
                if (tabDiagInfo.SelectedTab == tabPageChidinhThuoc)
                    cmdCreateNewPres_Click(cmdCreateNewPres, new EventArgs());
                else
                    cmdInsertAssign_Click(cmdInsertAssign, new EventArgs());
            }

        }

        

        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            ModifyCommmands();
            ShowResult();
        }

        void ShowResult()
        {
            try
            {
                Int16 trangthai_chitiet = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.TrangthaiChitiet), 0);
                Int16 CoChitiet = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);

                int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                int IdDichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);

                int IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                int IdChidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);

                string maloai_dichvuCLS = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                string MaChidinh = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh), "XN");
                string MaBenhpham = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham), "XN");
                if (trangthai_chitiet < 3)//0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
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
                        DataTable dt = SPs.ClsTimkiemketquaXNChitiet(objLuotkham.MaLuotkham, MaChidinh, MaBenhpham, IdChidinh, CoChitiet, IdDichvu, IdChitietdichvu).GetDataSet().Tables[0];
                        Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1", "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

                        Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
                    }
                    else//XQ,SA,DT,NS
                    {
                        FillDynamicValues(IdChitietdichvu, IdChitietchidinh);
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {


            }
        }
        void FillDynamicValues(int IdDichvuChitiet, int idchidinhchitiet)
        {
            try
            {
                pnlDynamicValues.Controls.Clear();

                DataTable dtData = clsHinhanh.GetDynamicFieldsValues(IdDichvuChitiet, txtMauKQ.myCode, "", "", -1, idchidinhchitiet);

                foreach (DataRow dr in dtData.Select("1=1", "Stt_hthi"))
                {
                    dr[DynamicValue.Columns.IdChidinhchitiet] = Utility.Int32Dbnull(idchidinhchitiet);
                    ucDynamicParam _ucTextSysparam = new ucDynamicParam(dr, true);
                    _ucTextSysparam._ReadOnly = true;
                    _ucTextSysparam.TabStop = true;
                    _ucTextSysparam.TabIndex = 10 + Utility.Int32Dbnull(dr[DynamicField.Columns.Stt], 0);
                    _ucTextSysparam.Init();
                    _ucTextSysparam.Size = PropertyLib._DynamicInputProperties.DynamicSize;
                    _ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.TextSize;
                    _ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.LabelSize;
                    pnlDynamicValues.Controls.Add(_ucTextSysparam);
                }
            }
            catch (Exception ex)
            {


            }
        }
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Utility.Int32Dbnull(cboKhoanoitru.SelectedValue, -1) < 0)
                {
                    Utility.ShowMsg("Bạn cần chọn khoa nội trú trước khi chọn Bệnh nhân để lập phiếu điều trị");
                    cboKhoanoitru.Focus();
                    return;
                }
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter)
                {
                    string _patient_Code = Utility.AutoFullPatientCode(txtPatient_Code.Text);
                    ClearControl();
                    

                    dtPatient = _KCB_THAMKHAM.TimkiemBenhnhan(txtPatient_Code.Text,
                                                   -1,(byte)1, 0);
                   
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
                    txtMaluotkham.Text = _patient_Code;
                    m_dtPatients = _KCB_THAMKHAM.NoitruTimkiembenhnhan("01/01/1900", "01/01/1900", "", 1, Utility.DoTrim(txtMaluotkham.Text),
                                                         -1,
                                                         -1, chkChuyenkhoa.Checked ? 1 : 0);

                    if (!m_dtPatients.Columns.Contains("MAUSAC"))
                        m_dtPatients.Columns.Add("MAUSAC", typeof(int));

                    Utility.SetDataSourceForDataGridEx_Basic(grdPatientList, m_dtPatients, true, true, "", KcbDanhsachBenhnhan.Columns.TenBenhnhan); //"locked=0", "");

                    if (m_dtPatients.Rows.Count > 0)
                    {
                        AllowTextChanged = false;
                        if (dt_ICD_PHU != null) dt_ICD_PHU.Rows.Clear();
                        GetData();
                        txtPatient_Code.SelectAll();
                    }
                    else
                    {
                        string sPatientTemp = txtPatient_Code.Text;
                        ClearControl();
                        ModifyCommmands();
                        txtPatient_Code.Text = sPatientTemp;
                        txtPatient_Code.SelectAll();
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
                AllowTextChanged = true;
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
                        DataRow[] arrDr = m_dtPatients.Select( "id_kham=" + txtIdPhieudieutri.Text);
                        if (arrDr.Length > 0)
                            arrDr[0]["Locked"] = 1;
                        Utility.ShowMsg("Đã cập nhật thông tin thành công");
                        tabDiagInfo.Enabled = false;
                        cmdInphieudieutri.Visible = true;
                        // cmdBenhAnNgoaiTru.Visible = true;
                    }
                    else
                    {
                        Utility.ShowMsg("Chưa lưu được thông tin vào cơ sở dữ liệu");
                    }
                    ModifyCommmands();
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lưu thông tin");
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

                DataTable temdt = SPs.KcbThamkhamLayketquacls(malankham).GetDataSet().Tables[0];
                if (!temdt.Columns.Contains("id_dichvu_temp"))
                    temdt.Columns.Add(new DataColumn("id_dichvu_temp", typeof (string)));
                var lstid_dichvu = new List<string>();
                foreach (DataRow dr in temdt.Rows)
                {
                    string service_ID = Utility.sDbnull(dr[ "id_dichvu"], "");
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
        public static ReportDocument GetReport(string fileName)//, ref string ErrMsg)
        {
            try
            {
                ReportDocument crpt = new ReportDocument();
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
       

      
        private void mnuDelDrug_Click(object sender, EventArgs e)
        {
            if (!InValidDeleteSelectedDrug()) return;
            PerformActionDeleteSelectedDrug();
            ModifyCommmands();
        }

        private void mnuDeleteCLS_Click(object sender, EventArgs e)
        {
            if (!InValiSelectedCLS()) return;
            PerforActionDeleteSelectedCLS();
            ModifyCommmands();
        }

        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveDefaultPrinter();
        }

        private void LoadLaserPrinters()
        {
            if (!string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInBienlai))
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
                catch
                {
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

       
        private void txtNhietDo_Click(object sender, EventArgs e)
        {
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
            //try
            //{
            //    if (e.Column.Key == "Ghi_Chu")
            //    {

            //        new Update(KcbChidinhclsChitiet.Schema)
            //            .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
            //            .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
            //            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(
            //                Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChitietchidinh))).Execute
            //            ();
            //        grdAssignDetail.CurrentRow.BeginEdit();
            //        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NgaySua].Value = globalVariables.SysDate;
            //        grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiSua].Value = globalVariables.UserName;
            //        grdAssignDetail.CurrentRow.EndEdit();
            //    }
            //}
            //catch (Exception exception)
            //{
            //}
        }

        /// <summary>
        /// hàm thực hiện việc xóa thông tin chỉ định cận lâm sàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelteAssign_Click(object sender, EventArgs e)
        {
            if (!IsValidChidinhCLS()) return;
            PerforActionDeleteAssign();
            ModifyCommmands();
        }

        private bool IsValidChidinhCLS()
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
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Bạn chỉ có thể xóa những chỉ định chưa thanh toán !", "Thông báo",
                                MessageBoxIcon.Warning);
                return false;
            }
            foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }
        private bool IsValidGoidichvu()
        {
            bool b_Cancel = false;
            if (grdGoidichvu.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa gói dịch vụ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdGoidichvu.Focus();
                return false;
            }

            foreach (GridEXRow gridExRow in grdGoidichvu.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Gói dịch vụ bạn chọn đã thanh toán nên không thể xóa. Mời bạn chọn lại !", "Thông báo",
                                MessageBoxIcon.Warning);
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
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                int id_chidinh = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                                    -1);
                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail_ID);
                gridExRow.Delete();
                m_dtAssignDetail.AcceptChanges();
            }
        }
        private void XoaGoidichvu()
        {
            foreach (GridEXRow gridExRow in grdGoidichvu.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                int id_chidinh = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                                    -1);
                _KCB_CHIDINH_CANLAMSANG.GoidichvuXoachitiet(AssignDetail_ID);
                gridExRow.Delete();
                m_dtGoidichvu.AcceptChanges();
            }
        }
        private void PerforActionDeleteSelectedCLS()
        {
            try
            {
                int AssignDetail_ID =
                    Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                        -1);
                int id_chidinh =
                    Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                        -1);
                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail_ID);
                grdAssignDetail.CurrentRow.Delete();
                m_dtAssignDetail.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Bạn nên xóa CLS bằng cách chọn và nhấn nút Xóa CLS-->" + ex.Message);
            }
        }

        private bool InValiUpdateChiDinh()
        {
            int id_chidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(id_chidinh)
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu này đã thanh toán, Mời bạn thêm phiếu mới để thực hiện", "Thông báo");
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
                if (!CheckPatientSelected()) return;
                if (!InValiUpdateChiDinh()) return;
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.HosStatus = 1;
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.Exam_ID = -1;
                frm.objLuotkham = objLuotkham;// CreatePatientExam();
                frm.objBenhnhan = objBenhnhan;
                frm.m_eAction = action.Update;
                frm.txtAssign_ID.Text = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    ModifyCommmands();
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
        }
        bool CheckPatientSelected()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân trước khi thực hiện các công việc chỉ định Thăm khám, CLS, Kê đơn");
                return false;
            }
            if (objPhieudieutri == null)
            {
                Utility.ShowMsg("Bạn phải chọn Phiếu chỉ định trước khi thực hiện các công việc chỉ định Thăm khám, CLS, Kê đơn");
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
                if (!cmdInsertAssign.Enabled) return;
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.txtAssign_ID.Text = "-100";
                frm.Exam_ID =-1;
                frm.objLuotkham = objLuotkham;// CreatePatientExam();
                frm.objBenhnhan = objBenhnhan;
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.m_eAction = action.Insert;
                frm.txtAssign_ID.Text = "-1";
                frm.HosStatus = 1;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    if (PropertyLib._ThamKhamProperties.TudongthugonCLS)
                        grdAssignDetail.GroupMode = GroupMode.Collapsed;
                    Utility.GotoNewRowJanus(grdAssignDetail, KcbChidinhclsChitiet.Columns.IdChidinh, frm.txtAssign_ID.Text);
                    ModifyCommmands();
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
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
                int v_AssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                string v_AssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                string nhomincls = "ALL";
                if (cboServicePrint.SelectedIndex > 0)
                {
                    nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");

                }
                KCB_INPHIEU.InphieuChidinhCLS((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, v_AssignId, v_AssignCode, nhomincls, cboServicePrint.SelectedIndex, chkIntach.Checked, ref mayin);
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
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.DiaChi, typeof(string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.NamSinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.NamSinh, typeof (int));


            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.TenBenhnhan))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.TenBenhnhan, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.GioiTinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.GioiTinh, typeof(string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Tuoi"))
                dtReportPhieuXetNghiem.Columns.Add("Tuoi", typeof(int));
            if (!dtReportPhieuXetNghiem.Columns.Contains(KcbDanhsachBenhnhan.Columns.GioiTinh))
                dtReportPhieuXetNghiem.Columns.Add(KcbDanhsachBenhnhan.Columns.GioiTinh, typeof(string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Rank_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Rank_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Position_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Position_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("Unit_Name"))
                dtReportPhieuXetNghiem.Columns.Add("Unit_Name", typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("chan_doan"))
                dtReportPhieuXetNghiem.Columns.Add("chan_doan", typeof(string));
            if (!dtReportPhieuXetNghiem.Columns.Contains(DmucDoituongkcb.Columns.TenDoituongKcb))
                dtReportPhieuXetNghiem.Columns.Add(DmucDoituongkcb.Columns.TenDoituongKcb, typeof (string));
            if (!dtReportPhieuXetNghiem.Columns.Contains("mathe_bhyt"))
                dtReportPhieuXetNghiem.Columns.Add("mathe_bhyt", typeof(string));
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
                KcbDonthuocCollection lstPres =
                    new Select()
                        .From(KcbDonthuoc.Schema)
                        .Where(KcbDonthuoc.MaLuotkhamColumn).IsEqualTo(Utility.sDbnull(objLuotkham.MaLuotkham)).
                        ExecuteAsCollection<KcbDonthuocCollection>();

                var lstPres1 = from p in lstPres
                               where p.IdPhieudieutri == objPhieudieutri.IdPhieudieutri
                               select p;
                if (objLuotkham.MaDoituongKcb == "BHYT")
                {
                    if (_kenhieudon == "Y" && lstPres1.Count() <= 0)//Được phép kê mỗi phòng khám 1 đơn thuốc
                        return false;
                    if (_kenhieudon == "N" && lstPres.Count > 0 && lstPres1.Count() <= 0)//Cảnh báo ko được phép kê đơn tiếp
                    {
                        Utility.ShowMsg("Chú ý: Bệnh nhân này thuộc đối tượng BHYT và đã được kê đơn thuốc tại phòng khám khác. Bạn cần trao đổi với bộ phận khác để được cấu hình kê đơn thuốc tại nhiều phòng khác khác nhau với đối tượng BHYT này", "Thông báo");
                        return false;
                    }
                }
                else//Bệnh nhân dịch vụ-->cho phép kê 1 đơn nếu đơn chưa thanh toán và nhiều đơn nếu các đơn trước đã thanh toán
                {
                    if (lstPres1.Count() > 0)
                        if (lstPres1.FirstOrDefault().TrangthaiThanhtoan == 0)//Chưa thanh toán-->Cần sửa đơn
                            return true;
                        else//Đã thanh toán-->Cho phép thêm đơn mới
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
            if (objLuotkham.TrangthaiNoitru>=1 ||(objLuotkham.TrangthaiNoitru==0 && !ExistsDonThuoc()))
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
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("THUOC");
                frm.em_Action = action.Insert;
                frm.objLuotkham = CreatePatientExam();
                frm._KcbChandoanKetluan = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.MyCode;
                frm._Chandoan = txtChanDoan.Text;
                frm.dt_ICD = globalVariables.gv_dtDmucBenh;
                if (objPhieudieutri != null)
                    frm.forced2Add =Utility.Byte2Bool( objPhieudieutri.TthaiBosung);
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.id_kham = -1;
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 1;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.SetCode(frm._MabenhChinh);
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbChandoanKetluan != null) _KcbChandoanKetluan = frm._KcbChandoanKetluan;
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                    ModifyCommmands();
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
            //        LaythongtinPhieudieutri();
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
            if (!CheckPatientSelected()) return;
            if (!cmdUpdatePres.Enabled) return;
            UpdateDonThuoc();
        }

        private bool IsValid_UpdateDonthuoc(int pres_id,string thuoc_vt)
        {
            TPhieuCapphatChitiet _capphat = new Select().From(TPhieuCapphatChitiet.Schema).Where(TPhieuCapphatChitiet.Columns.IdDonthuoc).IsEqualTo(pres_id)
                .ExecuteSingle<TPhieuCapphatChitiet>();
            if (_capphat != null)
            {
                Utility.ShowMsg("Đơn " + thuoc_vt + " đã được tổng hợp lĩnh " + thuoc_vt + " nội trú nên bạn không được phép sửa. Đề nghị kiểm tra lại");
                return false;
            }
            KcbDonthuoc _item =
                new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(pres_id)
                .And(KcbDonthuoc.TrangThaiColumn).IsEqualTo(1).ExecuteSingle<KcbDonthuoc>();
            if (_item != null)
            {
                Utility.ShowMsg("Đơn " + thuoc_vt + " này đang ở trạng thái đã duyệt cho Bệnh nhân nên không thể chỉnh sửa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }

        private void UpdateDonThuoc()
        {
            try
            {
                if (grdPresDetail.CurrentRow != null && grdPresDetail.CurrentRow.RowType == RowType.Record)
                {
                    KcbLuotkham objPatientExam = CreatePatientExam();
                    if (objPatientExam != null)
                    {
                        int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                        if (!IsValid_UpdateDonthuoc(Pres_ID,"thuốc"))
                        {
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
                            frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("THUOC");
                            frm.em_Action = action.Update;
                            frm._KcbChandoanKetluan = _KcbChandoanKetluan;
                            frm._MabenhChinh = txtMaBenhChinh.MyCode;
                            frm._Chandoan = txtChanDoan.Text;
                            frm.dt_ICD = globalVariables.gv_dtDmucBenh;
                            if (objPhieudieutri != null)
                                frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                            frm.dt_ICD_PHU = dt_ICD_PHU;
                            frm.noitru = 1;
                            frm.objLuotkham = CreatePatientExam();
                            frm.id_kham =-1;
                            frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                            frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                            frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                            frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                            frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                            frm.ShowDialog();
                            if (!frm.m_blnCancel)
                            {
                                txtMaBenhChinh.SetCode( frm._MabenhChinh);
                                txtChanDoan._Text = frm._Chandoan;
                                dt_ICD_PHU = frm.dt_ICD_PHU;
                                if (frm._KcbChandoanKetluan != null) _KcbChandoanKetluan = frm._KcbChandoanKetluan;
                                LaythongtinPhieudieutri();
                                TinhtoanTongchiphi();
                                Utility.GotoNewRowJanus(grdPresDetail, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                        Utility.sDbnull(frm.txtPres_ID.Text));
                                ModifyCommmands();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        List<int> GetIdChitietVTTH(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr = m_dtVTTH.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " + KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
                var p1 = (from q in arrDr.AsEnumerable()
                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                var p = (from q in arrDr.AsEnumerable()
                         select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList<int>();
            }
            return new List<int>();
        }
        List<int> GetIdChitietVTTHtronggoi(int IdDonthuoc,int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr = m_dtVTTH_tronggoi.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc .ToString()+" AND "+ KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
                var p1 = (from q in arrDr.AsEnumerable()
                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                var p = (from q in arrDr.AsEnumerable()
                         select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList<int>();
            }
            return new List<int>();
        }
        List<int> GetIdChitiet(int IdDonthuoc, int id_thuoc, decimal don_gia, ref string s)
        {
            DataRow[] arrDr = m_dtDonthuoc.Select(KcbDonthuocChitiet.Columns.IdDonthuoc + "=" + IdDonthuoc.ToString() + " AND " + KcbDonthuocChitiet.Columns.IdThuoc + "=" + id_thuoc.ToString()
                    + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" + don_gia.ToString());
            if (arrDr.Length > 0)
            {
                var p1 = (from q in arrDr.AsEnumerable()
                          select Utility.sDbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                s = string.Join(",", p1.ToArray());
                var p = (from q in arrDr.AsEnumerable()
                         select Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc])).Distinct();
                return p.ToList<int>();
            }
            return new List<int>();
        }
        void XoachandoanKCB(List<int> lstIdChandoanKCB)
        {
            try
            {
                var p = (from q in m_dtChandoanKCB.Select("1=1").AsEnumerable()
                         where lstIdChandoanKCB.Contains(Utility.Int32Dbnull(q[KcbChandoanKetluan.Columns.IdChandoan]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtChandoanKCB.Rows.Remove(p[i]);
                m_dtChandoanKCB.AcceptChanges();
            }
            catch
            {
            }
        }
        void XoaDinhduong(List<int> lstIdDinhduong)
        {
            try
            {
                var p = (from q in m_dtChedoDinhduong.Select("1=1").AsEnumerable()
                         where lstIdDinhduong.Contains(Utility.Int32Dbnull(q[NoitruPhieudinhduong.Columns.Id]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtChedoDinhduong.Rows.Remove(p[i]);
                m_dtChedoDinhduong.AcceptChanges();
            }
            catch
            {
            }
        }
        void XoaVTTHKhoiBangDulieu_trongoi(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                var p = (from q in m_dtVTTH.Select("1=1").AsEnumerable()
                         where lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtVTTH_tronggoi.Rows.Remove(p[i]);
                m_dtVTTH_tronggoi.AcceptChanges();
            }
            catch
            {
            }
        }
        void XoaVTTHKhoiBangDulieu(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                var p = (from q in m_dtVTTH.Select("1=1").AsEnumerable()
                         where lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtVTTH.Rows.Remove(p[i]);
                m_dtVTTH.AcceptChanges();
            }
            catch
            {
            }
        }
        void XoaThuocKhoiBangdulieu(List<int> lstIdChitietDonthuoc)
        {
            try
            {
                var p = (from q in m_dtDonthuoc.Select("1=1").AsEnumerable()
                         where lstIdChitietDonthuoc.Contains(Utility.Int32Dbnull(q[KcbDonthuocChitiet.Columns.IdChitietdonthuoc]))
                         select q).ToArray<DataRow>();
                for (int i = 0; i <= p.Length - 1; i++)
                    m_dtDonthuoc.Rows.Remove(p[i]);
                m_dtDonthuoc.AcceptChanges();
            }
            catch
            {
            }
        }

        private void ThemphieuVattu_tronggoi()
        {
            try
            {
                // KeDonThuocTheoDoiTuong();
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                frm.em_Action = action.Insert;
                frm.objLuotkham = CreatePatientExam();
                frm._KcbChandoanKetluan = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.MyCode;
                frm._Chandoan = txtChanDoan.Text;
                if (objPhieudieutri != null)
                    frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                frm.dt_ICD = globalVariables.gv_dtDmucBenh;
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.id_kham = -1;
                frm.id_goidv=Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdGoidichvu,KcbChidinhclsChitiet.Columns.IdChitietchidinh),-1) ;
                frm.trong_goi=1;
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 1;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.SetCode( frm._MabenhChinh);
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbChandoanKetluan != null) _KcbChandoanKetluan = frm._KcbChandoanKetluan;
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    Utility.GotoNewRowJanus(grdVTTH_tronggoi, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                    ModifyCommmands();
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
                txtPatient_Code.Focus();
                txtPatient_Code.SelectAll();
            }
        }
        private void SuaphieuVattu_tronggoi()
        {
            try
            {
                if (!CheckPatientSelected()) return;
                if (!Utility.isValidGrid(grdVTTH_tronggoi))
                {
                    if (grdVTTH_tronggoi.GetDataRows().Length > 0)
                        grdVTTH_tronggoi.MoveFirst();
                }
                //Check lại cho chắc ăn
                if (!Utility.isValidGrid(grdVTTH_tronggoi))
                {
                    return;
                }
                KcbLuotkham objPatientExam = CreatePatientExam();
                if (objPatientExam != null)
                {
                    int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                    if (!IsValid_UpdateDonthuoc(Pres_ID,"vật tư"))
                    {
                        return;
                    }
                    var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                        .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                        .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                        .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                    if (v_collect.Count > 0)
                    {
                        Utility.ShowMsg(
                            "Phiếu vật tư bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Phiếu vật tư Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư tiêu hao để hủy xác nhận Phiếu vật tư tại kho vật tư");
                        return;
                    }
                    KcbDonthuoc objPrescription = KcbDonthuoc.FetchByID(Pres_ID);
                    if (objPrescription != null)
                    {
                        frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                        frm.em_Action = action.Update;
                        frm._KcbChandoanKetluan = _KcbChandoanKetluan;
                        frm._MabenhChinh = txtMaBenhChinh.MyCode;
                        frm._Chandoan = txtChanDoan.Text;
                        frm.dt_ICD = globalVariables.gv_dtDmucBenh;
                        frm.dt_ICD_PHU = dt_ICD_PHU;
                        if (objPhieudieutri != null)
                            frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                        frm.noitru = 1;
                        frm.id_goidv = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdGoidichvu, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        frm.trong_goi = 1;
                        frm.objLuotkham = CreatePatientExam();
                        frm.id_kham = -1;
                        frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                        frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                        frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                        frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                        frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            txtMaBenhChinh.SetCode( frm._MabenhChinh);
                            txtChanDoan._Text = frm._Chandoan;
                            dt_ICD_PHU = frm.dt_ICD_PHU;
                            if (frm._KcbChandoanKetluan != null) _KcbChandoanKetluan = frm._KcbChandoanKetluan;
                            LaythongtinPhieudieutri();
                            TinhtoanTongchiphi();
                            Utility.GotoNewRowJanus(grdVTTH_tronggoi, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                    Utility.sDbnull(frm.txtPres_ID.Text));
                            ModifyCommmands();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void PerformActionDeleteVTTH_tronggoi()
        {
            try
            {
                string s = "";
                int Pres_ID = Utility.Int32Dbnull(grdVTTH_tronggoi.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                if (!IsValid_UpdateDonthuoc(Pres_ID,"vật tư"))
                {
                    return;
                }
                var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                    .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                    .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                    .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                if (v_collect.Count > 0)
                {
                    Utility.ShowMsg(
                        "Phiếu vật tư bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Phiếu vật tư Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư tiêu hao để hủy xác nhận Phiếu vật tư tại kho vật tư");
                    return;
                }
                List<int> lstIdchitiet = new List<int>();
                foreach (GridEXRow gridExRow in grdVTTH_tronggoi.GetCheckedRows())
                {
                    string stempt = "";
                    int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                    int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                    decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                    List<int> _temp = GetIdChitietVTTHtronggoi(IdDonthuoc,id_thuoc, dongia, ref stempt);
                    s += "," + stempt;
                    lstIdchitiet.AddRange(_temp);
                    gridExRow.Delete();
                    grdVTTH.UpdateData();

                }
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
                XoaVTTHKhoiBangDulieu_trongoi(lstIdchitiet);
                m_dtVTTH_tronggoi.AcceptChanges();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }
        private void XoaDinhduong(GridEXRow gridExRow)
        {
            try
            {
                string s = "";
                List<int> lstId = new List<int>();
                string stempt = "";
                int Id = Utility.Int32Dbnull(gridExRow.Cells[NoitruPhieudinhduong.Columns.Id].Value, 0m);
                s += "," + Id.ToString();
                lstId.Add(Id);
                grdChedoDinhduong.Delete();
                grdChedoDinhduong.UpdateData();


                _KCB_KEDONTHUOC.NoitruXoaDinhduong(s);
                XoaDinhduong(lstId);
                m_dtChedoDinhduong.AcceptChanges();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
        private void XoaDinhduong()
        {
            try
            {
                string s = "";
                List<int> lstId = new List<int>();
                foreach (GridEXRow gridExRow in grdChedoDinhduong.GetCheckedRows())
                {
                    string stempt = "";
                    int Id = Utility.Int32Dbnull(gridExRow.Cells[NoitruPhieudinhduong.Columns.Id].Value, 0m);
                    s += "," + Id.ToString();
                    lstId.Add(Id);
                    grdChedoDinhduong.Delete();
                    grdChedoDinhduong.UpdateData();

                }
                _KCB_KEDONTHUOC.NoitruXoaDinhduong(s);
                XoaDinhduong(lstId);
                m_dtChedoDinhduong.AcceptChanges();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
        private void XoaChandoan()
        {
            try
            {
                string s = "";
                int Pres_ID = Utility.Int32Dbnull(grdChandoan.GetValue(KcbChandoanKetluan.Columns.IdChandoan));
                List<int> lstIdchitiet = new List<int>();
                foreach (GridEXRow gridExRow in grdChandoan.GetCheckedRows())
                {
                    string stempt = "";
                    int IdChandoan = Utility.Int32Dbnull(gridExRow.Cells[KcbChandoanKetluan.Columns.IdChandoan].Value, 0m);
                    s += "," + IdChandoan.ToString();
                    lstIdchitiet.Add(IdChandoan);
                    grdChandoan.Delete();
                    grdChandoan.UpdateData();

                }
                _KCB_KEDONTHUOC.NoitruXoachandoan(s);
                XoachandoanKCB(lstIdchitiet);
                m_dtChandoanKCB.AcceptChanges();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

        }
        private void ThemphieuVattu()
        {
            try
            {
               
                frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                frm.em_Action = action.Insert;
                frm.objLuotkham = CreatePatientExam();
                frm._KcbChandoanKetluan = _KcbChandoanKetluan;
                frm._MabenhChinh = txtMaBenhChinh.MyCode;
                frm._Chandoan = txtChanDoan.Text;
                frm.dt_ICD = globalVariables.gv_dtDmucBenh;
                if (objPhieudieutri != null)
                    frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                frm.dt_ICD_PHU = dt_ICD_PHU;
                frm.id_kham = Utility.Int32Dbnull(txtExam_ID.Text);
                frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                frm.id_kham = -1;
                frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                frm.txtPres_ID.Text = "-1";
                frm.noitru = 1;
                frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    txtMaBenhChinh.SetCode( frm._MabenhChinh);
                    txtChanDoan._Text = frm._Chandoan;
                    dt_ICD_PHU = frm.dt_ICD_PHU;
                    if (frm._KcbChandoanKetluan != null) _KcbChandoanKetluan = frm._KcbChandoanKetluan;
                    LaythongtinPhieudieutri();
                    TinhtoanTongchiphi();
                    Utility.GotoNewRowJanus(grdVTTH, KcbDonthuoc.Columns.IdDonthuoc,
                                            Utility.sDbnull(frm.txtPres_ID.Text));
                    ModifyCommmands();
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

                KcbLuotkham objPatientExam = CreatePatientExam();
                if (objPatientExam != null)
                {
                    int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
                    if (!IsValid_UpdateDonthuoc(Pres_ID,"vật tư"))
                    {
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
                        frm_KCB_KE_DONTHUOC frm = new frm_KCB_KE_DONTHUOC("VT");
                        frm.em_Action = action.Update;
                        frm._KcbChandoanKetluan = _KcbChandoanKetluan;
                        frm._MabenhChinh = txtMaBenhChinh.MyCode;
                        if (objPhieudieutri != null)
                            frm.forced2Add = Utility.Byte2Bool(objPhieudieutri.TthaiBosung);
                        frm._Chandoan = txtChanDoan.Text;
                        frm.dt_ICD = globalVariables.gv_dtDmucBenh;
                        frm.dt_ICD_PHU = dt_ICD_PHU;
                        frm.noitru = 1;
                        frm.objLuotkham = CreatePatientExam();
                        frm.id_kham = -1;
                        frm.objPhieudieutriNoitru = NoitruPhieudieutri.FetchByID(Utility.Int32Dbnull(txtIdPhieudieutri.Text));
                        frm.txtPatientCode.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                        frm.txtPatientID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                        frm.txtPres_ID.Text = Utility.sDbnull(objPrescription.IdDonthuoc);
                        frm.CallActionKeDon = CallActionKieuKeDon.TheoDoiTuong;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            txtMaBenhChinh.SetCode( frm._MabenhChinh);
                            txtChanDoan._Text = frm._Chandoan;
                            dt_ICD_PHU = frm.dt_ICD_PHU;
                            if (frm._KcbChandoanKetluan != null) _KcbChandoanKetluan = frm._KcbChandoanKetluan;
                            LaythongtinPhieudieutri();
                            TinhtoanTongchiphi();
                            Utility.GotoNewRowJanus(grdVTTH, KcbDonthuocChitiet.Columns.IdDonthuoc,
                                                    Utility.sDbnull(frm.txtPres_ID.Text));
                            ModifyCommmands();
                        }
                    }
                }

            }
            catch
            {
            }
        }
        private void PerformActionDeleteVTTH()
        {
            string s = "";
            int Pres_ID = Utility.Int32Dbnull(grdVTTH.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            if (!IsValid_UpdateDonthuoc(Pres_ID,"vật tư"))
            {
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
            List<int> lstIdchitiet = new List<int>();
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, 0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value, 0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                List<int> _temp = GetIdChitietVTTH(IdDonthuoc,id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdVTTH.UpdateData();

            }
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            XoaVTTHKhoiBangDulieu(lstIdchitiet);
            m_dtVTTH.AcceptChanges();
        }
        private void PerformActionDeletePres()
        {
            string s = "";
            int Pres_ID = Utility.Int32Dbnull(grdPresDetail.GetValue(KcbDonthuocChitiet.Columns.IdDonthuoc));
            if (!IsValid_UpdateDonthuoc(Pres_ID,"thuốc"))
            {
                return;
            }
            var v_collect = new Select().From(KcbDonthuocChitiet.Schema.TableName)
                .Where(KcbDonthuocChitiet.TrangthaiThanhtoanColumn.ColumnName).IsEqualTo(1)
                .And(KcbDonthuocChitiet.IdDonthuocColumn.ColumnName).IsEqualTo(Pres_ID)
                .ExecuteAsCollection<KcbDonthuocChitietCollection>();
            if (v_collect.Count > 0)
            {
                Utility.ShowMsg(
                    "Đơn thuốc bạn đang chọn sửa đã được thanh toán. Muốn sửa lại Đơn thuốc Bạn cần phải liên hệ với bộ phận Thanh toán để hủy thanh toán và Bộ phận cấp vật tư để hủy xác nhận thuốc tại kho thuốc");
                return;
            }
            List<int> lstIdchitiet = new List<int>();
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                string stempt = "";
                int id_thuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,0m);
                decimal dongia = Utility.DecimaltoDbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.DonGia].Value,0m);
                int IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value, 0m);
                List<int> _temp = GetIdChitiet(IdDonthuoc,id_thuoc, dongia, ref stempt);
                s += "," + stempt;
                lstIdchitiet.AddRange(_temp);
                gridExRow.Delete();
                grdPresDetail.UpdateData();
               
            }
            _KCB_KEDONTHUOC.XoaChitietDonthuoc(s);
            XoaThuocKhoiBangdulieu(lstIdchitiet);
            m_dtDonthuoc.AcceptChanges();
        }
        private void PerformActionDeletePres_old()
        {
            string s = "";
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                int Pres_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                                  -1);
                int PresDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                                        -1);
                int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                                  -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(PresDetail_ID);
                gridExRow.Delete();
                grdPresDetail.UpdateData();
                m_dtDonthuoc.AcceptChanges();
            }
        }
        private void PerformActionDeleteSelectedDrug()
        {
            try
            {
                int Pres_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdDonthuoc].Value,
                                        -1);
                int PresDetail_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int Drug_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value,
                                        -1);
                _KCB_KEDONTHUOC.XoaChitietDonthuoc(PresDetail_ID);
                grdPresDetail.CurrentRow.Delete();
                grdPresDetail.UpdateData();
                m_dtDonthuoc.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message + "-->" +
                                "Bạn nên dùng chức năng xóa thuốc bằng cách chọn thuốc và sử dụng nút xóa thuốc");
            }
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

            KcbDonthuocChitiet objKcbDonthuocChitiet = null;
            foreach (GridEXRow gridExRow in grdVTTH.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int PresDetail_ID =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                     objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(PresDetail_ID)
                        .ExecuteSingle<KcbDonthuocChitiet>();
                    if (objKcbDonthuocChitiet!=null)
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
        private bool IsValidVTTH_delete_trongoi()
        {
            bool b_Cancel = false;
            if (grdVTTH_tronggoi.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin Vật tư tiêu hao ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdVTTH_tronggoi.Focus();
                return false;
            }
            KcbDonthuocChitiet objKcbDonthuocChitiet = null;
            foreach (GridEXRow gridExRow in grdVTTH_tronggoi.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int PresDetail_ID =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                     objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(PresDetail_ID)
                        .ExecuteSingle<KcbDonthuocChitiet>();
                    if (objKcbDonthuocChitiet!=null)
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
                grdVTTH_tronggoi.Focus();
                return false;
            }
            return true;
        }
        private bool IsValidThuoc_delete()
        {
            bool b_Cancel = false;
            if (grdPresDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện việc xóa thông tin thuốc ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }

            KcbDonthuocChitiet objKcbDonthuocChitiet = null;
            foreach (GridEXRow gridExRow in grdPresDetail.GetCheckedRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    int PresDetail_ID =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value, -1);
                    int Drug_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                    objKcbDonthuocChitiet = new Select().From(KcbDonthuocChitiet.Schema)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(PresDetail_ID)
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
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

        private bool InValiSelectedCLS()
        {
            bool b_Cancel = false;
            if (grdAssignDetail.RowCount <= 0 || grdAssignDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định CLS", "Thông báo",
                                MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }


            int AssignDetail_ID =
                Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                    -1);
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
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

            AssignDetail_ID =
                Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                    -1);
            sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                b_Cancel = true;
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }

        private bool InValidDeleteSelectedDrug()
        {
            bool b_Cancel = false;
            if (grdPresDetail.RowCount <= 0 || grdPresDetail.CurrentRow.RowType != RowType.Record)
            {
                Utility.ShowMsg("Bạn phải chọn một thuốc để xóa ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }


            if (grdPresDetail.CurrentRow.RowType == RowType.Record)
            {
                int PresDetail_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdChitietdonthuoc].Value,
                                        -1);
                int Drug_ID =
                    Utility.Int32Dbnull(grdPresDetail.CurrentRow.Cells[KcbDonthuocChitiet.Columns.IdThuoc].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(PresDetail_ID)
                    .And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                }
            }

            if (b_Cancel)
            {
                Utility.ShowMsg("Bản ghi đã thanh toán, Bạn không thể xóa thông tin được ", "Thông báo",
                                MessageBoxIcon.Warning);
                grdPresDetail.Focus();
                return false;
            }
            return true;
        }

        private void cmdDeletePres_Click(object sender, EventArgs e)
        {
            if (!IsValidThuoc_delete()) return;
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
                PrintPres(Pres_ID,"");
            }
            catch (Exception)
            {
                // throw;
            }
        }

        /// <summary>
        /// hàm thực hiện việc in đơn thuốc
        /// </summary>
        /// <param name="PresID"></param>
        private void PrintPres(int PresID,string forcedTitle)
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
            ReportDocument reportDocument = new ReportDocument();
            string reportCode = "";
             string tieude="", reportname = "";
            switch (KhoGiay)
            {
                case "A5":
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument =  Utility.GetReport("thamkham_InDonthuocA5",ref tieude,ref reportname);
                    break;
                case "A4":
                    reportCode = "thamkham_InDonthuocA4";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA4" ,ref tieude,ref reportname);
                    break;
                default:
                    reportCode = "thamkham_InDonthuocA5";
                    reportDocument = Utility.GetReport("thamkham_InDonthuocA5" ,ref tieude,ref reportname);
                    break;
            }
            if (reportDocument == null) return;
            //v_dtData.AcceptChanges();
            Utility.WaitNow(this);
            if (Utility.DoTrim(forcedTitle).Length > 0)
                tieude = forcedTitle;
            ReportDocument crpt = reportDocument;
            var objForm = new frmPrintPreview("IN ĐƠN THUỐC BỆNH NHÂN", crpt, true, true);
            try
            {
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = reportCode;
                crpt.SetDataSource(v_dtData);
                Utility.SetParameterValue(crpt,"ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt,"BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt,"Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt,"Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "ReportTitle", tieude);
                Utility.SetParameterValue(crpt,"CurrentDate", Utility.FormatDateTime(globalVariables.SysDate));
                Utility.SetParameterValue(crpt,"BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewInDonthuoc))
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

        string GetDanhsachBenhphu()
        {
            var sMaICDPHU = new StringBuilder("");
            try
            {
                int recordRow = 0;

                foreach (DataRow row in dt_ICD_PHU.Rows)
                {
                    if (recordRow > 0)
                        sMaICDPHU.Append(",");
                    sMaICDPHU.Append(Utility.sDbnull(row[DmucBenh.Columns.MaBenh], ""));
                    recordRow++;
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
        private KcbChandoanKetluan CreateDiagInfo()
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
            _KcbChandoanKetluan.MaLuotkham = Utility.sDbnull(malankham, "");
            _KcbChandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(txtPatient_ID.Text, "-1");
            _KcbChandoanKetluan.MabenhChinh = Utility.sDbnull(txtMaBenhChinh.MyCode, "");
            _KcbChandoanKetluan.Nhommau = txtNhommau.Text;
            _KcbChandoanKetluan.Nhietdo = Utility.sDbnull(txtNhietDo.Text);
            _KcbChandoanKetluan.Huyetap = txtHa.Text;
            _KcbChandoanKetluan.Mach = txtMach.Text;
            _KcbChandoanKetluan.Nhiptim = Utility.sDbnull(txtNhipTim.Text);
            _KcbChandoanKetluan.Nhiptho = Utility.sDbnull(txtNhipTho.Text);
            _KcbChandoanKetluan.Chieucao = Utility.sDbnull(txtChieucao.Text);
            _KcbChandoanKetluan.Cannang = Utility.sDbnull(txtCannang.Text);
            _KcbChandoanKetluan.HuongDieutri = "";
            _KcbChandoanKetluan.SongayDieutri = 0;
            _KcbChandoanKetluan.Ketluan = "";
            if (cboBSDieutri.SelectedIndex > 0)
                _KcbChandoanKetluan.IdBacsikham = Utility.Int16Dbnull(cboBSDieutri.SelectedValue, -1);
            else
            {
                _KcbChandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
            }
            string sMaICDPHU = GetDanhsachBenhphu();
            _KcbChandoanKetluan.MabenhPhu = Utility.sDbnull(sMaICDPHU.ToString(), "");
            if (objPhieudieutri != null)
            {
                _KcbChandoanKetluan.IdPhongkham = Utility.Int32Dbnull(objPhieudieutri.IdKhoanoitru, -1);
                DmucKhoaphong objDepartment = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(objPhieudieutri.IdKhoanoitru, -1));
                if (objDepartment != null)
                {
                    _KcbChandoanKetluan.TenPhongkham = Utility.sDbnull(objDepartment.TenKhoaphong, "");
                }
                _KcbChandoanKetluan.IdPhongkham = Utility.Int32Dbnull(objPhieudieutri.IdKhoanoitru);
            }
            else
            {
                _KcbChandoanKetluan.IdPhongkham = globalVariables.idKhoatheoMay;
            }
            _KcbChandoanKetluan.IdKham = Utility.Int32Dbnull(txtIdPhieudieutri.Text, -1);
            _KcbChandoanKetluan.NgayTao = dtpCreatedDate.Value;
            _KcbChandoanKetluan.NguoiTao = globalVariables.UserName;
            _KcbChandoanKetluan.NgayChandoan = dtpCreatedDate.Value;
            _KcbChandoanKetluan.Ketluan = "";
            _KcbChandoanKetluan.Chandoan = Utility.ReplaceString(txtChanDoan.Text);
            _KcbChandoanKetluan.ChandoanKemtheo = Utility.sDbnull(txtChanDoanKemTheo.Text);

            _KcbChandoanKetluan.Noitru = (byte)1;
            return _KcbChandoanKetluan;
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi đóng gói dữ liệu vào Entity
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            if (string.IsNullOrEmpty(txtPatient_Code.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn Bệnh nhân để thực hiện lập phiếu điều trị", true);
                txtPatient_Code.Focus();
                return false;
            }
            KcbLuotkham _item = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(malankham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(txtPatient_ID.Text).ExecuteSingle<KcbLuotkham>();
            if (_item==null)
            {
                Utility.ShowMsg("Bạn phải chọn Bệnh nhân hoặc bệnh nhân không tồn tại!", "Thông báo",
                                MessageBoxIcon.Warning);
                txtPatient_Code.Focus();
                return false;
            }
          
            
           

            if (objLuotkham != null && objLuotkham.MaDoituongKcb == "BHYT" && Utility.DoTrim( txtMaBenhChinh.MyCode)=="-1")
            {
                Utility.SetMsg( lblMsg,"Bạn cần nhập Mã bệnh chính cho đối tượng BHYT",true);
                tabDiagInfo.SelectedTab = TabPageChanDoan;
                txtMaBenhChinh.Focus();
                return false;
            }
            
            return true;
        }
        void TinhtoanTongchiphi()
        {
            try
            {
                DataSet dsData = SPs.NoitruTongchiphi(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan).GetDataSet();
                decimal Tong_CLS = Utility.DecimaltoDbnull(dsData.Tables[0].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Thuoc = Utility.DecimaltoDbnull(dsData.Tables[1].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_VTTH = Utility.DecimaltoDbnull(dsData.Tables[2].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Giuong = Utility.DecimaltoDbnull(dsData.Tables[3].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Goi = Utility.DecimaltoDbnull(dsData.Tables[4].Compute("SUM(TT_BN)", "1=1"));
                decimal Tong_Tamung = Utility.DecimaltoDbnull(m_dtTamung.Compute("SUM(so_tien)", "1=1"));
                decimal Tong_chiphi = Tong_CLS + Tong_Thuoc + Tong_Giuong + Tong_Goi + Tong_VTTH;

                lblCLS.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_CLS.ToString()));
                lblThuoc.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_Thuoc.ToString()));
                lblVTTH.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_VTTH.ToString()));
                lblBuonggiuong.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_Giuong.ToString()));
                lblDichvu.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_Goi.ToString()));
                lblTongChiphi.Text = String.Format(Utility.FormatDecimal(), Convert.ToDecimal(Tong_chiphi.ToString()));
                string canhbaotamung = THU_VIEN_CHUNG.Canhbaotamung(objLuotkham, dsData, m_dtTamung);
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CANHBAOTAMUNG_PHIEUDIEUTRI", "1", false) == "1")
                {
                    if (canhbaotamung.Trim() != "")
                        ShowErrorStatus(canhbaotamung);
                    else
                    {
                        if (ucError1 != null)
                        {
                            UIAction._Visible(ucError1, false);
                            ucError1.Reset();
                        }
                    }
                }
                Utility.SetMsg(lblChenhlech, "Tổng tạm ứng - Tổng chi phí = " + String.Format(Utility.FormatDecimal(), Convert.ToDecimal((Tong_Tamung - Tong_chiphi).ToString())), Tong_Tamung - Tong_chiphi > 0 ? false : true);
                lblCLS.Text = Utility.DoTrim(lblCLS.Text) == "" ? "0" : lblCLS.Text;
                lblThuoc.Text = Utility.DoTrim(lblThuoc.Text) == "" ? "0" : lblThuoc.Text;
                lblVTTH.Text = Utility.DoTrim(lblVTTH.Text) == "" ? "0" : lblVTTH.Text;
                lblBuonggiuong.Text = Utility.DoTrim(lblBuonggiuong.Text) == "" ? "0" : lblBuonggiuong.Text;
                lblDichvu.Text = Utility.DoTrim(lblDichvu.Text) == "" ? "0" : lblDichvu.Text;
                lblTongChiphi.Text = Utility.DoTrim(lblTongChiphi.Text) == "" ? "0" : lblTongChiphi.Text;
                lblChenhlech.Text = Utility.DoTrim(lblChenhlech.Text) == "" ? "Tổng tạm ứng - Tổng chi phí =0" : lblTongChiphi.Text;
            }
            catch (Exception ex)
            {

                
            }
        }
        void HideErrorStatus()
        {
            try
            {
                UIAction._Visible(ucError1, false);
                ucError1.Reset();
            }
            catch
            {
            }
        }
        void ShowErrorStatus(string msg)
        {
            try
            {
                UIAction._Visible(ucError1, true);
                ucError1.Reset();
                ucError1.Start(msg);


            }
            catch
            {
            }
        }
        /// <summary>
        /// Thực hiện thao tác Insert,Update,Delete tới CSDL theo m_enAction
        /// </summary>
        private void PerformAction()
        {
            try
            {
                ////Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                //if (!IsValidData())
                //{
                //    return;
                //}
                //objPhieudieutri.TrangThai = (byte?) (chkDaThucHien.Checked ? 1 : 0);
                //DataRow[] arrDr = m_dtPatients.Select("id_kham=" + txtIdPhieudieutri.Text);
                //if (arrDr.Length > 0)
                //{

                //    arrDr[0]["trang_thai"] = chkDaThucHien.Checked ? 1 : 0;
                //}
                //objPhieudieutri.IdBacsikham = Utility.Int16Dbnull(cboDoctorAssign.SelectedValue, -1);
                //if (!THU_VIEN_CHUNG.IsBaoHiem((byte) objLuotkham.IdLoaidoituongKcb))//Đối tượng dịch vụ được khóa ngay sau khi kết thúc khám
                //{
                //    objLuotkham.NguoiKetthuc = globalVariables.UserName;
                //    objLuotkham.NgayKetthuc = globalVariables.SysDate;
                //    objLuotkham.Locked = 1;
                //}
                //ActionResult actionResult =
                //   _KCB_THAMKHAM.UpdateExamInfo(
                //         CreateDiagInfo(), objPhieudieutri, objLuotkham);
                //switch (actionResult)
                //{
                //    case ActionResult.Success:
                //        IEnumerable<GridEXRow> query = from kham in grdList.GetDataRows()
                //                                       where
                //                                           kham.RowType == RowType.Record &&
                //                                           Utility.Int32Dbnull(kham.Cells[KcbDangkyKcb.Columns.IdKham].Value) ==
                //                                           Utility.Int32Dbnull(txtIdPhieudieutri.Text)
                //                                       select kham;
                //        if (query.Count() > 0)
                //        {
                //            GridEXRow gridExRow = query.FirstOrDefault();
                //            //gridExRow.BeginEdit();
                //            gridExRow.Cells[KcbDangkyKcb.Columns.TrangThai].Value = (byte?) (chkDaThucHien.Checked ? 1 : 0);
                //            gridExRow.Cells[KcbLuotkham.Columns.NguoiKetthuc].Value = globalVariables.UserName;
                //            gridExRow.Cells[KcbLuotkham.Columns.NgayKetthuc].Value = globalVariables.SysDate;
                //            //gridExRow.EndEdit();
                //            grdList.UpdateData();
                //            Utility.GotoNewRowJanus(grdList, KcbDangkyKcb.Columns.IdKham, Utility.sDbnull(txtIdPhieudieutri.Text));
                //        }
                //        cmdInphieudieutri.Visible = true;
                //        cboChonBenhAn.Visible = true && THU_VIEN_CHUNG.Laygiatrithamsohethong("BENH_AN", "0", true) == "1"; ;
                //        lblBenhan.Visible = cboChonBenhAn.Visible;
                //        chkDaThucHien.Checked = true;

                //        //Tự động ẩn BN về tab đã khám
                //        int Status = radChuaKham.Checked ? 0 : 1;
                //        if (Status == 0)
                //        {
                //            m_dtPatients.DefaultView.RowFilter = "1=1";
                //            m_dtPatients.DefaultView.RowFilter = "trang_thai=" + Status;
                //        }
                //        if (objLuotkham.Locked == 1)//Đối tượng dịch vụ được khóa ngay sau khi kết thúc khám
                //        {
                //            cmdUnlock.Visible = true;
                //            cmdUnlock.Enabled = true;
                //        }
                //        DmucNhanvien objStaff = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.UserNameColumn).IsEqualTo(Utility.sDbnull(objLuotkham.NguoiKetthuc, "")).ExecuteSingle<DmucNhanvien>();
                //        string TenNhanvien = objLuotkham.NguoiKetthuc;
                //        if (objStaff != null)
                //            TenNhanvien = objStaff.TenNhanvien;
                //        if (!cmdUnlock.Enabled)
                //            toolTip1.SetToolTip(cmdUnlock, "Bạn không có quyền mở khóa Bệnh nhân này. Đề nghị liên hệ " + TenNhanvien + "(" + objLuotkham.NguoiKetthuc + " - Là người khóa BN này) để được họ mở khóa. Hoặc liên hệ Quản trị hệ thống");
                //        else
                //            toolTip1.SetToolTip(cmdUnlock, "Nhấn vào đây để mở khóa cho bệnh nhân đang chọn(Phím tắt Ctrl+U). Điều kiện là chỉ mở khóa đối với đối tượng Dịch vụ. Muốn mở khóa đối tượng BHYT thì cần liên lạc với bộ phận thanh toán hủy in phôi BHYT");
                //        break;
                //    case ActionResult.Error:
                //        Utility.ShowMsg("Lỗi trong quá lưu thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                //        break;
                //}
                //ModifyCommmands();
                //cmdNhapVien.Enabled = objPhieudieutri.TrangThai == 1;
                //cmdHuyNhapVien.Enabled = objLuotkham.TrangthaiNoitru >= 1;
                //cmdNhapVien.Tag = objLuotkham.TrangthaiNoitru == 0 ? "0" : "1";
                //cmdNhapVien.Text = objLuotkham.TrangthaiNoitru == 0 ? "Nhập viện" : "Cập nhật";
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }

        #endregion

        private void tabDiagInfo_SelectedTabChanged(object sender, TabEventArgs e)
        {

        }

      
    }
}