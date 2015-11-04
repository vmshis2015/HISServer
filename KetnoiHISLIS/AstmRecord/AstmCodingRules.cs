using System;

namespace VMS.HIS.HLC.ASTM
{
    [Serializable]
    public class AstmCodingRules
    {
        //ASTM Rules Messages

        /// <summary>
        ///     Hàm khởi tạo mặc định các giá trị
        /// </summary>
        public AstmCodingRules()
        {
            EndOfRecordCharacter = DeviceHelper.CR;
            FieldDelimiter = '|';
            RepeatDelimiter = '\\';
            ComponentDelimiter = '^';
            EscapeCharacter = '&';
        }

        /// <summary>
        ///     Ký tự ngăn cách các Record (Mặc định là CR)
        /// </summary>
        public char EndOfRecordCharacter { get; set; }

        /// <summary>
        ///     Ký tự ngăn cách các trường (mặc định là dấu sổ thẳng "|")
        /// </summary>
        public char FieldDelimiter { get; set; }

        /// <summary>
        ///     Ký tự lặp
        /// </summary>
        public char RepeatDelimiter { get; set; }

        /// <summary>
        ///     Ký tự ngăn cách các thành phần
        /// </summary>
        public char ComponentDelimiter { get; set; }

        /// <summary>
        ///     EscapeCharacter
        /// </summary>
        public char EscapeCharacter { get; set; }
    }
}