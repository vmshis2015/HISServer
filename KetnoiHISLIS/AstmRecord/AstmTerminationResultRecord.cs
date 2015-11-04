using System;

namespace VMS.HIS.HLC.ASTM
{
    [Serializable]
    public class AstmTerminationResultRecord : AstmBaseRecord
    {
        #region Termination Properties

        public AstmField SequenceNumber = new AstmField(1, "1");
        public AstmField TerminationCode = new AstmField(2, "L");

        #endregion

        #region Constructor

        public AstmTerminationResultRecord() : this(string.Empty)
        {
        }

        public AstmTerminationResultRecord(string rawString)
        {
            RecordType = new AstmField(0, "L");
            RecordLength = 2;
            DefaultValue = string.Empty;
            Parse(rawString);
        }

        #endregion
    }
}