using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Janus.Windows.UI.Dock;
using Janus.Windows.UI.StatusBar;
using NLog;
using NLog.Config;
using NLog.Targets;
using SubSonic;
using VNS.Libs;
using VNS.Core.Classes;
using VNS.HIS.DAL;
using System.Collections.Generic;
using VNS.Properties;
using System.Data;
using System.Diagnostics;

namespace CIS.CoreApp
{
    /// <summary>
    /// abcd
    /// </summary>
    public partial class frm_MainForm_new : Form
    {
        Thread t = null;
        Thread t1 = null;
        private readonly string sPath = Application.StartupPath;
        private Logger log;
        bool fored2Close = false;
        bool Click2Update = false;
        private string sFileName = "File_Config/autoHie.txt";
        private string sFileNameScreen = Application.StartupPath + "/File_Config/defaultscreen.txt";
        
        /// <summary>
        /// hàm thực hiện việc khởi tạo
        /// </summary>
        public frm_MainForm_new()
        {
            try
            {
                WS._AdminWS = new VNSCore.AWS.LoginWS();
                 
                globalVariables.m_strPropertiesFolder = Application.StartupPath + @"\Properties\";
                if (!new ConnectionSQL().ReadConfig())
                {
                    Try2ExitApp();
                    return;
                }

                InitializeComponent();
                lblUpdateVersion.Click += new EventHandler(lblUpdateVersion_Click);
                lblDepartment.Click += new EventHandler(lblDepartment_Click);
                Utility.autoStartWServices(new List<string> { "Spooler" });
                PanelManager.MdiTabCreated += new MdiTabEventHandler(PanelManager_MdiTabCreated);
                this.KeyDown += new KeyEventHandler(frm_MainForm_new_KeyDown);
                PanelManager.MdiTabClosed += new MdiTabClosedEventHandler(PanelManager_MdiTabClosed);
                PanelManager.MdiTabDoubleClick += new MdiTabEventHandler(PanelManager_MdiTabDoubleClick);
                PanelManager.MdiTabClick += new MdiTabEventHandler(PanelManager_MdiTabClick);
                cmdLoadSysparams.Click += new EventHandler(cmdLoadSysparams_Click);
                cmdClose.Click += new EventHandler(cmdClose_Click);
                cmdHelp.Click += new EventHandler(cmdHelp_Click);
                mnureLoad.Click += new EventHandler(mnureLoad_Click);
                
                lblCopyright.Click += new EventHandler(lblCopyright_Click);
                InitLogs();
                WS._AdminWS.Url = PropertyLib._ConfigProperties.WSURL;
                if (PropertyLib._ConfigProperties.HIS_AppMode!=VNS.Libs.AppType.AppEnum.AppMode.Demo && PropertyLib._ConfigProperties.RunUnderWS)
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
                Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
                Try2ReadApp();
                //Kill chương trình UpdateVersion
                Utility.KillProcess(AppName);
                //Thực hiện kiểm tra phiên bản
                CheckVersion();
                treeView.CollapseAll();
                if (!globalVariables.IsAdmin)
                {
                    treeView.ExpandAll();
                }
            }
            catch
            {
            }
            finally
            {
                PanelManager.ContainerControl = null;
                this.IsMdiContainer = false;
                pnlHeader.Visible = false;
                Application.DoEvents();
            }

        }

        void mnureLoad_Click(object sender, EventArgs e)
        {
            try
            {
                globalVariables.gv_dtSysparams = new Select().From(SysSystemParameter.Schema).ExecuteDataSet().Tables[0];
                globalVariables.gv_dtSysTieude = new Select().From(SysTieude.Schema).ExecuteDataSet().Tables[0];
                THU_VIEN_CHUNG.LoadThamSoHeThong();

                SqlQuery sqlQueryUnit =
               new Select().From(SysManagementUnit.Schema).Where(SysManagementUnit.Columns.PkSBranchID).IsEqualTo(
                    globalVariables.Branch_ID);
           SysManagementUnit objManagementUnit = sqlQueryUnit.ExecuteSingle<SysManagementUnit>();
           if (objManagementUnit != null)
           {
               globalVariables.Branch_ID = objManagementUnit.PkSBranchID;
               globalVariables.Branch_Address = objManagementUnit.SAddress;
               globalVariables.Branch_Name = objManagementUnit.SName;
               globalVariables.Branch_Email = objManagementUnit.SEMAIL;
               globalVariables.Branch_Phone = objManagementUnit.SPhone;
               globalVariables.ParentBranch_Name = objManagementUnit.SParentBranchName;
               globalVariables.Branch_Website = objManagementUnit.Website;
               globalVariables._NumberofBrlink = 3;
               globalVariables.SysLogo = objManagementUnit.Logo;

           }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void lblDepartment_Click(object sender, EventArgs e)
        {
            frm_ChuyenKhoaKCB _ChuyenKhoaKCB = new frm_ChuyenKhoaKCB();
            _ChuyenKhoaKCB._OnChangedDepartment += new frm_ChuyenKhoaKCB.OnChangedDepartment(_ChuyenKhoaKCB__OnChangedDepartment);
            _ChuyenKhoaKCB.ShowDialog();

        }

        void _ChuyenKhoaKCB__OnChangedDepartment()
        {
            try
            {
                string lstID = string.Join(",", dic.Keys);
                if (PropertyLib._AppProperties == null) PropertyLib._AppProperties = PropertyLib.GetAppPropertiess();
                PropertyLib._AppProperties.AutoLogin = true;
                PropertyLib._AppProperties.OpenningList = lstID;
                PropertyLib._AppProperties.OpenOnlyCurrent = false;
                globalVariables.idKhoatheoMay = (Int16)THU_VIEN_CHUNG.LayIDPhongbanTheoMay(globalVariables.MA_KHOA_THIEN);
                globalVariablesPrivate.objKhoaphong = DmucKhoaphong.FetchByID(globalVariables.idKhoatheoMay);
                ClearTab();
                Application.DoEvents();
                lstTab.Clear();
                dic.Clear();
                AutoOpen(true);
                PropertyLib._AppProperties.OpenningList = "";
                PropertyLib._AppProperties.AutoLogin = false;
                PropertyLib._AppProperties.CurrentOpenning = "-1";
                PropertyLib.SaveProperty(PropertyLib._AppProperties);
            }
            catch (Exception ex)
            {
            }
        }

        void lblCopyright_Click(object sender, EventArgs e)
        {
            string facebook = THU_VIEN_CHUNG.Laygiatrithamsohethong("FaceBook", "http://www.facebook.com/HIS.QLBV", true);
            Utility.OpenProcess(facebook);
        }

        void PanelManager_MdiTabDoubleClick(object sender, MdiTabEventArgs e)
        {
         if(PropertyLib._AppProperties.TabDoubleClick2Close)   e.Tab.Form.Close();
        }

        void lblUpdateVersion_Click(object sender, EventArgs e)
        {
            Click2Update = true;
            CheckVersion();
            Click2Update = false;
        }
        //CheckVersion
        void CheckVersion()
        {
            try
            {
                DataTable dtData = SPs.SysGetversions().GetDataSet().Tables[0];
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                  if(Click2Update)  Utility.ShowMsg("Hệ thống vừa thực hiện kiểm tra phiên bản và xác nhận Phiên bản bạn đang dùng là mới nhất!.\nMời bạn nhấn OK để tiếp tục làm việc", "Thông báo kiểm tra cập nhật phiên bản");
                    return;
                }
                DataTable dtLastVersion = dtData.Clone();
                foreach (DataRow dr in dtData.Rows)
                {
                    if (!File.Exists(Application.StartupPath + "\\" + Utility.sDbnull(dr["sFileName"], "")))
                    {
                        InsertNewRow(dr, dtData, ref dtLastVersion);
                        //Nếu tồn tại thì kiểm tra xem Version có khác nhau không?
                    }
                    else
                    {
                        FileVersionInfo _FileVersionInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\" + dr["sFileName"]);
                        string sVersion = _FileVersionInfo.ProductVersion;
                        if ((sVersion == null))
                        {
                            //InsertNewRow(dr, sv_DSVersion.Tables(0), sv_DSLastestV)
                        }
                        else
                        {
                            if (!sVersion.Equals(dr["sVersion"]) && Utility.Int32Dbnull(dr["isUpdate"], 0) == 1)
                            {
                                InsertNewRow(dr, dtData, ref dtLastVersion);
                            }
                        }

                    }
                }
                dtLastVersion.AcceptChanges();
                //Kiểm tra nếu có phiên bản thì kích hoạt chương trình UpdateVersion
                if (dtLastVersion != null && dtLastVersion.Rows.Count > 0)
                {
                    if (Click2Update)
                        if (!Utility.AcceptQuestion("Hệ thống phát hiện phiên bản cần mới cần cập nhật. Bạn có chắc chắn muốn cập nhật phiên bản hay không?\nChú ý: Nếu đồng ý cập nhật, phần mềm sẽ tự động đóng lại và bật lên ngay sau khi cập nhật xong", "Thông báo cập nhật phiên bản mới", true))
                            return;
                        else//Đồng ý-->Đánh dấu tự động đăng nhập
                        {
                            string lstID = string.Join(",", dic.Keys);
                            if (PropertyLib._AppProperties == null) PropertyLib._AppProperties = PropertyLib.GetAppPropertiess();
                            PropertyLib._AppProperties.AutoLogin = true;
                            PropertyLib._AppProperties.OpenningList = lstID;
                            PropertyLib.SaveProperty(PropertyLib._AppProperties);
                        }

                    //Thực hiện kiểm tra phiên bản
                    Utility.OpenProcess(AppName);//Khi gọi phần này thì tự động sẽ kill chính bản thân chương trình này
                }
                else//Tiếp tục vào HIS
                {
                    if (Click2Update)
                        Utility.ShowMsg("Hệ thống vừa thực hiện kiểm tra phiên bản và xác nhận Phiên bản bạn đang dùng là mới nhất!.\nMời bạn nhấn OK để tiếp tục làm việc", "Thông báo kiểm tra cập nhật phiên bản");
                    return;
                }
            }
            catch
            {
            }
        }
        public void InsertNewRow(DataRow dr, DataTable pv_SourceTable, ref DataTable pv_DS)
        {
            try
            {
                DataRow DRLastestV = pv_DS.NewRow();
                foreach (DataColumn col in pv_SourceTable.Columns)
                {
                    DRLastestV[col.ColumnName] = dr[col.ColumnName];
                }
                pv_DS.Rows.Add(DRLastestV);

            }
            catch (Exception ex)
            {
            }
        }
        string AppName = Application.StartupPath + @"\HIS.exe";
        void Try2ReadApp()
        {
            try
            {
                string StartupFile = Application.StartupPath + @"\Startup.txt";
                if (File.Exists(StartupFile))
                    AppName = Application.StartupPath + @"\" + GetFirstValueFromFile(StartupFile);
            }
            catch
            {
                AppName = Application.StartupPath + @"\HIS.exe";
            }
        }
        string GetFirstValueFromFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName)) return "";
                using (StreamReader _Reader = new StreamReader(fileName))
                {
                    _Reader.ReadLine();
                    object obj = _Reader.ReadLine();
                    if (obj == null) return "";
                    return obj.ToString().Trim();
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        void cmdHelp_Click(object sender, EventArgs e)
        {

        }

        void cmdClose_Click(object sender, EventArgs e)
        {
            fored2Close = true;
            if (Utility.AcceptQuestion("Bạn có thực sự muốn thoát khỏi chương trình?", "Xác nhận", true))
                Application.Exit();
            else
                fored2Close = false;
        }

        void cmdLoadSysparams_Click(object sender, EventArgs e)
        {
            frm_Settings _Settings = new frm_Settings();
            Font oldFont = PropertyLib._AppProperties.MenuFont;
            _Settings.ShowDialog();
            if (IsFontChanged(oldFont, PropertyLib._AppProperties.MenuFont))
                LoadMenu();

        }
        bool IsFontChanged(Font f1, Font f2)
        {
            return f1.Name != f2.Name || f1.Size != f2.Size || f1.Style != f2.Style;
        }

        void frm_MainForm_new_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                if (pnlHeader.Height == 0)
                    pnlHeader.Height = 38;
                else pnlHeader.Height = 0;
                PropertyLib._AppProperties.AutoHideHeader = pnlHeader.Height == 0;
                PropertyLib.SaveProperty(PropertyLib._AppProperties);
            }
            if (e.KeyCode == Keys.F11)
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn chuyển giao diện hiển thị về dạng " + (globalVariables.sMenuStyle == "MENU" ? " giao diện hòm thư Outlook hay không?" : " menu truyền thống hay không?"), "Xác nhận chuyển giao diện", true))
                {
                    if (globalVariables.sMenuStyle == "MENU")
                    {
                        globalVariables.sMenuStyle = "DOCKING";
                        PropertyLib._AppProperties.MenuStype = 1;
                    }
                    else
                    {
                        if (pMain != null)
                            pMain.AutoHide = false;
                        globalVariables.sMenuStyle = "MENU";
                        PropertyLib._AppProperties.MenuStype = 0;
                    }
                    PropertyLib.SaveProperty(PropertyLib._AppProperties);
                    LoadMenu();
                }
            }
        }
        void InitLogs()
        {
            try
            {
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget();
                config.AddTarget("file", fileTarget);
                fileTarget.FileName =
                    "${basedir}/Mylogs/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log";
                fileTarget.Layout = "${date:format=HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${message}";
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
                log = LogManager.GetCurrentClassLogger();
            }
            catch
            {
            }
        }
        void Try2ExitApp()
        {
            try
            {
                this.Close();
                this.Dispose();
                Application.Exit();
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện việc cho phép load thông tin của forrm heienjtaij
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_MainForm_new_Load(object sender, EventArgs e)
        {
            try
            {

                LoadBackground();
                SetsyncDateTime();
                lblTime.Text ="Bây giờ là: "+ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                ClearTab();
                LoadFormMain();
                if (!PropertyLib._AppProperties.AutoHideHeader)
                    pnlHeader.Height = 38;
                else
                    pnlHeader.Height = 0;
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       
        private void SetsyncDateTime()
        {
            SystemTime updatedTime = new SystemTime();
            DateTime dateTime = new LoginService().GetSysDateTime();
            updatedTime.DayOfWeek = (short)dateTime.DayOfWeek;
            updatedTime.Year = (short)dateTime.Year;
            updatedTime.Month = (short)dateTime.Month;
            updatedTime.Day = (short)dateTime.Day;
            updatedTime.Hour = (short)dateTime.Hour;
            updatedTime.Minute = (short)dateTime.Minute;
            updatedTime.Second = (short)dateTime.Second;
            SetLocalTime(ref updatedTime);
        }

        public struct SystemTime
        {
            public short Year;
            public short Month;
            public short DayOfWeek;
            public short Day;
            public short Hour;
            public short Minute;
            public short Second;
            public short Millisecond;
        };


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetLocalTime(ref SystemTime theDateTime);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void GetLocalTime(ref SYSTEMTIME theDateTime);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME theDateTime);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void GetSystemTime(ref SYSTEMTIME theDateTime);
        /// <summary>
        /// hàm thực hiện việc clear tab
        /// </summary>
        public void ClearTab()
        {
            try
            {
                Form[] charr = this.MdiChildren;


                foreach (Form chform in charr)
                {

                    chform.Close();
                    chform.Dispose();

                }
            }
            catch (Exception)
            {


            }


        }
        /// <summary>
        /// hàm thực hiện load backgroud 
        /// </summary>
        private void LoadBackground()
        {
            try
            {
                string filename = "";
                string s = string.Format("{0}{1}Images{1}default.jpg", sPath, Path.DirectorySeparatorChar);
                if (File.Exists(s)) filename = s;

                s = string.Format("{0}{1}Images{1}default.png", sPath, Path.PathSeparator);
                if (File.Exists(s)) filename = s;

                s = string.Format("{0}{1}Images{1}default.bmp", sPath, Path.PathSeparator);
                if (File.Exists(s)) filename = s;
                //if (!string.IsNullOrEmpty(filename)) this.BackgroundImage = Utility.LoadBitmap(filename);

                if (!string.IsNullOrEmpty(filename))
                {
                    Image img;
                    img = Image.FromFile(filename);
                    BackgroundImage = img;
                }

            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void LoadFormMain()
        {
            try
            {
                isLoginFirstTime = false;
                if (!globalVariables.LoginSuceess) CallLogin();

                if (globalVariables.LoginSuceess)
                {
                    PanelManager.ContainerControl = this;
                    pnlHeader.Visible = true;
                    this.IsMdiContainer = true;
                    Text = globalVariables.FORMTITLE;
                    Application.DoEvents();
                    SetInfor();

                    LoadMenu();
                    LoadBackground();
                }
            }
            catch (Exception exception)
            {
            }
        }

        // private int Status = 1;
        // [DllImport("VietBaIT.HISLink.LoadEnvironments.dll")]
        private void LoadDataForm()
        {
            //Application.DoEvents();
            //var hisData = new HISData();
            //var objThread = new Thread(hisData.LoadList);
            //objThread.Start();
            //Application.DoEvents();
        }

        /// <summary>
        /// hàm thực hiện việc gọi form đăng nhập
        /// </summary>
        private void CallLogin()
        {
            Utility.WaitNow(this);
            Application.DoEvents();
            var frm = new frm_Login();
            frm.ShowDialog();
            Utility.DefaultNow(this);
        }



        private bool b_Hasloaded = false;
        /// <summary>
        /// hàm thực hiện việc load thông tin của phần staatus trạng thái của core chạy chương trình
        /// </summary>
        private void SetInfor()
        {
            try
            {
                lblHospital.Text = globalVariables.Branch_Name + " Tuyến: " + THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TUYEN", "NONE", true);
                lblUser.Text = globalVariables.UserName;
                lblIP.Text = THU_VIEN_CHUNG.GetIP4Address();
                DmucKhoaphong objDepartment = new Select().From(DmucKhoaphong.Schema)
                   .Where(DmucKhoaphong.Columns.MaKhoaphong).IsEqualTo(globalVariables.MA_KHOA_THIEN).ExecuteSingle
                   <DmucKhoaphong>();
                if (objDepartment != null)
                {
                    lblDepartment.Text = string.Format("{0}-{1}", globalVariables.MA_KHOA_THIEN, objDepartment.TenKhoaphong);
                }
                else
                {
                    lblDepartment.Text = globalVariables.MA_KHOA_THIEN;
                }
                lblCopyright.Text = THU_VIEN_CHUNG.Laygiatrithamsohethong("TEN_CONGTYPHANMEM","COPYRIGHT © Công ty cổ phần CNTT VINASOFT", true);
                Text = globalVariables.FORMTITLE;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetInfor.-->Exception()" + ex.Message);
            }
        }

        /// <summary>
        /// hàm thực hiện việc làm tạo các phân hệ thông tin 
        /// </summary>
        private void LoadMenu()
        {

            switch (globalVariables.sMenuStyle)
            {
                case "DOCKING":
                    LoadMenuStyleDocking();
                    break;
                case "MENU":
                    LoadMenuStyleNormal();
                    break;
                default:
                    LoadMenuStyleNormal();
                    break;
            }
            if (PropertyLib._AppProperties.AutoLogin)
            {
                AutoOpen(false);
                if (PropertyLib._AppProperties == null) PropertyLib._AppProperties = PropertyLib.GetAppPropertiess();
                PropertyLib._AppProperties.AutoLogin = true;
                PropertyLib._AppProperties.OpenningList = "";

                PropertyLib._AppProperties.AutoLogin = false;
                PropertyLib._AppProperties.CurrentOpenning = "-1";
                PropertyLib.SaveProperty(PropertyLib._AppProperties);
            }
        }

        private void LoadMenuStyleNormal()
        {
            try
            {
                pMain.Visible = false;
                menuStrip.Visible = true;
                menuStrip.Items.Clear();
                InitializeSystemControl();
                FindMenuChild(-2);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo menu hệ thống\n" + ex.Message);
            }
            finally
            {

            }
        }
        void AutoOpen( bool forced2Open)
        {
            try
            {
                DataRow[] arrDrActive;
                if (PropertyLib._AppProperties.AutoOpen || forced2Open)
                {
                    int _id = Utility.Int32Dbnull(PropertyLib._AppProperties.CurrentOpenning, "-1");
                    List<string> lstID = PropertyLib._AppProperties.OpenningList.Split(',').ToList<string>();
                    if (!PropertyLib._AppProperties.OpenOnlyCurrent)
                    {
                        foreach (string sId in lstID)
                        {
                            arrDrActive = dtSysRoles.Select("iRoleID=" + sId);
                            if (arrDrActive.Length > 0)
                                DisplaySingleFunction(arrDrActive[0]);
                        }
                    }
                    if (_id > 0)
                    {
                        arrDrActive = dtSysRoles.Select("iRoleID=" + _id.ToString());
                        if (arrDrActive.Length > 0)
                            DisplaySingleFunction(arrDrActive[0]);
                    }
                    UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_id.ToString()].Form);
                    this.ActivateMdiChild(dic[_id.ToString()].Form);
                }

            }
            catch (Exception ex)
            {
            }

        }
        DataTable dtSysRoles = new DataTable();
        private void FindMenuChild(int Parent_ID)
        {
            dtSysRoles = SPs.SysGetroles(globalVariables.UserName, globalVariables.IsAdmin ? 1 : 0, globalVariables.Branch_ID).GetDataSet().Tables[0];
            DataRow[] arrDrPhanhe = dtSysRoles.Select("iParentRoleID=-2", "iOrder asc ");
            //Kiểm tra nếu chỉ có 1 chức năng thì tự động kích hoạt luôn
            DataRow[] arrDrActive = dtSysRoles.Select("PK_iID<>-1 AND PK_iID is not null", "iOrder asc ");

            foreach (DataRow drCha in arrDrPhanhe)//Phân hệ
            {
                var objMenuphanhe = new ToolStripMenuItem();
                objMenuphanhe.Text = Utility.sDbnull(drCha["sRoleName"], "");
                objMenuphanhe.Name = Utility.sDbnull(drCha["iRoleID"], "-1");
                objMenuphanhe.ToolTipText = Utility.sDbnull(drCha["sRoleName"], "");
                menuStrip.Items.Add(objMenuphanhe);
                menuStrip.Font = PropertyLib._AppProperties.MenuFont;
                DataRow[] arrDrMenu = dtSysRoles.Select("iParentRoleID=" + Utility.sDbnull(drCha["iRoleID"], "-1"), "iOrder asc ");
                //Gọi đệ quy
                AddChildMenu(objMenuphanhe, Utility.Int32Dbnull(drCha["iRoleID"], -1));
                
            }
            if (arrDrActive.Length > 1)
            {

            }
            else if (arrDrActive.Length == 1)
            {
                isSingleFunction = true;
                DisplaySingleFunction(arrDrActive[0]);
            }
        }

       
        /// <summary>
        /// Tạo và duyệt menu cấp 1 của từng phân hệ
        /// </summary>
        /// <param name="objMenuItem"></param>
        /// <param name="arrDrMenu"></param>
        /// <param name="dt"></param>
        /// <param name="idx"></param>
        /// <param name="bLastRecord"></param>
        private void AddChildMenu(ToolStripMenuItem ParentMenuItem, int iRoleID)
        {
            DataRow[] arrDrMenu = dtSysRoles.Select("iParentRoleID=" + Utility.sDbnull(iRoleID), "iOrder asc ");
            foreach (DataRow drMenu in arrDrMenu)//Top node loop
            {
                ToolStripMenuItem objMenuChild = CreateMenuChild(ParentMenuItem, drMenu);
              AddChildMenu(objMenuChild, Utility.Int32Dbnull(drMenu["iRoleID"], -1));
            }
        }
        /// <summary>
        /// Tạo và duyệt các menu cấp 2 của từng menu cấp 1
        /// </summary>
        /// <param name="MenuItem"></param>
        /// <param name="drMenu"></param>
        /// <param name="dt"></param>
        /// <param name="b_LastRecord"></param>
        private ToolStripMenuItem CreateMenuChild(ToolStripMenuItem ParentMenuItem, DataRow drMenu)
        {
            int idx = 0;
            string sDllName = "";
            string sRoleName = "";
            string sFunctionName = "";
            string IsTabView = "0";
            string sThamSo = "";
            int IRoleID = Utility.Int32Dbnull(drMenu["iRoleID"], -1);
            string isTabView = Utility.sDbnull(drMenu["isTabView"], "0");
            sDllName = Utility.sDbnull(drMenu["sDLLName"], "");// objSysFunction.SDLLname;
            sFunctionName = Utility.sDbnull(drMenu["sFormName"], "");//objSysFunction.SFormName;
            sThamSo = Utility.sDbnull(drMenu["sParameterList"], "");// objChildSysRole.SParameterList;
            sRoleName = Utility.sDbnull(drMenu["sRoleName"], "");

            ToolStripMenuItem objMenuChild = new ToolStripMenuItem(sRoleName);

            objMenuChild.Text = sRoleName.Trim().Replace("&", "");
            objMenuChild.Tag = sFunctionName + "#" + isTabView + "#" + sThamSo + "#" + IRoleID.ToString();
            //objMenuChild.Tag = sFunctionName + "#" + Utility.sDbnull(objChildSysRole.IsTabView, 0) + "#" + sThamSo;
            objMenuChild.Name = sDllName + "#" + sRoleName + "#" + IRoleID.ToString();
            objMenuChild.Font = PropertyLib._AppProperties.MenuFont;
            objMenuChild.ForeColor = Color.Navy;
            objMenuChild.ToolTipText = sRoleName;
            //objMenuChild.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            //objTreeNode.SelectedImageIndex = 3;

            objMenuChild.ImageIndex = 2;
            ParentMenuItem.DropDownItems.Add(objMenuChild);

            objMenuChild.Click += objmenuStrip_Click;
            return objMenuChild;
        }
       
        private void InitializeSystemControl()
        {
            //menuStrip.ImageList = SystemLogin;
            SystemToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
                                                               {
                                                                   cmdMenuLogin,
                                                                   ToolStripSeparator4,
                                                                   cmdMenuLogout,
                                                                   ToolStripSeparator1,
                                                                   cmdChangePassword,
                                                                   ToolStripSeparator3,
                                                                   cmdMenuDonVi_LamViec,
                                                                   ToolStripSeparator2,
                                                                   cmdMenuExit
                                                               });

            SystemToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
                                                               {
                                                               });
            SystemToolStripMenuItem.Text = "Hệ thống";
            SystemToolStripMenuItem.ToolTipText = "Cấu hình hệ thống";
            //menuStrip.Items.Add(SystemToolStripMenuItem);
            //menuStrip.Font = new Font("Tahoma", 9F);

            //menuStrip.Location = new Point(0, 0);
            //menuStrip.Size = new Size(957, 24);

            cmdMenuLogin.ImageIndex = 0;
            ;
            cmdMenuLogin.Name = "LoginToolStripMenuItem";
            cmdMenuLogin.Size = new Size(231, 22);
            cmdMenuLogin.Text = "Đăng &nhập";
            cmdMenuLogin.Click += cmdLoginMenuItem_Click;
            // 
            // LogoutToolStripMenuItem
            // 
            cmdMenuLogout.ImageIndex = 1;
            cmdMenuLogout.Name = "LogoutToolStripMenuItem";
            cmdMenuLogout.Size = new Size(231, 22);
            cmdMenuLogout.Text = "Đăng &xuất";
            cmdMenuLogout.Click += cmdMenuLogout_Click;
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(228, 6);
            // 
            // thayĐổiMậtKhẩuToolStripMenuItem
            // 
            cmdChangePassword.ImageIndex = 2;
            cmdChangePassword.Name = "thayĐổiMậtKhẩuToolStripMenuItem";
            cmdChangePassword.Size = new Size(231, 22);
            cmdChangePassword.Text = "Thay đổi mật khẩu";
            cmdChangePassword.Click += cmdChangePasswordMenuItem_Click;

            // 
            // ToolStripSeparator3
            // 
            ToolStripSeparator3.Name = "ToolStripSeparator3";
            ToolStripSeparator3.Size = new Size(228, 6);
            // 
            // cmdDonVi_LamViec
            // 
            cmdMenuDonVi_LamViec.ImageIndex = 3;
            //this.cmdMenuDonVi_LamViec.Image = ((System.Drawing.Image)(resources.GetObject("cmdDonVi_LamViec.Image")));
            cmdMenuDonVi_LamViec.Name = "cmdDonVi_LamViec";
            cmdMenuDonVi_LamViec.Size = new Size(231, 22);
            cmdMenuDonVi_LamViec.Text = "&Đơn vị làm việc";
            cmdMenuDonVi_LamViec.Click += cmdDonVi_LamViec_Click;
            // 
            // ToolStripSeparator2
            // 
            ToolStripSeparator2.Name = "ToolStripSeparator2";
            ToolStripSeparator2.Size = new Size(228, 6);
            // 
            // ExitToolStripMenuItem
            // 
            cmdMenuExit.ImageIndex = 4;
            //this.cmdMenuExit.Image = ((System.Drawing.Image)(resources.GetObject("ExitToolStripMenuItem.Image")));
            cmdMenuExit.Name = "ExitToolStripMenuItem";
            cmdMenuExit.ShortcutKeys = (((Keys.Control | Keys.D)));
            cmdMenuExit.Size = new Size(231, 22);
            cmdMenuExit.Text = "Thoát";
            cmdMenuExit.Click += cmdExitMenuItem_Click;
        }
        void AutoloadControlPanel()
        {
            try
            {
                ControlPanel form = new ControlPanel();
                form.Tag = -1;
                form.MdiParent = this;
                form.ShowInTaskbar = false;
                form.Show();
            }
            catch
            {
            }
        }
        private void LoadMenuStyleDocking()
        {
            try
            {
                pMain.Visible = true;
                menuStrip.Items.Clear();
                menuStrip.Visible = false;
                pMain.AutoHide = true;
                pMain.SuspendLayout();
                //Load bảng điều khiển
                //AutoloadControlPanel();
                PanelManager.ImageList = imageList1;
                dtSysRoles = SPs.SysGetroles(globalVariables.UserName, globalVariables.IsAdmin ? 1 : 0, globalVariables.Branch_ID).GetDataSet().Tables[0];

                pMain.Panels.Clear();
                DataRow[] arrDrPhanhe = dtSysRoles.Select("iParentRoleID=-2", "iOrder asc ");

                List<UIPanel> lstPanel = new List<UIPanel>();

                //Kiểm tra nếu chỉ có 1 chức năng thì tự động kích hoạt luôn
                DataRow[] arrDrActive = dtSysRoles.Select("PK_iID<>-1 AND PK_iID is not null", "iOrder asc ");



                foreach (DataRow drCha in arrDrPhanhe)//Phân hệ
                {
                    TreeView objTreeView = null;
                    objTreeView = new TreeView();
                    objTreeView.FullRowSelect = true;
                    objTreeView.ImageList = imageList;
                    //objTreeView.Name = "treeview" + Utility.sDbnull(objSysRole.IRole);
                    objTreeView.Dock = DockStyle.Fill;
                    objTreeView.ImageIndex = 2;
                    objTreeView.AutoSize = true;

                    objTreeView.HideSelection = false;
                    objTreeView.MouseDown += new MouseEventHandler(objTreeView_MouseDown);
                    objTreeView.DoubleClick += objTreeView_DoubleClick;
                    objTreeView.Click += new EventHandler(objTreeView_Click);
                    objTreeView.AfterSelect += new TreeViewEventHandler(objTreeView_AfterSelect);
                    var objPanel = new UIPanel();
                    var innerContainer = new UIPanelInnerContainer();
                    innerContainer.Controls.Add(objTreeView);
                    objPanel.InnerContainer = innerContainer;
                    objPanel.Location = new Point(0, 0);
                    objPanel.Name = Utility.sDbnull(drCha["iRoleID"], "");
                    objPanel.Size = new Size(196, 429);
                    objPanel.TabIndex = Utility.Int32Dbnull(drCha["iOrder"], -1);
                    objPanel.Text = Utility.sDbnull(drCha["sRoleName"], "Không xác định");
                    objPanel.ImageIndex = 17;
                    objPanel.LargeImageIndex = 17;
                    objPanel.Font = new Font("Tahoma", 10, FontStyle.Bold);
                    //lstPanel.Add(objPanel);

                    //Duyệt qua từng menu trong phân hệ
                    DataRow[] arrDrTopMenu = dtSysRoles.Select("iParentRoleID=" + Utility.sDbnull(drCha["iRoleID"], "-1"), "iOrder asc ");
                    foreach (DataRow drTopmenu in arrDrTopMenu)//Top Node Loop
                    {
                        TreeNode objTopNode = AddNewTreeNode(objTreeView, null, drTopmenu);
                        CreateChildNode(objTreeView, objTopNode, Utility.Int32Dbnull(drTopmenu["iRoleID"], -1));
                    }
                    pMain.Panels.Add(objPanel);

                }
                //foreach (UIPanel _panel in lstPanel)
                //    pMain.Panels.Add(_panel);
                if (arrDrActive.Length > 1)
                {


                    pMain.AutoHide = false;
                }
                else if (arrDrActive.Length == 1)
                {
                    PanelManager.DefaultPanelSettings.CloseButtonVisible = false;
                    pMain.AutoHide = false;
                    isSingleFunction = true;
                    DisplaySingleFunction(arrDrActive[0]);
                }
                if (isSingleFunction)
                    pMain.AutoHide = true;
            }
            catch
            {
            }
            finally
            {
                pMain.ResumeLayout();
            }
        }
        /// <summary>
        /// Top Node of Subsystem
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="SystemRoleID"></param>
        private void CreateChildNode(TreeView treeView, TreeNode parentNode, int ParentRoleID)
        {
            DataRow[] arrDrMenu = dtSysRoles.Select("iParentRoleID=" + Utility.sDbnull(ParentRoleID), "iOrder asc ");
            foreach (DataRow drmenu in arrDrMenu)//Top Node Loop
            {
                TreeNode objNode = AddNewTreeNode(treeView, parentNode, drmenu);
                CreateChildNode(treeView, objNode, Utility.Int32Dbnull(drmenu["iRoleID"], -1));
            }
        }
       
        private TreeNode AddNewTreeNode(TreeView objTreeView, TreeNode objParentTreeNode, DataRow drItems)
        {
            string sDllName = "";
            string sRoleName = "";
            string sFunctionName = "";
            string IsTabView = "0";
            string sThamSo = "";
            int IRoleID = Utility.Int32Dbnull(drItems["iRoleID"], -1);
            sDllName = Utility.sDbnull(drItems["sDLLName"], "");// objSysFunction.SDLLname;
            sFunctionName = Utility.sDbnull(drItems["sFormName"], "");//objSysFunction.SFormName;
            sThamSo = Utility.sDbnull(drItems["sParameterList"], "");// objChildSysRole.SParameterList;
            sRoleName = Utility.sDbnull(drItems["sRoleName"], "");

            TreeNode objnewChildNode = new TreeNode(sRoleName);

            objnewChildNode.Text = sRoleName.Trim().Replace("&", "");

            objnewChildNode.Tag = sFunctionName + "#" + Utility.sDbnull(drItems["isTabView"], "0") + "#" + sThamSo + "#" + IRoleID.ToString();
            objnewChildNode.Name = sDllName + "#" + sRoleName + "#" + IRoleID.ToString();
            objnewChildNode.NodeFont = new Font("Tahoma", 9, FontStyle.Regular);
            objnewChildNode.ForeColor = Color.Navy;
            objnewChildNode.ToolTipText = sRoleName;
            objnewChildNode.ImageIndex = 2;
            if(objParentTreeNode ==null)
                objTreeView.Nodes.Add(objnewChildNode);
            else
                objParentTreeNode.Nodes.Add(objnewChildNode);
            return objnewChildNode;
        }
        bool isSingleFunction = false;
        void DisplaySingleFunction(DataRow drSingle)
        {
            string sDllName = "";
            string sRoleName = "";
            string sFunctionName = "";
            string sThamSo = "";
            int IRoleID = Utility.Int32Dbnull(drSingle["iRoleID"], -1);
            string isTabView = Utility.sDbnull(drSingle["isTabView"], "0");
            sDllName = Utility.sDbnull(drSingle["sDLLName"], "");// objSysFunction.SDLLname;
            sFunctionName = Utility.sDbnull(drSingle["sFormName"], "");//objSysFunction.SFormName;
            sThamSo = Utility.sDbnull(drSingle["sParameterList"], "");// objChildSysRole.SParameterList;
            sRoleName = Utility.sDbnull(drSingle["sRoleName"], "");

            string _Tag = sFunctionName + "#" + isTabView + "#" + sThamSo + "#" + IRoleID.ToString();
            string _Name = sDllName + "#" + sRoleName + "#" + IRoleID.ToString();

            string[] arrFunctionName = _Tag.ToString().Split('#');
            string sFormName = arrFunctionName[0];
            string[] arrDllName = _Name.Split('#');
            string DllName = Application.StartupPath + @"\" + arrDllName[0] + ".DLL";
            string _ID = arrFunctionName[3];

            if (File.Exists(DllName))
            {
                if (Utility.Int32Dbnull(arrDllName[2], -1) > -1)
                {
                    new Delete().From(SysScreen.Schema).Where(SysScreen.Columns.UserId).IsEqualTo(
                        globalVariables.UserName)
                        .And(SysScreen.Columns.RoleId).IsEqualTo(Utility.Int32Dbnull(arrDllName[2], -1)).Execute();
                    SysScreen.Insert(Utility.Int32Dbnull(arrDllName[2], -1), globalVariables.UserName, DateTime.Now);
                }

                Assembly assembly = Assembly.LoadFile(DllName);
                Type[] type = assembly.GetTypes();
                foreach (Type objType in type)
                {
                    if (objType.Name.ToUpper() == sFormName.ToUpper())
                    {
                        if (ExistsObject(arrDllName[1])) continue;
                        if (string.IsNullOrEmpty(sThamSo))
                        {
                            object obj = new DynamicInitializer().NewInstance(objType);

                            var form = obj as Form;
                            if (form != null)
                            {
                                if (!dic.ContainsKey(_ID))
                                {
                                    form.Tag = _ID;
                                    if (isTabView.Equals("1"))
                                        form.MdiParent = this;
                                    form.Text = sRoleName;
                                    form.ShowIcon = false;
                                    form.ShowInTaskbar = false;
                                    if (isTabView.Equals("1")) form.Show();
                                    else form.ShowDialog();
                                }
                                {
                                    if (dic.ContainsKey(_ID))
                                    {
                                        UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                        this.ActivateMdiChild(dic[_ID].Form);
                                        globalVariables.FunctionID = Utility.Int32Dbnull(dic[_ID].Form.Tag, -1);
                                        globalVariables.FunctionName = dic[_ID].Text;
                                    }
                                }

                            }
                        }
                        else
                        {
                            object obj = new DynamicInitializer().CreateInstance(objType, new object[] { sThamSo });
                            var form = obj as Form;
                            if (form != null)
                            {
                                if (!dic.ContainsKey(_ID))
                                {
                                    form.Tag = _ID;
                                    if (isTabView.Equals("1"))
                                        form.MdiParent = this;
                                    form.Text = sRoleName;
                                    form.ShowIcon = false;
                                    form.ShowInTaskbar = false;
                                    if (isTabView.Equals("1")) form.Show();
                                    else form.ShowDialog();
                                }
                                else
                                {
                                    if (dic.ContainsKey(_ID))
                                    {
                                        UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                        this.ActivateMdiChild(dic[_ID].Form);
                                        globalVariables.FunctionID = Utility.Int32Dbnull(dic[_ID].Form.Tag, -1);
                                        globalVariables.FunctionName = dic[_ID].Text;
                                    }
                                }
                            }
                        }


                        break;
                    }
                }
            }
        }
        void objTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        void objTreeView_MouseDown(object sender, MouseEventArgs e)
        {
           
            var objTreeView = (TreeView)sender;
            if (objTreeView.SelectedNode == null || !objTreeView.SelectedNode.Equals(objTreeView.GetNodeAt(e.X, e.Y)))
                objTreeView.SelectedNode = objTreeView.GetNodeAt(e.X, e.Y);
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            if (!PropertyLib._AppProperties.Click) return;
            try
            {
                Utility.WaitNow(this);

                //var objTreeView = (TreeView)sender;
                TreeNode objTreeNode = objTreeView.SelectedNode;

                string[] arrFunctionName = objTreeNode.Tag.ToString().Split('#');
                string sFormName = arrFunctionName[0];
                string IsTabView = arrFunctionName[1];
                string sThamSo = arrFunctionName[2];
                string[] arrDllName = objTreeNode.Name.Split('#');
                string DllName = Application.StartupPath + @"\" + arrDllName[0] + ".DLL";
                string _ID = arrFunctionName[3];

                if (File.Exists(DllName))
                {
                    if (Utility.Int32Dbnull(arrDllName[2], -1) > -1)
                    {
                        new Delete().From(SysScreen.Schema).Where(SysScreen.Columns.UserId).IsEqualTo(
                            globalVariables.UserName)
                            .And(SysScreen.Columns.RoleId).IsEqualTo(Utility.Int32Dbnull(arrDllName[2], -1)).Execute();
                        SysScreen.Insert(Utility.Int32Dbnull(arrDllName[2], -1), globalVariables.UserName, DateTime.Now);
                    }
                    Assembly assembly = Assembly.LoadFile(DllName);
                    Type[] type = assembly.GetTypes();
                    foreach (Type objType in type)
                    {
                        if (objType.Name.ToUpper() == sFormName.ToUpper())
                        {
                            if (ExistsObject(arrDllName[1])) continue;
                            if (string.IsNullOrEmpty(sThamSo))
                            {
                                object obj = new DynamicInitializer().NewInstance(objType);

                                var form = obj as Form;
                                if (form != null)
                                {
                                    if (!dic.ContainsKey(_ID))
                                    {
                                        form.Tag = _ID;
                                        if (IsTabView.Equals("1"))
                                            form.MdiParent = this;
                                        form.ShowInTaskbar = false;
                                        form.ShowIcon = false;
                                        form.Text = objTreeNode.Text;
                                        if (IsTabView.Equals("1")) form.Show();
                                        else form.ShowDialog();
                                    }
                                    {

                                        if (dic.ContainsKey(_ID))
                                        {
                                            UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                            this.ActivateMdiChild(dic[_ID].Form);
                                            globalVariables.FunctionID = Utility.Int32Dbnull(dic[_ID].Form.Tag, -1);
                                            globalVariables.FunctionName = dic[_ID].Text;
                                        }
                                        //frm.MdiParent = null;
                                        //frm.Hide();
                                        //frm.ShowDialog();
                                    }

                                }
                            }
                            else
                            {
                                object obj = new DynamicInitializer().CreateInstance(objType, new object[] { sThamSo });
                                var form = obj as Form;
                                if (form != null)
                                {
                                    if (!dic.ContainsKey(_ID))
                                    {
                                        form.Tag = _ID;
                                        if (IsTabView.Equals("1"))
                                            form.MdiParent = this;
                                        form.Text = objTreeNode.Text;
                                        form.ShowIcon = false;
                                        form.ShowInTaskbar = false;
                                        if (IsTabView.Equals("1")) form.Show();
                                        else form.ShowDialog();
                                    }
                                    else
                                    {
                                        if (dic.ContainsKey(_ID))
                                        {
                                            UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                            this.ActivateMdiChild(dic[_ID].Form);
                                            globalVariables.FunctionID = Utility.Int32Dbnull(dic[_ID].Form.Tag, -1);
                                            globalVariables.FunctionName = dic[_ID].Text;
                                        }
                                    }
                                }
                            }


                            break;
                        }
                    }
                }
            }
            catch
            {
                Utility.DefaultNow(this);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }

        void objTreeView_Click(object sender, EventArgs e)
        {

        }
       
        private void CallForm(string pv_sDLLName, string pv_sAssemblyName, string gv_sAnnouncement)
        {
            Assembly sv_oAss;
            bool bExistForm = false;
            Type sv_oAllForm; //Contains All Forms from Assembly
            if (pv_sAssemblyName == "-1") return;
            string sTenDll = Application.StartupPath + string.Format("\\{0}.dll", pv_sAssemblyName);
            if (File.Exists(sTenDll))
            {
                MessageBox.Show("Không tồn tại phân hệ " + pv_sDLLName + ".DLL", gv_sAnnouncement, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                return;
            }

            Assembly LibAss = Assembly.LoadFrom(Application.StartupPath + "\\CommonLibrary.DLL");
            Utility.LoadParamsValues(LibAss);
            //Load Assembly Information from AssemblyName
            sv_oAss = Assembly.LoadFrom(Application.StartupPath + string.Format("\\{0}.DLL", pv_sDLLName));
            Assembly assembly = Assembly.LoadFrom(sTenDll);
            Type type = assembly.GetType(Application.StartupPath + string.Format("\\{0}.DLL", pv_sDLLName));
            var form = (Form)Activator.CreateInstance(type);
            form.ShowDialog();
        }
        bool isLoginFirstTime = true;
        /// <summary>
        /// hàm thực hiện việc đăng nhập lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLogin_Click(object sender, EventArgs e)
        {

            var frm = new frm_Login();
            frm.blnRelogin = !isLoginFirstTime;
            frm.ShowDialog();
            if (!frm.b_Cancel)
            {
                if (globalVariables.LoginSuceess)
                {
                    ClearTab();
                    LoadFormMain();
                }
            }
            else//Nhấn Cancel thì quay lại giao diện cũ không thực hiện gì cả
                if (!isLoginFirstTime) globalVariables.LoginSuceess = true;

        }

        /// <summary>
        /// hàm thực hiện viecj đăng nhập lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdMenuLogout_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn muốn thoát chương trình đang chạy không ?", "Thông báo", true))
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// hàm thực hiện việc gọi sự kiện của phần treeview double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void objTreeView_DoubleClick(object sender, EventArgs eventArgs)
        {
            if (PropertyLib._AppProperties.Click) return;
            var objTreeView = (TreeView)sender;
            //if (objTreeView.SelectedNode == null || !objTreeView.SelectedNode.Equals(objTreeView.GetNodeAt(e.X, e.Y)))
            //    objTreeView.SelectedNode = objTreeView.GetNodeAt(e.X, e.Y);
           // if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            try
            {
                Utility.WaitNow(this);

                //var objTreeView = (TreeView)sender;
                TreeNode objTreeNode = objTreeView.SelectedNode;

                string[] arrFunctionName = objTreeNode.Tag.ToString().Split('#');
                string sFormName = arrFunctionName[0];
                string IsTabView = arrFunctionName[1];
                string sThamSo = arrFunctionName[2];
                string[] arrDllName = objTreeNode.Name.Split('#');
                string DllName = Application.StartupPath + @"\" + arrDllName[0] + ".DLL";
                string _ID = arrFunctionName[3];

                if (File.Exists(DllName))
                {
                    if (Utility.Int32Dbnull(arrDllName[2], -1) > -1)
                    {
                        new Delete().From(SysScreen.Schema).Where(SysScreen.Columns.UserId).IsEqualTo(
                            globalVariables.UserName)
                            .And(SysScreen.Columns.RoleId).IsEqualTo(Utility.Int32Dbnull(arrDllName[2], -1)).Execute();
                        SysScreen.Insert(Utility.Int32Dbnull(arrDllName[2], -1), globalVariables.UserName, DateTime.Now);
                    }
                    Assembly assembly = Assembly.LoadFile(DllName);
                    Type[] type = assembly.GetTypes();
                    foreach (Type objType in type)
                    {
                        if (objType.Name.ToUpper() == sFormName.ToUpper())
                        {
                            if (ExistsObject(arrDllName[1])) continue;
                            if (string.IsNullOrEmpty(sThamSo))
                            {
                                object obj = new DynamicInitializer().NewInstance(objType);

                                var form = obj as Form;
                                if (form != null)
                                {
                                    if (!dic.ContainsKey(_ID))
                                    {
                                        form.Tag = _ID;
                                        if (IsTabView.Equals("1"))
                                            form.MdiParent = this;
                                        form.ShowInTaskbar = false;
                                        form.ShowIcon = false;
                                        form.Text = objTreeNode.Text;
                                        if (IsTabView.Equals("1")) form.Show();
                                        else form.ShowDialog();
                                    }
                                    {

                                        if (dic.ContainsKey(_ID))
                                        {
                                            UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                            this.ActivateMdiChild(dic[_ID].Form);
                                            globalVariables.FunctionID = Utility.Int32Dbnull(dic[_ID].Form.Tag, -1);
                                            globalVariables.FunctionName = dic[_ID].Text;
                                        }
                                        //frm.MdiParent = null;
                                        //frm.Hide();
                                        //frm.ShowDialog();
                                    }

                                }
                            }
                            else
                            {
                                object obj = new DynamicInitializer().CreateInstance(objType, new object[] { sThamSo });
                                var form = obj as Form;
                                if (form != null)
                                {
                                    if (!dic.ContainsKey(_ID))
                                    {
                                        form.Tag = _ID;
                                        if (IsTabView.Equals("1"))
                                            form.MdiParent = this;
                                        form.Text = objTreeNode.Text;
                                        form.ShowIcon = false;
                                        form.ShowInTaskbar = false;
                                        if (IsTabView.Equals("1")) form.Show();
                                        else form.ShowDialog();
                                    }
                                    else
                                    {
                                        if (dic.ContainsKey(_ID))
                                        {
                                            UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                            this.ActivateMdiChild(dic[_ID].Form);
                                            globalVariables.FunctionID = Utility.Int32Dbnull(dic[_ID].Form.Tag, -1);
                                            globalVariables.FunctionName = dic[_ID].Text;
                                        }
                                    }
                                }
                            }


                            break;
                        }
                    }
                }
            }
            catch
            {
                Utility.DefaultNow(this);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }

        private void objmenuStrip_Click(object sender, EventArgs eventArgs)
        {
            try
            {
                Utility.WaitNow(this);

                var objmenuStrip = (ToolStripMenuItem)sender;
                string[] arrFunctionName = objmenuStrip.Tag.ToString().Split('#');
                string sFormName = arrFunctionName[0];
                string IsTabView = arrFunctionName[1];
                string sThamSo = arrFunctionName[2];
                string[] arrDllName = objmenuStrip.Name.Split('#');
                string DllName = Application.StartupPath + @"\" + arrDllName[0] + ".DLL";
                string _ID = arrFunctionName[3];
                if (File.Exists(DllName))
                {
                    if (Utility.Int32Dbnull(arrDllName[2], -1) > -1)
                    {
                        new Delete().From(SysScreen.Schema).Where(SysScreen.Columns.UserId).IsEqualTo(
                            globalVariables.UserName)
                            .And(SysScreen.Columns.RoleId).IsEqualTo(Utility.Int32Dbnull(arrDllName[2], -1)).Execute();
                        SysScreen.Insert(Utility.Int32Dbnull(arrDllName[2], -1), globalVariables.UserName, DateTime.Now);
                    }

                    Assembly assembly = Assembly.LoadFile(DllName);
                    Type[] type = assembly.GetTypes();
                    foreach (Type objType in type)
                    {
                        if (objType.Name.ToUpper() == sFormName.ToUpper())
                        {
                            if (ExistsObject(arrDllName[1])) continue;
                            if (string.IsNullOrEmpty(sThamSo))
                            {
                                object obj = new DynamicInitializer().NewInstance(objType);

                                var form = obj as Form;
                                if (form != null)
                                {
                                    if (!dic.ContainsKey(_ID))
                                    {
                                        form.Tag = _ID;
                                        if (IsTabView.Equals("1"))
                                            form.MdiParent = this;
                                        form.Text = objmenuStrip.Text;
                                        form.ShowIcon = false;
                                        form.ShowInTaskbar = false;
                                        if (IsTabView.Equals("1")) form.Show();
                                        else form.ShowDialog();
                                    }
                                    {

                                        UIMdiChildTab _item = PanelManager.FindMdiTab(dic[_ID].Form);
                                        Form frm = dic[_ID].Form;
                                        this.ActivateMdiChild(dic[_ID].Form);
                                        globalVariables.FunctionID = Utility.Int32Dbnull(_ID, -1);
                                        globalVariables.FunctionName = dic[_ID].Text;
                                    }

                                }
                            }
                            else
                            {
                                object obj = new DynamicInitializer().CreateInstance(objType, new object[] { sThamSo });
                                var form = obj as Form;
                                if (form != null)
                                {
                                    if (!dic.ContainsKey(_ID))
                                    {
                                        form.Tag = _ID;
                                        if (IsTabView.Equals("1"))
                                            form.MdiParent = this;
                                        form.Text = objmenuStrip.Text;
                                        form.ShowIcon = false;
                                        form.ShowInTaskbar = false;
                                        if (IsTabView.Equals("1")) form.Show();
                                        else form.ShowDialog();
                                    }
                                    else
                                    {
                                        this.ActivateMdiChild(dic[_ID].Form);
                                        Form frm = dic[_ID].Form;
                                        this.ActivateMdiChild(dic[_ID].Form);
                                        globalVariables.FunctionID = Utility.Int32Dbnull(_ID, -1);
                                        globalVariables.FunctionName = dic[_ID].Text;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch
            {
                Utility.DefaultNow(this);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }

        //  private void objTreeView_DoubleClick
        /// <summary>
        /// hàm thực hiện kiểm tra thông tin của đối tượng đã tồn tại
        /// </summary>
        /// <param name="sFormName"></param>
        /// <returns></returns>
        public bool ExistsObject(string sFormName)
        {
            bool bFind = false;
            //this.Cursor = Cursors.WaitCursor;
            for (int x = 0; x <= (MdiChildren.Length) - 1; x++)
            {
                Form tempChild = MdiChildren[x];

                if (tempChild.Name == sFormName)
                {
                    tempChild.Activate();
                    bFind = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            return bFind;
        }


        private void frm_MainForm_new_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!fored2Close)
            //    if (!Utility.AcceptQuestion("Bạn có thực sự muốn thoát khỏi chương trình?", "Xác nhận", true))
            //    {
            //        e.Cancel = true;
            //    }
        }

        private void ntfSystemInfo_DoubleClick(object sender, EventArgs e)
        {
            Show();
            ntfSystemInfo.Visible = false;
        }

        /// <summary>
        /// hàm thực hienje viecj thay đổi thông tin mật khẩu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangePass_Click(object sender, EventArgs e)
        {
            var frm = new frm_ChangePass();
            frm.ShowDialog();
        }

        private void PanelManager_PanelAutoHideChanged(object sender, PanelActionEventArgs e)
        {
            WriteHideForm();
        }

        private void WriteHideForm()
        {
            if (File.Exists(sFileName))
            {
                File.WriteAllText(sFileName, pMain.AutoHide ? "1" : "0");
            }
            else
            {
                File.WriteAllText(sFileName, pMain.AutoHide ? "1" : "0");
            }
        }

        private void StatusBar_PanelClick(object sender, StatusBarEventArgs e)
        {
        }

        private void cmdManHinhGhiNho_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// hàm thực hiện khởi tạo thông tin khi datetime
        /// </summary>
        private void IntialDateTime()
        {

            // new HISData().LoadList();
        }


        private void cmdNoiDKBD_Click(object sender, EventArgs e)
        {
            var frm = new frm_DonVi_LamViec();
            frm.ShowDialog();
        }



        private void cmdLogout_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn muốn thoát chương trình đang chạy không ?", "Thông báo", true))
            {
                Application.Exit();
            }
        }

        #region "Khởi tạo thông tin menu system"
        /// <summary>
        /// hàm thực hiện việc cập nhập đơn vị làm việc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDonVi_LamViec_Click(object sender, EventArgs e)
        {
            var frm = new frm_DonVi_LamViec();
            frm.ShowDialog();
        }
        /// <summary>
        /// hàm thực iheenj viec thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExitMenuItem_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion("Bạn có muốn thoát khỏi chương trình không ?", "Thông báo", true))
            {
                Application.Exit();
            }
        }
        /// <summary>
        /// hàm thực hiện đăng nhập lại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdLoginMenuItem_Click(object sender, EventArgs e)
        {

            var frmDangNhap = new frm_Login();
            frmDangNhap.ShowDialog();
        }

        /// <summary>
        /// hàm thực hiện việc thay đổi mật khẩu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangePasswordMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frm_ChangePass();
            frm.ShowDialog();
        }

        #endregion

        #region "Khởi tạo đối tượng"

        private readonly ToolStripMenuItem SystemToolStripMenuItem = new ToolStripMenuItem();
        private readonly ToolStripSeparator ToolStripSeparator1 = new ToolStripSeparator();
        private readonly ToolStripSeparator ToolStripSeparator2 = new ToolStripSeparator();

        private readonly ToolStripSeparator ToolStripSeparator3 = new ToolStripSeparator();
        private readonly ToolStripSeparator ToolStripSeparator4 = new ToolStripSeparator();
        private readonly ToolStripMenuItem cmdChangePassword = new ToolStripMenuItem();
        private readonly ToolStripMenuItem cmdMenuDonVi_LamViec = new ToolStripMenuItem();
        private readonly ToolStripMenuItem cmdMenuExit = new ToolStripMenuItem();
        private readonly ToolStripMenuItem cmdMenuLogin = new ToolStripMenuItem();
        private readonly ToolStripMenuItem cmdMenuLogout = new ToolStripMenuItem();

        #endregion

        #region Nested type: SYSTEMTIME

        public partial struct SYSTEMTIME
        {
            public ushort wDay;
            public ushort wDayOfWeek;
            public ushort wHour;
            public ushort wMilliseconds;
            public ushort wMinute;
            public ushort wMonth;
            public ushort wSecond;
            public ushort wYear;
        }

        #endregion
        List<string> lstTab = new List<string>();
        Dictionary<string, UIMdiChildTab> dic = new Dictionary<string, UIMdiChildTab>();
        private void PanelManager_MdiTabCreated(object sender, MdiTabEventArgs e)
        {
            PropertyLib._AppProperties.CurrentOpenning = Utility.sDbnull(e.Tab.Form.Tag, "-1");
            globalVariables.FunctionID = Utility.Int32Dbnull(e.Tab.Form.Tag, -1);
            globalVariables.FunctionName = e.Tab.Text;
            if (!lstTab.Contains(e.Tab.Form.Tag.ToString())) lstTab.Add(e.Tab.Form.Tag.ToString());
            if (!dic.ContainsKey(e.Tab.Form.Tag.ToString())) dic.Add(e.Tab.Form.Tag.ToString(), e.Tab);
        }

        private void PanelManager_MdiTabClosed(object sender, MdiTabClosedEventArgs e)
        {

            if (lstTab.Contains(e.Tab.Form.Tag.ToString())) lstTab.Remove(e.Tab.Form.Tag.ToString());
            if (dic.ContainsKey(e.Tab.Form.Tag.ToString())) dic.Remove(e.Tab.Form.Tag.ToString());
        }

        private void PanelManager_MdiTabClick(object sender, MdiTabEventArgs e)
        {
            try
            {
                PropertyLib._AppProperties.CurrentOpenning = Utility.sDbnull(e.Tab.Form.Tag, "-1");
                globalVariables.FunctionID =Utility.Int32Dbnull( e.Tab.Form.Tag,-1);
                globalVariables.FunctionName = e.Tab.Text;
            }
            catch
            {
            }
        }

        private void cmdrelogin_Click(object sender, EventArgs e)
        {
            var frm = new frm_Login();
            frm.blnRelogin = !isLoginFirstTime;
            frm.ShowDialog();
            if (!frm.b_Cancel)
            {
                if (globalVariables.LoginSuceess)
                {
                    ClearTab();
                    LoadFormMain();
                }
            }
            else//Nhấn Cancel thì quay lại giao diện cũ không thực hiện gì cả
                if (!isLoginFirstTime) globalVariables.LoginSuceess = true;
        }

        private void cmdChangePWD_Click(object sender, EventArgs e)
        {
            frm_ChangePass _ChangePass = new frm_ChangePass();
            _ChangePass.ShowDialog();
        }
        int _timeCount = 0;
        /// <summary>
        /// Một phút update lại Datetime với máy chủ 1 lần
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                _timeCount += 1000;
                if (_timeCount == 60000)
                {
                    _timeCount = 0;
                    globalVariables.SysDate = THU_VIEN_CHUNG.GetSysDateTime();
                }
                lblTime.Text = "Bây giờ là: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch (Exception ex)
            {
                
            }
            
        }
    }
    public class WS
    {
        public static VNSCore.AWS.LoginWS _AdminWS;
    }
}