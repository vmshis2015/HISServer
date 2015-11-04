using System;

namespace VMS.HIS.HLC.ASTM
{
    [Serializable]
    public class AstmTerminationOrderRecord : AstmBaseRecord
    {
        #region Termination Properties

        public AstmField SequenceNumber = new AstmField(1, "1");
        public AstmField TerminationCode = new AstmField(2, "F");

        #endregion

        #region Constructor

        public AstmTerminationOrderRecord() : this(string.Empty)
        {
        }

        public AstmTerminationOrderRecord(string rawString)
        {
            RecordType = new AstmField(0, "L");
            RecordLength = 2;
            DefaultValue = string.Empty;
            Parse(rawString);
        }

        #endregion
    }
}