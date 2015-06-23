using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using System.Xml;

using System.Data;
using System.Windows.Forms;
using VNS.Libs;
using VNS.Properties;

namespace VNS.Core.Classes
{
  public class ConnectionSQL
  {
     
      public bool ReadConfig()
      {
          try
          {
              PropertyLib._ConfigProperties = PropertyLib.GetConfigProperties();
              globalVariables.ServerName = PropertyLib._ConfigProperties.DataBaseServer;
              globalVariables.sUName = PropertyLib._ConfigProperties.UID;
              globalVariables.sPwd = PropertyLib._ConfigProperties.PWD;
              globalVariables.sDbName = PropertyLib._ConfigProperties.DataBaseName;
              globalVariables.sMenuStyle = "DOCKING"; // Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["INTERFACEDISPLAY"], "MENU").ToUpper();

              globalVariables.MA_KHOA_THIEN = PropertyLib._ConfigProperties.MaKhoa;
              globalVariables.MA_PHONG_THIEN = PropertyLib._ConfigProperties.Maphong;
              globalVariables.SOMAYLE = PropertyLib._ConfigProperties.Somayle;
              globalVariables.MIN_STT=PropertyLib._ConfigProperties.Min;
              globalVariables.MAX_STT = PropertyLib._ConfigProperties.Max;
              return true;
              //DataSet dsConfigXML = new DataSet();

              //string sPathXML = Application.StartupPath + @"\Config.XML";
              //if (System.IO.File.Exists(sPathXML))
              //{
              //    dsConfigXML.ReadXml(sPathXML);
              //    if (dsConfigXML.Tables[0].Rows.Count > 0)
              //    {
              //        //globalVariables.gv_MaDonVi = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["BranchID"], ".");
              //        globalVariables.ServerName = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["SERVERADDRESS"], ".");
              //        globalVariables.sUName = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["USERNAME"], "sa");
              //        globalVariables.sPwd = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["PASSWORD"], "sa");
              //        globalVariables.sDbName = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["DATABASE_ID"], "RISLINK_DB");
              //        globalVariables.sMenuStyle = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["INTERFACEDISPLAY"], "MENU").ToUpper();
              //        globalVariables.MIN_STT = Utility.Int32Dbnull(dsConfigXML.Tables[0].Rows[0]["MIN_STT"], 1);
              //        globalVariables.MAX_STT = Utility.Int32Dbnull(dsConfigXML.Tables[0].Rows[0]["MAX_STT"], 99999999);
              //        string[] _value = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["LOCALALIAS"], "KYC-108-145").ToUpper().Split('-');
              //        if (_value.Length != 4)
              //        {
              //            Utility.ShowMsg("Giá trị Localalias trong file Config phải có dạng Mã khoa thực hiện-Mã phòng khám-Số máy lẻ của khoa-Danh sách kho thuốc thanh toán. Bạn nên thoát chương trình và vào lại sau khi chỉnh sửa xong");
              //            return false;
              //        }
              //        globalVariables.MA_KHOA_THIEN = _value[0];
              //        globalVariables.MA_PHONG_THIEN = _value[1];
              //        globalVariables.SOMAYLE = _value[2];
              //        globalVariables.DANHSACHKHO = _value[3];
              //        return true;
              //    }
              //    return true;
              //}

              //Utility.ShowMsg("Không tìm thấy File Config.XML , Bạn xem lại", "Thông báo", MessageBoxIcon.Error);
              //return false;
          }
          catch (Exception ex)
          {
              Utility.ShowMsg("Lỗi khi load file Config.xml\n" + ex.Message);
              return false;
          }
      }
     public ConnectionSQL()
     {
     }
    
      public  string KhoiTaoKetNoi()
      {

          string strSQL =
              string.Format(
                  "workstation id={0};packet size=4096;data source={0};persist security info=False;initial catalog={1};uid={2};pwd={3}",
                  globalVariables.ServerName, globalVariables.sDbName, globalVariables.sUName, globalVariables.sPwd);

          return strSQL;


      }

   
     
     
    }
}
