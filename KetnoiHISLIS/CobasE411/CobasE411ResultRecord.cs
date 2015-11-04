using System;

namespace VMS.HIS.HLC.ASTM
{
    [Serializable]
    public class CobasE411ResultRecord : AstmBaseRecord
    {
        #region Termination Properties

        public AstmField DataOrMeasurementValue = new AstmField(3);
        public AstmField DateTimeTestCompleted = new AstmField(12);
        public AstmField DateTimeTestStarted = new AstmField(11);
        public AstmField OperatorIdentification = new AstmField(10);
        public AstmField ReferenceRanges = new AstmField(5);
        public AstmField ResultAbnormalFlags = new AstmField(6);
        public AstmField ResultStatus = new AstmField(8);
        public AstmField SequenceNumber = new AstmField(1);
        public AstmField Units = new AstmField(4);
        public AstmField UniversalTestId = new AstmField(2);

        #endregion

        #region Constructor

        public CobasE411ResultRecord() : this(string.Empty)
        {
        }

        public CobasE411ResultRecord(string rawString)
        {
            RecordType = new AstmField(0, "R");
            RecordLength = 13;
            DefaultValue = string.Empty;
            Parse(rawString);
        }

        #endregion

        #region Public Method

        //public ResultItem GetResult()
        //{
        //    var result = new ResultItem();

        //    //lấy về test name 
        //    string[] testnameArr = UniversalTestId.Data.Split(Rules.ComponentDelimiter);
        //    testnameArr = testnameArr[testnameArr.Length - 1].Split('/');
        //    result.TestName = testnameArr[0].Trim();
        //    result.TestDataId = result.TestName;
        //    result.MeasureUnit = Units.Data.Trim();
        //    string[] tempResult = DataOrMeasurementValue.Data.Split(Rules.ComponentDelimiter);
        //    result.TestValue = tempResult[tempResult.Length - 1];
        //    return result;
        //}

        #endregion
    }
}