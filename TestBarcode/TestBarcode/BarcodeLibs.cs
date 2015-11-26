using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Leadtools;
using Leadtools.Barcode;
using Leadtools.Codecs;
using Leadtools;
using Leadtools.Barcode;
using Leadtools.Codecs;
using Leadtools.Forms;

namespace BarcodeLibs
{
    public class BarcodeCreator
    {
        //public static byte[] CreateBarcode( string value, int resolution, int Width, int Height, bool Save2File,bool usingMemory, ref string ErrMsg)
        //{
        //    byte[] arrImg;
        //    BarcodeEngine barEngine;
        //    try
        //    {
        //        RasterImage theImage = RasterImage.Create((int)(8.5 * resolution), (int)(11.0 * resolution), 1, resolution, RasterColor.FromKnownColor(RasterKnownColor.White));
        //        // Unlock barcode support.
        //        // Note that this is a sample key, which will not work in your toolkit
        //        BarcodeEngine.Startup(BarcodeMajorTypeFlags.Barcodes1d);
        //        BarcodeEngine.Startup(BarcodeMajorTypeFlags.BarcodesPdfWrite);
        //        BarcodeEngine.Startup(BarcodeMajorTypeFlags.BarcodesDatamatrixWrite);
        //        BarcodeEngine.Startup(BarcodeMajorTypeFlags.BarcodesQrWrite);

        //        // Initialize barcodes
        //        barEngine = new BarcodeEngine();

        //        BarcodeData data = new BarcodeData();

        //        LeadRect rc = new LeadRect(0, 0, Width, Height);
        //        data.Unit = BarcodeUnit.ScanlinesPerPixels;
        //        data.Location = rc;
        //        data.SearchType = BarcodeSearchTypeFlags.DatamatrixDefault;

        //        string[] barcodeText;
        //        barcodeText = new string[1];
        //        barcodeText[0] = value;
        //        data.Data = BarcodeData.ConvertFromStringArray(barcodeText);

        //        BarcodeColor barColor = new BarcodeColor();
        //        barColor.BarColor = RasterColor.FromKnownColor(RasterKnownColor.Black);
        //        barColor.SpaceColor = RasterColor.FromKnownColor(RasterKnownColor.White);
        //        Barcode1d bar1d = new Barcode1d();
        //        BarcodeWritePdf barPDF = new BarcodeWritePdf();
        //        BarcodeWriteDatamatrix barDM = new BarcodeWriteDatamatrix();
        //        bar1d.StandardFlags = Barcode1dStandardFlags.Barcode1dCode128EncodeA;

        //        barDM.Justify = BarcodeJustifyFlags.Right;
        //        barDM.FileIdHigh = 0;
        //        barDM.FileIdLow = 0;
        //        barDM.GroupNumber = 0;
        //        barDM.GroupTotal = 0;
        //        barDM.XModule = 0;
                
        //        BarcodeWriteQr barQR = new BarcodeWriteQr();
        //        string barcodeFileName = AppDomain.CurrentDomain.BaseDirectory + @"\barcode.tif";
        //        barEngine.Write(theImage, data, barColor, BarcodeWriteFlags.UseColors | BarcodeWriteFlags.Transparent | BarcodeWriteFlags.DisableCompression, bar1d, barPDF, barDM, barQR, LeadRect.Empty);
        //        if (usingMemory)
        //        {
        //            using (MemoryStream _stream = new MemoryStream())
        //            {
        //                using (RasterCodecs _Codecs = new RasterCodecs())
        //                {
        //                    _Codecs.Save(theImage, _stream, RasterImageFormat.Tif, theImage.BitsPerPixel);
        //                    arrImg = _stream.ToArray();
        //                    if (Save2File)
        //                    {
        //                        _Codecs.Save(theImage, barcodeFileName, RasterImageFormat.Tif, theImage.BitsPerPixel);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            using (RasterCodecs _Codecs = new RasterCodecs())
        //            {
        //                _Codecs.Save(theImage, barcodeFileName, RasterImageFormat.Tif, theImage.BitsPerPixel);
        //                arrImg = System.IO.File.ReadAllBytes(barcodeFileName);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrMsg = ex.Message;
        //        return null;
        //    }
        //    return arrImg;

        //}
        public static byte[] CreateBarcode(BarcodeSymbology _BarcodeSymbology, string value, int resolution, int Width, int Height, bool Save2File, bool usingMemory, ref string ErrMsg)
        {
            ErrMsg = "";
            BarcodeEngine barcodeEngineInstance = new BarcodeEngine();
            RasterImage theImage = RasterImage.Create((int)(8.5 * resolution), (int)(11.0 * resolution), 1, resolution, RasterColor.FromKnownColor(RasterKnownColor.White));
            // Create a UPC A barcode
            BarcodeData data = new BarcodeData();
            data.Symbology = _BarcodeSymbology;
            data.Value = value;
            data.Bounds = new LogicalRectangle(10, 10, Width, Height, LogicalUnit.Pixel);
            // Setup the options to enable error checking and show the text on the bottom of the barcode
            OneDBarcodeWriteOptions options = new OneDBarcodeWriteOptions();
            options.EnableErrorCheck = true;
            options.TextPosition = BarcodeOutputTextPosition.None;// OneDBarcodeTextPosition.Default;
            byte[] arrImg;
            try
            {
                string barcodeFileName = AppDomain.CurrentDomain.BaseDirectory + @"\barcode.tif";
                // Write the barcode
                barcodeEngineInstance.Writer.WriteBarcode(theImage, data, options);
                if (usingMemory)
                {
                    using (MemoryStream _stream = new MemoryStream())
                    {
                        using (RasterCodecs _Codecs = new RasterCodecs())
                        {
                            _Codecs.Save(theImage, _stream, RasterImageFormat.Tif, theImage.BitsPerPixel);

                            arrImg = _stream.ToArray();
                            if (Save2File)
                            {
                                _Codecs.Save(theImage, barcodeFileName, RasterImageFormat.Tif, theImage.BitsPerPixel);
                            }
                        }
                    }
                }
                else
                {
                    using (RasterCodecs _Codecs = new RasterCodecs())
                    {
                        _Codecs.Save(theImage, barcodeFileName, RasterImageFormat.Tif, theImage.BitsPerPixel);
                        arrImg = System.IO.File.ReadAllBytes(barcodeFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                ErrMsg = ex.Message;
                return null;
            }

            return arrImg;
        }
    }
}
//namespace Leadtools.Runtime.License
//{
//    public static class Support
//    {
//        public static bool KernelExpired
//        {
//            get
//            {
//                if (RasterSupport.KernelExpired)
//                {
//                    return true;
//                }
//                else
//                    return false;
//            }
//        }
       
       
//        public static void UnlockNO(bool check)
//        {
//            RasterSupport.Unlock(RasterSupportType.Abc, "R3naWU3mHs");
//            RasterSupport.Unlock(RasterSupportType.AbicRead, "dpKdJvh2P7");
//            RasterSupport.Unlock(RasterSupportType.AbicSave, "Nb39Cvv6Q2");
//            RasterSupport.Unlock(RasterSupportType.Barcodes1D, "UbXxzPTCVP");
//            RasterSupport.Unlock(RasterSupportType.Barcodes1DSilver, "jvMTAubUkH");
//            RasterSupport.Unlock(RasterSupportType.BarcodesDataMatrixRead, "E4Fy2TzBCc");
//            RasterSupport.Unlock(RasterSupportType.BarcodesDataMatrixWrite, "Np2utTGDD3");
//            RasterSupport.Unlock(RasterSupportType.BarcodesPdfRead, "n4WidQyJxd");
//            RasterSupport.Unlock(RasterSupportType.BarcodesPdfWrite, "WpKd6QFMyB");
//            RasterSupport.Unlock(RasterSupportType.BarcodesQRRead, "ypC4PUHpip");
//            RasterSupport.Unlock(RasterSupportType.BarcodesQRWrite, "BbXXGuVSjk");
//            RasterSupport.Unlock(RasterSupportType.Bitonal, "KbGGrSUz3N");
//            RasterSupport.Unlock(RasterSupportType.Ccow, "QvMN82YPTR");
//            RasterSupport.Unlock(RasterSupportType.Cmw, "rhiWfrmt5X");
//            RasterSupport.Unlock(RasterSupportType.Dicom, "y47S3rZv6U");
//            RasterSupport.Unlock(RasterSupportType.Document, "HbQR9NSXQ3");
//            RasterSupport.Unlock(RasterSupportType.DocumentWriters, "BhaNezSEBB");
//            RasterSupport.Unlock(RasterSupportType.DocumentWritersPdf, "3b39Q3YMdX");
//            RasterSupport.Unlock(RasterSupportType.ExtGray, "bpTmxSfx8R");
//            RasterSupport.Unlock(RasterSupportType.Forms, "GpC33ZK78k");
//            RasterSupport.Unlock(RasterSupportType.IcrPlus, "9vdKEtBhFy");
//            RasterSupport.Unlock(RasterSupportType.IcrProfessional, "3p2UAxjTy5");
//            RasterSupport.Unlock(RasterSupportType.J2k, "Hvu2PRAr3z");
//            RasterSupport.Unlock(RasterSupportType.Jbig2, "43WiSV4YNB");
//            RasterSupport.Unlock(RasterSupportType.Jpip, "YbGG7wWiVJ");
//            RasterSupport.Unlock(RasterSupportType.Pro, "");
//            RasterSupport.Unlock(RasterSupportType.LeadOmr, "J3vh828GC8");
//            RasterSupport.Unlock(RasterSupportType.MediaWriter, "TpjDw2kJD2");
//            RasterSupport.Unlock(RasterSupportType.Medical, "ZhyFRnk3sY");
//            RasterSupport.Unlock(RasterSupportType.Medical3d, "DvuzH3ePeu");
//            RasterSupport.Unlock(RasterSupportType.MedicalNet, "b4nBinY7tv");
//            RasterSupport.Unlock(RasterSupportType.MedicalServer, "QbXwuZxA3h");
//            RasterSupport.Unlock(RasterSupportType.Mobile, "");
//            RasterSupport.Unlock(RasterSupportType.Nitf, "G37rmw5dTr");
//            RasterSupport.Unlock(RasterSupportType.OcrAdvantage, "vhyejyrZ4T");
//            RasterSupport.Unlock(RasterSupportType.OcrAdvantagePdfLeadOutput, "83nacy746p");
//            RasterSupport.Unlock(RasterSupportType.OcrArabic, "RpTMEwJfUN");
//            RasterSupport.Unlock(RasterSupportType.OcrArabicPdfLeadOutput, "mhiVa3Trfr");
//            RasterSupport.Unlock(RasterSupportType.OcrPlus, "rvdKxn8Zr4");
//            RasterSupport.Unlock(RasterSupportType.OcrPlusPdfOutput, "4hS7bsn9bF");
//            RasterSupport.Unlock(RasterSupportType.OcrPlusPdfLeadOutput, "Bv4CWXckvf");
//            RasterSupport.Unlock(RasterSupportType.OcrProfessional, "jhr6pXRnwc");
//            RasterSupport.Unlock(RasterSupportType.OcrProfessionalAsian, "WbQQMTuFE4");
//            RasterSupport.Unlock(RasterSupportType.OcrProfessionalPdfOutput, "T3eYHx6Rx3");
//            RasterSupport.Unlock(RasterSupportType.OcrProfessionalPdfLeadOutput, "NvdjSYDX2v");
//            RasterSupport.Unlock(RasterSupportType.PdfAdvanced, "8hivUWQbSU");
//            RasterSupport.Unlock(RasterSupportType.PdfRead, "Wvuz2WC3rX");
//            RasterSupport.Unlock(RasterSupportType.PdfSave, "tv4CJsa5aJ");
//            RasterSupport.Unlock(RasterSupportType.PrintDriver, "YvMsmzECAE");
//            RasterSupport.Unlock(RasterSupportType.PrintDriverServer, "v37Ry49tHN");
//            RasterSupport.Unlock(RasterSupportType.Vector, "KpC5bPeAUs");

           
//            if (check)
//            {
//                Array a = Enum.GetValues(typeof(RasterSupportType));
//                foreach (RasterSupportType i in a)
//                {
//                    if (i != RasterSupportType.Vector && i != RasterSupportType.MedicalNet)
//                    {
//                        if (RasterSupport.IsLocked(i))
//                        {
//                        }
//                    }
//                }
//            }

//        }
       

//    }
//}
