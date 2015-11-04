using System;
using System.Globalization;

namespace VMS.HIS.HLC.ASTM
{
    [Serializable]
    public class AstmField
    {
        /// <summary>
        ///     Hàm khởi tạo một trường mới
        /// </summary>
        /// <param name="index">Index của trường</param>
        public AstmField(int index)
            : this(index, string.Empty)
        {
        }

        /// <summary>
        ///     Hàm khởi tạo một trường mới
        /// </summary>
        /// <param name="index">Index của trường</param>
        /// <param name="data">Data với index được truyền</param>
        public AstmField(int index, string data)
        {
            Data = data;
            Index = index;
        }

        /// <summary>
        ///     Dữ liệu của trường
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        ///     Thứ tự Index của trường (tính từ 0)
        /// </summary>
        public int Index { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:\t{1}", Index.ToString(CultureInfo.InvariantCulture).PadRight(4), Data);
        }
    }
}