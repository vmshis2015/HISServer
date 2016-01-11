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
            log = LogManager.GetLogger("KCB_CHIDINHCLS");
        }
         public void XoaChiDinhCLSChitiet(long IdChitietchidinh)
         {
             SPs.ChidinhclsXoaChitiet(IdChitietchidinh).Execute();
         }
         public DataTable DmucTimkiemNhomchidinhCls(int? IdNhom, string Manhom, string TenNhom, string MaLoainhom,byte Loainhom, int? IdDichvuChitiet, string username)
         {
             return SPs.DmucTimkiemNhomchidinhCls(IdNhom, Manhom, TenNhom, MaLoainhom,Loainhom, IdDichvuChitiet,username).GetDataSet().Tables[0];
         }
         public ActionResult Xoanhom(int IdNhom)
         {
             try
             {
                 using (var scope = new TransactionScope())
                 {
                     using (var sh = new SharedDbConnectionScope())
                     {
                         new Delete().From(DmucNhomcanlamsang.Schema).Where(DmucNhomcanlamsang.Columns.Id).IsEqualTo(IdNhom).Execute();
                         new Delete().From(DmucNhomcanlamsangChitiet.Schema).Where(DmucNhomcanlamsangChitiet.Columns.IdNhom).IsEqualTo(IdNhom).Execute();
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
         public DataTable DmucLaychitietCLSTheonhomchidinhCls(int ID)
         {
             return SPs.DmucLaychitietCLSTheonhomchidinhCls(ID).GetDataSet().Tables[0];
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
         public DataTable LaythongtininphieuchidinhCLS(KcbChidinhcl objChidinh)
         {
             return SPs.KcbThamkhamLaythongtinclsInphieuTach(objChidinh.MaChidinh, objChidinh.MaLuotkham,
                                                              Utility.Int32Dbnull(objChidinh.IdBenhnhan)).GetDataSet().
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
         public ActionResult InsertDataChiDinhCLS(KcbChidinhcl objChidinh, KcbLuotkham objLuotkham, KcbChidinhclsChitiet[] arrAssignDetails)
         {
             try
             {
                 using (var scope = new TransactionScope())
                 {
                     using (var sh = new SharedDbConnectionScope())
                     {
                         if (objChidinh != null)
                         {
                             log.Trace("BEGIN INSERTING..........................................................");
                             if (objLuotkham == null)
                             {
                                 objLuotkham = new Select().From(KcbLuotkham.Schema)
                                     .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objChidinh.MaLuotkham)
                                     .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(
                                         Utility.Int32Dbnull(objChidinh.IdBenhnhan)).ExecuteSingle<KcbLuotkham>();
                             }
                             if (objLuotkham != null)
                             {
                                 objChidinh.MaChidinh = THU_VIEN_CHUNG.SinhMaChidinhCLS();
                                 objChidinh.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                                 objChidinh.IdLoaidoituongKcb = objLuotkham.IdLoaidoituongKcb;
                                 objChidinh.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                 objChidinh.MaKhoaChidinh = globalVariables.MA_KHOA_THIEN;

                                 var sp = SPs.SpKcbThemmoiChidinh(objChidinh.IdChidinh, objChidinh.IdKham, objChidinh.IdBuongGiuong, objChidinh.IdDieutri, objChidinh.IdKhoadieutri
                                     , objChidinh.MaLuotkham, objChidinh.IdBenhnhan, objChidinh.NgayChidinh, objChidinh.IdBacsiChidinh, objChidinh.IdPhongChidinh, objChidinh.NgayThanhtoan
                                     , objChidinh.TrangthaiThanhtoan, Utility.ByteDbnull(objChidinh.TrangThai,0), objChidinh.NguoiTao, objChidinh.NgayTao, objChidinh.TinhtrangIn, objChidinh.Barcode, objChidinh.Noitru
                                     , objChidinh.IdKhoaChidinh, objChidinh.MaKhoaChidinh, objChidinh.MaChidinh, objChidinh.MaBenhpham, objChidinh.IdDoituongKcb, objChidinh.IdLoaidoituongKcb
                                     , objChidinh.MaDoituongKcb, objChidinh.KieuChidinh, objChidinh.IdLichsuDoituongKcb, objChidinh.MatheBhyt, objChidinh.IpMaytao, objChidinh.TenMaytao
                                     , objChidinh.NguoigiaoMau, objChidinh.NguoinhanMau, objChidinh.MotaThem, objChidinh.DaBangiaomau, objChidinh.LuongmauHoaly, objChidinh.LuongmauVisinh, objChidinh.LuongmauGui
                                     , objChidinh.LuuMau, objChidinh.DieukienLuumau, objChidinh.ThanhlyMau, objChidinh.NgayThanhly, objChidinh.NguoiThanhly, objChidinh.LastActionName);
                                 sp.Execute();
                                 objChidinh.IdChidinh = Utility.Int64Dbnull(sp.OutputValues[0]);
                                 log.Trace("1. Da them moi chi dinh CLS");
                                 SPs.SpKcbCapnhatBacsiKham(objChidinh.IdKham, objChidinh.IdBacsiChidinh, 1).Execute();
                                 InsertAssignDetail(objChidinh, objLuotkham, arrAssignDetails);
                                 log.Trace("2. Da them moi chi tiet chi dinh CLS");
                             }
                             else
                             {
                                 return ActionResult.Error;
                             }
                         }
                     }
                     scope.Complete();
                     log.Trace("FINISH INSERTING..........................................................");
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error(string.Format( "Loi khi them moi chi dinh dich vu CLS {0}", exception.Message));
                 return ActionResult.Error;
             }
         }
         public void InsertAssignDetail(KcbChidinhcl objChidinh, KcbLuotkham objLuotkham, KcbChidinhclsChitiet[] assignDetail)
         {
             using (var scope = new TransactionScope())
             {
                 if (objLuotkham == null) return;
                 foreach (KcbChidinhclsChitiet objChidinhCtiet in assignDetail)
                 {
                     log.Info("Them moi thong tin cua phieu chi dinh chi tiet voi ma phieu Id_chidinh=" +
                              objChidinh.IdChidinh);
                     objChidinhCtiet.IdDoituongKcb = Utility.Int16Dbnull(objLuotkham.IdDoituongKcb);
                     objChidinhCtiet.IdChidinh = Utility.Int32Dbnull(objChidinh.IdChidinh);
                     objChidinhCtiet.IdKham = Utility.Int32Dbnull(objChidinh.IdKham, -1);
                     decimal PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                     TinhCLS.GB_TinhPhtramBHYT(objChidinhCtiet, objLuotkham, Utility.Byte2Bool(objChidinh.Noitru), PtramBHYT);
                     objChidinhCtiet.MaLuotkham = objChidinh.MaLuotkham;
                     objChidinhCtiet.IdBenhnhan = objChidinh.IdBenhnhan;
                     if (Utility.Int32Dbnull(objChidinhCtiet.SoLuong) <= 0) objChidinhCtiet.SoLuong = 1;
                     if (objChidinhCtiet.IdChitietchidinh <= 0)
                     {
                         var sp = SPs.SpKcbThemmoiChitietChidinh(objChidinhCtiet.IdChitietchidinh, objChidinhCtiet.IdKham, objChidinhCtiet.IdChidinh, objChidinhCtiet.IdChidinhChuyengoi
                             , objChidinhCtiet.IdDichvu, objChidinhCtiet.IdChitietdichvu, objChidinhCtiet.PtramBhytGoc, objChidinhCtiet.PtramBhyt, objChidinhCtiet.GiaDanhmuc, objChidinhCtiet.MadoituongGia
                             , objChidinhCtiet.DonGia, objChidinhCtiet.PhuThu, objChidinhCtiet.NguoiTao, objChidinhCtiet.IdLoaichidinh, objChidinhCtiet.NgayTao, objChidinhCtiet.TrangthaiThanhtoan
                             , objChidinhCtiet.NgayThanhtoan, objChidinhCtiet.TrangthaiHuy, objChidinhCtiet.TuTuc, objChidinhCtiet.LoaiChietkhau, objChidinhCtiet.IdDoituongKcb
                             , objChidinhCtiet.IdBenhnhan, objChidinhCtiet.MaLuotkham, objChidinhCtiet.SoLuong, objChidinhCtiet.TrangThai, objChidinhCtiet.TrangthaiBhyt, objChidinhCtiet.HienthiBaocao
                             , objChidinhCtiet.BhytChitra, objChidinhCtiet.BnhanChitra, objChidinhCtiet.IdThanhtoan, objChidinhCtiet.IdKhoaThuchien, objChidinhCtiet.IdPhongThuchien
                             , objChidinhCtiet.TileChietkhau, objChidinhCtiet.TienChietkhau, objChidinhCtiet.KieuChietkhau, objChidinhCtiet.IdGoi, objChidinhCtiet.TrongGoi
                             , objChidinhCtiet.IdBacsiThuchien, objChidinhCtiet.NguoiThuchien, objChidinhCtiet.NgayThuchien, objChidinhCtiet.ImgPath1, objChidinhCtiet.ImgPath2, objChidinhCtiet.ImgPath3, objChidinhCtiet.ImgPath4
                             , objChidinhCtiet.FTPImage, objChidinhCtiet.KetQua, objChidinhCtiet.ChidinhGoidichvu, objChidinhCtiet.NguonThanhtoan, objChidinhCtiet.IpMaytao, objChidinhCtiet.TenMaytao
                             , objChidinhCtiet.ChitieuPhantich, objChidinhCtiet.MahoaMau, objChidinhCtiet.MauUutien, objChidinhCtiet.NgayhenTrakq, objChidinhCtiet.ThetichkhoiluongMau, objChidinhCtiet.TinhtrangMau
                             );
                         sp.Execute();
                         objChidinhCtiet.IdChitietchidinh = Utility.Int64Dbnull(sp.OutputValues[0]);
                     }
                     else
                     {
                         SPs.SpKcbCapnhatChitietChidinh(objChidinhCtiet.IdChitietchidinh, objChidinhCtiet.SoLuong, objChidinhCtiet.NgaySua, objChidinhCtiet.NguoiSua
                             , objChidinhCtiet.IpMaysua, objChidinhCtiet.TenMaysua).Execute();
                     }
                 }
                 scope.Complete();
             }
         }
        
         public ActionResult UpdateDataChiDinhCLS(KcbChidinhcl objChidinh, KcbLuotkham objLuotkham, KcbChidinhclsChitiet[] arrAssignDetails)
         {
             try
             {
                 using (var scope = new TransactionScope())
                 {
                     using (var sh = new SharedDbConnectionScope())
                     {
                         log.Trace("BEGIN UPDATE.................................................................");
                         if (objLuotkham == null)
                         {
                             objLuotkham = new Select().From(KcbLuotkham.Schema)
                                 .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objChidinh.MaLuotkham)
                                 .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(
                                     Utility.Int32Dbnull(objChidinh.IdBenhnhan)).ExecuteSingle<KcbLuotkham>();
                         }
                         SPs.SpKcbCapnhatChidinh(objChidinh.IdChidinh, objChidinh.NgayChidinh, objChidinh.IdBacsiChidinh, objChidinh.IdPhongChidinh, objChidinh.NguoiSua
                             , objChidinh.NgaySua, objChidinh.MaChidinh, objChidinh.IpMaysua, objChidinh.TenMaysua, objChidinh.NguoigiaoMau
                             , objChidinh.NguoinhanMau, objChidinh.MotaThem, objChidinh.LastActionName).Execute();
                         log.Trace("1. Da cap nhat chi dinh dich vu CLS");
                         if (Utility.Int32Dbnull(objChidinh.IdKham) > 0)
                         {
                             SPs.SpKcbCapnhatBacsiKham(objChidinh.IdKham,objChidinh.IdBacsiChidinh,1).Execute();
                         }
                         log.Info("Cap nhap lai thong tin cua phieu chi dinh voi Id_chidinh=" + objChidinh.IdChidinh);
                         InsertAssignDetail(objChidinh, objLuotkham, arrAssignDetails);
                         log.Trace("1. Da cap nhat chi tiet chi dinh dich vu CLS");
                     }
                     scope.Complete();
                     log.Trace("END UPDATE.................................................................");
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
         public DataTable MaukiemnghiemLaydanhsachdvukiemnghiem()
         {
             DataTable dataTable = new DataTable();
             try
             {
                 dataTable = SPs.SpMaukiemnghiemLaydanhsachdvukiemnghiem().GetDataSet().Tables[0];
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
