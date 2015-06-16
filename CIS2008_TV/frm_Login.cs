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

namespace CIS.CoreApp
{
    public partial class frm_Login : Form
    {
        private BackgroundWorker bw = new BackgroundWorker();
        public frm_Login()
        {
            InitializeComponent();
           
           
            txtUserName.LostFocus+=new EventHandler(txtUserName_LostFocus);
            if (globalVariables.sMenuStyle == "MENU")
                cbogiaodien.SelectedIndex = 0;
            else
                cbogiaodien.SelectedIndex = 1;
            
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
                    Application.Exit();
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
                if (cboKhoaKCB.SelectedValue.ToString() == "-1")
                {
                    UIAction.SetTextStatus(lblMsg, "Bạn cần chọn khoa làm việc", true);
                    cboKhoaKCB.Focus();
                    return;
                }
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
            LoadDataForm();
            return true;
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
               
                
                //var query = from o in Directory.GetFiles(Application.StartupPath + @"/Hislink_log", "*.*",
                //SearchOption.AllDirectories)
                //            let x = new FileInfo(o)
                //            where x.CreationTime <= THU_VIEN_CHUNG.GetSysDateTime().AddMonths(-6)

                //            select o;
                //foreach (var item in query)
                //{
                //    File.Delete(item);

                //}
                globalVariables.gv_dtDmucNhanvien = new Select().From(VDmucNhanvien.Schema).ExecuteDataSet().Tables[0];
                Utility.LoadImageLogo();


            }// globalVariables.b_LISConnectionState = HIS_LIS.isLISConnectionState()

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
