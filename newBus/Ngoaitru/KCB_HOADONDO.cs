using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

namespace VNS.HIS.BusRule.Classes
{
    public class KCB_HOADONDO
    {
       /// <summary>
       /// hàm thực hiệnv iệc update thông tin của biên lại hóa đơn 
       /// </summary>
       /// <param name="objhoalog"></param>
       /// <param name="objPayment"></param>
       /// <param name="HOADON_CAPPHAT_ID"></param>
       /// <returns></returns>
       public ActionResult UpdateBienLaiHoaDon(HoadonLog objhoalog, KcbThanhtoan objPayment, int HOADON_CAPPHAT_ID)
       {
           try
           {
               using (var Scope = new TransactionScope())
               {
                   using (var dbScope = new SharedDbConnectionScope())
                   {
                       objhoalog.IdThanhtoan = Utility.Int32Dbnull(objPayment.IdThanhtoan);
                       objhoalog.IdBenhnhan = Utility.Int32Dbnull(objPayment.IdBenhnhan);
                       objhoalog.MaLuotkham = Utility.sDbnull(objPayment.MaLuotkham);
                       objhoalog.MaNhanvien = globalVariables.UserName;
                       objhoalog.NgayIn = globalVariables.SysDate;
                       objhoalog.TrangThai = 0;
                       objhoalog.IsNew = true;
                       objhoalog.Save();
                       new Update(HoadonCapphat.Schema)
                           .Set(HoadonCapphat.Columns.SerieHientai).EqualTo(objhoalog.Serie)
                           .Set(HoadonCapphat.Columns.TrangThai).EqualTo(1)
                           .Where(HoadonCapphat.Columns.IdCapphat).IsEqualTo(HOADON_CAPPHAT_ID).
                           Execute();
                   }
                   Scope.Complete();
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {

               return ActionResult.Error;
           }
           
       }

       public static int DeleteRedInVoice(int IdHdonLog)
       {
          return new Delete().From(HoadonLog.Schema)
                                               .Where(HoadonLog.Columns.IdHdonLog)
                                               .IsEqualTo(IdHdonLog)
                                               .Execute();
       }
       public static int UpdateTrangThaiHoaDon(int IdHdonLog)
       {
           
           return 
               new Update(HoadonLog.Schema).Set(HoadonLog.Columns.TrangThai).EqualTo(1).Where(
                             HoadonLog.Columns.IdHdonLog).IsEqualTo(IdHdonLog).
                             Execute();
       }
    }
}
