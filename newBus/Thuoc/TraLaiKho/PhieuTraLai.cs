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
        public ActionResult ThemPhieuTraLaiKho(TPhieutrathuocKholeVekhochan objPhieuNhap, TPhieutrathuocKholeVekhochanChitiet[] arrPhieuNhapCts)
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
                          
                            foreach (TPhieutrathuocKholeVekhochanChitiet objPhieuNhapTraCt in arrPhieuNhapCts)
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
                        new Delete().From(TPhieutrathuocKholeVekhochanChitiet.Schema)
                            .Where(TPhieutrathuocKholeVekhochanChitiet.Columns.IdPhieu).IsEqualTo(IDPhieu).Execute();
                        new Delete().From(TPhieutrathuocKholeVekhochan.Schema)
                           .Where(TPhieutrathuocKholeVekhochan.Columns.IdPhieu).IsEqualTo(IDPhieu).Execute();
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
        public ActionResult UpdatePhieuTraLaiKho(TPhieutrathuocKholeVekhochan objPhieuNhap, TPhieutrathuocKholeVekhochanChitiet[] arrPhieuNhapCts)
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
                        new Delete().From(TPhieutrathuocKholeVekhochanChitiet.Schema)
                            .Where(TPhieutrathuocKholeVekhochanChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).Execute();

                        foreach (TPhieutrathuocKholeVekhochanChitiet objPhieuNhapTraCt in arrPhieuNhapCts)
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
        public ActionResult Kiemtrathuochuyxacnhan(TPhieutrathuocKholeVekhochan objPhieuNhap, TPhieutrathuocKholeVekhochanChitiet objPhieuNhapCt)
        {
            TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
              TThuockho.CreateQuery()
              .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhonhan)
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
        public ActionResult HuyXacNhanPhieuTralaiKho(TPhieutrathuocKholeVekhochan objPhieuNhapTra)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieutrathuocKholeVekhochanChitiet.Schema)
                            .Where(TPhieutrathuocKholeVekhochanChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhapTra.IdPhieu);
                        TPhieutrathuocKholeVekhochanChitietCollection objPhieuNhaptraCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieutrathuocKholeVekhochanChitietCollection>();

                        foreach (TPhieutrathuocKholeVekhochanChitiet objPhieuNhapTraCt in objPhieuNhaptraCtCollection)
                        {
                            //Kiểm tra ở kho lĩnh xem thuốc đã sử dụng chưa
                            ActionResult _Kiemtrathuochuyxacnhan = Kiemtrathuochuyxacnhan(objPhieuNhapTra, objPhieuNhapTraCt);
                            if (_Kiemtrathuochuyxacnhan != ActionResult.Success) return _Kiemtrathuochuyxacnhan;

                            //Xóa toàn bộ chi tiết trong TBiendongThuoc
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhapTra.IdPhieu)
                                .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapTraCt.IdPhieuChitiet)
                                .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo(LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan).Execute();

                            //Xóa toàn bộ chi tiết trong TBiendongThuoc
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhapTra.IdPhieu)
                                .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapTraCt.IdPhieuChitiet)
                                .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo(LoaiPhieu.PhieuXuatKhoLeTraKhoChan).Execute();

                            new Update(TPhieutrathuocKholeVekhochanChitiet.Schema).Set(TPhieutrathuocKholeVekhochanChitiet.Columns.IdChuyen).EqualTo(-1)
                             .Where(TPhieutrathuocKholeVekhochanChitiet.Columns.IdPhieuChitiet).IsEqualTo(objPhieuNhapTraCt.IdPhieuChitiet).Execute();
                            int id_thuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapTraCt.NgayHethan, objPhieuNhapTraCt.GiaNhap, objPhieuNhapTraCt.GiaBan,
                                                                      objPhieuNhapTraCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapTraCt.Vat),
                                                                      objPhieuNhapTraCt.IdThuoc, objPhieuNhapTra.IdKhotra, 
                                                                      objPhieuNhapTraCt.MaNhacungcap, objPhieuNhapTraCt.SoLo, -1,id_thuockho,
                                                                      objPhieuNhapTraCt.NgayNhap, objPhieuNhapTraCt.GiaBhyt, objPhieuNhapTraCt.PhuthuDungtuyen, objPhieuNhapTraCt.PhuthuTraituyen);
                            sp.Execute();
                            sp = SPs.ThuocXuatkho(objPhieuNhapTra.IdKhonhan, objPhieuNhapTraCt.IdThuoc,
                                                          objPhieuNhapTraCt.NgayHethan, objPhieuNhapTraCt.GiaNhap, objPhieuNhapTraCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapTraCt.Vat),
                                                          Utility.Int32Dbnull(objPhieuNhapTraCt.SoLuong), objPhieuNhapTraCt.IdChuyen, objPhieuNhapTraCt.MaNhacungcap, objPhieuNhapTraCt.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                        }
                       

                        new Update(TPhieutrathuocKholeVekhochan.Schema)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.IdNhanvien).EqualTo(null)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.TrangThai).EqualTo(0)
                            .Where(TPhieutrathuocKholeVekhochan.Columns.IdPhieu).IsEqualTo(objPhieuNhapTra.IdPhieu)
                            .And(TPhieutrathuocKholeVekhochan.LoaiPhieuColumn).IsEqualTo(objPhieuNhapTra.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi hủy xác nhận phiếu trả thuốc từ kho lẻ về kho chẵn",ex);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult XacNhanTraLaiKhoLeVeKhoChan(TPhieutrathuocKholeVekhochan objPhieuNhap, DateTime _ngayxacnhan)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieutrathuocKholeVekhochanChitiet.Schema)
                            .Where(TPhieutrathuocKholeVekhochanChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieutrathuocKholeVekhochanChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieutrathuocKholeVekhochanChitietCollection>();
                        foreach (TPhieutrathuocKholeVekhochanChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            long idthuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                   objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                   objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhan, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objPhieuNhapCt.IdThuockho, idthuockho, objPhieuNhapCt.NgayNhap, objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.PhuthuDungtuyen, objPhieuNhapCt.PhuthuTraituyen);
                            sp.Execute();
                            idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            //log.Info(string.Format("Nhạp tra lai kho {0} voi so phieu {1}", objPhieuNhap.IdKhonhan, objPhieuNhapCt.IdPhieuChitiet));

                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhotra, objPhieuNhapCt.IdThuoc,
                                                         objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                         Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                         Utility.Int32Dbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdThuockho,
                                                         objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                            new Update(TPhieutrathuocKholeVekhochanChitiet.Schema).Set(TPhieutrathuocKholeVekhochanChitiet.Columns.IdChuyen).EqualTo(idthuockho)
                              .Where(TPhieutrathuocKholeVekhochanChitiet.Columns.IdPhieuChitiet).IsEqualTo(objPhieuNhapCt.IdPhieuChitiet).Execute();
                            objPhieuNhapCt.IdThuockho = idthuockho;
                            ///phiếu nhập trả từ kho lẻ về kho chẵn
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuChitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayTra;
                            objXuatNhap.KieuThuocvattu = Utility.sDbnull(objPhieuNhapCt.KieuThuocvattu);

                            objXuatNhap.SoChungtuKemtheo = "";
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.GiaBhyt = objPhieuNhapCt.GiaBhyt;
                            objXuatNhap.GiaBhytCu = 0;

                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.PhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.PhuthuTraituyen;
                            objXuatNhap.DuTru = 0;


                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.IdThuockho = objPhieuNhapCt.IdThuockho;
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao = globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhonhan);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhan);
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
                            objXuatNhap.IdPhieuChitiet= Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuChitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayTra;
                            objXuatNhap.KieuThuocvattu = Utility.sDbnull(objPhieuNhapCt.KieuThuocvattu);

                            objXuatNhap.SoChungtuKemtheo = "";
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.GiaBhyt = objPhieuNhapCt.GiaBhyt;
                            objXuatNhap.GiaBhytCu = 0;
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.PhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.PhuthuTraituyen;
                            objXuatNhap.DuTru = 0;


                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.IdThuockho = objPhieuNhapCt.IdThuockho;
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhotra);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhotra);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                          
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuXuatKhoLeTraKhoChan);
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuXuatKhoLeTraKhoChan);
                            objXuatNhap.NgayBiendong = _ngayxacnhan;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                           

                        }
                        new Update(TPhieutrathuocKholeVekhochan.Schema)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.NgayXacnhan).EqualTo( globalVariables.SysDate)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.TrangThai).EqualTo(1)
                            .Where(TPhieutrathuocKholeVekhochan.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieutrathuocKholeVekhochan.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi xác nhận phiếu trả thuốc từ kho lẻ về kho chẵn",ex);
                return ActionResult.Error;

            }

        }

        /// <summary>
        /// hàm thực hiện việc xác nhận phiếu nhập
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult XacNhanPhieuNhapTraNhaCungCap(TPhieutrathuocKholeVekhochan objPhieuNhap)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TPhieutrathuocKholeVekhochanChitiet.Schema)
                            .Where(TPhieutrathuocKholeVekhochanChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu);
                        TPhieutrathuocKholeVekhochanChitietCollection objPhieuNhapCtCollection =
                            sqlQuery.ExecuteAsCollection<TPhieutrathuocKholeVekhochanChitietCollection>();
                        foreach (TPhieutrathuocKholeVekhochanChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            ///phiếu nhập trả từ kho lẻ về kho chẵn
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuChitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhonhan);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhan);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                            objXuatNhap.MaNhacungcap = objPhieuNhap.MaNhacungcap;
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuNhapTraLaiKhoLeVeKhoChan);
                            objXuatNhap.TenLoaiphieu = Utility.sDbnull(objPhieuNhap.TenLoaiphieu);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            int id_thuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                   objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                   objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhan, 
                                                                   objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, -1,id_thuockho,
                                                                   objPhieuNhap.NgayXacnhan, objPhieuNhapCt.GiaBhyt, 
                                                                   objPhieuNhapCt.PhuthuDungtuyen, objPhieuNhapCt.PhuthuTraituyen);
                            sp.Execute();
                            log.Info(string.Format("Nhạp tra lai kho {0} voi so phieu {1}", objPhieuNhap.IdKhonhan, objPhieuNhapCt.IdPhieuChitiet));
                            ///phiếu xuất về kho từ kho lẻ
                            objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuChitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = string.Empty;
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKhoaLinh = Utility.Int16Dbnull(objPhieuNhap.IdKhonhan);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhan);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
                            objXuatNhap.MaNhacungcap = objPhieuNhap.MaNhacungcap;
                            objXuatNhap.MaLoaiphieu = Utility.ByteDbnull(LoaiPhieu.PhieuXuatKho);
                            objXuatNhap.TenLoaiphieu = Utility.sDbnull(objPhieuNhap.TenLoaiphieu);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhotra, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.Int32Dbnull(objXuatNhap.SoLuong), objPhieuNhapCt.IdThuockho, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                            log.Info(string.Format("xuat tra lai kho {0} voi so phieu {1}", objPhieuNhap.IdKhotra, objPhieuNhapCt.IdPhieuChitiet));

                        }
                        new Update(TPhieutrathuocKholeVekhochan.Schema)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.NgayXacnhan).EqualTo( globalVariables.SysDate)
                            .Set(TPhieutrathuocKholeVekhochan.Columns.TrangThai).EqualTo(1)
                            .Where(TPhieutrathuocKholeVekhochan.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieutrathuocKholeVekhochan.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
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
