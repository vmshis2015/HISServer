
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using NLog.LogReceiverService;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using NLog;
using VerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment;
using Excel = Microsoft.Office.Interop.Excel;
using VNS.Properties;
using VNS.HIS.DAL;
namespace VNS.Libs
{

    public class ExcelUtlity
    {

        private string sStartCell = "C";
        private string sEndCell = "T";
        private readonly Logger log;
        public ExcelUtlity()
        {
            log = LogManager.GetCurrentClassLogger();
        }

       
        /// <summary>
        /// FUNCTION FOR EXPORT TO EXCEL
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="worksheetName"></param>
        /// <param name="saveAsLocation"></param>
        /// <returns></returns>
        public bool WriteDataTableToExcel_SoTamTra(System.Data.DataTable dataTable, string worksheetName, string saveAsLocation, string ReporType, string tenkhoa, string ngaylinh)
        {
            Utility.CreateFolder(saveAsLocation);
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;
            // Excel.Range chartRange;
            //Excel.Application xlApp;
            //Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;
            //Excel.Range chartRange;
            try
            {
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet) excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;

                excelSheet.get_Range("C1", "T1").Merge(true);

                excelCellrange = excelSheet.get_Range("C1", "T1");
                excelCellrange.Font.FontStyle = new Font("Times New Roman", 20, FontStyle.Bold);

                if (PropertyLib._DuocNoitruProperties != null)
                {
                    excelCellrange.Font.Size = PropertyLib._DuocNoitruProperties.TitleFont.Size;
                    excelCellrange.FormulaR1C1 = string.Format("{0}:{1}", PropertyLib._DuocNoitruProperties.tieudebaocao, ReporType);
                }
                else
                {
                    excelCellrange.Font.Size = 18;
                    excelCellrange.FormulaR1C1 = string.Format("{0}:{1}", "SỔ TỔNG HỢP THUỐC HÀNG NGÀY", ReporType);
                }

                excelCellrange.HorizontalAlignment = 3;
                excelCellrange.VerticalAlignment = VerticalAlignment.Center;
                excelCellrange.Font.Bold = true;


                excelSheet.get_Range("C2", "T2").Merge(true);
                excelCellrange = excelSheet.get_Range("C2", "T2");
                excelCellrange.Font.FontStyle = new Font("Times New Roman", 14, FontStyle.Bold);
                excelCellrange.Font.Size = 14;
                excelCellrange.FormulaR1C1 = string.Format("{0}:{1}", "Tên khoa", tenkhoa);
                excelCellrange.HorizontalAlignment = 3;
                excelCellrange.VerticalAlignment = VerticalAlignment.Center;
                excelCellrange.Font.Bold = true;
                //excelSheet = excelSheet.get_Range("b2", "e3");
                //excelSheet.FormulaR1C1 = "MARK LIST";
                //excelSheet.HorizontalAlignment = 3;
                //excelSheet.VerticalAlignment = 3;
                //excelSheet.Cells[3, 1] = ReporType;
                excelSheet.Cells[3, 2] = "Ngày lĩnh : " + ngaylinh;

                // loop through each row and add values to our sheet
                int rowcount = 4;
                //  int colHeader = 4;
                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= dataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 5)
                        {
                            bool isBold = false;
                            excelSheet.Cells[rowcount, i] = GetCapTion(dataTable.Columns[i - 1].ColumnName, ref isBold);
                            //excelCellrange.Font=new Font() font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
                            excelSheet.Cells.Font.Color =
                                System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                            excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, i], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                            excelCellrange.Font.Bold = isBold;
                            if (PropertyLib._DuocNoitruProperties != null)
                            {
                                excelCellrange.Font.Size =
                                    PropertyLib._DuocNoitruProperties.ContentFont.Size;
                            }
                            else
                            {
                                excelCellrange.Font.Size = 8;
                            }
                            excelCellrange.Orientation = i >= 8 ? 90 : 0;
                            BorderCellRange(excelCellrange);
                        }

                        excelSheet.Cells[rowcount + 1, i] = datarow[i - 1].ToString();
                        excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount + 1, 1], excelSheet.Cells[rowcount + 1, dataTable.Columns.Count]];
                        if (PropertyLib._DuocNoitruProperties != null)
                        {
                            excelCellrange.Font.Size =
                                PropertyLib._DuocNoitruProperties.ContentFont.Size;
                        }

                        else
                        {
                            excelCellrange.Font.Size = 8;
                        }

                        BorderCellRange(excelCellrange);


                    }

                }
                // excelSheet.get_Range("G4", "H2").Merge(true);

                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[6, 1], excelSheet.Cells[6, dataTable.Columns.Count]];
                //excelCellrange.BorderAround()
                excelCellrange.EntireColumn.AutoFit();
                //  border.Weight = 2d;

                BorderCellRange(excelCellrange);
                // excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[2, dataTable.Columns.Count]];
                // FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);


                //now save the workbook and exit Excel
                // Get the current printer
                if (System.IO.File.Exists(saveAsLocation)) File.Delete(saveAsLocation);
                excelworkBook.SaveAs(saveAsLocation); ;
                if (PropertyLib._DuocNoitruProperties.isInThangMayin)
                {
                    PrintMyExcelFile(saveAsLocation);
                }
                else
                {
                    System.Diagnostics.Process.Start(saveAsLocation);
                }
                excelworkBook.Close();

                //excel.Quit();
                excel.Application.Quit();

                GC.Collect();
                GC.WaitForPendingFinalizers();



                Marshal.FinalReleaseComObject(excelSheet);

                //excelworkBook.Close(false, Type.Missing, Type.Missing);
                Marshal.FinalReleaseComObject(excelworkBook);

                excel.Quit();
                Marshal.FinalReleaseComObject(excel);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {

                GC.Collect();
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;

            }



        }

        private void setvalue(Microsoft.Office.Interop.Excel.Worksheet excelSheet, Microsoft.Office.Interop.Excel.Range excelCellrange, string vitri, string value)
        {
            excelSheet.get_Range(vitri).Merge(true);
            excelCellrange = excelSheet.get_Range(vitri);
            excelCellrange.Font.FontStyle = new Font("Times New Roman", 11, FontStyle.Bold);
            excelCellrange.Font.Size = 11;
            excelCellrange.FormulaR1C1 = value;
            excelCellrange.HorizontalAlignment = 3;
            excelCellrange.VerticalAlignment = VerticalAlignment.Center;
            excelCellrange.Font.Bold = true;

        }
       
        void PrintMyExcelFile(string sFileName)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Open the Workbook:
            Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Workbooks.Open(sFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Get the first worksheet.
            // (Excel uses base 1 indexing, not base 0.)
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

            var _with1 = ws.PageSetup;
            // A4 papersize
            _with1.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            // Landscape orientation
            _with1.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
            // Fit Sheet on One Page 
            _with1.FitToPagesWide = 1;
            _with1.FitToPagesTall = 1;
            // Normal Margins
            _with1.LeftMargin = excelApp.InchesToPoints(0.7);
            _with1.RightMargin = excelApp.InchesToPoints(0.7);
            _with1.TopMargin = excelApp.InchesToPoints(0.75);
            _with1.BottomMargin = excelApp.InchesToPoints(0.75);
            _with1.HeaderMargin = excelApp.InchesToPoints(0.3);
            _with1.FooterMargin = excelApp.InchesToPoints(0.3);
            object misValue = System.Reflection.Missing.Value;
            // Print out 1 copy to the default printer:

            ws.PrintOut(
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Cleanup:
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.FinalReleaseComObject(ws);

            wb.Close(false, Type.Missing, Type.Missing);
            Marshal.FinalReleaseComObject(wb);

            excelApp.Quit();
            Marshal.FinalReleaseComObject(excelApp);
        }
     
        public void ExportToExcel(System.Data.DataTable dt, string saveAsLocation)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            excel = new Microsoft.Office.Interop.Excel.Application();

            // for making Excel visible
            excel.Visible = false;
            excel.DisplayAlerts = false;

            // Creation a new Workbook
            excelworkBook = excel.Workbooks.Add(Type.Missing);

            // Workk sheet
            excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
            excelSheet.Name = "Sheet1";
            // Copy the DataTable to an object array
            object[,] rawData = new object[dt.Rows.Count + 1, dt.Columns.Count];

            // Copy the column names to the first row of the object array
            log.Trace("chay de tao ten cot");
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                rawData[0, col] = dt.Columns[col].ColumnName;
                // log.Trace(string.Format("cot:{0}-ten:{1}", col, dt.Columns[col].ColumnName));


            }
            //log.Trace("\n");
            //log.Trace("chay de tao dong");
            // Copy the values to the object array
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    var sname = dt.Rows[row].ItemArray[col];
                    rawData[row + 1, col] = sname;
                    //log.Trace(string.Format("dongthu:{0}-ten:{1}", row, dt.Rows[row].ItemArray[col]));
                    //log.Trace("\n");
                }
            }


            // Fast data export to Excel
            string excelRange = string.Format("A1:{0}{1}", LastCoulmLetter(dt.Columns.Count), dt.Rows.Count + 1);
            excelSheet.get_Range(excelRange, Type.Missing).Value2 = rawData;
            //excelSheet.f
            excelworkBook.SaveAs(saveAsLocation); ;
            excelworkBook.Close();
            excel.Quit();

        }

        public string LastCoulmLetter(int coulmnCount)
        {

            string finalColLetter = string.Empty;
            string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int colCharsetLen = colCharset.Length;

            if (coulmnCount > colCharsetLen)
            {
                finalColLetter = colCharset.Substring(
                    (coulmnCount - 1) / colCharsetLen - 1, 1);
            }

            finalColLetter += colCharset.Substring(
                    (coulmnCount - 1) % colCharsetLen, 1);

            return finalColLetter;
        }
        private string GetCapTion(string colName, ref  bool isBold)
        {
            string replaceName = "";
            switch (colName)
            {
                case "ten_benhnhan":
                    isBold = true;
                    replaceName = "Họ và tên BN";
                    break;
                case "Tuoi":
                    replaceName = "Tuổi";
                    isBold = true;
                    break;
                case "ten_doituong_kcb":
                    replaceName = "Đối tượng";
                    isBold = true;
                    break;
                case "ten_giuong":
                    replaceName = "Giường";
                    isBold = true;
                    break;
                case "ten_buong":
                    replaceName = "Buồng";
                    isBold = true;
                    break;
                case "ten_khoanoitru":
                    replaceName = "Khoa nội trú";
                    isBold = true;
                    break;
                default:
                    replaceName = colName;
                    isBold = false;
                    break;
            }
            return replaceName;
        }
        private void BorderCellRange(Microsoft.Office.Interop.Excel.Range excelCellrange)
        {
            Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
            // Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
            border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[XlBordersIndex.xlEdgeLeft].LineStyle =
           Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }
        /// <summary>
        /// FUNCTION FOR FORMATTING EXCEL CELLS
        /// </summary>
        /// <param name="range"></param>
        /// <param name="HTMLcolorCode"></param>
        /// <param name="fontColor"></param>
        /// <param name="IsFontbool"></param>
        public void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }
        static HSSFWorkbook hssfworkbook;
        static void WriteToFile(string fileName)
        {
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(fileName, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }
        static void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            //Create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = globalVariables.Branch_Name;
            hssfworkbook.DocumentSummaryInformation = dsi;

            //Create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "bhyt";
            hssfworkbook.SummaryInformation = si;
        }
        public static void ExportToExcel_HSS(DataTable m_dtExportExcel, string filesave)
        {
            InitializeWorkbook();

            try
            {

                Sheet sheet = hssfworkbook.CreateSheet("Sheet1");
                Row row;
                Cell cell;
                int iRow = 0;
                int iCol = 0;
                row = sheet.CreateRow(iRow);



                //Puts in headers (these are table row headers, omit if you

                //just need a straight data dump

                for (int j = 0; j < m_dtExportExcel.Columns.Count; j++)
                {

                    cell = row.CreateCell(j);

                    String columnName = m_dtExportExcel.Columns[j].ToString();

                    cell.SetCellValue(columnName);



                }
                iRow++;
                foreach (DataRow dr in m_dtExportExcel.Rows)
                {

                    row = sheet.CreateRow(iRow);
                    foreach (DataColumn col in m_dtExportExcel.Columns)
                    {
                        cell = row.CreateCell(iCol);
                        string sname = Utility.sDbnull(dr[col]);
                        sheet.GetRow(iRow).GetCell(iCol).SetCellValue(sname);
                        iCol++;
                    }
                    iCol = 0;
                    iRow++;

                }

                WriteToFile(filesave);
                //  sheet.FitToPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }

        }

    }
}
