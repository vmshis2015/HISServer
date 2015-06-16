using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VNS.Libs;
using VNS.HIS.DAL;

namespace VNS.Libs
{
   public class TinhCLS
    {
       public static void TinhGiaChiDinhCLS(KcbLuotkham objLuotkham,KcbChidinhclsChitiet objAssignDetail)
       {
           GB_TinhPhtramBHYT(objAssignDetail, objLuotkham,Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0));
       }
      
       /// <summary>
       /// hàm thực hienj việc tính phâm trăm bảo hiểm
       /// </summary>
       /// <param name="objAssignDetail"></param>
       /// <param name="PTramBHYT"></param>
       public static void GB_TinhPhtramBHYT(KcbChidinhclsChitiet objAssignDetail, KcbLuotkham objLuotkham, decimal PTramBHYT)
       {
           byte TrangthaiBhyt = 1;
           decimal BHYT_PTRAM_TRAITUYENNOITRU = Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_PTRAM_TRAITUYENNOITRU", "0", false), 0m);
           bool b_ExistPtramBHYT = false;
           if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))//(objLuotkham.MaDoituongKcb == "DV")//Tự túc
           {
               TrangthaiBhyt = (byte)0;
               objAssignDetail.TuTuc = 0;
           }
           else
               TrangthaiBhyt = (byte)(globalVariables.gv_blnApdungChedoDuyetBHYT ? 0 : 1);
           if (Utility.Int32Dbnull(objAssignDetail.TrangthaiHuy, -1) == -1) objAssignDetail.TrangthaiHuy = 0;
           DmucDichvuclsChitiet obServiceDetail =
               DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(objAssignDetail.IdChitietchidinh));
           if (obServiceDetail != null)
           {
               objAssignDetail.GiaDanhmuc = Utility.DecimaltoDbnull(obServiceDetail.DonGia);
           }
           objAssignDetail.PtramBhyt = PTramBHYT;
           objAssignDetail.PtramBhytGoc = objLuotkham.PtramBhytGoc;
           objAssignDetail.LoaiChietkhau = 0;
           objAssignDetail.TrangthaiBhyt = TrangthaiBhyt;
           objAssignDetail.IdLoaichidinh = 0;

           if (Utility.Int32Dbnull(objAssignDetail.TuTuc, 0) == 1)
           {
               objAssignDetail.BhytChitra = 0;
               objAssignDetail.BnhanChitra = Utility.DecimaltoDbnull(objAssignDetail.DonGia, 0);
               objAssignDetail.PtramBhyt = 0;
           }
           else
           {
               PtramBHYTDacBiet(objAssignDetail, objLuotkham, 2, ref b_ExistPtramBHYT);
               if (b_ExistPtramBHYT)
               {
                   objAssignDetail.BhytChitra = Utility.DecimaltoDbnull(objAssignDetail.DonGia) *
                                          Utility.DecimaltoDbnull(objAssignDetail.PtramBhyt) / 100;
                   objAssignDetail.BnhanChitra = Utility.DecimaltoDbnull(objAssignDetail.DonGia, 0) -
                                         Utility.DecimaltoDbnull(objAssignDetail.BhytChitra);
               }
               else
               {
                   
                   PTramBHYT = Utility.DecimaltoDbnull(objLuotkham.PtramBhyt);
                   decimal BHCT = 0m;
                   if (objLuotkham.DungTuyen == 1)
                   {
                       BHCT = Utility.DecimaltoDbnull(objAssignDetail.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                   }
                   else
                   {
                       if (objLuotkham.TrangthaiNoitru <= 0)
                           BHCT = Utility.DecimaltoDbnull(objAssignDetail.DonGia, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhyt, 0) / 100);
                       else//Nội trú cần tính=đơn giá * % đầu thẻ * % tuyến
                           BHCT = Utility.DecimaltoDbnull(objAssignDetail.DonGia.Value, 0) * (Utility.DecimaltoDbnull(objLuotkham.PtramBhytGoc, 0) / 100) * (BHYT_PTRAM_TRAITUYENNOITRU / 100);
                   }
                   decimal BNCT =
                       Utility.DecimaltoDbnull(objAssignDetail.DonGia, 0) -
                       BHCT;
                   objAssignDetail.BhytChitra =BHCT;
                   objAssignDetail.BnhanChitra = BNCT;
                   //objAssignDetail.BhytChitra = Utility.DecimaltoDbnull(objAssignDetail.DonGia) *
                   //                     Utility.DecimaltoDbnull(PTramBHYT) / 100;
               }
               
           }
       }
       /// <summary>
       /// hàm thực hiện viec tính toán giá đặc biệt cho bệnh nhân
       /// </summary>
       /// <param name="objAssignDetail"></param>
       /// <param name="objLuotkham"></param>
       /// <param name="PaymentType_ID"></param>
       public static void PtramBHYTDacBiet(KcbChidinhclsChitiet objAssignDetail,KcbLuotkham objLuotkham,int PaymentType_ID,ref bool b_Exist)
       {
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
           SqlQuery sqlQuery = new Select().From(DmucBhytChitraDacbiet.Schema)
               .Where(DmucBhytChitraDacbiet.Columns.IdDichvuChitiet).IsEqualTo(objAssignDetail.IdChitietchidinh)
               .And(DmucBhytChitraDacbiet.Columns.MaLoaithanhtoan).IsEqualTo(PaymentType_ID)
               .And(DmucBhytChitraDacbiet.Columns.DungtuyenTraituyen).IsEqualTo(IsDungTuyen)
               .And(DmucBhytChitraDacbiet.Columns.MaDoituongKcb).IsEqualTo(objLuotkham.MaDoituongKcb);
           DmucBhytChitraDacbiet objDetailPtramBhyt = sqlQuery.ExecuteSingle<DmucBhytChitraDacbiet>();
           if(objDetailPtramBhyt!=null)
           {
               objAssignDetail.IdLoaichidinh = 1;
               objAssignDetail.BhytChitra = Gia_BHYT(objDetailPtramBhyt.TileGiam,Utility.DecimaltoDbnull(objAssignDetail.DonGia, 0));
               objAssignDetail.BnhanChitra = Utility.DecimaltoDbnull(objAssignDetail.DonGia, 0) -
                                         objAssignDetail.BhytChitra;
               objAssignDetail.PtramBhyt = Utility.DecimaltoDbnull(objDetailPtramBhyt.TileGiam, 0);
               objAssignDetail.LoaiChietkhau = 1;
               b_Exist = true;
               // objAssignDetail.DonGia =
           }
       }
       private static decimal Gia_BHYT(decimal PhanTramBHYT, decimal DON_GIA)
       {
           return PhanTramBHYT * DON_GIA / 100;
       }
       

      
       
    }
}
