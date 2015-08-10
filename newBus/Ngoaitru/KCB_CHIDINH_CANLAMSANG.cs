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
using System.Collections.Generic;
namespace VNS.HIS.BusRule.Classes
{
    public class KCB_CHIDINH_CANLAMSANG
    {
         private NLog.Logger log;
         public KCB_CHIDINH_CANLAMSANG()
        {
            log = LogManager.GetCurrentClassLogger();
        }
         public void XoaChiDinhCLSChitiet(long IdChitietchidinh)
         {
             SPs.ChidinhclsXoaChitiet(IdChitietchidinh).Execute();
         }
         public void XoaCLSChitietKhoinhom(long IdChitiet)
         {
             new Delete().From(DmucNhomcanlamsangChitiet.Schema).Where(DmucNhomcanlamsangChitiet.Columns.IdChitiet).IsEqualTo(IdChitiet).Execute();
         }
         public void GoidichvuXoachitiet(long IdChitietchidinh)
         {
             SPs.KcbGoidichvuXoachitiet(IdChitietchidinh).Execute();
         }
         public DataTable DmucLaychitietNhomchidinhCls(int ID)
         {
             return SPs.DmucLaychitietNhomchidinhCls(ID).GetDataSet().Tables[0];
         }
         public DataTable LaythongtinCLS_Thuoc(int ID, string KieuMau)
         {
             return SPs.ChidinhclsLaythongtinChidinhclsTheoid(ID, KieuMau).GetDataSet().Tables[0];
         }
         public DataTable LaythongtininphieuchidinhCLS(string MaChidinh, string PatientCode, int PatientID)
         {
             return SPs.KcbThamkhamLaythongtinclsInphieuTach(MaChidinh, PatientCode,
                                                              PatientID).GetDataSet().
                     Tables[0];
         }
         public DataTable LaythongtininphieuchidinhCLS(KcbChidinhcl objKcbChidinhcls)
         {
             return SPs.KcbThamkhamLaythongtinclsInphieuTach(objKcbChidinhcls.MaChidinh, objKcbChidinhcls.MaLuotkham,
                                                              Utility.Int32Dbnull(objKcbChidinhcls.IdBenhnhan)).GetDataSet().
                     Tables[0];
         }
         public ActionResult ThemnhomChidinhCLS(DmucNhomcanlamsang objNhom, List< DmucNhomcanlamsangChitiet> lstChitiet)
         {
             try
             {
                 using (var scope = new TransactionScope())
                 {
                     using (var sh = new SharedDbConnectionScope())
                     {
                         if (objNhom != null)
                         {
                             objNhom.IsNew = true;
                             objNhom.Save();
                             foreach (DmucNhomcanlamsangChitiet objChitiet in lstChitiet)
                             {
                                 objChitiet.IdNhom = objNhom.Id;
                                 if (Utility.Int32Dbnull(objChitiet.SoLuong) <= 0) objChitiet.SoLuong = 1;
                                 if (objChitiet.IdChitiet <= 0)
                                 {
                                     objChitiet.IsNew = true;
                                     objChitiet.Save();
                                 }
                                 else
                                 {
                                     objChitiet.MarkOld();
                                     objChitiet.IsNew = false;
                                     objChitiet.Save();
                                 }
                             }
                         }
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 return ActionResult.Error;
             }
         }
         public ActionResult InsertDataChiDinhCLS(KcbChidinhcl objKcbChidinhcls, KcbLuotkham objLuotkham, KcbChidinhclsChitiet[] arrAssignDetails)
         {
             try
             {
                 using (var scope = new TransactionScope())
                 {
                     using (var sh = new SharedDbConnectionScope())
                     {
                         if (objKcbChidinhcls != null)
                         {
                             if (objLuotkham == null)
                             {
                                 objLuotkham = new Select().From(KcbLuotkham.Schema)
                                     .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objKcbChidinhcls.MaLuotkham)
                                     .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(
                                         Utility.Int32Dbnull(objKcbChidinhcls.IdBenhnhan)).ExecuteSingle<KcbLuotkham>();
                             }
                             if (objLuotkham != null)
                             {
                                 objKcbChidinhcls.MaChidinh = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                                 objKcbChidinhcls.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                                 objKcbChidinhcls.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                                 objKcbChidinhcls.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                 objKcbChidinhcls.MaKhoaChidinh = globalVariables.MA_KHOA_THIEN;
                                 objKcbChidinhcls.IsNew = true;
                                 objKcbChidinhcls.Save();
                                 InsertAssignDetail(objKcbChidinhcls, objLuotkham, arrAssignDetails);
                             }
                             else
                             {
                                 return ActionResult.Error;
                             }
                         }
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.InfoException("Loi thong tin {0}", exception);
                 return ActionResult.Error;
             }
         }
         public void InsertAssignDetail(KcbChidinhcl objKcbChidinhcls, KcbLuotkham objLuotkham, KcbChidinhclsChitiet[] assignDetail)
         {
             using (var scope = new TransactionScope())
             {
                 if (objLuotkham == null) return;
                 foreach (KcbChidinhclsChitiet objAssignDetail in assignDetail)
                 {
                     log.Info("Them moi thong tin cua phieu chi dinh chi tiet voi ma phieu Assign_ID=" +
                              objKcbChidinhcls.IdChidinh);
                     TinhCLS.TinhGiaChiDinhCLS(objLuotkham, objAssignDetail);
                     objAssignDetail.IdDoituongKcb = Utility.Int16Dbnull(objLuotkham.IdDoituongKcb);
                     objAssignDetail.IdChidinh = Utility.Int32Dbnull(objKcbChidinhcls.IdChidinh);
                     objAssignDetail.IdKham = Utility.Int32Dbnull(objKcbChidinhcls.IdKham, -1);
                     decimal PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                     TinhCLS.GB_TinhPhtramBHYT(objAssignDetail, objLuotkham, PtramBHYT);
                     objAssignDetail.MaLuotkham = objKcbChidinhcls.MaLuotkham;
                     objAssignDetail.IdBenhnhan = objKcbChidinhcls.IdBenhnhan;
                     if (Utility.Int32Dbnull(objAssignDetail.SoLuong) <= 0) objAssignDetail.SoLuong = 1;
                     if (objAssignDetail.IdChitietchidinh <= 0)
                     {

                         objAssignDetail.IsNew = true;
                         objAssignDetail.Save();
                     }
                     else
                     {
                         objAssignDetail.MarkOld();
                         objAssignDetail.IsNew = false;
                         objAssignDetail.Save();
                     }
                 }
                 scope.Complete();
             }
         }
        
         public ActionResult UpdateDataChiDinhCLS(KcbChidinhcl objKcbChidinhcls, KcbLuotkham objLuotkham, KcbChidinhclsChitiet[] arrAssignDetails)
         {
             try
             {
                 using (var scope = new TransactionScope())
                 {
                     using (var sh = new SharedDbConnectionScope())
                     {
                         if (objLuotkham == null)
                         {
                             objLuotkham = new Select().From(KcbLuotkham.Schema)
                                 .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objKcbChidinhcls.MaLuotkham)
                                 .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(
                                     Utility.Int32Dbnull(objKcbChidinhcls.IdBenhnhan)).ExecuteSingle<KcbLuotkham>();
                         }
                         new Update(KcbChidinhcl.Schema)
                             .Set(KcbChidinhcl.Columns.IdBacsiChidinh).EqualTo(objKcbChidinhcls.IdBacsiChidinh)
                             .Set(KcbChidinhcl.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                             .Set(KcbChidinhcl.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                             .Where(KcbChidinhcl.Columns.IdChidinh).IsEqualTo(Utility.Int32Dbnull(objKcbChidinhcls.IdChidinh)).Execute();
                         if (Utility.Int32Dbnull(objKcbChidinhcls.IdKham) > 0)
                         {
                             new Update(KcbDangkyKcb.Schema)
                                 .Set(KcbDangkyKcb.Columns.IdBacsikham).EqualTo(objKcbChidinhcls.IdBacsiChidinh)
                                 .Where(KcbDangkyKcb.IdKhamColumn).IsEqualTo(objKcbChidinhcls.IdKham).Execute();
                         }
                         log.Info("Cap nhap lai thong tin cua phieu chi dinh voi Assign_ID=" + objKcbChidinhcls.IdChidinh);
                         InsertAssignDetail(objKcbChidinhcls, objLuotkham, arrAssignDetails);
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.InfoException("Loi thong tin ", exception);
                 return ActionResult.Error;
             }
         }
         public ActionResult CapnhatnhomchidinhCLS(DmucNhomcanlamsang objNhom, List<DmucNhomcanlamsangChitiet> lstChitiet)
         {
             try
             {
                 using (var scope = new TransactionScope())
                 {
                     using (var sh = new SharedDbConnectionScope())
                     {

                         objNhom.Save();
                         foreach (DmucNhomcanlamsangChitiet objChitiet in lstChitiet)
                         {
                             objChitiet.IdNhom = objNhom.Id;
                             if (Utility.Int32Dbnull(objChitiet.SoLuong) <= 0) objChitiet.SoLuong = 1;
                             if (objChitiet.IdChitiet <= 0)
                             {
                                 objChitiet.IsNew = true;
                                 objChitiet.Save();
                             }
                             else
                             {
                                 objChitiet.MarkOld();
                                 objChitiet.IsNew = false;
                                 objChitiet.Save();
                             }
                         }
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.InfoException("Loi thong tin ", exception);
                 return ActionResult.Error;
             }
         }
         public DataTable DmucLaydanhmucclsTaonhomchidinh(string nhomchidinh)
         {
             DataTable dataTable = new DataTable();
             try
             {
                 dataTable = SPs.DmucLaydanhmucclsTaonhomchidinh(nhomchidinh).GetDataSet().Tables[0];
                 if (!dataTable.Columns.Contains("TenDichvu_khongdau"))
                     dataTable.Columns.Add("TenDichvu_khongdau", typeof(string));
                 if (!dataTable.Columns.Contains("TenChitietDichvu_khongdau"))
                     dataTable.Columns.Add("TenChitietDichvu_khongdau", typeof(string));
                 foreach (DataRow drv in dataTable.Rows)
                 {
                     drv["TenDichvu_khongdau"] = Utility.UnSignedCharacter(drv[DmucDichvucl.Columns.TenDichvu].ToString());
                     drv["TenChitietDichvu_khongdau"] = Utility.UnSignedCharacter(drv[DmucDichvuclsChitiet.Columns.TenChitietdichvu].ToString());
                 }
                 dataTable.AcceptChanges();
             }
             catch (Exception)
             {
                 return null;
             }
             return dataTable;
         }
         public DataTable LaydanhsachCLS_chidinh(string MaDoiTuong, byte Noitru, byte cogiayBHYT, int ID_GoiDV, int dungtuyen, string MA_KHOA_THIEN, string nhomchidinh)
         {
             DataTable dataTable = new DataTable();
             try
             {
                 dataTable = SPs.ChidinhclsLaydanhsachclsChidinh(MaDoiTuong, Noitru, cogiayBHYT,ID_GoiDV, dungtuyen, MA_KHOA_THIEN, nhomchidinh).GetDataSet().Tables[0];
                 if (!dataTable.Columns.Contains("TenDichvu_khongdau"))
                     dataTable.Columns.Add("TenDichvu_khongdau", typeof(string));
                 if (!dataTable.Columns.Contains("TenChitietDichvu_khongdau"))
                     dataTable.Columns.Add("TenChitietDichvu_khongdau", typeof(string));
                 foreach (DataRow drv in dataTable.Rows)
                 {
                     drv["TenDichvu_khongdau"] = Utility.UnSignedCharacter(drv[DmucDichvucl.Columns.TenDichvu].ToString());
                     drv["TenChitietDichvu_khongdau"] = Utility.UnSignedCharacter(drv[DmucDichvuclsChitiet.Columns.TenChitietdichvu].ToString());
                 }
                 dataTable.AcceptChanges();
             }
             catch (Exception)
             {
                 return null;
             }
             return dataTable;
         }
    }
}
