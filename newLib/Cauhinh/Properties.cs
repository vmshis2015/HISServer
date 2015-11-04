using System;
using System.ComponentModel;
using System.Drawing;

using System.Xml.Serialization;
using System.IO.Ports;
using System.Drawing.Design;
using System.Windows.Forms;
using System.IO;
using VNS.Libs;
using VNS.Libs.AppType;

namespace VNS.Properties
{
    public class AppProperties
    {

        public AppProperties()
        {
            MenuStype = 1;
            UID = "";
            REM = true;
            PWD = "";
            GridView = false;
            AppName = "HIS";
            AutoLogin = false;
            OpenningList = "";
            AutoOpen = true;
            OpenOnlyCurrent = true;
            CurrentOpenning = "";
            AutoHideHeader = true;
            MenuFont = new Font("Arial", 9, FontStyle.Regular);
            Click = false;
            TabDoubleClick2Close = false;
            CloseAfterPWDChange = true;
            ShowActiveControl = false;
            Makhoathien = "KKB";
            _DepartmentType = AppEnum.DepartmentType.tatca;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
   Description("Loại khoa được phép khám ở máy này"),
   DisplayName("Loại khoa được phép khám ở máy này")]
        public AppEnum.DepartmentType _DepartmentType { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
    Description("Mã khoa thực hiện"),
    DisplayName("Mã khoa thực hiện")]
        public string Makhoathien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
     Description("ShowActiveControl(F11))"),
     DisplayName("ShowActiveControl(F11)")]
        public bool ShowActiveControl { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
     Description("Đóng ngay sau khi thay đổi mật khẩu)"),
     DisplayName("Đóng ngay sau khi thay đổi mật khẩu")]
        public bool CloseAfterPWDChange { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
      Description("true=Click chuột. False= Kích đúp chuột(Double Click)"),
      DisplayName("Mở chức năng bằng cách Click chuột")]
        public bool Click { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
       Description("Tắt chức năng bằng cách kích đúp chuột vào Tab đang mở"),
       DisplayName("Tắt chức năng bằng cách kích đúp chuột vào Tab đang mở")]
        public bool TabDoubleClick2Close { get; set; }

        [Browsable(false), ReadOnly(false), Category("Cấu hình ứng dụng"),
     Description("CurrentOpenning"),
     DisplayName("CurrentOpenning")]
        public string CurrentOpenning { get; set; }

        [Browsable(false), ReadOnly(false), Category("Cấu hình ứng dụng"),
      Description("OpenningList"),
      DisplayName("OpenningList")]
        public string OpenningList { get; set; }

        [Browsable(false), ReadOnly(false), Category("Cấu hình ứng dụng"),
       Description("App Name"),
       DisplayName("App Name")]
        public string AppName { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
       Description("MenuStype"),
       DisplayName("MenuStype")]
        public int MenuStype { get; set; }
        [Browsable(false), ReadOnly(false), Category("Cấu hình ứng dụng"),
        Description("UID"),
        DisplayName("UID")]
        public string UID { get; set; }
        [Browsable(false), ReadOnly(false), Category("Cấu hình ứng dụng"),
        Description("PWD"),
        DisplayName("PWD")]
        public string PWD { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
        Description("Remember me"),
        DisplayName("Remember me")]
        public bool REM { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
       Description("Kê đơn thuốc dùng lưới"),
       DisplayName("Kê đơn thuốc dùng lưới")]
        public bool GridView { get; set; }

        [Browsable(false), ReadOnly(false), Category("Cấu hình ứng dụng"),
     Description("AutoLogin"),
     DisplayName("AutoLogin")]
        public bool AutoLogin { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
    Description("AutoOpen"),
    DisplayName("AutoOpen")]
        public bool AutoOpen { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
  Description("AutoHide App Header"),
  DisplayName("AutoHide App Header")]
        public bool AutoHideHeader { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
   Description("OpenOnlyCurrent"),
   DisplayName("OpenOnlyCurrent")]
        public bool OpenOnlyCurrent { get; set; }
        [XmlIgnore]
        [Browsable(true), ReadOnly(false), Category("Schedule color settings"),
         Description("MenuFont"),
         DisplayName("MenuFont")]
        public Font MenuFont { get; set; }

        [Browsable(false)]
        [XmlElement(" MenuFontName")]
        public string MenuFontName
        {
            get
            {
                return MenuFont.Name;
            }
            set
            {
                MenuFont = new Font(value, MenuFontsize, MenuFontstyle);
            }
        }

        [Browsable(false)]
        [XmlElement(" MenuFontsize")]
        public float MenuFontsize
        {
            get
            {
                return MenuFont.Size;
            }
            set
            {
                MenuFont = new Font(MenuFontName, value, MenuFontstyle);
            }
        }

        [Browsable(false)]
        [XmlElement(" MenuFontstyle")]
        public FontStyle MenuFontstyle
        {
            get
            {
                return MenuFont.Style;
            }
            set
            {
                MenuFont = new Font(MenuFontName, MenuFontsize, value);
            }
        }
    }
    public class NoitruProperties
    {

        public NoitruProperties()
        {
            InphieuNhapvienNgaysaukhiNhapvien = true;
            ThoatNgaysaukhiNhapvienthanhcong = true;
            
            Songaysaochep = 0;
            Saochepngaytruocdo = true;
            HienthithuocControngkho = true;
            ViewOnClick = false;
            Hienthitatcaphieudieutrikhisaochep = true;
            Songayhienthi = -1;
            SaochepVTTH = false;
            SaochepThuoc = false;
            SaochepYLenh = false;
            SaochepCLS = false;
            SaochepDinhDuong = false;
            InsaukhiLuu = true;
        }
        [Browsable(false), ReadOnly(false), Category("Cấu hình tạm ứng"),
 Description("In sau khi lưu"),
 DisplayName("In sau khi lưu")]
        public bool InsaukhiLuu { get; set; }

        [Browsable(false), ReadOnly(false), Category("Cấu hình sao chép"),
  Description("Sao chép VTTH"),
  DisplayName("Sao chép VTTH")]
        public bool SaochepVTTH { get; set; }

        [Browsable(false), ReadOnly(false), Category("Cấu hình sao chép"),
  Description("Sao chép thuốc"),
  DisplayName("Sao chép thuốc")]
        public bool SaochepThuoc { get; set; }
        [Browsable(false), ReadOnly(false), Category("Cấu hình sao chép"),
  Description("Sao chép Y lệnh"),
  DisplayName("Sao chép Y lệnh")]
        public bool SaochepYLenh { get; set; }
        [Browsable(false), ReadOnly(false), Category("Cấu hình sao chép"),
  Description("Sao chép CLS"),
  DisplayName("Sao chép CLS")]
        public bool SaochepCLS { get; set; }
        [Browsable(false), ReadOnly(false), Category("Cấu hình sao chép"),
  Description("Sao chép phiếu dinh dưỡng"),
  DisplayName("Sao chép phiếu dinh dưỡng")]
        public bool SaochepDinhDuong { get; set; }

     

       

        [Browsable(true), ReadOnly(false), Category("Cấu hình Phiếu điều trị"),
Description("Số ngày điều trị hiển thị trên lưới Phiếu điều trị kể từ ngày hiện tại trở về trước(<=0 nghĩa là hiển thị tất cả)"),
DisplayName("Số ngày hiển thị phiếu điều trị")]
        public int Songayhienthi { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình Phiếu điều trị"),
 Description("Hiển thị tất cả phiếu điều trị của bệnh nhân khi tìm kiếm phiếu điều trị trong chức năng sao chép. Bạn có thể chọn/hủy mục Hiển thị tất cả trong chức năng sap chép phiếu điều trị"),
 DisplayName("Hiển thị tất cả phiếu điều trị khi sao chép")]
        public bool Hienthitatcaphieudieutrikhisaochep { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình Phiếu điều trị"),
   Description("Hiển thị thông tin phiếu điều trị ngay sau khi chọn trên lưới không cần bấm vào nút Hiển thị(hình kính lúp)"),
   DisplayName("Hiển thị thông tin phiếu điều trị ngay sau khi chọn")]
        public bool ViewOnClick { get; set; }
       


        [Browsable(true), ReadOnly(false), Category("Cấu hình Phiếu điều trị"),
     Description("true=Chỉ hiển thị thuốc còn trong kho để sao chép"),
     DisplayName("Chỉ hiển thị thuốc còn trong kho để sao chép")]
        public bool HienthithuocControngkho { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình Phiếu điều trị"),
      Description("Số ngày sao chép khi sử dụng menu sao chép cls+đơn thuốc(Nếu để giá trị=0 có nghĩa là sẽ hiển thị tất cả các ngày điều trị khác ngày đang chọn)"),
      DisplayName("Số ngày sao chép khi sử dụng menu sao chép cls+đơn thuốc")]
        public int Songaysaochep { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình Phiếu điều trị"),
     Description("true=Chỉ sao chép các ngày trước ngày lập phiếu đang chọn. False=Không phân biệt ngày trước hoặc sau"),
     DisplayName("Chỉ sao chép các ngày trước ngày lập phiếu đang chọn")]
        public bool Saochepngaytruocdo { get; set; }

       

        [Browsable(true), ReadOnly(false), Category("Cấu hình nội trú"),
        Description("Tự đôngk in phiếu nhập viện ngay sau khi nhập viện tại chức năng thăm khám"),
        DisplayName("In phiếu nhập viện ngay sau khi nhập viện")]
        public bool InphieuNhapvienNgaysaukhiNhapvien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình nội trú"),
       Description("Thoát form nhập viện ngay sau khi nhấn nút nhập viện thành công"),
       DisplayName("Thoát form nhập viện ngay sau khi nhấn nút nhập viện thành công")]
        public bool ThoatNgaysaukhiNhapvienthanhcong { get; set; }
    }

    public class QuaythuocProperties
    {

        public QuaythuocProperties()
        {
            Tudongthanhtoan = false;
            Themmoilientuc = false;
            TudonginBienlai = true;
            BoquaChidanthem = true;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy thuốc"),
   Description("Thanh toán ngay sau khi ghi"),
   DisplayName("Thanh toán ngay sau khi ghi")]
        public bool Tudongthanhtoan { get; set; }

          [Browsable(true), ReadOnly(false), Category("Cấu hình quầy thuốc"),
   Description("In biên lai ngay sau khi thanh toán"),
   DisplayName("In biên lai ngay sau khi thanh toán")]
        public bool TudonginBienlai { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy thuốc"),
  Description("Thêm mới liên tục"),
  DisplayName("Thêm mới liên tục")]
        public bool Themmoilientuc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy thuốc"),
  Description("Bỏ qua không nhảy vào mục Chỉ dẫn thêm"),
  DisplayName("Bỏ qua không nhảy vào mục Chỉ dẫn thêm")]
        public bool BoquaChidanthem { get; set; }
    }

    public class BenhAnProperties
    {

        public BenhAnProperties()
        {
            TudongInngaysaukhiLuu = true;
            Tudongthoatformngaysaukhiluu = true;
            
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình Bệnh án"),
   Description("Tự động in bệnh án ngay sau khi lưu"),
   DisplayName("In ngay sau khi lưu")]
        public bool TudongInngaysaukhiLuu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình Bệnh án"),
 Description("Tự động thoát khỏi chức năng ngay sau khi lưu"),
 DisplayName("Thoát khỏi chức năng ngay sau khi lưu")]
        public bool Tudongthoatformngaysaukhiluu { get; set; }


    }
    public class DynamicInputProperties
    {

        public DynamicInputProperties()
        {
            DynamicSize = new Size(378, 35);
            LabelSize = new Size(257, 35);
            TextSize = new Size(121, 22);

            RtfDynamicSize = new Size(378, 35);
            RtfLabelSize = new Size(257, 35);
            RtfTextSize = new Size(121, 22);

        }
        [Browsable(true), ReadOnly(false), Category("DynamicInput Properties"),
   Description("DynamicSize"),
   DisplayName("Kích thước đối tượng")]
        public Size DynamicSize { get; set; }

        [Browsable(true), ReadOnly(false), Category("DynamicInput Properties"),
  Description("LabelSize"),
  DisplayName("Kích thước mô tả")]
        public Size LabelSize { get; set; }

        [Browsable(true), ReadOnly(false), Category("DynamicInput Properties"),
  Description("TextSize"),
  DisplayName("Kích thước giá trị")]
        public Size TextSize { get; set; }

        [Browsable(true), ReadOnly(false), Category("RichtextBox DynamicInput Properties"),
  Description("DynamicSize"),
  DisplayName("Kích thước đối tượng")]
        public Size RtfDynamicSize { get; set; }

        [Browsable(true), ReadOnly(false), Category("RichtextBox DynamicInput Properties"),
  Description("LabelSize"),
  DisplayName("Kích thước mô tả ")]
        public Size RtfLabelSize { get; set; }

        [Browsable(true), ReadOnly(false), Category("RichtextBox DynamicInput Properties"),
  Description("TextSize"),
  DisplayName("Kích thước giá trị ")]
        public Size RtfTextSize { get; set; }

    }
    public class ConfigProperties
    {

        public ConfigProperties()
        {
            DataBaseServer = "192.168.1.254";
            DataBaseName = "PACS";
            UID = "sa";
            PWD = "123456";
            ORM = "ORM";
            MaKhoa = "KKB";
            Maphong = "101";
            Somayle = "12345678";
            MaDvi = "HIS";
            Min = 0;
            Max = 1000;
            HIS_AppMode = AppEnum.AppMode.License;
            HIS_HardKeyType = AppEnum.HardKeyType.SOFTKEY;
            RunUnderWS = true;
            WSURL = "http://localhost:1695/AdminWS.asmx";
        }
        [Browsable(true), ReadOnly(false), Category("Webservice settings"),
Description("Địa chỉ Webservice"),
DisplayName("Địa chỉ Webservice")]
        public string WSURL { get; set; }
        [Browsable(true), ReadOnly(false), Category("Webservice settings"),
 Description("true=Kết nối qua Webservice để nhận chuỗi kết nối chung và kiểm tra giấy phép sử dụng trên máy chủ CSDL. False = Từng máy đăng ký và tự cấu hình vào CSDL"),
 DisplayName("Kết nối qua Webservice")]
        public bool RunUnderWS { get; set; }

        [Browsable(true), ReadOnly(false), Category("HIS License settings"),
  Description("Kiểu giấy phép"),
  DisplayName("HIS License Type")]
        public AppEnum.HardKeyType HIS_HardKeyType { get; set; }
        [Browsable(true), ReadOnly(false), Category("HIS License settings"),
Description("Kiểu ứng dụng"),
DisplayName("HIS Application Mode")]
        public AppEnum.AppMode HIS_AppMode { get; set; }
        [Browsable(true), ReadOnly(false), Category("Department Settings"),
   Description("Số mã bệnh phẩm nhỏ nhất khi bác sĩ chỉ định CLS"),
   DisplayName("số mã bệnh phẩm nhỏ nhất")]
        public int Min { get; set; }
        [Browsable(true), ReadOnly(false), Category("Department Settings"),
   Description("Số mã bệnh phẩm lớn nhất khi bác sĩ chỉ định CLS"),
   DisplayName("Số mã bệnh phẩm lớn nhất")]
        public int Max { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
    Description("Mã đơn vị(Bệnh viện) sử dụng phần mềm"),
    DisplayName("Mã đơn vị thực hiện")]
        public string MaDvi { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
     Description("Mã khoa đang sử dụng hệ thống phần mềm"),
     DisplayName("Mã khoa thực hiện")]
        public string MaKhoa { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
     Description("Mã phòng đang sử dụng hệ thống phần mềm"),
     DisplayName("Mã phòng thực hiện")]
        public string Maphong { get; set; }

        [Browsable(true), ReadOnly(false), Category("Department Settings"),
     Description("Số máy lẻ của khoa sử dụng"),
     DisplayName("Số máy lẻ khoa sử dụng")]
        public string Somayle { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
     Description("DataBase Server"),
     DisplayName("DataBase Server")]
        public string DataBaseServer { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
       Description("DataBase Name"),
       DisplayName("DataBase Name")]
        public string DataBaseName { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
     Description("DataBase User"),
     DisplayName("DataBase User")]
        public string UID { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
     Description("DataBase Password"),
     DisplayName("DataBase Password")]
        public string PWD { get; set; }

        [Browsable(true), ReadOnly(false), Category("DataBase Settings"),
    Description("ProviderName"),
    DisplayName("ProviderName")]
        public string ORM { get; set; }

    }
    public class FTPProperties
    {
        public FTPProperties()
        {
            IPAddress = "192.168.1.254";
            UID ="user";
            PWD = "123456";
            UNCPath=@"\\192.168.1.254\PACSImage";
            IamLocal = false;
            Push2FTP = false;
            ImageFolder = Application.StartupPath + @"\ImageSA";
            PrintAfterSave = false;
            PrintPreview = true;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình FTP"),
 Description("In ngay sau khi lưu"),
 DisplayName("In ngay sau khi lưu")]
        public bool PrintAfterSave { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình FTP"),
  Description("Xem trước khi in"),
  DisplayName("Xem trước khi in")]
        public bool PrintPreview { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình FTP"),
   Description("IPAddress"),
   DisplayName("IPAddress")]
        public string IPAddress { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình FTP"),
   Description("UID"),
   DisplayName("UID")]
        public string UID { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình FTP"),
   Description("PWD"),
   DisplayName("PWD")]
        public string PWD { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình FTP"),
   Description("Thư mục share chứa ảnh"),
   DisplayName("UNCPath")]
        public string UNCPath { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình FTP"),
   Description("Thư mục chứa ảnh máy Local dùng để chứa ảnh Download từ FTP"),
   DisplayName("Local Image Forlder")]
        public string ImageFolder { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình FTP"),
   Description("Sử dụng FTP để nạp ảnh"),
   DisplayName("Sử dụng FTP để nạp ảnh")]
        public bool IamLocal { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình FTP"),
  Description("Đẩy ảnh lên FTP"),
  DisplayName("Đẩy ảnh lên FTP")]
        public bool Push2FTP { get; set; }

    }
    public class QheGiaCLSProperties
    {

        public QheGiaCLSProperties()
        {
            TudongDieuChinhGiaPTTT = false;
            TudongDieuChinhGiaDichVu = false;
            TudongDieuChinhGiaBHYT = true;
            TieudeBaocaoGiaCls = "GIÁ DỊCH VỤ CẬN LÂM SÀNG";
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình giá CLS"),
   Description("Tiêu đề in giá CLS"),
   DisplayName("Tiêu đề in giá CLS")]
        public string TieudeBaocaoGiaCls { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình giá CLS"),
       Description("Tự động điều chỉnh giá. Giá phụ thu trái tuyến BHYT=Giá DV-Giá BHYT"),
       DisplayName("Tự điều chỉnh giá phụ thu trái tuyến")]
        public bool TudongDieuChinhGiaPTTT { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình giá CLS"),
      Description("Tự động điều chỉnh giá Dịch vụ. Giá Dịch vụ của một dịch vụ trong toàn viện là giống nhau nếu chỉnh sửa giá tại bất kỳ khoa khám chữa bệnh nào"),
      DisplayName("Tự điều chỉnh giá Dịch vụ")]
        public bool TudongDieuChinhGiaDichVu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình giá CLS"),
      Description("Tự động điều chỉnh giá BHYT. Giá BHYT của một dịch vụ trong toàn viện là giống nhau nếu chỉnh sửa giá tại bất kỳ khoa khám chữa bệnh nào"),
      DisplayName("Tự điều chỉnh giá BHYT")]
        public bool TudongDieuChinhGiaBHYT { get; set; }
       
    }
      public class QheGiaThuocProperties
    {

          public QheGiaThuocProperties()
        {
            TudongDieuChinhGiaPTTT = false;
            TudongDieuChinhGiaDichVu = false;
            TudongDieuChinhGiaBHYT = true;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
       Description("Tự động điều chỉnh giá. Giá phụ thu trái tuyến BHYT=Giá DV-Giá BHYT"),
       DisplayName("Tự điều chỉnh giá phụ thu trái tuyến")]
        public bool TudongDieuChinhGiaPTTT { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
      Description("Tự động điều chỉnh giá Dịch vụ. Giá Dịch vụ của một thuốc trong toàn viện là giống nhau nếu chỉnh sửa giá tại bất kỳ khoa khám chữa bệnh nào"),
      DisplayName("Tự điều chỉnh giá Dịch vụ")]
        public bool TudongDieuChinhGiaDichVu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình ứng dụng"),
      Description("Tự động điều chỉnh giá BHYT. Giá BHYT của một thuốc trong toàn viện là giống nhau nếu chỉnh sửa giá tại bất kỳ khoa khám chữa bệnh nào"),
      DisplayName("Tự điều chỉnh giá BHYT")]
        public bool TudongDieuChinhGiaBHYT { get; set; }
       
    }

    public class HISBaoBenhAnProperties1
    {
        private const string PhieuChamSoc = "Cấu hình in chăm sóc và theo dõi";
        private const string PhieuRaVien = "Cấu hình in phiếu ra viện";
        private const string PhieuDieuTri = "Cấu hình in phiếu điều trị";
        public HISBaoBenhAnProperties1()
        {
            PathPhieuChamSoc = string.Format("{0}{1}", globalVariables.gv_strReportFolder, "CRP_YTa_PhieuChamSocRpt.rpt");
            TieuDePhieuChamSoc = "PHIẾU CHĂM SÓC";
            PathPhieuRaVien = string.Format("{0}{1}", globalVariables.gv_strReportFolder, "CRPT_GIAYRAVIEN.rpt");
            PathPhieuDieuTri = string.Format("{0}{1}", globalVariables.gv_strReportFolder, "crpt_PhieuDieuTri.rpt");

        }
        [Browsable(true), ReadOnly(false), Category(PhieuChamSoc),
       Description("Tiêu đề phiếu chăm sóc"),
       DisplayName("Tiêu đề phiếu chăm sóc")]
        public string TieuDePhieuChamSoc { get; set; }
        [Browsable(true), ReadOnly(false), Category(PhieuChamSoc),
        Description(@"Đường dẫn báo cáo phiếu chăm sóc"),
        DisplayName(@"Đường dẫn báo cáo phiếu chăm sóc"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathPhieuChamSoc { get; set; }

        [Browsable(true), ReadOnly(false), Category(PhieuRaVien),
     Description(@"Đường dẫn báo cáo phiếu ra viện"),
     DisplayName(@"Đường dẫn báo cáo phiếu ra viện"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathPhieuRaVien { get; set; }

        [Browsable(true), ReadOnly(false), Category(PhieuDieuTri),
  Description(@"Đường dẫn báo cáo phiếu điều trị"),
  DisplayName(@"Đường dẫn báo cáo phiếu in điều trị"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathPhieuDieuTri { get; set; }



    }

    public class HISBaoCaoBHYTProperties1
    {
        private const string CategroiesName21A = "Cấu hình báo cáo bảo hiểm 21A";
        private const string CategroiesName20A = "Cấu hình báo cáo bảo hiểm 20A";
        private const string CategroiesName14A = "Cấu hình báo cáo bảo hiểm 14A";
        private const string CategroiesName14B = "Cấu hình báo cáo bảo hiểm 14B";
        private const string CategroiesName25A = "Cấu hình báo cáo bảo hiểm 25A";
        private const string CategroiesName05A = "Cấu hình báo cáo bảo hiểm 05A";
        private const string CategroiesName79A = "Cấu hình báo cáo bảo hiểm 79A";
        private const string CategroiesName80A = "Cấu hình báo cáo bảo hiểm 80A";
        private const string PhoiBaoHiem = "Cấu hình in phôi bảo hiểm";
        private const string PhoiTamUng = "Cấu hình in phôi tạm ứng";
        private const string LaoKhoaBaoHiemYTe = "Cấu hình báo cáo bảo hiểm cho lão khoa";
        public HISBaoCaoBHYTProperties1()
        {
            PathBHYT_21A = string.Format("{0}{1}", globalVariables.gv_strReportFolder, "KYDONG_CRPT_TKE_THOP_SDUNG_DVKT.rpt");
            PathBHYT_79A_TH = string.Format("{0}{1}", globalVariables.gv_strReportFolder, "crpt_BaoCao79a-hd_LaoKhoa_TongHop.rpt");
            PathBHYT_79A_CT = string.Format("{0}{1}", globalVariables.gv_strReportFolder, "crpt_BaoCao79a-hd_LaoKhoa.rpt");



        }
        #region "Cấu hình báo cáo của bảo hiểm chuẩn"
        [Browsable(true), ReadOnly(false), Category(CategroiesName21A),
         Description(@"Đường dẫn báo cáo bảo hiểm 20A"),
         DisplayName(@"Đường dẫn 20A"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_20A { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName20A),
         Description("Tiêu đề báo cáo 20A"),
         DisplayName("Tiêu đề báo cáo 20A")]
        public string TieuDeBHYT_20A { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName21A),
         Description(@"Đường dẫn báo cáo bảo hiểm 21A"),
         DisplayName(@"Đường dẫn 21A"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_21A { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName21A),
         Description("Tiêu đề báo cáo 21A"),
         DisplayName("Tiêu đề báo cáo 21A")]
        public string TieuDeBHYT_21A { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName14A),
         Description(@"Đường dẫn báo cáo bảo hiểm 14A chi tiết"),
         DisplayName(@"Đường dẫn 14A chi tiết"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_14A { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName14A),
         Description("Tiêu đề báo cáo 14A"),
         DisplayName("Tiêu đề báo cáo 14A")]
        public string TieuDeBHYT_14A { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName14B),
         Description(@"Đường dẫn báo cáo bảo hiểm 14A tổng hợp"),
         DisplayName(@"Đường dẫn 14A tổng hợp"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_14B { get; set; }
        [Browsable(true), ReadOnly(false), Category(CategroiesName14B),
        Description("Tiêu đề báo cáo 14B"),
        DisplayName("Tiêu đề báo cáo 14B")]
        public string TieuDeBHYT_14B { get; set; }



        [Browsable(true), ReadOnly(false), Category(CategroiesName25A),
         Description(@"Đường dẫn báo cáo bảo hiểm 25A chi tiết"),
         DisplayName(@"Đường dẫn 25A chi tiết"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_25_CT { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName14A),
         Description("Tiêu đề báo cáo 25 chi tiết"),
         DisplayName("Tiêu đề báo cáo 25 chi tiết")]
        public string TieuDeBHYT_25A_CT { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName25A),
         Description(@"Đường dẫn báo cáo bảo hiểm 25A tổng hợp"),
         DisplayName(@"Đường dẫn 25A tổng hợp"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_25_TH { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName25A),
         Description("Tiêu đề báo cáo 25A tổng hợp"),
         DisplayName("Tiêu đề báo cáo 25A tổng hợp")]
        public string TieuDeBHYT_25A_TH { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName05A),
         Description(@"Đường dẫn báo cáo bảo hiểm 05A chi tiết"),
         DisplayName(@"Đường dẫn 05A chi tiết"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_05_CT { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName05A),
         Description("Tiêu đề báo cáo 05A chi tiết"),
         DisplayName("Tiêu đề báo cáo 05A chi tiết")]
        public string TieuDeBHYT_05A_CT { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName05A),
         Description(@"Đường dẫn báo cáo bảo hiểm 05A tổng hợp"),
         DisplayName(@"Đường dẫn 05A tổng hợp"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_05_TH { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName05A),
         Description("Tiêu đề báo cáo 05A tổng hợp"),
         DisplayName("Tiêu đề báo cáo 05A tổng hợp")]
        public string TieuDeBHYT_05A_TH { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName79A),
         Description(@"Đường dẫn báo cáo bảo hiểm 79A chi tiết"),
         DisplayName(@"Đường dẫn 79A chi tiết"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_79A_CT { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName79A),
         Description("Tiêu đề báo cáo 79A chi tiết"),
         DisplayName("Tiêu đề báo cáo 79A chi tiết")]
        public string TieuDeBHYT_79A_CT { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName79A),
       Description(@"Đường dẫn báo cáo bảo hiểm 79A tổng hợp"),
       DisplayName(@"Đường dẫn 79A tổng hợp"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_79A_TH { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName79A),
         Description("Tiêu đề báo cáo 79A tổng hợp"),
         DisplayName("Tiêu đề báo cáo 79A tổng hợp")]
        public string TieuDeBHYT_79A_TH { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName80A),
         Description(@"Đường dẫn báo cáo bảo hiểm 80A"),
         DisplayName(@"Đường dẫn 80A tổng hợp"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_80A_TH { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName80A),
         Description("Tiêu đề báo cáo 80A tổng hợp"),
         DisplayName("Tiêu đề báo cáo 80A tổng hợp")]
        public string TieuDeBHYT_80A_TH { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName80A),
         Description(@"Đường dẫn báo cáo bảo hiểm 80A"),
         DisplayName(@"Đường dẫn 80A chi tiết"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_80A_CT { get; set; }

        [Browsable(true), ReadOnly(false), Category(CategroiesName80A),
         Description("Tiêu đề báo cáo 80A chi tiết"),
         DisplayName("Tiêu đề báo cáo 80A chi tiết")]
        public string TieuDeBHYT_80A_CT { get; set; }
        #endregion




        #region "cấu hình báo cáo bảo hiểm của đơn vị lão khoa"
        [Browsable(true), ReadOnly(false), Category(LaoKhoaBaoHiemYTe),
          Description(@"Đường dẫn báo cáo bảo hiểm đồng chi trả (frm_BAOCAO_THUTIEN_DONG_CHITRA)"),
          DisplayName(@"Đường dẫn bảo hiểm đồng chi trả"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_DCT { get; set; }

        [Browsable(true), ReadOnly(false), Category(LaoKhoaBaoHiemYTe),
         Description(@"Đường dẫn báo cáo bảo hiểm đồng chi trả và chênh lệch (frm_BAOCAO_THUTIEN_DONGCHITRA_CHECHLECH)"),
         DisplayName(@"Đường dẫn bảo hiểm đồng chi trả"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_DCT_CL { get; set; }

        [Browsable(true), ReadOnly(false), Category(LaoKhoaBaoHiemYTe),
        Description(@"Đường dẫn báo cáo bảo hiểm đồng chi trả và chênh lệch tổng hợp (frm_BAOCAO_THUTIEN_DONGCHITRA_CHECHLECH_TONGHOP)"),

        DisplayName(@"Đường dẫn bảo hiểm đồng chi trả tổng hợp"), Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string PathBHYT_DCT_CL_TongHop { get; set; }
        #endregion

    }

    public class HISBaoCaoProperties1
    {
        public HISBaoCaoProperties1()
        {
            BaoCao79A = true;
            BaoCao80A = true;
            BaoCao25A = true;
            BaoCao80A_TH = true;
            PathHoanKyQuiChiTiet = string.Format("{0}{1}", globalVariables.gv_strReportFolder, "CRPT_BAOCAO_TAMUNG_CHITIET.rpt");
            PathKyQuiTongHop = string.Format("{0}{1}", globalVariables.gv_strReportFolder, "CRPT_BAOCAO_TAMUNG_TONGHOP.rpt");
            PathHoanKyQuiChiTiet = string.Format("{0}{1}", globalVariables.gv_strReportFolder, "CRPT_BAOCAO_HOANUNG_CHITIET.rpt");
            PathHoanKyQuiTongHop = string.Format("{0}{1}", globalVariables.gv_strReportFolder, "CRPT_BAOCAO_HOANUNG_TONGHOP.rpt");
        }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Báo Cáo 79A "),
         Description("Lấy xuất File Excel của báo cáo 79A"),
         DisplayName("Xuất Excel báo cáo 79A")]
        public bool BaoCao79A { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Báo Cáo 80A "),
         Description("Lấy xuất File Excel của báo cáo 80A"),
         DisplayName("Xuất Excel báo cáo 80A")]
        public bool BaoCao80A { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Báo Cáo 80A "),
         Description("Lấy xuất File Excel của báo cáo 80A tổng hợp"),
         DisplayName("Xuất Excel báo cáo 80A tổng hợp")]
        public bool BaoCao80A_TH { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Báo Cáo 25A "),
         Description("Lấy xuất File Excel của báo cáo 25A"),
         DisplayName("Xuất Excel báo cáo 25A")]
        public bool BaoCao25A { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Báo thống kê ký quĩ "),
         Description("đường dẫn báo cáo ký quĩ"),
         DisplayName("Đường dẫn báo cáo chi tiết ")]
        public string PathKyQuiChiTiet { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Báo thống kê ký quĩ "),
         Description("đường dẫn báo cáo ký quĩ"),
         DisplayName("Đường dẫn báo cáo tổng hợp ")]
        public string PathKyQuiTongHop { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu Hình Báo thống kê hoàn ký quĩ "),
     Description("đường dẫn báo cáo hoàn ký quĩ"),
     DisplayName("Đường dẫn báo cáo hoàn ký quĩ chi tiết ")]
        public string PathHoanKyQuiChiTiet { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Báo thống kê hoàn ký quĩ "),
         Description("đường dẫn báo cáo hoàn  ký quĩ"),
         DisplayName("Đường dẫn báo cáo tổng hợp hoàn ký quĩ ")]
        public string PathHoanKyQuiTongHop { get; set; }
    }

    public class FileLocationEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (var ofd = new OpenFileDialog())
            {
                string initialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ofd.InitialDirectory = initialDirectory;
                ofd.RestoreDirectory = true;
                // set file filter info here
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string editValue = ofd.FileName;
                    editValue = GetRelativePath(editValue, initialDirectory);
                    return editValue;
                }
            }
            return value;
        }

        private string GetRelativePath(string filespec, string folder)
        {
            var pathUri = new Uri(filespec);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            var folderUri = new Uri(folder);
            return
                Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri)
                    .ToString()
                    .Replace('/', Path.DirectorySeparatorChar));
        }
    }

    public class HISCauHinhPhanBuongGiuongProperties1
    {
        public HISCauHinhPhanBuongGiuongProperties1()
        {
            ChonPhongBenh = false;
            ChonGiuongBenh = false;
            IsLocTheoKhoa = true;
            IsHienthiKhoaNguoiDung = false;
            XemTatca = false;
            GioiGianTinhTrongNgay = 5;
            IsTrongGiuongLenDau = true;
            IsTrongPhongLenDau = true;
            IsLocPhieuDaIn = false;
            IsLocLichSuTheoKhoa = false;
            IsTongHopPhieuDieuTri = false;
        }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình phần buồng giường "),
         Description("Cho phép hiển thị theo khoa đấy"),
         DisplayName("Chỉ hiển thị theo khoa người đăng nhập")]
        public bool IsHienthiKhoaNguoiDung { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển khoa "),
         Description("Bắt buộc phải chọn phòng khi chuyển khoa"),
         DisplayName("Bắt buộc phải chọn phòng khi chuyển khoa")]
        public bool ChonPhongBenh { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển khoa "),
         Description("bắt buộc phải chọn giường luôn lúc chuyển khoa"),
         DisplayName("Bắt buộc chọn giường lúc chuyển khoa")]
        public bool ChonGiuongBenh { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình phiếu điều trị "),
         Description("Lọc thông tin theo khoa"),
         DisplayName("Lọc thông tin theo khoa")]
        public bool IsLocTheoKhoa { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình phiếu điều trị "),
        Description("Tổng hợp phiếu điều trị"),
        DisplayName("Tổng hợp phiếu điều trị")]
        public bool IsTongHopPhieuDieuTri { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình phiếu điều trị "),
       Description("Lọc thông tin phiếu điều trị đã in "),
       DisplayName("Lọc thông tin phiếu điều trị  đã in ")]
        public bool IsLocPhieuDaIn { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình phiếu điều trị "),
     Description("Lọc thông tin lịch sử bệnh án theo khoa "),
     DisplayName("Lọc thông tin lịch sử bệnh án theo khoa ")]
        public bool IsLocLichSuTheoKhoa { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình phiếu điều trị "),
       Description("Trong gói phụ thu =0 "),
       DisplayName("Trong gói phụ thu sẽ nhận giá trị =0")]
        public bool IsPhuThuTrongGoi { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình phiếu điều trị "),
         Description("Xem phiếu điều trị trong tất cả các ngày=true. Xem của ngày hiện tại=False"),
         DisplayName("Xem tất cả")]
        public bool XemTatca { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển giường "),
         Description("Chuyển giường trong thời gian nào thì tính 1 ngày, mặc định 5 tiếng"),
         DisplayName("Thời gian vào giường tính 1 ngày")]
        public int GioiGianTinhTrongNgay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển giường "),
         Description("Tự động sắp xếp giường trống lên đầu"),
         DisplayName("Tự động sắp xếp trống giường lên đầu")]
        public bool IsTrongGiuongLenDau { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển phòng "),
         Description("Tự động sắp xếp phòng trống lên đầu"),
         DisplayName("Tự động sắp xếp trống phòng lên đầu")]
        public bool IsTrongPhongLenDau { get; set; }
    }
    public class HISCLSProperties
    {
        public HISCLSProperties()
        {
          
            IsChuyenCLS = true;
            MaDTTuDong = "DV";
          
            GiaCLSNOITRU = "THEOKHOA";

            InsertAfterSelectGroup = true;


        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình chuyển cận lâm sàng "),
        Description("true=Cho phép thêm chỉ định chi tiết ngay sau khi chọn nhóm.False= phải nhấn chấp nhận mới thêm chi tiết chỉ định"),
        DisplayName("Cho phép thêm chỉ định chi tiết ngay sau khi chọn nhóm")]
        public bool InsertAfterSelectGroup { get; set; }

       

        [Browsable(true), ReadOnly(false), Category("Cấu hình chuyển cận lâm sàng "),
         Description("Cho phép chuyển cận lâm sàng mới cho phép thực hiện "),
         DisplayName("Cho phép chuyển cận lâm sàng")]
        public bool IsChuyenCLS { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình chuyển cận lâm sàng "),
         Description("Mã đối tượng khai báo ngăn cách nhau dấu phẩy, mã đối tượng sẽ được chuyển cận sang cận lâm sang ngay "),
         DisplayName("Mã đối tượng cho phép chuyển cận ngay khi thanh toán")]
        public string MaDTTuDong { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình lấy giá CLS nội trú "),
         Description("Lấy giá CLS. THEOKHOA: giá cls tính theo khoa đang sử dụng. KKB: giá theo Khoa khám bênh. KYC: giá theo Khoa Yêu cầu... "),
         DisplayName("Giá cận lâm sàng khoa nội trú")]
        public string GiaCLSNOITRU { get; set; }
    }
    public class DuocNoitruProperties
    {
        private string _characteristics;

        [Browsable(true), ReadOnly(false), Category("Cấu hình chức năng cấp phát dược nội trú "),
         Description("Chọn thông tin đối tượng sử dụng"),
         DisplayName("Đối tượng dùng")]
        public Doituongdung Doituong { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình chức năng cấp phát dược nội trú "),
           Description("Quyền hủy xác nhận"),
           DisplayName("Hủy xác nhận")]
        public bool ChophepHuyxacnhan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình chức năng cấp phát dược nội trú "),
        Description("Tiêu đề chung khi in"),
        DisplayName("Tiêu đề")]
        public string Tieude { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình chức năng cấp phát dược nội trú "),
        Description("Tiêu đề khi in phiếu chỉnh sửa đơn thuốc"),
        DisplayName("Tiêu đề phiếu chỉnh sửa đơn thuốc")]
        public string Tieudedonthuocbosung { get; set; }



        [Browsable(true), ReadOnly(false), Category("Cấu hình chức năng cấp phát dược nội trú "),
       Description("Tiêu đề cấp phát thuốc khoa"),
       DisplayName("Tiêu đề")]
        public string Tieude_capphatthuoc_khoa { get; set; }



        [Browsable(true), ReadOnly(false), Category("Cấu hình chức năng phát thuốc cho Bệnh nhân "),
           Description("Quyền hủy phát thuốc cho bệnh nhân đã lĩnh thuốc"),
           DisplayName("Hủy phát thuốc")]
        public bool ChophepHuyphatthuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình chức năng phát thuốc cho Bệnh nhân "),
          Description("Loại khoa nội trú"),
          DisplayName("Khoa nội trú theo máy")]
        public bool Khoanoitrutheomay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình chức năng phát thuốc cho Bệnh nhân "),
         Description("Đến ngày=ngày hiện tại. Từ ngày được tính bằng đến ngày trừ số ngày lùi. Nếu Số ngày lùi=0 thì Từ ngày=Đến ngày"),
         DisplayName("Số ngày lùi khi tìm kiếm đơn thuốc tạo phiếu lĩnh thuốc nội trú")]
        public int Songayluitimdonthuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình chức năng phát thuốc cho Bệnh nhân "),
        Description("Đến ngày=ngày hiện tại. Từ ngày được tính bằng đến ngày trừ số ngày lùi. Nếu Số ngày lùi=0 thì Từ ngày=Đến ngày"),
        DisplayName("Số ngày lùi khi tìm kiếm phiếu lĩnh thuốc nội trú")]
        public int Songayluitimphieu { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình chức năng phát thuốc cho Bệnh nhân "),
         Description("Lọc thông tin trạng thái duyệt thuốc khi duyệt,Nếu bạn chọn false thì sẽ không thay đổi, nếu true sẽ lọc trên lưới ngay khi duyệt"),
         DisplayName("Lọc xác nhận ngay khi duyệt")]
        public bool LocDonThuocKhiDuyet { get; set; }

       

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thêm-sửa-xóa-in"),
         Description("Quyền xác nhận"),
         DisplayName("Xác nhận")]
        public bool Xacnhan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thêm-sửa-xóa-in"),
          Description("Quyền hủy xác nhận"),
          DisplayName("Hủy xác nhận")]
        public bool Huyxacnhan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình sổ tam tra"),
      Description("tiêu dề báo cáo "),
      DisplayName("tiêu dề báo cáo")]
        public string tieudebaocao { get; set; }

      

        [Browsable(true), ReadOnly(false), Category("Cấu hình sổ tam tra"),
        Description("In thăng ra máy in   "),
        DisplayName("In thăng ra máy in ")]
        public bool isInThangMayin { get; set; }

        [XmlIgnore]
        [Browsable(true), ReadOnly(false), Category("Cấu hình sổ tam tra"),
         Description("TitleFont"),
         DisplayName("TitleFont")]
        public Font TitleFont { get; set; }

        [Browsable(false)]
        [XmlElement(" TitleFontName")]
        public string TitleFontName
        {
            get
            {
                return TitleFont.Name;
            }
            set
            {
                TitleFont = new Font(value, TitleFontsize, TitleFontstyle);
            }
        }

        [Browsable(false)]
        [XmlElement(" TitleFontsize")]
        public float TitleFontsize
        {
            get
            {
                return TitleFont.Size;
            }
            set
            {
                TitleFont = new Font(TitleFontName, value, TitleFontstyle);
            }
        }

        [Browsable(false)]
        [XmlElement(" TitleFontstyle")]
        public FontStyle TitleFontstyle
        {
            get
            {
                return TitleFont.Style;
            }
            set
            {
                TitleFont = new Font(TitleFontName, TitleFontsize, value);
            }
        }

        [XmlIgnore]
        [Browsable(true), ReadOnly(false), Category("Schedule color settings"),
         Description("ContentFont"),
         DisplayName("ContentFont")]
        public Font ContentFont { get; set; }

        [Browsable(false)]
        [XmlElement(" ContentFontName")]
        public string ContentFontName
        {
            get
            {
                return ContentFont.Name;
            }
            set
            {
                ContentFont = new Font(value, ContentFontsize, ContentFontstyle);
            }
        }

        [Browsable(false)]
        [XmlElement(" ContentFontsize")]
        public float ContentFontsize
        {
            get
            {
                return ContentFont.Size;
            }
            set
            {
                ContentFont = new Font(ContentFontName, value, ContentFontstyle);
            }
        }

        [Browsable(false)]
        [XmlElement(" ContentFontstyle")]
        public FontStyle ContentFontstyle
        {
            get
            {
                return ContentFont.Style;
            }
            set
            {
                ContentFont = new Font(ContentFontName, ContentFontsize, value);
            }
        }
        public DuocNoitruProperties()
        {
            Doituong = Doituongdung.Yta;
            ChophepHuyxacnhan = false;
            Khoanoitrutheomay = true;
            ChophepHuyphatthuoc = true;
            LocDonThuocKhiDuyet = true;
            Tieude = "PHIẾU LĨNH CẤP PHÁT HÀNG NGÀY";
            Tieudedonthuocbosung = "PHIẾU HIỆU CHỈNH ĐƠN THUỐC";
            Tieude_capphatthuoc_khoa = "PHIẾU BỔ SUNG THUỐC DỰ TRÙ";
            Xacnhan = true;
            Huyxacnhan = true;

            Songayluitimphieu = 0;
            Songayluitimdonthuoc = 0;

            tieudebaocao = "SỔ TỔNG HỢP THUỐC HÀNG NGÀY";
            TitleFont = new Font("Arial", 18, FontStyle.Bold);
            ContentFont = new Font("Arial", 10, FontStyle.Regular);
            isInThangMayin = false;
        }
    }
    public class QMSPrintProperties
    {
        [Browsable(true), ReadOnly(false), Category("Tên Bệnh Viện"),
         Description("Cấu hình hiển thị tên bệnh viện"),
         DisplayName("Cấu hình hiển thị tên bệnh viện")]
        public string TenBenhVien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy hiển thị"),
         Description("Mã quầy thu ngân cần hiển thị số"),
         DisplayName("Mã quầy thu ngân cần  ")]
        public string MaQuay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy hiển thị"),
         Description("Tên quầy thu ngân cần hiển thị số"),
         DisplayName("Tên quầy thu ngân cần  ")]
        public string TenQuay { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy hiển thị"),
        Description("Hiển thị khoa khám bệnh hoặc khoa yêu cầu"),
        DisplayName("Mã khoa thực hiện")]
        public string MaKhoaThuchien { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy hiển thị"),
         Description("Cỡ chữ của hệ thống"),
         DisplayName("Font size ")]
        public Int32 FontSize { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình tự động lấy thông tin "),
         Description("Cấu hình cho phép lấy thông tin dữ liệu trên lưới"),
         DisplayName("Tự động lấy thông tin lên danh sách ")]
        public bool TuDongLayThongTin { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tự động lấy thông tin "),
         Description("Thời gian tự động lấy thông tin"),
         DisplayName("Thời gian lấy dữ liệu ")]
        public Int32 ThoiGianTuDongLay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình số khám ưu tiên"),
       Description("Chỉ hiển thị số ưu tiên"),
       DisplayName("Chỉ hiển thị số ưu tiên")]
        public bool Chilaysouutien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình số khám ưu tiên"),
      Description("Cho phép lấy số ưu tiên"),
      DisplayName("Cho phép lấy số ưu tiên")]
        public bool Chopheplaysouutien { get; set; }
        [ReadOnly(false), DisplayName("Đối tượng KCB"),
        Browsable(true), Category("Cấu hình mã đối tượng KCB."),
        Description("Mã đối tượng KCB")]
        public string MaDoituongKCB { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình in máy in nhiệt "),
         Description("Lấy thông tin máy in nhiệt"),
         DisplayName("Tên máy in nhiệt cần in  ")]
        public string PrinterName { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình in máy in nhiệt "),
         Description("Lấy thông tin máy in nhiệt"),
         DisplayName("Tên máy in nhiệt cần in  ")]
        public bool PrintStatus { get; set; }


        [ReadOnly(false), DisplayName("Mã khoa lấy số khoa KCB"), 
        Browsable(true),   Category("Cấu hình phát số chờ tiếp đón KCB "), 
        Description("Mã khoa KCB")]
        public string MaKhoaKhamBenh { get; set; }

        [Browsable(true),   Category("Cấu hình phát số chờ tiếp đón KCB "),
        DisplayName("Mã khoa KCB yêu cầu"), 
        ReadOnly(false), 
        Description("Mã khoa KCB yêu cầu")]
        public string MaKhoaYeuCau { get; set; }

        [Category("Cấu hình phát số chờ tiếp đón KCB "),
        DisplayName("Tên khoa KCB"), 
        Description("Tên khoa KCB"), 
        Browsable(true), ReadOnly(false)]
        public string TenKhoaKhamBenh { get; set; }

        [Category("Cấu hình phát số chờ tiếp đón KCB "),
        DisplayName("Tên khoa KCB theo yêu cầu"),
        Description("Tên khoa KCB theo yêu cầu"), 
        Browsable(true), ReadOnly(false)]
        public string TenKhoaYeuCau { get; set; }

        [XmlIgnore]
        [Browsable(true), ReadOnly(false), Category("Cấu hình màu sắc"),
         Description("Màu nền số khám thường"),
         DisplayName("Màu nền thường")]
        public Color MauB1 { get; set; }

        [Browsable(false)]
        [XmlElement("MauB1")]
        public string _MauB1
        {
            get
            {
                return MauB1.Name;
            }
            set
            {
                MauB1 = Color.FromName(value);
            }
        }

        [XmlIgnore]
        [Browsable(true), ReadOnly(false), Category("Cấu hình màu sắc"),
         Description("Màu chữ số khám thường"),
         DisplayName("Màu chữ thường")]
        public Color MauF1 { get; set; }

        [Browsable(false)]
        [XmlElement("MauF1")]
        public string _MauF1
        {
            get
            {
                return MauF1.Name;
            }
            set
            {
                MauF1 = Color.FromName(value);
            }
        }

        [XmlIgnore]
        [Browsable(true), ReadOnly(false), Category("Cấu hình màu sắc"),
         Description("Màu nền số khám yêu cầu"),
         DisplayName("Màu nền khám yêu cầu")]
        public Color MauB2 { get; set; }

        [Browsable(false)]
        [XmlElement("MauB2")]
        public string _MauB2
        {
            get
            {
                return MauB2.Name;
            }
            set
            {
                MauB2 = Color.FromName(value);
            }
        }

        [XmlIgnore]
        [Browsable(true), ReadOnly(false), Category("Cấu hình màu sắc"),
         Description("Màu chữ số khám yêu cầu"),
         DisplayName("Màu chữ khám yêu cầu")]
        public Color MauF2 { get; set; }

        [Browsable(false)]
        [XmlElement("MauF2")]
        public string _MauF2
        {
            get
            {
                return MauF2.Name;
            }
            set
            {
                MauF2 = Color.FromName(value);
            }
        }

        [XmlIgnore]
        [Browsable(true), ReadOnly(false), Category("Cấu hình màu sắc"),
         Description("Màu nền số khám ưu tiên"),
         DisplayName("Màu nền ưu tiên")]
        public Color MauB3 { get; set; }

        [Browsable(false)]
        [XmlElement("MauB3")]
        public string _MauB3
        {
            get
            {
                return MauB3.Name;
            }
            set
            {
                MauB3 = Color.FromName(value);
            }
        }

        [XmlIgnore]
        [Browsable(true), ReadOnly(false), Category("Cấu hình màu sắc"),
         Description("Màu chữ số khám ưu tiên"),
         DisplayName("Màu chữ ưu tiên")]
        public Color MauF3 { get; set; }

        [Browsable(false)]
        [XmlElement("MauF3")]
        public string _MauF3
        {
            get
            {
                return MauF3.Name;
            }
            set
            {
                MauF3 = Color.FromName(value);
            }
        }
        public QMSPrintProperties()
        {
            TenBenhVien = "BỆNH VIỆN NỘI TIẾT TRUNG ƯƠNG";
            TuDongLayThongTin = false;
            ThoiGianTuDongLay = 500;
            MaQuay = "QUAYSO_1";
            TenQuay = "Quầy thu ngân số 1";
            FontSize = 400;
            PrinterName = Utility.GetDefaultPrinter();
            PrintStatus = true;

            MaKhoaThuchien = "KKB";


            Chopheplaysouutien = false;
            Chilaysouutien = false;
            MaKhoaThuchien = "KKB";
            MaDoituongKCB = "ALL";

            MaKhoaYeuCau = "KYC";
            TenKhoaKhamBenh = "KHOA KHÁM CHỮA BỆNH (TẦNG 1)";
            TenKhoaYeuCau = "KHOA KHÁM YÊU CẦU (TẦNG 2 PHÒNG A-205)";

        }
    }
    public class TrathuocthuaProperties
    {
        [Browsable(true), ReadOnly(false), Category("Cấu hình tìm kiếm phiếu trả thuốc thừa"),
       Description("Tự động tìm kiếm khi chuyển trạng thái"),
       DisplayName("Tự động tìm kiếm khi chuyển trạng thái")]
        public bool TimkiemkhiChontrangthai { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tìm kiếm phiếu trả thuốc thừa"),
         Description("Lọc thông tin trạng thái duyệt thuốc sau khi duyệt,Nếu bạn chọn false thì sẽ không thay đổi, nếu true sẽ lọc trên lưới ngay khi duyệt"),
         DisplayName("Lọc xác nhận ngay khi duyệt")]
        public bool Locphieusaukhiduyet { get; set; }



        [Browsable(true), ReadOnly(false), Category("Cấu hình trả thuốc thừa"),
        Description("Nếu là true cho phép, false là ẩn quyển"),
        DisplayName("Quyền được thêm phiếu")]
        public bool QuyenThemPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình trả thuốc thừa"),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được sửa thêm phiếu")]
        public bool QuyenSuaPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình trả thuốc thừa"),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được xóa phiếu")]
        public bool QuyenXoaPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình trả thuốc thừa"),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được xác nhận phiếu")]
        public bool QuyenXacNhanPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình trả thuốc thừa"),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được hủy xác nhận phiếu")]
        public bool QuyenHuyXacNhanPhieu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình trả thuốc thừa"),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền in phiếu")]
        public bool QuyenInphieu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tìm kiếm phiếu trả thuốc thừa"),
    Description("Đến ngày=ngày hiện tại. Từ ngày được tính bằng đến ngày trừ số ngày lùi. Nếu Số ngày lùi=0 thì Từ ngày=Đến ngày"),
    DisplayName("Số ngày lùi khi tìm kiếm phiếu trả thuốc thừa")]
        public int Songayluitimphieu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu ập nhật phiếu trả thuốc thừa"),
     Description("In ngay sau khi lưu"),
     DisplayName("In ngay sau khi lưu")]
        public bool Insaukhiluu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình in phiếu trả thuốc thừa"),
   Description("Loại phiếu in"),
   DisplayName("Loại phiếu in")]
        public LoaiphieuIn Kieuin { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tìm kiếm phiếu trả thuốc thừa"),
   Description("True=Tìm theo ngày trả thuốc.False=Tìm theo ngày lập phiếu"),
   DisplayName("Tìm theo ngày trả thuốc")]
        public bool Timtheongaytra { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình in phiếu trả thuốc thừa"),
  Description("Hiển thị chọn in phiếu"),
  DisplayName("Hiển thị chọn in phiếu")]
        public bool Hienthichonin { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu ập nhật phiếu trả thuốc thừa"),
  Description("0=Chuyển về trạng thái cập nhật;1=thoát sau khi Lưu;2= Chuyển về trạng thái thêm mới phiếu khác"),
  DisplayName("Thoát sau khi lưu")]
        public int ThoatsaukhiLuu { get; set; }

        public TrathuocthuaProperties()
        {
            TimkiemkhiChontrangthai = true;
            QuyenHuyXacNhanPhieu = true;
            QuyenSuaPhieu = true;
            QuyenThemPhieu = true;
            QuyenXacNhanPhieu = true;
            QuyenXoaPhieu = true;
            QuyenInphieu = true;
            TimkiemkhiChontrangthai = false;
            Timtheongaytra = false;
            Kieuin = LoaiphieuIn.Chitiet;
            Insaukhiluu = false;
            Locphieusaukhiduyet = false;
        }
    }
    public enum Doituongdung { Yta = 0, Nhanvienkho = 1 };
    public enum LoaiphieuIn { Cahai = 0, Tonghop = 1, Chitiet=2 };
    public enum DisplayType { Tatca = 0, Noitru = 1,Ngoaitru=2 };
    public class HisDuocProperties
    {
        private string _characteristics;

        [Browsable(true), ReadOnly(false), Category("Cấu hình thuốc - vật tư"),
         Description("THUOC=Chức năng dành cho thuốc. VT=Chức năng dành cho Vật tư tiêu hao"),
         DisplayName("Kiểu thuốc vật tư")]
        public string KieuThuocVattu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình phần chốt thuốc"),
       Description("Hiển thị chi tiết đơn thuốc tại chức năng chốt thuốc"),
       DisplayName("Hiển thị chi tiết đơn thuốc tại chức năng chốt thuốc")]
        public bool Hienthichitietkhichotthuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình phần chốt thuốc"),
       Description("Tự động tìm kiếm khi chuyển trạng thái tại chức năng chốt thuốc"),
       DisplayName("Tự động tìm kiếm khi chuyển trạng thái tại chức năng chốt thuốc")]
        public bool TimkiemkhiChontrangthai { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình phần chốt thuốc"),
        Description("True=Cho phép người dùng hủy chốt thuốc. False= Không cho phép"),
        DisplayName("Cho phép hủy chốt")]
        public bool ChoPhepHuyChotThuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình phần chốt thuốc"),
       Description("True=Cho phép người dùng nhập ngày chốt thuốc. False= Không cho phép"),
       DisplayName("Nhập ngày chốt")]
        public bool Chophepnhapngaychot { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Duyệt đơn thuốc tại kho "),
         Description("Lọc thông tin trạng thái duyệt thuốc khi duyệt,Nếu bạn chọn false thì sẽ không thay đổi, nếu true sẽ lọc trên lưới ngay khi duyệt"),
         DisplayName("Lọc xác nhận ngay khi duyệt")]
        public bool LocDonThuocKhiDuyet { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Duyệt đơn thuốc tại kho "),
         Description("Cho phép cấu hình thông tin chỉnh ngày duyệt đơn thuốc"),
         DisplayName("Cho phép chỉnh ngày duyệt đơn thuốc")]
        public bool ChoPhepChinhNgayDuyet { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Duyệt đơn thuốc tại kho "),
         Description("Lấy thông tin đơn thuốc ngoại trú"),
         DisplayName("Ngoại Trú")]
        public bool NgoaiTru { get; set; }

      


        [Browsable(true), ReadOnly(false), Category("Cấu Hình Duyệt đơn thuốc tại kho "),
         Description("Lấy thông tin đơn thuốc nội trú"),
         DisplayName("Nội Trú")]
        public bool NoiTru { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Duyệt đơn thuốc tại kho "),
         Description("Cho phép hủy xác nhận đơn"),
         DisplayName("Hủy xác nhận đơn thuốc")]
        public bool HuyXacNhan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Duyệt đơn thuốc tại kho "),
         Description("Cho phép xóa chi tiết"),
         DisplayName("Cho phép xóa chi tiết")]
        public bool Chophepxoachitiet { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Duyệt đơn thuốc tại kho "),
         Description("Cho phép chọn duyệt đơn thuốc hay theo chi tiết của đơn thuốc (đơn tách)"),
         DisplayName("Kiểu duyệt đơn thuốc")]
        public string KieuDuyetDonThuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Duyệt đơn thuốc tại kho "),
         Description("Cho phép thêm mới, sửa xóa thông tin đơn thuốc"),
         DisplayName("Thêm,sửa, xóa đơn thuốc kê cho bảo hiểm y tế")]
        public bool ThemBHYTDonThuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Lấy thông tin "),
         Description("Cấu hình phiếu xuất thuốc gây nghiện"),
         DisplayName("Đơn vị")]
        public string Tendonvi { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình Lấy thông tin "),
         Description("Cấu hình phiếu xuất thuốc gây nghiện"),
         DisplayName("Mã nhóm thuốc gây nghiện")]
        public string MathuocGaynghien { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình kê đơn thuốc tại quầy "),
         Description("Cho phép chọn duyệt đơn thuốc tại khoa hoặc tại quầy "),
         DisplayName("Kiểu duyệt đơn thuốc tại quầy")]
        public string DuyetDonThuoctaiKhoVaQuay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình kê đơn thuốc tại quầy "),
         Description(
             "Cho phép kê đơn thuốc tại quầy lấy thông tin vật tư hoặc thuốc. Nếu là thuốc thì cầu hình là : THUOC, Nếu là vật tư thì lấy thông tin :VT "
             ),
         DisplayName("Thông tin lấy dữ liệu tại kê đơn thuốc tại quầy")]
        public string KieuLayThongTinThuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình kê đơn thuốc tại quầy "),
         Description("cho phép thanh toán tại quầy thuốc, hủy đơn thuốc "),
         DisplayName("Thông tin cho phép thanh toán tại quầy thuốc")]
        public bool ThanhToanThuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình kê đơn thuốc tại quầy "),
         Description("Thêm, sửa, xóa đơn thuốc tại quầy "),
         DisplayName("Thêm, sửa, xóa đơn thuốc tại quầy")]
        public bool ThemSuaXoaThuocTaiQuay { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình kê đơn thuốc tại quầy "),
        Description("Xóa thông tin chi tiết đơn thuốc bán tại quầy "),
        DisplayName("xóa thông tin chi tiết bán thuốc tại quầy")]
        public bool XoaDonThuocChiTietTaiQuay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình kê đơn thuốc tại quầy "),
         Description("Cho phép phát thuốc "),
         DisplayName("Cho phép phát thuốc tại quầy")]
        public bool ChoPhepPhatThuocTaiQuay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình kê đơn thuốc tại quầy "),
         Description("Mặc định kê tại quầy thuốc "),
         DisplayName("Mặc định kê đơn thuốc tại quầy")]
        public bool MacDinhKeDonThuocTaiQuay { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình kê đơn thuốc tại quầy "),
         Description(
             "Lọc thông tin trạng thái duyệt thuốc khi duyệt,Nếu bạn chọn false thì sẽ không thay đổi, nếu true sẽ lọc trên lưới ngay khi duyệt"
             ),
         DisplayName("Lọc xác nhận ngay khi duyệt tại quầy")]
        public bool LocDonThuocKhiDuyetTaiQuay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình kê đơn thuốc tại quầy "),
         Description("Cho phép cấu hình thông tin chỉnh ngày duyệt đơn thuốc thực hiện tại quầy"),
         DisplayName("Cho phép chỉnh ngày duyệt đơn thuốc")]
        public bool ChoPhepChinhNgayDuyetTaiQuay { get; set; }

     

        [Browsable(true), ReadOnly(false), Category("Cập nhập số lượng tồn"),
      Description("Cho phép sửa trực tiếp số lượng tồn trên lưới"),
      DisplayName("Cho phép sửa số lượng tồn trên lưới")]
        public bool ChoPhepSuaSLuongTon { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cập nhập số lượng tồn"),
     Description("Thời gian tự động lấy số liệu tính bằng giây. 0= Không tự động lấy số liệu"),
     DisplayName("Thời gian tự động lấy số liệu tính bằng giây")]
        public uint Tudonglaysolieu { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình trừ kho thuốc"),
      Description("Nếu là true thì sẽ xóa thông tin bản ghi số lượng tồn trong kho nếu=0, nếu là false thì không làm gì"),
      DisplayName("Xóa thông tin trừ kho nếu thuốc đã hết")]
        public bool XoaDulieuKhiThuocDaHet { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình quyền xuất/nhập kho"),
        Description("Nếu là true cho phép, false là ẩn quyển"),
        DisplayName("Quyền được thêm phiếu xuất/nhập")]
        public bool QuyenThemPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình quyền xuất/nhập kho"),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được sửa thêm phiếu xuất/nhập")]
        public bool QuyenSuaPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình quyền xuất/nhập kho"),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được xóa phiếu xuất/nhập")]
        public bool QuyenXoaPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình quyền xuất/nhập kho"),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được xác nhận phiếu xuất/nhập")]
        public bool QuyenXacNhanPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình quyền xuất/nhập kho"),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được hủy xác nhận phiếu xuất/nhập")]
        public bool QuyenHuyXacNhanPhieu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình quyền xuất/nhập kho"),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền in phiếu")]
        public bool QuyenInphieu { get; set; }

       
        // public string Ke { get; set; }
        public HisDuocProperties()
        {
            TimkiemkhiChontrangthai = true;
            Hienthichitietkhichotthuoc = true;
            Tudonglaysolieu = 0;
            NgoaiTru = true;
            KieuThuocVattu = "THUOC";
            NoiTru = true;
            HuyXacNhan = true;
            KieuDuyetDonThuoc = "DONTHUOC";
            DuyetDonThuoctaiKhoVaQuay = "KHO";
            KieuLayThongTinThuoc = "ALL";
            ThanhToanThuoc = false;
            ThemSuaXoaThuocTaiQuay = false;
            ChoPhepPhatThuocTaiQuay = true;
            Tendonvi = "KYDONG";
            MathuocGaynghien = "TGNHT";
            MacDinhKeDonThuocTaiQuay = true;
            ThemBHYTDonThuoc = true;
            Chophepxoachitiet = true;
           
            LocDonThuocKhiDuyet = false;
            ChoPhepChinhNgayDuyet = false;
            LocDonThuocKhiDuyetTaiQuay = false;
            ChoPhepChinhNgayDuyetTaiQuay = false;
            XoaDonThuocChiTietTaiQuay = true;
            XoaDulieuKhiThuocDaHet = false;
            ChoPhepSuaSLuongTon = true;
            QuyenHuyXacNhanPhieu = true;
            QuyenSuaPhieu = true;
            QuyenThemPhieu = true;
            QuyenXacNhanPhieu = true;
            QuyenXoaPhieu = true;
            QuyenInphieu = true;
            ChoPhepHuyChotThuoc = false;
            Chophepnhapngaychot = false;
        }
    }
   
    public class HISPrintProperties1
    {
        public HISPrintProperties1()
        {
            InThuocsaukhiLuu = true;
            ChophepIndonthuoc = false;
            HienthituychonInsaukhiLuu = false;
            InCLSsaukhiLuu = true;
            InPhieukhamsaukhiLuu = true;
            InPhoiKhiThanhToan = false;
            IsChiDinhNhanh = false;
            Inngay = true;
            TenmayinThuocCLS = Utility.GetDefaultPrinter();
            Tenmayinphieukham = Utility.GetDefaultPrinter();
            TenMayThanhToan = Utility.GetDefaultPrinter();
            sPrinterNameBHYT = Utility.GetDefaultPrinter();
            Sobanin = 1;
            KhoGiay = "A5";
            IsQMS = false;
            SoLuongCanInBHYT = 1;
        }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình in phôi BHYT "),
         Description("Tên máy in để in phôi BHYT"),
         DisplayName("Tên máy in để in phôi BHYT")]
        public string sPrinterNameBHYT { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình in phôi BHYT "),
         Description("Số lượng cần in ra phôi bảo hiểm,mặc định là số lượng=1"),
         DisplayName("Số lượng cần in ra phôi bảo hiểm")]
        public int SoLuongCanInBHYT { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình máy in hóa đơn "),
         Description("Tên máy in để in phôi hóa đơn"),
         DisplayName("Tên máy in để in hóa đơnT")]
        public string sPrinterNameHoaDon { get; set; }



        public int SoLuongCanInHoaDon { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu Hình in "),
         Description("Cho phép tính năng in đơn tại form kê đơn thuốc"),
         DisplayName("Cho phép in đơn thuốc")]
        public bool ChophepIndonthuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình in "),
         Description("In đơn thuốc ngay sau khi lưu. Có in=true; Không in=false"),
         DisplayName("In đơn ngay sau khi lưu")]
        public bool InThuocsaukhiLuu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình in "),
         Description("Hiển thị tùy chọn In sau khi lưu. Có hiển trị=true; Không hiển thị=false"),
         DisplayName("Hiển thị tùy chọn in sau khi lưu")]
        public bool HienthituychonInsaukhiLuu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình in "),
         Description("In phiếu chỉ định CLS ngay sau khi lưu. Có in=true;Không in=false"),
         DisplayName("In CLS sau khi lưu")]
        public bool InCLSsaukhiLuu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình chỉ định CLS "),
         Description("Cho phép chỉ định nhanh"),
         DisplayName("Cấu hình chỉ định nhanh")]
        public bool IsChiDinhNhanh { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình in "),
         Description("In phiếu khám ngay sau khi lưu. Có in=true;Không in=false"),
         DisplayName("In phiếu KCB sau khi lưu")]
        public bool InPhieukhamsaukhiLuu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình in "),
         Description("In phôi bảo hiểm ngay sau khi lưu. Có in=true;Không in=false"),
         DisplayName("In phôi bảo hiểm khi thanh toán")]
        public bool InPhoiKhiThanhToan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình in "),
         Description("In ngay sau khi preview. Có in=true;Không in=false"),
         DisplayName("In ngay sau khi Preview")]
        public bool Inngay { get; set; }


        [Browsable(true), ReadOnly(false), Category("Máy in"),
         Description("Tên máy in chỉ định CLS và thuốc"),
         DisplayName("Tên máy in chỉ định CLS và thuốc")]
        public string TenmayinThuocCLS { get; set; }

        [Browsable(true), ReadOnly(false), Category("Máy in"),
         Description("Tên máy in phiếu khám"),
         DisplayName("Tên máy in phiếu khám")]
        public string Tenmayinphieukham { get; set; }

        [Browsable(true), ReadOnly(false), Category("Máy in"),
         Description("Tên máy in thanh toán"),
         DisplayName("Tên máy in thanh toán")]
        public string TenMayThanhToan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Máy in"),
         Description("Tên máy in phiếu khám"),
         DisplayName("Tên máy in phiếu khám")]
        public Int32 Sobanin { get; set; }


        [Browsable(true), ReadOnly(false), Category("Khổ giấy in cận lâm sàng"),
         Description("Xét khổ giấy in cận lâm sàng A4, A5"),
         DisplayName("Tên khổ giấy ")]
        public string KhoGiay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS hàng đợt"),
         Description("Cho phép hiển thị phần gọi số thứ tự QMS"),
         DisplayName("Cho phép dùng QMS ")]
        public bool IsQMS { get; set; }
        

    }
    public class HISQMSProperties
    {
        public HISQMSProperties()
        {
            ThoiGianCho = 3;
            TestMode = false;
            ThoiGianTuDongLay = 3000;
            HienThiBacSyKham = true;
            TenBacSyHienThi = "";
            IsQMS = false;
            MaQuay = "QUAY_1";
            TenQuay = "Quầy thu ngân số 1";
            TenBenhVien = "BỆNH VIỆN NỘI TIẾT TRUNG ƯƠNG";
            FontSize = 400;
            LoaiBNFontSize = 80;
            SoLuongHienThi = 5;
            MaPhongKham = "P6";
            Chopheplaysouutien = false;
            Chilaysouutien = false;
            MaDoituongKCB = "ALL";
            LoaiQMS_bo = "NO";
            TenLoaiQMS = "";
            LoaiQMS = 0;
            TenSothuong = "SỐ THƯỜNG";
            TenSoUutien = "SỐ ƯU TIÊN";
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
   Description("Tên số thường hiển thị ở màn hình chờ"),
   DisplayName("Tên số thường")]
        public string TenSothuong { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
  Description("Tên số ưu tiên hiển thị ở màn hình chờ"),
  DisplayName("Tên số ưu tiên")]
        public string TenSoUutien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
    Description("Để trắng=Hệ thống tự phân biệt. Ngược lại, nếu muốn tách riêng phòng tiếp đón sử dụng số KHÁC thì để tên loại số đó. Ví dụ: Số tiêm chủng, Số đặc biệt..."),
    DisplayName("Tên loại QMS: Số ưu tiên, Số thường, Số tiêm chủng")]
        public string TenLoaiQMS { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
    Description("Loại QMS bỏ không lấy.NO=Không bỏ số nào. Ngược lại, muốn bỏ số thường thì đặt 0. muốn bỏ số thường+số ưu tiên thì đặt 0,1. ..."),
    DisplayName("Loại QMS bỏ")]
        public string LoaiQMS_bo { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
   Description("Loại QMS lấy.-1= Không xác định. Muốn lấy số thường thì đặt 0. muốn lấy số khác đặt=2. Số ưu tiên hệ thống tự động dò"),
   DisplayName("Loại QMS lấy")]
        public int LoaiQMS { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
     Description("Chỉ hiển thị số ưu tiên tại quầy tiếp đón"),
     DisplayName("Chỉ hiển thị số ưu tiên tại quầy tiếp đón")]
        public bool Chilaysouutien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
      Description("Cho phép lấy số ưu tiên tại quầy tiếp đón(Tổng số QMS= Tổng số thường+Tổng số ưu tiên). Nếu không thì hệ thống chỉ lấy số thường"),
      DisplayName("Cho phép lấy số ưu tiên tại quầy tiếp đón này")]
        public bool Chopheplaysouutien { get; set; }
        [ReadOnly(false), DisplayName("Đối tượng KCB"),
        Browsable(true), Category("Cấu hình mã đối tượng KCB."),
        Description("Mã đối tượng KCB")]
        public string MaDoituongKCB { get; set; }
        
        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS hàng đợi"),
         Description("Cho phép hiển thị phần gọi số thứ tự QMS"),
         DisplayName("Cho phép dùng QMS ")]
        public bool IsQMS { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS hàng đợi phòng khám bệnh"),
         Description("Mã phòng khám sử dụng QMS chờ khám"),
         DisplayName("Mã phòng khám sử dụng QMS")]
        public string MaPhongKham { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS hàng đợi phòng khám bệnh"),
         Description("Số lượng hiển thị trên màn hình"),
         DisplayName("Số lượng hiển thị trên màn hình chờ ")]
        public Int32 SoLuongHienThi { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS hàng đợi phòng khám bệnh"),
      Description("Cho phép hiển thị bác sỹ khám ngoài màn hình hiển thị"),
      DisplayName("Hiển thị bác sỹ khám")]
        public bool HienThiBacSyKham { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS hàng đợi phòng khám bệnh"),
        Description("Tên bác sỹ hiển thị ngoài màn hình"),
        DisplayName("Tên bác sỹ hiển thị ngoài màn hình")]
        public string TenBacSyHienThi { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS hàng đợi phòng khám bệnh"),
         Description("Thời gian sẽ tự động lấy lại dữ liệu"),
         DisplayName("Thời gian sẽ tự động lấy lại dữ liệu ")]
        public Int32 ThoiGianTuDongLay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
         Description("Mã quầy tiếp đón"),
         DisplayName("Mã quầy tiếp đón")]
        public string MaQuay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
         Description("Tên quầy tiếp đón"),
         DisplayName("Tên quầy tiếp đón")]
        public string TenQuay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Tên Bệnh Viện"),
         Description("Cấu hình hiển thị tên bệnh viện"),
         DisplayName("Cấu hình hiển thị tên bệnh viện")]
        public string TenBenhVien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
         Description("Cỡ chữ QMS: 01,02,99,100..."),
         DisplayName("Cỡ chữ QMS")]
        public Int32 FontSize { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS Quầy tiếp đón"),
    Description("Cỡ chữ loại QMS: Số thường, Số ưu tiên..."),
    DisplayName("Cỡ chữ loại QMS")]
        public Int32 LoaiBNFontSize { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS hàng đợi"),
         Description("Thời gian chờ sau mỗi đợt gọi bằng tay"),
         DisplayName("Thời gian chờ")]
        public uint ThoiGianCho { get; set; }
      

        [Browsable(true), ReadOnly(false), Category("Cấu hình QMS hàng đợi"),
         Description("Cấu hình chế độ chạy cho phép test hoặc chạy thật"),
         DisplayName("Chế độ chạy test")]
        public bool TestMode { get; set; }
    }
    public class HISThanhToanNoiTruProperties1
    {
        public HISThanhToanNoiTruProperties1()
        {
            IsDalinh = false;
            IsGhepNgoaiTru = true;
            IsThanhToanRaVien = true;
            ThanhToanlaRaVien = true;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình thanh toán nội trú "),
         Description("Cấu hình cho phép thanh toán nội trú những dịch vụ đã dùng đã xác nhận"),
         DisplayName("Những dịch vụ nội trú đã dùng xác nhận")]
        public bool IsDalinh { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình thanh toán nội trú "),
       Description("Cấu hình cho phép in ghép cả ngoại trú của BHYT"),
       DisplayName("In ghép của bảo hiểm y tế ngoại trú vào nội trú")]
        public bool IsGhepNgoaiTru { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình thanh toán nội trú "),
     Description("Cấu hình cho phép in ghép cả ngoại trú của BHYT"),
     DisplayName("In ghép của bảo hiểm y tế ngoại trú vào nội trú")]
        public bool IsThanhToanRaVien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình thanh toán nội trú "),
      Description("Cấu hình cho thanh toán là bật trạng thái lên 3"),
       DisplayName("Thanh toán bật trạng thái bệnh nhân lên 3")]
        public bool ThanhToanlaRaVien { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu Hình thanh toán nội trú "),
      Description("Hoàn ứng là ra viện luôn bật trạng thái lên 3"),
       DisplayName("Hoàn ứng bật trạng thái ra viện lên là 3")]
        public bool HoanUngLaRaVien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình thanh toán nội trú "),
   Description("Tự động chuyển thanh toán khi in phôi"),
    DisplayName("Tự động chuyển thanh toán khi in phôi")]
        public bool IsInPhoiChuyenThanhToan { get; set; }
    }
    public class KetNoiTienIchProperties1
    {

        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public bool IsTuNgay { get; set; }
        public int WithSpliper { get; set; }

        public bool IsNhapLienTuc { get; set; }
        public KetNoiTienIchProperties1()
        {

            TuNgay = DateTime.Now;
            DenNgay = DateTime.Now;
            IsTuNgay = false;
            IsNhapLienTuc = false;
            WithSpliper = 400;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu tên đăng nhập "),
        Description("Tên đăng nhập của user name khi đăng nhập login"),
        DisplayName("UserName")]
        public string sUserName { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu tên đăng nhập "),
         Description("Nhớ khi đăng nhập"),
         DisplayName("Remember me")]
        public bool IsRemember { get; set; }

    }
    public enum Papersize
    {
        A4=1,
        A5=2
    }
    public enum KieuIn
    {
        Innhiet = 1,
        InLaser = 2
    }
    public enum Kieuhienthi
    {
        Trenluoi = 1,
        Nutbam = 2,
        Cahai=3
    }
    public enum KieuView
    {
        Chon = 1,
        Click = 2

    }
    public class MayInProperties
    {


        public MayInProperties()
        {

            TenMayInPhieuKCB = "";
            TenMayInHoadon = "";
            TenMayInBienlai = "";
            Tenmayincuoicung = "";

            PreviewInPhoiBHYT = true;
            PreviewInHoadon = true;
            PreviewInBienlai = true;
            PreviewInDonthuoc = true;
            PreviewInCLS = true;
            PreviewPhieuKCB = true;
            PreviewInTomtatDieutriNgoaitru = true;
            PreviewPhieuTamung = true;
           
        CoGiayInBienlai = Papersize.A4;
        CoGiayInTomtatDieutriNgoaitru = Papersize.A4;
            CoGiayInDonthuoc = Papersize.A4;
            CoGiayInCLS = Papersize.A4;
            CoGiayInPhieuKCB = Papersize.A4;
            
            KieuInPhieuKCB = KieuIn.Innhiet;


            InPhieuKCBsaukhiluu = true;
            InHoadonSaukhithanhtoan_KCB = false;
            InCLSsaukhiluu = false;
            InDonthuocsaukhiluu = false;

           
            mauinhoadon = "mau1";
            mauinbienlai = "mau1";
           
            TudonginhoadonSaukhiThanhtoan = true;
            TudonginPhieuchiSaukhitratien = true;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình phiếu chi"),
     Description("Tự dộng in phiếu chi sau khi trả tiền"),
     DisplayName("Tự dộng in phiếu chi sau khi trả tiền")]
        public bool TudonginPhieuchiSaukhitratien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng in hóa đơn"),
      Description("Tự dộng in sau khi thanh toán"),
      DisplayName("Tự dộng in sau khi thanh toán")]
        public bool TudonginhoadonSaukhiThanhtoan { get; set; }
       

      

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng in hóa đơn"),
        Description("giá trị=mau1 hoặc mau2"),
        DisplayName("Mẫu hóa đơn")]
        public string mauinhoadon { get; set; }

      





        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng in hóa đơn"),
       Description("giá trị=mau1 hoặc mau2"),
       DisplayName("Mẫu biên lai")]
        public string mauinbienlai { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình xác nhận công việc"),
       Description("In CLS sau khi lưu"),
       DisplayName("In CLS sau khi lưu")]
        public bool InCLSsaukhiluu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình xác nhận công việc"),
        Description("In đơn thuốc sau khi lưu"),
        DisplayName("In đơn thuốc sau khi lưu")]
        public bool InDonthuocsaukhiluu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình xác nhận công việc"),
        Description("In hóa đơn sau khi thanh toán"),
        DisplayName("In hóa đơn sau khi thanh toán ở chức năng Tiếp đón")]
        public bool InHoadonSaukhithanhtoan_KCB { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình xác nhận công việc"),
         Description("In phiếu khám ngay sau khi lưu Bệnh nhân"),
         DisplayName("In phiếu KCB ngay sau khi lưu Bệnh nhân")]
        public bool InPhieuKCBsaukhiluu { get; set; }
        
            [Browsable(true), ReadOnly(false), Category("Cấu hình cỡ giấy"),
      Description("Cỡ giấy in tóm tắt điều trị ngoại trú"),
      DisplayName("Cỡ giấy in tóm tắt điều trị ngoại trú")]
        public Papersize CoGiayInTomtatDieutriNgoaitru { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình cỡ giấy"),
      Description("Cỡ giấy in biên lai"),
      DisplayName("Cỡ giấy in biên lai")]
        public Papersize CoGiayInBienlai { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình cỡ giấy"),
      Description("Cỡ giấy in đơn thuốc"),
      DisplayName("Cỡ giấy in đơn thuốc")]
        public Papersize CoGiayInDonthuoc { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình cỡ giấy"),
      Description("Cỡ giấy in cận lâm sàng"),
      DisplayName("Cỡ giấy in cận lâm sàng")]
        public Papersize CoGiayInCLS { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình cỡ giấy"),
      Description("Cỡ giấy in phiếu KCB"),
      DisplayName("Cỡ giấy in phiếu KCB")]
        public Papersize CoGiayInPhieuKCB { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình tên máy in"),
        Description("Tên máy in nhiệt cho phần phiếu khám hoặc phiếu hóa sinh"),
        DisplayName("Tên máy in phiếu KCB")]
        public string TenMayInPhieuKCB { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình tên máy in"),
       Description("Tên máy in hóa đơn"),
       DisplayName("Tên máy in hóa đơn")]
        public string TenMayInHoadon { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình tên máy in"),
       Description("Tên máy in phôi BHYT, biên lai, hóa đơn, đơn thuốc, CLS..."),
       DisplayName("Tên máy in khác")]
        public string TenMayInBienlai { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tên máy in"),
      Description("Tên máy in cuối cùng được sử dụng bởi người dùng máy tính này"),
      DisplayName("Tên máy in cuối cùng")]
        public string Tenmayincuoicung { get; set; }
   
        [Browsable(true), ReadOnly(false), Category("Cấu hình in nhiệt"),
       Description("In nhiệt hoặc in laser"),
       DisplayName("Kiểu in")]
        public KieuIn KieuInPhieuKCB { get; set; }

     
     
         [Browsable(true), ReadOnly(false), Category("Cấu hình xem trước khi in"),
       Description("Xem trước khi in tóm tắt điều trị ngoại trú"),
       DisplayName("Xem trước khi in tóm tắt điều trị ngoại trú")]
        public bool PreviewInTomtatDieutriNgoaitru { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình xem trước khi in"),
       Description("Xem trước khi in phôi BHYT"),
       DisplayName("Xem trước khi in phôi BHYT")]
        public bool PreviewInPhoiBHYT { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình xem trước khi in"),
       Description("Xem trước khi in hóa đơn"),
       DisplayName("Xem trước khi in hóa đơn")]
        public bool PreviewInHoadon { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình xem trước khi in"),
       Description("Xem trước khi in biên lai"),
       DisplayName("Xem trước khi in biên lai")]
        public bool PreviewInBienlai { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình xem trước khi in"),
       Description("Xem trước khi in đơn thuốc"),
       DisplayName("Xem trước khi in đơn thuốc")]
        public bool PreviewInDonthuoc { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình xem trước khi in"),
       Description("Xem trước khi in CLS"),
       DisplayName("Xem trước khi in CLS")]
        public bool PreviewInCLS { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình xem trước khi in"),
       Description("Xem trước khi in phiếu tiếp đón KCB"),
       DisplayName("Xem trước khi in phiếu tiếp đón KCB")]
        public bool PreviewPhieuKCB { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình xem trước khi in Tạm ứng"),
      Description("Xem phiếu tạm ứng trước khi in"),
      DisplayName("Xem phiếu tạm ứng trước khi in")]
        public bool PreviewPhieuTamung { get; set; }
        
    }

    public class NhapkhoProperties
    {
       
      

        [Browsable(true), ReadOnly(false), Category("Cấu Hình nhập kho "),
       Description("Thêm mới phiếu ngay sau khi lưu xong?"),
       DisplayName("Cho phép thêm mới liên tục")]
        public bool Themmoilientuc { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu Hình nhập kho "),
        Description("Nếu là true cho phép, false là ẩn quyển"),
        DisplayName("Quyền được thêm phiếu xuất/nhập")]
        public bool QuyenThemPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình nhập kho "),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được sửa thêm phiếu xuất/nhập")]
        public bool QuyenSuaPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình nhập kho "),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được xóa phiếu xuất/nhập")]
        public bool QuyenXoaPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình nhập kho "),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được xác nhận phiếu xuất/nhập")]
        public bool QuyenXacNhanPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình nhập kho "),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được hủy xác nhận phiếu xuất/nhập")]
        public bool QuyenHuyXacNhanPhieu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình nhập kho "),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền in phiếu")]
        public bool QuyenInphieu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình nhập kho "),
     Description("Cho phép nhập giá BHYT"),
     DisplayName("Cho phép nhập giá BHYT")]
        public bool NhapgiaBHYT { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình nhập kho "),
    Description("Thời gian tự động lưu phiếu nhập kho(tính=giây)"),
    DisplayName("Tự động lưu phiếu nhập kho sau:")]
        public int autosaveAfter { get; set; }
       
        // public string Ke { get; set; }
        public NhapkhoProperties()
        {
            autosaveAfter = 30;
            Themmoilientuc = true;
           
            NhapgiaBHYT = false;
           
            QuyenHuyXacNhanPhieu = true;
            QuyenSuaPhieu = true;
            QuyenThemPhieu = true;
            QuyenXacNhanPhieu = true;
            QuyenXoaPhieu = true;
            QuyenInphieu = true;

            HienthiChietkhauChitiet = false;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình nhập kho "),
      Description("Cho phép nhập chiết khấu khi nhập chi tiết phiếu nhập hay không?"),
      DisplayName("Hiển thị nhập chiết khấu chi tiết")]
        public bool HienthiChietkhauChitiet { get; set; }

       
    }

    public class ChuyenkhoProperties
    {
        public ChuyenkhoProperties()
        {
            Themmoilientuc = true;
            

            QuyenHuyXacNhanPhieu = true;
            QuyenSuaPhieu = true;
            QuyenThemPhieu = true;
            QuyenXacNhanPhieu = true;
            QuyenXoaPhieu = true;
            QuyenInphieu = true;
            Chonngayxacnhan = false;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển nội bộ "),
      Description("true=Hiển thị chọn ngày xác nhận, false= Ngày xác nhận mặc định ngày hiện tại"),
      DisplayName("Hiển thị chọn ngày xác nhận")]
        public bool Chonngayxacnhan { get; set; }

      

        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển nội bộ "),
       Description("Thêm mới phiếu ngay sau khi lưu xong?"),
       DisplayName("Cho phép thêm mới liên tục")]
        public bool Themmoilientuc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển nội bộ "),
        Description("Nếu là true cho phép, false là ẩn quyển"),
        DisplayName("Quyền được thêm phiếu xuất/nhập")]
        public bool QuyenThemPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển nội bộ "),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được sửa thêm phiếu xuất/nhập")]
        public bool QuyenSuaPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển nội bộ "),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được xóa phiếu xuất/nhập")]
        public bool QuyenXoaPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển nội bộ "),
       DisplayName("Quyền được xác nhận phiếu xuất/nhập")]
        public bool QuyenXacNhanPhieu { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển nội bộ "),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền được hủy xác nhận phiếu xuất/nhập")]
        public bool QuyenHuyXacNhanPhieu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu Hình chuyển nội bộ "),
       Description("Nếu là true cho phép, false là ẩn quyển"),
       DisplayName("Quyền in phiếu")]
        public bool QuyenInphieu { get; set; }


        // public string Ke { get; set; }
       
    }

    public class KCBProperties
    {
        public KCBProperties()
        {
            Chophepthanhtoan = false;
            XoaBHYT = false;
            Andangky = true;
            Chieurong = 480;
            Chieucao = 0;
            GoMaDvu = false;
            Tudongthemmoi = true;
            Hoitruockhithanhtoan = true;
            Kieuhienthi = Kieuhienthi.Cahai;
            Thanhtoancaphidichvukemtheo = true;
            Hoikhikhongdangkyphongkham = true;
            FullDelete = false;
            SexInput = true;
            Nhapngaythangnamsinh = false;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
    Description("true=Nhập ngày tháng năm sinh.False= Nhập năm sinh"),
    DisplayName("Nhập ngày tháng năm sinh")]
        public bool Nhapngaythangnamsinh { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
     Description("true=Yêu cầu nhập giới tính khi thêm mới.False= giữ nguyên giới tính vừa chọn"),
     DisplayName("Yêu cầu nhập giới tính")]
        public bool SexInput { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình Xóa đăng ký khám"),
     Description("True=Cho phép xóa đăng ký khám cùng các chỉ định CLS+Đơn thuốc nếu như chưa được thanh toán.False= không cho phép xóa nếu đã có đơn thuốc hoặc chỉ định CLS"),
     DisplayName("Cho phép xóa")]
        public bool FullDelete { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình hỏi xác nhận"),
      Description("True=Nếu Bệnh nhân chưa có phòng khám nào mà nhấn nút Ghi thì sẽ cảnh báo để biết"),
      DisplayName("Hỏi khi không đăng ký phòng khám")]
        public bool Hoikhikhongdangkyphongkham { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình thanh toán"),
     Description("True=Thanh toán cả phí dịch vụ kèm theo (nếu có)"),
     DisplayName("Thanh toán cả phí dịch vụ kèm theo")]
        public bool Thanhtoancaphidichvukemtheo { get; set; }
       
        [Browsable(true), ReadOnly(false), Category("Cấu hình in hóa đơn"),
       Description("Hỏi trước khi thanh toán tiền dăng ký phòng khám "),
       DisplayName("Hỏi trước khi thanh toán")]
        public bool Hoitruockhithanhtoan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình in hóa đơn"),
       Description("Xem trước khi in hóa đơn"),
       DisplayName("Xem trước khi in hóa đơn")]
        public bool PreviewHoadon { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình in hóa đơn"),
        Description("Cho phép thanh toán tiền khám tại quầy tiếp đón"),
        DisplayName("Cho phép thanh toán")]
        public bool Chophepthanhtoan { get; set; }


       
       

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
      Description("Kiểu hiển thị nút chức năng: thanh toán, in phiếu, xóa khám"),
      DisplayName("Kiểu hiển thị nút")]
        public Kieuhienthi Kieuhienthi { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
     Description("Kiểu hiển thị nút chức năng: thanh toán, in phiếu, xóa khám"),
     DisplayName("Kiểu hiển thị nút")]
        public bool CapnhatNeuThanhtoanhet { get; set; }

      

        
        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
         Description("Hiển thị nút trên lưới đăng ký phòng khám"),
         DisplayName("Hiển thị nút trên lưới đăng ký phòng khám")]
        public bool Hienthinuttrenluoi { get; set; }

       

       

        
        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
         Description("Gõ mã=true; Gõ kiểu khám+phòng khám=false"),
         DisplayName("Chọn dịch vụ KCB bằng cách gõ Mã ")]
        public bool GoMaDvu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
         Description("Tự động thêm mới liên tục"),
         DisplayName("Tự động thêm mới liên tục")]
        public bool Tudongthemmoi { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
        Description("Ẩn đăng ký phòng khám"),
        DisplayName("Ẩn đăng ký phòng khám")]
        public bool Andangky { get; set; }

        

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
       Description("true=Xóa thông tin liên quan đến BHYT nếu người dùng chuyển từ BHYT sang Dịch vụ. false= Giữ lại thông tin để có thể chuyển lại"),
       DisplayName("Xóa thông tin BHYT")]
        public bool XoaBHYT { get; set; }

      

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
    Description("Độ rộng chỉ định CLS. Chỉ có tác dụng khi tham số hệ thống KCB_CHOPHEP_CHIDINH_KHONGQUAPHONGKHAM=1<->Có cho phép chỉ định không thông qua bác sĩ khám "),
    DisplayName("Độ rộng chỉ định CLS")]
        public int Chieurong { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng tiếp đón Bệnh nhân"),
   Description("Độ cao vùng đăng ký phòng khám"),
   DisplayName("Độ cao vùng đăng ký phòng khám")]
        public int Chieucao { get; set; }

    }
    public  class  XMLProperties
    {
        public XMLProperties()
        {
            Chonduongdan = "A";
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình xuất File XML"),
        Description("Điền vào đường dẫn File XML xuất ra"),
        DisplayName("Điền vào đường dẫn File XML")]
        public string Chonduongdan { get; set; }
    }
    public class ThuocProperties
    {
        public ThuocProperties()
        {
            TieudeInDanhmucThuoc = "Danh mục thuốc";
            TieudeBaocaoGiathuoc = "GIÁ THUỐC ĐỐI TƯỢNG";
            NhomthuocIngia = true;
            NhomthuocIngia = true;
            LuuNgaysaukhisua = false;

            Chophepsuagiaban = true;
            Chophepsuasoluong = false;
            ApdungCauhinhXuatthuoc = false;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình xuất thuốc khi kê đơn"),
   Description("Cho phép hiển thị cấu hình xuất thuốc khi kê đơn. Mặc định nếu không cho phép thì trừ theo STT bán,Ngày hết hạn gần nhất"),
   DisplayName("Hiển thị cấu hình xuất thuốc khi kê đơn")]
        public bool ApdungCauhinhXuatthuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình cập nhật số lượng tồn"),
    Description("Cho phép sửa giá bán"),
    DisplayName("Cho phép sửa giá bán")]
        public bool Chophepsuagiaban { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình cập nhật số lượng tồn"),
      Description("Cho phép sửa số lượng tồn(Chỉ dùng khi chưa chạy các chức năng nhập xuất thuốc)"),
      DisplayName("Cho phép sửa số lượng tồn(Chỉ dùng khi chưa chạy các chức năng nhập xuất thuốc)")]
        public bool Chophepsuasoluong { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình danh mục thuốc"),
      Description("Nhóm thuốc theo lại khi in danh mục thuốc"),
      DisplayName("Nhóm thuốc khi in danh mục")]
        public bool NhomthuocIndanhmuc { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình danh mục thuốc"),
      Description("Nhóm thuốc theo mục khi in giá"),
      DisplayName("Nhóm thuốc khi in giá")]
        public bool NhomthuocIngia { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình danh mục thuốc"),
      Description("Cho phép sau khi sửa trên lưới, nhấn enter sẽ lưu ngay thông tin"),
      DisplayName("Lưu ngay sau khi sửa trên lưới")]
        public bool LuuNgaysaukhisua { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình danh mục thuốc"),
     Description("Tiêu đề danh mục thuốc"),
     DisplayName("Tiêu đề in danh mục thuốc")]
        public string TieudeInDanhmucThuoc { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình danh mục thuốc"),
    Description("Tiêu đề in giá thuốc"),
    DisplayName("Tiêu đề in giá thuốc")]
        public string TieudeBaocaoGiathuoc { get; set; }


    }
  
    public class ThamKhamProperties
    {
        public ThamKhamProperties()
        {
            IDKho = -1;
            IDKhoVT = -1;
            ChophepIndonthuoc = true;
            TudongthugonCLS = false;
            Hienthinhomthuoc = true;
            Hoitruockhixoathuoc = true;
            Timtrenluoi = true;
            DorongVungKetquaCLS = 300;
            HienthiKetquaCLSTrongluoiChidinh = false;
            ChieucaoluoidanhsachBenhnhan = 0;
            AntimkiemNangcao = true;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng kê thuốc - vật tư tiêu hao"),
     Description("true=Bác sĩ chỉ tìm kiếm theo mã lượt khám. False=Bác sĩ có thể tìm kiếm BN theo ngày đăng ký, tên, số thứ tự khám..."),
     DisplayName("Ẩn tìm kiếm nâng cao")]
        public bool AntimkiemNangcao { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng kê thuốc - vật tư tiêu hao"),
     Description("<=0:Không hiển thị lưới. >0: Chiều cao của vùng lưới hiển thị"),
     DisplayName("Chiều cao lưới danh sách Bệnh nhân")]
        public int ChieucaoluoidanhsachBenhnhan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng kê thuốc - vật tư tiêu hao"),
     Description("True=Hiển thị kết quả CLS trong lưới chỉ định. False=Hiển thị bằng lưới bên cạnh. Với Cận lâm sàng có chi tiết(Tổng phân tích tế bào máu, tổng phân tích nước tiểu...) thì hiển thị ở lưới bên cạnh"),
     DisplayName("Hiển thị kết quả CLS trong lưới chỉ định")]
        public bool HienthiKetquaCLSTrongluoiChidinh { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng kê thuốc - vật tư tiêu hao"),
      Description("Độ rộng vùng xem kết quả CLS"),
      DisplayName("Độ rộng vung xem kết quả CLS")]
        public int DorongVungKetquaCLS { get; set; }

        [Browsable(false), ReadOnly(false), Category("Cấu hình tính năng kê thuốc - vật tư tiêu hao"),
       Description("ID kho vật tư vừa kê"),
       DisplayName("ID kho vật tư vừa kê")]
        public int IDKhoVT { get; set; }


        [Browsable(false), ReadOnly(false), Category("Cấu hình tính năng kê thuốc - vật tư tiêu hao"),
       Description("ID kho vừa kê"),
       DisplayName("ID kho vừa kê")]
        public int IDKho { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thăm khám"),
         Description("true=Khi gõ mã bệnh nhân sẽ ưu tiên tìm trên lưới trước. Nếu không có mới tìm từ CSDL. False=Luôn tìm từ CSDL"),
         DisplayName("Tìm trên lưới trước")]
        public bool Timtrenluoi { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thăm khám"),
        Description("Tự động thu gọn phiếu CLS"),
        DisplayName("Tự động thu gọn phiếu CLS")]
        public bool TudongthugonCLS { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thăm khám"),
      Description("Hiển thị chi tiết đơn thuốc theo nhóm trên lưới"),
      DisplayName("Hiển thị chi tiết đơn thuốc theo nhóm trên lưới")]
        public bool Hienthinhomthuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thăm khám"),
     Description("Luôn hỏi người dùng khi thực hiện thao tác xóa thuốc"),
     DisplayName("Hỏi trước khi xóa thuốc")]
        public bool Hoitruockhixoathuoc { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thăm khám"),
        Description("True=Hiển thị thông tin Bệnh nhân ngay sau khi Click chuột. False=Phải nhấn nút chọn"),
        DisplayName("Xem ngay sau khi click chuột")]
        public bool ViewAfterClick { get; set; }

       

       

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thăm khám"),
       Description("Thêm CLS ngay sau khi chọn"),
       DisplayName("Thêm CLS ngay sau khi chọn")]
        public bool Chidinhnhanh { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thăm khám"),
      Description("Cho phép in đơn thuốc khi tạo đơn"),
      DisplayName("Cho phép in đơn thuốc khi tạo đơn")]
        public bool ChophepIndonthuoc { get; set; }
    }
    public class ThanhtoanProperties
    {
        public ThanhtoanProperties()
        {
            Hienthidichvuchuathanhtoan = true;
            Hienthihuythanhtoan = true;
            Hienthihuyphieuchi = true;
            Hoitruockhihuythanhtoan = true;
            Hoitruockhithanhtoan = true;
            HienthiChietkhauChitiet = false;
            Hienthiphuthu = true;
            HienthiChiTra = true;
            CachChietkhau = 0;
            HienthidichvuNgaysaukhithanhtoan = true;
            AnTabDaThanhtoan = true;
            TaodulieuDCTkhiInphieuDCT = false;
            SearchWhenStart = true;
            HideStatusBar = true;
            AutoTab = false;
            CachhienthidulieuNoitru = DisplayType.Tatca;
            ChieucaohienthiLuoidanhsachBNthanhtoan = 0;
            AutoSelectpatientAfterSearch = true;
            HienthidichvuNgaysaukhitratien = true;
        }
        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
Description("true =Chọn ngay bệnh nhân nếu kết quả tìm kiếm có duy nhất 1 bệnh nhân đó.False= Người dùng tự tay chọn trên lưới(Chú ý nếu để false thì phải bật tính năng hiển thị lưới)"),
DisplayName("Chọn ngay bệnh nhân nếu tìm kiếm được")]
        public bool AutoSelectpatientAfterSearch { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình nội trú"),
 Description("Chiều cao của lưới hiển thị. Để giá trị<=0 nếu không muốn hiển thị lưới danh sách BN"),
 DisplayName("Hiển thị lưới danh sách BN thanh toán")]
        public int ChieucaohienthiLuoidanhsachBNthanhtoan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình nội trú"),
 Description("Hiển thị tất cả- chỉ dữ liệu tạo khi nội trú- chỉ dữ liệu tạo ngoại trú"),
 DisplayName("Cách hiển thị dữ liệu thanh toán nội trú")]
        public DisplayType CachhienthidulieuNoitru { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng in phôi BHYT"),
  Description("true=Cho phép in phiếu ĐCT trước khi in phôi BHYT.False= Cần in phôi BHYT xong mới được in phiếu ĐCT"),
  DisplayName("Cho phép in phiếu ĐCT trước khi in phôi BHYT")]
        public bool TaodulieuDCTkhiInphieuDCT { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
 Description("true = Tự động chuyển về Tab thanh toán ngay sau khi chọn bệnh nhân khác. false= giữ nguyên Tab đang xem "),
 DisplayName("Chuyển Tab sau khi chọn bệnh nhân")]
        public bool AutoTab { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
  Description("Tự động tìm kiếm ngay khi vào chức năng thanh toán"),
  DisplayName("Tự động tìm kiếm ngay khi vào chức năng thanh toán")]
        public bool SearchWhenStart { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
Description("Ẩn thanh trạng thái StatusBar"),
DisplayName("Ẩn thanh trạng thái StatusBar")]
        public bool HideStatusBar { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
     Description("Cách thức chiết khấu chi tiết:0=Nhập % chiết khấu;1=Nhập tiền chiết khấu;2=% -->Tiền;Tiền-->%"),
     DisplayName("Cách thức chiết khấu")]
        public Int16 CachChietkhau { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
      Description("Hiển thị các mục liên quan đến chi trả(giá BN chi trả, BHYT chi trả, Tiền BN chi trả, tiền BHYT chi trả...)"),
      DisplayName("Hiển thị mục chi trả")]
        public bool HienthiChiTra { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
      Description("Hiển thị các mục liên quan đến phụ thu(giá phụ thu, tiền phụ thu...)"),
      DisplayName("Hiển thị phụ thu")]
        public bool Hienthiphuthu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
       Description("Cho phép chiết khấu chi tiết theo từng chi tiết dịch vụ trên lưới"),
       DisplayName("Cho phép chiết khấu chi tiết")]
        public bool HienthiChietkhauChitiet { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
         Description("chỉ hiển thị các dịch vụ chưa thanh toán tại Tab Thông tin chi tiết(F2)"),
         DisplayName("Hiển thị dịch vụ chưa thanh toán")]
        public bool Hienthidichvuchuathanhtoan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
        Description("Hiển thị dịch vụ vừa thanh toán ngay sau khi thanh toán xong"),
        DisplayName("Hiển thị dịch vụ vừa thanh toán ngay sau khi thanh toán xong")]
        public bool HienthidichvuNgaysaukhithanhtoan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình phiếu chi"),
        Description("Hiển thị dịch vụ vừa trả tiền ngay sau khi trả tiền xong"),
        DisplayName("Hiển thị dịch vụ vừa trả tiền ngay sau khi trả tiền xong")]
        public bool HienthidichvuNgaysaukhitratien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
      Description("Ẩn tab đã thanh toán"),
      DisplayName("Ẩn tab đã thanh toán")]
        public bool AnTabDaThanhtoan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng hủy phiếu chi"),
       Description("Xem trước khi hủy phiếu chi"),
       DisplayName("Xem trước khi hủy phiếu chi")]
        public bool Hienthihuyphieuchi { get; set; }


        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng hủy thanh toán"),
        Description("Xem trước khi hủy thanh toán"),
        DisplayName("Xem trước khi hủy thanh toán")]
        public bool Hienthihuythanhtoan { get; set; }



        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng hủy thanh toán"),
       Description("Hỏi trước khi hủy thanh toán"),
       DisplayName("Hỏi trước khi hủy thanh toán")]
        public bool Hoitruockhihuythanhtoan { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình tính năng thanh toán"),
      Description("Hỏi trước khi thanh toán"),
      DisplayName("Hỏi trước khi thanh toán")]
        public bool Hoitruockhithanhtoan { get; set; }
    }
}