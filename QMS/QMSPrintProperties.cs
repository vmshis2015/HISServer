using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VNS.Libs;
using System.Drawing;
using System.Xml.Serialization;
namespace VNS.QMS
{


    public class QMSProperties
    {
             [Browsable(true), ReadOnly(false), Category("Kích thước"),
         Description("Kích thước QMS buồng khám"),
         DisplayName("Kích thước QMS buồng khám")]
        public Size mySize { get; set; }
         [Browsable(true), ReadOnly(false), Category("Kích thước"),
         Description("Độ cao phần hiển thị số"),
         DisplayName("Độ cao phần hiển thị số")]
        public int NumberHeigh { get; set; }
         [Browsable(true), ReadOnly(false), Category("Kích thước"),
         Description("Độ cao nút lấy số"),
         DisplayName("Độ cao nút lấy số")]
        public int ButtonHeigh { get; set; }

         [XmlIgnore]
         [Browsable(true), ReadOnly(false), Category("Kích thước"),
          Description("Cỡ chữ số QMS"),
          DisplayName("MenuFont")]
         public Font NumberF { get; set; }

         [Browsable(false)]
         [XmlElement("NumberFName")]
         public string NumberFName
         {
             get
             {
                 return NumberF.Name;
             }
             set
             {
                 NumberF = new Font(value, NumberFsize, NumberFstyle);
             }
         }

         [Browsable(false)]
         [XmlElement("NumberFsize")]
         public float NumberFsize
         {
             get
             {
                 return NumberF.Size;
             }
             set
             {
                 NumberF = new Font(NumberFName, value, NumberFstyle);
             }
         }

         [Browsable(false)]
         [XmlElement("NumberFstyle")]
         public FontStyle NumberFstyle
         {
             get
             {
                 return NumberF.Style;
             }
             set
             {
                 NumberF = new Font(NumberFName, NumberFsize, value);
             }
         }

         [XmlIgnore]
         [Browsable(true), ReadOnly(false), Category("Kích thước"),
          Description("Cỡ chữ nút lấy số"),
          DisplayName("MenuFont")]
         public Font ButtonF { get; set; }

         [Browsable(false)]
         [XmlElement("ButtonFName")]
         public string ButtonFName
         {
             get
             {
                 return ButtonF.Name;
             }
             set
             {
                 ButtonF = new Font(value, ButtonFsize, ButtonFstyle);
             }
         }

         [Browsable(false)]
         [XmlElement("ButtonFsize")]
         public float ButtonFsize
         {
             get
             {
                 return ButtonF.Size;
             }
             set
             {
                 ButtonF = new Font(ButtonFName, value, ButtonFstyle);
             }
         }

         [Browsable(false)]
         [XmlElement("ButtonFstyle")]
         public FontStyle ButtonFstyle
         {
             get
             {
                 return ButtonF.Style;
             }
             set
             {
                 ButtonF = new Font(ButtonFName, ButtonFsize, value);
             }
         }


        [Browsable(true), ReadOnly(false), Category("Tên Bệnh Viện"),
         Description("Cấu hình hiển thị tên bệnh viện"),
         DisplayName("Cấu hình hiển thị tên bệnh viện")]
        public string TenBenhVien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy hiển thị"),
         Description("Mã quầy tiếp đón cần hiển thị số"),
         DisplayName("Mã quầy tiếp đón cần  ")]
        public string MaQuay { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy hiển thị"),
         Description("Tên quầy tiếp đón cần hiển thị số"),
         DisplayName("Tên quầy tiếp đón cần  ")]
        public string TenQuay { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy hiển thị"),
        Description("Hiển thị khoa khám bệnh hoặc khoa yêu cầu"),
        DisplayName("Mã khoa thực hiện")]
        public string MaKhoaThuchien { get; set; }
        [Browsable(true), ReadOnly(false), Category("Cấu hình quầy hiển thị"),
         Description("Cỡ chữ của hệ thống"),
         DisplayName("Font size ")]
        public Int32 FontSize { get; set; }


      

        [Browsable(true), ReadOnly(false), Category("Cấu hình in máy in nhiệt "),
         Description("Lấy thông tin máy in nhiệt"),
         DisplayName("Tên máy in nhiệt cần in  ")]
        public string PrinterName { get; set; }

        [Browsable(true), ReadOnly(false), Category("Cấu hình in máy in nhiệt "),
         Description("Trạng thái in"),
         DisplayName("Trạng thái in")]
        public bool PrintStatus { get; set; }

      


        [ReadOnly(false), DisplayName("Mã khoa lấy số khoa KCB"),
        Browsable(true), Category("Cấu hình phát số chờ tiếp đón KCB "),
        Description("Mã khoa KCB")]
        public string MaKhoaKhamBenh { get; set; }

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

        [Browsable(true), Category("Cấu hình phát số chờ tiếp đón KCB "),
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
        [Browsable(false), ReadOnly(false), Category("Cấu hình màu sắc"),
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
        [Browsable(false), ReadOnly(false), Category("Cấu hình màu sắc"),
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
        [Browsable(false), ReadOnly(false), Category("Cấu hình màu sắc"),
         Description("Màu nền số khám yêu cầu/BHYT"),
         DisplayName("Màu nền khám yêu cầu/BHYT")]
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
        [Browsable(false), ReadOnly(false), Category("Cấu hình màu sắc"),
         Description("Màu chữ số khám yêu cầu/BHYT"),
         DisplayName("Màu chữ khám yêu cầu/BHYT")]
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
        [Browsable(false), ReadOnly(false), Category("Cấu hình màu sắc"),
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
        [Browsable(false), ReadOnly(false), Category("Cấu hình màu sắc"),
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
        [Browsable(true), ReadOnly(false), Category("Thời gian nghỉ"),
    Description("Thời gian nghỉ giữa mỗi lần lấy số tính bằng mili giây."),
    DisplayName("Thời gian nghỉ giữa mỗi lần lấy số")]
        public int SleepTime { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
     Description("Tên hiển thị lấy số dịch vụ"),
     DisplayName("Tên hiển thị lấy số dịch vụ")]
        public string tensodichvu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
     Description("Tên hiển thị lấy số BHYT"),
     DisplayName("Tên hiển thị lấy số BHYT")]
        public string tensobhyt { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
     Description("Tên hiển thị lấy số khác"),
     DisplayName("Tên hiển thị lấy số khác")]
        public string tensokhac { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
    Description("Tên hiển thị lấy số khác ưu tiên"),
    DisplayName("Tên hiển thị lấy số khác ưu tiên")]
        public string tensokhacUutien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
    Description("Tên hiển thị lấy số ưu tiên"),
    DisplayName("Tên hiển thị lấy số ưu tiên")]
        public string tensouutien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
    Description("Tên hiển thị trên phiếu QMS dịch vụ(hoặc số thường)"),
    DisplayName("Tên hiển thị trên phiếu QMS dịch vụ")]
        public string Sodichvu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
     Description("Tên hiển thị trên phiếu QMS BHYT(hoặc số thường)"),
     DisplayName("Tên hiển thị trên phiếu QMS BHYT")]
        public string Sobhyt { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
     Description("Tên hiển thị trên phiếu QMS khác(hoặc số tiêm chủng)"),
     DisplayName("Tên hiển thị trên phiếu QMS khác(hoặc số tiêm chủng)")]
        public string Sokhac { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
    Description("Tên hiển thị trên phiếu QMS ưu tiên khác(hoặc số tiêm chủng)"),
    DisplayName("Tên hiển thị trên phiếu QMS ưu tiên khác(hoặc số tiêm chủng)")]
        public string SokhacUutien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
    Description("Tên hiển thị trên phiếu QMS Ưu tiên"),
    DisplayName("Tên hiển thị trên phiếu QMS Ưu tiên")]
        public string Souutien { get; set; }
        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
   Description("Tên hiển thị trên phiếu QMS khoa KCB yêu cầu"),
   DisplayName("Tên hiển thị trên phiếu QMS khoa KCB yêu cầu")]
        public string SoYeucau { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
       Description("Mã đối tượng lấy số khác: ALL,DV hoặc BHYT"),
     DisplayName("Mã đối tượng lấy số DV")]
        public string madoituongdichvu { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
       Description("Mã đối tượng lấy số khác: ALL,DV hoặc BHYT"),
     DisplayName("Mã đối tượng lấy số BHYT")]
        public string madoituongbhyt { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
     Description("Mã đối tượng lấy số khác: ALL,DV hoặc BHYT"),
     DisplayName("Mã đối tượng lấy số khác")]
        public string madoituongkhac { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
    Description("Mã đối tượng lấy số ưu tiên: ALL,DV hoặc BHYT"),
    DisplayName("Mã đối tượng lấy số ưu tiên")]
        public string madoituonguutien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
  Description("Hiển thị lấy số Ưu tiên"),
  DisplayName("Hiển thị lấy số Ưu tiên")]
        public bool hienthiLaysoUutien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
    Description("Hiển thị lấy số DV"),
    DisplayName("Hiển thị lấy số DV")]
        public bool hienthiLaysoDV { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
    Description("Hiển thị lấy số khác"),
    DisplayName("Hiển thị lấy số khác")]
        public bool hienthiLaysokhac { get; set; }



        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
   Description("Hiển thị lấy số khác ưu tiên"),
   DisplayName("Hiển thị lấy số khác ưu tiên")]
        public bool hienthiLaysokhacUutien { get; set; }

        [Browsable(true), ReadOnly(false), Category("Hiển thị(QMS không chọn buồng)"),
    Description("Hiển thị lấy số BHYT"),
    DisplayName("Hiển thị lấy số BHYT")]
        public bool hienthiLaysoBHYT { get; set; }

        [Browsable(true), ReadOnly(false), Category("Phím tắt"),
   Description("Sử dụng các phím tắt để lấy số: F1=Số thường;F2=Số ưu tiên thường;F3=Số khác;F4=Số ưu tiên khác;F5=Số thường BHYT;F6=Số khoa YC"),
   DisplayName("Sử dụng phím chức năng")]
        public bool EnableFKey { get; set; }


        public QMSProperties()
        {
            EnableFKey = false;
            madoituonguutien = "ALL";
            madoituongdichvu = "DV";
            madoituongbhyt = "BHYT";
            madoituongkhac = "ALL";
            tensouutien = "Lấy số Ưu tiên";
            tensobhyt = "Lấy số BHYT";
            tensodichvu = "Lấy số Dịch vụ";
            tensokhac = "Lấy số tiêm chủng";
            tensokhacUutien = "Lấy số tiêm chủng ưu tiên";
            hienthiLaysokhac = true;
            hienthiLaysoBHYT = true;
            hienthiLaysoDV = true;
            hienthiLaysoUutien = true;
            hienthiLaysokhacUutien = true;
            TenBenhVien = "BỆNH VIỆN NỘI TIẾT TRUNG ƯƠNG";
            MaQuay = "QUAYSO_1";
            TenQuay = "Quầy tiếp đón số 1";
            FontSize = 400;
            PrinterName = Utility.GetDefaultPrinter();
            PrintStatus = true;
            Chopheplaysouutien = false;
            Chilaysouutien = false;
            MaKhoaThuchien = "KKB";
            MaDoituongKCB = "ALL";
            NumberF = new Font("Arial", 50, FontStyle.Bold);
            ButtonF = new Font("Arial", 20, FontStyle.Bold);
            MaKhoaKhamBenh = "KKB";
            MaKhoaYeuCau = "KYC";
            TenKhoaKhamBenh = "KHOA KHÁM CHỮA BỆNH (TẦNG 1)";
            TenKhoaYeuCau = "KHOA KHÁM YÊU CẦU (TẦNG 2 PHÒNG A-205)";

        }
    }
}

