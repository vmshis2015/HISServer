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

namespace VNS.HIS.BusRule.Classes
{
    public class BAOCAO_THUOC
    {
        private NLog.Logger log;
        public BAOCAO_THUOC()
        {
            log = LogManager.GetCurrentClassLogger();
        }
        public static DataTable ThuocBaocaoTinhhinhBenhnhanlinhthuoc(DateTime? FromDate, DateTime? ToDate, int? IDKHOXUAT, int? iddoituong)
        {
            return SPs.ThuocBaocaoTinhhinhBenhnhanlinhthuoc(FromDate, ToDate, IDKHOXUAT, iddoituong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoTinhhinhPhatthuocbenhnhan(DateTime? FromDate, DateTime? ToDate, int? IDKHOXUAT, 
            int? iddoituong, int? Kieuthongke,string kieuthuoc_vt)
        {
            return SPs.ThuocBaocaoTinhhinhPhatthuocbenhnhan(FromDate, ToDate, IDKHOXUAT, iddoituong, Kieuthongke, kieuthuoc_vt).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoTinhhinhkedonthuocTheobacsy(int? IDKHOXUAT, int? iddoituong, string mabschidinh, int? idThuoc, DateTime? FromDate, DateTime? ToDate, short trangthai)
        {
            return SPs.ThuocBaocaoTinhhinhkedonthuocTheobacsy(IDKHOXUAT, iddoituong, mabschidinh, idThuoc, FromDate, ToDate,trangthai).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoThuochethan(int? IDThuoc, int? IDKHO, int? CanhBaoTruoc, int? NhomThuoc)
        {
            return SPs.ThuocBaocaoThuochethan(IDThuoc,
                IDKHO, CanhBaoTruoc, NhomThuoc).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoSoluongtonthuoctheokho(string IDKHOLIST, int? IDTHUOC, int idloaithuoc, short? HETHAN, string kieu_thuocvattu)
        {
            return SPs.ThuocBaocaoSoluongtonthuoctheokho(IDKHOLIST, IDTHUOC, idloaithuoc, HETHAN, kieu_thuocvattu).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoThuoctheonhacungcap(DateTime? FromDate, DateTime? ToDate, int? IDKHO, string ma_nhacungcap,string kieu_thuocvattu,byte laphieuvay)
        {
            return SPs.ThuocBaocaoThuoctheonhacungcap(FromDate, ToDate, IDKHO, ma_nhacungcap, kieu_thuocvattu, laphieuvay).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoBiendongthuocTrongkhotong(string FromDate, string ToDate, int? IDKHO, string NhomThuoc, int? IDThuoc, int? Cobiendong)
        {
            return SPs.ThuocBaocaoBiendongthuocTrongkhotong(FromDate,ToDate, IDKHO, NhomThuoc, IDThuoc, Cobiendong).GetDataSet().Tables[0];
               
        }
        public static DataTable ThuocBaocaohuychot(string FromDate, string ToDate, int? IDKHO, string NhomThuoc, int? IDThuoc, byte huychothuyxacnhan)
        {
            return SPs.ThuocBaocaohuychot(FromDate, ToDate, IDKHO, NhomThuoc, IDThuoc,huychothuyxacnhan).GetDataSet().Tables[0];

        }
        public static DataTable ThuocBaocaonhapxuatton(string FromDate, string ToDate, int? IDKHO, string NhomThuoc, int? IDThuoc, int? Cobiendong)
        {
            return SPs.ThuocBaocaonhapxuatton(FromDate, ToDate, IDKHO, NhomThuoc, IDThuoc, Cobiendong).GetDataSet().Tables[0];

        }
        public static DataTable ThuocBaocaoBiendongthuocTrongkhole(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocBaocaoBiendongthuocTrongkhole(FromDate, ToDate, IDKHO, IDTHUOC, NhomThuoc, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuocKhole(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuocKhole(FromDate, ToDate, IDKHO, IDTHUOC, NhomThuoc, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuocTutruc(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuocTutruc(FromDate, ToDate, IDKHO, IDTHUOC, NhomThuoc, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuocKhochan(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuocKhochan(FromDate, ToDate, IDKHO, NhomThuoc, IDTHUOC, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuoc(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuoc(FromDate, ToDate, IDKHO, NhomThuoc, IDTHUOC, Cobiendong).GetDataSet().Tables[0];
        }
        
        public static DataTable ThuocThethuocChitiet(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuocChitiet(FromDate, ToDate, IDKHO, NhomThuoc, IDTHUOC, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocThethuocTutrucChitiet(string FromDate, string ToDate, int? IDKHO, int? IDTHUOC, string NhomThuoc, int? Cobiendong)
        {
            return SPs.ThuocThethuocTutrucChitiet(FromDate, ToDate, IDKHO, NhomThuoc, IDTHUOC, Cobiendong).GetDataSet().Tables[0];
        }
        public static DataTable ThuocBaocaoTinhhinhnhapkhothuoc(string FromDate, string ToDate, int? TrangThai, int? IDKho, int id_thuoc, byte loaiphieu, int kieungaytimkiem, string lydohuy, string manhacungcap, string kieu_thuocvattu)
        {
            return SPs.ThuocBaocaoTinhhinhnhapkhothuoc(FromDate, ToDate, TrangThai, IDKho, id_thuoc, loaiphieu, kieungaytimkiem, lydohuy, manhacungcap, kieu_thuocvattu).GetDataSet().Tables[0];
        }
        
    }
}
