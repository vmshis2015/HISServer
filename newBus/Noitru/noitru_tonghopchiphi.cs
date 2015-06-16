using System;
using System.Data;
using System.Transactions;
using System.Linq;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

using System.Text;

using SubSonic;
using NLog;
using VNS.Properties;

namespace VNS.HIS.BusRule.Classes
{
    public class noitru_tonghopchiphi
    {
        private NLog.Logger log;
        public noitru_tonghopchiphi()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public static ActionResult TongHopChiPhi(KcbLuotkham objPatientExam)
        {

            try
            {

                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        new Update(KcbLuotkham.Schema)
                        .Set(KcbLuotkham.Columns.TrangthaiNoitru).EqualTo(Utility.Int32Dbnull(objPatientExam.TrangthaiNoitru))
                        .Set(KcbLuotkham.Columns.TthaiThopNoitru).EqualTo(Utility.Int32Dbnull(objPatientExam.TthaiThopNoitru))
                        .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                        .Set(KcbLuotkham.Columns.NgaySua).EqualTo(DateTime.Now)
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                        .And(KcbLuotkham.Columns.IdBenhnhan)
                        .IsEqualTo(Utility.Int32Dbnull(objPatientExam.IdBenhnhan))
                        .Execute();

                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                return ActionResult.Error;
            }
        }
        
    }
}
