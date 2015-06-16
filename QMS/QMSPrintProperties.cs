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
        [Browsable(true), ReadOnly(false), Category("Cấu hình màu sắc"),
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
        [Browsable(true), ReadOnly(false), Category("Thời gian nghỉ"),
    Description("Thời gian nghỉ giữa mỗi lần lấy số tính bằng mili giây."),
    DisplayName("Thời gian nghỉ giữa mỗi lần lấy số")]
        public int SleepTime { get; set; }

        public QMSProperties()
        {
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

