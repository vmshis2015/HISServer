using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using SubSonic;
using VNS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.BusRule.Classes
{
   public class clsHinhanh
    {
        public static ActionResult UpdateDynamicValues(List<DynamicValue> lstValues)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        foreach (DynamicValue _object in lstValues)
                        {
                            if (_object.Id > 0)
                            {
                                _object.MarkOld();
                                _object.IsNew = false;
                                _object.Save();
                            }
                            else//Insert
                            {
                                _object.IsNew = true;

                                _object.Save();
                            }
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
                return ActionResult.Error;
            }

        }

        public static DataTable GetDynamicFieldsValues(int? IdChitietdichvu, string bodypart, string viewposition, long? ImageId, long? idchitietchidinh)
        {
            try
            {
                return SPs.HinhanhGetDynamicFieldsValues(IdChitietdichvu, bodypart, viewposition, ImageId, idchitietchidinh).GetDataSet().Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
   public class clsXN
   {
       public static ActionResult UpdateResult(List< KcbKetquaCl> lstResult,List< KcbChidinhclsChitiet> lstDetails)
       {
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sp = new SharedDbConnectionScope())
                   {
                       foreach(KcbKetquaCl _result in lstResult)
                       _result.Save();
                       foreach(KcbChidinhclsChitiet _detail in lstDetails)
                       _detail.Save();
                   }
                   scope.Complete();
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {
               Utility.ShowMsg(exception.Message);
               return ActionResult.Error;
           }

       }

      
   }
}
