using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VNS.HIS.DAL;
using VNS.Libs;
using SubSonic;
using NLog;
namespace VNS.HIS.NGHIEPVU.THUOC
{

    public class ChotThuoc
    {
        private NLog.Logger log;
        public ChotThuoc()
        {
            log = NLog.LogManager.GetCurrentClassLogger();
        }
        public ActionResult CHOT_CAPPHAT(List<int> lstIDDonthuoc, DateTime Ngay_Chot)
        {
            int v_intResult;
            ActionResult actResult;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        TLichsuChotthuoc _newItem = new TLichsuChotthuoc();
                        _newItem.NgayChot = Ngay_Chot;
                        _newItem.NguoiChot = globalVariables.UserName;
                        _newItem.IsNew = true;
                        _newItem.Save();
                        object obj = TLichsuChotthuoc.CreateQuery().GetMax("ID_CHOT");
                        TPhieuXuatthuocBenhnhanCollection vCollection = new TPhieuXuatthuocBenhnhanCollection();
                        v_intResult = new Update(TPhieuXuatthuocBenhnhan.Schema.TableName).Set(TPhieuXuatthuocBenhnhan.NgayChotColumn).EqualTo(Ngay_Chot)
                                .Set(TPhieuXuatthuocBenhnhan.IdChotColumn).EqualTo(Utility.Int32Dbnull(obj, -1))
                                .Set(TPhieuXuatthuocBenhnhan.NgayHuychotColumn).EqualTo(null)
                                 .Set(TPhieuXuatthuocBenhnhan.NguoiHuychotColumn).EqualTo(string.Empty)
                                 .Set(TPhieuXuatthuocBenhnhan.LydoHuychotColumn).EqualTo(string.Empty)
                                .Where(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsNull()
                                .And(TPhieuXuatthuocBenhnhan.IdDonthuocColumn).In(lstIDDonthuoc)
                                .Execute();

                        new Update(KcbDonthuoc.Schema.TableName).Set(KcbDonthuoc.NgayChotColumn).EqualTo(Ngay_Chot)
                                .Set(KcbDonthuoc.IdChotColumn).EqualTo(Utility.Int32Dbnull(obj, -1))
                                .Set(KcbDonthuoc.NgayHuychotColumn).EqualTo(null)
                                 .Set(KcbDonthuoc.NguoiHuychotColumn).EqualTo(string.Empty)
                                 .Set(KcbDonthuoc.LydoHuychotColumn).EqualTo(string.Empty)
                                .Where(KcbDonthuoc.NgayChotColumn).IsNull()
                                .And(KcbDonthuoc.IdDonthuocColumn).In(lstIDDonthuoc)
                                .Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:\n" + ex.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult HUYCHOT_CAPPHAT(List<int> lstIDDonthuoc,DateTime ngayhuy,string lydohuy)
        {
            int v_intResult;
            ActionResult actResult;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        TPhieuXuatthuocBenhnhanCollection vCollection = new Select()
                            .From(TPhieuXuatthuocBenhnhan.Schema)
                            .Where(TPhieuXuatthuocBenhnhan.IdDonthuocColumn).In(lstIDDonthuoc)
                            .And(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsNotNull()
                            .ExecuteAsCollection<TPhieuXuatthuocBenhnhanCollection>();
                        List<int> lstIdchot = (from p in vCollection
                                               select Utility.Int32Dbnull(p.IdChot, -1)).ToList<int>();
                        foreach (TPhieuXuatthuocBenhnhan _item in vCollection)
                        {
                               new Update(TPhieuXuatthuocBenhnhan.Schema.TableName)
                                .Set(TPhieuXuatthuocBenhnhan.NgayChotColumn).EqualTo(null)
                                .Set(TPhieuXuatthuocBenhnhan.NgayHuychotColumn).EqualTo(ngayhuy)
                                .Set(TPhieuXuatthuocBenhnhan.LydoHuychotColumn).EqualTo(lydohuy)
                                .Set(TPhieuXuatthuocBenhnhan.NguoiHuychotColumn).EqualTo(globalVariables.UserName)
                                .Set(TPhieuXuatthuocBenhnhan.IdChotColumn).EqualTo(null)
                                .Where(TPhieuXuatthuocBenhnhan.IdPhieuColumn).IsEqualTo(_item.IdPhieu)
                                .Execute();

                               new Update(KcbDonthuoc.Schema.TableName)
                                   .Set(KcbDonthuoc.NgayChotColumn).EqualTo(null)
                                   .Set(KcbDonthuoc.NgayHuychotColumn).EqualTo(ngayhuy)
                                   .Set(KcbDonthuoc.LydoHuychotColumn).EqualTo(lydohuy)
                                   .Set(KcbDonthuoc.NguoiHuychotColumn).EqualTo(globalVariables.UserName)
                                   .Set(KcbDonthuoc.IdChotColumn).EqualTo(null)
                                   .Where(KcbDonthuoc.IdDonthuocColumn).IsEqualTo(_item.IdDonthuoc)
                                   .Execute();
                        }
                        int itemremains = new Select()
                            .From(TPhieuXuatthuocBenhnhan.Schema)
                            .Where(TPhieuXuatthuocBenhnhan.IdChotColumn).In(lstIdchot)
                            .And(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsNotNull()
                            .ExecuteAsCollection<TPhieuXuatthuocBenhnhanCollection>().Count;
                        if (itemremains <= 0)
                            new Delete().From(TLichsuChotthuoc.Schema).Where(TLichsuChotthuoc.IdChotColumn).In(lstIdchot).Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:\n" + ex.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult CHOT_CAPPHAT(DateTime ngaycapphattu, DateTime ngaycapphatden, DateTime Ngay_Chot)
        {
            int v_intResult;
            ActionResult actResult;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        TLichsuChotthuoc _newItem = new TLichsuChotthuoc();
                        _newItem.NgayChot = Ngay_Chot;
                        _newItem.NguoiChot = globalVariables.UserName;
                        _newItem.IsNew = true;
                        _newItem.Save();
                        object obj = TLichsuChotthuoc.CreateQuery().GetMax("ID_CHOT");
                        TPhieuXuatthuocBenhnhanCollection vCollection = new TPhieuXuatthuocBenhnhanCollection();

                        if (ngaycapphattu.ToString("dd/MM/yyyy") == "01/01/1900")
                        {
                            v_intResult = new Update(TPhieuXuatthuocBenhnhan.Schema.TableName)
                                .Set(TPhieuXuatthuocBenhnhan.NgayChotColumn).EqualTo(Ngay_Chot)
                                .Set(TPhieuXuatthuocBenhnhan.IdChotColumn).EqualTo(Utility.Int32Dbnull(obj, -1))
                                 .Set(TPhieuXuatthuocBenhnhan.NgayHuychotColumn).EqualTo(null)
                                 .Set(TPhieuXuatthuocBenhnhan.NguoiHuychotColumn).EqualTo(string.Empty)
                                 .Set(TPhieuXuatthuocBenhnhan.LydoHuychotColumn).EqualTo(string.Empty)
                                .Where(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsNull()
                                .Execute();

                            new Update(KcbDonthuoc.Schema.TableName)
                               .Set(KcbDonthuoc.NgayChotColumn).EqualTo(Ngay_Chot)
                               .Set(KcbDonthuoc.IdChotColumn).EqualTo(Utility.Int32Dbnull(obj, -1))
                                .Set(KcbDonthuoc.NgayHuychotColumn).EqualTo(null)
                                .Set(KcbDonthuoc.NguoiHuychotColumn).EqualTo(string.Empty)
                                .Set(KcbDonthuoc.LydoHuychotColumn).EqualTo(string.Empty)
                               .Where(KcbDonthuoc.NgayChotColumn).IsNull()
                               .Execute();

                        }
                        else
                        {
                            v_intResult = new Update(TPhieuXuatthuocBenhnhan.Schema.TableName)
                                .Set(TPhieuXuatthuocBenhnhan.NgayChotColumn).EqualTo(Ngay_Chot)
                                 .Set(TPhieuXuatthuocBenhnhan.IdChotColumn).EqualTo(Utility.Int32Dbnull(obj, -1))
                                 .Set(TPhieuXuatthuocBenhnhan.NgayHuychotColumn).EqualTo(null)
                                 .Set(TPhieuXuatthuocBenhnhan.NguoiHuychotColumn).EqualTo(string.Empty)
                                 .Set(TPhieuXuatthuocBenhnhan.LydoHuychotColumn).EqualTo(string.Empty)
                                 .Where(TPhieuXuatthuocBenhnhan.NgayXacnhanColumn).IsBetweenAnd(ngaycapphattu, ngaycapphatden)
                                 .And(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsNull()
                                 .Execute();

                            new Update(KcbDonthuoc.Schema.TableName)
                               .Set(KcbDonthuoc.NgayChotColumn).EqualTo(Ngay_Chot)
                                .Set(KcbDonthuoc.IdChotColumn).EqualTo(Utility.Int32Dbnull(obj, -1))
                                .Set(KcbDonthuoc.NgayHuychotColumn).EqualTo(null)
                                .Set(KcbDonthuoc.NguoiHuychotColumn).EqualTo(string.Empty)
                                .Set(KcbDonthuoc.LydoHuychotColumn).EqualTo(string.Empty)
                                .Where(KcbDonthuoc.NgayXacnhanColumn).IsBetweenAnd(ngaycapphattu, ngaycapphatden)
                                .And(KcbDonthuoc.NgayChotColumn).IsNull()
                                .Execute();
                        }

                      

                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:\n" + ex.ToString());
                return ActionResult.Error;
            }
        }
        public ActionResult HUYCHOT_CAPPHAT(DateTime Ngay_Chot, DateTime ngayhuy, string lydohuy)
        {
            int v_intResult;
            ActionResult actResult;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        TPhieuXuatthuocBenhnhanCollection vCollection = new TPhieuXuatthuocBenhnhanController().FetchByQuery(
                              TPhieuXuatthuocBenhnhan.CreateQuery()
                              .WHERE(TPhieuXuatthuocBenhnhan.NgayChotColumn.ColumnName, Comparison.Equals, Ngay_Chot));
                        //Lấy danh sách các lần chốt
                        List<int> lstIDChot = (from p in vCollection.AsEnumerable()
                                               select p.IdChot.Value).Distinct().ToList<int>();
                        //Update thông tin chốt về giá trị mặc định
                        v_intResult = new Update(TPhieuXuatthuocBenhnhan.Schema.TableName).Set(TPhieuXuatthuocBenhnhan.NgayChotColumn).EqualTo(null)
                            .Set(TPhieuXuatthuocBenhnhan.IdChotColumn).EqualTo(0)
                             .Set(TPhieuXuatthuocBenhnhan.NgayHuychotColumn).EqualTo(ngayhuy)
                                .Set(TPhieuXuatthuocBenhnhan.LydoHuychotColumn).EqualTo(lydohuy)
                            .Set(TPhieuXuatthuocBenhnhan.NguoiHuychotColumn).EqualTo(globalVariables.UserName)
                            .Where(TPhieuXuatthuocBenhnhan.NgayChotColumn).IsEqualTo(Ngay_Chot)
                            .Execute();

                        new Update(KcbDonthuoc.Schema.TableName).Set(KcbDonthuoc.NgayChotColumn).EqualTo(null)
                            .Set(KcbDonthuoc.IdChotColumn).EqualTo(0)
                             .Set(KcbDonthuoc.NgayHuychotColumn).EqualTo(ngayhuy)
                                .Set(KcbDonthuoc.LydoHuychotColumn).EqualTo(lydohuy)
                            .Set(KcbDonthuoc.NguoiHuychotColumn).EqualTo(globalVariables.UserName)
                            .Where(KcbDonthuoc.NgayChotColumn).IsEqualTo(Ngay_Chot)
                            .Execute();
                        //Xóa lịch sử chốt
                        new Delete().From(TLichsuChotthuoc.Schema).Where(TLichsuChotthuoc.IdChotColumn).In(lstIDChot).Execute();
                       
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:\n" + ex.ToString());
                return ActionResult.Error;
            }
        }
    }
}
