using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VNS.HIS.DAL;
using VNS.Libs;
using SubSonic;
using NLog;
using VNS.Properties;
using System.Data;
namespace VNS.HIS.NGHIEPVU.THUOC
{

    public class XuatThuoc
    {
        private NLog.Logger log;
        public XuatThuoc()
        {
            log = NLog.LogManager.GetCurrentClassLogger();
        }
        /// <summary>
        /// HÀM THỰC HIỆN VIECJ CHO PHÉP CẬP NHẬP ĐƠN THUỐC
        /// </summary>
        /// <returns></returns>
        public ActionResult LinhThuocBenhNhan(KcbDonthuoc objDonthuoc,KcbDonthuocChitiet []arrPresDetails, TPhieuXuatthuocBenhnhan objXuatBnhan)
        {
            try
            {
                HisDuocProperties hisDuocProperties=new HisDuocProperties();
                using (var scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        hisDuocProperties = PropertyLib._HisDuocProperties;
                        objXuatBnhan.IdBenhnhan = objDonthuoc.IdBenhnhan;
                        objXuatBnhan.MaLuotkham = objDonthuoc.MaLuotkham;
                        objXuatBnhan.MaPhieu = THU_VIEN_CHUNG.MaPhieuXuatBN();
                        objXuatBnhan.Noitru = Utility.ByteDbnull(objDonthuoc.Noitru);
                        objXuatBnhan.TenKhongdau = Utility.UnSignedCharacter(objXuatBnhan.TenBenhnhan);
                        objXuatBnhan.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);
                        objXuatBnhan.IsNew = true;
                        objXuatBnhan.Save();
                        Int32 PtramBHYT = 0;
                        SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema).Where(
                            KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objDonthuoc.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objDonthuoc.IdBenhnhan);
                        KcbLuotkham objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                        if(objLuotkham!=null)
                        {
                            PtramBHYT = Utility.Int32Dbnull(objLuotkham.PtramBhyt);
                        }
                        foreach (KcbDonthuocChitiet objDetail in arrPresDetails)
                        {
                            ActionResult actionResult = TruThuocTrongKho(objDonthuoc,objDetail, objXuatBnhan);
                            switch (actionResult)
                            {
                                case ActionResult.NotEnoughDrugInStock:
                                    return actionResult;
                                    break;
                            }
                            //REM lại để tránh trường hợp vi phạm phần nội trú. Đơn thuốc được cấp phát nhiều lần
                            //new Delete().From(TXuatthuocTheodon.Schema)
                            //    .Where(TXuatthuocTheodon.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                            TXuatthuocTheodon objThuocCt=new TXuatthuocTheodon();
                            objThuocCt.IdPhieuXuat = Utility.Int32Dbnull(objXuatBnhan.IdPhieu);
                            objThuocCt.IdThuoc = Utility.Int32Dbnull(objDetail.IdThuoc);
                            objThuocCt.NgayTao = globalVariables.SysDate;
                            objThuocCt.SoLuong = Utility.Int32Dbnull(objDetail.SoLuong);
                            objThuocCt.NguoiTao = globalVariables.UserName;
                            objThuocCt.PhuThu = Utility.DecimaltoDbnull(objDetail.PhuThu);
                            objThuocCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);
                          
                          //  objThuocCt.gi = Utility.DecimaltoDbnull(objDetail.DonGia);
                            objThuocCt.BnhanChitra = Utility.DecimaltoDbnull(objDetail.BnhanChitra);
                            objThuocCt.BhytChitra = Utility.DecimaltoDbnull(objDetail.BhytChitra);
                            objThuocCt.ChiDan = Utility.sDbnull(objDetail.MotaThem);
                            objThuocCt.ChidanThem = Utility.sDbnull(objDetail.ChidanThem);
                            objThuocCt.SolanDung = Utility.sDbnull(objDetail.SolanDung);
                            objThuocCt.SoluongDung = Utility.sDbnull(objDetail.SoluongDung);
                            objThuocCt.CachDung = Utility.sDbnull(objDetail.CachDung);
                            objThuocCt.PtramBhyt = PtramBHYT;
                            objThuocCt.IdChitietdonthuoc = Utility.Int32Dbnull(objDetail.IdChitietdonthuoc);
                            objThuocCt.IdDonthuoc = Utility.Int32Dbnull(objDetail.IdDonthuoc);
                           // objThuocCt.LoaiDonThuoc = 0;
                            objThuocCt.IsNew = true;
                            objThuocCt.Save();
                        }
                        sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                            .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery.GetRecordCount()<=0?1:0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh cap don thuoc {0}", exception);
                return ActionResult.Error;
            }
        }
        public ActionResult LinhThuocBenhNhan(long Pres_ID, Int16 id_kho, DateTime ngaythuchien)
        {
            try
            {
                string ErrMsg = "";
                HisDuocProperties hisDuocProperties = new HisDuocProperties();
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SharedDbConnectionScope dbScope = new SharedDbConnectionScope())
                    {

                        hisDuocProperties = PropertyLib._HisDuocProperties;
                        KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(Pres_ID);
                        KcbDanhsachBenhnhan objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objDonthuoc.IdBenhnhan);
                        KcbLuotkham objLuotkham = new Select().From(KcbLuotkham.Schema)
                            .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objDonthuoc.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objDonthuoc.IdBenhnhan).ExecuteSingle<KcbLuotkham>();
                        TPhieuXuatthuocBenhnhan objXuatBnhan = CreatePhieuXuatBenhNhan(objDonthuoc, objBenhnhan, objLuotkham);
                        objXuatBnhan.NgayXacnhan = ngaythuchien;
                        objXuatBnhan.MaPhieu = THU_VIEN_CHUNG.MaPhieuXuatBN();
                        objXuatBnhan.IdKho = id_kho;
                        objXuatBnhan.IsNew = true;
                        objXuatBnhan.Save();
                        Int32 PtramBHYT = 0;
                        if (objLuotkham != null)
                        {
                            PtramBHYT = Utility.Int32Dbnull(objLuotkham.PtramBhyt);
                        }
                        KcbDonthuocChitietCollection lstDetail
                            = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.IdDonthuocColumn).IsEqualTo(objDonthuoc.IdDonthuoc)
                            .And(KcbDonthuocChitiet.Columns.IdKho).IsEqualTo(id_kho)
                            .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        //Chỉ việc trừ theo chi tiết do ngay khi kê đơn đã tự động xác định các thuốc cần trừ trong kho theo id_thuockho
                        foreach (KcbDonthuocChitiet objDetail in lstDetail)
                        {
                            TThuockho objTThuockho = new Select().From(TThuockho.Schema)
                                .Where(TThuockho.IdThuockhoColumn).IsEqualTo(objDetail.IdThuockho)
                                .ExecuteSingle<TThuockho>();
                            //Kiểm tra xem thuốc còn đủ hay không?
                            if (objTThuockho.SoLuong <= objDetail.SoLuong)
                            {
                                //Sau này có thể mở rộng thêm code tự động dò và xác định lại Id_thuockho cho các chi tiết đơn thuốc
                                return ActionResult.NotEnoughDrugInStock;
                            }
                            UpdateXuatChiTietBN(objDonthuoc,objDetail, objTThuockho, objDetail.SoLuong, objXuatBnhan);
                            StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                                                                          Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                                                                          objTThuockho.NgayHethan, objDetail.GiaNhap, Utility.DecimaltoDbnull(objDetail.GiaBan),
                                                                          Utility.DecimaltoDbnull(objTThuockho.Vat), objDetail.SoLuong, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, PropertyLib._HisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, ErrMsg);

                            sp.Execute();

                            new Update(KcbDonthuocChitiet.Schema)
                   .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                   .Set(KcbDonthuocChitiet.Columns.NgayXacnhan).EqualTo(ngaythuchien)
                   .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();

                            //REM lại để tránh trường hợp vi phạm phần nội trú. Đơn thuốc được cấp phát nhiều lần
                            TXuatthuocTheodon objThuocCt = new TXuatthuocTheodon();
                            objThuocCt.IdPhieuXuat = Utility.Int32Dbnull(objXuatBnhan.IdPhieu);
                            objThuocCt.IdThuoc = Utility.Int32Dbnull(objDetail.IdThuoc);
                            objThuocCt.NgayTao = globalVariables.SysDate;
                            objThuocCt.SoLuong = Utility.Int32Dbnull(objDetail.SoLuong);
                            objThuocCt.NguoiTao = globalVariables.UserName;
                            objThuocCt.PhuThu = Utility.DecimaltoDbnull(objDetail.PhuThu);
                            objThuocCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);
                            
                            objThuocCt.BnhanChitra = Utility.DecimaltoDbnull(objDetail.BnhanChitra);
                            objThuocCt.BhytChitra = Utility.DecimaltoDbnull(objDetail.BhytChitra);
                            objThuocCt.ChiDan = Utility.sDbnull(objDetail.MotaThem);
                            objThuocCt.ChidanThem = Utility.sDbnull(objDetail.ChidanThem);
                            objThuocCt.SolanDung = Utility.sDbnull(objDetail.SolanDung);
                            objThuocCt.SoluongDung = Utility.sDbnull(objDetail.SoluongDung);
                            objThuocCt.CachDung = Utility.sDbnull(objDetail.CachDung);
                            objThuocCt.PtramBhyt = PtramBHYT;
                            objThuocCt.IdChitietdonthuoc = Utility.Int32Dbnull(objDetail.IdChitietdonthuoc);
                            objThuocCt.IdDonthuoc = Utility.Int32Dbnull(objDetail.IdDonthuoc);
                            objThuocCt.IsNew = true;
                            objThuocCt.Save();
                        }
                        SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                             .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                             .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery.GetRecordCount() <= 0 ? 1 : 0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh cap don thuoc {0}", exception);
                return ActionResult.Error;
            }
        }


        public static bool InValiKiemTraDonThuoc(KcbDonthuocChitietCollection lstChitiet,byte noitru)
        {
            try
            {
                foreach (KcbDonthuocChitiet item in lstChitiet)
                {

                    int SoLuongTon = CommonLoadDuoc.SoLuongTonTrongKho((long)item.IdDonthuoc, Utility.Int32Dbnull(item.IdKho, -1), item.IdThuoc, Utility.Int64Dbnull(item.IdThuockho, -1),0, noitru);//Ko cần kiểm tra chờ xác nhận
                    if (SoLuongTon < item.SoLuong)
                    {
                        Utility.ShowMsg(string.Format("Bạn không thể xác nhận đơn thuốc, Vì thuốc :{0} số lượng tồn hiện tại trong kho không đủ\n Mời bạn xem lại số lượng", item.IdThuoc));
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public TPhieuXuatthuocBenhnhan CreatePhieuXuatBenhNhan(KcbDonthuoc objDonthuoc)
        {
            KcbDanhsachBenhnhan objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objDonthuoc.IdBenhnhan);
            KcbLuotkham objLuotkham = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objDonthuoc.MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objDonthuoc.IdBenhnhan).ExecuteSingle<KcbLuotkham>();

            TPhieuXuatthuocBenhnhan objPhieuXuatBnhan = new TPhieuXuatthuocBenhnhan();
            objPhieuXuatBnhan.IdBenhnhan = objDonthuoc.IdBenhnhan;
            objPhieuXuatBnhan.MaLuotkham = objDonthuoc.MaLuotkham;

            objPhieuXuatBnhan.NgayXacnhan =  globalVariables.SysDate;
            objPhieuXuatBnhan.IdPhongChidinh = Utility.Int16Dbnull(objDonthuoc.IdPhongkham);
            objPhieuXuatBnhan.IdKhoaChidinh = Utility.Int16Dbnull(objDonthuoc.IdKhoadieutri);
            objPhieuXuatBnhan.IdBacsiKedon = Utility.Int16Dbnull(objDonthuoc.IdBacsiChidinh);
            objPhieuXuatBnhan.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);
            objPhieuXuatBnhan.IdNhanvien = globalVariables.gv_intIDNhanvien;
            //objPhieuXuatBnhan.HienThi = 1;
            objPhieuXuatBnhan.ChanDoan = Utility.sDbnull(objLuotkham.ChanDoan);
            objPhieuXuatBnhan.MabenhChinh = Utility.sDbnull(objLuotkham.MabenhChinh);
            objPhieuXuatBnhan.IdDoituongKcb = Utility.Int16Dbnull(objLuotkham.IdDoituongKcb);
            objPhieuXuatBnhan.MaDoituongKcb = objLuotkham.MaDoituongKcb;
            objPhieuXuatBnhan.GioiTinh = objBenhnhan.GioiTinh;
            objPhieuXuatBnhan.TenBenhnhan = Utility.sDbnull(objBenhnhan.TenBenhnhan);
            objPhieuXuatBnhan.TenKhongdau = Utility.sDbnull(Utility.UnSignedCharacter(objBenhnhan.TenBenhnhan));
            objPhieuXuatBnhan.DiaChi = Utility.sDbnull(objBenhnhan.DiaChi);
            objPhieuXuatBnhan.NamSinh = Utility.Int32Dbnull(objBenhnhan.NamSinh);
            objPhieuXuatBnhan.MatheBhyt = Utility.sDbnull(objLuotkham.MatheBhyt);
            objPhieuXuatBnhan.NgayKedon = objDonthuoc.NgayKedon;
            objPhieuXuatBnhan.NgayTao = globalVariables.SysDate;
            objPhieuXuatBnhan.NguoiTao = globalVariables.UserName;
            objPhieuXuatBnhan.Noitru = objDonthuoc.Noitru;
            objPhieuXuatBnhan.LoaiPhieu = (byte?)LoaiPhieu.PhieuXuatKhoBenhNhan;
            return objPhieuXuatBnhan;
        }
        public TPhieuXuatthuocBenhnhan CreatePhieuXuatBenhNhan(KcbDonthuoc objDonthuoc, KcbDanhsachBenhnhan objBenhnhan, KcbLuotkham objLuotkham)
        {
           

            TPhieuXuatthuocBenhnhan objPhieuXuatBnhan = new TPhieuXuatthuocBenhnhan();
            objPhieuXuatBnhan.IdBenhnhan = objDonthuoc.IdBenhnhan;
            objPhieuXuatBnhan.MaLuotkham = objDonthuoc.MaLuotkham;
            objPhieuXuatBnhan.NgayXacnhan = globalVariables.SysDate;
            objPhieuXuatBnhan.IdPhongChidinh = Utility.Int16Dbnull(objDonthuoc.IdPhongkham);
            objPhieuXuatBnhan.IdKhoaChidinh = Utility.Int16Dbnull(objDonthuoc.IdKhoadieutri);
            objPhieuXuatBnhan.IdBacsiKedon = Utility.Int16Dbnull(objDonthuoc.IdBacsiChidinh);
            objPhieuXuatBnhan.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);
            objPhieuXuatBnhan.IdNhanvien = globalVariables.gv_intIDNhanvien;
            //objPhieuXuatBnhan.HienThi = 1;
            if (objLuotkham != null)
            {
                objPhieuXuatBnhan.ChanDoan = Utility.sDbnull(objLuotkham.ChanDoan);
                objPhieuXuatBnhan.MabenhChinh = Utility.sDbnull(objLuotkham.MabenhChinh);
                objPhieuXuatBnhan.IdDoituongKcb = Utility.Int16Dbnull(objLuotkham.IdDoituongKcb);
                objPhieuXuatBnhan.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                objPhieuXuatBnhan.MatheBhyt = Utility.sDbnull(objLuotkham.MatheBhyt);
            }
            else
            {
                objPhieuXuatBnhan.ChanDoan = "";
                objPhieuXuatBnhan.MabenhChinh = "";
                objPhieuXuatBnhan.IdDoituongKcb = -1;
                objPhieuXuatBnhan.MaDoituongKcb = "DV";
                objPhieuXuatBnhan.MatheBhyt = "";
            }
            objPhieuXuatBnhan.GioiTinh = objBenhnhan.GioiTinh;
            objPhieuXuatBnhan.KieuThuocvattu = objDonthuoc.KieuThuocvattu;
            objPhieuXuatBnhan.TenBenhnhan = Utility.sDbnull(objBenhnhan.TenBenhnhan);
            objPhieuXuatBnhan.TenKhongdau = Utility.sDbnull(Utility.UnSignedCharacter(objBenhnhan.TenBenhnhan));
            objPhieuXuatBnhan.DiaChi = Utility.sDbnull(objBenhnhan.DiaChi);
            objPhieuXuatBnhan.NamSinh = Utility.Int32Dbnull(objBenhnhan.NamSinh);
            
            objPhieuXuatBnhan.NgayKedon = objDonthuoc.NgayKedon;
            objPhieuXuatBnhan.NgayTao = globalVariables.SysDate;
            objPhieuXuatBnhan.NguoiTao = objDonthuoc.NguoiTao;//Dùng cho báo cáo kê đơn theo bác sĩ(trạng thái đã cấp phát để biết người tạo là Admin)
            objPhieuXuatBnhan.NguoiPhatthuoc = globalVariables.UserName;
            objPhieuXuatBnhan.QuayThuoc = objDonthuoc.DonthuocTaiquay;
            objPhieuXuatBnhan.Noitru = objDonthuoc.Noitru;
            objPhieuXuatBnhan.LoaiPhieu = (byte?)LoaiPhieu.PhieuXuatKhoBenhNhan;
            
            return objPhieuXuatBnhan;
        }
        public ActionResult LinhThuocBenhNhan_Tutruc(KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrPresDetails, TPhieuXuatthuocBenhnhan objXuatBnhan)
        {
            try
            {
                HisDuocProperties hisDuocProperties = new HisDuocProperties();
                using (var scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        hisDuocProperties = PropertyLib._HisDuocProperties;
                        objXuatBnhan.IdBenhnhan = objDonthuoc.IdBenhnhan;
                        objXuatBnhan.MaLuotkham = objDonthuoc.MaLuotkham;

                        objXuatBnhan.MaPhieu = THU_VIEN_CHUNG.MaPhieuXuatBN();
                        objXuatBnhan.Noitru = Utility.ByteDbnull(objDonthuoc.Noitru);
                        objXuatBnhan.TenKhongdau = Utility.UnSignedCharacter(objXuatBnhan.TenBenhnhan);
                        objXuatBnhan.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);
                        objXuatBnhan.IsNew = true;
                        objXuatBnhan.Save();
                        Int32 PtramBHYT = 0;
                        SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema).Where(
                            KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objDonthuoc.MaLuotkham)
                            .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objDonthuoc.IdBenhnhan);
                        KcbLuotkham objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                        if (objLuotkham != null)
                        {
                            PtramBHYT = Utility.Int32Dbnull(objLuotkham.PtramBhyt);
                        }
                        foreach (KcbDonthuocChitiet objDetail in arrPresDetails)
                        {
                            ActionResult actionResult = TruThuocTrongTuTruc(objDonthuoc,objDetail, objXuatBnhan);
                            switch (actionResult)
                            {
                                case ActionResult.NotEnoughDrugInStock:
                                    return actionResult;
                                    break;
                            }
                            TXuatthuocTheodon objThuocCt = new TXuatthuocTheodon();
                            objThuocCt.IdPhieuXuat = Utility.Int32Dbnull(objXuatBnhan.IdPhieu);
                            objThuocCt.IdThuoc = Utility.Int32Dbnull(objDetail.IdThuoc);
                            objThuocCt.NgayTao = globalVariables.SysDate;
                            objThuocCt.SoLuong = Utility.Int32Dbnull(objDetail.SoLuong);
                            objThuocCt.NguoiTao = globalVariables.UserName;
                            objThuocCt.PhuThu = Utility.DecimaltoDbnull(objDetail.PhuThu);
                            objThuocCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);

                            //  objThuocCt.gi = Utility.DecimaltoDbnull(objDetail.DonGia);
                            objThuocCt.BnhanChitra = Utility.DecimaltoDbnull(objDetail.BnhanChitra);
                            objThuocCt.BhytChitra = Utility.DecimaltoDbnull(objDetail.BhytChitra);
                            objThuocCt.ChiDan = Utility.sDbnull(objDetail.MotaThem);
                            objThuocCt.ChidanThem = Utility.sDbnull(objDetail.ChidanThem);
                            objThuocCt.SolanDung = Utility.sDbnull(objDetail.SolanDung);
                            objThuocCt.SoluongDung = Utility.sDbnull(objDetail.SoluongDung);
                            objThuocCt.CachDung = Utility.sDbnull(objDetail.CachDung);
                            objThuocCt.PtramBhyt = PtramBHYT;
                            objThuocCt.IdChitietdonthuoc = Utility.Int32Dbnull(objDetail.IdChitietdonthuoc);
                            objThuocCt.IdDonthuoc = Utility.Int32Dbnull(objDetail.IdDonthuoc);
                            // objThuocCt.LoaiDonThuoc = 0;
                            objThuocCt.IsNew = true;
                            objThuocCt.Save();
                        }
                        sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                            .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery.GetRecordCount() <= 0 ? 1 : 0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh cap don thuoc {0}", exception);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// HÀM THỰC HIỆN VIECJ CHO PHÉP CẬP NHẬP ĐƠN THUỐC
        /// </summary>
        /// <returns></returns>
        public ActionResult Linhthuocnoitru(KcbDonthuoc objDonthuoc, TPhieuXuatthuocBenhnhan objXuatBnhan, int ID_KHO_XUAT, DateTime ngaythuchien)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    string THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC", "0", false);
                    HisDuocProperties hisDuocProperties = new HisDuocProperties();
                    objXuatBnhan.IdBenhnhan = objDonthuoc.IdBenhnhan;
                    objXuatBnhan.MaLuotkham = objDonthuoc.MaLuotkham;
                    objXuatBnhan.MaPhieu = THU_VIEN_CHUNG.MaPhieuXuatBN();
                    objXuatBnhan.Noitru = Utility.ByteDbnull(objDonthuoc.Noitru);
                    objXuatBnhan.TenKhongdau = Utility.UnSignedCharacter(objXuatBnhan.TenBenhnhan);
                    objXuatBnhan.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);
                    objXuatBnhan.IsNew = true;
                    objXuatBnhan.Save();
                    Int32 PtramBHYT = 0;
                    SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema).Where(
                        KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objDonthuoc.MaLuotkham)
                        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objDonthuoc.IdBenhnhan);
                    KcbLuotkham objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                    if (objLuotkham != null)
                    {
                        PtramBHYT = Utility.Int32Dbnull(objLuotkham.PtramBhyt);
                    }
                    sqlQuery = new Select().From(TPhieuCapphatChitiet.Schema)
                        .Where(TPhieuCapphatChitiet.Columns.IdCapphat).IsEqualTo(objXuatBnhan.IdCapphat)
                        .And(TPhieuCapphatChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc);

                    TPhieuCapphatChitietCollection objDPhieuCapphatCtCollection =
                        sqlQuery.ExecuteAsCollection<TPhieuCapphatChitietCollection>();
                    foreach (TPhieuCapphatChitiet objCapphatDetail in objDPhieuCapphatCtCollection)
                    {
                        KcbDonthuocChitiet objDetail = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objCapphatDetail.IdChitietdonthuoc)
                            .ExecuteSingle<KcbDonthuocChitiet>();
                        if (objDetail == null) return ActionResult.Exceed;
                        objDetail.SetColumnValue("id_kho", ID_KHO_XUAT);
                        ActionResult actionResult = TruThuocTrongKho_Noitru(objDonthuoc,objCapphatDetail, objDetail, objXuatBnhan, ID_KHO_XUAT, ngaythuchien);
                        switch (actionResult)
                        {
                            case ActionResult.NotEnoughDrugInStock:
                                return actionResult;
                        }

                        TXuatthuocTheodon objThuocCt = new TXuatthuocTheodon();
                        objThuocCt.IdPhieuXuat = Utility.Int32Dbnull(objXuatBnhan.IdPhieu);
                        objThuocCt.IdThuoc = Utility.Int32Dbnull(objCapphatDetail.IdThuoc);
                        objThuocCt.NgayTao = globalVariables.SysDate;
                        objThuocCt.SoLuong = objCapphatDetail.SoLuong;
                        objThuocCt.NguoiTao = globalVariables.UserName;
                        objThuocCt.PhuThu = Utility.DecimaltoDbnull(objDetail.PhuThu);
                        objThuocCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);
                        objThuocCt.BnhanChitra = Utility.DecimaltoDbnull(objDetail.BnhanChitra);
                        objThuocCt.BhytChitra = Utility.DecimaltoDbnull(objDetail.BhytChitra);
                        objThuocCt.ChiDan = Utility.sDbnull(objDetail.MotaThem);
                        objThuocCt.ChidanThem = Utility.sDbnull(objDetail.ChidanThem);
                        objThuocCt.SolanDung = Utility.sDbnull(objDetail.SolanDung);
                        objThuocCt.SoluongDung = Utility.sDbnull(objDetail.SoluongDung);
                        objThuocCt.CachDung = Utility.sDbnull(objDetail.CachDung);
                        objThuocCt.PtramBhyt = PtramBHYT;
                        objThuocCt.IdChitietdonthuoc = Utility.Int32Dbnull(objCapphatDetail.IdChitietdonthuoc);
                        objThuocCt.IdDonthuoc = Utility.Int32Dbnull(objCapphatDetail.IdThuoc);
                        objThuocCt.IsNew = true;
                        objThuocCt.Save();
                        
                    }
                    byte DA_LINH = (byte)(THUOC_NOITRU_XACNHANDALINH_KHIXACNHANDONTHUOC == "1" ? 1 : 0);
                    new Update(TPhieuCapphatChitiet.Schema)
                        .Set(TPhieuCapphatChitiet.Columns.DaLinh).EqualTo(DA_LINH)
                        .Set(TPhieuCapphatChitiet.Columns.IdPhieuxuatthuocBenhnhan).EqualTo(objXuatBnhan.IdPhieu)
                        .Where(TPhieuCapphatChitiet.Columns.IdCapphat).IsEqualTo(objXuatBnhan.IdCapphat).Execute();
                    sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                                .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                                .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                    int status = sqlQuery.GetRecordCount() <= 0 ? 1 : 0;
                    new Update(KcbDonthuoc.Schema)
                              .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                              .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                              .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                              .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    scope.Complete();
                }
                
                return ActionResult.Success;
            }
            catch (Exception exception)
            {
                log.Error("loi trong qua trinh cap don thuoc {0}", exception);
                return ActionResult.Error;

            }
        }

        private ActionResult TruThuocTrongKho_Noitru(KcbDonthuoc objDonthuoc, TPhieuCapphatChitiet objCapphatDetail, KcbDonthuocChitiet objDetail, TPhieuXuatthuocBenhnhan objPhieuXuatBnhan, int ID_KHO_XUAT, DateTime ngaythuchien)
        {
            string errorMessage = "";
            try
            {
                using (var scope = new TransactionScope())
                {
                    TThuockho objTThuockho = new Select().From(TThuockho.Schema)
                                   .Where(TThuockho.IdThuockhoColumn).IsEqualTo(objDetail.IdThuockho)
                                   .ExecuteSingle<TThuockho>();
                    //Kiểm tra xem thuốc còn đủ hay không?
                    if (objTThuockho.SoLuong <= objCapphatDetail.SoLuong)
                    {
                        //Sau này có thể mở rộng thêm code tự động dò và xác định lại Id_thuockho cho các chi tiết đơn thuốc
                        return ActionResult.NotEnoughDrugInStock;
                    }

                    int TONGSOLUONG_LINH = 0;
                    int SOLUONG_LINH = objCapphatDetail.SoLuong;
                    TONGSOLUONG_LINH = objDetail.SluongLinh == null ? 0 : objDetail.SluongLinh.Value;
                    //Tạm REM lại
                    //if (objDetail.SluongLinh.Value <= 0)//Cấp phát lần đầu
                    //    SOLUONG_LINH = objDetail.Quantity;
                    //else//Cấp phát lần n...
                    //{
                    //    if (objDetail.SluongSua.Value > objDetail.SluongLinh.Value)
                    //        SOLUONG_LINH = objDetail.SluongSua.Value - objDetail.SluongLinh.Value;
                    //}

                    TONGSOLUONG_LINH += SOLUONG_LINH;
                    //Đã xác định xong số thuốc cần lĩnh đợt này-->Kiểm tra xem còn đủ hay không
                    List<TThuockho> objThuocKhoCollection = new List<TThuockho>();//Tạm rem lại 20150127 GetObjThuocKhoCollection_Noitru(objCapphatDetail, ID_KHO_XUAT);
                    objThuocKhoCollection.Add(objTThuockho);
                    int iSoLuongConLai = 0;
                    int iSoLuongDonThuoc = 0;
                    int iSoLuongTru = 0;
                    iSoLuongDonThuoc = SOLUONG_LINH;
                    if (objThuocKhoCollection.Sum(c => c.SoLuong) < iSoLuongDonThuoc) return ActionResult.NotEnoughDrugInStock;
                    foreach (TThuockho objDThuocKho in objThuocKhoCollection)
                    {
                        string ErrMsg = "";
                        iSoLuongConLai = Utility.Int32Dbnull(objDThuocKho.SoLuong);
                        ///nếu trưởng hợp số lượng thuốc trong đơn nhỏ hơn số lượng có trong kho thì trừ thẳng luôn
                        if (iSoLuongConLai >= iSoLuongDonThuoc)
                        {
                            iSoLuongTru = iSoLuongConLai - iSoLuongDonThuoc;
                            UpdateXuatChiTietBN(objDonthuoc,objDetail, objDThuocKho, iSoLuongDonThuoc, objPhieuXuatBnhan);
                            StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objDThuocKho.IdKho),
                                                                          Utility.Int32Dbnull(objDThuocKho.IdThuoc, -1),
                                                                          objDThuocKho.NgayHethan, objDetail.GiaNhap, Utility.DecimaltoDbnull(objDetail.GiaBan),
                                                                          Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongDonThuoc, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, PropertyLib._HisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, ErrMsg);

                            sp.Execute();
                            break;
                        }
                        else
                        {
                            iSoLuongTru = iSoLuongDonThuoc - iSoLuongConLai;
                            iSoLuongDonThuoc = iSoLuongTru;
                            UpdateXuatChiTietBN(objDonthuoc,objDetail, objDThuocKho, iSoLuongConLai, objPhieuXuatBnhan);
                            StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objDThuocKho.IdKho),
                                                                          Utility.Int32Dbnull(objDThuocKho.IdThuoc, -1),
                                                                          objDThuocKho.NgayHethan, objDetail.GiaNhap, Utility.DecimaltoDbnull(objDetail.GiaBan),
                                                                          Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongConLai, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, PropertyLib._HisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, ErrMsg);
                            sp.Execute();
                        }
                    }
                    new Update(KcbDonthuocChitiet.Schema)
                        .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                        .Set(KcbDonthuocChitiet.Columns.IdThuockho).EqualTo(objTThuockho.IdThuockho)
                        .Set(KcbDonthuocChitiet.Columns.IdKho).EqualTo(ID_KHO_XUAT)
                         .Set(KcbDonthuocChitiet.Columns.NgayXacnhan).EqualTo(ngaythuchien)
                        .Set(KcbDonthuocChitiet.Columns.SluongLinh).EqualTo(TONGSOLUONG_LINH)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi ban ra tu sp :{0}", errorMessage);
                log.Error("loi trong qua trinh tru thuoc trong kho :{0}", exception.ToString());
            }
            return ActionResult.Success;
        }

        /// <summary>
        /// hàm thực hiện việc trả lại kho 
        /// </summary>
        /// <param name="objDonthuoc"></param>
        /// <param name="objXuatBnhan"></param>
        /// <returns></returns>
        public ActionResult TraLaiThuocChoKho_khongdung(KcbDonthuoc objDonthuoc, TPhieuXuatthuocBenhnhan objXuatBnhan)
        {
            //try
            //{
            //    using (var Scope = new TransactionScope())
            //    {
            //        using (var dbScope = new SharedDbConnectionScope())
            //        {
            //            new Update(TPhieuXuatthuocBenhnhan.Schema)
            //               .Set(TPhieuXuatthuocBenhnhan.Columns.TrangThai).EqualTo(0)
            //               .Where(TPhieuXuatthuocBenhnhan.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
            //               .Execute();
            //            //objXuatBnhan.TrangThai = 0;
            //            //objXuatBnhan.HienThi = 0;
            //            objXuatBnhan.MaPhieu = THU_VIEN_CHUNG.MaPhieuXuatBN();;
            //            objXuatBnhan.Noitru = Utility.ByteDbnull(objDonthuoc.Noitru);
            //            objXuatBnhan.TenKhongdau = Utility.UnSignedCharacter(objXuatBnhan.TenBenhnhan);
            //            objXuatBnhan.IdDonthuoc = Utility.Int32Dbnull(objDonthuoc.IdDonthuoc);
            //            objXuatBnhan.IsNew = true;
            //            objXuatBnhan.Save();
            //            SqlQuery sqlQuery = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
            //                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
            //                .And(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuoc).In(
            //                    new Select(TPhieuXuatthuocBenhnhan.Columns.IdDonthuoc).From(TPhieuXuatthuocBenhnhan.Schema).Where(TPhieuXuatthuocBenhnhan.Columns.TrangThai).
            //                        IsEqualTo(1).And(TPhieuXuatthuocBenhnhan.Columns.IdDonthuoc).IsEqualTo(
            //                            objDonthuoc.IdDonthuoc));

            //            TPhieuXuatthuocBenhnhanChitietCollection objDonthuocDetailCollection =
            //                sqlQuery.ExecuteAsCollection<TPhieuXuatthuocBenhnhanChitietCollection>();
            //            foreach (TPhieuXuatthuocBenhnhanChitiet objPhieuNhapCt in objDonthuocDetailCollection)
            //            {

            //                new Update(KcbDonthuocChitiet.Schema)
            //                    .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(0)
            //                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
            //                    .Execute();
            //                StoredProcedure sp = SPs.ThuocXuatkho(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap,objPhieuNhapCt.GiaBan,
            //                                                         objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
            //                                                         objPhieuNhapCt.IdThuoc, objXuatBnhan.IdKhoxuat, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo);

            //                sp.Execute();
                            
            //                objPhieuNhapCt.IdPhieuXuat = Utility.Int32Dbnull(objXuatBnhan.IdPhieuXuat);
            //                objPhieuNhapCt.IsNew = true;
            //                objPhieuNhapCt.Save();
            //                TBiendongThuoc objXuatNhap = new TBiendongThuoc();
            //                objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuXuat);
            //                objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
            //                objXuatNhap.MaPhieu = Utility.sDbnull(objXuatBnhan.MaPhieu);
            //                objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
            //                //objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhapCt.SoHoadon);
            //                objXuatNhap.PhuThu = 0;
            //                objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
            //                objXuatNhap.Ngaytao =  globalVariables.SysDate;
            //                objXuatNhap.NguoiTao = globalVariables.UserName;
            //                objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.SoLuong*objPhieuNhapCt.GiaNhap);
            //                objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
            //                objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhapCt.Vat);
            //                objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objXuatBnhan.IdNhanvien);
            //                objXuatNhap.IdKho = Utility.Int16Dbnull(objXuatBnhan.IdKhoxuat);
            //                //objXuatNhap.IdKhoxuat = Utility.Int16Dbnull(objXuatBnhan.IdKhoxuat);
            //                objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan;
            //               // objXuatNhap.IdNhaCcap = Utility.Int32Dbnull(objPhieuNhap.IdNhaCcap);
            //                objXuatNhap.IpMayTao = BusinessHelper.GetIP4Address();
            //                objXuatNhap.LoaiPhieu = Utility.ByteDbnull(objXuatBnhan.LoaiPhieu);
            //                objXuatNhap.NgayBiendong = objXuatBnhan.NgayXacnhan;
            //                objXuatNhap.IsNew = true;
            //                objXuatNhap.Save();
                         
                         
            //            }
                      
            //            new Update(KcbDonthuoc.Schema)
            //                .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
            //                .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
            //                .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(1)
            //                .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
            //        }
            //        Scope.Complete();
            //        return ActionResult.Success;
            //    }
            //}
            //catch (Exception)
            //{
                return ActionResult.Error;

            //}
        }
        private ActionResult TruThuocTrongKho(KcbDonthuoc objDonthuoc, KcbDonthuocChitiet objDetail, TPhieuXuatthuocBenhnhan objPhieuXuatBnhan)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var scope = new TransactionScope())
                {

                    TThuockhoCollection objThuocKhoCollection = GetObjThuocKhoCollection(objDetail);
                    int iSoLuongConLai = 0;
                    int iSoLuongDonThuoc = 0;
                    int iSoLuongTru = 0;
                    iSoLuongDonThuoc = Utility.Int32Dbnull(objDetail.SoLuong);
                    if (objThuocKhoCollection.Sum(c => c.SoLuong) < iSoLuongDonThuoc) return ActionResult.NotEnoughDrugInStock;
                    foreach (TThuockho objTThuockho in objThuocKhoCollection)
                    {
                        iSoLuongConLai = Utility.Int32Dbnull(objTThuockho.SoLuong);
                        ///nếu trưởng hợp số lượng thuốc trong đơn nhỏ hơn số lượng có trong kho thì trừ thẳng luôn
                        if (iSoLuongConLai >= iSoLuongDonThuoc)
                        {
                            
                            iSoLuongTru = iSoLuongConLai - iSoLuongDonThuoc;
                            UpdateXuatChiTietBN(objDonthuoc,objDetail, objTThuockho, iSoLuongDonThuoc, objPhieuXuatBnhan);
                            StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                                                                          Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                                                                          objTThuockho.NgayHethan, objTThuockho.GiaNhap, Utility.DecimaltoDbnull(objTThuockho.GiaBan),
                                                                          Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongDonThuoc, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                            break;


                        }
                        else
                        {
                            iSoLuongTru = iSoLuongDonThuoc - iSoLuongConLai;
                            iSoLuongDonThuoc = iSoLuongTru;
                            UpdateXuatChiTietBN(objDonthuoc,objDetail, objTThuockho, iSoLuongConLai, objPhieuXuatBnhan);
                            StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                                                                          Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                                                                          objTThuockho.NgayHethan, objTThuockho.GiaNhap, Utility.DecimaltoDbnull(objTThuockho.GiaBan),
                                                                          Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongConLai, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                        }



                    }
                    new Update(KcbDonthuocChitiet.Schema)
                        .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi ban ra tu sp :{0}", errorMessage);
                log.Error("loi trong qua trinh tru thuoc trong kho :{0}", exception.ToString());
            }

            return ActionResult.Success;
        }
        private ActionResult TruThuocTrongTuTruc(KcbDonthuoc objDonthuoc ,KcbDonthuocChitiet objDetail, TPhieuXuatthuocBenhnhan objPhieuXuatBnhan)
        {
            HisDuocProperties objHisDuocProperties = PropertyLib._HisDuocProperties;
            string errorMessage = "";
            try
            {
                using (var scope = new TransactionScope())
                {
                    int TONGSOLUONG_LINH = 0;
                    int SOLUONG_LINH = 0;
                    TONGSOLUONG_LINH = objDetail.SluongLinh == null ? 0 : objDetail.SluongLinh.Value;
                    if (objDetail.SluongLinh.Value <= 0)//Cấp phát lần đầu
                        SOLUONG_LINH = objDetail.SoLuong;
                    else//Cấp phát lần n...
                    {
                        if (objDetail.SluongSua.Value > objDetail.SluongLinh.Value)
                            SOLUONG_LINH = objDetail.SluongSua.Value - objDetail.SluongLinh.Value;
                    }

                    TONGSOLUONG_LINH += SOLUONG_LINH;
                    //Đã xác định xong số thuốc cần lĩnh đợt này-->Kiểm tra xem còn đủ hay không
                    List<TThuockho> objThuocKhoCollection = GetObjThuocKhoCollection_Tutruc(objDetail, objPhieuXuatBnhan.IdKho.Value);
                    int iSoLuongConLai = 0;
                    int iSoLuongDonThuoc = 0;
                    int iSoLuongTru = 0;
                    iSoLuongDonThuoc = SOLUONG_LINH;
                    if (objThuocKhoCollection.Sum(c => c.SoLuong) < iSoLuongDonThuoc) return ActionResult.NotEnoughDrugInStock;
                    foreach (TThuockho objTThuockho in objThuocKhoCollection)
                    {
                        iSoLuongConLai = Utility.Int32Dbnull(objTThuockho.SoLuong);
                        ///nếu trưởng hợp số lượng thuốc trong đơn nhỏ hơn số lượng có trong kho thì trừ thẳng luôn
                        if (iSoLuongConLai >= iSoLuongDonThuoc)
                        {
                            iSoLuongTru = iSoLuongConLai - iSoLuongDonThuoc;
                            UpdateXuatChiTietBN(objDonthuoc,objDetail, objTThuockho, iSoLuongDonThuoc, objPhieuXuatBnhan);
                            StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                                                                          Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                                                                          objTThuockho.NgayHethan, objTThuockho.GiaNhap, Utility.DecimaltoDbnull(objTThuockho.GiaBan),
                                                                          Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongDonThuoc, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                            break;


                        }
                        else
                        {
                            iSoLuongTru = iSoLuongDonThuoc - iSoLuongConLai;
                            iSoLuongDonThuoc = iSoLuongTru;
                            UpdateXuatChiTietBN(objDonthuoc,objDetail, objTThuockho, iSoLuongConLai, objPhieuXuatBnhan);
                            StoredProcedure sp = SPs.ThuocXuatkho(Utility.Int32Dbnull(objTThuockho.IdKho),
                                                                          Utility.Int32Dbnull(objTThuockho.IdThuoc, -1),
                                                                          objTThuockho.NgayHethan, objTThuockho.GiaNhap, Utility.DecimaltoDbnull(objTThuockho.GiaBan),
                                                                          Utility.DecimaltoDbnull(objTThuockho.Vat), iSoLuongConLai, objTThuockho.IdThuockho, objTThuockho.MaNhacungcap, objTThuockho.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                        }



                    }
                    new Update(KcbDonthuocChitiet.Schema)
                        .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                        .Set(KcbDonthuocChitiet.Columns.SluongLinh).EqualTo(TONGSOLUONG_LINH)
                        .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi ban ra tu sp :{0}", errorMessage);
                log.Error("loi trong qua trinh tru thuoc trong kho :{0}", exception.ToString());
            }
            return ActionResult.Success;
        }
      
        private TThuockhoCollection GetObjThuocKhoCollection(KcbDonthuocChitiet objDetail)
        {
            SqlQuery sqlQuery;
            sqlQuery = new Select().From(TThuockho.Schema)
                .Where(TThuockho.Columns.IdKho).IsEqualTo(objDetail.IdKho)
                .And(TThuockho.Columns.NgayHethan).IsGreaterThanOrEqualTo( globalVariables.SysDate.Date)
                .And(TThuockho.Columns.IdThuoc).IsEqualTo(objDetail.IdThuoc)
                .And(TThuockho.Columns.SoLuong).IsGreaterThan(0)
                .OrderAsc(TThuockho.Columns.NgayHethan)
                .OrderAsc(TThuockho.Columns.GiaNhap);
            return sqlQuery.ExecuteAsCollection<TThuockhoCollection>();
        }
        public DataTable GetObjThuocKhoCollection(int id_kho, int id_thuoc, int id_thuockho, int so_luong, byte id_loaidoituong_kcb, byte Dungtuyen, byte Noitru)
        {
            //Lấy thuốc trong kho
            
            
            DataTable dtData = SPs.ThuocLaythuocTrongkhoKedon(id_kho, id_thuoc, id_thuockho, id_loaidoituong_kcb, Dungtuyen, Noitru).GetDataSet().Tables[0];
            
            DataTable dtReturnData = dtData.Clone();
            //Lấy số lượng ảo của các đơn thuốc có thuốc chưa được xác nhận
            DataTable dtPresDetail = SPs.ThuocLaythuocKedontrongngayChuaxacnhan(id_kho, id_thuoc, so_luong).GetDataSet().Tables[0];
            List<TThuockho> lstItems = new List<TThuockho>();
            int iSoLuongConLai = 0;
            int iSoLuongDonThuoc = so_luong;
            int iSoLuongTru = 0;
            int soluongAo = 0;
            foreach (DataRow item in dtData.Rows)
            {
                DataRow[] arrDR = dtPresDetail.Select(TThuockho.Columns.IdThuockho + "=" + Utility.sDbnull(item[TThuockho.Columns.IdThuockho]));
                if (arrDR.Length > 0)
                    soluongAo = Utility.Int32Dbnull(arrDR.CopyToDataTable().Compute("SUM(" + KcbDonthuocChitiet.Columns.SoLuong + ")", "1=1"), 0);
                else
                    soluongAo = 0;
                //Tính số lượng kho còn lại
                iSoLuongConLai =Utility.Int32Dbnull(item[TThuockho.Columns.SoLuong],0)  - soluongAo;
                if (iSoLuongConLai >= iSoLuongDonThuoc)
                {
                    iSoLuongTru = iSoLuongConLai - iSoLuongDonThuoc;
                    item[TThuockho.Columns.SoLuong] = iSoLuongDonThuoc;
                    if (Utility.Int32Dbnull(item[TThuockho.Columns.SoLuong], 0) > 0)
                        dtReturnData.ImportRow(item);
                    break;
                }
                else//Số lượng kho < số lượng thuốc
                {
                    //Lượng nhỏ hơn
                    iSoLuongTru = iSoLuongDonThuoc - iSoLuongConLai;
                    iSoLuongDonThuoc = iSoLuongTru;
                    item[TThuockho.Columns.SoLuong] = iSoLuongConLai;
                    if (Utility.Int32Dbnull(item[TThuockho.Columns.SoLuong], 0) > 0)
                        dtReturnData.ImportRow(item);
                }
            }
            return dtReturnData;

        }
       
       
        private List<TThuockho> GetObjThuocKhoCollection_Tutruc(KcbDonthuocChitiet objDetail, int KHO)
        {
            return new TThuockhoController().FetchByQuery(TThuockho.CreateQuery()
                .AddWhere(TThuockho.Columns.IdKho, Comparison.Equals, KHO)
                .AND(TThuockho.Columns.NgayHethan, Comparison.GreaterOrEquals,  globalVariables.SysDate.Date)
                .AND(TThuockho.Columns.IdThuoc, Comparison.Equals, objDetail.IdThuoc)
                .AND(TThuockho.Columns.SoLuong, Comparison.GreaterThan, 0)
                .ORDER_BY(TThuockho.NgayHethanColumn, "ASC")).ToList<TThuockho>();
        }
        /// <summary>
        /// hàm thực hiện việc xuất thôn gtin bảng chi tiết của bệnh nhân
        /// </summary>
        /// <param name="objDetail"></param>
        /// <param name="objTThuockho"></param>
        /// <param name="iSoLuongDonThuoc"></param>
        /// <param name="objPhieuXuatBnhan"></param>
        private void UpdateXuatChiTietBN(KcbDonthuoc objDonthuoc, KcbDonthuocChitiet objDetail, TThuockho objTThuockho, int iSoLuonTru, TPhieuXuatthuocBenhnhan objPhieuXuatBnhan)
        {
            using (var scope = new TransactionScope())
            {
                TPhieuXuatthuocBenhnhanChitiet objXuatBnhanCt = new TPhieuXuatthuocBenhnhanChitiet();
                objXuatBnhanCt.IdPhieu = Utility.Int32Dbnull(objPhieuXuatBnhan.IdPhieu);
                objXuatBnhanCt.SoLuong = iSoLuonTru;
                objXuatBnhanCt.ChiDan = objDetail.MotaThem;
                objXuatBnhanCt.IdThuoc = Utility.Int32Dbnull(objDetail.IdThuoc);
                objXuatBnhanCt.NgayHethan = objDetail.NgayHethan;// objTThuockho.NgayHethan.Date;
                objXuatBnhanCt.IdThuockho = objDetail.IdThuockho;
                objXuatBnhanCt.SoLo = objDetail.SoLo;
                objXuatBnhanCt.MaNhacungcap = objDetail.MaNhacungcap;
                objXuatBnhanCt.Vat = (int)objDetail.Vat;
                objXuatBnhanCt.DonGia = Utility.DecimaltoDbnull(objDetail.DonGia);//đơn giá cho bệnh nhân
                objXuatBnhanCt.Vat = Utility.Int32Dbnull(objDetail.Vat);
                objXuatBnhanCt.GiaBan = Utility.DecimaltoDbnull(objDetail.GiaBan);//giá bán
                objXuatBnhanCt.GiaNhap = Utility.DecimaltoDbnull(objDetail.GiaNhap);//giá nhập

                objXuatBnhanCt.PhuthuTraituyen = objDetail.PhuthuTraituyen;
                objXuatBnhanCt.PhuthuDungtuyen = objDetail.PhuthuDungtuyen;

                objXuatBnhanCt.IdKho = Utility.Int16Dbnull(objDetail.IdKho);
                objXuatBnhanCt.IdChitietdonthuoc = Utility.Int32Dbnull(objDetail.IdChitietdonthuoc);
                
                objXuatBnhanCt.NgayNhap = objTThuockho.NgayNhap;
                objXuatBnhanCt.IsNew = true;
                objXuatBnhanCt.Save();
                TBiendongThuoc objNhapXuat = new TBiendongThuoc();
                objNhapXuat.NgayHethan = objDetail.NgayHethan;// objTThuockho.NgayHethan.Date;
                objNhapXuat.IdThuockho = objDetail.IdThuockho;
                objNhapXuat.SoLo = objDetail.SoLo;
                objNhapXuat.MaNhacungcap = objDetail.MaNhacungcap;
                objNhapXuat.Vat = (int)objDetail.Vat;
                objNhapXuat.QuayThuoc = objPhieuXuatBnhan.QuayThuoc;
                objNhapXuat.MaPhieu = Utility.sDbnull(objPhieuXuatBnhan.MaPhieu);
                objNhapXuat.Noitru = objPhieuXuatBnhan.Noitru;
                objNhapXuat.NgayHoadon = objDonthuoc.NgayKedon;
                objNhapXuat.NgayBiendong = objPhieuXuatBnhan.NgayXacnhan;
                objNhapXuat.NgayTao = globalVariables.SysDate;
                objNhapXuat.NguoiTao = globalVariables.UserName;
                objNhapXuat.SoLuong = Utility.Int32Dbnull(objXuatBnhanCt.SoLuong);
                objNhapXuat.Vat = Utility.Int32Dbnull(objXuatBnhanCt.Vat);
                objNhapXuat.DonGia = Utility.DecimaltoDbnull(objXuatBnhanCt.DonGia);
                objNhapXuat.GiaBan = Utility.DecimaltoDbnull(objXuatBnhanCt.GiaBan);
                objNhapXuat.GiaNhap = Utility.DecimaltoDbnull(objXuatBnhanCt.GiaNhap);
                objNhapXuat.PhuThu = objDetail.PhuThu;
                objNhapXuat.SoHoadon = "-1";
                objNhapXuat.IdThuoc = Utility.Int32Dbnull(objXuatBnhanCt.IdThuoc);
                objNhapXuat.IdPhieu = Utility.Int32Dbnull(objPhieuXuatBnhan.IdPhieu);
                objNhapXuat.IdPhieuChitiet = Utility.Int32Dbnull(objXuatBnhanCt.IdPhieuChitiet);
                objNhapXuat.IdNhanvien = globalVariables.gv_intIDNhanvien;
                objNhapXuat.NgayNhap = objTThuockho.NgayNhap;
                objNhapXuat.KieuThuocvattu = objPhieuXuatBnhan.KieuThuocvattu;

                objNhapXuat.GiaPhuthuTraituyen = objDetail.PhuthuTraituyen;
                objNhapXuat.GiaPhuthuDungtuyen = objDetail.PhuthuDungtuyen;

                objNhapXuat.MaNhacungcap = objXuatBnhanCt.MaNhacungcap;
                objNhapXuat.IdKho = Utility.Int16Dbnull(objPhieuXuatBnhan.IdKho);
                objNhapXuat.MaPhieu = Utility.sDbnull(objPhieuXuatBnhan.MaPhieu);
                objNhapXuat.MaLoaiphieu = Utility.ByteDbnull(objPhieuXuatBnhan.LoaiPhieu);
                objNhapXuat.TenLoaiphieu = Utility.TenLoaiPhieu((LoaiPhieu)objPhieuXuatBnhan.LoaiPhieu);
                objNhapXuat.IdKhoaLinh = objPhieuXuatBnhan.IdKhoaChidinh;
                objNhapXuat.KieuThuocvattu = objDonthuoc.KieuThuocvattu;
                objNhapXuat.ThanhTien = Utility.DecimaltoDbnull(objXuatBnhanCt.DonGia) *
                                        Utility.Int32Dbnull(objXuatBnhanCt.SoLuong);
                objNhapXuat.IsNew = true;
                objNhapXuat.Save();
                scope.Complete();
            }
        }

        /// <summary>
        /// hàm thực hiện việc cập nhập trạng thái của đơn thuốc
        /// </summary>
        /// <param name="objDonthuoc"></param>
        private void UpdateTrangThaiDonThuoc(KcbDonthuoc objDonthuoc)
        {
            using (var scope = new TransactionScope())
            {
                new Update(KcbDonthuoc.Schema)
                    .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(0)
                    .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                new Update(KcbDonthuocChitiet.Schema)
                    .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(1)
                    .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                scope.Complete();
            }
        }

        public ActionResult XacNhanPhieuHuy_thanhly_thuoc(TPhieuNhapxuatthuoc objPhieuNhap, DateTime ngayxacnhan, ref string errMsg)
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
                        objPhieuNhap.NgayXacnhan = ngayxacnhan;
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            //Kiểm tra đề phòng Kho A-->Xuất kho B. Kho B xác nhận-->Xuất kho C. Kho B hủy xác nhận. Kho C xác nhận dẫn tới việc kho B chưa có thuốc để trừ kho

                            ActionResult _Kiemtrathuocxacnhan = Kiemtrathuocxacnhan(objPhieuNhap, objPhieuNhapCt, ref errMsg);
                            if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;

                            long idthuockho = -1;
                            StoredProcedure sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.Int32Dbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdChuyen,
                                                          objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                                          objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                            //Insert dòng kho xuất
                            TBiendongThuoc  objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.DonGia);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);

                            objXuatNhap.GiaBhyt = objPhieuNhapCt.GiaBhyt;
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;

                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;

                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.DuTru = objPhieuNhap.DuTru;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao = globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.IdThuockho = Utility.Int32Dbnull(objPhieuNhapCt.IdChuyen);
                            
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.MaLoaiphieu = objPhieuNhap.LoaiPhieu;
                            objXuatNhap.TenLoaiphieu = objPhieuNhap.TenLoaiphieu;
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();

                        }
                        new Update(TPhieuNhapxuatthuoc.Schema)
                             .Set(TPhieuNhapxuatthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(ngayxacnhan)
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
                Utility.CatchException("Lỗi khi xác nhận phiếu ",ex);
                return ActionResult.Error;
            }
        }
        public ActionResult HuyXacNhanPhieuHuy_thanhly_Thuoc(TPhieuNhapxuatthuoc objPhieuNhap, ref string errMsg)
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
                            //Nhập lại kho thanh lý
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, idthuockho, idthuockho, objPhieuNhapCt.NgayNhap, objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen);
                            sp.Execute();
                            idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            if (idthuockho != objPhieuNhapCt.IdThuockho)//Nếu ai đó xóa bằng tay trong bảng thuốc kho thì cần update lại
                                new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(idthuockho)
                                    .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                        }
                        //Xóa toàn bộ chi tiết trong TBiendongThuoc
                        new Delete().From(TBiendongThuoc.Schema)
                            .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(0)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi hủy xác nhận phiếu ",ex);
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult XacNhanPhieuXuatKho(TPhieuNhapxuatthuoc objPhieuNhap,DateTime ngayxacnhan, ref string errMsg)
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
                        objPhieuNhap.NgayXacnhan = ngayxacnhan;
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in objPhieuNhapCtCollection)
                        {
                            //Kiểm tra đề phòng Kho A-->Xuất kho B. Kho B xác nhận-->Xuất kho C. Kho B hủy xác nhận. Kho C xác nhận dẫn tới việc kho B chưa có thuốc để trừ kho

                            ActionResult _Kiemtrathuocxacnhan = Kiemtrathuocxacnhan(objPhieuNhap, objPhieuNhapCt, ref errMsg);
                            if (_Kiemtrathuocxacnhan != ActionResult.Success) return _Kiemtrathuocxacnhan;

                            long idthuockho = -1;
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhonhap,
                                                                      objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, -1, idthuockho, ngayxacnhan, objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen);
                            sp.Execute();
                            idthuockho = Utility.Int64Dbnull(sp.OutputValues[0], -1);
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.Int32Dbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdChuyen,
                                                          objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo,
                                                          objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                            new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(idthuockho)
                               .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                            objPhieuNhapCt.IdThuockho = idthuockho;
                            //Insert dòng kho nhập
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.IdChuyen = objPhieuNhapCt.IdChuyen;
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            
                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;
                            
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.DuTru = objPhieuNhap.DuTru;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao = globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.IdThuockho = Utility.Int32Dbnull(objPhieuNhapCt.IdThuockho);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhonhap);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;

                            objXuatNhap.MaLoaiphieu = (byte)LoaiPhieu.PhieuNhapKho;
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuNhapKho);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            //Insert dòng của kho xuất
                            objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.NgayNhap = objPhieuNhapCt.NgayNhap;
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);

                            objXuatNhap.GiaPhuthuDungtuyen = objPhieuNhapCt.GiaPhuthuDungtuyen;
                            objXuatNhap.GiaPhuthuTraituyen = objPhieuNhapCt.GiaPhuthuTraituyen;

                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.IdChuyen = -1;
                            objXuatNhap.DuTru = objPhieuNhap.DuTru;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao = globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.IdThuockho = Utility.Int32Dbnull(objPhieuNhapCt.IdChuyen);
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhapCt.MaNhacungcap;
                            objXuatNhap.SoChungtuKemtheo = objPhieuNhap.SoChungtuKemtheo;
                            objXuatNhap.SoLo = objPhieuNhapCt.SoLo;
                            objXuatNhap.MaLoaiphieu = (byte)LoaiPhieu.PhieuXuatKho;
                            objXuatNhap.TenLoaiphieu = Utility.TenLoaiPhieu(LoaiPhieu.PhieuXuatKho);
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayXacnhan;
                            objXuatNhap.NgayHoadon = objPhieuNhap.NgayHoadon;
                            objXuatNhap.KieuThuocvattu = objPhieuNhapCt.KieuThuocvattu;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                           
                        }
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(ngayxacnhan)
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
                Utility.CatchException("Lỗi khi xác nhận phiếu xuất kho", ex);
                return ActionResult.Error;
            }
        }

        public ActionResult XacnhanPhieuTrathuocNhacungcap(TPhieuNhapxuatthuoc objPhieuNhap)
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
                            //Insert dòng hủy vào TBiendongThuoc
                            TBiendongThuoc objXuatNhap = new TBiendongThuoc();
                            objXuatNhap.IdPhieu = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieu);
                            objXuatNhap.IdPhieuChitiet = Utility.Int32Dbnull(objPhieuNhapCt.IdPhieuchitiet);
                            objXuatNhap.MaPhieu = Utility.sDbnull(objPhieuNhap.MaPhieu);
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            
                            objXuatNhap.DonGia = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.NgayHoadon =objPhieuNhap.NgayHoadon;
                            objXuatNhap.GiaBan = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaBan);
                            objXuatNhap.GiaNhap = Utility.DecimaltoDbnull(objPhieuNhapCt.GiaNhap);
                            objXuatNhap.SoLo = Utility.sDbnull(objPhieuNhapCt.SoLo);
                            objXuatNhap.IdThuockho = Utility.Int32Dbnull(objPhieuNhapCt.IdThuockho);
                            objXuatNhap.KieuThuocvattu = Utility.sDbnull(objPhieuNhapCt.KieuThuocvattu);

                            objXuatNhap.SoChungtuKemtheo = "";
                            objXuatNhap.Noitru = 0;
                            objXuatNhap.QuayThuoc = 0;
                            objXuatNhap.GiaBhyt = 0;
                            objXuatNhap.GiaBhytCu = 0;
                            objXuatNhap.GiaPhuthuDungtuyen = 0;
                            objXuatNhap.GiaPhuthuTraituyen = 0;
                            objXuatNhap.DuTru = 0;

                            objXuatNhap.SoHoadon = Utility.sDbnull(objPhieuNhap.SoHoadon);
                            objXuatNhap.PhuThu = 0;
                            objXuatNhap.SoLuong = Utility.Int32Dbnull(objPhieuNhapCt.SoLuong);
                            objXuatNhap.NgayTao =  globalVariables.SysDate;
                            objXuatNhap.NguoiTao = globalVariables.UserName;
                            objXuatNhap.ThanhTien = Utility.DecimaltoDbnull(objPhieuNhapCt.ThanhTien);
                            objXuatNhap.IdThuoc = Utility.Int32Dbnull(objPhieuNhapCt.IdThuoc);
                            objXuatNhap.Vat = Utility.Int32Dbnull(objPhieuNhap.Vat);
                            objXuatNhap.IdNhanvien = Utility.Int16Dbnull(objPhieuNhap.IdNhanvien);
                            objXuatNhap.IdKho = Utility.Int16Dbnull(objPhieuNhap.IdKhoxuat);
                            objXuatNhap.NgayHethan = objPhieuNhapCt.NgayHethan.Date;
                            objXuatNhap.MaNhacungcap = objPhieuNhap.MaNhacungcap;
                            objXuatNhap.MaLoaiphieu = objPhieuNhap.LoaiPhieu;
                            objXuatNhap.TenLoaiphieu = objPhieuNhap.TenLoaiphieu;
                            objXuatNhap.NgayBiendong = objPhieuNhap.NgayHoadon;
                            objXuatNhap.IsNew = true;
                            objXuatNhap.Save();
                            StoredProcedure sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhoxuat, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.Int32Dbnull(objXuatNhap.SoLuong), objPhieuNhapCt.IdThuockho, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);

                            sp.Execute();
                        }
                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(globalVariables.gv_intIDNhanvien)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(globalVariables.UserName)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo( globalVariables.SysDate)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(1)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).Execute();
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
        /// <summary>
        /// Kiểm tra xem thuốc trong kho xuất đã được sử dụng hay chưa?
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="objPhieuNhapCt"></param>
        /// <returns></returns>
        public ActionResult Kiemtrathuochuyxacnhan(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt,ref string errMsg)
        {
            TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
              TThuockho.CreateQuery()
              .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhonhap)
              .AND(TThuockho.IdThuocColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuoc)
              .AND(TThuockho.NgayHethanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayHethan.Date)
              .AND(TThuockho.GiaNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaNhap)
              .AND(TThuockho.GiaBanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBan)
              .AND(TThuockho.MaNhacungcapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.MaNhacungcap)
              .AND(TThuockho.SoLoColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.SoLo)
              .AND(TThuockho.VatColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.Vat)
              );

            if (vCollection.Count <= 0)
            {
                errMsg = string.Format("ID thuốc={0}, không tồn tại trong kho {1}", objPhieuNhapCt.IdThuoc.ToString(), objPhieuNhap.IdKhonhap.ToString());
                return ActionResult.Exceed;//Lỗi không có dòng dữ liệu trong bảng kho-thuốc
            }
            int SoLuong = vCollection[0].SoLuong;
            SoLuong = SoLuong - objPhieuNhapCt.SoLuong;
            if (SoLuong < 0)
            {
                errMsg = string.Format("ID thuốc={0}, Số lượng còn trong kho {1}, Số lượng bị trừ {2}", objPhieuNhapCt.IdThuoc.ToString(), vCollection[0].SoLuong.ToString(), objPhieuNhapCt.SoLuong.ToString());
                return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
            }
            return ActionResult.Success;
        }
        public ActionResult Kiemtrathuocxacnhan(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet objPhieuNhapCt, ref string errMsg)
        {
            TThuockhoCollection vCollection = new TThuockhoController().FetchByQuery(
              TThuockho.CreateQuery()
              .WHERE(TThuockho.IdKhoColumn.ColumnName, Comparison.Equals, objPhieuNhap.IdKhoxuat)
              .AND(TThuockho.IdThuocColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.IdThuoc)
              .AND(TThuockho.NgayHethanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayHethan.Date)
              .AND(TThuockho.GiaNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaNhap)
              .AND(TThuockho.GiaBanColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBan)
              .AND(TThuockho.MaNhacungcapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.MaNhacungcap)
              .AND(TThuockho.SoLoColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.SoLo)
               .AND(TThuockho.NgayNhapColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.NgayNhap)
                .AND(TThuockho.GiaBhytColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.GiaBhyt)
              .AND(TThuockho.VatColumn.ColumnName, Comparison.Equals, objPhieuNhapCt.Vat)
              );

            if (vCollection.Count <= 0)
            {
                errMsg = string.Format("ID thuốc={0}, không tồn tại trong kho {1}", objPhieuNhapCt.IdThuoc.ToString(), objPhieuNhap.IdKhonhap.ToString());
                return ActionResult.Exceed;//Lỗi không có dòng dữ liệu trong bảng kho-thuốc
            }
            int SoLuong = vCollection[0].SoLuong;
            SoLuong = SoLuong - objPhieuNhapCt.SoLuong;
            if (SoLuong < 0)
            {
                errMsg = string.Format("ID thuốc={0}, Số lượng còn trong kho {1}, Số lượng bị trừ {2}", objPhieuNhapCt.IdThuoc.ToString(), vCollection[0].SoLuong.ToString(), objPhieuNhapCt.SoLuong.ToString());
                return ActionResult.NotEnoughDrugInStock;//Thuốc đã sử dụng nhiều nên không thể hủy
            }
            return ActionResult.Success;
        }
        /// <summary>
        /// hàm thực hiện việc xác nhận thông tin 
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <returns></returns>
        public ActionResult HuyXacNhanPhieuXuatKho(TPhieuNhapxuatthuoc objPhieuNhap, ref string errMsg)
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
                            //Kiểm tra ở kho nhập xem thuốc đã sử dụng chưa
                            ActionResult _Kiemtrathuochuyxacnhan = Kiemtrathuochuyxacnhan(objPhieuNhap, objPhieuNhapCt, ref errMsg);
                            if (_Kiemtrathuochuyxacnhan != ActionResult.Success) return _Kiemtrathuochuyxacnhan;
                            //Xóa biến động kho nhập
                            new Delete().From(TBiendongThuoc.Schema)
                                .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                                .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
                                .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)LoaiPhieu.PhieuNhapKho).Execute();
                            //Xóa biến động kho xuất
                            new Delete().From(TBiendongThuoc.Schema)
                               .Where(TBiendongThuoc.IdPhieuColumn).IsEqualTo(objPhieuNhap.IdPhieu)
                               .And(TBiendongThuoc.IdPhieuChitietColumn).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet)
                               .And(TBiendongThuoc.MaLoaiphieuColumn).IsEqualTo((byte)LoaiPhieu.PhieuXuatKho).Execute();
                            long id_Thuockho_new = -1;
                            new Update(TPhieuNhapxuatthuocChitiet.Schema).Set(TPhieuNhapxuatthuocChitiet.Columns.IdThuockho).EqualTo(-1)
                              .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieuchitiet).IsEqualTo(objPhieuNhapCt.IdPhieuchitiet).Execute();
                            StoredProcedure sp = SPs.ThuocNhapkhoOutput(objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap, objPhieuNhapCt.GiaBan,
                                                                      objPhieuNhapCt.SoLuong, Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                                      objPhieuNhapCt.IdThuoc, objPhieuNhap.IdKhoxuat, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objPhieuNhapCt.IdThuockho.Value, id_Thuockho_new, objPhieuNhapCt.NgayNhap, objPhieuNhapCt.GiaBhyt, objPhieuNhapCt.GiaPhuthuDungtuyen, objPhieuNhapCt.GiaPhuthuTraituyen);
                            sp.Execute();
                            sp = SPs.ThuocXuatkho(objPhieuNhap.IdKhonhap, objPhieuNhapCt.IdThuoc,
                                                          objPhieuNhapCt.NgayHethan, objPhieuNhapCt.GiaNhap,objPhieuNhapCt.GiaBan,
                                                          Utility.DecimaltoDbnull(objPhieuNhapCt.Vat),
                                                          Utility.Int32Dbnull(objPhieuNhapCt.SoLuong), objPhieuNhapCt.IdThuockho, objPhieuNhapCt.MaNhacungcap, objPhieuNhapCt.SoLo, objHisDuocProperties.XoaDulieuKhiThuocDaHet ? 1 : 0, errorMessage);
                            sp.Execute();
                        }
                       

                        new Update(TPhieuNhapxuatthuoc.Schema)
                            .Set(TPhieuNhapxuatthuoc.Columns.IdNhanvien).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NguoiXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.NgayXacnhan).EqualTo(null)
                            .Set(TPhieuNhapxuatthuoc.Columns.TrangThai).EqualTo(0)
                            .Where(TPhieuNhapxuatthuoc.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu)
                            .And(TPhieuNhapxuatthuoc.LoaiPhieuColumn).IsEqualTo(objPhieuNhap.LoaiPhieu).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi hủy xác nhận phiếu chuyển kho", ex);
                return ActionResult.Error;
            }
        }

      
        /// <summary>
        /// hàm thực hiện việc thêm phiếu nhập kho thuốc
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="arrPhieuNhapCts"></param>
        /// <returns></returns>
        public ActionResult ThemPhieuXuatKho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet[] arrPhieuNhapCts)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        objPhieuNhap.NgayTao = globalVariables.SysDate;
                        objPhieuNhap.NguoiTao = globalVariables.UserName;
                        objPhieuNhap.MaPhieu = Utility.sDbnull(THU_VIEN_CHUNG.MaNhapKho(Utility.Int32Dbnull(objPhieuNhap.LoaiPhieu)));
                        objPhieuNhap.TongTien = arrPhieuNhapCts.Sum(c => c.ThanhTien);
                        objPhieuNhap.IsNew = true;
                        objPhieuNhap.Save();
                       
                        objPhieuNhap = TPhieuNhapxuatthuoc.FetchByID(objPhieuNhap.IdPhieu);
                        if (objPhieuNhap != null)
                        {
                            foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in arrPhieuNhapCts)
                            {
                                objPhieuNhapCt.IdPhieu = Utility.Int32Dbnull(objPhieuNhap.IdPhieu, -1);
                                objPhieuNhapCt.IsNew = true;
                                objPhieuNhapCt.Save();
                            }
                        }
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh them :{0}", exception);
                return ActionResult.Error;

            }
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập thông tin nhập kho thuốc
        /// </summary>
        /// <param name="objPhieuNhap"></param>
        /// <param name="arrPhieuNhapCts"></param>
        /// <returns></returns>
        public ActionResult UpdatePhieuXuatKho(TPhieuNhapxuatthuoc objPhieuNhap, TPhieuNhapxuatthuocChitiet[] arrPhieuNhapCts)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        objPhieuNhap.NgaySua =  globalVariables.SysDate;
                        objPhieuNhap.NguoiSua = globalVariables.UserName;
                        objPhieuNhap.TongTien = arrPhieuNhapCts.Sum(c => c.ThanhTien);
                        objPhieuNhap.Save();

                        new Delete().From(TPhieuNhapxuatthuocChitiet.Schema)
                            .Where(TPhieuNhapxuatthuocChitiet.Columns.IdPhieu).IsEqualTo(objPhieuNhap.IdPhieu).Execute();
                        foreach (TPhieuNhapxuatthuocChitiet objPhieuNhapCt in arrPhieuNhapCts)
                        {
                            objPhieuNhapCt.IdPhieu= Utility.Int32Dbnull(objPhieuNhap.IdPhieu, -1);
                            objPhieuNhapCt.IsNew = true;
                            objPhieuNhapCt.Save();
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
        /// hàm thực hiện việc xóa thông tin chi tiết đơn thuốc
        /// </summary>
        /// <param name="PresDetail_ID"></param>
        /// <returns></returns>
        public ActionResult XoaThongTinChiTietDonThuoc(System.Collections.ArrayList arrayList,int Pres_ID)
        {
              try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Delete().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(arrayList).Execute();
                        SqlQuery sqlQuery = new Select().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(Pres_ID);
                        if(sqlQuery.GetRecordCount()<=0)
                        {
                            KcbDonthuoc.Delete(Pres_ID);
                        }

                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
              catch (Exception exception)
              {
                  log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                  return ActionResult.Error;

              }
            
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin chi tiết đơn thuốc
        /// </summary>
        /// <param name="PresDetail_ID"></param>
        /// <returns></returns>
        public ActionResult XoaThongTinThongTinDonThuoc(int Pres_ID)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        new Delete().From(KcbDonthuocChitiet.Schema)
                            .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).In(Pres_ID).Execute();
                        KcbDonthuoc.Delete(Pres_ID);
                      

                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;
            }
        }

      
      
     

        #region "hàm thực hiện việc hủy thông tin phat thuốc cho kho"
        public ActionResult HuyXacNhanDonThuocBN(KcbDonthuoc objDonthuoc,KcbDonthuocChitiet []arrDetails)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        HisDuocProperties objHisDuocProperties=new HisDuocProperties();
                       // if(objHisDuocProperties.KieuDuyetDonThuoc=="DONTHUOC")id_kho=Utility.Int32Dbnull(objDonthuoc.IdKho)
                        objHisDuocProperties = PropertyLib._HisDuocProperties;
                        int id_thuockho = -1;
                        foreach (KcbDonthuocChitiet objDetail in arrDetails)
                        {
                            SqlQuery sqlQuery = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc);
                            TPhieuXuatthuocBenhnhanChitietCollection objXuatBnhanCtCollection =
                                sqlQuery.ExecuteAsCollection<TPhieuXuatthuocBenhnhanChitietCollection>();
                            foreach (TPhieuXuatthuocBenhnhanChitiet PhieuXuatBnhanCt in objXuatBnhanCtCollection)
                            {
                                StoredProcedure sp = SPs.ThuocNhapkhoOutput(PhieuXuatBnhanCt.NgayHethan, PhieuXuatBnhanCt.DonGia, PhieuXuatBnhanCt.GiaBan,
                                                                 PhieuXuatBnhanCt.SoLuong, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.Vat),
                                                                 PhieuXuatBnhanCt.IdThuoc, PhieuXuatBnhanCt.IdKho, PhieuXuatBnhanCt.MaNhacungcap, 
                                                                 PhieuXuatBnhanCt.SoLo,-1,id_thuockho,
                                                                 objDonthuoc.NgayXacnhan, PhieuXuatBnhanCt.DonGia, PhieuXuatBnhanCt.PhuthuDungtuyen, PhieuXuatBnhanCt.PhuthuTraituyen);

                                sp.Execute();
                                ///xóa thông tin bảng chi tiết
                                new Delete().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .Execute();

                                new Delete().From(TBiendongThuoc.Schema)
                                    .Where(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(3).Execute();

                                sqlQuery = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieu).IsEqualTo(
                                        PhieuXuatBnhanCt.IdPhieu);
                                if(sqlQuery.GetRecordCount()<=0)
                                {
                                    TPhieuXuatthuocBenhnhan.Delete(PhieuXuatBnhanCt.IdPhieu);
                                }
                            }
                            new Delete().From(TXuatthuocTheodon.Schema)
                                .Where(TXuatthuocTheodon.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                            new Update(KcbDonthuocChitiet.Schema)
                                .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(0)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).
                                Execute();

                        }
                       SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                             .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                             .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                      
                       
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;

            }

        }
        public ActionResult HuyXacNhanDonThuocBN(long Pres_ID, int id_kho,DateTime ngay_huy,string lydohuy)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        HisDuocProperties objHisDuocProperties = new HisDuocProperties();
                        objHisDuocProperties = PropertyLib._HisDuocProperties;
                        KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(Pres_ID);
                        KcbDonthuocChitietCollection lstDetail
                           = new Select().From(KcbDonthuocChitiet.Schema)
                           .Where(KcbDonthuocChitiet.IdDonthuocColumn).IsEqualTo(objDonthuoc.IdDonthuoc)
                           .And(KcbDonthuocChitiet.Columns.IdKho).IsEqualTo(id_kho)
                           .ExecuteAsCollection<KcbDonthuocChitietCollection>();
                        foreach (KcbDonthuocChitiet objDetail in lstDetail)
                        {
                            TPhieuXuatthuocBenhnhanChitietCollection objXuatBnhanCtCollection = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc)
                                .ExecuteAsCollection<TPhieuXuatthuocBenhnhanChitietCollection>();
                            
                            //Phần mới này thì mỗi detail chỉ có duy nhất 1 phieuxuatchitiet
                            foreach (TPhieuXuatthuocBenhnhanChitiet PhieuXuatBnhanCt in objXuatBnhanCtCollection)
                            {
                                //Cộng trả lại kho xuất
                                long id_Thuockho_new = -1;
                                long iTThuockho_old = PhieuXuatBnhanCt.IdThuockho.Value;
                                StoredProcedure sp = SPs.ThuocNhapkhoOutput(PhieuXuatBnhanCt.NgayHethan, PhieuXuatBnhanCt.GiaNhap, PhieuXuatBnhanCt.GiaBan,
                                                                  PhieuXuatBnhanCt.SoLuong, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.Vat),
                                                                  PhieuXuatBnhanCt.IdThuoc, PhieuXuatBnhanCt.IdKho,
                                                                  PhieuXuatBnhanCt.MaNhacungcap, PhieuXuatBnhanCt.SoLo,
                                                                  PhieuXuatBnhanCt.IdThuockho.Value, id_Thuockho_new, PhieuXuatBnhanCt.NgayNhap, PhieuXuatBnhanCt.DonGia, PhieuXuatBnhanCt.PhuthuDungtuyen, PhieuXuatBnhanCt.PhuthuTraituyen);
                                sp.Execute();
                                //Lấy đầu ra iTThuockho nếu thêm mới để update lại presdetail
                                id_Thuockho_new = Utility.Int32Dbnull(sp.OutputValues[0]);
                                ///xóa thông tin bảng chi tiết
                                new Delete().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .Execute();
                                //Xóa trong bảng biến động
                                new Delete().From(TBiendongThuoc.Schema)
                                    .Where(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(LoaiPhieu.PhieuXuatKhoBenhNhan).Execute();
                                //Cập nhật laijiTThuockho mới cho chi tiết đơn thuốc
                                if (id_Thuockho_new != -1) //Gặp trường hợp khi xuất hết thuốc thì xóa kho-->Khi hủy thì tạo ra dòng thuốc kho mới
                                {
                                    //Cập nhật tất cả các bảng liên quan
                                    new Update(KcbDonthuocChitiet.Schema)
                                    .Set(KcbDonthuocChitiet.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                    .Where(KcbDonthuocChitiet.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                    Execute();

                                    new Update(TBiendongThuoc.Schema)
                                    .Set(TBiendongThuoc.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                    .Where(TBiendongThuoc.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                    Execute();

                                    new Update(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Set(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuockho).EqualTo(id_Thuockho_new)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdThuockho).IsEqualTo(iTThuockho_old).
                                    Execute();

                                }
                            }
                            //Xóa phiếu đơn thuốc chi tiết
                            new Delete().From(TXuatthuocTheodon.Schema)
                                .Where(TXuatthuocTheodon.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                            //Update trạng thái xác nhận của chi tiết
                            new Update(KcbDonthuocChitiet.Schema)
                                .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(0)
                                .Set(KcbDonthuocChitiet.Columns.NgayXacnhan).EqualTo(DBNull.Value)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).
                                Execute();
                        }
                        //Xóa phiếu xuất bệnh nhân theo ID đơn thuốc
                        new Delete().From(TPhieuXuatthuocBenhnhan.Schema).Where(TPhieuXuatthuocBenhnhan.IdDonthuocColumn).IsEqualTo(Pres_ID).Execute();
                        //Update trạng thái xác nhận của toàn đơn thuốc-->Phần mới 100% sẽ chạy câu Update
                        SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                              .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                              .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Set(KcbDonthuoc.Columns.NgayCapphat).EqualTo(null)
                                  .Set(KcbDonthuoc.Columns.NgayXacnhan).EqualTo(null)
                                  .Set(KcbDonthuoc.Columns.NgayHuyxacnhan).EqualTo(ngay_huy)
                                  .Set(KcbDonthuoc.Columns.LydoHuyxacnhan).EqualTo(lydohuy)
                                  .Set(KcbDonthuoc.Columns.NguoiHuyxacnhan).EqualTo(globalVariables.UserName)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;
            }
        }
        public ActionResult HuyXacNhanDonThuocBN_Tutruc(KcbDonthuoc objDonthuoc, KcbDonthuocChitiet[] arrDetails)
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        HisDuocProperties objHisDuocProperties = new HisDuocProperties();
                        // if(objHisDuocProperties.KieuDuyetDonThuoc=="DONTHUOC")id_kho=Utility.Int32Dbnull(objDonthuoc.IdKho)
                        objHisDuocProperties = PropertyLib._HisDuocProperties;
                        int id_thuockho = -1;
                        foreach (KcbDonthuocChitiet objDetail in arrDetails)
                        {
                            SqlQuery sqlQuery = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieuChitiet).IsEqualTo(objDetail.IdChitietdonthuoc);
                            TPhieuXuatthuocBenhnhanChitietCollection objXuatBnhanCtCollection =
                                sqlQuery.ExecuteAsCollection<TPhieuXuatthuocBenhnhanChitietCollection>();
                            foreach (TPhieuXuatthuocBenhnhanChitiet PhieuXuatBnhanCt in objXuatBnhanCtCollection)
                            {
                                StoredProcedure sp = SPs.ThuocNhapkhoOutput(PhieuXuatBnhanCt.NgayHethan, PhieuXuatBnhanCt.DonGia, PhieuXuatBnhanCt.GiaBan,
                                                                 PhieuXuatBnhanCt.SoLuong, Utility.DecimaltoDbnull(PhieuXuatBnhanCt.Vat),
                                                                 PhieuXuatBnhanCt.IdThuoc, PhieuXuatBnhanCt.IdKho, PhieuXuatBnhanCt.MaNhacungcap,
                                                                 PhieuXuatBnhanCt.SoLo,-1,id_thuockho,
                                                                 objDonthuoc.NgayXacnhan, PhieuXuatBnhanCt.DonGia,
                                                                 PhieuXuatBnhanCt.PhuthuDungtuyen, PhieuXuatBnhanCt.PhuthuTraituyen);

                                sp.Execute();
                                ///xóa thông tin bảng chi tiết
                                new Delete().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .Execute();
                                
                                new Delete().From(TBiendongThuoc.Schema)
                                    .Where(TBiendongThuoc.Columns.IdPhieuChitiet).IsEqualTo(Utility.Int32Dbnull(PhieuXuatBnhanCt.IdPhieuChitiet))
                                    .And(TBiendongThuoc.Columns.MaLoaiphieu).IsEqualTo(3).Execute();

                                sqlQuery = new Select().From(TPhieuXuatthuocBenhnhanChitiet.Schema)
                                    .Where(TPhieuXuatthuocBenhnhanChitiet.Columns.IdPhieu).IsEqualTo(
                                        PhieuXuatBnhanCt.IdPhieu);
                                if (sqlQuery.GetRecordCount() <= 0)
                                {
                                    TPhieuXuatthuocBenhnhan.Delete(PhieuXuatBnhanCt.IdPhieu);
                                }
                            }
                            new Delete().From(TXuatthuocTheodon.Schema)
                                .Where(TXuatthuocTheodon.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).Execute();
                            new Update(KcbDonthuocChitiet.Schema)
                                .Set(KcbDonthuocChitiet.Columns.TrangThai).EqualTo(0)
                                .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objDetail.IdChitietdonthuoc).
                                Execute();

                        }
                        SqlQuery sqlQuery1 = new Select().From(KcbDonthuocChitiet.Schema)
                              .Where(KcbDonthuocChitiet.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                              .And(KcbDonthuocChitiet.Columns.TrangThai).IsEqualTo(0);
                        int status = sqlQuery1.GetRecordCount() <= 0 ? 1 : 0;
                        new Update(KcbDonthuoc.Schema)
                                  .Set(KcbDonthuoc.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                                  .Set(KcbDonthuoc.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                                  .Set(KcbDonthuoc.Columns.TrangThai).EqualTo(status)
                                  .Where(KcbDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc).Execute();


                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                log.Error("Loi trong qua trinh xac nhan don thuoc :{0}", exception);
                return ActionResult.Error;

            }

        }


        
        #endregion
    }
}
