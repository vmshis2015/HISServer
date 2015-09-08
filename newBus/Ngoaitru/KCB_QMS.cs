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

namespace VNS.HIS.BusRule.Classes
{
    public class KCB_QMS
    {
        private NLog.Logger log;
        public KCB_QMS()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public void LaySoKhamQMS(string MaQuay, string MaKhoa, string madoituongkcb, ref int SoKham, ref int idDichvukcb, ref int idQMS,byte uutien, int loaiqms,string loaiqmsbo)
        {
            SoKham = 0;
            idDichvukcb = 0;
            StoredProcedure sp = SPs.QmsLayso(MaQuay, MaKhoa, madoituongkcb, SoKham, idDichvukcb, idQMS,uutien, loaiqms, loaiqmsbo);
            sp.Execute();
            SoKham= Utility.Int32Dbnull(sp.OutputValues[0]);
            idDichvukcb = Utility.Int32Dbnull(sp.OutputValues[1]);
            idQMS = Utility.Int32Dbnull(sp.OutputValues[2]);
        }
    }
}
