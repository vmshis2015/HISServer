using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using NLog;
using System.Transactions;
using VNS.Properties;
namespace VNS.HIS.NGHIEPVU.THUOC
{
   public class PhieuTraLai
    {
         private NLog.Logger log;
         public PhieuTraLai()
        {
            log = NLog.LogManager.GetCurrentClassLogger();

        } 
        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="arrPhieuNhapCts"></param>
        /// <returns></returns>
        public ActionResult ThemPhieuTraLaiKho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet[] arrPhieuNhapCts)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        objPhieuNhap.NgayTao = globalVariables.SysDate;
                        objPhieuNhap.NguoiTao = globalVariables.UserName;
                        objPhieuNhap.MaPhieu = Utility.sDbnull(THU_VIEN_CHUNG.MaTraLaiKho());
                        objPhieuNhap.IsNew = true;
                        objPhieuNhap.Save();
                        if (objPhieuNhap != null)
                        {
                          
                            foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapTraCt in arrPhieuNhapCts)
                            {
                                objPhieuNhapTraCt.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapTraCt.GiaNhap) *
                                                          Utility.Int32Dbnull(objPhieuNhapTraCt.SoLuong);
                                objPhieuNhapTraCt.IdPhieu = Utility.Int32Dbnull(objPhieuNhap.IdPhieu, -1);
                                objPhieuNhapTraCt.IsNew = true;
                                objPhieuNhapTraCt.Save();
                            }
                        }

                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh them phieu nhap kho :{0}", exception);
                return ActionResult.Error;

            }
        }
       /// <summary>
       /// hàm thưc hiện việc xóa thông tin của phiếu nhập trả kho lẻ
       /// </summary>
       /// <param name="IDPhieu"></param>
       /// <returns></returns>
        public ActionResult XoaPhieuNhapTraKho(int IDPhieu)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Delete().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(IDPhieu).Execute();
                        new Delete().From(TPhieuNhapxuatthuoc.Schema)
                           .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(IDPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xóa phiếu trả thuốc từ kho lẻ về kho chẵn", ex);
                return ActionResult.Error;

            }
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin nhập kho thuốc
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="arrPhieuNhapCts"></param>
        /// <returns></returns>
        public ActionResult UpdatePhieuTraLaiKho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet[] arrPhieuNhapCts)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        objPhieuNhap.NgaySua =  globalVariables.SysDate;
                        objPhieuNhap.NguoiSua = globalVariables.UserName;
                        objPhieuNhap.MarkOld();
                        objPhieuNhap.IsNew = false;
                        objPhieuNhap.Save();
                        new Delete().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).Execute();

                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapTraCt in arrPhieuNhapCts)
                        {
                            objPhieuNhapTraCt.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapTraCt.GiaNhap)*
                                                          Utility.Int32Dbnull(objPhieuNhapTraCt.SoLuong);
                            objPhieuNhapTraCt.IdPhieu = Utility.Int32Dbnull(objPhieuNhap.IdPhieu, -1);
                            objPhieuNhapTraCt.IsNew = true;
                            objPhieuNhapTraCt.Save();
                        }
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh sua phieu nhap kho :{0}", exception);
                return ActionResult.Error;

            }
        }
        /// <summary>
        /// Kiểm tra xem thuốc trong kho xuất đã được sử dụng hay chưa?
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="objPhieuNhapCt"></param>
        /// <returns></returns>
        public ActionResult Kiemtrathuochuyxacnhan(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt)
        {
            TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
              TThuockho.CreateQuery()
              .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhonhap)
              .AND(TThuockho.IdThuocColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuoc)
              .AND(TThuockho.NgayHethanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayHethan.Date)
              .AND(TThuockho.GiaNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaNhap)
              .AND(TThuockho.GiaBanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBan)
              );

            if (vCollection.Count <= 0) return ActionResult.Exceed;//Lỗi không có dòng dữ liệu trong bảng kho-thuốc
            int SoLuong = vCollection[0].SoLuong;
            SoLuong = SoLuong - objPhieuNhapCt.SoLuong;
            if (SoLuong < 0) return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
            return ActionResult.Success;
        }
        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult HuyXacNhanPhieuTralaiKho(TPhieuNhapxuatthuoc objPhieuNhapTra,ref string ErrMsg)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhapTra.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhaptraCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();

                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapTraCt in objPhieuNhaptraCtCollection)
                        {
                            //Kiểm tra ở kho lĩnh xem thuốc đã sử dụng chưa
                            ActionResult _Kiemtrathuochuyxacnhan = Kiemtrathuochuyxacnhan(objPhieuNhapTra, objPhieuNhapTraCt);
                            if (_Kiemtrathuochuyxacnhan != ActionResult.Success) return _Kiemtrathuochuyxacnhan;

                            //Xóa toàn bộ chi tiết trong TBiendongThuoc
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhapTra.IdPhieu)
                                .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapTraCt.IdPhieuchitiet)
                                .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo(LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan).Execute();

                            //Xóa toàn bộ chi tiết trong TBiendongThuoc
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhapTra.IdPhieu)
                                .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapTraCt.IdPhieuchitiet)
                                .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo(LoaiPhieu.PhieuXuatKhoLeTraKhoChan).Execute();

                            new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdChuyen).EqualTo(-1)
                             .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapTraCt.IdPhieuchitiet).Execute();
                            int id_thuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapTraCt.NgayHethan, objPhieuNhapTraCt.GiaNhap, objPhieuNhapTraCt.GiaBan,
                                                                      objPhieuNhapTraCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapTraCt.Vat),
                                                                      objPhieuNhapTraCt.IdThuoc, objPhieuNhapTra.IdKhoxuat,
                                                                      objPhieuNhapTraCt.MaNhacungcap, objPhieuNhapTraCt.SoLo, objPhieuNhapTraCt.SoDky, objPhieuNhapTraCt.SoQdinhthau, -1, id_thuockho,
                                                                      objPhieuNhapTraCt.NgayNhap, objPhieuNhapTraCt.GiaBhyt, objPhieuNhapTraCt.GiaPhuthuDungtuyen, objPhieuNhapTraCt.GiaPhuthuTraituyen, objPhieuNhapTraCt.KieuThuocvattu);
                            sp.Execute();
                            sp = SPs.ThuocXuatkho(objPhieuNhapTra.IdKhonhap, objPhieuNhapTraCt.IdThuoc,
                                                          objPhieuNhapTraCt.NgayHethan, objPhieuNhapTraCt.GiaNhap, objPhieuNhapTraCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapTraCt.Vat),
                                                          Utility.Int32Dbnull(objPhieuNhapTraCt.SoLuong), objPhieuNhapTraCt.IdChuyen, objPhieuNhapTraCt.MaNhacungcap, objPhieuNhapTraCt.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                        }
                       

                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(0)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhapTra.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhapTra.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
               ErrMsg="Lỗi khi hủy xác nhận phiếu trả thuốc từ kho lẻ về kho chẵn-->"+ex.Message;
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult XacNhanTraLaiKhoLeVeKhoChan(TPhieuNhapxuatthuoc objPhieuNhap, DateTime _ngayxacnhan,ref string ErrMsg)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            long idthuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                   objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                   objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap, objPhieuNhapCt.MaNhacungcap,
                                                                   objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, objPhieuNhapCt.IdThuockho, idthuockho, objPhieuNhapCt.NgayNhap, objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu);
                            sp.Execute();
                            idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            //log.Info(string.Format("Nhạp tra lai kho {0} voi so phieu {1}", objPhieuNhap.IdKhonhan, objPhieuNhapCt.IdPhieuChitiet));

                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                         objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                         Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                         Utility.Int32Dbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdThuockho,
                                                         objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                            new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdChuyen).EqualTo(idthuockho)
                              .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                            objPhieuNhapCt.IdThuockho = idthuockho;
                            ///phiếu nhập trả từ kho lẻ về kho chẵn
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = Utility.sDbnull(objPhieuNhapCt.KieuThuocvattu);
                            objXuatNhap.MotaThem = objPhieuNhap.MotaThem;
                            objXuatNhap.SoChungtuKemtheo = "";
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.GiaBhyt = objPhieuNhapCt.GiaBhyt;
                            objXuatNhap.GiaBhytCu = 0;
                            objXuatNhap.ThuocVay = 0;
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;
                            objXuatNhap.DuTru = 0;


                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdThuockho = objPhieuNhapCt.IdThuockho;
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao = globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan);
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan);
                            objXuatNhap.NgayBiendong = _ngayxacnhan;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                           
                            ///phiếu xuất về kho từ kho lẻ
                            objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet= Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = Utility.sDbnull(objPhieuNhapCt.KieuThuocvattu);
                            objXuatNhap.MotaThem = objPhieuNhap.MotaThem;
                            objXuatNhap.SoChungtuKemtheo = "";
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.GiaBhyt = objPhieuNhapCt.GiaBhyt;
                            objXuatNhap.GiaBhytCu = 0;
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;
                            objXuatNhap.DuTru = 0;
                            objXuatNhap.ThuocVay = 0;

                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                             objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.IdThuockho = objPhieuNhapCt.IdThuockho;
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                          
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuXuatKhoLeTraKhoChan);
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuXuatKhoLeTraKhoChan);
                            objXuatNhap.NgayBiendong = _ngayxacnhan;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                           

                        }
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo( globalVariables.SysDate)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(1)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
               ErrMsg="Lỗi khi xác nhận phiếu trả thuốc từ kho lẻ về kho chẵn-->"+ex.Message;
                return ActionResult.Error;

            }

        }

        /// <summary>
        /// hàm thực hiện việc xác nhận phiếu nhập
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult XacNhanPhieuNhapTraNhaCungCap(TPhieuNhapxuatthuoc objPhieuNhap)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieuNhapxuatthuocChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieuNhapxuatthuocChitietCollection>();
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                           
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.MotaThem = objPhieuNhap.MotaThem;
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                            objXuatNhap.MaNhacungcap = objPhieuNhap.MaNhacungcap;
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuTraNCC);
                            objXuatNhap.TenLoaiphieu = Utility.sDbnull(objPhieuNhap.TenLoaiphieu);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            int id_thuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                   objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                   objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap,
                                                                   objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objPhieuNhapCt.SoDky, objPhieuNhapCt.SoQdinhthau, -1, id_thuockho,
                                                                   objPhieuNhap.NgayXacnhan, objPhieuNhapCt.GiaBhyt,
                                                                   objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen, objPhieuNhapCt.KieuThuocvattu);
                            sp.Execute();
                            log.Info(string.Format("Nhạp tra lai kho {0} voi so phieu {1}", objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdPhieuchitiet));
                            ///phiếu xuất về kho từ kho lẻ
                            objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.SoDky = objPhieuNhapCt.SoDky;
                            objXuatNhap.SoQdinhthau = objPhieuNhapCt.SoQdinhthau;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                            objXuatNhap.MaNhacungcap = objPhieuNhap.MaNhacungcap;
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuXuatKho);
                            objXuatNhap.TenLoaiphieu = Utility.sDbnull(objPhieuNhap.TenLoaiphieu);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.MotaThem = objPhieuNhap.MotaThem;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.Int32Dbnull(objXuatNhap.SoLuong), objPhieuNhapCt.IdThuockho, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                            log.Info(string.Format("xuat tra lai kho {0} voi so phieu {1}", objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdPhieuchitiet));

                        }
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo( globalVariables.SysDate)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(1)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi ban ra tu sp :{0}", errorMessage);
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;

            }

        }
    }
}
