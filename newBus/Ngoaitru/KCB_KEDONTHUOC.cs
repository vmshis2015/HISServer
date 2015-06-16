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
    public class KCB_KEDONTHUOC
    {
         private NLog.Logger log;
         public KCB_KEDONTHUOC()
        {
            log = LogManager.GetCurrentClassLogger();
        }
         public void XoaChitietDonthuoc(int IdChitietdonthuoc)
         {
             SPs.DonthuocXoaChitiet(IdChitietdonthuoc).Execute();
         }
         public void Capnhatchidanchitiet(long IdChitietdonthuoc,string columnName,object _value)
         {
             new Update(KcbDonthuocChitiet.Schema).Set(columnName).EqualTo(_value).Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(IdChitietdonthuoc).Execute();
         }
         
         public void NoitruXoachandoan(string lstIdChandoan)
         {

             SPs.NoitruXoachandoan(lstIdChandoan).Execute();
         }
         public void NoitruXoaDinhduong(string lstIdChandoan)
         {

             SPs.NoitruXoaDinhduong(lstIdChandoan).Execute();
         }
         public void XoaChitietDonthuoc(string lstIdChitietDonthuoc)
         {

             SPs.DonthuocXoaChitietNew(lstIdChitietDonthuoc).Execute();
         }
         public ActionResult XoaDonthuoctaiquay(long idbenhnhan, long iddonthuoc)
         {
             try
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         new Delete().From(KcbDanhsachBenhnhan.Schema).Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(idbenhnhan).Execute();
                         new Delete().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(iddonthuoc).And(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(idbenhnhan).Execute();
                         new Delete().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(iddonthuoc).Execute();
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch
             {
                 return ActionResult.Exception;
             }
         }
         public void XoaChitietDonthuoc(string lstIdChitietDonthuoc,int iddetail,int soluong)
         {
             using (TransactionScope scope = new TransactionScope())
             {
                 using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                 {
                     if (soluong > 0)
                         new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.SoLuong).EqualTo(soluong)
                             .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(iddetail).Execute();
                     else
                         lstIdChitietDonthuoc = lstIdChitietDonthuoc + "," + iddetail.ToString();
                     SPs.DonthuocXoaChitietNew(lstIdChitietDonthuoc).Execute();
                 }
                 scope.Complete();
             }
         }
         public DataTable LaythongtinDonthuoc_In(int id_donthuoc)
         {
             return SPs.DonthuocLaythongtinDein(id_donthuoc).GetDataSet().Tables[0];
         }
         public DataTable Laythongtinchitietdonthuoc(int id_donthuoc)
         {
             return SPs.DonthuocLaythongtinDexem(id_donthuoc).GetDataSet().Tables[0];
         }
         public DataTable LayThuoctrongkhokedon(int id_kho, string KieuThuocVT, string ma_doituong_kcb,int Dungtuyen,int Noitru, string MaKHOATHIEN)
         {
             return SPs.DonthuocLaythongtinThuoctrongkhoKedon(id_kho, KieuThuocVT, ma_doituong_kcb,Dungtuyen,Noitru, MaKHOATHIEN).GetDataSet().Tables[0];
         }
         public DataTable LayThuoctrongkho(int id_kho, int id_maloaithuoc, string KieuKho)
         {
             return SPs.ThuocTimkiemThuoctrongkho(id_kho, id_maloaithuoc, KieuKho).GetDataSet().Tables[0];
         }
         public ActionResult CapnhatChandoan(KcbChandoanKetluan objKcbChandoanKetluan)
         {
             try
             {
                 if (objKcbChandoanKetluan == null) return ActionResult.Cancel;
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         if (objKcbChandoanKetluan.IsNew )
                         {
                             objKcbChandoanKetluan.Save();
                         }
                         else
                         {
                             objKcbChandoanKetluan.MarkOld();
                             objKcbChandoanKetluan.Save();
                         }

                         SqlQuery sqlQuery = new Select().From( KcbChandoanKetluan.Schema)
                                .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objKcbChandoanKetluan.MaLuotkham)
                                .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objKcbChandoanKetluan.IdBenhnhan).OrderAsc(
                                    KcbChandoanKetluan.Columns.NgayChandoan);
                         KcbChandoanKetluanCollection objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
                         var query = (from chandoan in objInfoCollection.AsEnumerable()
                                      let y = Utility.sDbnull(chandoan.Chandoan)
                                      where (y != "")
                                      select y).ToArray();
                         string cdchinh = string.Join(";", query);
                         var querychandoanphu = (from chandoan in objInfoCollection.AsEnumerable()
                                                 let y = Utility.sDbnull(chandoan.ChandoanKemtheo)
                                                 where (y != "")
                                                 select y).ToArray();
                         string cdphu = string.Join(";", querychandoanphu);
                         var querybenhchinh = (from benhchinh in objInfoCollection.AsEnumerable()
                                               let y = Utility.sDbnull(benhchinh.MabenhChinh)
                                               where (y != "")
                                               select y).ToArray();
                         string mabenhchinh = string.Join(";", querybenhchinh);

                         var querybenhphu = (from benhphu in objInfoCollection.AsEnumerable()
                                             let y = Utility.sDbnull(benhphu.MabenhPhu)
                                             where (y != "")
                                             select y).ToArray();
                         string mabenhphu = string.Join(";", querybenhphu);
                         new Update(KcbLuotkham.Schema)
                             .Set(KcbLuotkham.Columns.MabenhChinh).EqualTo(mabenhchinh)
                             .Set(KcbLuotkham.Columns.MabenhPhu).EqualTo(mabenhphu)
                             .Set(KcbLuotkham.Columns.ChanDoan).EqualTo(cdchinh)
                             .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                             .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                             .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objKcbChandoanKetluan.MaLuotkham)
                             .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objKcbChandoanKetluan.IdBenhnhan).Execute();

                     }

                     scope.Complete();
                     //  Reg_ID = Utility.Int32Dbnull(objRegExam.IdKham, -1);
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error("Loi trong qua trinh chuyen vien khoi noi tru {0}", exception);
                 return ActionResult.Error;
             }
         }
         public ActionResult ThemDonThuoc(KcbDanhsachBenhnhan objBenhnhan,  KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet, ref int p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
         {
             try
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         objBenhnhan.Save();
                         if (objBenhnhan != null)
                         {
                             if (objDonthuoc.NgayKedon <= Convert.ToDateTime("01/01/1900"))
                                 objDonthuoc.NgayKedon = globalVariables.SysDate;
                             objDonthuoc.IdBenhnhan = objBenhnhan.IdBenhnhan;
                             objDonthuoc.MaLuotkham = "";
                             objDonthuoc.IdKham = -1;
                             objDonthuoc.IsNew = true;
                             objDonthuoc.TenDonthuoc = "";

                             objDonthuoc.Save();
                             p_intIdDonthuoc = objDonthuoc.IdDonthuoc;
                             decimal PtramBH = 0;

                             foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                             {
                                 objDonthuocChitiet.IdKham = objDonthuoc.IdKham;
                                 objDonthuocChitiet.MaLuotkham = objDonthuoc.MaLuotkham;
                                 objDonthuocChitiet.IdBenhnhan = objDonthuoc.IdBenhnhan;
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 objDonthuocChitiet.IsNew = true;
                                 objDonthuocChitiet.Save();
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                             }

                         }


                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 Utility.CatchException(exception);
                 log.Error("Loi trong qua trinh luu don thuoc {0}", exception);
                 return ActionResult.Error;
             }

         }
         public ActionResult ThemDonThuoc(KcbLuotkham objLuotkham, KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet,KcbChandoanKetluan _KcbChandoanKetluan, ref int p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
         {
             // Query _Query = KcbDonthuoc.CreateQuery();
             try
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         if (objLuotkham != null)
                         {
                             if (objDonthuoc.NgayKedon <= Convert.ToDateTime("01/01/1900"))
                                 objDonthuoc.NgayKedon = globalVariables.SysDate;

                             objDonthuoc.IsNew = true;
                             objDonthuoc.TenDonthuoc = THU_VIEN_CHUNG.TaoTenDonthuoc(objLuotkham.MaLuotkham,
                                                                                        Utility.Int32Dbnull(
                                                                                            objLuotkham.IdBenhnhan,
                                                                                            -1));

                             objDonthuoc.Save();
                             if (!Utility.Byte2Bool(objDonthuoc.Noitru))
                                 CapnhatChandoan(_KcbChandoanKetluan);
                             p_intIdDonthuoc = objDonthuoc.IdDonthuoc;
                             decimal PtramBH = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);

                             foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                             {
                                 objDonthuocChitiet.IdKham = objDonthuoc.IdKham;
                                 objDonthuocChitiet.MaLuotkham = objDonthuoc.MaLuotkham;
                                 objDonthuocChitiet.IdBenhnhan = objDonthuoc.IdBenhnhan;
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 ThemChitiet(objDonthuoc, objDonthuocChitiet, PtramBH, objLuotkham);
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                             }

                         }


                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {

                 log.Error("Loi trong qua trinh luu don thuoc {0}", exception);
                 return ActionResult.Error;
             }

         }
         public void ThemChitiet(KcbDonthuoc objDonthuoc, KcbDonthuocChitiet objDonthuocChitiet, decimal PtramBHYT, KcbLuotkham objLuotkham)
         {
             using (TransactionScope scope = new TransactionScope())
             {
                 byte TrangthaiBhyt = 1;
                 if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//(objLuotkham.MaDoituongKcb == "DV")//Tự túc
                 {
                     PtramBHYT = 0m;
                     TrangthaiBhyt = (byte)0;
                     //ĐỐi tượng dịch vụ thì ko cần đánh dấu tự túc
                     objDonthuocChitiet.TuTuc = 0;
                 }
                 else
                     TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
                 //Tính giá BHYT chi trả và BN chi trả theo Đối tượng và % bảo hiểm-->Hơi thừa có thể bỏ qua do đã tính ở Client
                 //Nếu có dùng thì cần lấy lại KcbLuotkham do lo sợ người khác thay đổi đối tượng
                 //TinhGiaThuoc.GB_TinhPhtramBHYT(objDonthuocChitiet, PtramBHYT);
                 objDonthuocChitiet.TrangthaiBhyt = TrangthaiBhyt;// Utility.isTrue(objDonthuocChitiet.TuTuc.Value, 0, 1);
                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                 objDonthuocChitiet.IsNew = true;
                 objDonthuocChitiet.Save();
                 scope.Complete();
             }

             


         }
         public ActionResult CapnhatDonthuoc(KcbDanhsachBenhnhan objBenhnhan, KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet,  ref int p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
         {
             try
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         objBenhnhan.Save();
                         p_intIdDonthuoc = objDonthuoc.IdDonthuoc;
                         new Update(KcbDonthuoc.Schema)
                             .Set(KcbDonthuoc.Columns.NgayKedon).EqualTo(objDonthuoc.NgayKedon)
                             .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                             .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                             .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                        
                         foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                         {
                             objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                             if (objDonthuocChitiet.IdChitietdonthuoc == -1)//Moi bo sung
                             {
                                 objDonthuocChitiet.IdKham = objDonthuoc.IdKham;
                                 objDonthuocChitiet.IsNew = true;
                                 objDonthuocChitiet.SluongLinh = 0;
                                 objDonthuocChitiet.SluongSua = 0;
                                 objDonthuocChitiet.TrangThai = 0;

                                 objDonthuocChitiet.IdThanhtoan = -1;
                                 objDonthuocChitiet.MaLuotkham = objDonthuoc.MaLuotkham;
                                 objDonthuocChitiet.IdBenhnhan = objDonthuoc.IdBenhnhan;
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 objDonthuocChitiet.CachDung = objDonthuocChitiet.MotaThem;
                                 objDonthuocChitiet.IsNew = true;
                                 objDonthuocChitiet.Save();
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                             }
                             else
                             {
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                                 new Update(KcbDonthuocChitiet.Schema)
                                     .Set(KcbDonthuocChitiet.SoLuongColumn).EqualTo(objDonthuocChitiet.SoLuong)
                                     .Set(KcbDonthuocChitiet.NgaySuaColumn).EqualTo(objDonthuocChitiet.NgaySua)
                                     .Set(KcbDonthuocChitiet.NguoiSuaColumn).EqualTo(objDonthuocChitiet.NguoiSua)
                                     .Where(KcbDonthuocChitiet.IdChitietdonthuocColumn).IsEqualTo(objDonthuocChitiet.IdChitietdonthuoc).Execute();
                             }
                         }
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error("Loi trong qua trinh luu don thuoc", exception);
                 return ActionResult.Error;
             }
         }

         public ActionResult CapnhatDonthuoc(KcbLuotkham objLuotkham, KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet,KcbChandoanKetluan  _KcbChandoanKetluan,ref int p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
         {
             try
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         //KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(objDonthuoc.IdDonthuoc);
                         p_intIdDonthuoc = objDonthuoc.IdDonthuoc;
                         new Update(KcbDonthuoc.Schema)
                             .Set(KcbDonthuoc.Columns.TaiKham).EqualTo(objDonthuoc.TaiKham)
                             .Set(KcbDonthuoc.Columns.NgayTaikham).EqualTo(objDonthuoc.NgayTaikham)
                             .Set(KcbDonthuoc.Columns.LoidanBacsi).EqualTo(objDonthuoc.LoidanBacsi)
                             .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                             .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                             .Set(KcbDonthuoc.Columns.IpMaysua).EqualTo(objDonthuoc.IpMaysua)
                             .Set(KcbDonthuoc.Columns.TenMaysua).EqualTo(objDonthuoc.TenMaysua)
                             .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                         if (Utility.Int32Dbnull(objDonthuoc.IdKham) > 0)
                         {
                             new Update(KcbDangkyKcb.Schema)
                          .Set(KcbDangkyKcb.Columns.IdBacsikham).EqualTo(objDonthuoc.IdBacsiChidinh)
                          .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objDonthuoc.IdKham).Execute();
                         }
                         decimal PtramBH = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);

                         foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                         {
                             objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                         }
                         if (objLuotkham.TrangthaiNoitru <= 0) CapnhatChandoan(_KcbChandoanKetluan);
                         foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                         {
                             
                             if (objDonthuocChitiet.IdChitietdonthuoc == -1)
                             {
                                 objDonthuocChitiet.IdKham = objDonthuoc.IdKham;
                                 objDonthuocChitiet.IsNew = true;
                                 objDonthuocChitiet.SluongLinh = 0;
                                 objDonthuocChitiet.SluongSua = 0;
                                 objDonthuocChitiet.TrangThai = 0;
                                 
                                 objDonthuocChitiet.IdThanhtoan = -1;
                                 objDonthuocChitiet.MaLuotkham = objDonthuoc.MaLuotkham;
                                 objDonthuocChitiet.IdBenhnhan = objDonthuoc.IdBenhnhan;
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 objDonthuocChitiet.CachDung = objDonthuocChitiet.MotaThem;
                                 ThemChitiet(objDonthuoc, objDonthuocChitiet, PtramBH, objLuotkham);
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                             }
                             else
                             {
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                                 new Update(KcbDonthuocChitiet.Schema)
                                     .Set(KcbDonthuocChitiet.SoLuongColumn).EqualTo(objDonthuocChitiet.SoLuong)
                                     .Set(KcbDonthuocChitiet.NgaySuaColumn).EqualTo(objDonthuocChitiet.NgaySua)
                                     .Set(KcbDonthuocChitiet.NguoiSuaColumn).EqualTo(objDonthuocChitiet.NguoiSua)
                                     .Set(KcbDonthuocChitiet.Columns.IpMaysua).EqualTo(objDonthuocChitiet.IpMaysua)
                                     .Set(KcbDonthuocChitiet.Columns.TenMaysua).EqualTo(objDonthuocChitiet.TenMaysua)
                                     .Where(KcbDonthuocChitiet.IdChitietdonthuocColumn).IsEqualTo(objDonthuocChitiet.IdChitietdonthuoc).Execute();
                             }
                         }
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error("Loi trong qua trinh luu don thuoc", exception);
                 return ActionResult.Error;
             }
         }

         public ActionResult CapnhatChitiet( long id_chitiet,decimal PtramBHYT, byte tu_tuc)
         {
             try
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         KcbDonthuocChitiet objChitietDonthuoc =KcbDonthuocChitiet.FetchByID(id_chitiet);
                         if (objChitietDonthuoc != null)
                         {
                             if (tu_tuc == 1)
                             {
                                 objChitietDonthuoc.BhytChitra = 0;
                                 objChitietDonthuoc.BnhanChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0);
                                 objChitietDonthuoc.PtramBhyt = 0;
                             }
                             else
                             {
                                 objChitietDonthuoc.BhytChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia) *
                                                             Utility.DecimaltoDbnull(PtramBHYT) / 100;

                                 objChitietDonthuoc.BnhanChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) -
                                                           Utility.DecimaltoDbnull(objChitietDonthuoc.BhytChitra, 0);
                                 objChitietDonthuoc.PtramBhyt = Utility.DecimaltoDbnull(PtramBHYT);
                             }
                             objChitietDonthuoc.IsNew = false;
                             objChitietDonthuoc.MarkOld();
                             objChitietDonthuoc.Save();
                         }
                         

                        

                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error("Loi trong qua trinh luu don thuoc", exception);
                 return ActionResult.Error;
             }
         }

    }
}
