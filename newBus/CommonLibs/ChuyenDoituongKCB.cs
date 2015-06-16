using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;
using System.Transactions;
using VNS.Properties;
using System.Data;
using VNS.HIS.BusRule.Classes;

namespace VNS.Libs
{
    public class ChuyenDoituongKCB
    {
        public static ActionResult Chuyendoituong(KcbLuotkham objLuotkham,decimal PtramBHYTcu)
        {
            try
            {
            ActionResult _ActionResult = ActionResult.Success;
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        //if (PtramBHYTcu != objLuotkham.PtramBhyt.Value )
                           _ActionResult= CapnhatGiatheodoituong(objLuotkham);
                        if (_ActionResult == ActionResult.Cancel)
                        {
                            return _ActionResult;
                        }
                        new Update(KcbLuotkham.Schema)
                           .Set(KcbLuotkham.Columns.LuongCoban).EqualTo(objLuotkham.LuongCoban)
                           .Set(KcbLuotkham.Columns.TthaiChuyendi).EqualTo(objLuotkham.TthaiChuyendi)
                           .Set(KcbLuotkham.Columns.MatheBhyt).EqualTo(objLuotkham.MatheBhyt)
                           .Set(KcbLuotkham.Columns.MaNoicapBhyt).EqualTo(objLuotkham.MaNoicapBhyt)
                           .Set(KcbLuotkham.Columns.MaQuyenloi).EqualTo(objLuotkham.MaQuyenloi)
                           .Set(KcbLuotkham.Columns.NgaybatdauBhyt).EqualTo(objLuotkham.NgaybatdauBhyt)
                           .Set(KcbLuotkham.Columns.NgayketthucBhyt).EqualTo(objLuotkham.NgayketthucBhyt)
                           .Set(KcbLuotkham.Columns.NoicapBhyt).EqualTo(objLuotkham.NoicapBhyt)
                           .Set(KcbLuotkham.Columns.IdDoituongKcb).EqualTo(objLuotkham.IdDoituongKcb)
                           .Set(KcbLuotkham.Columns.MaKcbbd).EqualTo(objLuotkham.MaKcbbd)
                           .Set(KcbLuotkham.Columns.NoiDongtrusoKcbbd).EqualTo(objLuotkham.NoiDongtrusoKcbbd)
                           .Set(KcbLuotkham.Columns.MaDoituongBhyt).EqualTo(objLuotkham.MaDoituongBhyt)
                           .Set(KcbLuotkham.Columns.DungTuyen).EqualTo(objLuotkham.DungTuyen)
                           .Set(KcbLuotkham.Columns.MaDoituongKcb).EqualTo(objLuotkham.MaDoituongKcb)
                           .Set(KcbLuotkham.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                           .Set(KcbLuotkham.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                           .Set(KcbLuotkham.Columns.MadtuongSinhsong).EqualTo(objLuotkham.MadtuongSinhsong)
                           .Set(KcbLuotkham.Columns.GiayBhyt).EqualTo(objLuotkham.GiayBhyt)
                           .Set(KcbLuotkham.Columns.PtramBhyt).EqualTo(objLuotkham.PtramBhyt)
                           .Set(KcbLuotkham.Columns.PtramBhytGoc).EqualTo(objLuotkham.PtramBhytGoc)
                           .Set(KcbLuotkham.Columns.DiachiBhyt).EqualTo(objLuotkham.DiachiBhyt)
                           .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                           .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
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
        public static ActionResult CapnhatGiatheodoituong(KcbLuotkham objLuotkham)
        {
            using (var Scope = new TransactionScope())
            {
                ActionResult _ActionResult = ActionResult.Success;

                _ActionResult = CapnhatChiphiKCB(objLuotkham);
                if (_ActionResult == ActionResult.Cancel)
                    return _ActionResult;
                _ActionResult = CapnhatChiphiCLS(objLuotkham);
                if (_ActionResult == ActionResult.Cancel)
                    return _ActionResult;
                _ActionResult = CapnhatChiphiThuoc(objLuotkham);
                if (_ActionResult == ActionResult.Cancel)
                    return _ActionResult;
                Scope.Complete();
                return ActionResult.Success;
            }

        }
        private static ActionResult CapnhatChiphiThuoc(KcbLuotkham objLuotkham)
        {
            using (var Scope = new TransactionScope())
            {
                decimal BHYT_PTRAM_TRAITUYENNOITRU =Utility.DecimaltoDbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false),0m);
                THUOC_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_GIATHEO_KHOAKCB", "0", true) == "1";
                SqlQuery sqlQuery;
                sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                    .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).In(
                        new Select(KcbDonthuoc.Columns.IdDonthuoc).From(KcbDonthuoc.Schema).Where(
                            KcbDonthuoc.Columns.MaLuotkham).
                            IsEqualTo(objLuotkham.MaLuotkham).And(KcbDonthuoc.Columns.IdBenhnhan).
                            IsEqualTo(objLuotkham.IdBenhnhan));
                var objChitietDonthuocCollection =
                    sqlQuery.ExecuteAsCollection<KcbDonthuocChitietCollection>();

                bool ApdunggiathuocDoituong = THU_VIEN_CHUNG.Laygiatrithamsohethong("APDUNG_GIATHUOC_DOITUONG", "0", true) == "1";

                DmucDoituongkcb _DmucDoituongkcb = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();
                if (_DmucDoituongkcb == null) return ActionResult.Success;
                decimal PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                if (Utility.ByteDbnull(objLuotkham.TrangthaiNoitru, 0) > 0)
                    PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc);

                foreach (KcbDonthuocChitiet objChitietDonthuoc in objChitietDonthuocCollection)
                {
                    if (Utility.Int16Dbnull(objChitietDonthuoc.TrangthaiThanhtoan, 0) == 0)
                    {
                        DmucThuoc _DmucThuoc = DmucThuoc.FetchByID(objChitietDonthuoc.IdThuoc);
                        if (ApdunggiathuocDoituong || Utility.Byte2Bool( _DmucDoituongkcb.GiathuocQuanhe.Value))// globalVariables.gv_GiathuoctheoGiatrongKho)
                        {
                            //Giá theo quan he-->
                            QheDoituongThuoc _item = THU_VIEN_CHUNG.LayQheDoituongThuoc(objLuotkham.MaDoituongKcb,
                                                                      objChitietDonthuoc.IdThuoc,
                                                                      objLuotkham.MaKhoaThuchien, CLS_GIATHEO_KHOAKCB);
                            if (_item != null)
                            {
                                objChitietDonthuoc.DonGia = Utility.DecimaltoDbnull(_item.DonGia);
                                objChitietDonthuoc.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen.Value) ? Utility.DecimaltoDbnull(_item.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_item.PhuthuTraituyen));
                                objChitietDonthuoc.TuTuc = 0;
                                objChitietDonthuoc.PtramBhyt = Utility.DecimaltoDbnull(PtramBHYT);
                            }
                            else
                            {
                                _item = THU_VIEN_CHUNG.LayQheDoituongThuoc("DV",
                                                                       objChitietDonthuoc.IdThuoc,
                                                                       objLuotkham.MaKhoaThuchien, THUOC_GIATHEO_KHOAKCB);
                                if (_item != null)
                                {
                                    objChitietDonthuoc.DonGia = Utility.DecimaltoDbnull(_item.DonGia);
                                    objChitietDonthuoc.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen.Value) ? Utility.DecimaltoDbnull(_item.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_item.PhuthuTraituyen));
                                    objChitietDonthuoc.TuTuc = 1;
                                    objChitietDonthuoc.PtramBhyt = 0;
                                }
                            }
                            
                        }
                        else //Giá theo kho-->
                        {
                           
                            TThuockho objTK = TThuockho.FetchByID(objChitietDonthuoc.IdThuockho);
                            if (objTK != null)
                            {
                                objChitietDonthuoc.DonGia = objLuotkham.IdLoaidoituongKcb == 0 ? Utility.DecimaltoDbnull(objTK.GiaBhyt.Value, objChitietDonthuoc.DonGia) : objTK.GiaBan;
                            }
                            if (objLuotkham.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                                objChitietDonthuoc.TuTuc = 0;
                            if (Utility.Int32Dbnull(objChitietDonthuoc.TuTuc, 0) == 1)
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
                        }
                        //Tính lại các mục tự túc, BHYT chi trả, BN chi trả
                        if (_DmucThuoc!=null && Utility.Int32Dbnull(_DmucThuoc.TuTuc, 0) == 1)
                            objChitietDonthuoc.TuTuc = 1;//Ke ca co trong bang quan he

                        if (objLuotkham.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                            objChitietDonthuoc.TuTuc = 0;
                        if (Utility.Int32Dbnull(objChitietDonthuoc.TuTuc, 0) == 0)
                        {
                            decimal BHCT = 0m;
                            if (objLuotkham.DungTuyen == 1)
                            {
                                BHCT = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                            }
                            else
                            {
                                if (objLuotkham.TrangthaiNoitru <= 0)
                                    BHCT = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                                else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                    BHCT = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                            }
                            // decimal num2 = (Utility.DecimaltoDbnull(row[KcbDonthuocChitiet.Columns.DonGia], 0) * Utility.DecimaltoDbnull(this.objLuotkham.PtramBhyt, 0)) / 100M;
                            decimal num3 = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0) - BHCT;
                            objChitietDonthuoc.BhytChitra = BHCT;
                            objChitietDonthuoc.BnhanChitra = num3;
                        }
                        else
                        {
                            objChitietDonthuoc.PtramBhyt = 0;
                            objChitietDonthuoc.BhytChitra = 0;
                            objChitietDonthuoc.BnhanChitra = Utility.DecimaltoDbnull(objChitietDonthuoc.DonGia, 0);
                        }
                    }
                    else
                    {
                        return ActionResult.Cancel;
                    }
                }
                objChitietDonthuocCollection.SaveAll();
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        static bool CLS_GIATHEO_KHOAKCB = false;
        static bool THUOC_GIATHEO_KHOAKCB = false;
        private static ActionResult CapnhatChiphiCLS_old(KcbLuotkham objLuotkham)
        {
            using (var Scope = new TransactionScope())
            {
                CLS_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_GIATHEO_KHOAKCB","0", true) == "1";
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(
                        new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema)
                        .Where(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        );
                var objAssignDetailCollection =
                    sqlQuery.ExecuteAsCollection<KcbChidinhclsChitietCollection>();
                foreach (KcbChidinhclsChitiet objAssignDetail in objAssignDetailCollection)
                {
                    if (objAssignDetail.TrangthaiThanhtoan == 0)
                    {
                        QheDoituongDichvucl _Items = THU_VIEN_CHUNG.LayQheDoituongCLS(objLuotkham.MaDoituongKcb, objAssignDetail.IdChitietdichvu, objLuotkham.MaKhoaThuchien, CLS_GIATHEO_KHOAKCB);
                        if (_Items != null)
                        {

                            objAssignDetail.DonGia = Utility.DecimaltoDbnull(_Items.DonGia);
                            objAssignDetail.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(_Items.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_Items.PhuthuTraituyen));
                            objAssignDetail.TuTuc = 0;
                            objAssignDetail.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                            objAssignDetail.PtramBhyt = objLuotkham.PtramBhyt;
                        }
                        else
                        {
                            _Items = THU_VIEN_CHUNG.LayQheDoituongCLS("DV", objAssignDetail.IdChitietdichvu, objLuotkham.MaKhoaThuchien, CLS_GIATHEO_KHOAKCB);
                            if (_Items != null)
                            {
                                objAssignDetail.DonGia = Utility.DecimaltoDbnull(_Items.DonGia);
                                objAssignDetail.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(_Items.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_Items.PhuthuTraituyen));
                                objAssignDetail.TuTuc = 1;
                                objAssignDetail.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                objAssignDetail.PtramBhyt = 0;
                            }
                        }
                        DmucDichvuclsChitiet _DmucDichvuclsChitiet = DmucDichvuclsChitiet.FetchByID(objAssignDetail.IdChitietdichvu);
                        if (_DmucDichvuclsChitiet != null && Utility.Int32Dbnull(_DmucDichvuclsChitiet.TuTuc, 0) == 1)
                            objAssignDetail.TuTuc = 1;//Ke ca co trong bang quan he
                        if (objLuotkham.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                            objAssignDetail.TuTuc = 0;

                        objAssignDetail.NguoiSua = globalVariables.UserName;
                        objAssignDetail.NgaySua = DateTime.Now;
                        decimal PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                        TinhCLS.GB_TinhPhtramBHYT(objAssignDetail, objLuotkham, PtramBHYT);
                    }
                    else
                    {
                        return ActionResult.Cancel;
                    }
                }
                objAssignDetailCollection.SaveAll();
                Scope.Complete();
                return ActionResult.Success;
            }
        }
        /// <summary>
        /// Lấy toàn bộ dữ liệu CLS giống phần chỉ định CLS sau đó tính giá dựa trên dữ liệu lấy được đó. Thay vì select lại từ các bảng quan hệ
        /// </summary>
        /// <param name="objLuotkham"></param>
        /// <returns></returns>
        private static ActionResult CapnhatChiphiCLS(KcbLuotkham objLuotkham)
        {
            using (var Scope = new TransactionScope())
            {
                DataTable m_dtServiceDetail = new KCB_CHIDINH_CANLAMSANG().LaydanhsachCLS_chidinh(objLuotkham.MaDoituongKcb, objLuotkham.TrangthaiNoitru, Utility.ByteDbnull(objLuotkham.GiayBhyt, 0), -1, Utility.Int32Dbnull(objLuotkham.DungTuyen.Value, 0), objLuotkham.MaKhoaThuchien, "-GOI,-TIEN");//Ko lấy dữ liệu liên quan đến gói dịch vụ+tiền phí phụ thêm
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChidinh).In(
                        new Select(KcbChidinhcl.Columns.IdChidinh).From(KcbChidinhcl.Schema)
                        .Where(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        );
                var objAssignDetailCollection =
                    sqlQuery.ExecuteAsCollection<KcbChidinhclsChitietCollection>();
                foreach (KcbChidinhclsChitiet objAssignDetail in objAssignDetailCollection)
                {
                   
                    if (objAssignDetail.TrangthaiThanhtoan == 0)//Chưa thanh toán
                    {
                         DataRow[] arrDr = m_dtServiceDetail.Select(KcbChidinhclsChitiet.Columns.IdChitietdichvu + "=" + Utility.Int32Dbnull(objAssignDetail.IdChitietdichvu, -1));
                         if (arrDr.Length > 0)
                         {
                             objAssignDetail.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                             objAssignDetail.PtramBhyt = objLuotkham.PtramBhyt;
                             objAssignDetail.DonGia =Utility.DecimaltoDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.DonGia], 0);
                             objAssignDetail.GiaDanhmuc = Utility.DecimaltoDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.DonGia], 0);
                             objAssignDetail.TuTuc = Utility.ByteDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.TuTuc],0);
                             objAssignDetail.PhuThu = Utility.DecimaltoDbnull(arrDr[0][KcbChidinhclsChitiet.Columns.PhuThu], 0);
                             objAssignDetail.NguoiSua = globalVariables.UserName;
                             objAssignDetail.NgaySua = DateTime.Now;
                             TinhCLS.GB_TinhPhtramBHYT(objAssignDetail, objLuotkham, Utility.DecimaltoDbnull(objLuotkham.PtramBhyt));

                         }
                         else//Rất khó nhảy vào nhánh này trừ phi lỗi dữ liệu đặc biệt nào đó
                         {
                             CLS_GIATHEO_KHOAKCB = THU_VIEN_CHUNG.Laygiatrithamsohethong("CLS_GIATHEO_KHOAKCB", "0", true) == "1";
                             QheDoituongDichvucl _Items = THU_VIEN_CHUNG.LayQheDoituongCLS(objLuotkham.MaDoituongKcb, objAssignDetail.IdChitietdichvu, objLuotkham.MaKhoaThuchien, CLS_GIATHEO_KHOAKCB);
                             if (_Items != null)
                             {

                                 objAssignDetail.DonGia = Utility.DecimaltoDbnull(_Items.DonGia);
                                 objAssignDetail.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(_Items.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_Items.PhuthuTraituyen));
                                 objAssignDetail.TuTuc = 0;
                                 objAssignDetail.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                 objAssignDetail.PtramBhyt = objLuotkham.PtramBhyt;
                             }
                             else
                             {
                                 _Items = THU_VIEN_CHUNG.LayQheDoituongCLS("DV", objAssignDetail.IdChitietdichvu, objLuotkham.MaKhoaThuchien, CLS_GIATHEO_KHOAKCB);
                                 if (_Items != null)
                                 {
                                     objAssignDetail.DonGia = Utility.DecimaltoDbnull(_Items.DonGia);
                                     objAssignDetail.PhuThu = (Utility.isTrue(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(_Items.PhuthuDungtuyen) : Utility.DecimaltoDbnull(_Items.PhuthuTraituyen));
                                     objAssignDetail.TuTuc = 1;
                                     objAssignDetail.IdDoituongKcb = objLuotkham.IdDoituongKcb;
                                     objAssignDetail.PtramBhyt = 0;
                                 }
                             }
                             DmucDichvuclsChitiet _DmucDichvuclsChitiet = DmucDichvuclsChitiet.FetchByID(objAssignDetail.IdChitietdichvu);
                             if (_DmucDichvuclsChitiet != null && Utility.Int32Dbnull(_DmucDichvuclsChitiet.TuTuc, 0) == 1)
                                 objAssignDetail.TuTuc = 1;//Ke ca co trong bang quan he
                             if (objLuotkham.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                                 objAssignDetail.TuTuc = 0;

                             objAssignDetail.NguoiSua = globalVariables.UserName;
                             objAssignDetail.NgaySua = DateTime.Now;
                             decimal PtramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                             TinhCLS.GB_TinhPhtramBHYT(objAssignDetail, objLuotkham, PtramBHYT);
                         }
                       
                    }
                    else//Đã thanh toán-->Ko đổi
                    {
                        return ActionResult.Cancel;
                    }
                }
                objAssignDetailCollection.SaveAll();
                Scope.Complete();
                return ActionResult.Success;
            }
        }

        private static ActionResult CapnhatChiphiKCB(KcbLuotkham objLuotkham)
        {
            using (var Scope = new TransactionScope())
            {
                KcbDangkyKcbCollection objRegExamCollection =
                    new Select().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(
                        objLuotkham.MaLuotkham).And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbDangkyKcb.Columns.LaPhidichvukemtheo).IsEqualTo(0)
                        .ExecuteAsCollection<KcbDangkyKcbCollection>();
                if (objRegExamCollection.Count > 0)
                {
                    //CHUYỂN GIÁ KHÁM BỆNH VÀO PHÒNG
                    foreach (KcbDangkyKcb objRegExam in objRegExamCollection)
                    {
                        if (objRegExam.TrangthaiThanhtoan == 0)
                        {
                           
                            DmucDichvukcb _DichvukcbCu =
                                DmucDichvukcb.FetchByID(objRegExam.IdDichvuKcb);
                            var _DichvukcbMoi =
                                new Select().From(DmucDichvukcb.Schema)
                                    .Where(DmucDichvukcb.Columns.IdKhoaphong).IsEqualTo(_DichvukcbCu.IdKhoaphong)
                                    .And(DmucDichvukcb.Columns.IdPhongkham).IsEqualTo(_DichvukcbCu.IdPhongkham)
                                         .And(DmucDichvukcb.Columns.IdKieukham).IsEqualTo(_DichvukcbCu.IdKieukham)
                                    .AndExpression(DmucDichvukcb.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb)
                                    .Or(DmucDichvukcb.Columns.MaDoituongKcb).IsEqualTo("ALL").CloseExpression()
                                    .ExecuteSingle<DmucDichvukcb>();
                            if (_DichvukcbMoi != null)
                            {
                                objRegExam.IdDichvuKcb = Utility.Int16Dbnull(_DichvukcbMoi.IdDichvukcb, -1);
                                if (Utility.Int32Dbnull(_DichvukcbMoi.TuTuc, 0) == 1)
                                    objRegExam.TuTuc = 1;//Ke ca co trong bang quan he
                                if (objLuotkham.IdLoaidoituongKcb == 1)//Đối tượng dịch vụ-->ko cần phải đánh dấu tự túc
                                    objRegExam.TuTuc = 0;
                                objRegExam.TenDichvuKcb = _DichvukcbMoi.TenDichvukcb;
                                objRegExam.DonGia = Utility.DecimaltoDbnull(_DichvukcbMoi.DonGia);
                                objRegExam.PhuThu = !Utility.Byte2Bool(objLuotkham.DungTuyen)
                                                   ? Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuDungtuyen)
                                                   : Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuTraituyen);
                                objRegExam.PtramBhyt = objLuotkham.PtramBhyt;//% BHYT ngoại trú
                                if (Utility.Byte2Bool( objRegExam.KhamNgoaigio))
                                {
                                    objRegExam.KhamNgoaigio = 1;
                                    objRegExam.DonGia = Utility.DecimaltoDbnull(_DichvukcbMoi.DongiaNgoaigio, 0);
                                    objRegExam.PhuThu = !Utility.Byte2Bool(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuNgoaigio, 0) : Utility.DecimaltoDbnull(_DichvukcbMoi.PhuthuDungtuyen);
                                }

                                if (Utility.Int32Dbnull(objRegExam.TuTuc, 0) == 0)
                                {
                                    objRegExam.BhytChitra = Utility.DecimaltoDbnull(objRegExam.DonGia) *
                                                           Utility.DecimaltoDbnull(objLuotkham.PtramBhyt) / 100;
                                    objRegExam.BnhanChitra = Utility.DecimaltoDbnull(objRegExam.DonGia, 0) -
                                                              Utility.DecimaltoDbnull(objRegExam.BhytChitra, 0);
                                }
                                else
                                {
                                    objRegExam.BhytChitra = 0;
                                    objRegExam.BnhanChitra = Utility.DecimaltoDbnull(objRegExam.DonGia, 0);
                                }
                            }
                        }
                        else
                        {
                            return ActionResult.Cancel;
                        }
                    }
                    objRegExamCollection.SaveAll();
                }
                Scope.Complete();
                return ActionResult.Success;
            }
        }
       

      
       
    }
}
