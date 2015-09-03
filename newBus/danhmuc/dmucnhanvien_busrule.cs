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
   public class dmucnhanvien_busrule
    {


       public static string Insert(DmucNhanvien objDmucNhanvien, QheNhanvienKhoCollection lstQhekho, QheBacsiKhoaphongCollection lstQhekhoa, QheNhanvienQuyensudungCollection lstQheQuyensudung, QheNhanvienDanhmucCollection lstQheDmuc)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        objDmucNhanvien.Save();
                        new Delete().From(QheNhanvienDanhmuc.Schema).Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        new Delete().From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        new Delete().From(QheBacsiKhoaphong.Schema).Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        new Delete().From(QheNhanvienQuyensudung.Schema).Where(QheNhanvienQuyensudung.Columns.IdNhanvien).IsEqualTo(objDmucNhanvien.IdNhanvien).Execute();
                        foreach (QheNhanvienDanhmuc obj in lstQheDmuc)
                        {
                            obj.IdNhanvien = objDmucNhanvien.IdNhanvien;
                        }
                        foreach (QheNhanvienKho obj in lstQhekho)
                        {
                            obj.IdNhanvien = objDmucNhanvien.IdNhanvien;
                        }
                        foreach (QheBacsiKhoaphong obj in lstQhekhoa)
                        {
                            obj.IdBacsi = objDmucNhanvien.IdNhanvien;
                        }
                        foreach (QheNhanvienQuyensudung obj in lstQheQuyensudung)
                        {
                            obj.IdNhanvien = objDmucNhanvien.IdNhanvien;
                        }
                        lstQheDmuc.SaveAll();
                        lstQhekho.SaveAll();
                        lstQhekhoa.SaveAll();
                        lstQheQuyensudung.SaveAll();
                    }
                    scope.Complete();
                }
                return string.Empty;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }
       public static string Delete(int IdNhanvien)
       {
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       new Delete().From(QheNhanvienDanhmuc.Schema).Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(IdNhanvien).Execute();
                       new Delete().From(DmucNhanvien.Schema).Where(DmucNhanvien.Columns.IdNhanvien).IsEqualTo(IdNhanvien).Execute();
                       new Delete().From(QheNhanvienKho.Schema).Where(QheNhanvienKho.Columns.IdNhanvien).IsEqualTo(IdNhanvien).Execute();
                       new Delete().From(QheBacsiKhoaphong.Schema).Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(IdNhanvien).Execute();
                       new Delete().From(QheNhanvienQuyensudung.Schema).Where(QheNhanvienQuyensudung.Columns.IdNhanvien).IsEqualTo(IdNhanvien).Execute();
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
