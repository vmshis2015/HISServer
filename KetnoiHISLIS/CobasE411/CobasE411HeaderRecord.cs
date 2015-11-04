using System;

namespace VMS.HIS.HLC.ASTM
{
    [Serializable]
    public class CobasE411HeaderRecord : AstmBaseRecord
    {
        #region Header Properties

        public AstmField CommentOrSpecialInstructions = new AstmField(10);
        public AstmField DelimiterDefinition = new AstmField(1);
        public AstmField ProcessingId = new AstmField(11);
        public AstmField SoftwareName = new AstmField(9);
        public AstmField SenderNameOrId = new AstmField(4);
        public AstmField VersionNo = new AstmField(12, "1");

        #endregion

        #region Constructor

        public CobasE411HeaderRecord() : this(string.Empty)
        {
        }

        public CobasE411HeaderRecord(string rawString)
        {
            RecordType = new AstmField(0, "H");
            RecordLength = 13;
            DefaultValue = string.Empty;
            Parse(rawString);
        }

        #endregion
    }
}