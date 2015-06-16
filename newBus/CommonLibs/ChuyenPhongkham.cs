using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using System.Transactions;
using VNS.Properties;

namespace VNS.Libs
{
    public class ChuyenPhongkham
    {
        public static ActionResult ChuyenPhong(long IdKham,string LydoChuyen, DmucDichvukcb objDichvuKcb)
        {
            try
            {
            ActionResult _ActionResult = ActionResult.Success;
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Update(KcbDangkyKcb.Schema)
                        .Set(KcbDangkyKcb.Columns.IdPhongkham).EqualTo(objDichvuKcb.IdPhongkham)
                        .Set(KcbDangkyKcb.Columns.IdDichvuKcb).EqualTo(objDichvuKcb.IdDichvukcb)
                        .Set(KcbDangkyKcb.Columns.IdKieukham).EqualTo(objDichvuKcb.IdKieukham)
                        .Set(KcbDangkyKcb.Columns.TenDichvuKcb).EqualTo(objDichvuKcb.TenDichvukcb)
                        .Set(KcbDangkyKcb.Columns.NgayDangky).EqualTo(globalVariables.SysDate)
                        .Set(KcbDangkyKcb.Columns.NguoiChuyen).EqualTo(globalVariables.UserName)
                        .Set(KcbDangkyKcb.Columns.NgayChuyen).EqualTo(globalVariables.SysDate)
                        .Set(KcbDangkyKcb.Columns.LydoChuyen).EqualTo(LydoChuyen)
                        .Set(KcbDangkyKcb.Columns.TrangthaiChuyen).EqualTo(1)
                        .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(IdKham)
                        .Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển đối tượng:\n"+ex.Message);
                return ActionResult.Exception;
            }
        }
       

      
       
    }
}
