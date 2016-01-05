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
            log = LogManager.GetLogger("KCB_KEDONTHUOC");
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
                     {
                         new Update(KcbDonthuocChitiet.Schema).Set(KcbDonthuocChitiet.Columns.SoLuong).EqualTo(soluong)
                             .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(iddetail).Execute();
                         new Update(TblKedonthuocTempt.Schema).Set(TblKedonthuocTempt.Columns.SoLuong).EqualTo(soluong)
                          .Where(TblKedonthuocTempt.Columns.IdChitietdonthuoc).IsEqualTo(iddetail).Execute();
                     }
                     else
                         lstIdChitietDonthuoc = lstIdChitietDonthuoc + "," + iddetail.ToString();
                     SPs.DonthuocXoaChitietNew(lstIdChitietDonthuoc).Execute();
                 }
                 scope.Complete();
             }
         }
         public static DataTable KcbThamkhamDulieuTiemchungTheoBenhnhan(long id_benhnhan)
         {
             return SPs.KcbThamkhamDulieuTiemchungTheoBenhnhan(id_benhnhan).GetDataSet().Tables[0];
         }
         public DataTable LaythongtinDonthuoc_In(int id_donthuoc)
         {
             return SPs.DonthuocLaythongtinDein(id_donthuoc).GetDataSet().Tables[0];
         }
         public DataTable Laythongtinchitietdonthuoc(long id_donthuoc)
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
                 if (objKcbChandoanKetluan == null || objKcbChandoanKetluan.IdBenhnhan <= 0 || objKcbChandoanKetluan.IdKham<=0) return ActionResult.Cancel;
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         SqlQuery sqlkt = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(objKcbChandoanKetluan.IdKham);
                         if (objKcbChandoanKetluan.IsNew || sqlkt.GetRecordCount()<=0)
                         {
                            var sp= SPs.SpKcbThemmoiChandoanKetluan(objKcbChandoanKetluan.IdChandoan, objKcbChandoanKetluan.IdKham, objKcbChandoanKetluan.IdBenhnhan, objKcbChandoanKetluan.MaLuotkham
                                 , objKcbChandoanKetluan.IdBacsikham, objKcbChandoanKetluan.NgayChandoan, objKcbChandoanKetluan.NguoiTao, objKcbChandoanKetluan.NgayTao, objKcbChandoanKetluan.IdKhoanoitru
                                 , objKcbChandoanKetluan.IdBuonggiuong, objKcbChandoanKetluan.IdBuong, objKcbChandoanKetluan.IdGiuong, objKcbChandoanKetluan.IdPhieudieutri
                                 , objKcbChandoanKetluan.Noitru, objKcbChandoanKetluan.IdPhongkham, objKcbChandoanKetluan.TenPhongkham, objKcbChandoanKetluan.Mach
                                 , objKcbChandoanKetluan.Nhietdo, objKcbChandoanKetluan.Huyetap, objKcbChandoanKetluan.Nhiptim, objKcbChandoanKetluan.Nhiptho, objKcbChandoanKetluan.Cannang
                                 , objKcbChandoanKetluan.Chieucao, objKcbChandoanKetluan.Nhommau, objKcbChandoanKetluan.Ketluan, objKcbChandoanKetluan.HuongDieutri, objKcbChandoanKetluan.SongayDieutri
                                 , objKcbChandoanKetluan.TrieuchungBandau, objKcbChandoanKetluan.Chandoan, objKcbChandoanKetluan.ChandoanKemtheo, objKcbChandoanKetluan.MabenhChinh, objKcbChandoanKetluan.MabenhPhu
                                 , objKcbChandoanKetluan.IpMaytao, objKcbChandoanKetluan.TenMaytao, objKcbChandoanKetluan.PhanungSautiemchung, objKcbChandoanKetluan.KPL1
                                 , objKcbChandoanKetluan.KPL2, objKcbChandoanKetluan.KPL3, objKcbChandoanKetluan.KPL4, objKcbChandoanKetluan.KPL5, objKcbChandoanKetluan.KPL6
                                 , objKcbChandoanKetluan.KPL7, objKcbChandoanKetluan.KPL8, objKcbChandoanKetluan.KL1, objKcbChandoanKetluan.KL2, objKcbChandoanKetluan.KL3
                                 , objKcbChandoanKetluan.KetluanNguyennhan, objKcbChandoanKetluan.NhanXet, objKcbChandoanKetluan.ChongchidinhKhac, objKcbChandoanKetluan.SoNgayhen);

                            sp.Execute();
                            objKcbChandoanKetluan.IdChandoan = Utility.Int64Dbnull(sp.OutputValues[0]);
                         }
                         else
                         {
                             SPs.SpKcbCapnhatChandoanKetluan(objKcbChandoanKetluan.IdChandoan
                                  , objKcbChandoanKetluan.IdBacsikham,objKcbChandoanKetluan.NgayChandoan, objKcbChandoanKetluan.NguoiSua, objKcbChandoanKetluan.NgaySua, objKcbChandoanKetluan.IdPhieudieutri
                                  , objKcbChandoanKetluan.Noitru, objKcbChandoanKetluan.IdPhongkham, objKcbChandoanKetluan.TenPhongkham, objKcbChandoanKetluan.Mach
                                  , objKcbChandoanKetluan.Nhietdo, objKcbChandoanKetluan.Huyetap, objKcbChandoanKetluan.Nhiptim, objKcbChandoanKetluan.Nhiptho, objKcbChandoanKetluan.Cannang
                                  , objKcbChandoanKetluan.Chieucao, objKcbChandoanKetluan.Nhommau, objKcbChandoanKetluan.Ketluan, objKcbChandoanKetluan.HuongDieutri, objKcbChandoanKetluan.SongayDieutri
                                  , objKcbChandoanKetluan.TrieuchungBandau, objKcbChandoanKetluan.Chandoan, objKcbChandoanKetluan.ChandoanKemtheo, objKcbChandoanKetluan.MabenhChinh, objKcbChandoanKetluan.MabenhPhu
                                  , objKcbChandoanKetluan.IpMaytao, objKcbChandoanKetluan.TenMaytao, objKcbChandoanKetluan.PhanungSautiemchung, objKcbChandoanKetluan.KPL1
                                  , objKcbChandoanKetluan.KPL2, objKcbChandoanKetluan.KPL3, objKcbChandoanKetluan.KPL4, objKcbChandoanKetluan.KPL5, objKcbChandoanKetluan.KPL6
                                  , objKcbChandoanKetluan.KPL7, objKcbChandoanKetluan.KPL8, objKcbChandoanKetluan.KL1, objKcbChandoanKetluan.KL2, objKcbChandoanKetluan.KL3
                                  , objKcbChandoanKetluan.KetluanNguyennhan, objKcbChandoanKetluan.NhanXet, objKcbChandoanKetluan.ChongchidinhKhac, objKcbChandoanKetluan.SoNgayhen).Execute();
                         }
                         DataTable dtData = SPs.SpKcbLaydulieuChandoanKetluanTheoluotkham(objKcbChandoanKetluan.IdBenhnhan, objKcbChandoanKetluan.MaLuotkham).GetDataSet().Tables[0];
                         var query = (from chandoan in dtData.AsEnumerable()
                                      let y = Utility.sDbnull(chandoan["Chandoan"])
                                      where (y != "")
                                      select y).ToArray();
                         string cdchinh = string.Join(";", query);
                         var querychandoanphu = (from chandoan in dtData.AsEnumerable()
                                                 let y = Utility.sDbnull(chandoan["chandoan_kemtheo"])
                                                 where (y != "")
                                                 select y).ToArray();
                         string cdphu = string.Join(";", querychandoanphu);
                         var querybenhchinh = (from benhchinh in dtData.AsEnumerable()
                                               let y = Utility.sDbnull(benhchinh["mabenh_chinh"])
                                               where (y != "")
                                               select y).ToArray();
                         string mabenhchinh = string.Join(";", querybenhchinh);

                         var querybenhphu = (from benhphu in dtData.AsEnumerable()
                                             let y = Utility.sDbnull(benhphu["mabenh_phu"])
                                             where (y != "")
                                             select y).ToArray();
                         string mabenhphu = string.Join(";", querybenhphu);
                         SPs.SpKcbCapnhatMabenhChoLuotkham(objKcbChandoanKetluan.IdBenhnhan, objKcbChandoanKetluan.MaLuotkham,
                             mabenhchinh, mabenhphu, cdchinh, globalVariables.SysDate, globalVariables.UserName).Execute();
                     }

                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error("Loi trong qua trinh chuyen vien khoi noi tru {0}", exception);
                 return ActionResult.Error;
             }
         }
         public ActionResult ThemDonThuoc(KcbDanhsachBenhnhan objBenhnhan,  KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet, ref long p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
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
                 log.Error(string.Format("Loi khi them moi don thuoc {0}", exception.Message));
                 return ActionResult.Error;
             }

         }
         public ActionResult ThemDonThuoc(KcbLuotkham objLuotkham, KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet, KcbChandoanKetluan _KcbChandoanKetluan, ref long p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
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
                             log.Trace("4. Bat dau luu thuoc vao CSDL");
                             if (objDonthuoc.NgayKedon <= Convert.ToDateTime("01/01/1900"))
                                 objDonthuoc.NgayKedon = globalVariables.SysDate;

                             objDonthuoc.IsNew = true;
                             objDonthuoc.TenDonthuoc = THU_VIEN_CHUNG.TaoTenDonthuoc(objLuotkham.MaLuotkham,
                                                                                        Utility.Int32Dbnull(
                                                                                            objLuotkham.IdBenhnhan,
                                                                                            -1));
                             var sp = SPs.SpKcbThemmoiDonthuoc(objDonthuoc.IdDonthuoc, objDonthuoc.IdPhieudieutri, objDonthuoc.IdKhoadieutri, objDonthuoc.IdDonthuocthaythe, objDonthuoc.IdKham
                                 , objDonthuoc.IdBenhnhan, objDonthuoc.MaLuotkham, objDonthuoc.NgayKedon, objDonthuoc.IdBacsiChidinh, objDonthuoc.TrangThai, objDonthuoc.TthaiTonghop
                                 , objDonthuoc.TrangthaiThanhtoan, objDonthuoc.NgayThanhtoan, objDonthuoc.IdGoi, objDonthuoc.TrongGoi, objDonthuoc.NguoiTao, objDonthuoc.NgayTao
                                 , objDonthuoc.MotaThem, objDonthuoc.TenDonthuoc, objDonthuoc.MaDoituongKcb, objDonthuoc.Noitru, objDonthuoc.KieuDonthuoc, objDonthuoc.IdPhongkham
                                 , objDonthuoc.IdBuongGiuong, objDonthuoc.IdBuongNoitru, objDonthuoc.IdGiuongNoitru, objDonthuoc.LoidanBacsi, objDonthuoc.NgayTaikham
                                 , objDonthuoc.TaiKham, objDonthuoc.MaKhoaThuchien, objDonthuoc.NgayCapphat, objDonthuoc.KieuThuocvattu, objDonthuoc.NgayChot
                                 , objDonthuoc.IdChot, objDonthuoc.NgayHuychot, objDonthuoc.NguoiHuychot, objDonthuoc.LydoHuychot, objDonthuoc.NgayHuyxacnhan, objDonthuoc.NguoiHuyxacnhan
                                 , objDonthuoc.LydoHuyxacnhan, objDonthuoc.NgayXacnhan, objDonthuoc.NguoiXacnhan, objDonthuoc.IdLichsuDoituongKcb, objDonthuoc.MatheBhyt
                                 , objDonthuoc.IpMaytao, objDonthuoc.TenMaytao, objDonthuoc.LastActionName);
                             sp.Execute();
                             objDonthuoc.IdDonthuoc = Utility.Int64Dbnull(sp.OutputValues[0]);
                             log.Trace("4.1 Da luu don thuoc CSDL");
                                 SPs.SpKcbCapnhatBacsiKham(objDonthuoc.IdKham, objDonthuoc.IdBacsiChidinh, 0).Execute();
                                 log.Trace("4.2 Cap nhat bac si kham = BS ke don");

                             if (!Utility.Byte2Bool(objDonthuoc.Noitru))
                                 CapnhatChandoan(_KcbChandoanKetluan);
                             log.Trace("4.3 Da luu chan doan ket luan");
                             p_intIdDonthuoc = objDonthuoc.IdDonthuoc;
                             decimal PtramBH = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);
                             bool TUDONGDANHDAU_TRANGTHAISUDUNG = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEMCHUNG_TUDONGDANHDAU_TRANGTHAISUDUNG", "0", false) == "1";
                             foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                             {
                                 objDonthuocChitiet.IdKham = objDonthuoc.IdKham;
                                 objDonthuocChitiet.MaLuotkham = objDonthuoc.MaLuotkham;
                                 objDonthuocChitiet.IdBenhnhan = objDonthuoc.IdBenhnhan;
                                 objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                                 objDonthuocChitiet.NgaySudung = objDonthuoc.NgayKedon;
                                 objDonthuocChitiet.DaDung=Utility.Bool2byte(TUDONGDANHDAU_TRANGTHAISUDUNG);
                                 ThemChitiet(objDonthuoc, objDonthuocChitiet, PtramBH, objLuotkham);
                                 if (!lstChitietDonthuoc.ContainsKey(objDonthuocChitiet.IdThuockho.Value))
                                     lstChitietDonthuoc.Add(objDonthuocChitiet.IdThuockho.Value, objDonthuocChitiet.IdChitietdonthuoc);
                             }
                             log.Trace("4.4 Da luu xong chi tiet don thuoc");

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
                 var sp = SPs.SpKcbThemmoiChitietDonthuoc(objDonthuocChitiet.IdChitietdonthuoc, objDonthuocChitiet.IdDonthuoc, objDonthuocChitiet.IdDonthuocChuyengoi
                     , objDonthuocChitiet.IdBenhnhan, objDonthuocChitiet.MaLuotkham, objDonthuocChitiet.IdKham, objDonthuocChitiet.IdKho, objDonthuocChitiet.IdThuoc, objDonthuocChitiet.NgayHethan
                     , objDonthuocChitiet.SoLuong, objDonthuocChitiet.SluongSua, objDonthuocChitiet.SluongLinh, objDonthuocChitiet.DonGia, objDonthuocChitiet.IdThuockho
                     , objDonthuocChitiet.NgayNhap, objDonthuocChitiet.GiaNhap, objDonthuocChitiet.GiaBan, objDonthuocChitiet.GiaBhyt, objDonthuocChitiet.SoLo, objDonthuocChitiet.Vat
                     , objDonthuocChitiet.MaNhacungcap, objDonthuocChitiet.PhuThu, objDonthuocChitiet.PhuthuDungtuyen, objDonthuocChitiet.PhuthuTraituyen, objDonthuocChitiet.MotaThem
                     , objDonthuocChitiet.SoluongHuy, objDonthuocChitiet.TrangthaiHuy, objDonthuocChitiet.NguoiHuy, objDonthuocChitiet.NgayHuy, objDonthuocChitiet.TuTuc, objDonthuocChitiet.TrangThai
                     , objDonthuocChitiet.TrangthaiTonghop, objDonthuocChitiet.NgayXacnhan, objDonthuocChitiet.TrangthaiBhyt, objDonthuocChitiet.SttIn, objDonthuocChitiet.MadoituongGia
                     , objDonthuocChitiet.PtramBhytGoc, objDonthuocChitiet.PtramBhyt, objDonthuocChitiet.BhytChitra, objDonthuocChitiet.BnhanChitra, objDonthuocChitiet.MaDoituongKcb
                     , objDonthuocChitiet.IdThanhtoan, objDonthuocChitiet.TrangthaiThanhtoan, objDonthuocChitiet.NgayThanhtoan, objDonthuocChitiet.CachDung
                     , objDonthuocChitiet.ChidanThem, objDonthuocChitiet.DonviTinh, objDonthuocChitiet.SolanDung, objDonthuocChitiet.SoluongDung, objDonthuocChitiet.TrangthaiChuyen
                     , objDonthuocChitiet.NgayTao, objDonthuocChitiet.NguoiTao, objDonthuocChitiet.TileChietkhau, objDonthuocChitiet.TienChietkhau, objDonthuocChitiet.KieuChietkhau
                     , objDonthuocChitiet.IdGoi, objDonthuocChitiet.TrongGoi, objDonthuocChitiet.KieuBiendong, objDonthuocChitiet.NguonThanhtoan, objDonthuocChitiet.IpMaytao
                     , objDonthuocChitiet.TenMaytao, objDonthuocChitiet.DaDung, objDonthuocChitiet.LydoTiemchung, objDonthuocChitiet.NguoiTiem, objDonthuocChitiet.VitriTiem
                     , objDonthuocChitiet.MuiThu, objDonthuocChitiet.NgayhenMuiketiep, objDonthuocChitiet.PhanungSautiem, objDonthuocChitiet.Xutri, objDonthuocChitiet.KetluanNguyennhan
                     , objDonthuocChitiet.KetQua, objDonthuocChitiet.NgaySudung, objDonthuocChitiet.SoDky, objDonthuocChitiet.SoQdinhthau, objDonthuoc.NgayKedon);
                 sp.Execute();
                 objDonthuocChitiet.IdChitietdonthuoc = Utility.Int64Dbnull(sp.OutputValues[0]);
                 scope.Complete();
             }

             


         }
         public ActionResult CapnhatDonthuoc(KcbDanhsachBenhnhan objBenhnhan, KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet, ref long p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
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

         public ActionResult CapnhatDonthuoc(KcbLuotkham objLuotkham, KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDonthuocChitiet, KcbChandoanKetluan _KcbChandoanKetluan, ref long p_intIdDonthuoc, ref Dictionary<long, long> lstChitietDonthuoc)
         {
             try
             {
                 using (TransactionScope scope = new TransactionScope())
                 {
                     using (SharedDbConnectionScope sh = new SharedDbConnectionScope())
                     {
                         log.Trace("4. Bat dau cap nhat don thuoc");
                         p_intIdDonthuoc = objDonthuoc.IdDonthuoc;
                         SPs.SpKcbCapnhatDonthuoc(objDonthuoc.IdDonthuoc, globalVariables.UserName, globalVariables.SysDate, objDonthuoc.LoidanBacsi,
                             objDonthuoc.NgayTaikham, objDonthuoc.TaiKham, objDonthuoc.IpMaysua, objDonthuoc.TenMaysua).Execute();
                         log.Trace("4.1 Da cap nhat don thuoc");
                         if (Utility.Int32Dbnull(objDonthuoc.IdKham) > 0)
                         {
                             SPs.SpKcbCapnhatBacsiKham(objDonthuoc.IdKham, objDonthuoc.IdBacsiChidinh,0).Execute();
                             log.Trace("4.2 Da cap nhat BS kham=BS ke don");
                         }
                         decimal PtramBH = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0);

                         foreach (KcbDonthuocChitiet objDonthuocChitiet in arrDonthuocChitiet)
                         {
                             objDonthuocChitiet.IdDonthuoc = objDonthuoc.IdDonthuoc;
                         }
                         if (objLuotkham.TrangthaiNoitru <= 0) CapnhatChandoan(_KcbChandoanKetluan);
                         log.Trace("4.3 Da cap nhat thong tin chan doan ket luan");
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
                                 SPs.SpKcbCapnhatChitietDonthuoc(objDonthuocChitiet.IdChitietdonthuoc, objDonthuocChitiet.SoLuong, objDonthuocChitiet.NgaySua
                                     , objDonthuocChitiet.NguoiSua, objDonthuocChitiet.IpMaysua, objDonthuocChitiet.TenMaysua).Execute();
                             }
                         }
                         log.Trace("4.4 Da cap nhat xong chi tiet don thuoc");
                     }
                     scope.Complete();
                     return ActionResult.Success;
                 }
             }
             catch (Exception exception)
             {
                 log.Error(string.Format("Loi khi cap nhat don thuoc {0}", exception.Message));
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
                         }          }
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
