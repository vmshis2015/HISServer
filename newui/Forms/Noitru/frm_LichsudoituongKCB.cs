using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Windows.Forms;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.StatusBar;
using SubSonic;
using SubSonic.Sugar;
using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VNS.HIS.DAL;
using VNS.UI.QMS;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;
using TriState = Janus.Windows.GridEX.TriState;
using VNS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs.AppUI;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.NGOAITRU;
namespace VNS.HIS.UI.NOITRU
{
    public delegate void SetParameterValueDelegate(string value, int IsUuTien);

    public delegate void SetParameterValueDelegateColose(Form frm);
    /// <summary>
    /// Đẩy thử code=Github
    /// </summary>
    public partial class frm_LichsudoituongKCB : Form
    {
        public delegate void OnActionSuccess();
        public event OnActionSuccess _OnActionSuccess;
        NoitruPhanbuonggiuong objBuonggiuong = null;
        NoitruPhanbuonggiuongCollection LstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        KCB_QMS _KCB_QMS = new KCB_QMS();
        DMUC_CHUNG _DMUC_CHUNG = new DMUC_CHUNG();
        private readonly AutoCompleteStringCollection namesCollectionThanhPho = new AutoCompleteStringCollection();
        private readonly string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";

        private readonly string strSaveandprintPath1 = Application.StartupPath +
                                                       @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";

        private string MA_DTUONG = "DV";
        private string SoBHYT = "";
        private string TrongGio = "";
        public bool m_blnCancel;
        private bool b_HasLoaded;
        private bool AllowTextChanged;
        private bool AllowGridSelecttionChanged=true;
        private string _rowFilter = "1=1";
        private bool b_NhapNamSinh;

        public GridEX grdList;
        private bool hasjustpressBACKKey;
        private bool isAutoFinding;
        bool m_blnHasJustInsert = false;
        private DataTable m_DC;

        private DataTable m_dtDataRoom = new DataTable();
        private DataTable m_dtDatabed = new DataTable();
        private DataTable m_PhongKham = new DataTable();
        private DataTable m_kieuKham;
        private DataTable m_dtChoKham = new DataTable();
        private DataTable m_dtDoiTuong = new DataTable();
        private DataTable m_dtTrieuChung = new DataTable();
        public action m_enAction = action.Insert;

        private DataTable m_dataDataRegExam = new DataTable();
        private DataTable mdt_DataQuyenhuyen;
        private frm_ScreenSoKham _QMSScreen;
        public DataTable m_dtPatient = new DataTable();
        string m_strMaluotkham = "";//Lưu giá trị patientcode khi cập nhật để người dùng ko được phép gõ Patient_code lung tung
        KcbDanhsachBenhnhan objBenhnhan = null;
        public frm_LichsudoituongKCB()
        {
            InitializeComponent();
            InitEvents();
            txtTEN_BN.CharacterCasing = globalVariables.CHARACTERCASING == 0
                                            ? CharacterCasing.Normal
                                            : CharacterCasing.Upper;
            dtFromDate.Value =dtToDate.Value= globalVariables.SysDate;
            ucBHYT1.Init();
            CauHinhKCB();
        }

        void InitEvents()
        {
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_LichsudoituongKCB_FormClosing);
            this.Load += new System.EventHandler(this.frm_LichsudoituongKCB_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_LichsudoituongKCB_KeyDown);
            txtMaBN.KeyDown += new KeyEventHandler(txtMaBN_KeyDown);
            txtMaLankham.KeyDown += new KeyEventHandler(txtMaLankham_KeyDown);
            dtpBOD.TextChanged += dtpBOD_TextChanged;
            txtTEN_BN.TextChanged+=new EventHandler(txtTEN_BN_TextChanged);
            txtTEN_BN.LostFocus += txtTEN_BN_LostFocus;
            txtCMT.KeyDown += txtCMT_KeyDown;
            cboPatientSex.SelectedIndex = 0;
            cmdThemMoiBN.Click += new System.EventHandler(cmdThemMoiBN_Click);
            cmdSave.Click += new System.EventHandler(cmdSave_Click);
            txtTuoi.TextChanged += new System.EventHandler(txtTuoi_TextChanged);
            txtTuoi.Click += new System.EventHandler(txtTuoi_Click);
            txtTuoi.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTuoi_KeyDown);

            txtTuoi.LostFocus += txtTuoi_LostFocus;
            txtNamSinh.TextChanged += txtNamSinh_TextChanged;
            txtNamSinh.LostFocus += txtNamSinh_LostFocus;
            chkChuyenVien.CheckedChanged += new EventHandler(chkChuyenVien_CheckedChanged);
            cboDoituongKCB.SelectedIndexChanged += new EventHandler(cboDoituongKCB_SelectedIndexChanged);
            cmdGetBV.Click += new EventHandler(cmdGetBV_Click);
            ucBHYT1._OnFindPatientByMatheBHYT += ucBHYT1__OnFindPatientByMatheBHYT;
        }

        void ucBHYT1__OnFindPatientByMatheBHYT(string matheBHYT)
        {
            FindPatientIDbyBHYT(matheBHYT);
        }
        void dtpBOD_TextChanged(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "1")
            {
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - dtpBOD.Value.Year);
            }
        }
        void cmdGetBV_Click(object sender, EventArgs e)
        {
            frm_danhsachbenhvien _danhsachbenhvien = new frm_danhsachbenhvien();
            if (_danhsachbenhvien.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtNoichuyenden.SetId(_danhsachbenhvien.idBenhvien);
            }
        }
        void cboDoituongKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowTextChanged) return;
                _MaDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
                objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                ChangeObjectRegion();
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void chkChuyenVien_CheckedChanged(object sender, EventArgs e)
        {
            txtNoichuyenden.Enabled = chkChuyenVien.Checked;
            cmdGetBV.Enabled = chkChuyenVien.Checked;
        }
        bool AutoLoad = false;
        void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
            CauHinhKCB();
        }

        private string GetSoBHYT
        {
            get { return SoBHYT; }
            set { SoBHYT = value; }
        }

        private void txtTEN_BN_LostFocus(object sender, EventArgs e)
        {
            txtTEN_BN.Text =Utility.CapitalizeWords(txtTEN_BN.Text.Trim());
        }
        private void txtMaLankham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaLankham.Text.Trim() != "")
            {
                ucBHYT1.ClearNoiDKKCBBD();
                isAutoFinding = true;
                string patient_ID = Utility.GetYY(globalVariables.SysDate) +
                                    Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLankham.Text, 0), "000000");
                txtMaLankham.Text = patient_ID;
                FindPatientIDbyMaLanKham(txtMaLankham.Text.Trim());
                isAutoFinding = false;
            }
        }

        private void FindPatientIDbyBHYT(string Insurance_Num)
        {
            try
            {

                DataTable temdt = SPs.KcbTimkiembenhnhantheomathebhyt(Insurance_Num).GetDataSet().Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), Insurance_Num);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select(KcbLuotkham.Columns.MatheBhyt+ "='" + Insurance_Num + "'");
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), Insurance_Num);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, Insurance_Num);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void FindPatientIDbyCMT(string CMT)
        {
            try
            {
                DataTable temdt = SPs.KcbTimkiembenhnhantheosocmt(CMT).GetDataSet().Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select(KcbDanhsachBenhnhan.Columns.Cmt+ "='" + CMT + "'");
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaBN.Text.Trim() != "")
            {
                ucBHYT1.ClearNoiDKKCBBD();
                isAutoFinding = true;
                FindPatient(txtMaBN.Text.Trim());
                isAutoFinding = false;
            }
        }

        private void FindPatient(string patient_ID)
        {
            try
            {
                QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql =
                    "Select id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan where id_benhnhan like '%" +
                    patient_ID + "%'";

                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    DataRow[] arrDr = temdt.Select("id_benhnhan=" + patient_ID);
                    if (arrDr.Length == 1)
                        AutoFindLastExamandFetchIntoControls(arrDr[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                    else
                    {
                        var _ChonBN = new frm_CHON_BENHNHAN();
                        _ChonBN.temdt = temdt;
                        _ChonBN.ShowDialog();
                        if (!_ChonBN.mv_bCancel)
                        {
                            AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
        }

        private void FindPatientIDbyMaLanKham(string malankham)
        {
            try
            {
                QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql =
                    "Select id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan p where exists(select 1 from kcb_luotkham where id_benhnhan=P.id_benhnhan and ma_luotkham like '%" +
                    malankham + "%')";
                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count <= 0) return;
                if (temdt.Rows.Count == 1)
                {
                    AutoFindLastExamandFetchIntoControls(temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan].ToString(), string.Empty);
                }
                else //Show dialog for select
                {
                    var _ChonBN = new frm_CHON_BENHNHAN();
                    _ChonBN.temdt = temdt;
                    _ChonBN.ShowDialog();
                    if (!_ChonBN.mv_bCancel)
                    {
                        AutoFindLastExamandFetchIntoControls(_ChonBN.Patient_ID, string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
            }
            
        }

        private void AutoFindLastExamandFetchIntoControls(string patientID, string sobhyt)
        {
            try
            {
                if (!Utility.CheckLockObject(m_strMaluotkham, "Tiếp đón", "TD"))
                    return;
                //Trả lại mã lượt khám nếu chưa được dùng đến
                new Update(KcbDmucLuotkham.Schema)
                       .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(0)
                       .Set(KcbDmucLuotkham.Columns.UsedBy).EqualTo(DBNull.Value)
                       .Set(KcbDmucLuotkham.Columns.StartTime).EqualTo(DBNull.Value)
                       .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(null)
                       .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.Int32Dbnull(m_strMaluotkham, "-1"))
                       .And(KcbDmucLuotkham.Columns.TrangThai).IsEqualTo(1)
                       .And(KcbDmucLuotkham.Columns.UsedBy).IsEqualTo(globalVariables.UserName)
                       .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year).Execute();
                ;

                SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(patientID);
                if (!string.IsNullOrEmpty(sobhyt))
                {
                    sqlQuery.And(KcbLuotkham.Columns.MatheBhyt).IsEqualTo(sobhyt);
                }
                sqlQuery.OrderDesc(KcbLuotkham.Columns.NgayTiepdon);

                var objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                if (objLuotkham != null)
                {
                    txtMaBN.Text = patientID;
                    txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    m_strMaluotkham = objLuotkham.MaLuotkham;
                    m_enAction = action.Update;
                    AllowTextChanged = false;
                    LoadThongtinBenhnhan();
                    ModifyCommand();
                }
                else
                {
                    Utility.ShowMsg("Không tìm thấy thông tin bệnh nhân. Đề nghị liên hệ với VMS để được giải đáp");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("AutoFindLastExam().Exception-->" + ex.Message);
            }
            finally
            {
                AllowTextChanged = true;
            }
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của phần dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_LichsudoituongKCB_Load(object sender, EventArgs e)
        {
            try
            {
                AllowTextChanged = false;
                b_HasLoaded = false;
                XoathongtinBHYT(true);
                AddAutoCompleteDiaChi();
                AutocompleteBenhvien();
                
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(), DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "", false);
                objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                ChangeObjectRegion();
                ModifyCommand();
                AllowTextChanged = true;
            }
            catch
            {
            }
            finally
            {
                if (PropertyLib._ConfigProperties.HIS_AppMode != VNS.Libs.AppType.AppEnum.AppMode.License)
                    this.Text = "Đăng ký KCB -->Demo 1500";
                ModifyCommand();
                b_HasLoaded = true;
                
            }
        }
        byte _IdLoaidoituongKcb = 1;
        Int16 _IdDoituongKcb = 1;
        string _MaDoituongKcb = "DV";
        string _TenDoituongKcb = "Dịch vụ";
        decimal PtramBhytCu = 0m;
        decimal PtramBhytGocCu = 0m;
        KcbLuotkham objLuotkham = null;
        private void LoadThongtinBenhnhan()
        {
            PtramBhytCu = 0m;
            PtramBhytGocCu = 0m;
            AllowTextChanged = false;
            LstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
            objBuonggiuong = null;
            objBenhnhan = KcbDanhsachBenhnhan.FetchByID(txtMaBN.Text);
            if (objBenhnhan != null)
            {
                txtTEN_BN.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan);
                txtNamSinh.Text = Utility.sDbnull(objBenhnhan.NamSinh);
                
                txtDiachi.Text = Utility.sDbnull(objBenhnhan.DiaChi);
                if (objBenhnhan.NgaySinh != null) dtpBOD.Value = objBenhnhan.NgaySinh.Value;
                else dtpBOD.Value = new DateTime((int)objBenhnhan.NamSinh, 1, 1);
                txtNamSinh.Text = Utility.sDbnull(objBenhnhan.NamSinh);
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(objBenhnhan.NamSinh));
                cboPatientSex.SelectedIndex = Utility.GetSelectedIndex(cboPatientSex, Utility.sDbnull(objBenhnhan.IdGioitinh));
                txtCMT.Text = Utility.sDbnull(objBenhnhan.Cmt);


                objLuotkham = new Select().From(KcbLuotkham.Schema)
                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaLankham.Text)
                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text, -1)).ExecuteSingle
                   <KcbLuotkham>();
                if (objLuotkham != null)
                {
                    m_strMaluotkham = objLuotkham.MaLuotkham;
                    
                    txtSolankham.Text = Utility.sDbnull(objLuotkham.SolanKham);
                    _IdDoituongKcb = objLuotkham.IdDoituongKcb;
                    dtpInputDate.Value = objLuotkham.NgayTiepdon;
                    dtCreateDate.Value = objLuotkham.NgayTiepdon;
                   
                    _MaDoituongKcb = Utility.sDbnull(objLuotkham.MaDoituongKcb);
                    objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();

                    ChangeObjectRegion();
                    PtramBhytCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                    _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
                    _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
                    cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                    chkChuyenVien.Checked = Utility.Int32Dbnull(objLuotkham.TthaiChuyenden, 0) == 1;
                    txtNoichuyenden.SetId(Utility.Int32Dbnull(objLuotkham.IdBenhvienDen, -1));
                    if (!string.IsNullOrEmpty(objLuotkham.MatheBhyt))//Thông tin BHYT
                    {
                        ucBHYT1.SetValues(chkChuyenVien.Checked, objDoituongKCB, objLuotkham);
                    }
                    else
                    {
                        XoathongtinBHYT(true);
                    }
                }
                else
                {
                }
            }
            chkChuyenVien_CheckedChanged(chkChuyenVien, new EventArgs());
        }

        void XoathongtinBHYT(bool forcetodel)
        {
            if (forcetodel)
            {
                _IdDoituongKcb = 1;
                _MaDoituongKcb = "DV";
                _TenDoituongKcb = "Dịch vụ";
                ucBHYT1.ClearMe();
            }
        }
       
        private void AddAutoCompleteDiaChi()
        {
            ucBHYT1.DoAutoComplete();
        }
        
        private void AutocompleteBenhvien()
        {
          
            try
            {
                DataTable m_dtBenhvien = new Select().From(DmucBenhvien.Schema).ExecuteDataSet().Tables[0];
                if (m_dtBenhvien == null) return;
                txtNoichuyenden.Init(m_dtBenhvien, new List<string>() { DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien });
               
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
              

            }
        }
       
        /// <summary>
        /// hàm thực hiện viecj tính tuổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNamSinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "1") return;
                if (txtNamSinh.Text.Length < 4) return;
                if (!string.IsNullOrEmpty(txtNamSinh.Text))
                {
                    txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtNamSinh.Text, 0));
                }
                else
                {
                    txtTuoi.Clear();
                }
                if (txtNamSinh.Focused)
                {
                    txtTuoi.Focus();
                    txtTuoi.SelectAll();
                }

            }
            catch (Exception exception)
            {
            }
        }

        private void txtTuoi_LostFocus(object sender, EventArgs e)
        {
            //txtNamSinh.TextChanged += new EventHandler(txtNamSinh_TextChanged);   
        }

        /// <summary>
        /// hàm thực hiện việc tính toán tuổi của bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTuoi.Text))
                {
                    if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "0")
                        txtNamSinh.Text = Utility.sDbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtTuoi.Text, 0));
                    else
                        dtpBOD.Value = new DateTime(Utility.Int32Dbnull(globalVariables.SysDate.Year - Utility.Int32Dbnull(txtTuoi.Text, 0)),dtpBOD.Value.Month, dtpBOD.Value.Day);
                }
            }
            catch (Exception exception)
            {
            }
        }

      
        /// <summary>
        /// hàm thực hiện việc làm sách thông tin của bệnh nhân
        /// </summary>
        private void ClearControl()
        {
            Utility.SetMsg(lblMsg, "", false);
            //tabControl1.SelectedTab = tabControl1.TabPages[0];
            objBuonggiuong = null;
            objLuotkham = null;
            LstNoitruPhanbuonggiuong = new NoitruPhanbuonggiuongCollection();
            m_blnHasJustInsert = false;
            txtSolankham.Text = "1";
            txtTEN_BN.Clear();
            txtNamSinh.Clear();
            dtpBOD.Value = globalVariables.SysDate;
            txtTuoi.Clear();
            txtCMT.Clear();
            txtDiachi.Clear();
            chkChuyenVien.Checked = false;
            txtNoichuyenden.SetCode("-1");
            if (m_dtDataRoom != null) m_dtDataRoom.Clear();
            if (m_dtDatabed != null) m_dtDatabed.Clear();
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
               this.Text= "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
            ModifyCommand();
            AllowTextChanged = false;
            XoathongtinBHYT(true);

            _MaDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
            objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
            if (objDoituongKCB == null) return;
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _IdLoaidoituongKcb = objDoituongKCB.IdLoaidoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;
           

           
            chkChuyenVien.Checked = false;
           
            AllowTextChanged = true;
            //Chuyển về trạng thái thêm mới
            m_enAction = action.Insert;
            if (PropertyLib._KCBProperties.SexInput) cboPatientSex.SelectedIndex = -1;
            m_dataDataRegExam.Clear();
            ucBHYT1.ResetMe(objDoituongKCB);
            if (ucBHYT1.IsBHYT)
            {
               
            }
            else
            {
                PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
                PtramBhytGocCu = PtramBhytCu;
                txtTEN_BN.Focus();
            }
            if (m_enAction == action.Insert)
            {
                dtpInputDate.Value = globalVariables.SysDate;
                dtCreateDate.Value = globalVariables.SysDate;
               
            }
        }

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        /// <summary>
        /// hàm thực hiện viecj thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện việc lưu thông tin của đối tượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
               
                cmdSave.Enabled = false;
                PerformAction();
                cmdSave.Enabled = true;
            }
            catch
            {
            }
            finally
            {
                cmdSave.Enabled = true;
            }
        }
       
        private bool IsValidData()
        {
            if (m_enAction==action.Insert && dtCreateDate.Value.ToString("dd/MM/yyyy") != globalVariables.SysDate.ToString("dd/MM/yyyy"))
            {
                if (!Utility.AcceptQuestion("Ngày tiếp đón khác ngày hiện tại. Bạn có chắc chắn hay không?","Cảnh báo",true))
                {
                    dtCreateDate.Focus();
                    return false;
                }
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(_IdLoaidoituongKcb))
            {
                if (!ucBHYT1.IsValidBHYT()) return false;
                if (!ucBHYT1.IsValidTheBHYT()) return false;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(objDoituongKCB.IdLoaidoituongKcb))
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BHYT", "0", false) == "1")
                {
                    if (Utility.DoTrim(ucBHYT1.txtDiachi_bhyt.Text) == "")
                    {
                        Utility.SetMsg(lblMsg, "Bạn phải nhập địa chỉ thẻ BHYT", true);
                        ucBHYT1.txtDiachi_bhyt.Focus();
                        return false;
                    }
                }
                if (Utility.DoTrim(ucBHYT1.txtMaDTsinhsong.Text) != "" && ucBHYT1.txtMaDTsinhsong.myCode == "-1")
                {
                    Utility.SetMsg(lblMsg, "Mã đối tượng sinh sống chưa đúng. Mời bạn nhập lại", true);
                    ucBHYT1.txtMaDTsinhsong.SelectAll();
                    ucBHYT1.txtMaDTsinhsong.Focus();
                    return false;
                }

                
            }
            if (chkChuyenVien.Checked)
            {
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BATNHAPNOICHUYENDEN", "0", false) == "1")
                {
                    if (txtNoichuyenden.MyCode == "-1")
                    {
                        Utility.SetMsg(lblMsg, "Bạn phải nhập bệnh viện chuyển đến", true);
                        txtNoichuyenden.SelectAll();
                        txtNoichuyenden.Focus();
                        return false;
                    }
                }
            }
            if (string.IsNullOrEmpty(txtTEN_BN.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên Bệnh nhân", true);
                txtTEN_BN.Focus();
                return false;
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "0" && string.IsNullOrEmpty(txtNamSinh.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập ngày tháng năm sinh, hoặc năm sinh cho bệnh nhân ", true);
                txtNamSinh.Focus();
                return false;
            }
            if (cboPatientSex.SelectedIndex<0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn giới tính của Bệnh nhân",true);
                cboPatientSex.Focus();
                return false;
            }

            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "0", false) == "1")
                {
                    if (Utility.DoTrim(txtDiachi.Text) == "")
                    {
                        Utility.SetMsg(lblMsg, "Bạn phải nhập địa chỉ Bệnh nhân", true);
                        txtDiachi.Focus();
                        return false;
                    }
                }
            
            return isValidIdentifyNum();
        }

       

        private void  ModifyCommand()
        {
            cmdSave.Enabled = Utility.DoTrim(txtTEN_BN.Text).Length > 0;
        }
        private void PerformAction()
        {
            if (!IsValidData()) return;
            switch (m_enAction)
            {
                case action.Update:
                    if (!IsValid4Update()) return;
                    CapnhatthongtinBenhnhan();
                    break;
                case action.Insert:
                    InsertPatient();
                    break;
               
            }
           
            ModifyCommand();
        }

        private bool IsValid4Update()
        {
            try
            {
               
               
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi kiểm tra hợp lệ dữ liệu trước khi cập nhật Bệnh nhân",ex);
                return false;
            }
        }

       
        private void txtTuoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) cboPatientSex.Focus();
        }

        /// <summary>
        /// ham thwucj hiện việc chọn thông tin tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        void ChangeObjectRegion()
        {
            if (objDoituongKCB == null) return;
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _IdLoaidoituongKcb = objDoituongKCB.IdLoaidoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;
            ucBHYT1.ChangeObjectRegion(objDoituongKCB);
            if (objDoituongKCB.IdLoaidoituongKcb == 0)//ĐỐi tượng BHYT
            {
               
            }
            else//Đối tượng khác BHYT
            {
                XoathongtinBHYT(PropertyLib._KCBProperties.XoaBHYT);
                txtTEN_BN.Focus();
            }
        }
        DmucDoituongkcb objDoituongKCB = null;
        /// <summary>
        /// hàm thực hienej phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_LichsudoituongKCB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (this.ActiveControl != null && this.ActiveControl.Name == txtTEN_BN.Name && Utility.DoTrim(txtTEN_BN.Text)!="")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM();
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", Utility.DoTrim(txtTEN_BN.Text), "", "", "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ucBHYT1.ClearNoiDKKCBBD();
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                else if (this.ActiveControl != null && this.ActiveControl.Name == txtCMT.Name && Utility.DoTrim(txtCMT.Text) != "")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM();
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", "", Utility.DoTrim(txtCMT.Text), "", "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ucBHYT1.ClearNoiDKKCBBD();
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                
                return;
            }

            if (e.Control && e.KeyCode == Keys.D)
            {
               
                _MaDoituongKcb = "DV";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                return;
            }
            if (e.Control && e.KeyCode == Keys.B)
            {
                _MaDoituongKcb = "BHYT";
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                return;
            }
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.P))
            {
                AllowTextChanged = false;
                txtDiachi.Text = ucBHYT1.txtDiachi_bhyt.Text;
                AllowTextChanged = true;
                return;
            }
            
            string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
            
            if (e.Control && e.KeyCode == Keys.F)
            {
                txtMaBN.SelectAll();
                txtMaBN.Focus();
            }
            
            //if (e.KeyCode == Keys.F1)
            //{
            //    tabControl1.SelectedTab = tabControl1.TabPages[0];
            //    return;
            //}
            //if (e.KeyCode == Keys.F2)
            //{
            //    tabControl1.SelectedTab = tabControl1.TabPages[1];
            //    return;
            //}
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.Escape && this.ActiveControl != null
                && (
                this.ActiveControl.GetType()!=txtDiachi.GetType()
                || this.ActiveControl.GetType() != txtNoichuyenden.GetType()
                ))
            {

                Close();
            }
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoiBN.PerformClick();
            if ( e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }


        private void cboThanhPho_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void txtTEN_BN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cmdSave.Enabled = Utility.DoTrim(txtTEN_BN.Text).Length > 0;
            }
            catch (Exception exception)
            {
            }
        }
        private void txtNamSinh_LostFocus(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "1") return;
            if (!string.IsNullOrEmpty(txtNamSinh.Text))
            {
                if (txtNamSinh.Text.Length < 4)
                {
                    Utility.ShowMsg("Năm sinh của bệnh nhân phải là 4 số", "Thông báo", MessageBoxIcon.Information);
                    txtNamSinh.Focus();
                    txtNamSinh.SelectAll();
                }
            }
        }

        private void txtTuoi_Click(object sender, EventArgs e)
        {
        }
        private void txtMaQuyenloi_BHYT_Click(object sender, EventArgs e)
        {
        }

        private void lnkCungDC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AllowTextChanged = false;
            txtDiachi.Text = ucBHYT1.txtDiachi_bhyt.Text;
            AllowTextChanged = true;
        }

        private void txtCMT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter && txtCMT.Text.Trim() != "")
            {
                FindPatientIDbyCMT(txtCMT.Text.Trim());
            }
        }

        private void txtKieuKham_Enter(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text= "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
            
        }

        private void txtPhongkham_Enter(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.IsNgoaiGio())
            {
                this.Text = "Bệnh nhân đang khám dịch vụ ngoài giờ";
            }
           
        }

        private bool isQMSActive(string name)
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

        private void frm_LichsudoituongKCB_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Utility.FreeLockObject(m_strMaluotkham);
            }
            catch (Exception exception)
            {
            }
        }
       
        private void CauHinhKCB()
        {
            dtpBOD.Value = globalVariables.SysDate;
            dtpBOD.Visible=THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "1";
            txtNamSinh.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_NHAP_NGAYTHANGNAMSINH", false) == "0";
            if (dtpBOD.Visible)
                txtTuoi.Text = Utility.sDbnull(globalVariables.SysDate.Year - dtpBOD.Value.Year);
           
        }

       

        #region "Sự kiện bắt cho phần khám bệnh"
                     
        #endregion

        #region "khởi tạo sự kiện để lưu lại thông tin của bệnh nhân"

        private string mavuasinh = "";

        private void ThemMoiLanKhamVaoLuoi()
        {
            try
            {
                DataRow dr = m_dtPatient.NewRow();
                dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan] = objBenhnhan.IdBenhnhan;
                dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = objBenhnhan.TenBenhnhan;

               
                m_dtPatient.Rows.InsertAt(dr, 0);
            }
            catch (Exception)
            {
                
               
            }
            
        }

        private void UpdateBNVaoTrenLuoi()
        {
            try
            {
                EnumerableRowCollection<DataRow> query = from bn in m_dtPatient.AsEnumerable()
                                                         where
                                                             Utility.sDbnull(bn[KcbLuotkham.Columns.MaLuotkham]) ==
                                                             txtMaLankham.Text
                                                         select bn;
                if (query.Count() > 0)
                {
                    DataRow dr = query.FirstOrDefault();
                    dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan] = objBenhnhan.IdBenhnhan;
                    dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = objBenhnhan.TenBenhnhan;

                    dr[KcbDanhsachBenhnhan.Columns.GioiTinh] = objBenhnhan.GioiTinh;
                    dr[KcbDanhsachBenhnhan.Columns.IdGioitinh] = objBenhnhan.IdGioitinh;

                    dr[KcbDanhsachBenhnhan.Columns.NamSinh] = objBenhnhan.NamSinh;

                   
                    m_dtPatient.AcceptChanges();
                }
            }
            catch (Exception)
            {
                
            }
           
        }

        private bool isValidIdentifyNum()
        {
            try
            {
                if (txtCMT.Text.Trim() == "") return true;
                string sql = "";
                QueryCommand cmd = KcbDanhsachBenhnhan.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                sql =
                    "Select cmt,id_benhnhan,ten_benhnhan,gioi_tinh from kcb_danhsach_benhnhan ";
                sql += " where cmt = '" + txtCMT.Text.Trim() + "'";
                if (m_enAction == action.Insert)
                    sql += "";
                else //Là update hoặc thêm mới lần khám cần kiểm tra có trùng với BN khác chưa
                    sql += " AND id_benhnhan <> " + txtMaBN.Text.Trim();
                cmd.CommandSql = sql;
                DataTable temdt = DataService.GetDataSet(cmd).Tables[0];
                if (temdt.Rows.Count > 0)
                {
                    Utility.ShowMsg(
                        string.Format("Số CMT này đang được sử dụng cho Bệnh nhân {0}:{1}\nMời bạn kiểm tra lại",
                                      temdt.Rows[0][KcbDanhsachBenhnhan.Columns.IdBenhnhan], temdt.Rows[0]["ten_benhnhan"]));
                    txtCMT.Focus();
                    return false;
                }
                return temdt.Rows.Count <= 0;
            }
            catch
            {
                return false;
            }
        }
        private void InsertPatient()
        {
            
            //objBenhnhan = TaoBenhNhan();
            //objLuotkham = TaoLuotkham();
           
               
            //long v_id_kham = -1;
            //string msg = "";
            //errorProvider1.Clear();
            //ActionResult actionResult = _KCB_DANGKY.ThemmoiBenhnhanCapcuu(objBenhnhan, objLuotkham, objSokham,objBuonggiuong,
            //                                                                ngaychuyenkhoa, ref msg);

            //if (msg.Trim() != "")
            //{
            //    errorProvider1.SetError(txtSoKcb, msg);
            //}
            //switch (actionResult)
            //{
            //    case ActionResult.Success:

            //        if (objLuotkham.SoBenhAn!=null && objLuotkham.SoBenhAn != txtSoBenhAn.Text)
            //        {
            //            Utility.ShowMsg(string.Format( "Chú ý: Số bệnh án nội trú {0} đã được Bệnh nhân khác sử dụng nên số bệnh án nội trú mới của Bệnh nhân là {1}",txtSoBenhAn.Text,objLuotkham.SoBenhAn ));
            //        }
            //        txtSoBenhAn.Text = objLuotkham.SoBenhAn;

            //        PtramBhytCu = Utility.DecimaltoDbnull(ucBHYT1.txtPtramBHYT.Text, 0);
            //        PtramBhytGocCu = Utility.DecimaltoDbnull(ucBHYT1.txtptramDauthe.Text, 0);
            //        txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
            //        txtMaBN.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
            //        mavuasinh = Utility.sDbnull(objLuotkham.IdBenhnhan);
            //        m_enAction = action.Update;
            //        m_blnHasJustInsert = true;
            //        m_strMaluotkham = txtMaLankham.Text;
            //        ThemMoiLanKhamVaoLuoi();
            //        if (_OnActionSuccess != null) _OnActionSuccess();
            //        Utility.SetMsg(lblMsg, "Bạn thêm mới bệnh nhân thành công", false);
            //        Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
            //        m_blnCancel = false;
            //        if (chkTudongthemmoi.Checked)
            //        {
            //            cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
            //        }
            //         else
            //        {
            //            ucTamung1.ChangePatients(objLuotkham, "LYDOTAMUNGCAPCUU");
            //            tabControl1.SelectedTab = tabControl1.TabPages[1];
            //            ucTamung1.Themmoi();
            //        }
                        
            //        txtMaBN.Text = Utility.sDbnull(mavuasinh);
            //        break;
            //    case ActionResult.Error:
            //        Utility.SetMsg(lblMsg, "Bạn thực hiện thêm dữ liệu không thành công !", true);
            //        cmdSave.Focus();
            //        break;
            //}
        }
       
       
        private void CapnhatthongtinBenhnhan()
        {
            //DateTime ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
            //                              dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
            //                              Utility.Int32Dbnull(txtPhut.Text), 00);
            //objBenhnhan = TaoBenhNhan();
            
            //objLuotkham = TaoLuotkham();
            
            //    objBuonggiuong = TaodulieuBuonggiuong();
            //KcbDangkySokham objSokham = TaosoKCB();
            //string msg = "";
            //errorProvider1.Clear();
            //ActionResult actionResult = _KCB_DANGKY.UpdateBenhnhanCapcuu(objBenhnhan, objLuotkham, objSokham, objBuonggiuong, ngaychuyenkhoa, PtramBhytCu, PtramBhytGocCu, ref msg);
            //// THEM_PHI_DVU_KYC(objLuotkham);
            //if (msg.Trim() != "")
            //{
            //    errorProvider1.SetError(txtSoKcb, msg);
            //}
            //switch (actionResult)
            //{
            //    case ActionResult.Success:
                   
            //        if (objLuotkham.SoBenhAn!=null && objLuotkham.SoBenhAn != txtSoBenhAn.Text)
            //        {
            //            Utility.ShowMsg(string.Format( "Chú ý: Số bệnh án nội trú {0} đã được Bệnh nhân khác sử dụng nên số bệnh án nội trú mới của Bệnh nhân là {1}",txtSoBenhAn.Text,objLuotkham.SoBenhAn ));
            //        }
            //        txtSoBenhAn.Text=objLuotkham.SoBenhAn;
            //        //gọi lại nếu thay đổi địa chỉ
            //        m_blnHasJustInsert = false;
            //        PtramBhytCu = Utility.DecimaltoDbnull(ucBHYT1.txtPtramBHYT.Text, 0);
            //        PtramBhytGocCu = Utility.DecimaltoDbnull(ucBHYT1.txtptramDauthe.Text, 0);
            //        Utility.SetMsg(lblMsg, "Bạn sửa thông tin Bệnh nhân thành công", false);
            //        UpdateBNVaoTrenLuoi();
            //        if (_OnActionSuccess != null) _OnActionSuccess();
                    
            //        if (string.IsNullOrEmpty(objLuotkham.MatheBhyt))
            //        {
            //            ucBHYT1.ClearMe();
            //            chkChuyenVien.Checked = false;
            //        }
            //        Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
            //        m_blnCancel = false;

            //        break;
            //    case ActionResult.Error:
            //        Utility.SetMsg(lblMsg, "Bạn thực hiện sửa thông tin không thành công !", true);
            //        break;
            //    case ActionResult.Cancel:
            //        Utility.ShowMsg(string.Format("Bệnh nhân này đã thanh toán một số dịch vụ nên bạn không được phép chuyển đối tượng hoặc thay đổi phần trăm BHYT\nPhần trăm cũ {0} % - Phần trăm mới {1} %", PtramBhytCu.ToString(), ucBHYT1.txtPtramBHYT.Text), "Cảnh báo");
            //        break;
            //}
        }

        

        /// <summary>
        /// hàm thực hiện việc khwoir tạo thoog tin PatietnExam
        /// </summary>
        /// <returns></returns>
        private KcbLuotkham TaoLuotkham()
        {
           
            //if (m_enAction == action.Insert || m_enAction == action.Add)
            //{
            //    objLuotkham = new KcbLuotkham();
            //    objLuotkham.IsNew = true;
            //}
            //else
            //{
            //    objLuotkham.IsLoaded = true;
            //    objLuotkham.MarkOld();
            //    objLuotkham.IsNew = false;
            //}
            //if (string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.SoBenhAn, "")))
            //{
            //    txtSoBenhAn.Text = THU_VIEN_CHUNG.LaySoBenhAn();
            //}
            //else
            //{
            //    txtSoBenhAn.Text = Utility.sDbnull(objLuotkham.SoBenhAn, "");
            //}
            //objLuotkham.SoBenhAn = Utility.sDbnull(txtSoBenhAn.Text);
            //objLuotkham.MotaNhapvien = Utility.DoTrim(txtGhiChu.Text);

            //objLuotkham.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            //objLuotkham.Noitru = 0;
            //objLuotkham.IdDoituongKcb = _IdDoituongKcb;
            //objLuotkham.IdLoaidoituongKcb = _IdLoaidoituongKcb;
            //objLuotkham.Locked = 0;
            //objLuotkham.HienthiBaocao = 1;
            //objLuotkham.TrangthaiCapcuu = 1;
            //objLuotkham.CachTao = 1;
            //objLuotkham.IdKhoatiepnhan = globalVariables.idKhoatheoMay;
            //objLuotkham.NguoiTao = globalVariables.UserName;
            //objLuotkham.NgayTao = globalVariables.SysDate;
            //objLuotkham.Cmt = Utility.sDbnull(txtCMT.Text, "");
            //objLuotkham.DiaChi = txtDiachi.Text;
            //objLuotkham.Email = "";
            //objLuotkham.NoiGioithieu = "";
            //objLuotkham.NhomBenhnhan = "-1";
            //objLuotkham.IdBenhvienDen = Utility.Int16Dbnull(txtNoichuyenden.MyID, -1);
            //objLuotkham.TthaiChuyenden = (byte)(chkChuyenVien.Checked ? 1 : 0);
            //if (THU_VIEN_CHUNG.IsBaoHiem(_IdLoaidoituongKcb))
            //{
            //    ucBHYT1.Laymathe_BHYT();
            //    objLuotkham.MaKcbbd = Utility.sDbnull(ucBHYT1.txtNoiDKKCBBD.Text, "");
            //    objLuotkham.NoiDongtrusoKcbbd = Utility.sDbnull(ucBHYT1.txtNoiDongtrusoKCBBD.Text, "");
            //    objLuotkham.MaNoicapBhyt = Utility.sDbnull(ucBHYT1.txtNoiphattheBHYT.Text);
            //    objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
            //    objLuotkham.MatheBhyt = ucBHYT1.Laymathe_BHYT();
            //    objLuotkham.MaDoituongBhyt = Utility.sDbnull(ucBHYT1.txtMaDtuong_BHYT.Text);
            //    objLuotkham.MaQuyenloi = Utility.Int32Dbnull(ucBHYT1.txtMaQuyenloi_BHYT.Text, null);
            //    objLuotkham.DungTuyen = !ucBHYT1.chkTraiTuyen.Visible ? 1 : (((byte?)(ucBHYT1.chkTraiTuyen.Checked ? 0 : 1)));

            //    objLuotkham.MadtuongSinhsong = ucBHYT1.txtMaDTsinhsong.myCode;
            //    objLuotkham.GiayBhyt = Utility.Bool2byte(ucBHYT1.chkGiayBHYT.Checked);

            //    objLuotkham.NgayketthucBhyt = ucBHYT1.dtInsToDate.Value.Date;
            //    objLuotkham.NgaybatdauBhyt = ucBHYT1.dtInsFromDate.Value.Date;
            //    objLuotkham.NoicapBhyt = Utility.GetValue(ucBHYT1.lblNoiCapThe.Text, false);
            //    objLuotkham.DiachiBhyt = Utility.sDbnull(ucBHYT1.txtDiachi_bhyt.Text);
                
            //}
            //else
            //{
            //    objLuotkham.GiayBhyt = 0;
            //    objLuotkham.MadtuongSinhsong = "";
            //    objLuotkham.MaKcbbd = "";
            //    objLuotkham.NoiDongtrusoKcbbd = "";
            //    objLuotkham.MaNoicapBhyt = "";
            //    objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
            //    objLuotkham.MatheBhyt = "";
            //    objLuotkham.MaDoituongBhyt = "";
            //    objLuotkham.MaQuyenloi = -1;
            //    objLuotkham.DungTuyen = 0;
               
            //    objLuotkham.NgayketthucBhyt = null;
            //    objLuotkham.NgaybatdauBhyt = null;
            //    objLuotkham.NoicapBhyt = "";
            //    objLuotkham.DiachiBhyt = "";
               
            //}
            
            //objLuotkham.SolanKham = Utility.Int16Dbnull(txtSolankham.Text, 0);
            //objLuotkham.TrieuChung = Utility.ReplaceStr(txtTrieuChungBD.Text);
            ////Tránh lỗi khi update người dùng nhập mã lần khám lung tung
            //if (m_enAction == action.Update) txtMaLankham.Text = m_strMaluotkham;
            //objLuotkham.MaLuotkham = Utility.sDbnull(txtMaLankham.Text, "");
            //objLuotkham.IdBenhnhan = Utility.Int64Dbnull(txtMaBN.Text, -1);
            //DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_IdDoituongKcb);
            //if (objectType != null)
            //{
            //    objLuotkham.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
            //}
            //if (m_enAction == action.Update)
            //{
            //    objLuotkham.NgayTiepdon = dtCreateDate.Value;
            //    objLuotkham.NguoiSua = globalVariables.UserName;
            //    objLuotkham.NgaySua = globalVariables.SysDate;
            //    objLuotkham.IpMaysua = globalVariables.gv_strIPAddress;
            //    objLuotkham.TenMaysua = globalVariables.gv_strComputerName;
            //}
            //if (m_enAction == action.Add || m_enAction == action.Insert)
            //{
            //    objLuotkham.NgayTiepdon = dtCreateDate.Value;
            //    objLuotkham.NguoiTiepdon = globalVariables.UserName;

            //    objLuotkham.IpMaytao = globalVariables.gv_strIPAddress;
            //    objLuotkham.TenMaytao = globalVariables.gv_strComputerName;
            //}
            //objLuotkham.PtramBhytGoc = Utility.DecimaltoDbnull(ucBHYT1.txtptramDauthe.Text, 0);
            //objLuotkham.PtramBhyt = Utility.DecimaltoDbnull(ucBHYT1.txtPtramBHYT.Text, 0);
            return objLuotkham;
        }

        #endregion

       
       
       
    }
}