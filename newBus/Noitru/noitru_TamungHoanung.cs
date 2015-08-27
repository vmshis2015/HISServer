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
using System.Collections.Generic;

namespace VNS.HIS.BusRule.Classes
{
    public class noitru_TamungHoanung
    {
        private NLog.Logger log;
        public noitru_TamungHoanung()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public static  bool NoptienTamung(NoitruTamung objTamung)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        objTamung.Save();
                        KcbLuotkham objKcbLuotkham = Utility.getKcbLuotkham(objTamung.IdBenhnhan, objTamung.MaLuotkham);
                        if (objKcbLuotkham != null)
                        {
                            objKcbLuotkham.IsNew = false;
                            objKcbLuotkham.MarkOld();
                            if (Utility.ByteDbnull(objKcbLuotkham.TrangthaiNoitru, 0) == 1)
                            {
                                objKcbLuotkham.TrangthaiNoitru = 2;
                                objKcbLuotkham.Save();
                            }
                        }
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static DataTable BaocaoTamungHoanung(int IdTNV, string tungay, string denngay, short IdKhoanoitru, short IdDoituongKcb, byte kieutamung)
        {
            return SPs.BaocaoTamungHoanung(IdTNV, tungay, denngay, IdKhoanoitru, IdDoituongKcb, kieutamung).GetDataSet().Tables[0];

        }
        public static bool XoaTienTamung(NoitruTamung objTamung,bool VanConTamung,string lydohuy)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        KcbLoghuy objHuy = new KcbLoghuy();
                        objHuy.IdBenhnhan = objTamung.IdBenhnhan;
                        objHuy.MaLuotkham = objTamung.MaLuotkham;
                        objHuy.LoaiphieuHuy = 2;//0= hủy thanh toán;1= hủy phiếu chi;2= hủy tạm ứng
                        objHuy.LydoHuy = lydohuy;
                        objHuy.SotienHuy =Utility.DecimaltoDbnull( objTamung.SoTien,0);
                        objHuy.IdNhanvien = globalVariables.gv_intIDNhanvien;
                        objHuy.NgayHuy=globalVariables.SysDate;
                        objHuy.NguoiTao=globalVariables.UserName;
                        objHuy.NgayTao=globalVariables.SysDate;
                        objHuy.IsNew=true;
                        objHuy.Save();
                        new Delete().From(NoitruTamung.Schema).Where(NoitruTamung.Columns.Id).IsEqualTo(objTamung.Id).Execute();
                        NoitruPhieudieutri objNoitruPhieudieutri = Utility.getNoitruPhieudieutri(objTamung.IdBenhnhan, objTamung.MaLuotkham);
                        KcbLuotkham objKcbLuotkham = Utility.getKcbLuotkham(objTamung.IdBenhnhan, objTamung.MaLuotkham);
                        if (Utility.Byte2Bool(objKcbLuotkham.TrangthaiNoitru) && objNoitruPhieudieutri == null && !VanConTamung)//Chỉ update nếu là nội trú
                        {
                            objKcbLuotkham.IsNew = false;
                            objKcbLuotkham.TrangthaiNoitru = 1;
                            objKcbLuotkham.MarkOld();
                            objKcbLuotkham.Save();
                        }
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DathanhtoanhetNgoaitru(long id_benhnhan, string ma_luotkham)
        {
            try
            {
                DataTable dtData = SPs.KcbKiemtrathanhtoanhetDoituongDVTruockhinhapvien(id_benhnhan, ma_luotkham).GetDataSet().Tables[0];
                if (dtData == null) return true;
                return Utility.DecimaltoDbnull(dtData.Compute("SUM(bnhan_chitra)", "1=1"), 0) == 0;
            }
            catch (Exception ex)
            {
                return false;
                
            }
        }
        public static DataTable NoitruInphieutamung(long idTamung)
        {
            try
            {
               return SPs.NoitruInphieutamung(idTamung).GetDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return null;

            }
        }
    }
}
