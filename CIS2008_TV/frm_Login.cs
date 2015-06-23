using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SubSonic;

using VNS.Libs;
using VNS.Core.Classes;
using VNS.Libs.RegistryManager;
using VNS.Libs.AppUI;
using VNS.HIS.DAL;
using System.IO;
using VNS.Properties;
using xvhrk;

namespace CIS.CoreApp
{
    public partial class frm_Login : Form
    {
        private BackgroundWorker bw = new BackgroundWorker();
        bool oldVal = false;
        string oldUID = "";
        public frm_Login()
        {
            InitializeComponent();
            cmdSettings.Click+=cmdSettings_Click;
            txtUserName.LostFocus+=new EventHandler(txtUserName_LostFocus);
            if (globalVariables.sMenuStyle == "MENU")
                cbogiaodien.SelectedIndex = 0;
            else
                cbogiaodien.SelectedIndex = 1;
            
        }
        private void cmdSettings_Click(object sender, EventArgs e)
        {
            oldVal = PropertyLib._ConfigProperties.RunUnderWS;
            frm_Properties _Properties = new frm_Properties(PropertyLib._ConfigProperties);
            _Properties.TopMost = true;
            _Properties.ShowDialog();
            if (oldVal != PropertyLib._ConfigProperties.RunUnderWS)
            {
                if (PropertyLib._ConfigProperties.RunUnderWS)
                {
                    string DataBaseServer = "";
                    string DataBaseName = "";
                    string UID = "";
                    string PWD = "";
                    WS._AdminWS.GetConnectionString(ref DataBaseServer, ref DataBaseName, ref UID, ref PWD);
                    PropertyLib._ConfigProperties.DataBaseServer = DataBaseServer;
                    PropertyLib._ConfigProperties.DataBaseName = DataBaseName;
                    PropertyLib._ConfigProperties.UID = UID;
                    PropertyLib._ConfigProperties.PWD = PWD;
                    globalVariables.ServerName = PropertyLib._ConfigProperties.DataBaseServer;
                    globalVariables.sUName = PropertyLib._ConfigProperties.UID;
                    globalVariables.sPwd = PropertyLib._ConfigProperties.PWD;
                    globalVariables.sDbName = PropertyLib._ConfigProperties.DataBaseName;
                }
                else
                {
                    globalVariables.ServerName = PropertyLib._ConfigProperties.DataBaseServer;
                    globalVariables.sUName = PropertyLib._ConfigProperties.UID;
                    globalVariables.sPwd = PropertyLib._ConfigProperties.PWD;
                    globalVariables.sDbName = PropertyLib._ConfigProperties.DataBaseName;
                    globalVariables.sMenuStyle = "DOCKING";

                    globalVariables.MA_KHOA_THIEN = PropertyLib._ConfigProperties.MaKhoa;
                    globalVariables.MA_PHONG_THIEN = PropertyLib._ConfigProperties.Maphong;
                    globalVariables.SOMAYLE = PropertyLib._ConfigProperties.Somayle;
                    globalVariables.MIN_STT = PropertyLib._ConfigProperties.Min;
                    globalVariables.MAX_STT = PropertyLib._ConfigProperties.Max;
                }
                Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
            }
        }
       
        bool b_HasSuccess = false;
       public bool blnRelogin = false;
       
    
        /// <summary>
        /// hàm thực hiện việc đăng nhập thông tin của khi đăng nhập Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Login_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtKhoaphong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
                DataBinding.BindDataCombobox(cboKhoaKCB, dtKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "---Khoa làm việc---", false);
                PropertyLib._AppProperties = PropertyLib.GetAppPropertiess();
                cbogiaodien.SelectedIndex = PropertyLib._AppProperties.MenuStype;
                txtUserName.Text = PropertyLib._AppProperties.UID;
                oldUID = txtUserName.Text;
                chkRemember.Checked = PropertyLib._AppProperties.REM;
                cboKhoaKCB.SelectedIndex = Utility.GetSelectedIndex(cboKhoaKCB, PropertyLib._AppProperties.Makhoathien);
                if (PropertyLib._AppProperties.AutoLogin)
                {
                    txtPassWord.Text = PropertyLib._AppProperties.PWD;
                    cmdLogin_Click(cmdLogin, e);
                }
            }
            catch (Exception)
            {
                
                
            }
           finally
            {
            }
        }

       
        /// <summary>
        /// hàm thực hienj việc lưu lại thông itn 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void txtUserName_LostFocus(object  sender,EventArgs eventArgs)
        {
            try
            {
                if (oldUID != Utility.sDbnull(txtUserName.Text))
                {
                    oldUID = Utility.sDbnull(txtUserName.Text);
                    globalVariables.UserName = oldUID;
                   bool isAdmin= new LoginService().isAdmin(Utility.sDbnull(oldUID));
                    DataBinding.BindDataCombox(cboKhoaKCB,
                                               THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(isAdmin), (byte)2),
                                               DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                               "---Khoa làm việc---", false);
                    cboKhoaKCB.SelectedIndex = Utility.GetSelectedIndex(cboKhoaKCB, PropertyLib._AppProperties.Makhoathien);
                }
            }
            catch (Exception ex)
            {
                
            }
           
        }
        /// <summary>
        /// hàm thực hienj viecj đang nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Login_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
        }
        /// <summary>
        /// hàm thực hiện việc đóng for
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                b_Cancel = true;
                if (!blnRelogin)
                {
                    if (Utility.AcceptQuestion("Bạn có thực sự muốn thoát khỏi chương trình?", "Xác nhận", true))
                    {
                        Application.Exit();
                    }
                }
                else
                    this.Close();
            }
            catch
            {
            }
        }
        private bool isProcessRunning1 = false;

        /// <summary>
        /// hàm thực hiện viecj đang nhập thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLogin_Click(object sender, EventArgs e)
        {
            try
            {

               
                Application.DoEvents();
                Utility.WaitNow(this);
                cmdLogin.Enabled = false;
                if (!IsValid())
                {
                    cmdLogin.Enabled = true;
                    Utility.DefaultNow(this);
                    return;
                }
                if (chkRemember.Checked)
                {
                    PropertyLib._AppProperties.UID=Utility.sDbnull(txtUserName.Text);
                    PropertyLib._AppProperties.REM=chkRemember.Checked ;
                    PropertyLib._AppProperties.MenuStype=cbogiaodien.SelectedIndex;
                    PropertyLib.SaveProperty(PropertyLib._AppProperties);
                }
                
                PropertyLib._AppProperties.PWD = Utility.sDbnull(txtPassWord.Text);
                this.Close();
                Utility.DefaultNow(this);
            }
            catch
            { }
            finally
            {
                cmdLogin.Enabled = true;
                Utility.DefaultNow(this);
            }
           
        }
        
        private bool isProcessRunning = false;
       
        public bool b_Cancel = true;
        /// <summary>
        /// hàm thực hiện việc đăng nhập thông tin  kiểm tra
        /// quyền hợp lệ
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            UIAction.SetTextStatus(lblMsg, "", false);
            
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                UIAction.SetTextStatus(lblMsg, "Bạn phải nhập tên đăng nhập", true);
                cmdLogin.Enabled = true;
                UIAction.FocusEditbox(txtUserName);
                b_HasSuccess = true;
                return false;
            }
            if (cboKhoaKCB.Items.Count==0 || cboKhoaKCB.SelectedValue==null || cboKhoaKCB.SelectedValue.ToString() == "-1" || cboKhoaKCB.SelectedIndex < 0)
            {
                UIAction.SetTextStatus(lblMsg, "Bạn cần chọn khoa làm việc", true);
                cboKhoaKCB.Focus();
                return false;
            }
            PropertyLib._AppProperties.Makhoathien = Utility.sDbnull(cboKhoaKCB.SelectedValue, "KKB");
            globalVariables.MA_KHOA_THIEN = PropertyLib._AppProperties.Makhoathien;
            string UserName = Utility.sDbnull(Utility.GetPropertyValue(txtUserName, "Text"));
            string Password = Utility.sDbnull(Utility.GetPropertyValue(txtPassWord, "Text"));
            b_Cancel = true;
            globalVariables.LoginSuceess = new LoginService().isAdmin(Utility.sDbnull(UserName), Utility.sDbnull(Password));
            if (globalVariables.LoginSuceess) goto _Admin;
            globalVariables.LoginSuceess = new LoginService().KiemTraUserName(Utility.sDbnull(UserName));
            if (!globalVariables.LoginSuceess)
            {
                UIAction.SetTextStatus(lblMsg, "Sai tên đăng nhập. Mời bạn nhập lại", true);
                globalVariables._NumberofBrlink = 0;
                UIAction.FocusEditbox(txtUserName);
                return false;
            }
            globalVariables.LoginSuceess = new LoginService().KiemTraPassword(Utility.sDbnull(UserName), Utility.sDbnull(Password));
            if (!globalVariables.LoginSuceess)
            {
                UIAction.SetTextStatus(lblMsg, "Sai mật khẩu đăng nhập. Mời bạn nhập lại mật khẩu", true);
                globalVariables._NumberofBrlink = 0;
                UIAction.FocusEditbox(txtPassWord);
                return false;
            }
            globalVariables.LoginSuceess = new LoginService().DangNhap(Utility.sDbnull(UserName), Utility.sDbnull(Password.Trim()));
            if (!globalVariables.LoginSuceess)
            {
                Utility.ShowMsg("Thông tin đăng nhập không đúng, Mời bạn nhập lại User hoặc Password", "Thông báo", MessageBoxIcon.Warning);
                globalVariables._NumberofBrlink = 0;
                UIAction.FocusEditbox(txtUserName);
                return false;
            }
           
        _Admin:
            b_Cancel = false;
            if (cbogiaodien.SelectedIndex == 0)
                globalVariables.sMenuStyle = "MENU";
            else
                globalVariables.sMenuStyle = "DOCKING";
            if (!blnRelogin && PropertyLib._ConfigProperties.HIS_AppMode != VNS.Libs.AppType.AppEnum.AppMode.Demo)
            {
                UIAction.SetTextStatus(lblMsg, "Đang kiểm tra giấy phép sử dụng phần mềm...", false);
                if (PropertyLib._ConfigProperties.RunUnderWS)
                {
                    if (!WS._AdminWS.IsValidLicense())
                    {
                        globalVariables.LoginSuceess = false;
                        UIAction.SetTextStatus(lblMsg, "Phần mềm chưa đăng ký license. Đề nghị liên hệ nhà sản xuất phần mềm để được trợ giúp: 09 15 15 01 48 (Mr.Cường)", true);
                        return false;
                    }
                }
                else
                    if (!isValidSoftKey())
                    {
                        globalVariables.LoginSuceess = false;
                        Utility.ShowMsg("Phần mềm chưa đăng ký license. Đề nghị liên hệ nhà sản xuất phần mềm để được trợ giúp: 09 15 15 01 48 (Mr.Cường)");
                        UIAction.SetTextStatus(lblMsg, "Phần mềm chưa đăng ký license. Đề nghị liên hệ nhà sản xuất phần mềm để được trợ giúp: 09 15 15 01 48 (Mr.Cường)", true);
                        return false;
                    }
            }
            LoadDataForm();
            return true;
        }
        bool isValidSoftKey()
        {
            try
            {
                if (globalVariables.IsValidLicense) return true;
                string sRegKey = "";
                sRegKey = getRegKeyBasedOnSCPLicense();
                var AppKey = new MHardKey("XFW", 5, false);
                globalVariables.IsValidLicense = sRegKey == AppKey.RegKey;
                if (!globalVariables.IsValidLicense)
                {
                    VNS.Libs.AppLogger.LogAction.LogSCPService(string.Format(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->Kiểm tra khóa mềm không hợp lệ."));
                    return false;
                }
                else//Khóa check OK
                {
                    VNS.Libs.AppLogger.LogAction.LogSCPService(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->Kiểm tra khóa mềm hợp lệ...");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                VNS.Libs.AppLogger.LogAction.LogSCPService(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "-->Lỗi khi kiểm tra khóa mềm-->" + ex.Message);
                return false;
            }

        }
        string getRegKeyBasedOnSCPLicense()
        {
            try
            {
                string fileName = "";
                fileName = Application.StartupPath + @"\license.lic";
                if (!File.Exists(fileName)) return "";
                using (StreamReader _streamR = new StreamReader(fileName))
                {
                    return _streamR.ReadLine();
                }

            }
            catch
            {
                return "";
            }
        }
        private void LoadDataForm()
        {

            Application.DoEvents();
            LoadList();
            Application.DoEvents();

        }
        void Try2SaveXML()
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Application.StartupPath + @"\Config.xml");
                ds.Tables[0].Rows[0]["INTERFACEDISPLAY"] = globalVariables.sMenuStyle;
                ds.WriteXml(Application.StartupPath + @"\Config.xml", XmlWriteMode.IgnoreSchema);
            }
            catch
            {
            }
        }
        public void LoadList()
        {
            try
            {
               

                Try2SaveXML();
                UIAction.SetTextStatus(lblMsg, "Nạp thông tin cấu hình...", false);
                Utility.LoadProperties();
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu danh mục bệnh...", false);
                globalVariables.gv_dtDangbaoche = SPs.DmucLaydmucDangbaochethuoc().GetDataSet().Tables[0];
                globalVariables.gv_dtDmucChung = new Select().From(DmucChung.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucLoaibenh = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string> { "LOAIBENH" }, false);
                globalVariables.gv_dtDmucBenh = new Select().From(VDanhmucbenh.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu danh mục địa chính...", false);
                globalVariables.gv_dtDmucDiachinh = new Select().From(VDmucDiachinh.Schema).ExecuteDataSet().Tables[0];
                Utility.AutoCompeleteAddress(globalVariables.gv_dtDmucDiachinh);
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu danh mục nơi KCBBĐ...", false);
                globalVariables.gv_dtDmucNoiKCBBD = new Select().From(VDmucNoiKCBBD.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu dịch vụ CLS...", false);

                globalVariables.gv_dtDmucDichvuCls = new Select().From(VDmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtDmucDichvuClsChitiet = new Select().From(VDmucDichvuclsChitiet.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtQheDoituongDichvu =
                  new Select().From(QheDoituongDichvucl.Schema).ExecuteDataSet().Tables[0];
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu thuốc...", false);
                globalVariables.gv_dtQheDoituongThuoc = new Select().From(QheDoituongThuoc.Schema).ExecuteDataSet().Tables[0];
                
                UIAction.SetTextStatus(lblMsg, "Nạp dữ liệu hệ thống khác...", false);
                globalVariables.gv_dtSysparams = new Select().From(SysSystemParameter.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtSysTieude = new Select().From(SysTieude.Schema).ExecuteDataSet().Tables[0];

              
                globalVariables.IdKhoaNhanvien = (Int16)THU_VIEN_CHUNG.LayIDPhongbanTheoUser(globalVariables.UserName);
                globalVariables.idKhoatheoMay = (Int16)THU_VIEN_CHUNG.LayIDPhongbanTheoMay(globalVariables.MA_KHOA_THIEN);
                globalVariablesPrivate.objKhoaphong = DmucKhoaphong.FetchByID(globalVariables.idKhoatheoMay);
                globalVariablesPrivate.objNhanvien = new Select().From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.UserName).IsEqualTo(globalVariables.UserName).ExecuteSingle<DmucNhanvien>();
                if (globalVariablesPrivate.objNhanvien != null)
                    globalVariablesPrivate.objKhoaphongNhanvien = DmucKhoaphong.FetchByID(globalVariablesPrivate.objNhanvien.IdKhoa);
                globalVariables.gv_dtDanhmucchung = new Select().From(DmucChung.Schema).ExecuteDataSet().Tables[0];
                globalVariables.g_dtMeasureUnit = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string> { "DONVITINH" }, false);
                globalVariables.gv_dtDmucPhongban = new Select().From(DmucKhoaphong.Schema).ExecuteDataSet().Tables[0];
             
                globalVariables.SysDate = THU_VIEN_CHUNG.GetSysDateTime();
               
                globalVariables.gv_dtDmucNhanvien = new Select().From(VDmucNhanvien.Schema).ExecuteDataSet().Tables[0];
                Utility.LoadImageLogo();


            }

            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                THU_VIEN_CHUNG.GetIP4Address();
                //THU_VIEN_CHUNG.GetMACAddress();
                THU_VIEN_CHUNG.LoadThamSoHeThong();
                
                
            }


        }

        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {
            
            
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmdLogin_Click(cmdLogin, new EventArgs());
        }

        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string facebook = "http://www.facebook.com/HIS.QLBV";
            Utility.OpenProcess(facebook);
        }
    }
}
