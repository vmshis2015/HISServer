using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace VMS.HIS.HLC.ASTM
{
    [Serializable]
    public abstract class AstmBaseRecord : IAstmBaseRecord
    {
        #region Implementation of IAstmBaseRecord

        /// <summary>
        ///     Record Type
        /// </summary>
        public AstmField RecordType = new AstmField(0);

        protected AstmBaseRecord()
        {
            Rules = new AstmCodingRules();
        }

        public AstmCodingRules Rules { get; set; }

        public List<AstmField> AllAstmFields
        {
            get { return GetAllAstmField(); }
        }

        /// <summary>
        ///     Index lớn nhất của Record
        /// </summary>
        public int RecordLength { get; set; }

        /// <summary>
        ///     Giá trị mặc định cho những trường không sử dụng
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        ///     Hàm biến đổi chuỗi đầu vào thành object
        /// </summary>
        /// <param name="baseRecord"></param>
        public void Parse(string baseRecord)
        {
            //Nếu biến là rỗng thì thoát khỏi vòng luôn
            if (string.IsNullOrEmpty(baseRecord)) return;

            string[] allFields = baseRecord.Split(Rules.FieldDelimiter);
            for (int i = 0; i < allFields.Length; i++)
            {
                int i1 = i;
                IEnumerable<AstmField> f = (from field in AllAstmFields where field.Index == i1 select field);
                if (!f.Any()) continue;
                f.First().Data = allFields[i];
            }
        }

        /// <summary>
        ///     Hàm tạo ra dữ liệu trong từng Record
        /// </summary>
        /// <returns></returns>
        public string Create()
        {
            var listOfField = new List<string>();
            for (int i = 0; i <= RecordLength; i++)
            {
                int i1 = i;
                IEnumerable<AstmField> f = (from field in AllAstmFields where field.Index == i1 select field);
                listOfField.Add(!f.Any() ? DefaultValue : f.First().Data);
            }
            string @join = string.Join(Rules.FieldDelimiter.ToString(CultureInfo.InvariantCulture),
                listOfField.ToArray());
            return string.Format("{0}{1}", @join, Rules.EndOfRecordCharacter);
        }

        /// <summary>
        ///     Hàm biến đổi chuỗi đầu vào thành object
        /// </summary>
        /// <param name="allFields"></param>
        public void Parse(string[] allFields)
        {
            for (int i = 0; i < allFields.Length; i++)
            {
                int i1 = i;
                IEnumerable<AstmField> f = (from field in AllAstmFields where field.Index == i1 select field);
                if (!f.Any()) continue;
                f.First().Data = allFields[i];
            }
        }

        /// <summary>
        ///     Lấy về tất cả các trường
        /// </summary>
        /// <returns></returns>
        private List<AstmField> GetAllAstmField()
        {
            FieldInfo[] fields = GetType().GetFields();
            List<AstmField> x = (from info in fields
                where info.FieldType.FullName == typeof (AstmField).FullName
                orderby ((AstmField) info.GetValue(this)).Index ascending
                select (AstmField) info.GetValue(this)).ToList();
            return x;
        }

        #endregion

        /// <summary>
        ///     Tạo một đối tượng mới từ đối tượng đã có
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                return formatter.Deserialize(stream);
            }
        }

        /// <summary>
        ///     Dump Object to String
        /// </summary>
        /// <returns></returns>
        public string Dump()
        {
            try
            {
                FieldInfo[] fields = GetType().GetFields();
                var x = (from info in fields
                    where info.FieldType.FullName == typeof (AstmField).FullName
                    orderby ((AstmField) info.GetValue(this)).Index ascending
                    select new
                    {
                        Index = ((AstmField) info.GetValue(this)).Index.ToString(),
                        PropertyName = info.Name,
                        PropertyValue = ((AstmField) info.GetValue(this)).Data
                    }).ToList();
                const int maxIndex = 5;
                int maxPropertyName = (from f in x
                    select f.PropertyName.Length).Max();
                maxPropertyName = maxPropertyName > 12 ? maxPropertyName : 12;

                int maxPropertyValue = (from f in x
                    select f.PropertyValue.Length).Max();
                maxPropertyValue = maxPropertyValue > 13 ? maxPropertyValue : 13;
                string result = string.Format("{0}  {1}  {2}\r\n", "Index".PadRight(maxIndex, ' '),
                    "PropertyName".PadRight(maxPropertyName, ' '), "PropertyValue".PadRight(maxPropertyValue, ' '));
                result = x.Aggregate(result,
                    (current, v) =>
                        current +
                        string.Format("{0}  {1}  {2}\r\n", v.Index.PadRight(maxIndex, ' '),
                            v.PropertyName.PadRight(maxPropertyName, ' '),
                            v.PropertyValue.PadRight(maxPropertyValue, ' ')));
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}