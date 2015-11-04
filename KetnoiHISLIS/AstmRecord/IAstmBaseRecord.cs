using System.Collections.Generic;

namespace VMS.HIS.HLC.ASTM
{
    public interface IAstmBaseRecord
    {
        AstmCodingRules Rules { get; set; }
        List<AstmField> AllAstmFields { get; }

        /// <summary>
        ///     Index lớn nhất của Record
        /// </summary>
        int RecordLength { get; set; }

        /// <summary>
        ///     Giá trị mặc định cho những trường không sử dụng
        /// </summary>
        string DefaultValue { get; set; }

        //AstmField RecordType;
        /// <summary>
        ///     Record Type
        /// </summary>
        /// <summary>
        ///     Hàm biến đổi chuỗi đầu vào thành object
        /// </summary>
        /// <param name="baseRecord"></param>
        void Parse(string baseRecord);

        /// <summary>
        ///     Hàm tạo ra các record
        /// </summary>
        /// <returns></returns>
        string Create();
    }
}