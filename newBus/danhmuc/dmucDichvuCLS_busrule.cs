using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VNS.HIS.DAL;
using SubSonic;
using VNS.Libs;
using System.Transactions;

namespace VNS.HIS.NGHIEPVU
{
   public class dmucDichvuCLS_busrule
    {


       public static string Insert(DmucDichvuclsChitiet objClsChitiet, QheCamchidinhCLSChungphieuCollection lstQhe)
       {
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       objClsChitiet.Save();
                       if (!objClsChitiet.IsNew)
                       {
                           new Update(KcbChidinhclsChitiet.Schema)
                     .Set(KcbChidinhclsChitiet.Columns.IdDichvu).EqualTo(objClsChitiet.IdDichvu)
                     .Where(KcbChidinhclsChitiet.Columns.IdChitietdichvu).IsEqualTo(objClsChitiet.IdChitietdichvu)
                     .Execute();

                           //new Update(KcbThanhtoanChitiet.Schema)
                           //    .Set(KcbThanhtoanChitiet.Columns.IdDichvu).EqualTo(objClsChitiet.IdDichvu)
                           //    .Set(KcbThanhtoanChitiet.Columns.TenChitietdichvu).EqualTo(objClsChitiet.TenChitietdichvu)
                           //    .Where(KcbThanhtoanChitiet.Columns.IdLoaithanhtoan).IsEqualTo(2)
                           //    .And(KcbThanhtoanChitiet.Columns.IdChitietdichvu).IsEqualTo(objClsChitiet.IdChitietdichvu)
                           //    .Execute();

                       }
                       new Delete().From(QheCamchidinhCLSChungphieu.Schema).Where(QheCamchidinhCLSChungphieu.Columns.IdChitietdichvu).IsEqualTo(objClsChitiet.IdChitietdichvu).Execute();
                       new Delete().From(QheCamchidinhCLSChungphieu.Schema).Where(QheCamchidinhCLSChungphieu.Columns.IdChitietdichvuCamchidinhcung).IsEqualTo(objClsChitiet.IdChitietdichvu).Execute();
                       foreach (QheCamchidinhCLSChungphieu obj in lstQhe)
                       {
                           obj.IdChitietdichvu = objClsChitiet.IdChitietdichvu;
                       }
                       lstQhe.SaveAll();
                   }
                   scope.Complete();
               }
               return string.Empty;
           }
           catch (Exception ex)
           {
               return ex.Message;
           }

       }
       public static string Delete(DmucDichvuclsChitiet objClsChitiet)
       {
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                      
                   }
                   scope.Complete();
               }
               return string.Empty;
           }
           catch (Exception ex)
           {
               return ex.Message;
           }

       }
       
       
    }
}
