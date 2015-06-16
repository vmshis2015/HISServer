using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using VNS.Libs;

namespace VNS.Properties
{
    public class PropertyLib
    {
        public static ConfigProperties _ConfigProperties = new ConfigProperties();
        public static ConfigProperties GetConfigProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new ConfigProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ConfigProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ConfigProperties();
            }
        }

        public static DynamicInputProperties _DynamicInputProperties = new DynamicInputProperties();
        public static DynamicInputProperties GetDynamicInputProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new DynamicInputProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (DynamicInputProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new DynamicInputProperties();
            }
        }

        public static QuaythuocProperties _QuaythuocProperties = new QuaythuocProperties();
        public static QuaythuocProperties GetQuaythuocProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new QuaythuocProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QuaythuocProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QuaythuocProperties();
            }
        }
        public static BenhAnProperties _BenhAnProperties = new BenhAnProperties();
        public static BenhAnProperties GetBenhAnProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new BenhAnProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (BenhAnProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new BenhAnProperties();
            }
        }

        public static FTPProperties _FTPProperties = new FTPProperties();
        public static FTPProperties GetFTPProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new FTPProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (FTPProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new FTPProperties();
            }
        }

        public static NoitruProperties _NoitruProperties = new NoitruProperties();
        public static NoitruProperties GetNoitruProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new NoitruProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (NoitruProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new NoitruProperties();
            }
        }

        public static NhapkhoProperties _NhapkhoProperties = new NhapkhoProperties();
        public static NhapkhoProperties GetNhapkhoProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new NhapkhoProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (NhapkhoProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new NhapkhoProperties();
            }
        }
        public static ChuyenkhoProperties _ChuyenkhoProperties = new ChuyenkhoProperties();
        public static ChuyenkhoProperties GetChuyenkhoProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new ChuyenkhoProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ChuyenkhoProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ChuyenkhoProperties();
            }
        }

       
        public static KCBProperties _KCBProperties = new KCBProperties();
        public static KCBProperties GetKCBProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new KCBProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (KCBProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new KCBProperties();
            }
        }

        public static AppProperties _AppProperties = null;
        public static AppProperties GetAppPropertiess()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new AppProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (AppProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new AppProperties();
            }
        }

        public static HISCLSProperties _HISCLSProperties = new HISCLSProperties();
        public static HISCLSProperties GetHISCLSProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new HISCLSProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (HISCLSProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new HISCLSProperties();
            }
        }


        public static MayInProperties _MayInProperties = new MayInProperties();
        public static MayInProperties GetMayInProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new MayInProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (MayInProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new MayInProperties();
            }
        }

        public static ThamKhamProperties _ThamKhamProperties = new ThamKhamProperties();
        public static ThamKhamProperties GetThamKhamProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new ThamKhamProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ThamKhamProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ThamKhamProperties();
            }
        }
        public static ThanhtoanProperties _ThanhtoanProperties = new ThanhtoanProperties();
        public static ThanhtoanProperties GetThanhtoanProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new ThanhtoanProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ThanhtoanProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ThanhtoanProperties();
            }
        }
       
        public static QMSPrintProperties _QMSPrintProperties = new QMSPrintProperties();
        public static QMSPrintProperties GetQMSPrintProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new QMSPrintProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QMSPrintProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QMSPrintProperties();
            }
        }
       

       
        public static void SaveProperty(object _Property)
        {
            if (Utility.DoTrim(globalVariables.m_strPropertiesFolder)!="")
            {
                Utility.CreateFolder(globalVariables.m_strPropertiesFolder);
            }
            try
            {
                using (StreamWriter myWriter = new StreamWriter(string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, _Property.GetType().Name)))
                {
                    myWriter.AutoFlush = true;
                    var mySerializer = new XmlSerializer(_Property.GetType());
                    mySerializer.Serialize(myWriter, _Property);
                    myWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu cấu hình:\n"+ex.Message);
            }
        }

       
        public static DuocNoitruProperties _DuocNoitruProperties = null;
        public static DuocNoitruProperties GetDuocNoitruProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new DuocNoitruProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (DuocNoitruProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new DuocNoitruProperties();
            }
        }
        public static HisDuocProperties _HisDuocProperties = null;
        public static HisDuocProperties GetHisDuocProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new HisDuocProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (HisDuocProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new HisDuocProperties();
            }
        }
        
      
        public static HISQMSProperties _HISQMSProperties = null;
        public static HISQMSProperties GetHISQMSProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                HISQMSProperties myProperty = new HISQMSProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                FileStream myFileStream = new FileStream(filePath, FileMode.Open);
                XmlSerializer mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (HISQMSProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new HISQMSProperties();
            }
        }


        public static QheGiaThuocProperties _QheGiaThuocProperties = null;
        public static QheGiaThuocProperties GetQheGiaThuocProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new QheGiaThuocProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QheGiaThuocProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QheGiaThuocProperties();
            }
        }
        public static QheGiaCLSProperties _QheGiaCLSProperties = null;
        public static QheGiaCLSProperties GetQheGiaCLSProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new QheGiaCLSProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (QheGiaCLSProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new QheGiaCLSProperties();
            }
        }

        public static ThuocProperties _ThuocProperties = null;
        public static ThuocProperties GetThuocProperties()
        {
            try
            {
                if (!System.IO.Directory.Exists(globalVariables.m_strPropertiesFolder))
                {
                    Directory.CreateDirectory(globalVariables.m_strPropertiesFolder);
                }
                var myProperty = new ThuocProperties();
                string filePath = string.Format(@"{0}\{1}.xml", globalVariables.m_strPropertiesFolder, myProperty.GetType().Name);
                if (!File.Exists(filePath)) return myProperty;
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperty.GetType());
                myProperty = (ThuocProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperty;
            }
            catch (Exception ex)
            {
                return new ThuocProperties();
            }
        }

    }
}
