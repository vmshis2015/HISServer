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


       public static string Insert(DmucDichvuclsChitiet objClsChitiet, QheCamchidinhChungphieuCollection lstQhe,QheDichvuMauketquaCollection lstqhemauKq)
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
                       new Delete().From(QheDichvuMauketqua.Schema)
                           .Where(QheDichvuMauketqua.Columns.IdDichvuChitiet).IsEqualTo(objClsChitiet.IdChitietdichvu)
                           .Execute();
                       new Delete().From(QheCamchidinhChungphieu.Schema)
                           .Where(QheCamchidinhChungphieu.Columns.IdDichvu).IsEqualTo(objClsChitiet.IdChitietdichvu)
                           .And(QheCamchidinhChungphieu.Columns.Loai).IsEqualTo(0)
                           .Execute();
                       new Delete().From(QheCamchidinhChungphieu.Schema)
                           .Where(QheCamchidinhChungphieu.Columns.IdDichvuCamchidinhchung).IsEqualTo(objClsChitiet.IdChitietdichvu)
                           .And(QheCamchidinhChungphieu.Columns.Loai).IsEqualTo(0)
                           .Execute();
                       foreach (QheCamchidinhChungphieu obj in lstQhe)
                       {
                           obj.IdDichvu = objClsChitiet.IdChitietdichvu;
                       }
                       lstQhe.SaveAll();
                       foreach (QheDichvuMauketqua obj in lstqhemauKq)
                       {
                           obj.IdDichvuChitiet = objClsChitiet.IdChitietdichvu;
                       }
                       lstqhemauKq.SaveAll();
                       
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
