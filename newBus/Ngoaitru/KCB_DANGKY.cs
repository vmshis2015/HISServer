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
using System.Net.NetworkInformation;
using System.Net;
using System.Collections.Generic;

namespace VNS.HIS.BusRule.Classes
{
    public class KCB_DANGKY
    {
        private NLog.Logger log;
        public KCB_DANGKY()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public DataTable KcbLaythongtinBenhnhan(long IdBenhnhan)
        {
            return SPs.KcbLaythongtinBenhnhan(IdBenhnhan).GetDataSet().Tables[0];
        }
        public DataTable KcbTiepdonTimkiemBenhnhan(string FromDate, string ToDate, int? ObjectTypeID, int? TrangThai, string TenBenhnhan, int? IdBenhnhan, string MaLuotkham,string CMT, string PHONE,  string MAKHOATHIEN,byte trangthai_noitru)
        {
            return SPs.KcbTiepdonTimkiemBenhnhan(FromDate, ToDate, ObjectTypeID, TrangThai, TenBenhnhan, IdBenhnhan, MaLuotkham, CMT, PHONE, MAKHOATHIEN, trangthai_noitru).GetDataSet().Tables[0];
        }
       
        public ActionResult InsertRegExam(KcbDangkyKcb objKcbDangkyKcb,KcbLuotkham objLuotkham, ref long v_RegId, int KieuKham)
        {

            bool b_HasLoaded = false;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                         v_RegId = AddRegExam(objKcbDangkyKcb,objLuotkham,b_HasLoaded, KieuKham);
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString);
                return ActionResult.Error;
            }

        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objKcbDangkyKcb"></param>
        /// <param name="b_HasLoaded"></param>
        /// <returns></returns>
        public long AddRegExam(KcbDangkyKcb objKcbDangkyKcb, KcbLuotkham objLuotkham, bool b_HasLoaded, int KieuKham)
        {
            long v_RegId = -1;
            decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
            try
            {
                using (var scope = new TransactionScope())
                {
                    if (objKcbDangkyKcb.SttKham == -1)
                        objKcbDangkyKcb.SttKham = THU_VIEN_CHUNG.LaySothutuKCB(Utility.Int32Dbnull(objKcbDangkyKcb.IdPhongkham, -1));
                    objKcbDangkyKcb.PtramBhyt = objLuotkham.PtramBhyt;
                    objKcbDangkyKcb.PtramBhytGoc = objLuotkham.PtramBhytGoc;
                    if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                        objKcbDangkyKcb.TuTuc = 0;
                    if (Utility.ByteDbnull(objKcbDangkyKcb.TuTuc, 0) == 1)
                    {
                        objKcbDangkyKcb.BhytChitra =0;// objKcbDangkyKcb.DonGia * Utility.DecimaltoDbnull(objLuotkham.PtramBhyt) / 100;
                        objKcbDangkyKcb.BnhanChitra = objKcbDangkyKcb.DonGia;
                    }
                    else
                    {
                        decimal BHCT = 0m;
                        if (objLuotkham.DungTuyen == 1)
                        {
                            BHCT = objKcbDangkyKcb.DonGia * Utility.DecimaltoDbnull(objLuotkham.PtramBhyt) / 100;
                        }
                        else
                        {
                            if (objLuotkham.TrangthaiNoitru <= 0)
                                BHCT = objKcbDangkyKcb.DonGia * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                            else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                                BHCT = objKcbDangkyKcb.DonGia * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                        }

                        objKcbDangkyKcb.BhytChitra =BHCT;// objKcbDangkyKcb.DonGia * Utility.DecimaltoDbnull(objLuotkham.PtramBhyt) / 100;
                        objKcbDangkyKcb.BnhanChitra = objKcbDangkyKcb.DonGia - objKcbDangkyKcb.BhytChitra;
                    }
                   
                    objKcbDangkyKcb.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                    objKcbDangkyKcb.TrangThai = 0;
                    objKcbDangkyKcb.IsNew = true;
                    objKcbDangkyKcb.Save();
                  
                    //Thêm bản ghi trong bảng phân buồng giường để tiện tính toán
                    NoitruPhanbuonggiuong _newItem = new NoitruPhanbuonggiuong();
                    _newItem.IdBenhnhan = objKcbDangkyKcb.IdBenhnhan;
                    _newItem.MaLuotkham = objKcbDangkyKcb.MaLuotkham;
                    _newItem.IdKham = (int)objKcbDangkyKcb.IdKham;
                    _newItem.IdKhoanoitru = objKcbDangkyKcb.IdKhoakcb.Value;
                    _newItem.NgayVaokhoa = objKcbDangkyKcb.NgayDangky.Value;
                    _newItem.IdBacsiChidinh = objKcbDangkyKcb.IdBacsikham;
                    _newItem.NguoiTao = objKcbDangkyKcb.NguoiTao;
                    _newItem.NgayTao = objKcbDangkyKcb.NgayDangky.Value;
                    _newItem.NoiTru =0;
                    _newItem.DuyetBhyt = 0;
                    _newItem.TrongGoi =-1;
                    _newItem.SoLuong = 1;
                    
                    _newItem.DonGia = objKcbDangkyKcb.DonGia;
                    _newItem.PhuThu = objKcbDangkyKcb.PhuThu;
                    _newItem.BnhanChitra = objKcbDangkyKcb.BnhanChitra;
                    _newItem.BhytChitra = objKcbDangkyKcb.BhytChitra;
                    _newItem.TenHienthi = objKcbDangkyKcb.TenDichvuKcb;
                    _newItem.TuTuc = objKcbDangkyKcb.TuTuc;
                    _newItem.TrangthaiXacnhan = 0;
                    _newItem.GiaGoc = objKcbDangkyKcb.DonGia + objKcbDangkyKcb.PhuThu;
                    _newItem.IdBuong = -1;
                    _newItem.IdGiuong = -1;
                    _newItem.IdChuyen = -1;
                    _newItem.IdNhanvienPhangiuong = -1;
                    _newItem.IsNew = true;
                    _newItem.Save();

                    v_RegId = Utility.Int32Dbnull(objKcbDangkyKcb.IdKham);
                    if (objKcbDangkyKcb.IdKham > 0)
                    {
                        KieuKham = Utility.Int32Dbnull(objKcbDangkyKcb.IdDichvuKcb);
                        long _regid = objKcbDangkyKcb.IdKham;
                        //Lấy phí kèm theo trong bảng Quan hệ kiểu khám và đẩy vào bảng T_RegExam
                        //THEM_PHI_DVU_KYC(objLuotkham,objKcbDangkyKcb,  KieuKham);
                        //Lấy phí kèm theo trong bảng DmucPhikemtheoCollection
                        //(cấu hình theo từng phòng khám thay vì theo từng kiểu khám) và đẩy vào bảng T_RegExam
                        THEM_PHI_DVU_KYC(objLuotkham, objKcbDangkyKcb);
                        //Lấy phí dịch vụ trong bảng Quan hệ kiểu khám và đẩy vào bảng CLS
                        //THEM_PHI_DVU_KYC(objLuotkham, KieuKham);


                    }
                    scope.Complete();
                }

            }
            catch (Exception ex)
            {

                log.Error(ex.Message);
            }
            return v_RegId;
        }

      

        public void THEM_PHI_DVU_KYC(KcbLuotkham objLuotkham,KcbDangkyKcb objKcbDangkyKcb, int KieuKham)
        {
            using (var scope = new TransactionScope())
            {
                DmucDichvukcb objDepartDoctorRelation = DmucDichvukcb.FetchByID(KieuKham);
                if (objDepartDoctorRelation != null)
                {
                    if (Utility.Int32Dbnull(objDepartDoctorRelation.IdPhikemtheo, -1) > 0)
                    {

                        SqlQuery sql = new Select().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.MaLuotkham).
                            IsEqualTo(objLuotkham.MaLuotkham)
                            .And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(
                                KcbDangkyKcb.Columns.LaPhidichvukemtheo).IsEqualTo(1)
                                .And(KcbDangkyKcb.Columns.IdCha).IsEqualTo(objKcbDangkyKcb.IdKham);
                        if (sql.GetRecordCount() > 0)
                        {
                            return;
                        }
                        int IdDv = -1;
                        //Mã ưu tiên của một số đối tượng BHYT ko cần trả phí dịch vụ kèm theo(hiện tại là có mã quyền lợi 1,2,3)
                        string[] maUuTienKkb = globalVariables.gv_strMaUutien.Split(',');

                        if (globalVariables.MA_KHOA_THIEN != "KYC")
                        {
                            if (THU_VIEN_CHUNG.IsNgoaiGio())
                            {
                                IdDv = Utility.Int32Dbnull(objDepartDoctorRelation.IdPhikemtheongoaigio, -1);
                            }
                            else//Khám trong giờ cần xét đối tượng ưu tiên
                            {
                                //var query= from loz in Ma_UuTien.
                                if (!maUuTienKkb.Contains(Utility.sDbnull(objLuotkham.MaQuyenloi, "0")))
                                {
                                    IdDv = Utility.Int32Dbnull(objDepartDoctorRelation.IdPhikemtheo, -1);
                                }
                                else
                                {
                                    IdDv = -1;
                                }
                            }
                        }
                        else//Khám yêu cầu thì luôn bị tính phí dịch vụ kèm theo
                        {
                            IdDv = Utility.Int32Dbnull(objDepartDoctorRelation.IdPhikemtheo, -1);
                        }
                        DmucDichvuclsChitiet lServiceDetail = DmucDichvuclsChitiet.FetchByID(IdDv);
                        long reg_id = objKcbDangkyKcb.IdKham;
                        if (lServiceDetail != null)
                        {
                            objKcbDangkyKcb.DonGia = lServiceDetail.DonGia.Value;
                            objKcbDangkyKcb.PhuThu = 0;
                            objKcbDangkyKcb.BhytChitra = 0;
                            objKcbDangkyKcb.BnhanChitra = lServiceDetail.DonGia;
                            objKcbDangkyKcb.IdCha = reg_id;
                            objKcbDangkyKcb.TrangThai = 0;
                            objKcbDangkyKcb.SttKham = -1;
                            objKcbDangkyKcb.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                            objKcbDangkyKcb.TenDichvuKcb = "Phí dịch vụ kèm theo";
                            objKcbDangkyKcb.TuTuc = 0;
                            objKcbDangkyKcb.KhamNgoaigio = 0;
                            objKcbDangkyKcb.LaPhidichvukemtheo = 1;
                            objKcbDangkyKcb.IsNew = true;
                            objKcbDangkyKcb.Save();
                        }
                    }
                }
                scope.Complete();
            }
        }
        public void THEM_PHI_DVU_KYC(KcbLuotkham objLuotkham, KcbDangkyKcb objKcbDangkyKcb)
        {
            using (var scope = new TransactionScope())
            {
                DmucPhikemtheoCollection lstPhikemtheo = new Select().From(DmucPhikemtheo.Schema).Where(DmucPhikemtheo.IdKhoakcbColumn).IsEqualTo(objKcbDangkyKcb.IdKhoakcb).ExecuteAsCollection<DmucPhikemtheoCollection>();
                if (lstPhikemtheo != null && lstPhikemtheo.Count > 0)
                {
                    if (Utility.Int32Dbnull(lstPhikemtheo[0].IdPhikemtheo, -1) > 0)
                    {

                        SqlQuery sql = new Select().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.MaLuotkham).
                            IsEqualTo(objLuotkham.MaLuotkham)
                            .And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(
                                KcbDangkyKcb.Columns.LaPhidichvukemtheo).IsEqualTo(1)
                                .And(KcbDangkyKcb.Columns.IdCha).IsEqualTo(objKcbDangkyKcb.IdKham);
                        if (sql.GetRecordCount() > 0)//Chỉ được 1 lần phí dịch vụ kèm theo
                        {
                            return;
                        }
                        int IdDv = -1;
                        //Mã ưu tiên của một số đối tượng BHYT ko cần trả phí dịch vụ kèm theo(hiện tại là có mã quyền lợi 1,2,3)
                        string[] maUuTienKkb = globalVariables.gv_strMaUutien.Split(',');

                        if (globalVariables.MA_KHOA_THIEN != "KYC")
                        {
                            if (THU_VIEN_CHUNG.IsNgoaiGio())
                            {
                                IdDv = Utility.Int32Dbnull(lstPhikemtheo[0].IdPhikemtheongoaigio, -1);
                            }
                            else//Khám trong giờ cần xét đối tượng ưu tiên
                            {
                                //var query= from loz in Ma_UuTien.
                                if (!maUuTienKkb.Contains(Utility.sDbnull(objLuotkham.MaQuyenloi, "0")))
                                {
                                    IdDv = Utility.Int32Dbnull(lstPhikemtheo[0].IdPhikemtheo, -1);
                                }
                                else
                                {
                                    IdDv = -1;
                                }
                            }
                        }
                        else//Khám yêu cầu thì luôn bị tính phí dịch vụ kèm theo
                        {
                            IdDv = Utility.Int32Dbnull(lstPhikemtheo[0].IdPhikemtheo, -1);
                        }
                        DmucDichvuclsChitiet lServiceDetail = DmucDichvuclsChitiet.FetchByID(IdDv);
                        long reg_id = objKcbDangkyKcb.IdKham;
                        if (lServiceDetail != null)
                        {
                            objKcbDangkyKcb.DonGia = lServiceDetail.DonGia.Value;
                            objKcbDangkyKcb.PhuThu = 0;
                            objKcbDangkyKcb.BhytChitra = 0;
                            objKcbDangkyKcb.BnhanChitra = lServiceDetail.DonGia;
                            objKcbDangkyKcb.IdCha = reg_id;
                            objKcbDangkyKcb.TrangThai = 0;
                            objKcbDangkyKcb.SttKham = -1;
                            objKcbDangkyKcb.TenDichvuKcb = "Phí dịch vụ kèm theo";
                            objKcbDangkyKcb.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                            objKcbDangkyKcb.TuTuc = 0;
                            objKcbDangkyKcb.KhamNgoaigio = 0;
                            objKcbDangkyKcb.LaPhidichvukemtheo = 1;
                            objKcbDangkyKcb.IsNew = true;
                            objKcbDangkyKcb.Save();
                        }
                    }
                }
                scope.Complete();

            }
        }
       
        public ActionResult PerformActionDeleteRegExam(int IdKham)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        KcbDangkyKcb objKcbDangkyKcb = KcbDangkyKcb.FetchByID(IdKham);

                        if (objKcbDangkyKcb != null)
                        {
                            new Delete().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objKcbDangkyKcb.IdKham)
                                .Or(KcbDangkyKcb.Columns.IdCha).IsEqualTo(objKcbDangkyKcb.IdKham).Execute();
                           

                            new Delete().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdKham).IsEqualTo(
                                objKcbDangkyKcb.IdKham).Execute();
                            new Delete().From(NoitruPhanbuonggiuong.Schema).Where(NoitruPhanbuonggiuong.Columns.IdKham).IsEqualTo(
                               objKcbDangkyKcb.IdKham).Execute();
                            new Delete().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(
                               objKcbDangkyKcb.IdKham).Execute();
                            new Delete().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdKham).IsEqualTo(
                               objKcbDangkyKcb.IdKham).Execute();
                            new Delete().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(
                               objKcbDangkyKcb.IdKham).Execute();
                            new Delete().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(
                               objKcbDangkyKcb.IdKham).Execute();

                            KcbDangkyKcbCollection lstKham=new Select().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objKcbDangkyKcb.IdBenhnhan)
                                .And(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objKcbDangkyKcb.MaLuotkham).ExecuteAsCollection<KcbDangkyKcbCollection>();
                            if (lstKham.Count <= 0)
                            {
                                KcbLuotkham objluotkham=new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objKcbDangkyKcb.IdBenhnhan)
                                .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objKcbDangkyKcb.MaLuotkham).ExecuteSingle<KcbLuotkham>();
                                objluotkham.IdKhoanoitru = -1;
                                objluotkham.IdBuong = -1;
                                objluotkham.IdGiuong = -1;
                                objluotkham.IdNhapvien = -1;
                                objluotkham.IdRavien = -1;
                                objluotkham.TrangthaiNoitru = 0;
                                objluotkham.TrangthaiNgoaitru = 0;
                                objluotkham.TthaiChuyendi = 0;
                                objluotkham.Locked = 0;
                                objluotkham.MabenhChinh = "";
                                objluotkham.MabenhPhu = "";
                                objluotkham.LydoKetthuc = "";
                                objluotkham.IdBenhvienDi = -1;
                                objluotkham.MotaNhapvien = "";
                                objluotkham.MarkOld();
                                objluotkham.IsNew = false;
                                objluotkham.Save();
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

        public ActionResult PerformActionDeletePatientExam(string v_MaLuotkham, int v_Patient_ID)
        {
            int record = -1;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var db = new SharedDbConnectionScope())
                    {
                        //LẤY THÔNG TIN CHỈ ĐỊNH DỊCH VỤ CỦA LẦN KHÁM
                        KcbChidinhclCollection objAssignInfo =
                            new KcbChidinhclController().FetchByQuery(
                                KcbChidinhcl.CreateQuery().AddWhere(KcbChidinhcl.Columns.MaLuotkham, Comparison.Equals,
                                                                   v_MaLuotkham));
                        //LẤY THÔNG TIN CHỈ ĐỊNH THUỐC CỦA LẦN KHÁM
                        KcbDonthuocCollection prescriptionCollection =
                            new KcbDonthuocController().FetchByQuery(
                                KcbDonthuoc.CreateQuery().AddWhere(KcbDonthuoc.Columns.MaLuotkham,
                                                                     Comparison.Equals, v_MaLuotkham));
                        //KIẾM TRA NẾU CÓ THÔNG TIN CHỈ ĐỊNH DV HOẶC THUỐC THÌ KHÔNG ĐC PHÉP XÓA
                        if (prescriptionCollection.Count > 0 || objAssignInfo.Count > 0)
                            return ActionResult.Exception;
                       
                        
                        // XÓA chi định tự động
                        new Delete().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(
                            v_MaLuotkham)
                            .And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID).Execute();

                        
                        //XÓA THÔNG TIN ĐĂNG KÝ KHÁM
                        new Delete().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                            .And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID)
                            .Execute();

                        new Delete().From(NoitruPhanbuonggiuong.Schema).Where(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                            .And(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID)
                            .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(0)
                           .Execute();
                       
                        //LẤY VỀ CÁC THÔNG TIN LẦN KHÁM CỦA BỆNH NHÂN
                        KcbLuotkhamCollection tPatientExamCollection =
                            new KcbLuotkhamController().FetchByQuery(
                                KcbLuotkham.CreateQuery().AddWhere(KcbLuotkham.Columns.IdBenhnhan, Comparison.Equals,
                                                                    v_Patient_ID));

                        //XÓA LẦN ĐĂNG KÝ KHÁM CỦA BỆNH NHÂN
                        new Delete().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(
                            v_MaLuotkham).Execute();
                        //Cập nhật lại mã lượt khám để có thể dùng cho bệnh nhân khác
                        new Update(KcbDmucLuotkham.Schema)
                                  .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(0)
                                  .Set(KcbDmucLuotkham.Columns.UsedBy).EqualTo(DBNull.Value)
                                  .Set(KcbDmucLuotkham.Columns.StartTime).EqualTo(DBNull.Value)
                                  .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(null)
                                  .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                                  .And(KcbDmucLuotkham.Columns.TrangThai).IsEqualTo(2)
                                  .Execute();
                        ;
                        //KIỂM TRA NẾU BỆNH NHÂN CÓ >1 LẦN KHÁM THÌ CHỈ XÓA LẦN ĐĂNG KÝ ĐANG CHỌN. NẾU <= 1 LẦN KHÁM THÌ XÓA LUÔN THÔNG TIN BỆNH NHÂN
                        if (tPatientExamCollection.Count < 2)
                        {
                            new Delete().From(KcbDanhsachBenhnhan.Schema).Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(
                               v_Patient_ID).Execute();
                        }

                    }
                    scope.Complete();
                    return ActionResult.Success;

                }
            }
            catch (Exception ex)
            {
                return ActionResult.Error;
            }
        }
        private decimal SumOfPaymentDetail_NGOAITRU(KcbThanhtoanChitiet[] objArrPaymentDetail)
        {
            decimal SumOfPaymentDetail = 0;
            var sum = (from loz in objArrPaymentDetail.AsEnumerable()
                       where loz.TuTuc == 0
                       select loz).Sum(c => c.DonGia * c.SoLuong);
            //.Sum(c=>c.SoLuong*c.DonGia))
            foreach (KcbThanhtoanChitiet paymentDetail in objArrPaymentDetail)
            {
                if (paymentDetail.TuTuc == 0)
                    SumOfPaymentDetail += (Utility.Int32Dbnull(paymentDetail.SoLuong) *
                                           Utility.DecimaltoDbnull(paymentDetail.DonGia));


            }
            return SumOfPaymentDetail;
        }
        public decimal LayThongPtramBHYT(decimal v_decTotalPrice, KcbLuotkham objLuotkham, ref  decimal PtramBHYT)
        {
            decimal decDiscountTotalMoney = 0;
            SqlQuery q;
            if (!string.IsNullOrEmpty(objLuotkham.MaKcbbd) && THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))
            {
                ///Kiểm tra xem đối tượng BHYT là đúng tuyến?
                if (objLuotkham.DungTuyen == 1)
                {
                    //Đối tượng mã quyền lợi 1,2 được hưởng 100%
                    if (objLuotkham.MaQuyenloi.ToString() == "1" || objLuotkham.MaQuyenloi.ToString() == "2")
                    {
                        decDiscountTotalMoney = 0;
                        PtramBHYT = 100;
                    }
                    else
                    {
                        switch (globalVariables.gv_strTuyenBHYT)
                        {
                            case "TUYEN1"://Kiểm tra so với >15% lương cơ bản
                                if (v_decTotalPrice >= objLuotkham.LuongCoban * 15 / 100)
                                {
                                    
                                    q = new Select().From(DmucDoituongbhyt.Schema)
                                       .Where(DmucDoituongbhyt.Columns.IdDoituongKcb).IsEqualTo(objLuotkham.IdDoituongKcb)
                                       .And(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(objLuotkham.MaDoituongBhyt);
                                    DmucDoituongbhyt objInsuranceObject = q.ExecuteSingle<DmucDoituongbhyt>();
                                    if (objInsuranceObject != null)
                                    {
                                        PtramBHYT = Utility.DecimaltoDbnull(objInsuranceObject.PhantramBhyt, 0);
                                        decDiscountTotalMoney = v_decTotalPrice * (100 - Utility.DecimaltoDbnull(objInsuranceObject.PhantramBhyt, 0)) / 100;
                                        
                                    }

                                }
                                else//<15% lương cơ bản-->BHYT trả hết
                                {

                                    PtramBHYT = 100;
                                    decDiscountTotalMoney = 0;
                                }
                                break;
                            case "TW"://Tuyến trung ương ko cần so sánh với lương cơ bản
                               
                                q = new Select().From(DmucDoituongbhyt.Schema)
                                   .Where(DmucDoituongbhyt.Columns.IdDoituongKcb).IsEqualTo(objLuotkham.IdDoituongKcb)
                                   .And(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(objLuotkham.MaDoituongBhyt);
                                DmucDoituongbhyt objInsuranceObjectTW = q.ExecuteSingle<DmucDoituongbhyt>();
                                if (objInsuranceObjectTW != null)
                                {
                                    PtramBHYT = Utility.DecimaltoDbnull(objInsuranceObjectTW.PhantramBhyt, 0);
                                    decDiscountTotalMoney = v_decTotalPrice * (100 - Utility.DecimaltoDbnull(objInsuranceObjectTW.PhantramBhyt, 0)) / 100;
                                }
                                break;
                            default://Các tuyến khác-->Mặc định giống tuyến 1
                                if (v_decTotalPrice >= objLuotkham.LuongCoban * 15 / 100)
                                {
                                    q = new Select().From(DmucDoituongbhyt.Schema)
                                       .Where(DmucDoituongbhyt.Columns.IdDoituongKcb).IsEqualTo(objLuotkham.IdDoituongKcb)
                                       .And(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(objLuotkham.MaDoituongBhyt);
                                    DmucDoituongbhyt objInsuranceObject = q.ExecuteSingle<DmucDoituongbhyt>();
                                    if (objInsuranceObject != null)
                                    {
                                        PtramBHYT = Utility.DecimaltoDbnull(objInsuranceObject.PhantramBhyt, 0);
                                        decDiscountTotalMoney = v_decTotalPrice * (100 - Utility.DecimaltoDbnull(objInsuranceObject.PhantramBhyt, 0)) / 100;
                                    }
                                }
                                else
                                {

                                    PtramBHYT = 100;
                                    decDiscountTotalMoney = 0;
                                }
                                break;

                        }


                    }


                }
                else
                {
                    ///Nếu là đối tượng trái tuyến thực hiện lấy % của trái tuyến
                    DmucDoituongkcb objObjectType = DmucDoituongkcb.FetchByID(objLuotkham.IdDoituongKcb);
                    if (objObjectType != null)
                    {
                        decDiscountTotalMoney = v_decTotalPrice * (100 - Utility.DecimaltoDbnull(objObjectType.PhantramTraituyen)) / 100;
                        PtramBHYT = Utility.DecimaltoDbnull(objObjectType.PhantramTraituyen, 0);

                    }

                }


            }
            else//Đối tượng dịch vụ
            {
                //Có thể gán luôn PtramBHYT=0% và decDiscountTotalMoney=0
                DmucDoituongkcb objObjectType = DmucDoituongkcb.FetchByID(objLuotkham.IdDoituongKcb);
                if (objObjectType != null)
                    decDiscountTotalMoney = v_decTotalPrice * (100 - Utility.Int32Dbnull(objObjectType.PhantramDungtuyen, 0)) / 100; ;
                PtramBHYT = Utility.DecimaltoDbnull(objObjectType.PhantramDungtuyen, 0);

            }
            return decDiscountTotalMoney;
        }
        
        public void XuLyChiKhauDacBietBHYT(KcbLuotkham objLuotkham, decimal v_DiscountRate)
        {
            KcbThanhtoanCollection paymentCollection =
                new KcbThanhtoanController().FetchByQuery(
                    KcbThanhtoan.CreateQuery().AddWhere(KcbThanhtoan.Columns.MaLuotkham, Comparison.Equals,
                                                    objLuotkham.MaLuotkham).AND(KcbThanhtoan.Columns.IdBenhnhan,
                                                                                    Comparison.Equals,
                                                                                    objLuotkham.IdBenhnhan));
            foreach (KcbThanhtoan payment in paymentCollection)
            {
                KcbThanhtoanChitietCollection paymentDetailCollection =
                                new KcbThanhtoanChitietController().FetchByQuery(
                                    KcbThanhtoanChitiet.CreateQuery().AddWhere(KcbThanhtoanChitiet.Columns.IdThanhtoan,
                                                                          Comparison.Equals, payment.IdThanhtoan).AND(
                                                                              KcbThanhtoanChitiet.Columns.TuTuc,
                                                                              Comparison.Equals, 0));
                string IsDungTuyen = "DT";
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objLuotkham.IdDoituongKcb);
                if (objectType != null)
                {
                    switch (objectType.MaDoituongKcb)
                    {
                        case "BHYT":
                            if (Utility.Int32Dbnull(objLuotkham.DungTuyen, "0") == 1) IsDungTuyen = "DT";
                            else
                            {
                                IsDungTuyen = "TT";
                            }
                            break;
                        default:
                            IsDungTuyen = "KHAC";
                            break;
                    }

                }
                foreach (KcbThanhtoanChitiet PaymentDetail in paymentDetailCollection)
                {
                    SqlQuery sqlQuery = new Select().From(DmucBhytChitraDacbiet.Schema)
                     .Where(DmucBhytChitraDacbiet.Columns.IdDichvuChitiet).IsEqualTo(PaymentDetail.IdChitietdichvu)
                     .And(DmucBhytChitraDacbiet.Columns.MaLoaithanhtoan).IsEqualTo(PaymentDetail.IdLoaithanhtoan)
                     .And(DmucBhytChitraDacbiet.Columns.DungtuyenTraituyen).IsEqualTo(IsDungTuyen)
                     .And(DmucBhytChitraDacbiet.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb);
                    DmucBhytChitraDacbiet objDetailDiscountRate = sqlQuery.ExecuteSingle<DmucBhytChitraDacbiet>();
                    if (objDetailDiscountRate != null)
                    {
                        log.Info("Neu trong ton tai trong bang cau hinh chi tiet chiet khau void Id_Chitiet=" + PaymentDetail.IdChitiet);
                        PaymentDetail.PtramBhyt = objDetailDiscountRate.TileGiam;
                        PaymentDetail.BhytChitra = THU_VIEN_CHUNG.TinhBhytChitra(objDetailDiscountRate.TileGiam,
                                                      Utility.DecimaltoDbnull(
                                                          PaymentDetail.DonGia, 0));
                        PaymentDetail.BnhanChitra = THU_VIEN_CHUNG.TinhBnhanChitra(objDetailDiscountRate.TileGiam,
                                                                 Utility.DecimaltoDbnull(
                                                                     PaymentDetail.DonGia, 0));
                    }
                    else
                    {
                        PaymentDetail.PtramBhyt = v_DiscountRate;
                        PaymentDetail.BhytChitra = THU_VIEN_CHUNG.TinhBhytChitra(v_DiscountRate,
                                                       Utility.DecimaltoDbnull(
                                                           PaymentDetail.DonGia, 0));
                        PaymentDetail.BnhanChitra = THU_VIEN_CHUNG.TinhBnhanChitra(v_DiscountRate,
                                                                 Utility.DecimaltoDbnull(
                                                                     PaymentDetail.DonGia, 0));
                    }
                    log.Info("Thuc hien viec cap nhap thong tin lai gia can phai xem lại gia truoc khi thanh toan");




                }

            }

        }
        /// <summary>
        /// HÀM THỰC HIỆN VIỆC LẤY THÔNG TIN CHIÊT KHẤU
        /// </summary>
        /// <returns></returns>
        private string LayChiKhauChiTiet()
        {
            string PTramChiTiet = "KHONG";
            SqlQuery sqlQuery = new Select().From(SysSystemParameter.Schema)
                .Where(SysSystemParameter.Columns.SName).IsEqualTo("PTRAM_CHITIET");
            SysSystemParameter objSystemParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
            if (objSystemParameter != null) PTramChiTiet = objSystemParameter.SValue;
            return PTramChiTiet;
        }
      
        private void UpdateTrangThaiBangChucNang(KcbThanhtoan objPayment, KcbThanhtoanChitiet objPaymentDetail)
        {
            using (var scope = new TransactionScope())
            {
                switch (objPaymentDetail.IdLoaithanhtoan)
                {
                    case 1:
                        new Update(KcbDangkyKcb.Schema)
                            .Set(KcbDangkyKcb.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                            .Set(KcbDangkyKcb.Columns.TrangthaiThanhtoan).EqualTo(1)
                            .Set(KcbDangkyKcb.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                            .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objPaymentDetail.IdPhieu).Execute();
                        break;
                    case 2:
                        new Update(KcbChidinhclsChitiet.Schema)
                            .Set(KcbChidinhclsChitiet.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                            .Set(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
                            .Set(KcbChidinhclsChitiet.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(KcbChidinhclsChitiet.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(KcbChidinhclsChitiet.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(objPaymentDetail.IdChitietdichvu).Execute();
                        break;
                    case 3:
                        new Update(KcbDonthuocChitiet.Schema)
                            .Set(KcbDonthuocChitiet.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                            .Set(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
                            .Set(KcbDonthuocChitiet.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objPaymentDetail.IdChitietdichvu).Execute();
                        break;
                    case 5:
                        new Update(KcbDonthuocChitiet.Schema)
                            .Set(KcbDonthuocChitiet.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                            .Set(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
                            .Set(KcbDonthuocChitiet.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                            .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).IsEqualTo(objPaymentDetail.IdChitietdichvu).Execute();
                        break;
                    case 4:
                        //new Update(TPatientDept.Schema)
                        //    .Set(TPatientDept.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                        //    .Set(TPatientDept.Columns.TinhtrangThanhtoan).EqualTo(1)
                        //    .Set(TPatientDept.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                        //    .Where(TPatientDept.Columns.PatientDeptId).IsEqualTo(objPaymentDetail.Id).Execute();
                        break;
                    case 0:
                        new Update(KcbChidinhclsChitiet.Schema)
                            .Set(KcbChidinhclsChitiet.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                            .Set(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).EqualTo(1)
                            .Set(KcbChidinhclsChitiet.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                            .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(objPaymentDetail.IdChitietdichvu)
                            .Execute();
                        new Update(KcbDangkyKcb.Schema)
                           .Set(KcbDangkyKcb.Columns.IdThanhtoan).EqualTo(objPayment.IdThanhtoan)
                           .Set(KcbDangkyKcb.Columns.TrangthaiThanhtoan).EqualTo(1)
                           .Set(KcbDangkyKcb.Columns.NgayThanhtoan).EqualTo(objPayment.NgayThanhtoan)
                           .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objPaymentDetail.IdPhieu)
                           .And(KcbDangkyKcb.Columns.LaPhidichvukemtheo).IsEqualTo(1)
                           .Execute();
                        break;
                }
                scope.Complete();
            }
        }
        public DataTable LayChiDinhCLS_KhongKham(string MaLuotkham, int IdBenhnhan, int ExamID)
        {
            return SPs.KcbTiepdonLaychidinhclsKhongquaphongkham(MaLuotkham, IdBenhnhan, 200).GetDataSet().Tables[0];
        }
        public DataTable LayDsachDvuKCB(string MaLuotkham, long IdBenhnhan)
        {
            return SPs.KcbLaydanhsachdangkyphongkhamTheoIdCode(MaLuotkham, IdBenhnhan).GetDataSet().Tables[0];
        }
       
        /// <summary>
        /// Creates an object wrapper for the LAOKHOA_INPHIEU_KHAMBENH Procedure
        /// </summary>
        public  DataTable LayThongtinInphieuKCB(int RegID)
        {
            return SPs.KcbTiepdonInphieukcb(RegID).GetDataSet().Tables[0];
        }
        public DataTable LayDsachBnhanChoKham()
        {
            DataTable dataTable = new DataTable();

            dataTable = SPs.KcbTiepdonLaydsachBnhanchokham(globalVariables.SysDate,globalVariables.MA_KHOA_THIEN).GetDataSet().Tables[0];
            return dataTable;
        }
        private void UpdatePatientInfo(KcbDanhsachBenhnhan objKcbDanhsachBenhnhan)
        {
            using (var scope = new TransactionScope())
            {
                new Update(KcbDanhsachBenhnhan.Schema)
                    .Set(KcbDanhsachBenhnhan.Columns.TenBenhnhan).EqualTo(objKcbDanhsachBenhnhan.TenBenhnhan)
                    .Set(KcbDanhsachBenhnhan.Columns.GioiTinh).EqualTo(objKcbDanhsachBenhnhan.GioiTinh)
                    .Set(KcbDanhsachBenhnhan.Columns.IdGioitinh).EqualTo(objKcbDanhsachBenhnhan.IdGioitinh)
                    .Set(KcbDanhsachBenhnhan.Columns.DiachiBhyt).EqualTo(objKcbDanhsachBenhnhan.DiachiBhyt)
                    .Set(KcbDanhsachBenhnhan.Columns.DiaChi).EqualTo(objKcbDanhsachBenhnhan.DiaChi)
                    .Set(KcbDanhsachBenhnhan.Columns.NamSinh).EqualTo(objKcbDanhsachBenhnhan.NamSinh)
                    .Set(KcbDanhsachBenhnhan.Columns.NgheNghiep).EqualTo(objKcbDanhsachBenhnhan.NgheNghiep)
                    .Set(KcbDanhsachBenhnhan.Columns.Email).EqualTo(objKcbDanhsachBenhnhan.Email)
                    .Set(KcbDanhsachBenhnhan.Columns.MaQuocgia).EqualTo(objKcbDanhsachBenhnhan.MaQuocgia)
                    .Set(KcbDanhsachBenhnhan.Columns.MaTinhThanhpho).EqualTo(objKcbDanhsachBenhnhan.MaTinhThanhpho)
                    .Set(KcbDanhsachBenhnhan.Columns.MaQuanhuyen).EqualTo(objKcbDanhsachBenhnhan.MaQuanhuyen)
                    .Set(KcbDanhsachBenhnhan.Columns.DienThoai).EqualTo(objKcbDanhsachBenhnhan.DienThoai)
                    .Set(KcbDanhsachBenhnhan.Columns.CoQuan).EqualTo(objKcbDanhsachBenhnhan.CoQuan)
                    .Set(KcbDanhsachBenhnhan.Columns.NgaySinh).EqualTo(objKcbDanhsachBenhnhan.NgaySinh)
                    .Set(KcbDanhsachBenhnhan.Columns.Cmt).EqualTo(objKcbDanhsachBenhnhan.Cmt)
                    .Set(KcbDanhsachBenhnhan.Columns.NgayTiepdon).EqualTo(objKcbDanhsachBenhnhan.NgayTiepdon)
                    .Set(KcbDanhsachBenhnhan.Columns.NgaySua).EqualTo(objKcbDanhsachBenhnhan.NgaySua)
                    .Set(KcbDanhsachBenhnhan.Columns.NguoiSua).EqualTo(objKcbDanhsachBenhnhan.NguoiSua)
                    .Set(KcbDanhsachBenhnhan.Columns.DanToc).EqualTo(objKcbDanhsachBenhnhan.DanToc)
                    //.Set(KcbDanhsachBenhnhan.Columns.IpMaySua).EqualTo(globalVariables.IpAddress)
                    .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(objKcbDanhsachBenhnhan.IdBenhnhan).Execute();
                scope.Complete();
            }

        }
        public ActionResult ThemmoiLuotkham(KcbDanhsachBenhnhan objKcbDanhsachBenhnhan, KcbLuotkham objLuotkham, KcbDangkyKcb objKcbDangkyKcb, KcbDangkySokham objSoKCB, int KieuKham, ref long id_kham, ref string Msg)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        UpdatePatientInfo(objKcbDanhsachBenhnhan);
                        SqlQuery sqlQueryPatientExam = new Select().From(KcbLuotkham.Schema)
                           .Where(KcbLuotkham.Columns.IdBenhnhan).IsNotEqualTo(objLuotkham.IdBenhnhan)
                           .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham);
                        if (sqlQueryPatientExam.GetRecordCount() > 0)//Nếu BN khác đã lấy mã này
                        {

                            objLuotkham.MaLuotkham = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM();

                        }
                        objLuotkham.IsNew = true;
                        objLuotkham.Save();
                        
                        new Update(KcbDmucLuotkham.Schema)
                       .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(2)
                       .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(DateTime.Now)
                       .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .And(KcbDmucLuotkham.Columns.TrangThai).IsLessThanOrEqualTo(1)
                       .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year)
                       .And(KcbDmucLuotkham.Columns.UsedBy).IsLessThanOrEqualTo(globalVariables.UserName)
                       .Execute();
                       ;
                       if (objSoKCB != null)
                       {
                           //Kiểm tra xem có sổ KCB hay chưa
                           objSoKCB.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                           objSoKCB.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                           KcbDangkySokham _temp = new Select().From(KcbDangkySokham.Schema).Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                               .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                               .ExecuteSingle<KcbDangkySokham>();
                           if (_temp == null)
                           {
                               objSoKCB.NgayTao = globalVariables.SysDate;
                               objSoKCB.NguoiTao = globalVariables.UserName;
                               objSoKCB.IsNew = true;
                               objSoKCB.Save();
                           }
                           else
                           {
                               if (Utility.Int64Dbnull(_temp.IdThanhtoan, 0) > 0)//Ko làm gì cả
                               {
                                   Msg = "Đã thu tiền sổ khám của Bệnh nhân nên không được phép xóa hoặc cập nhật lại";
                               }
                               else//Update lại sổ KCB
                               {
                                   _temp.DonGia = objSoKCB.DonGia;
                                   _temp.BnhanChitra = objSoKCB.BnhanChitra;
                                   _temp.BhytChitra = objSoKCB.BhytChitra;
                                   _temp.PtramBhyt = objSoKCB.PtramBhyt;
                                   _temp.PtramBhytGoc = objSoKCB.PtramBhytGoc;
                                   _temp.PhuThu = objSoKCB.PhuThu;
                                   _temp.TuTuc = objSoKCB.TuTuc;
                                   _temp.NguonThanhtoan = objSoKCB.NguonThanhtoan;
                                   _temp.IdLoaidoituongkcb = objSoKCB.IdLoaidoituongkcb;
                                   _temp.IdDoituongkcb = objSoKCB.IdDoituongkcb;
                                   _temp.MaDoituongkcb = objSoKCB.MaDoituongkcb;
                                   _temp.Noitru = objSoKCB.Noitru;
                                   _temp.IdGoi = objSoKCB.IdGoi;
                                   _temp.TrongGoi = objSoKCB.TrongGoi;
                                   _temp.IdNhanvien = objSoKCB.IdNhanvien;
                                   _temp.NgaySua = globalVariables.SysDate;
                                   _temp.NguoiSua = globalVariables.UserName;
                                   _temp.IsNew = false;
                                   _temp.MarkOld();
                                   _temp.Save();
                               }
                           }
                       }
                       else
                       {
                           new Delete().From(KcbDangkySokham.Schema).Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                                          .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                                          .And(KcbDangkySokham.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                                          .Execute();
                       }
                        if (objKcbDangkyKcb != null)
                        {
                            objKcbDangkyKcb.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objKcbDangkyKcb.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                           id_kham= AddRegExam(objKcbDangkyKcb,objLuotkham, false, KieuKham);
                        }
                        scope.Complete();
                        return ActionResult.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                return ActionResult.Error;
            }
        }
        /// <summary>
        /// HAM THUC HIEN HIEN THEM LAN KHAM CUA BENH NHAN
        /// </summary>
        /// <param name="objKcbDanhsachBenhnhan"></param>
        /// <param name="objLuotkham"></param>
        /// <returns></returns>
        public ActionResult ThemmoiLuotkham(KcbDanhsachBenhnhan objKcbDanhsachBenhnhan, KcbLuotkham objLuotkham,  ref string MaLuotkham)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        UpdatePatientInfo(objKcbDanhsachBenhnhan);
                        SqlQuery sqlQueryPatientExam = new Select().From(KcbLuotkham.Schema)
                           .Where(KcbLuotkham.Columns.IdBenhnhan).IsNotEqualTo(objLuotkham.IdBenhnhan)
                           .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham);
                        if (sqlQueryPatientExam.GetRecordCount() > 0)
                        {

                            objLuotkham.MaLuotkham = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM();

                        }
                        objLuotkham.IsNew = true;
                        objLuotkham.Save();
                        new Update(KcbDmucLuotkham.Schema)
                      .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(2)
                      .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(DateTime.Now)
                      .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                      .And(KcbDmucLuotkham.Columns.TrangThai).IsLessThanOrEqualTo(1)
                      .And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year)
                      .And(KcbDmucLuotkham.Columns.UsedBy).IsLessThanOrEqualTo(globalVariables.UserName)
                      .Execute();
                        ;

                        MaLuotkham = objLuotkham.MaLuotkham;
                        scope.Complete();
                        return ActionResult.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                return ActionResult.Error;
            }
        }

        public ActionResult ThemmoiBenhnhan(KcbDanhsachBenhnhan objKcbDanhsachBenhnhan, KcbLuotkham objLuotkham, KcbDangkyKcb objKcbDangkyKcb,KcbDangkySokham objSoKCB, int KieuKham,ref long id_kham,ref string Msg)
        {
            int v_IdBenhnhan = -1;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objKcbDanhsachBenhnhan.IsNew = true;
                        objKcbDanhsachBenhnhan.Save();
                        //Thêm lần khám
                        objLuotkham.IdBenhnhan = objKcbDanhsachBenhnhan.IdBenhnhan;
                        objLuotkham.SoBenhAn = string.Empty;
                       
                        objLuotkham.SttKham = THU_VIEN_CHUNG.LaySTTKhamTheoDoituong(objLuotkham.IdDoituongKcb);
                        objLuotkham.NgayTao = globalVariables.SysDate;
                        objLuotkham.NguoiTao = globalVariables.UserName;
                        objLuotkham.IsNew = true;
                        objLuotkham.Save();
                        
                        SqlQuery sqlQueryPatientExam = new Select().From(KcbLuotkham.Schema)
                         .Where(KcbLuotkham.Columns.IdBenhnhan).IsNotEqualTo(objLuotkham.IdBenhnhan)
                         .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham);
                        if (sqlQueryPatientExam.GetRecordCount() > 0)
                        {
                            string patientCode = THU_VIEN_CHUNG.KCB_SINH_MALANKHAM();
                            new Update(KcbLuotkham.Schema)
                                .Set(KcbLuotkham.Columns.MaLuotkham).EqualTo(patientCode)
                                .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).Execute();
                            objLuotkham.MaLuotkham = patientCode;
                        }
                        new Update(KcbDmucLuotkham.Schema)
                        .Set(KcbDmucLuotkham.Columns.TrangThai).EqualTo(2)
                        .Set(KcbDmucLuotkham.Columns.EndTime).EqualTo(DateTime.Now)
                        .Where(KcbDmucLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbDmucLuotkham.Columns.TrangThai).IsLessThanOrEqualTo(1)
                        .And(KcbDmucLuotkham.Columns.UsedBy).IsLessThanOrEqualTo(globalVariables.UserName)
                        .Execute();
                        //.And(KcbDmucLuotkham.Columns.Nam).IsEqualTo(globalVariables.SysDate.Year)//Tạm bỏ tránh máy client cố tình điều chỉnh khác máy server
                        ;
                        if (objSoKCB != null)
                        {
                            //Kiểm tra xem có sổ KCB hay chưa
                            objSoKCB.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objSoKCB.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            KcbDangkySokham _temp = new Select().From(KcbDangkySokham.Schema).Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                .ExecuteSingle<KcbDangkySokham>();
                            if (_temp == null)
                            {
                                objSoKCB.NgayTao = globalVariables.SysDate;
                                objSoKCB.NguoiTao = globalVariables.UserName;
                                objSoKCB.IsNew = true;
                                objSoKCB.Save();
                            }
                            else
                            {
                                if (Utility.Int64Dbnull(_temp.IdThanhtoan, 0) > 0)//Ko làm gì cả
                                {
                                    Msg = "Đã thu tiền sổ khám của Bệnh nhân nên không được phép xóa hoặc cập nhật lại";
                                }
                                else//Update lại sổ KCB
                                {
                                    _temp.DonGia = objSoKCB.DonGia;
                                    _temp.BnhanChitra = objSoKCB.BnhanChitra;
                                    _temp.BhytChitra = objSoKCB.BhytChitra;
                                    _temp.PtramBhyt = objSoKCB.PtramBhyt;
                                    _temp.PtramBhytGoc = objSoKCB.PtramBhytGoc;
                                    _temp.PhuThu = objSoKCB.PhuThu;
                                    _temp.TuTuc = objSoKCB.TuTuc;
                                    _temp.NguonThanhtoan = objSoKCB.NguonThanhtoan;
                                    _temp.IdLoaidoituongkcb = objSoKCB.IdLoaidoituongkcb;
                                    _temp.IdDoituongkcb = objSoKCB.IdDoituongkcb;
                                    _temp.MaDoituongkcb = objSoKCB.MaDoituongkcb;
                                    _temp.Noitru = objSoKCB.Noitru;
                                    _temp.IdGoi = objSoKCB.IdGoi;
                                    _temp.TrongGoi = objSoKCB.TrongGoi;
                                    _temp.IdNhanvien = objSoKCB.IdNhanvien;
                                    _temp.NgaySua = globalVariables.SysDate;
                                    _temp.NguoiSua = globalVariables.UserName;
                                    _temp.IsNew = false;
                                    _temp.MarkOld();
                                    _temp.Save();
                                }
                            }
                        }
                        else
                        {
                            new Delete().From(KcbDangkySokham.Schema).Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                                           .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                                           .And(KcbDangkySokham.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                                           .Execute();
                        }
                        if (objKcbDangkyKcb != null)//Đôi lúc người dùng không chọn phòng khám
                        {
                            objKcbDangkyKcb.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objKcbDangkyKcb.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            id_kham = AddRegExam(objKcbDangkyKcb, objLuotkham, false, KieuKham);
                        }

                        scope.Complete();
                        return ActionResult.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("loi trong qua trinh cap nhap thong tin them moi thong tin benh nhan tiep don {0}", ex);
                return ActionResult.Error;
            }
        }
        public ActionResult UpdateLanKham(KcbDanhsachBenhnhan objKcbDanhsachBenhnhan, KcbLuotkham objLuotkham, KcbDangkyKcb objKcbDangkyKcb,KcbDangkySokham objSoKCB, int KieuKham, decimal PtramBhytCu, decimal PtramBhytgoc ,ref string Msg)
        {
            ActionResult _ActionResult = ActionResult.Success;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        
                        SqlQuery query =
                            new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(
                                objLuotkham.MaLuotkham).And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(
                                    objLuotkham.IdBenhnhan);
                        KcbLuotkham objExam = query.ExecuteSingle<KcbLuotkham>();
                        UpdatePatientInfo(objKcbDanhsachBenhnhan);
                        //decimal PtramBHYT = THU_VIEN_CHUNG.TinhPtramBHYT(objLuotkham);
                        //if (PtramBHYT != Utility.DecimaltoDbnull(objLuotkham.PtramBhyt))
                        //{
                        //    objLuotkham.PtramBhyt = PtramBHYT;
                        //}
                        objLuotkham.MarkOld();
                        objLuotkham.IsNew = false;
                        objLuotkham.Save();
                        if (objSoKCB != null)
                        {
                            //Kiểm tra xem có sổ KCB hay chưa
                            objSoKCB.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objSoKCB.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            KcbDangkySokham _temp = new Select().From(KcbDangkySokham.Schema).Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                .ExecuteSingle<KcbDangkySokham>();
                            if (_temp == null)
                            {
                                objSoKCB.NgayTao = globalVariables.SysDate;
                                objSoKCB.NguoiTao = globalVariables.UserName;
                                objSoKCB.IsNew = true;
                                objSoKCB.Save();
                            }
                            else
                            {
                                if (Utility.Int64Dbnull(_temp.IdThanhtoan, 0) > 0)//Ko làm gì cả
                                {
                                    Msg = "Đã thu tiền sổ khám của Bệnh nhân nên không được phép xóa hoặc cập nhật lại";
                                }
                                else//Update lại sổ KCB
                                {
                                    _temp.DonGia = objSoKCB.DonGia;
                                    _temp.BnhanChitra = objSoKCB.BnhanChitra;
                                    _temp.BhytChitra = objSoKCB.BhytChitra;
                                    _temp.PtramBhyt = objSoKCB.PtramBhyt;
                                    _temp.PtramBhytGoc = objSoKCB.PtramBhytGoc;
                                    _temp.PhuThu = objSoKCB.PhuThu;
                                    _temp.TuTuc = objSoKCB.TuTuc;
                                    _temp.NguonThanhtoan = objSoKCB.NguonThanhtoan;
                                    _temp.IdLoaidoituongkcb = objSoKCB.IdLoaidoituongkcb;
                                    _temp.IdDoituongkcb = objSoKCB.IdDoituongkcb;
                                    _temp.MaDoituongkcb = objSoKCB.MaDoituongkcb;
                                    _temp.Noitru = objSoKCB.Noitru;
                                    _temp.IdGoi = objSoKCB.IdGoi;
                                    _temp.TrongGoi = objSoKCB.TrongGoi;
                                    _temp.IdNhanvien = objSoKCB.IdNhanvien;
                                    _temp.NgaySua = globalVariables.SysDate;
                                    _temp.NguoiSua = globalVariables.UserName;
                                    _temp.IsNew = false;
                                    _temp.MarkOld();
                                    _temp.Save();
                                }
                            }
                        }
                        else
                        {
                            new Delete().From(KcbDangkySokham.Schema).Where(KcbDangkySokham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                                           .And(KcbDangkySokham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                                           .And(KcbDangkySokham.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                                                           .Execute();
                        }
                        //Kiểm tra nếu % bị thay đổi thì cập nhật lại tất cả các bảng
                        if (PtramBhytCu != Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) || PtramBhytgoc != Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0))
                           _ActionResult= THU_VIEN_CHUNG.UpdatePtramBHYT(objLuotkham, -1);
                        if (_ActionResult == ActionResult.Cancel)//Báo không cho phép thay đổi phần trăm BHYT do đã có dịch vụ đã thanh toán
                        {
                            return _ActionResult;
                        }

                        if (objKcbDangkyKcb != null)
                        {
                            objKcbDangkyKcb.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objKcbDangkyKcb.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            AddRegExam(objKcbDangkyKcb,objLuotkham, false, KieuKham);
                        }
                        scope.Complete();
                        return ActionResult.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi trong qua trinh update thong tin benh nhan {0}", ex);
                return ActionResult.Error;
            }
        }
        public  DataTable GetClinicCode(string ClinicCode)
        {
            return SPs.KcbLaythongtinNoikcbbd(ClinicCode).GetDataSet().Tables[0];
        }
    }
}
