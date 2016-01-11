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
namespace VNS.HIS.UI.NGOAITRU
{
   
    public partial class frm_Dangky_Kiemnghiem : Form
    {
        public delegate void OnActionSuccess();
        public event OnActionSuccess _OnActionSuccess;
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
        private bool b_HasSecondScreen;
        private bool b_NhapNamSinh;
       
        public GridEX grdList;
        private bool hasjustpressBACKKey;
        private bool isAutoFinding;
        bool m_blnHasJustInsert = false;
        private DataTable m_DC;
        private DataTable m_dtDoiTuong = new DataTable();
        public action m_enAction = action.Insert;
        private string m_strDefaultLazerPrinterName = "";
        private DataTable mdt_DataQuyenhuyen;
        public DataTable m_dtPatient = new DataTable();
        string m_strMaluotkham = "";//Lưu giá trị patientcode khi cập nhật để người dùng ko được phép gõ Patient_code lung tung
        string Args = "ALL";
        public delegate void OnAssign();
        public event OnAssign _OnAssign;
        public frm_Dangky_Kiemnghiem(string Args)
        {
            InitializeComponent();
            this.Args = Args;
            dtCreateDate.Value = globalVariables.SysDate;

            InitEvents();
            CauHinhKCB();
        }

        void InitEvents()
        {
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Dangky_Kiemnghiem_FormClosing);
            this.Load += new System.EventHandler(this.frm_Dangky_Kiemnghiem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Dangky_Kiemnghiem_KeyDown);

            txtMaBN.KeyDown += new KeyEventHandler(txtMaBN_KeyDown);
            txtMaLankham.KeyDown += new KeyEventHandler(txtMaLankham_KeyDown);

            txtTenKH.TextChanged += new EventHandler(txtTEN_BN_TextChanged);
            
            
            cmdConfig.Click += new EventHandler(cmdConfig_Click);
            
            cmdThemMoiBN.Click += new System.EventHandler(cmdThemMoiBN_Click);
            cmdSave.Click += new System.EventHandler(cmdSave_Click);

           
            chkTudongthemmoi.CheckedChanged += new EventHandler(chkTudongthemmoi_CheckedChanged);
            
            cboDoituongKCB.SelectedIndexChanged += new EventHandler(cboDoituongKCB_SelectedIndexChanged);
           
            cmdThemmoiDiachinh.Click += cmdThemmoiDiachinh_Click;
           
            txtLoaikham._OnShowData += txtLoaikham__OnShowData;
            txtMaLankham.LostFocus += txtMaLankham_LostFocus;
            txtTenKH._OnShowData += txtTenKH__OnShowData;
          
        }

        void txtTenKH__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtTenKH.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtTenKH.myCode;
                txtTenKH.Init();
                txtTenKH.SetCode(oldCode);
                txtTenKH.Focus();
            } 
        }

        void txtMaLankham_LostFocus(object sender, EventArgs e)
        {
            if (Utility.DoTrim(txtMaLankham.Text).Length >= 8 && Utility.DoTrim(txtMaLankham.Text) != m_strMaluotkham)//Đã bị thay đổi do nhập tay
            {
                int reval = 0;
                StoredProcedure spitem = SPs.KcbKiemtraMalankhamNhaptay(globalVariables.UserName, 1, m_strMaluotkham, Utility.DoTrim(txtMaLankham.Text), reval);
                spitem.Execute();
                reval = Utility.Int32Dbnull(spitem.OutputValues[0], -1);
                if (reval != 0)
                {
                    Utility.ShowMsg(string.Format("Mã lượt khám bạn vừa nhập {0} không có trong danh mục hoặc đang được sử dụng bởi người dùng khác. Hãy nhấn OK để hệ thống tự động sinh mã lần khám mới nhất", Utility.DoTrim(txtMaLankham.Text)));
                    SinhMaLanKham();
                    txtMaLankham.SelectAll();
                    txtMaLankham.Focus();
                }
                else
                {
                    m_strMaluotkham = Utility.DoTrim(txtMaLankham.Text);
                }
            }
        }

        void txtLoaikham__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLoaikham.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLoaikham.myCode;
                txtLoaikham.Init();
                txtLoaikham.SetCode(oldCode);
                txtLoaikham.Focus();
            } 
        }

       
       
        void cmdThemmoiDiachinh_Click(object sender, EventArgs e)
        {
            frm_themmoi_diachinh_new _themmoi_diachinh = new frm_themmoi_diachinh_new();
            _themmoi_diachinh.ShowDialog();
            if (_themmoi_diachinh.m_blnHasChanged)
            {
               
                AddAutoCompleteDiaChi();
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

       
        bool AutoLoad = false;
       

        void chkTudongthemmoi_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._KCBProperties.Tudongthemmoi = chkTudongthemmoi.Checked;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
        }

       

        void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
            CauHinhKCB();
        }

      
       
        private void txtMaLankham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaLankham.Text.Trim() != "")
            {
              
                isAutoFinding = true;
                string patient_ID = Utility.GetYY(globalVariables.SysDate) +
                                    Utility.FormatNumberToString(Utility.Int32Dbnull(txtMaLankham.Text, 0), "000000");
                txtMaLankham.Text = patient_ID;
                ResetLuotkham();
                FindPatientIDbyMaLanKham(txtMaLankham.Text.Trim());
                isAutoFinding = false;
            }
        }

       

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMaBN.Text.Trim() != "")
            {
               
                isAutoFinding = true;
                FindPatient(txtMaBN.Text.Trim());
                isAutoFinding = false;
            }
        }

        private bool NotPayment(string patient_ID, ref string NgayKhamGanNhat)
        {
            try
            {
                DataTable temdt = _KCB_DANGKY.KcbLaythongtinBenhnhan(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)));
                if (temdt != null && Utility.ByteDbnull(temdt.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) > 0 && Utility.ByteDbnull(temdt.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) < 4)
                {
                    Utility.ShowMsg("Khách hàng đang ở trạng thái nội trú và chưa ra viện nên không thể thêm lần đăng ký mới. Đề nghị bạn xem lại");
                    return true;
                }
               
                if (temdt != null && temdt.Rows.Count <= 0)
                {
                    NgayKhamGanNhat = "NOREG";
                    //Chưa đăngký khám lần nào(mới gõ thông tin BN)-->Trạng thái sửa
                    return true; //Chưa thanh toán-->Cho về trạng thái sửa
                }
                if (temdt != null && temdt.Rows.Count > 0 && temdt.Select("trangthai_thanhtoan=0").Length > 0)
                {
                    NgayKhamGanNhat = temdt.Select("trangthai_thanhtoan=0", "ma_luotkham")[0]["Ngay_Kham"].ToString();
                    return true; //Chưa thanh toán-->Có thể cho về trạng thái sửa
                }
                else //Đã thanh toán--.Thêm lần đăng ký mới
                    return false;
            }
            catch (Exception ex)
            {
                return false; //Đã thanh toán--.Thêm lần đăng ký mới
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
                ResetLuotkham();

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
                    string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))//Đã thanh toán-->Kiểm tra ngày hẹn xem có được phép khám tiếp
                    {

                        KcbChandoanKetluan _Info = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.MaLuotkhamColumn).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbChandoanKetluan>();
                        if (_Info != null && _Info.SongayDieutri != null)
                        {
                            int SoNgayDieuTri = 0;
                            if (_Info.SongayDieutri.Value.ToString() == "")
                            {
                                SoNgayDieuTri = 0;
                            }
                            else
                            {
                                SoNgayDieuTri = _Info.SongayDieutri.Value;
                            }
                            DateTime ngaykhamcu = _Info.NgayTao; ;
                            DateTime ngaykhammoi = globalVariables.SysDate;
                            TimeSpan songay = ngaykhammoi - ngaykhamcu;

                            int kt = songay.Days;
                            int kt1 = SoNgayDieuTri - kt;
                            kt1 = Utility.Int32Dbnull(kt1);
                            // nếu khoảng cách từ lần đăng ký trước đến ngày hiện tại lớn hơn ngày điều trị.
                            if (kt >= SoNgayDieuTri)
                            {
                                m_enAction = action.Add;
                                SinhMaLanKham();
                                chkPhongchuyenmon.Focus();
                            }
                            else if (kt < SoNgayDieuTri)
                            {
                                DialogResult dialogResult =
                                    MessageBox.Show(
                                        "Bác Sỹ hẹn :  " + SoNgayDieuTri + "ngày" + "\nNgày khám gần nhất:  " +
                                        ngaykhamcu + "\nCòn: " + kt1 + " ngày nữa mới được tái khám" +
                                        "\nBạn có muốn tiếp tục thêm lần đăng ký ", "Thông Báo", MessageBoxButtons.YesNo);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    m_enAction = action.Add;
                                    SinhMaLanKham();
                                    chkPhongchuyenmon.Focus();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    ClearControl();
                                    return;
                                }
                            }
                        }
                        else//Chưa thăm khám-->Để nguyên trạng thái cập nhật
                        {
                        }
                    }
                    else//Còn lần đăng ký chưa thanh toán-->Kiểm tra
                    {
                        //nếu là ngày hiện tại thì đặt về trạng thái sửa
                        if (ngay_kham == "NOREG" || ngay_kham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
                        {
                            //LoadThongtinBenhnhan();
                            if (ngay_kham == "NOREG")//Bn chưa đăng ký phòng khám nào cả. 
                            {
                                //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
                                if (globalVariables.SysDate.ToString("dd/MM/yyyy") == dtpInputDate.Value.ToString("dd/MM/yyyy"))
                                {
                                    m_enAction = action.Update;

                                    Utility.ShowMsg(
                                       "Khách hàng vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                    //LaydanhsachdangkyKCB();
                                    txtTenKH.Select();
                                }
                                else//Thêm lần đăng ký cho ngày mới
                                {
                                    m_enAction = action.Add;
                                    SinhMaLanKham();
                                }
                            }
                            else//Quay về trạng thái sửa
                            {
                                m_enAction = action.Update;

                                Utility.ShowMsg(
                                   "Khách hàng vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                //LaydanhsachdangkyKCB();
                                txtTenKH.Select();
                            }
                        }
                        else //Không cho phép thêm lần đăng ký khác nếu chưa thanh toán lần đăng ký của ngày hôm trước
                        {
                            Utility.ShowMsg(
                                "Khách hàng đang có lần đăng ký chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Khách hàng trước khi thêm lần đăng ký mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Khách hàng");
                            cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                        }
                    }
                    ModifyCommand();
                }
                else
                {
                    Utility.ShowMsg(
                        "Khách hàng này chưa có lần đăng ký nào-->Có thể bị lỗi dữ liệu. Đề nghị liên hệ với VNS để được giải đáp");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("AutoFindLastExam().Exception-->" + ex.Message);
            }
            finally
            {
                SetActionStatus();
                AllowTextChanged = true;
            }
        }

    
        
        // private  b_QMSStop=false;
        /// <summary>
        /// hàm thực hiện việc lấy thông tin của phần dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Dangky_Kiemnghiem_Load(object sender, EventArgs e)
        {
            try
            {
                AllowTextChanged = false;
                b_HasLoaded = false;
                Utility.SetColor(lblDiachiBN, THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "1", false) == "1" ? lblHoten.ForeColor : lblMaKH.ForeColor);
                AddAutoCompleteDiaChi();
                Get_DanhmucChung();
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(), DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "", false);
                objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                ChangeObjectRegion();
                if (m_enAction == action.Insert)//Thêm mới BN
                {
                    objLuotkham = null;
                    SinhMaLanKham();
                    txtTenKH.Select();
                }
                else if (m_enAction == action.Update)//Cập nhật thông tin Khách hàng
                {
                    LoadThongtinBenhnhan();
                    txtTenKH.Select();
                }
                else if (m_enAction == action.Add) //Thêm mới lần đăng ký
                {
                    objLuotkham = null;
                    string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
                    if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))//Nếu đã thanh toán xong hết thì thêm lần đăng ký mới
                    {
                        SinhMaLanKham();
                        LoadThongtinBenhnhan();
                        chkPhongchuyenmon.Focus();
                    }
                    else//Còn lần đăng ký chưa thanh toán-->Kiểm tra
                    {
                        //nếu là ngày hiện tại thì đặt về trạng thái sửa
                        if (ngay_kham == "NOREG" || ngay_kham==globalVariables.SysDate.ToString("dd/MM/yyyy"))
                        {
                            LoadThongtinBenhnhan();
                            if (ngay_kham == "NOREG")//Bn chưa đăng ký phòng khám nào cả. 
                            {
                                //Nếu ngày hệ thống=Ngày đăng ký gần nhất-->Sửa
                                if (globalVariables.SysDate.ToString("dd/MM/yyyy") == dtpInputDate.Value.ToString("dd/MM/yyyy"))
                                {
                                    m_enAction = action.Update;

                                    Utility.ShowMsg(
                                       "Khách hàng vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                    txtTenKH.Select();
                                }
                                else//Thêm lần đăng ký cho ngày mới
                                {
                                    m_enAction = action.Add;
                                    SinhMaLanKham();
                                    chkPhongchuyenmon.Focus();
                                }
                            }
                            else//Quay về trạng thái sửa
                            {
                                m_enAction = action.Update;

                                Utility.ShowMsg(
                                   "Khách hàng vừa được đăng ký ngày hôm nay nên hệ thống sẽ chuyển về chế độ Sửa thông tin. Nhấn OK để bắt đầu sửa");
                                txtTenKH.Select();
                            }
                        }
                        else //Không cho phép thêm lần đăng ký khác nếu chưa thanh toán lần đăng ký của ngày hôm trước
                        {
                            Utility.ShowMsg(
                                "Khách hàng đang có lần đăng ký chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Khách hàng trước khi thêm lần đăng ký mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Khách hàng");
                            cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                        }
                    }
                }
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
                SetActionStatus(); 
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
            KcbDanhsachBenhnhan objBenhnhan = KcbDanhsachBenhnhan.FetchByID(txtMaBN.Text);
            if (objBenhnhan != null)
            {
                txtTenKH._Text = Utility.sDbnull(objBenhnhan.TenBenhnhan);
                txtSoDT.Text = Utility.sDbnull(objBenhnhan.DienThoai);
                txtDiachi._Text = Utility.sDbnull(objBenhnhan.DiaChi);
                
                txtEmail.Text = Utility.sDbnull(objBenhnhan.Email);
                txtFax.Text = objBenhnhan.Fax;
                txtNguoiLienhe.Text = objBenhnhan.NguoiLienhe;
                txtSoDT.Text = objBenhnhan.DienThoai;
                
                objLuotkham = new Select().From(KcbLuotkham.Schema)
                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaLankham.Text)
                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text, -1)).ExecuteSingle
                   <KcbLuotkham>();
                if (objLuotkham != null)
                {
                    m_strMaluotkham = objLuotkham.MaLuotkham;
                    txtLoaikham.SetCode(objLuotkham.KieuKham);
                    txtSolankham.Text = Utility.sDbnull(objLuotkham.SolanKham);
                    _IdDoituongKcb = objLuotkham.IdDoituongKcb;
                    dtpInputDate.Value = objLuotkham.NgayTiepdon;
                    dtCreateDate.Value = objLuotkham.NgayTiepdon;
                    chkPhongchuyenmon.Checked = Utility.Byte2Bool(objLuotkham.TraKQPhongchuyenmon);
                    chkFax.Checked = Utility.Byte2Bool(objLuotkham.TraKQFax);
                    chkMail.Checked = Utility.Byte2Bool(objLuotkham.TraKQMail);
                    chkEmail.Checked = Utility.Byte2Bool(objLuotkham.TraKQEmail);
                    chkSosanh.Checked = Utility.Byte2Bool(objLuotkham.SosanhQcvn);
                    txtMotathem.Text = objLuotkham.MotaThem;
                    txtEmail.Text = objLuotkham.Email;
                    _MaDoituongKcb = Utility.sDbnull(objLuotkham.MaDoituongKcb);
                    objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();

                    ChangeObjectRegion();
                    PtramBhytCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                    PtramBhytGocCu = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0);
                    _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
                    _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
                    cboDoituongKCB.SelectedIndex = Utility.GetSelectedIndex(cboDoituongKCB, _MaDoituongKcb);
                   
                }
                else
                {
                }
            }
            
        }

      
        private void Get_DanhmucChung()
        {
           
            AutoCompleteDmucChung();
            
        }
       
      

        private void AddAutoCompleteDiaChi()
        {
          
            txtDiachi.dtData = globalVariables.dtAutocompleteAddress.Copy();
            this.txtDiachi.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
            this.txtDiachi.CaseSensitive = false;
            this.txtDiachi.MinTypedCharacters = 1;

            
         
        }
       

        private void AutoCompleteDmucChung()
        {
            txtLoaikham.Init(this.Args.Split('-')[0]);
           
        }

       
        private void CreateTable()
        {
            if (m_DC == null || m_DC.Columns.Count <= 0)
            {
                m_DC = new DataTable();
                m_DC.Columns.AddRange(new[]
                                          {
                                              new DataColumn("ShortCutXP", typeof (string)),
                                              new DataColumn("ShortCutQH", typeof (string)),
                                              new DataColumn("ShortCutTP", typeof (string)),
                                              new DataColumn("Value", typeof (string)),
                                              new DataColumn("ComparedValue", typeof (string))
                                          });
            }
        }
       
        

        private void SinhMaLanKham()
        {
            txtSolankham.Text = string.Empty;
            if (m_enAction == action.Insert)
            {
                txtMaBN.Text = "Tự sinh";
            }
            txtMaLankham.Text = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM(1);
            m_strMaluotkham = txtMaLankham.Text;
            //Tạm bỏ
            SqlQuery sqlQuery = new Select(Aggregate.Max(KcbLuotkham.Columns.SolanKham)).From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text, -1));
            var SoThuTuKham = sqlQuery.ExecuteScalar<Int32>();
            txtSolankham.Text = Utility.sDbnull(SoThuTuKham + 1);
        }
        


        /// <summary>
        /// hàm thực hiện việc làm sách thông tin của Khách hàng
        /// </summary>
        private void ClearControl()
        {
            setMsg(uiStatusBar1.Panels["MSG"], "", false);
            m_blnHasJustInsert = false;
            txtSolankham.Text = "1";
            txtTenKH.ResetText();

            txtDiachi.Clear();

            txtSoDT.Clear();
            txtNguoiLienhe.Clear();


            txtEmail.Clear();


            ModifyCommand();

            AllowTextChanged = false;


            _MaDoituongKcb = Utility.sDbnull(cboDoituongKCB.SelectedValue);
            objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
            if (objDoituongKCB == null) return;
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _IdLoaidoituongKcb = objDoituongKCB.IdLoaidoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;


            AllowTextChanged = true;
            //Chuyển về trạng thái thêm mới
            m_enAction = action.Insert;

            SinhMaLanKham();


            PtramBhytCu = 0;
            PtramBhytGocCu = 0;
            if (m_enAction == action.Insert)
            {
                dtpInputDate.Value = globalVariables.SysDate;
                dtCreateDate.Value = globalVariables.SysDate;

            }
            SetActionStatus();

        }

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            //Cập nhật lại mã lượt khám chưa dùng tới trong trường hợp nhấn New liên tục
            ResetLuotkham();

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
                if (isExceedData())
                {
                    Utility.ShowMsg("Phiên bản Demo chỉ cho phép bạn tiếp đón tối đa 5000 lượt khám. Mời bạn liên hệ để được trợ giúp");
                    return;
                }
                cmdSave.Enabled = false;
                if(m_enAction==action.Update)
                if (!IsValidData()) return;
                PerformAction();
                cmdSave.Enabled = true;
            }
            catch
            {
            }
            finally
            {
              
                DiachiBNCu = false;
                DiachiBHYTCu = false;
                cmdSave.Enabled = true;
            }
        }
        bool isExceedData()
        {
            try
            {
                if (PropertyLib._ConfigProperties.HIS_AppMode != VNS.Libs.AppType.AppEnum.AppMode.License)
                {
                    KcbLuotkhamCollection lst = new Select().From(KcbLuotkham.Schema).ExecuteAsCollection<KcbLuotkhamCollection>();
                    return lst.Count >= 1500;
                }
                return false;
            }
            catch(Exception ex)
            {
                Utility.CatchException("isExceedData()-->",ex);
                return true;
            }
        }
       

        private bool IsValidData()
        {
            Utility.SetMsg(uiStatusBar1.Panels["MSG"], "", false);
            if (m_enAction==action.Insert && dtCreateDate.Value.ToString("dd/MM/yyyy") != globalVariables.SysDate.ToString("dd/MM/yyyy"))
            {
                if (!Utility.AcceptQuestion("Ngày tiếp đón khác ngày hiện tại. Bạn có chắc chắn hay không?","Cảnh báo",true))
                {
                    dtCreateDate.Focus();
                    return false;
                }
            }
            if (txtLoaikham.myCode == "-1")
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải chọn loại khám", true);
                txtLoaikham.Focus();
                txtLoaikham.SelectAll();
                return false;
            }


            if (string.IsNullOrEmpty(txtTenKH.Text))
            {
                Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập tên Khách hàng", true);
                txtTenKH.Focus();
                return false;
            }
            

            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BENHNHAN", "0", false) == "1")
                {
                    if (Utility.DoTrim(txtDiachi.Text) == "")
                    {
                        Utility.SetMsg(uiStatusBar1.Panels["MSG"], "Bạn phải nhập địa chỉ Khách hàng", true);
                        txtDiachi.Focus();
                        return false;
                    }
                }



            return true;
        }

       

        private void  ModifyCommand()
        {

            cmdSave.Enabled = Utility.DoTrim(txtTenKH.Text).Length > 0;
           
           
            
        }

      
        

        private void PerformAction()
        {
            switch (m_enAction)
            {
                case action.Update:
                    if (!InValiExistsBN()) return;
                    UpdatePatient();
                    break;
                case action.Insert:
                    InsertPatient();
                    break;
                case action.Add:
                    ThemLanKham();
                    break;
            }
           
            ModifyCommand();
            
        }
        private bool InValiExistsBN()
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaLankham.Text))
                {
                    Utility.ShowMsg("Mã đăng ký không bỏ trống", "Thông báo", MessageBoxIcon.Error);
                    txtMaLankham.Focus();
                    txtMaLankham.SelectAll();
                    return false;
                }
                SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtMaLankham.Text));
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg("Mã đăng ký này không tồn tại trong CSDL,Mời bạn xem lại", "Thông báo",
                                    MessageBoxIcon.Error);
                    txtMaLankham.Focus();
                    txtMaLankham.SelectAll();
                    return false;
                }
                //Kiểm tra xem có thay đổi phần trăm BHYT
                if (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) != Utility.DecimaltoDbnull(0))
                {
                    KcbThanhtoanCollection _lstthanhtoan = new Select().From(KcbThanhtoan.Schema)
                        .Where(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbThanhtoan.Columns.PtramBhyt).IsEqualTo(Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0))
                        .ExecuteAsCollection<KcbThanhtoanCollection>();
                    if (_lstthanhtoan.Count > 0)
                    {
                        Utility.ShowMsg(string.Format("Khách hàng này đã thanh toán với mức BHYT {0}. Do đó hệ thống không cho phép bạn thay đổi phần trăm BHYT.\nMuốn thay đổi đề nghị bạn hủy hết các thanh toán",Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0).ToString()));
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi kiểm tra hợp lệ dữ liệu trước khi cập nhật Khách hàng",ex);
                return false;
            }
        }

        void ChangeObjectRegion()
        {
            if (objDoituongKCB == null) return;
            _IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
            _IdLoaidoituongKcb = objDoituongKCB.IdLoaidoituongKcb;
            _TenDoituongKcb = objDoituongKCB.TenDoituongKcb;
            PtramBhytCu = objDoituongKCB.PhantramTraituyen.Value;
            PtramBhytGocCu = PtramBhytCu;
          
            if (objDoituongKCB.IdLoaidoituongKcb == 0)//ĐỐi tượng BHYT
            {
               
            }
            else//Đối tượng khác BHYT
            {

                txtTenKH.Focus();
            }
        }
        DmucDoituongkcb objDoituongKCB = null;
        /// <summary>
        /// hàm thực hienej phím tắt của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Dangky_Kiemnghiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (this.ActiveControl != null && this.ActiveControl.Name == txtTenKH.Name && Utility.DoTrim(txtTenKH.Text) != "")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM();
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", Utility.DoTrim(txtTenKH.Text), "", "", "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        
                        isAutoFinding = true;
                        FindPatient(Timkiem_Benhnhan.IdBenhnhan.ToString());
                        isAutoFinding = false;
                    }
                }
                else if (this.ActiveControl != null && this.ActiveControl.Name == txtSoDT.Name && Utility.DoTrim(txtSoDT.Text) != "")
                {
                    frm_DSACH_BN_TKIEM Timkiem_Benhnhan = new frm_DSACH_BN_TKIEM();
                    Timkiem_Benhnhan.AutoSearch = true;
                    Timkiem_Benhnhan.FillAndSearchData(false, "", "", "", "", Utility.DoTrim(txtSoDT.Text), "-1");
                    if (Timkiem_Benhnhan.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
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
            
            string ngay_kham = globalVariables.SysDate.ToString("dd/MM/yyyy");
            if (e.Control && e.KeyCode == Keys.K)
            {
                if (!NotPayment(txtMaBN.Text.Trim(), ref ngay_kham))
                {
                    m_enAction = action.Add;
                    SinhMaLanKham();
                    chkPhongchuyenmon.Focus();
                }
                else
                {
                    //nếu là ngày hiện tại thì đặt về trạng thái sửa
                    if (ngay_kham == globalVariables.SysDate.ToString("dd/MM/yyyy"))
                    {
                        Utility.ShowMsg(
                            "Khách hàng đang có lần đăng ký chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Khách hàng trước khi thêm lần đăng ký mới.Nhấn OK để hệ thống quay về trạng thái sửa thông tin BN");
                        m_enAction = action.Update;
                        AllowTextChanged = false;
                        LoadThongtinBenhnhan();
                        txtTenKH.Focus();
                    }
                    else //Không cho phép thêm lần đăng ký khác nếu chưa thanh toán lần đăng ký của ngày hôm trước
                    {
                        Utility.ShowMsg(
                            "Khách hàng đang có lần đăng ký chưa được thanh toán. Cần thanh toán hết các lần đến khám bệnh của Khách hàng trước khi thêm lần đăng ký mới. Nhấn OK để hệ thống chuyển về trạng thái thêm mới Khách hàng");
                        cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    }
                }
                return;
            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                txtMaBN.SelectAll();
                txtMaBN.Focus();
            }
            
            if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
            if (e.KeyCode == Keys.Escape && this.ActiveControl != null && this.ActiveControl.GetType()!=txtDiachi.GetType())
            {

                Close();
            }
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.N && e.Control) cmdThemMoiBN.PerformClick();
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }


        private void txtTEN_BN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cmdSave.Enabled = Utility.DoTrim(txtTenKH.Text).Length > 0;
            }
            catch (Exception exception)
            {
            }
        }
        void ResetLuotkham()
        {
            new Update(KcbDmucLuotkham.Schema)
                      .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(0)
                      .Set(KcbDmucLuotkham.Columns.UsedBy).EqualTo(DBNull.Value)
                      .Set(KcbDmucLuotkham.Columns.StartTime).EqualTo(DBNull.Value)
                      .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(null)
                      .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(m_strMaluotkham)
                      .And(KcbDmucLuotkham.Columns.TrangThai).IsEqualTo(1)
                      .And(KcbDmucLuotkham.Columns.UsedBy).IsEqualTo(globalVariables.UserName)
                      .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year).Execute();
        }
        private void frm_Dangky_Kiemnghiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Utility.FreeLockObject(m_strMaluotkham);
                //Trả lại mã lượt khám nếu chưa được dùng đến
                ResetLuotkham();
            }
            catch (Exception exception)
            {
            }
        }

       
        void SetActionStatus()
        {
            lblStatus.Text = m_enAction == action.Insert ? "Khách hàng MỚI" : (m_enAction==action.Add?"THÊM lần đăng ký":"CẬP NHẬT");
        }
        private void CauHinhKCB()
        {
            if (PropertyLib._KCBProperties != null)
            {
                chkTudongthemmoi.Checked = PropertyLib._KCBProperties.Tudongthemmoi;

            }
        }
       

        #region "Su kien autocomplete của thành phố"

        private bool AllowTextChanged;
        private string _rowFilter = "1=1";


        #endregion

        #region "Su kien autocomplete của quận huyện"

        private string _rowFilterQuanHuyen = "1=1";

       


        #region Diachi

        private bool DiachiBHYTCu;
        private bool DiachiBNCu;

        private void setMsg(UIStatusBarPanel item, string msg, bool isError)
        {
            try
            {
                item.Text = msg;
                if (isError) item.FormatStyle.ForeColor = Color.Red;
                else item.FormatStyle.ForeColor = Color.DarkBlue;

                Application.DoEvents();
            }
            catch
            {
            }
        }

       

        #endregion

       
        #endregion

       

        #region "khởi tạo sự kiện để lưu lại thông tin của Khách hàng"

        private string mavuasinh = "";

        private void ThemMoiLanKhamVaoLuoi()
        {
            DataRow dr = m_dtPatient.NewRow();
            dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan] = Utility.sDbnull(txtMaBN.Text, "-1");
            dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = Utility.sDbnull(txtTenKH.Text, "");


            
            dr[KcbDanhsachBenhnhan.Columns.Email] = Utility.sDbnull(txtEmail.Text, "");
            dr[KcbDanhsachBenhnhan.Columns.DiaChi] = Utility.sDbnull(txtDiachi.Text, "");


            dr[KcbDanhsachBenhnhan.Columns.NguoiTao] = globalVariables.UserName;

           
            dr[KcbDanhsachBenhnhan.Columns.DienThoai] = Utility.sDbnull(txtSoDT.Text, "");
           
            dr[KcbLuotkham.Columns.MaLuotkham] = Utility.sDbnull(txtMaLankham.Text, "");
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_IdDoituongKcb);
            if (objectType != null)
                dr[KcbLuotkham.Columns.MaDoituongKcb] = Utility.sDbnull(objectType.MaDoituongKcb, "");
            dr[KcbLuotkham.Columns.IdDoituongKcb] = _IdDoituongKcb;
            dr[DmucDoituongkcb.Columns.TenDoituongKcb] = _TenDoituongKcb;

            dr[KcbLuotkham.Columns.TrangthaiNoitru] = 0;
            dr[KcbLuotkham.Columns.TrangthaiNgoaitru] = 0;

            dr[KcbLuotkham.Columns.Locked] = 0;

            dr[KcbLuotkham.Columns.NgayTiepdon] = dtCreateDate.Value;
            dr["sNgay_tiepdon"] = dtCreateDate.Value; 
            dr["Loai_benhnhan"] = "Ngoại trú";
            m_dtPatient.Rows.InsertAt(dr, 0);
        }

        private void UpdateBNVaoTrenLuoi()
        {
            EnumerableRowCollection<DataRow> query = from bn in m_dtPatient.AsEnumerable()
                                                     where
                                                         Utility.sDbnull(bn[KcbLuotkham.Columns.MaLuotkham]) ==
                                                         txtMaLankham.Text
                                                     select bn;
            if (query.Count() > 0)
            {
                DataRow dr = query.FirstOrDefault();
                dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan] = Utility.sDbnull(txtMaBN.Text, "-1");
                dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan] = Utility.sDbnull(txtTenKH.Text, "");

               
                dr[KcbDanhsachBenhnhan.Columns.Email] = Utility.sDbnull(txtEmail.Text, "");
                dr[KcbDanhsachBenhnhan.Columns.DiaChi] = Utility.sDbnull(txtDiachi.Text, "");

                dr[KcbDanhsachBenhnhan.Columns.NguoiTao] = globalVariables.UserName;
                
                dr[KcbDanhsachBenhnhan.Columns.DienThoai] = Utility.sDbnull(txtSoDT.Text, "");
                
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_IdDoituongKcb);
                if (objectType != null)
                {
                    dr[KcbLuotkham.Columns.MaDoituongKcb] = Utility.sDbnull(objectType.MaDoituongKcb, "");
                }

                dr[KcbLuotkham.Columns.MaLuotkham] = Utility.sDbnull(txtMaLankham.Text, "");
                dr[KcbLuotkham.Columns.IdDoituongKcb] = _IdDoituongKcb;
                dr[DmucDoituongkcb.Columns.TenDoituongKcb] = _TenDoituongKcb;

                dr[KcbLuotkham.Columns.TrangthaiNoitru] = 0;
                dr[KcbLuotkham.Columns.TrangthaiNgoaitru] = 0;

                dr[KcbLuotkham.Columns.Locked] = 0;

                dr[KcbLuotkham.Columns.NgayTiepdon] = dtCreateDate.Value;
                dr["sNgay_tiepdon"] = dtCreateDate.Value; //globalVariables.SysDate;
               
                m_dtPatient.AcceptChanges();
            }
        }

        private void ThemLanKham()
        {
            KcbDanhsachBenhnhan objBenhnhan = TaoBenhnhan();
            objLuotkham = TaoLuotkham();
           
            long v_id_kham = -1;
            string msg = "";
            errorProvider1.Clear();
            ActionResult actionResult = _KCB_DANGKY.ThemLuotDangkyKiemnghiem(objBenhnhan, objLuotkham, ref msg);

            
            switch (actionResult)
            {
                case ActionResult.Success:
                   
                    PtramBhytCu = 0;
                    PtramBhytGocCu = 0;
                    txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    txtMaBN.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                   
                    m_blnHasJustInsert = true;
                    m_enAction = action.Update;
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thêm mới lần đăng ký Khách hàng thành công", false);
                    ThemMoiLanKhamVaoLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    if (_OnAssign != null) _OnAssign();
                    cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    m_blnCancel = false;
                    break;
                case ActionResult.Error:
                    setMsg(uiStatusBar1.Panels["MSG"], "Lỗi trong quá trình thêm lần đăng ký !", true);
                    cmdSave.Focus();
                    break;
            }
        }


        private void InsertPatient()
        {
            KcbDanhsachBenhnhan objBenhnhan = TaoBenhnhan();
            objLuotkham = TaoLuotkham();
            long v_id_kham = -1;
            string msg = "";
            errorProvider1.Clear();
            ActionResult actionResult = _KCB_DANGKY.ThemmoiKhachhangDangkyKiemnghiem( objBenhnhan, objLuotkham, ref msg);

            
            switch (actionResult)
            {
                case ActionResult.Success:
                    
                    PtramBhytCu = 0;
                    PtramBhytGocCu = 0;
                    txtMaLankham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    txtMaBN.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                    mavuasinh = Utility.sDbnull(objLuotkham.IdBenhnhan);
                    m_enAction = action.Update;
                    m_blnHasJustInsert = true;
                    m_strMaluotkham = txtMaLankham.Text;
                    ThemMoiLanKhamVaoLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thêm mới Khách hàng thành công", false);
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    m_blnCancel = false;

                    if (_OnAssign!=null) _OnAssign();
                    if (PropertyLib._KCBProperties.Tudongthemmoi) cmdThemMoiBN_Click(cmdThemMoiBN, new EventArgs());
                    txtMaBN.Text = Utility.sDbnull(mavuasinh);
                    
                    break;
                case ActionResult.Error:
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thực hiện thêm dữ liệu không thành công !", true);
                    cmdSave.Focus();
                    break;
            }
        }
       
      

        private void UpdatePatient()
        {
            KcbDanhsachBenhnhan objBenhnhan = TaoBenhnhan();
            objLuotkham = TaoLuotkham();
          
            string msg = "";
            errorProvider1.Clear();
            ActionResult actionResult = _KCB_DANGKY.CapnhatDangkymauKiemnghiem( objBenhnhan, objLuotkham, ref msg);
            
            switch (actionResult)
            {
                case ActionResult.Success:
                   
                    //gọi lại nếu thay đổi địa chỉ
                    m_blnHasJustInsert = false;
                    PtramBhytCu = 0;
                    PtramBhytGocCu = 0;
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn sửa thông tin Khách hàng thành công", false);
                    
                    UpdateBNVaoTrenLuoi();
                    if (_OnActionSuccess != null) _OnActionSuccess();
                    
                   
                    Utility.GotoNewRowJanus(grdList, KcbLuotkham.Columns.MaLuotkham, txtMaLankham.Text);
                    m_blnCancel = false;

                    break;
                case ActionResult.Error:
                    setMsg(uiStatusBar1.Panels["MSG"], "Bạn thực hiện sửa thông tin không thành công !", true);
                    break;
                case ActionResult.Cancel:
                    break;
            }
        }


        /// <summary>
        /// Insert dữ liệu khi thêm mới hoàn toàn
        /// </summary>hàm chen du lieu moi tin day, benhnhan kham benh moi tinh
        private KcbDanhsachBenhnhan TaoBenhnhan()
        {
            
            var objBenhnhan = new KcbDanhsachBenhnhan();
            if (m_enAction == action.Add || m_enAction == action.Update)
            {
                objBenhnhan = KcbDanhsachBenhnhan.FetchByID(Utility.Int64Dbnull(txtMaBN.Text, -1));
                if (objBenhnhan == null) return null;
                objBenhnhan.IsNew = false;
                objBenhnhan.MarkOld();
            }
            objBenhnhan.TenBenhnhan = txtTenKH.Text;
            objBenhnhan.DiaChi = txtDiachi.Text;
            objBenhnhan.DienthoaiLienhe = txtSoDT.Text;
            objBenhnhan.DiachiBhyt = "";
            objBenhnhan.DienThoai = txtSoDT.Text;
            objBenhnhan.Email = Utility.sDbnull(txtEmail.Text, "");
            objBenhnhan.NguoiLienhe = Utility.sDbnull(txtNguoiLienhe.Text);
            objBenhnhan.Fax = Utility.sDbnull(txtFax.Text);
            objBenhnhan.NgayTao = globalVariables.SysDate;
            objBenhnhan.NguoiTao = globalVariables.UserName;
            objBenhnhan.NguonGoc = "KCB";
            objBenhnhan.Cmt = "";
            objBenhnhan.CoQuan = string.Empty;
            objBenhnhan.NgheNghiep = "";
            objBenhnhan.GioiTinh ="NAM";
            objBenhnhan.IdGioitinh = 0;
            objBenhnhan.NamSinh = Utility.Int16Dbnull(DateTime.Now.Year, 2000);
            objBenhnhan.KieuBenhnhan = 1;//0= Khách hàng thường đến khám chữa bệnh;1= Người gửi mẫu kiểm nghiệm cá nhân;2= Tổ chức gửi mẫu kiểm nghiệm
           
                objBenhnhan.NgaySinh = globalVariables.SysDate;
           

            if (m_enAction == action.Insert)
            {
                objBenhnhan.NgayTiepdon = dtCreateDate.Value;
                objBenhnhan.NguoiTao = globalVariables.UserName;
                objBenhnhan.IpMaytao = globalVariables.gv_strIPAddress;
                objBenhnhan.TenMaytao = globalVariables.gv_strComputerName;
            }
            if (m_enAction == action.Update)
            {
                objBenhnhan.NgaySua = globalVariables.SysDate;
                objBenhnhan.NguoiSua = globalVariables.UserName;
                objBenhnhan.NgayTiepdon = dtCreateDate.Value;

                objBenhnhan.IpMaysua = globalVariables.gv_strIPAddress;
                objBenhnhan.TenMaysua = globalVariables.gv_strComputerName;
            }
            return objBenhnhan;
        }

        /// <summary>
        /// hàm thực hiện việc khwoir tạo thoog tin PatietnExam
        /// </summary>
        /// <returns></returns>
        private KcbLuotkham TaoLuotkham()
        {
            objLuotkham = new KcbLuotkham();
            if (m_enAction == action.Insert || m_enAction == action.Add)
            {
                //Bỏ đi do đã sinh theo cơ chế bảng danh mục mã lượt khám. Nếu ko sẽ mất mã lượt khám hiện thời.
                // txtMaLankham.Text = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM();
                objLuotkham.IsNew = true;
            }
            else
            {
                objLuotkham = KcbLuotkham.FetchByID(m_strMaluotkham);
                if (objLuotkham == null) return null;
                objLuotkham.MarkOld();
                objLuotkham.IsNew = false;
            }
            objLuotkham.KieuKham = txtLoaikham.myCode;
            objLuotkham.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
            objLuotkham.Noitru = 0;
            objLuotkham.IdDoituongKcb = _IdDoituongKcb;
            objLuotkham.IdLoaidoituongKcb = _IdLoaidoituongKcb;
            objLuotkham.Locked = 0;
            objLuotkham.HienthiBaocao = 1;
            objLuotkham.TrangthaiCapcuu = 0;
            objLuotkham.IdKhoatiepnhan = globalVariables.idKhoatheoMay;
            objLuotkham.NguoiTao = globalVariables.UserName;
            objLuotkham.NgayTao = globalVariables.SysDate;
            objLuotkham.Cmt = "";
            objLuotkham.DiaChi = txtDiachi.Text;
            objLuotkham.CachTao = 0;
            objLuotkham.Email = txtEmail.Text;
            objLuotkham.NoiGioithieu = "";
            objLuotkham.TraKQPhongchuyenmon = Utility.Bool2byte(chkPhongchuyenmon.Checked);
            objLuotkham.TraKQFax = Utility.Bool2byte(chkFax.Checked);
            objLuotkham.TraKQMail = Utility.Bool2byte(chkMail.Checked);
            objLuotkham.TraKQEmail = Utility.Bool2byte(chkEmail.Checked);
            objLuotkham.SosanhQcvn = Utility.Bool2byte(chkSosanh.Checked);
            objLuotkham.MotaThem = Utility.DoTrim(txtMotathem.Text);
            objLuotkham.NhomBenhnhan = "-1";
            objLuotkham.IdBenhvienDen = -1;
            objLuotkham.TthaiChuyenden = 0;

            objLuotkham.GiayBhyt = 0;
            objLuotkham.MadtuongSinhsong = "";
            objLuotkham.MaKcbbd = "";
            objLuotkham.NoiDongtrusoKcbbd = "";
            objLuotkham.MaNoicapBhyt = "";
            objLuotkham.LuongCoban = globalVariables.LUONGCOBAN;
            objLuotkham.MatheBhyt = "";
            objLuotkham.MaDoituongBhyt = "";
            objLuotkham.MaQuyenloi = -1;
            objLuotkham.DungTuyen = 0;

            objLuotkham.NgayketthucBhyt = null;
            objLuotkham.NgaybatdauBhyt = null;
            objLuotkham.NoicapBhyt = "";
            objLuotkham.DiachiBhyt = "";
            objLuotkham.SolanKham = Utility.Int16Dbnull(txtSolankham.Text, 0);
            objLuotkham.TrieuChung = "";
            //Tránh lỗi khi update người dùng nhập Mã đăng ký lung tung
            if (m_enAction == action.Update) txtMaLankham.Text = m_strMaluotkham;
            objLuotkham.MaLuotkham = Utility.sDbnull(txtMaLankham.Text, "");
            objLuotkham.IdBenhnhan = Utility.Int64Dbnull(txtMaBN.Text, -1);
            DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(_IdDoituongKcb);
            if (objectType != null)
            {
                objLuotkham.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
            }
            if (m_enAction == action.Update)
            {
                objLuotkham.NgayTiepdon = dtCreateDate.Value;
                objLuotkham.NguoiSua = globalVariables.UserName;
                objLuotkham.NgaySua = globalVariables.SysDate;
                objLuotkham.IpMaysua = globalVariables.gv_strIPAddress;
                objLuotkham.TenMaysua = globalVariables.gv_strComputerName;
            }
            if (m_enAction == action.Add || m_enAction == action.Insert)
            {
                objLuotkham.NgayTiepdon = dtCreateDate.Value;
                objLuotkham.NguoiTiepdon = globalVariables.UserName;

                objLuotkham.IpMaytao = globalVariables.gv_strIPAddress;
                objLuotkham.TenMaytao = globalVariables.gv_strComputerName;
            }
            objLuotkham.PtramBhytGoc = 0;
            objLuotkham.PtramBhyt = 0;
            return objLuotkham;
        }

        #endregion

       
    }
}