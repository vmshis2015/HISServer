using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using VNS.Libs;
using VNS.HIS.DAL;
namespace VMS.HIS.HLC.ASTM
{
    public class RocheCommunication
    {
        public static DataTable ReadOrders(string FilePath)
        {
            DataTable dt = new DataTable("Order");
            dt.Columns.AddRange(new DataColumn[] { 
                new DataColumn("Header", typeof(string)), 
                new DataColumn("P", typeof(string)),
                new DataColumn("O", typeof(string)) ,
            new DataColumn("MA", typeof(string)),
            new DataColumn("KQ", typeof(string))});

            List<string> lstLines = new List<string>();
            try
            {
                using (new NetworkConnection(Path.GetDirectoryName(FilePath), Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false), Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
                using (StreamReader _reader = new StreamReader(FilePath))
                {
                    while (_reader.Peek() > -1)
                    {
                        lstLines.Add(_reader.ReadLine());
                    }
                    _reader.BaseStream.Flush();
                    _reader.Close();
                }
                var patientinfor = from p in lstLines.AsEnumerable()
                                   where p.StartsWith("P")
                                   select p;
                var HeaderInfor = from p in lstLines.AsEnumerable()
                                 where p.StartsWith("H")
                                 select p;
                List<string> lstOLines = (from p in lstLines.AsEnumerable()
                                           where p.StartsWith("O")
                                           select p).ToList<string>();
                foreach (string Ol in lstOLines)
                {
                    string[] arrValues = Ol.Split('|');
                    string ma_chidinh = arrValues[2];
                    string ngay_chidinh = arrValues[6];
                    string[] lstMaXN = arrValues[4].Replace("^","").Split('\\');
                    foreach (string ma in lstMaXN)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Header"] = HeaderInfor.FirstOrDefault();
                        dr["P"] = patientinfor.FirstOrDefault();
                        dr["O"] = "O|1|" + ma_chidinh + "|ALL|||" + ngay_chidinh + "|||||||||||||||||||F|";
                        dr["MA"] = ma;
                        dr["KQ"] = "";
                        dt.Rows.Add(dr);
                    }
                }
                dt.AcceptChanges();
                return dt;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }
        public static int WriteResultMessage(string ResultFolderPath, DataTable dtData)
        {
            if (!Directory.Exists(ResultFolderPath)) return -1;
            if (dtData == null || dtData.Rows.Count <= 0) return -2;
            string Header = dtData.Rows[0]["Header"].ToString();
            string patientInfor = dtData.Rows[0]["P"].ToString();
            List<string> LstOrderItems = (from p in dtData.AsEnumerable()
                                          select p["O"].ToString()).Distinct().ToList<string>();
            string Footer = "L|1|N";
            string CommentLine = "C|1||Test 11150 comment|";
            string orderfileName = Utility.FixedFolder(ResultFolderPath) + "Result_Message.txt";
            using (new NetworkConnection(ResultFolderPath, Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false), Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
            using (StreamWriter _writer = new StreamWriter(orderfileName, false))
            {
                _writer.WriteLine(Header);
                _writer.WriteLine(patientInfor);
                foreach (string orderitems in LstOrderItems)
                {
                    _writer.WriteLine(orderitems);
                    DataRow[] arrDr = dtData.Select("O='" + orderitems + "'");
                    int idx = 1;
                    foreach (DataRow dr in arrDr)
                    {
                        string _R = "R|" + idx.ToString() + "|^^^" + dr["MA"].ToString() + "^^^^|" + dr["KQ"].ToString() + "|mmol/L||||F||||20121031115507||";
                        _writer.WriteLine(_R);
                        _writer.WriteLine(CommentLine);
                        idx++;
                    }
                }
                _writer.WriteLine(Footer);
                _writer.Flush();
                _writer.Close();
            }
            return 0;
        }
        public static int WriteOrderMessage(string orderFolderPath, DataTable dtData)
        {
            if (!Directory.Exists(orderFolderPath)) return -1;
            if (dtData==null || dtData.Rows.Count <= 0) return -2;
            string Header = @"H|\^&|||VMS-HIS|||||PSM||P||" + Utility.GetYYYYMMDDHHMMSS(globalVariables.SysDate);
            string patientInfor = "";
            List<string> LstOrderItems = new List<string>();
            string Footer = "L|1|F";
            string ma_luotkham = Utility.sDbnull(dtData.Rows[0][KcbLuotkham.Columns.MaLuotkham], "");
            string ten_benhnhan = Utility.DoTrim(Utility.UnSignedCharacter(Utility.sDbnull(dtData.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], ""))).ToUpper();
            string[] arrValues = ten_benhnhan.Split(' ');
            string Ho = arrValues[0];
            string ten = ten_benhnhan.Substring(ten_benhnhan.IndexOf(' ') + 1);
            ten_benhnhan = Ho + "^" + ten;

            patientInfor = "P|1||" + ma_luotkham + "||" + ten_benhnhan + "||" + Utility.sDbnull(dtData.Rows[0]["sngay_sinh"]) + "|" + Utility.sDbnull(dtData.Rows[0]["sgioi_tinh"]);
            List<string> q = (from p in dtData.AsEnumerable()
                              select Utility.sDbnull(p["ma_chidinh"])).Distinct().ToList<string>();
            int i = 1;
            
            foreach (string ma_chidinh in q)
            {
                DataRow[] arrChitiet = dtData.Select("ma_chidinh='" + ma_chidinh + "'");
                string orderItems = "O|" + i.ToString() + "|" + ma_chidinh + "||";
                string ngay_chidinh="";
                string ma_khoa_chidinh="";
                foreach (DataRow drchitiet in arrChitiet)
                {
                    if (ngay_chidinh == "")
                    {
                        DateTime ngaycd = (DateTime)drchitiet["ngay_chidinh"];
                        ngay_chidinh = Utility.GetYYYYMMDDHHMMSS(ngaycd);
                    }
                    if (ma_khoa_chidinh == "")
                        ma_khoa_chidinh = Utility.sDbnull(drchitiet["ma_khoa_chidinh"], "KKB");
                    string ma_xetnghiem = "^^^" + Utility.sDbnull(drchitiet["ma_xetnghiem"]) + @"\";
                    orderItems = orderItems + ma_xetnghiem;
                }
                //Loại bỏ dấu \ cuối cùng
                orderItems = orderItems.Substring(0, orderItems.Length - 1);
                orderItems += "|R|" + ngay_chidinh + "|||||A|||||" + ma_khoa_chidinh + "|||||||||O";
                LstOrderItems.Add(orderItems);
            }
            if (!orderFolderPath.EndsWith(@"\")) orderFolderPath += @"\";
            string seqNum = Utility.GetSequence().ToString();
            if (seqNum == "-1") seqNum = Microsoft.VisualBasic.Strings.Right("000000" + dtData.Rows[0][KcbChidinhcl.Columns.IdChidinh].ToString(), 6);
            else
                seqNum = Microsoft.VisualBasic.Strings.Right("000000" + seqNum, 6);
            string orderfileName = orderFolderPath + "Order" + seqNum + ".txt";
            using (new NetworkConnection(orderFolderPath, Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false), Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
            using (StreamWriter _writer = new StreamWriter(orderfileName,false))
            {
                _writer.WriteLine(Header);
                _writer.WriteLine(patientInfor);
                foreach (string orderitems in LstOrderItems)
                    _writer.WriteLine(orderitems);
                _writer.WriteLine(Footer);
                _writer.Flush();
                _writer.Close();
            }
            return 0;
        }

    }
    public class DeviceHelper 
    {
        #region Fields


        private static readonly string[] VietnameseSigns =
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        private static readonly string _barcodeType = string.Empty;

        #endregion

      
        static DeviceHelper()
        {
            new DeviceHelper();
        }

        #region Constant

        public const char NULL = (char)0;
        public const char STX = (char)2;
        public const char ETX = (char)3;
        public const char EOT = (char)4;
        public const char ENQ = (char)5;
        public const char ACK = (char)6;
        public const char CR = (char)13;
        public const char LF = (char)10;
        public const char VT = (char)11;
        public const char NAK = (char)21;
        public const char ETB = (char)23;
        public const char FS = (char)28;
        public const char GS = (char)29;
        public const char RS = (char)30;
        public const char SOH = (char)1;
        public const char SYN = (char)22;
        public const char DC1 = (char)17;
        public const char DC2 = (char)18;
        public const char DC3 = (char)19;
        public const char DC4 = (char)20;
        public static readonly string CRLF = String.Format("{0}{1}", CR, LF);

        #endregion
       
    }
}
