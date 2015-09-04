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
    public class noitru_nhapvien
    {
        private NLog.Logger log;
        public noitru_nhapvien()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public DataSet KcbLaythongtinthuocKetquaCls(string maluotkham, int? idbenhnhan)
        {
            return SPs.KcbLaythongtinthuocKetquacls(maluotkham, idbenhnhan).GetDataSet();
        }
        public DataTable NoitruKiemtraBuongGiuong(int idkhoanoitru,int idBuong, int idGiuong)
        {
            return SPs.NoitruKiemtraBuongGiuong(idkhoanoitru,idBuong, idGiuong).GetDataSet().Tables[0];
        }
        public DataTable NoitruLaythongtinInphieunhapvien(string maluotkham, int? idbenhnhan)
        {
            return SPs.NoitruLaythongtinInphieunhapvien(maluotkham, idbenhnhan).GetDataSet().Tables[0];
        }
        public static NoitruPhanbuonggiuong LaythongtinBuonggiuongHtai(KcbLuotkham objPatientExam)
        {
            NoitruPhanbuonggiuong objPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
                .Where(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objPatientExam.MaLuotkham)
                .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objPatientExam.IdBenhnhan)
                .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                .And(NoitruPhanbuonggiuong.Columns.TrangThai).IsEqualTo(0).ExecuteSingle<NoitruPhanbuonggiuong>();
            return objPhanbuonggiuong;
        }
        public static ActionResult CapnhatSoluong(long id, int soluongngay,byte cachtinhsoluong)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                      new Update(NoitruPhanbuonggiuong.Schema)
                                             .Set(NoitruPhanbuonggiuong.Columns.SoLuong).EqualTo(soluongngay)
                                             .Set(NoitruPhanbuonggiuong.Columns.CachtinhSoluong).EqualTo(cachtinhsoluong)
                                             .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;

                }
            }
            catch (Exception exception)
            {
                Utility.CatchException(exception);
                return ActionResult.Error;
            }

        }
        public static ActionResult Capnhatgia(long id, decimal don_gia, byte cachtinh_gia)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {

                        new Update(NoitruPhanbuonggiuong.Schema)
                                             .Set(NoitruPhanbuonggiuong.Columns.DonGia).EqualTo(don_gia)
                                             .Set(NoitruPhanbuonggiuong.Columns.CachtinhGia).EqualTo(cachtinh_gia)
                                             .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;

                }
            }
            catch (Exception exception)
            {
                Utility.CatchException(exception);
                return ActionResult.Error;
            }

        }
        public ActionResult HuyBenhNhanVaoBuongGuong(NoitruPhanbuonggiuong objPhanbuonggiuong, ref int IdChuyen)
        {
            IdChuyen = -1;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        if (objPhanbuonggiuong != null)
                        {
                            NoitruPhanbuonggiuongCollection _NoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
                                       .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                                       .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                                       .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
                            if (_NoitruPhanbuonggiuong.Count == 1)
                            {

                                ///update thông tin của phòng giường 
                                objPhanbuonggiuong.MarkOld();
                                objPhanbuonggiuong.IsLoaded = true;
                                objPhanbuonggiuong.NgaySua = globalVariables.SysDate;
                                objPhanbuonggiuong.NguoiSua = globalVariables.UserName;
                                objPhanbuonggiuong.NgayPhangiuong = null;
                                objPhanbuonggiuong.NguoiPhangiuong = "";
                                objPhanbuonggiuong.IdNhanvienPhangiuong = -1;
                                objPhanbuonggiuong.IdBuong = -1;
                                objPhanbuonggiuong.IdGiuong = -1;
                                objPhanbuonggiuong.DonGia = 0;
                                objPhanbuonggiuong.GiaGoc = 0;
                                objPhanbuonggiuong.BnhanChitra = 0;
                                objPhanbuonggiuong.BhytChitra = 0;
                                objPhanbuonggiuong.CachtinhGia = 0;
                                objPhanbuonggiuong.CachtinhSoluong = 0;
                                objPhanbuonggiuong.TrangthaiChuyen = 0;
                                objPhanbuonggiuong.TrangThai = 0;
                                objPhanbuonggiuong.Save();

                                new Update(KcbLuotkham.Schema)
                                   .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                   .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                   .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(objPhanbuonggiuong.IdKhoanoitru)
                                   .Set(KcbLuotkham.Columns.IdBuong).EqualTo(objPhanbuonggiuong.IdBuong)
                                   .Set(KcbLuotkham.Columns.IdGiuong).EqualTo(objPhanbuonggiuong.IdGiuong)
                                   .Set(KcbLuotkham.Columns.IdRavien).EqualTo(objPhanbuonggiuong.Id)
                                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                                   .Execute();

                            }
                            else//Xóa bản ghi phân buồng giường hiện tại. Đưa về bản ghi phân buồng giường trước đó
                            {
                                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_XOAKHIHUYGIUONG", "0", true) == "1")
                                {
                                    IdChuyen = Utility.Int32Dbnull(objPhanbuonggiuong.IdChuyen.Value, -1);
                                    NoitruPhanbuonggiuong _item = new Select().From(NoitruPhanbuonggiuong.Schema)
                                           .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(IdChuyen).ExecuteSingle<NoitruPhanbuonggiuong>();
                                    if (_item != null)
                                    {
                                        new Delete().From(NoitruPhanbuonggiuong.Schema).Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(objPhanbuonggiuong.Id).Execute();
                                        new Update(NoitruPhanbuonggiuong.Schema)
                                            .Set(NoitruPhanbuonggiuong.Columns.TrangThai).EqualTo(0)
                                            .Set(NoitruPhanbuonggiuong.Columns.TrangthaiChuyen).EqualTo(0)
                                            .Set(NoitruPhanbuonggiuong.Columns.SoLuong).EqualTo(0)
                                            .Set(NoitruPhanbuonggiuong.Columns.SoluongGio).EqualTo(0)
                                            .Set(NoitruPhanbuonggiuong.Columns.NgayKetthuc).EqualTo(null)
                                            .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(_item.Id).Execute();
                                        new Update(KcbLuotkham.Schema)
                                       .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                       .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                       .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(_item.IdKhoanoitru)
                                       .Set(KcbLuotkham.Columns.IdBuong).EqualTo(_item.IdBuong)
                                       .Set(KcbLuotkham.Columns.IdGiuong).EqualTo(_item.IdGiuong)
                                       .Set(KcbLuotkham.Columns.IdRavien).EqualTo(_item.Id)
                                       .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                                       .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                                       .Execute();

                                    }
                                }
                                else//Chỉ hủy giường
                                {
                                    IdChuyen = Utility.Int32Dbnull(objPhanbuonggiuong.IdChuyen.Value, -1);
                                    ///update thông tin của phòng giường 
                                    objPhanbuonggiuong.MarkOld();
                                    objPhanbuonggiuong.IsLoaded = true;
                                    objPhanbuonggiuong.NgaySua = globalVariables.SysDate;
                                    objPhanbuonggiuong.NguoiSua = globalVariables.UserName;
                                    objPhanbuonggiuong.NgayPhangiuong = null;
                                    objPhanbuonggiuong.NguoiPhangiuong = "";
                                    objPhanbuonggiuong.IdNhanvienPhangiuong = -1;
                                    objPhanbuonggiuong.IdBuong = -1;
                                    objPhanbuonggiuong.IdGiuong = -1;
                                    objPhanbuonggiuong.DonGia = 0;
                                    objPhanbuonggiuong.GiaGoc = 0;
                                    objPhanbuonggiuong.BnhanChitra = 0;
                                    objPhanbuonggiuong.BhytChitra = 0;
                                    objPhanbuonggiuong.CachtinhGia = 0;
                                    objPhanbuonggiuong.CachtinhSoluong = 0;
                                    objPhanbuonggiuong.TrangthaiChuyen = 0;
                                    objPhanbuonggiuong.TrangThai = 0;
                                    objPhanbuonggiuong.Save();

                                    new Update(KcbLuotkham.Schema)
                                       .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                       .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                       .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(objPhanbuonggiuong.IdKhoanoitru)
                                       .Set(KcbLuotkham.Columns.IdBuong).EqualTo(objPhanbuonggiuong.IdBuong)
                                       .Set(KcbLuotkham.Columns.IdGiuong).EqualTo(objPhanbuonggiuong.IdGiuong)
                                       .Set(KcbLuotkham.Columns.IdRavien).EqualTo(objPhanbuonggiuong.Id)
                                       .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                                       .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                                       .Execute();
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
                Utility.CatchException(exception);
                return ActionResult.Error;
            }

        }
        public ActionResult HuyKhoanoitru(NoitruPhanbuonggiuong objPhanbuonggiuong, ref int IdChuyen)
        {
            IdChuyen = -1;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        if (objPhanbuonggiuong != null)
                        {
                            NoitruPhanbuonggiuongCollection _NoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
                                       .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                                       .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                                       .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();

                            if (_NoitruPhanbuonggiuong.Count == 1)
                                if (!Utility.AcceptQuestion("Chú ý: Bệnh nhân mới nhập viện. Bạn có muốn hủy nhập viện cho bệnh nhân này hay không?", "Cảnh báo", true))
                                {
                                    return ActionResult.Cancel;
                                }
                            IdChuyen = Utility.Int32Dbnull(objPhanbuonggiuong.IdChuyen.Value, -1);
                            NoitruPhanbuonggiuong _item = new Select().From(NoitruPhanbuonggiuong.Schema)
                                   .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(IdChuyen).ExecuteSingle<NoitruPhanbuonggiuong>();
                            if (_item != null)//Chuyển về khoa trước
                            {
                                new Delete().From(NoitruPhanbuonggiuong.Schema).Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(objPhanbuonggiuong.Id).Execute();
                                new Update(NoitruPhanbuonggiuong.Schema)
                                    .Set(NoitruPhanbuonggiuong.Columns.TrangThai).EqualTo(0)
                                    .Set(NoitruPhanbuonggiuong.Columns.TrangthaiChuyen).EqualTo(0)
                                     .Set(NoitruPhanbuonggiuong.Columns.SoLuong).EqualTo(0)
                                            .Set(NoitruPhanbuonggiuong.Columns.SoluongGio).EqualTo(0)
                                            .Set(NoitruPhanbuonggiuong.Columns.NgayKetthuc).EqualTo(null)
                                    .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(_item.Id).Execute();
                                new Update(KcbLuotkham.Schema)
                               .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                               .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                               .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(_item.IdKhoanoitru)
                               .Set(KcbLuotkham.Columns.IdBuong).EqualTo(_item.IdBuong)
                               .Set(KcbLuotkham.Columns.IdGiuong).EqualTo(_item.IdGiuong)
                               .Set(KcbLuotkham.Columns.IdRavien).EqualTo(_item.Id)
                               .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                               .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                               .Execute();
                            }
                            else//Hủy nhập viện
                            {
                                KcbLuotkham _KcbLuotkham = new Select().From(KcbLuotkham.Schema)
                                    .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                                    .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham).ExecuteSingle<KcbLuotkham>();
                                Huynhapvien(_KcbLuotkham);
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
                return ActionResult.Error;
            }

        }
        public ActionResult ChuyenKhoaDieuTri(NoitruPhanbuonggiuong objPhanbuonggiuong, KcbLuotkham objPatientExam, DateTime NgayChuyenKhoa, short IDKhoaChuyenDen, short IDPhong, short IDGiuong)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        if (objPhanbuonggiuong != null)
                        {
                            if (Utility.Int32Dbnull(objPhanbuonggiuong.IdBuong, -1) == -1 && Utility.Int32Dbnull(objPhanbuonggiuong.IdGiuong, -1) == -1)
                            {
                                //Chỉ việc cập nhật lại khoa nội trú do chưa phân buồng giường
                                objPhanbuonggiuong.MarkOld();
                                objPhanbuonggiuong.IsLoaded = true;
                                objPhanbuonggiuong.SoLuong = 0;
                                objPhanbuonggiuong.NgayVaokhoa = NgayChuyenKhoa;
                                objPhanbuonggiuong.TrangthaiXacnhan = Utility.ByteDbnull(objPhanbuonggiuong.TrangthaiXacnhan);
                                objPhanbuonggiuong.NgaySua = globalVariables.SysDate;
                                objPhanbuonggiuong.NguoiSua = globalVariables.UserName;
                                objPhanbuonggiuong.IdKhoanoitru = IDKhoaChuyenDen;
                                objPhanbuonggiuong.Save();

                                new Update(KcbLuotkham.Schema)
                                  .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                  .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(objPhanbuonggiuong.IdKhoanoitru)
                                  .Set(KcbLuotkham.Columns.IdBuong).EqualTo(objPhanbuonggiuong.IdBuong)
                                  .Set(KcbLuotkham.Columns.IdGiuong).EqualTo(objPhanbuonggiuong.IdGiuong)
                                  .Set(KcbLuotkham.Columns.IdRavien).EqualTo(objPhanbuonggiuong.Id)
                                  .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                                  .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                                  .Execute();

                            }
                            else
                            {
                                ///update thông tin của phòng giường 
                                objPhanbuonggiuong.MarkOld();
                                objPhanbuonggiuong.IsLoaded = true;
                                objPhanbuonggiuong.TrangthaiXacnhan = Utility.ByteDbnull(objPhanbuonggiuong.TrangthaiXacnhan);
                                objPhanbuonggiuong.NgaySua = globalVariables.SysDate;
                                objPhanbuonggiuong.NguoiSua = globalVariables.UserName;
                                objPhanbuonggiuong.NgayKetthuc = NgayChuyenKhoa;
                                objPhanbuonggiuong.TrangthaiChuyen = 1;
                                objPhanbuonggiuong.TrangThai = 1;
                                objPhanbuonggiuong.Save();


                                //NewItem
                                objPhanbuonggiuong.IdChuyen = (int?)objPhanbuonggiuong.Id;
                                objPhanbuonggiuong.IdKhoachuyen = objPhanbuonggiuong.IdKhoanoitru;
                                objPhanbuonggiuong.Id = -1;
                                objPhanbuonggiuong.NgayVaokhoa = NgayChuyenKhoa.AddMinutes(1);

                                objPhanbuonggiuong.NgayKetthuc = null;
                                objPhanbuonggiuong.NoiTru = 1;
                                objPhanbuonggiuong.TrangThai = 0;
                                objPhanbuonggiuong.TrangthaiChuyen = 0;
                                objPhanbuonggiuong.IdKhoanoitru = IDKhoaChuyenDen;
                                objPhanbuonggiuong.SoLuong = 0;
                                objPhanbuonggiuong.IdBuong = -1;
                                objPhanbuonggiuong.IdGiuong = -1;
                                objPhanbuonggiuong.NgayPhangiuong = null;
                                objPhanbuonggiuong.NguoiPhangiuong = "";
                                objPhanbuonggiuong.IdNhanvienPhangiuong = -1;
                                if (IDPhong > 0)
                                    objPhanbuonggiuong.IdBuong = IDPhong;
                                if (IDGiuong > 0)
                                    objPhanbuonggiuong.IdGiuong = IDGiuong;
                                if (IDPhong > 0 || IDGiuong > 0)
                                {
                                    objPhanbuonggiuong.NgayPhangiuong = NgayChuyenKhoa;
                                    objPhanbuonggiuong.NguoiPhangiuong = globalVariables.UserName;
                                    objPhanbuonggiuong.IdNhanvienPhangiuong = globalVariables.gv_intIDNhanvien;
                                    LayThongTinGia(objPhanbuonggiuong, objPatientExam);
                                }

                                objPhanbuonggiuong.NguoiTao = globalVariables.UserName;
                                objPhanbuonggiuong.NgayTao = globalVariables.SysDate;

                                objPhanbuonggiuong.IsNew = true;
                                objPhanbuonggiuong.Save();

                                objPatientExam.IdRavien = Utility.Int32Dbnull(objPhanbuonggiuong.Id);

                                new Update(KcbLuotkham.Schema)
                                   .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                   .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                   .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(objPhanbuonggiuong.IdKhoanoitru)
                                   .Set(KcbLuotkham.Columns.IdBuong).EqualTo(objPhanbuonggiuong.IdBuong)
                                   .Set(KcbLuotkham.Columns.IdGiuong).EqualTo(objPhanbuonggiuong.IdGiuong)
                                   .Set(KcbLuotkham.Columns.IdRavien).EqualTo(objPhanbuonggiuong.Id)
                                   .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                                   .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                                   .Execute();


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
                return ActionResult.Error;
            }
        }
        public ActionResult ChuyenGiuongDieuTri(NoitruPhanbuonggiuong objPhanbuonggiuong, KcbLuotkham objPatientExam, DateTime NgayChuyenKhoa, short IDPhong, short IDGiuong, int IdGia)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        if (objPhanbuonggiuong != null)
                        {
                            ///update thông tin của buồng giường cũ
                            objPhanbuonggiuong.MarkOld();
                            objPhanbuonggiuong.IsLoaded = true;
                            objPhanbuonggiuong.NgaySua = globalVariables.SysDate;
                            objPhanbuonggiuong.NguoiSua = globalVariables.UserName;
                            objPhanbuonggiuong.NgayKetthuc = NgayChuyenKhoa;
                            objPhanbuonggiuong.TrangThai = 1;
                            objPhanbuonggiuong.TrangthaiChuyen = 1;
                            objPhanbuonggiuong.Save();

                            new Update(NoitruDmucGiuongbenh.Schema).Set(NoitruDmucGiuongbenh.Columns.DangSudung).EqualTo(0)
                                .Where(NoitruDmucGiuongbenh.Columns.IdGiuong).IsEqualTo(objPhanbuonggiuong.IdGiuong).Execute();
                            //NewItem
                            objPhanbuonggiuong.IdChuyen = (int?)objPhanbuonggiuong.Id;
                            objPhanbuonggiuong.IdKhoachuyen = objPhanbuonggiuong.IdKhoanoitru;
                            objPhanbuonggiuong.Id = -1;
                            objPhanbuonggiuong.NgayVaokhoa = NgayChuyenKhoa.AddMinutes(1);
                            objPhanbuonggiuong.NgayPhangiuong = NgayChuyenKhoa.AddMinutes(1);
                            objPhanbuonggiuong.NgayKetthuc = null;
                            objPhanbuonggiuong.NoiTru = 1;
                            objPhanbuonggiuong.SoLuong = 0;
                            objPhanbuonggiuong.TrangThai = 0;
                            objPhanbuonggiuong.TrangthaiChuyen = 0;

                            objPhanbuonggiuong.IdBuong = IDPhong;
                            objPhanbuonggiuong.IdGiuong = IDGiuong;
                            objPhanbuonggiuong.NguoiPhangiuong = globalVariables.UserName;
                            objPhanbuonggiuong.IdNhanvienPhangiuong = globalVariables.gv_intIDNhanvien;
                            objPhanbuonggiuong.NguoiTao = globalVariables.UserName;
                            objPhanbuonggiuong.NgayTao = globalVariables.SysDate;
                            objPhanbuonggiuong.IdGia = IdGia;
                            LayThongTinGia(objPhanbuonggiuong, objPatientExam);
                            objPhanbuonggiuong.IsNew = true;
                            objPhanbuonggiuong.Save();
                            new Update(NoitruDmucGiuongbenh.Schema).Set(NoitruDmucGiuongbenh.Columns.DangSudung).EqualTo(1)
                               .Where(NoitruDmucGiuongbenh.Columns.IdGiuong).IsEqualTo(IDGiuong).Execute();
                            objPatientExam.IdRavien = Utility.Int32Dbnull(objPhanbuonggiuong.Id);
                            new Update(KcbLuotkham.Schema)
                                .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(objPhanbuonggiuong.IdKhoanoitru)
                                .Set(KcbLuotkham.Columns.IdBuong).EqualTo(objPhanbuonggiuong.IdBuong)
                                .Set(KcbLuotkham.Columns.IdGiuong).EqualTo(objPhanbuonggiuong.IdGiuong)
                                .Set(KcbLuotkham.Columns.IdRavien).EqualTo(objPhanbuonggiuong.Id)
                                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                                .Execute();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;

                }
            }
            catch (Exception exception)
            {
                Utility.CatchException(exception);
                return ActionResult.Error;
            }
        }
        public static ActionResult PhanGiuongDieuTriCapcuu(NoitruPhanbuonggiuong objPhanbuonggiuong, KcbLuotkham objPatientExam, DateTime NgayPhanGiuong, short IDPhong, short IDGiuong)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    if (objPhanbuonggiuong != null)
                    {
                        new Update(NoitruDmucGiuongbenh.Schema).Set(NoitruDmucGiuongbenh.Columns.DangSudung).EqualTo(1)
                           .Where(NoitruDmucGiuongbenh.Columns.IdGiuong).IsEqualTo(IDGiuong).Execute();
                        objPhanbuonggiuong.IsNew = false;
                        objPhanbuonggiuong.MarkOld();
                        objPhanbuonggiuong.IsLoaded = true;
                        objPhanbuonggiuong.NgayPhangiuong = NgayPhanGiuong;
                        objPhanbuonggiuong.IdBuong = IDPhong;
                        objPhanbuonggiuong.IdGiuong = IDGiuong;
                        objPhanbuonggiuong.IdNhanvienPhangiuong = globalVariables.gv_intIDNhanvien;
                        objPhanbuonggiuong.NguoiPhangiuong = globalVariables.UserName;
                        objPhanbuonggiuong.NguoiTao = globalVariables.UserName;
                        objPhanbuonggiuong.NgayTao = globalVariables.SysDate;
                        objPhanbuonggiuong.TrangThai = 0;
                        LayThongTinGia(objPhanbuonggiuong, objPatientExam);
                        objPhanbuonggiuong.Save();
                        new Update(KcbLuotkham.Schema)
                            .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(objPhanbuonggiuong.IdKhoanoitru)
                            .Set(KcbLuotkham.Columns.IdGiuong).EqualTo(objPhanbuonggiuong.IdGiuong)
                            .Set(KcbLuotkham.Columns.IdBuong).EqualTo(objPhanbuonggiuong.IdBuong)
                            .Set(KcbLuotkham.Columns.IdRavien).EqualTo(objPhanbuonggiuong.Id)
                            .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                            .Execute();

                        new Update(KcbLichsuDoituongKcb.Schema)
                           .Set(KcbLichsuDoituongKcb.Columns.IdKhoanoitru).EqualTo(objPhanbuonggiuong.IdKhoanoitru)
                           .Set(KcbLichsuDoituongKcb.Columns.IdGiuong).EqualTo(objPhanbuonggiuong.IdGiuong)
                           .Set(KcbLichsuDoituongKcb.Columns.IdBuong).EqualTo(objPhanbuonggiuong.IdBuong)
                           .Set(KcbLichsuDoituongKcb.Columns.IdRavien).EqualTo(objPhanbuonggiuong.Id)
                           .Where(KcbLichsuDoituongKcb.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                           .And(KcbLichsuDoituongKcb.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                           .Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;

                }
            }
            catch (Exception exception)
            {
                Utility.CatchException(exception);
                return ActionResult.Error;
            }
        }
        public ActionResult PhanGiuongDieuTri(NoitruPhanbuonggiuong objPhanbuonggiuong, KcbLuotkham objPatientExam, DateTime NgayPhanGiuong, short IDPhong, short IDGiuong)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        if (objPhanbuonggiuong != null)
                        {
                            new Update(NoitruDmucGiuongbenh.Schema).Set(NoitruDmucGiuongbenh.Columns.DangSudung).EqualTo(1)
                               .Where(NoitruDmucGiuongbenh.Columns.IdGiuong).IsEqualTo(IDGiuong).Execute();
                            objPhanbuonggiuong.IsNew = false;
                            objPhanbuonggiuong.MarkOld();
                            objPhanbuonggiuong.IsLoaded = true;
                            objPhanbuonggiuong.NgayPhangiuong = NgayPhanGiuong;
                            objPhanbuonggiuong.IdBuong = IDPhong;
                            objPhanbuonggiuong.IdGiuong = IDGiuong;
                            objPhanbuonggiuong.IdNhanvienPhangiuong = globalVariables.gv_intIDNhanvien;
                            objPhanbuonggiuong.NguoiPhangiuong = globalVariables.UserName;
                            objPhanbuonggiuong.NguoiTao = globalVariables.UserName;
                            objPhanbuonggiuong.NgayTao = globalVariables.SysDate;
                            objPhanbuonggiuong.TrangThai = 0;
                            LayThongTinGia(objPhanbuonggiuong, objPatientExam);
                            objPhanbuonggiuong.Save();
                            new Update(KcbLuotkham.Schema)
                                .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(objPhanbuonggiuong.IdKhoanoitru)
                                .Set(KcbLuotkham.Columns.IdGiuong).EqualTo(objPhanbuonggiuong.IdGiuong)
                                .Set(KcbLuotkham.Columns.IdBuong).EqualTo(objPhanbuonggiuong.IdBuong)
                                .Set(KcbLuotkham.Columns.IdRavien).EqualTo(objPhanbuonggiuong.Id)
                                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objPhanbuonggiuong.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objPhanbuonggiuong.IdBenhnhan)
                                .Execute();
                        }
                    }
                    scope.Complete();
                    return ActionResult.Success;

                }
            }
            catch (Exception exception)
            {
                Utility.CatchException( exception);
                return ActionResult.Error;
            }
        }
        public static void LayThongTinGia(NoitruPhanbuonggiuong objPhanbuonggiuong, KcbLichsuDoituongKcb objLichsu)
        {
            
            objPhanbuonggiuong.TuTuc = 0;
            NoitruGiabuonggiuong objGia = NoitruGiabuonggiuong.FetchByID(objPhanbuonggiuong.IdGia);
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_APGIABUONGGIUONG_THEODANHMUCGIA", "0", true) == "0")
                objGia = null;
            NoitruDmucGiuongbenh objGiuong = NoitruDmucGiuongbenh.FetchByID(objPhanbuonggiuong.IdGiuong);
            if (objGia != null)
            {
                objPhanbuonggiuong.DonGia = (objLichsu.MaDoituongKcb == "DV" ? Utility.DecimaltoDbnull(objGia.GiaDichvu) : (objLichsu.MaDoituongKcb == "BHYT" ? Utility.DecimaltoDbnull(objGia.GiaBhyt) : Utility.DecimaltoDbnull(objGia.GiaKhac)));
                objPhanbuonggiuong.PhuThu = (objLichsu.MaDoituongKcb == "BHYT" ? (Utility.Byte2Bool(objLichsu.DungTuyen) ? Utility.DecimaltoDbnull(objGia.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objGia.PhuthuTraituyen)) : 0);
                objPhanbuonggiuong.TuTuc = objGiuong.TthaiTunguyen;
                objPhanbuonggiuong.TenHienthi = Utility.sDbnull(objGiuong.TenGiuong);
                objPhanbuonggiuong.GiaGoc = objPhanbuonggiuong.DonGia;
                objPhanbuonggiuong.KieuThue = "GIUONG";
            }
            else if (objGiuong != null)
            {
                objPhanbuonggiuong.DonGia = (objLichsu.MaDoituongKcb == "DV" ? Utility.DecimaltoDbnull(objGiuong.GiaDichvu) : (objLichsu.MaDoituongKcb == "BHYT" ? Utility.DecimaltoDbnull(objGiuong.GiaBhyt) : Utility.DecimaltoDbnull(objGiuong.GiaKhac)));
                objPhanbuonggiuong.PhuThu = (objLichsu.MaDoituongKcb == "BHYT" ? (Utility.Byte2Bool(objLichsu.DungTuyen) ? Utility.DecimaltoDbnull(objGiuong.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objGiuong.PhuthuTraituyen)) : 0);
                objPhanbuonggiuong.TuTuc = objGiuong.TthaiTunguyen;
                objPhanbuonggiuong.TenHienthi = Utility.sDbnull(objGiuong.TenGiuong);
                objPhanbuonggiuong.GiaGoc = objPhanbuonggiuong.DonGia;
                objPhanbuonggiuong.KieuThue = "GIUONG";
            }
            else//Tìm vào các bảng quan hệ
            {
                SqlQuery sqlQuery = new Select().From<NoitruQheDoituongBuonggiuong>()
                    .Where(NoitruQheDoituongBuonggiuong.Columns.IdGiuong).IsEqualTo(objPhanbuonggiuong.IdGiuong)
                    .And(NoitruQheDoituongBuonggiuong.Columns.MaDoituongKcb).IsEqualTo(objLichsu.MaDoituongKcb);
                NoitruQheDoituongBuonggiuong objQhe = sqlQuery.ExecuteSingle<NoitruQheDoituongBuonggiuong>();
                if (objQhe != null)
                {
                    objPhanbuonggiuong.DonGia = Utility.DecimaltoDbnull(objQhe.DonGia);
                    objPhanbuonggiuong.PhuThu = Utility.Byte2Bool(objLichsu.DungTuyen) ? Utility.DecimaltoDbnull(objQhe.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objQhe.PhuthuTraituyen);
                    NoitruDmucGiuongbenh objLBed = NoitruDmucGiuongbenh.FetchByID(objPhanbuonggiuong.IdGiuong);
                    {
                        objPhanbuonggiuong.TuTuc = objLBed.TthaiTunguyen;
                        objPhanbuonggiuong.TenHienthi = Utility.sDbnull(objLBed.TenGiuong);
                        objPhanbuonggiuong.GiaGoc = Utility.DecimaltoDbnull(objLBed.GiaDichvu);
                        objPhanbuonggiuong.KieuThue = "GIUONG";
                    }
                }
                else
                {
                    NoitruDmucGiuongbenh objLBed = NoitruDmucGiuongbenh.FetchByID(objPhanbuonggiuong.IdGiuong);
                    if (objLBed != null)
                    {
                        objPhanbuonggiuong.TenHienthi = Utility.sDbnull(objLBed.TenGiuong);
                        objPhanbuonggiuong.DonGia = Utility.DecimaltoDbnull(objLBed.GiaDichvu);
                        objPhanbuonggiuong.PhuThu = Utility.DecimaltoDbnull(0);
                        objPhanbuonggiuong.TuTuc = objLBed.TthaiTunguyen;
                        objPhanbuonggiuong.KieuThue = "GIUONG";
                        objPhanbuonggiuong.GiaGoc = Utility.DecimaltoDbnull(objLBed.GiaDichvu);
                        if (!THU_VIEN_CHUNG.IsBaoHiem(objLichsu.IdLoaidoituongKcb))
                        {
                            objPhanbuonggiuong.TuTuc = 0;
                        }
                    }
                }
            }
            if (!THU_VIEN_CHUNG.IsBaoHiem(objLichsu.IdLoaidoituongKcb))
            {
                objPhanbuonggiuong.TuTuc = 0;
            }
            objPhanbuonggiuong.TrongGoi = 0;
            if (objPhanbuonggiuong.IdGiuong > 0 || objPhanbuonggiuong.IdBuong > 0)//Có giường thì mới tính
                TinhToanPtramBHYT.TinhPhanTramBHYT(objPhanbuonggiuong, objLichsu, Utility.DecimaltoDbnull(objLichsu.PtramBhytGoc));
        }
        public static void LayThongTinGia(NoitruPhanbuonggiuong objPhanbuonggiuong, KcbLuotkham objPatientExam)
        {
            objPhanbuonggiuong.TuTuc = 0;
                NoitruGiabuonggiuong objGia=NoitruGiabuonggiuong.FetchByID(objPhanbuonggiuong.IdGia);
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_APGIABUONGGIUONG_THEODANHMUCGIA", "0", true) == "0")
                    objGia = null;
            NoitruDmucGiuongbenh objGiuong = NoitruDmucGiuongbenh.FetchByID(objPhanbuonggiuong.IdGiuong);
            if(objGia!=null)
            {
                objPhanbuonggiuong.DonGia = (objPatientExam.MaDoituongKcb == "DV" ? Utility.DecimaltoDbnull(objGia.GiaDichvu) : (objPatientExam.MaDoituongKcb == "BHYT" ? Utility.DecimaltoDbnull(objGia.GiaBhyt) : Utility.DecimaltoDbnull(objGia.GiaKhac)));
                objPhanbuonggiuong.PhuThu = (objPatientExam.MaDoituongKcb == "BHYT" ? (Utility.Byte2Bool(objPatientExam.DungTuyen) ? Utility.DecimaltoDbnull(objGia.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objGia.PhuthuTraituyen)) : 0);
                objPhanbuonggiuong.TuTuc = objGiuong.TthaiTunguyen;
                objPhanbuonggiuong.TenHienthi = Utility.sDbnull(objGiuong.TenGiuong);
                objPhanbuonggiuong.GiaGoc = objPhanbuonggiuong.DonGia;
                objPhanbuonggiuong.KieuThue = "GIUONG";
            }
            else if (objGiuong != null)
            {
                objPhanbuonggiuong.DonGia = (objPatientExam.MaDoituongKcb == "DV" ? Utility.DecimaltoDbnull(objGiuong.GiaDichvu) : (objPatientExam.MaDoituongKcb == "BHYT" ? Utility.DecimaltoDbnull(objGiuong.GiaBhyt) : Utility.DecimaltoDbnull(objGiuong.GiaKhac)));
                objPhanbuonggiuong.PhuThu =(objPatientExam.MaDoituongKcb == "BHYT"? (Utility.Byte2Bool(objPatientExam.DungTuyen) ? Utility.DecimaltoDbnull(objGiuong.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objGiuong.PhuthuTraituyen)):0);
                objPhanbuonggiuong.TuTuc = objGiuong.TthaiTunguyen;
                objPhanbuonggiuong.TenHienthi = Utility.sDbnull(objGiuong.TenGiuong);
                objPhanbuonggiuong.GiaGoc = objPhanbuonggiuong.DonGia;
                objPhanbuonggiuong.KieuThue = "GIUONG";
            }
            else//Tìm vào các bảng quan hệ
            {
                SqlQuery sqlQuery = new Select().From<NoitruQheDoituongBuonggiuong>()
                    .Where(NoitruQheDoituongBuonggiuong.Columns.IdGiuong).IsEqualTo(objPhanbuonggiuong.IdGiuong)
                    .And(NoitruQheDoituongBuonggiuong.Columns.MaDoituongKcb).IsEqualTo(objPatientExam.MaDoituongKcb);
                NoitruQheDoituongBuonggiuong objQhe = sqlQuery.ExecuteSingle<NoitruQheDoituongBuonggiuong>();
                if (objQhe != null)
                {
                    objPhanbuonggiuong.DonGia = Utility.DecimaltoDbnull(objQhe.DonGia);
                    objPhanbuonggiuong.PhuThu = Utility.Byte2Bool(objPatientExam.DungTuyen) ? Utility.DecimaltoDbnull(objQhe.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objQhe.PhuthuTraituyen);
                    NoitruDmucGiuongbenh objLBed = NoitruDmucGiuongbenh.FetchByID(objPhanbuonggiuong.IdGiuong);
                    {
                        objPhanbuonggiuong.TuTuc = objLBed.TthaiTunguyen;
                        objPhanbuonggiuong.TenHienthi = Utility.sDbnull(objLBed.TenGiuong);
                        objPhanbuonggiuong.GiaGoc = Utility.DecimaltoDbnull(objLBed.GiaDichvu);
                        objPhanbuonggiuong.KieuThue = "GIUONG";
                    }
                }
                else
                {
                    NoitruDmucGiuongbenh objLBed = NoitruDmucGiuongbenh.FetchByID(objPhanbuonggiuong.IdGiuong);
                    if (objLBed != null)
                    {
                        objPhanbuonggiuong.TenHienthi = Utility.sDbnull(objLBed.TenGiuong);
                        objPhanbuonggiuong.DonGia = Utility.DecimaltoDbnull(objLBed.GiaDichvu);
                        objPhanbuonggiuong.PhuThu = Utility.DecimaltoDbnull(0);
                        objPhanbuonggiuong.TuTuc = objLBed.TthaiTunguyen;
                        objPhanbuonggiuong.KieuThue = "GIUONG";
                        objPhanbuonggiuong.GiaGoc = Utility.DecimaltoDbnull(objLBed.GiaDichvu);
                        if (!THU_VIEN_CHUNG.IsBaoHiem(objPatientExam.IdLoaidoituongKcb))
                        {
                            objPhanbuonggiuong.TuTuc = 0;
                        }
                    }
                }
            }
            if (!THU_VIEN_CHUNG.IsBaoHiem(objPatientExam.IdLoaidoituongKcb))
            {
                objPhanbuonggiuong.TuTuc = 0;
            }
                objPhanbuonggiuong.TrongGoi = 0;
            if (objPhanbuonggiuong.IdGiuong > 0 || objPhanbuonggiuong.IdBuong > 0)//Có giường thì mới tính
                TinhToanPtramBHYT.TinhPhanTramBHYT(objPhanbuonggiuong,objPatientExam, Utility.DecimaltoDbnull(objPatientExam.PtramBhytGoc));
        }
        public static ActionResult NhapvienCapcuu(NoitruPhanbuonggiuong objBuongGiuong, KcbLuotkham objLuotkham)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                        if (objBuongGiuong != null)
                        {
                            NoitruPhanbuonggiuongCollection _NoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
                                        .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                                        .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                        .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
                            if (_NoitruPhanbuonggiuong != null && _NoitruPhanbuonggiuong.Count == 1)
                            {
                                if (Utility.Int32Dbnull(_NoitruPhanbuonggiuong[0].IdBuong, -1) == -1)
                                {
                                    //Chỉ việc cập nhật lại thông tin khoa
                                    new Update(NoitruPhanbuonggiuong.Schema)
                                    .Set(NoitruPhanbuonggiuong.Columns.IdKhoanoitru).EqualTo(objBuongGiuong.IdKhoanoitru)
                                    .Set(NoitruPhanbuonggiuong.Columns.NguoiSua).EqualTo(objBuongGiuong.NguoiSua)
                                    .Set(NoitruPhanbuonggiuong.Columns.NgaySua).EqualTo(objBuongGiuong.NgaySua)
                                    .Set(NoitruPhanbuonggiuong.Columns.NgayVaokhoa).EqualTo(objBuongGiuong.NgayVaokhoa)
                                    .Set(NoitruPhanbuonggiuong.Columns.IdBacsiChidinh).EqualTo(objBuongGiuong.IdBacsiChidinh)
                                    .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(_NoitruPhanbuonggiuong[0].Id)
                                    .Execute();
                                    objBuongGiuong.Id = _NoitruPhanbuonggiuong[0].Id;

                                }
                            }
                            else
                            {
                                if (objBuongGiuong.NgayVaokhoa <= Convert.ToDateTime("01/01/1900"))
                                    objBuongGiuong.NgayVaokhoa = DateTime.Now;
                                if (objBuongGiuong.NgayTao <= Convert.ToDateTime("01/01/1900"))
                                    objBuongGiuong.NgayTao = DateTime.Now;
                                if (objLuotkham.NgayNhapvien <= Convert.ToDateTime("01/01/1900"))
                                    objLuotkham.NgayNhapvien = DateTime.Now;
                                new Delete().From(NoitruPhanbuonggiuong.Schema)
                                    .Where(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                    .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                    .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                                    .AndExpression(NoitruPhanbuonggiuong.Columns.IdBuong)
                                    .IsEqualTo(-1)
                                    .Or(NoitruPhanbuonggiuong.Columns.IdBuong)
                                    .IsNull().CloseExpression().Execute();

                                objBuongGiuong.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                                objBuongGiuong.IdChuyen = -1;
                                objBuongGiuong.SoLuong = 1;
                                objBuongGiuong.TuTuc = 0;
                                objBuongGiuong.IdGoi = -1;
                                objBuongGiuong.TrongGoi = -1;
                                objBuongGiuong.IdNhanvienPhangiuong = objBuongGiuong.IdBuong > 0 ? globalVariables.gv_intIDNhanvien : -1;
                                objBuongGiuong.TrangthaiXacnhan = 0;
                                objBuongGiuong.TenHienthi = "Nhập viện nội trú";
                                objBuongGiuong.NguoiTao = globalVariables.UserName;
                                objBuongGiuong.NgayTao = globalVariables.SysDate;
                                objBuongGiuong.NoiTru = 1;
                                objBuongGiuong.IsNew = true;
                                objBuongGiuong.Save();
                            }
                            KcbLuotkham _tempt = new Select().From(KcbLuotkham.Schema)
                               .Where(KcbLuotkham.Columns.MaLuotkham).IsNotEqualTo(objLuotkham.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsNotEqualTo(objLuotkham.IdBenhnhan)
                                .And(KcbLuotkham.Columns.SoBenhAn).IsEqualTo(objLuotkham.SoBenhAn)
                                .ExecuteSingle<KcbLuotkham>();
                            //Kiểm tra xem số BA đã dùng cho đối tượng nào chưa
                            if (_tempt != null)
                                objLuotkham.SoBenhAn = THU_VIEN_CHUNG.LaySoBenhAn();

                            new Update(KcbLuotkham.Schema)
                                .Set(KcbLuotkham.Columns.SoBenhAn).EqualTo(objLuotkham.SoBenhAn)
                                .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(objBuongGiuong.IdKhoanoitru)
                                .Set(KcbLuotkham.Columns.IdNhapvien).EqualTo(objBuongGiuong.Id)
                                .Set(KcbLuotkham.Columns.IdRavien).EqualTo(objBuongGiuong.Id)
                                .Set(KcbLuotkham.Columns.TrangthaiNoitru).EqualTo(1)
                                .Set(KcbLuotkham.Columns.NgayNhapvien).EqualTo(objBuongGiuong.NgayVaokhoa)
                                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();

                            new Update(KcbLichsuDoituongKcb.Schema)
                               .Set(KcbLichsuDoituongKcb.Columns.IdKhoanoitru).EqualTo(objBuongGiuong.IdKhoanoitru)
                               .Set(KcbLichsuDoituongKcb.Columns.IdRavien).EqualTo(objBuongGiuong.Id)
                               .Set(KcbLichsuDoituongKcb.Columns.TrangthaiNoitru).EqualTo(1)
                               .Where(KcbLichsuDoituongKcb.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                               .And(KcbLichsuDoituongKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                            
                        }
                        else
                        {
                            return ActionResult.Error;
                        }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return ActionResult.Error;
            }
        }
        public ActionResult Nhapvien(NoitruPhanbuonggiuong objBuongGiuong, KcbLuotkham objLuotkham,
            NoitruGoinhapvien objThongtinGoiDvu)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        if (objBuongGiuong != null)
                        {
                            NoitruPhanbuonggiuongCollection _NoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
                                        .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                                        .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                        .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
                            if (_NoitruPhanbuonggiuong!=null && _NoitruPhanbuonggiuong.Count == 1)
                            {
                                if (Utility.Int32Dbnull(_NoitruPhanbuonggiuong[0].IdBuong, -1) == -1)
                                {
                                    //Chỉ việc cập nhật lại thông tin khoa
                                    new Update(NoitruPhanbuonggiuong.Schema)
                                    .Set(NoitruPhanbuonggiuong.Columns.IdKhoanoitru).EqualTo(objBuongGiuong.IdKhoanoitru)
                                    .Set(NoitruPhanbuonggiuong.Columns.NguoiSua).EqualTo(objBuongGiuong.NguoiSua)
                                    .Set(NoitruPhanbuonggiuong.Columns.NgaySua).EqualTo(objBuongGiuong.NgaySua)
                                    .Set(NoitruPhanbuonggiuong.Columns.NgayVaokhoa).EqualTo(objBuongGiuong.NgayVaokhoa)
                                    .Set(NoitruPhanbuonggiuong.Columns.IdBacsiChidinh).EqualTo(objBuongGiuong.IdBacsiChidinh)
                                    .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(_NoitruPhanbuonggiuong[0].Id)
                                    .Execute();
                                    objBuongGiuong.Id = _NoitruPhanbuonggiuong[0].Id;
                                    
                                }
                            }
                            else
                            {
                                if (objBuongGiuong.NgayVaokhoa <= Convert.ToDateTime("01/01/1900"))
                                    objBuongGiuong.NgayVaokhoa = DateTime.Now;
                                if (objBuongGiuong.NgayTao <= Convert.ToDateTime("01/01/1900"))
                                    objBuongGiuong.NgayTao = DateTime.Now;
                                if (objLuotkham.NgayNhapvien <= Convert.ToDateTime("01/01/1900"))
                                    objLuotkham.NgayNhapvien = DateTime.Now;
                                new Delete().From(NoitruPhanbuonggiuong.Schema)
                                    .Where(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                    .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                    .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                                    .AndExpression(NoitruPhanbuonggiuong.Columns.IdBuong)
                                    .IsEqualTo(-1)
                                    .Or(NoitruPhanbuonggiuong.Columns.IdBuong)
                                    .IsNull().CloseExpression().Execute();

                                objBuongGiuong.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                                objBuongGiuong.IdBuong = -1;
                                objBuongGiuong.IdGiuong = -1;
                                objBuongGiuong.IdChuyen = -1;
                                objBuongGiuong.SoLuong = 1;
                                objBuongGiuong.TuTuc = 0;
                                objBuongGiuong.IdGoi = -1;
                                objBuongGiuong.TrongGoi = -1;
                                objBuongGiuong.IdNhanvienPhangiuong = -1;
                                objBuongGiuong.TrangthaiXacnhan = 0;
                                objBuongGiuong.TenHienthi = "Nhập viện nội trú";
                                objBuongGiuong.NguoiTao = globalVariables.UserName;
                                objBuongGiuong.NgayTao = globalVariables.SysDate;
                                objBuongGiuong.NoiTru = 1;
                                objBuongGiuong.IsNew = true;
                                objBuongGiuong.Save();
                            }
                           KcbLuotkham _tempt=new Select().From(KcbLuotkham.Schema)
                               .Where(KcbLuotkham.Columns.MaLuotkham).IsNotEqualTo(objLuotkham.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsNotEqualTo(objLuotkham.IdBenhnhan)
                                .And(KcbLuotkham.Columns.SoBenhAn).IsEqualTo(objLuotkham.SoBenhAn)
                                .ExecuteSingle<KcbLuotkham>();
                           //Kiểm tra xem số BA đã dùng cho đối tượng nào chưa
                            if (_tempt != null)
                               objLuotkham.SoBenhAn = THU_VIEN_CHUNG.LaySoBenhAn();
                            new Update(KcbLuotkham.Schema)
                                .Set(KcbLuotkham.Columns.SoBenhAn).EqualTo(objLuotkham.SoBenhAn)
                                .Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(objBuongGiuong.IdKhoanoitru)
                                .Set(KcbLuotkham.Columns.IdNhapvien).EqualTo(objBuongGiuong.Id)
                                .Set(KcbLuotkham.Columns.IdRavien).EqualTo(objBuongGiuong.Id)
                                .Set(KcbLuotkham.Columns.TrangthaiNoitru).EqualTo(1)
                                .Set(KcbLuotkham.Columns.NgayNhapvien).EqualTo(objBuongGiuong.NgayVaokhoa)
                                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                            //Phần gói này sẽ thiết kế lại sau
                            if (objThongtinGoiDvu != null)
                            {
                                if (Utility.Int32Dbnull(objThongtinGoiDvu.TrangthaiHuy, -1) <= 0) objThongtinGoiDvu.TrangthaiHuy = 0;
                                if (Utility.Int32Dbnull(objThongtinGoiDvu.TrangthaiDattruoc, -1) <= 0)
                                    objThongtinGoiDvu.TrangthaiDattruoc = 0;
                                if (objThongtinGoiDvu.NgayTao <= Convert.ToDateTime("01/01/1900"))
                                    objThongtinGoiDvu.NgayTao = globalVariables.SysDate;
                                SqlQuery sqlQuery = new Select().From(NoitruGoinhapvien.Schema)
                                    .Where(NoitruGoinhapvien.Columns.MaLuotkham).IsEqualTo(
                                        objThongtinGoiDvu.MaLuotkham)
                                    .And(NoitruGoinhapvien.Columns.IdBenhnhan).IsEqualTo(objThongtinGoiDvu.IdBenhnhan)
                                    .And(NoitruGoinhapvien.Columns.NoiTru).IsEqualTo(objThongtinGoiDvu.NoiTru)
                                    .And(NoitruGoinhapvien.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                                if (sqlQuery.GetRecordCount() > 0)
                                {
                                    return ActionResult.ExistedRecord;
                                }
                                sqlQuery = new Select().From(NoitruGoinhapvien.Schema)
                                    .Where(NoitruGoinhapvien.Columns.MaLuotkham).IsEqualTo(
                                        objThongtinGoiDvu.MaLuotkham)
                                    .And(NoitruGoinhapvien.Columns.IdBenhnhan).IsEqualTo(objThongtinGoiDvu.IdBenhnhan)
                                    .And(NoitruGoinhapvien.Columns.NoiTru).IsEqualTo(objThongtinGoiDvu.NoiTru);
                                if (sqlQuery.GetRecordCount() <= 0)
                                {
                                    //objThongtinGoiDvu.DatTruoc = 0;
                                    objThongtinGoiDvu.IdNhanvien = globalVariables.gv_intIDNhanvien;
                                    objThongtinGoiDvu.IsNew = true;
                                    objThongtinGoiDvu.Save();
                                }
                                else
                                {
                                    new Update(NoitruGoinhapvien.Schema)
                                        .Set(NoitruGoinhapvien.Columns.IdNhanvien)
                                        .EqualTo(globalVariables.gv_intIDNhanvien)
                                        .Set(NoitruGoinhapvien.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                        .Set(NoitruGoinhapvien.Columns.NgaySua).EqualTo(DateTime.Now)
                                        .Set(NoitruGoinhapvien.Columns.IdGoi).EqualTo(objThongtinGoiDvu.IdGoi)
                                        .Set(NoitruGoinhapvien.Columns.SoTien).EqualTo(objThongtinGoiDvu.SoTien)
                                        .Set(NoitruGoinhapvien.Columns.SoNgay).EqualTo(objThongtinGoiDvu.SoNgay)
                                        .Where(NoitruGoinhapvien.Columns.MaLuotkham).IsEqualTo(
                                            objThongtinGoiDvu.MaLuotkham)
                                        .And(NoitruGoinhapvien.Columns.IdBenhnhan).IsEqualTo(
                                            objThongtinGoiDvu.IdBenhnhan)
                                        .And(NoitruGoinhapvien.Columns.NoiTru).IsEqualTo(objThongtinGoiDvu.NoiTru).
                                        Execute();
                                }
                            }
                        }
                        else
                        {
                            return ActionResult.Error;
                        }
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
               Utility.CatchException( ex);
                return ActionResult.Error;
            }
        }
        public ActionResult Huynhapvien(KcbLuotkham objLuotkham)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        if (objLuotkham != null)
                        {
                            StoredProcedure sp = SPs.NoitruHuynhapvien(objLuotkham.MaLuotkham,(int) objLuotkham.IdBenhnhan);
                            sp.Execute();
                            
                        }
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return ActionResult.Error;
            }
        }
        
    }
}
