using System;
using System.Collections.Generic;
using System.Linq;

namespace VMS.HIS.HLC.ASTM
{
    [Serializable]
    public class CobasE411TestOrderRecord : AstmBaseRecord
    {
        #region Termination Properties

        public AstmField ActionCode = new AstmField(11);
        public AstmField DateTimeResultReportedOrLastModified = new AstmField(22);
        public AstmField InstrumentSpecimenId = new AstmField(3);
        public AstmField Priority = new AstmField(5);
        public AstmField ReportTypes = new AstmField(25);
        public AstmField RequestOrderDateAndTime = new AstmField(6);
        public AstmField SequenceNumber = new AstmField(1);
        public AstmField SpecimenCollectionDateAndTime = new AstmField(7);
        public AstmField SpecimenDescriptor = new AstmField(15);
        public AstmField SpecimenId = new AstmField(2);
        public AstmField UniversalTestId = new AstmField(4);

        #endregion

        #region Constructor

        public CobasE411TestOrderRecord() : this(string.Empty)
        {
        }

        public CobasE411TestOrderRecord(string rawString)
        {
            RecordType = new AstmField(0, "O");
            RecordLength = 30;
            DefaultValue = string.Empty;
            Parse(rawString);
        }

        #endregion

        #region Public Method

        public string CreateUniversalTestid(List<string> pOrderList)
        {
            List<string> resultList =
                pOrderList.Select(s => string.Format("{0}{0}{0}{1}{0}", Rules.ComponentDelimiter, s)).ToList();
            return string.Join(Rules.RepeatDelimiter.ToString(), resultList.ToArray());
        }

        #endregion
    }
}