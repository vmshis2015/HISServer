using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic;
using NLog;
using NLog.Config;
using NLog.Targets;
using SubSonic;
using VNS.Libs;

namespace VMS.FSW
{
    public partial class FSWService : ServiceBase
    {
        HLCFileWatcherServer HLCFW = null;
        int _interval = 10000;
        public FSWService()
        {
            InitializeComponent();
            LogConfig();
            InitSubSonic(GetConnectionString(@"C:\VMSConfig\HISconfig"), "ORM");
            string _intervalFile = AppDomain.CurrentDomain.BaseDirectory + @"\timerInterval.txt";
            if (File.Exists(_intervalFile))
            {
                _interval = Convert.ToInt32(File.ReadAllText(_intervalFile));

            }
            else
            {
                File.WriteAllText(_intervalFile, _interval.ToString());
            }
            HLCFW = new HLCFileWatcherServer();
            
            HLCFW.AddWatcher(Utility.Laygiatrithamsohethong("ASTM_RESULTS_FOLDER", @"\\192.168.1.254\Results\", false), _interval);
            
        }
        public static void LogConfig()
        {
            try
            {
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget
                {
                    FileName =
                        "${basedir}/AppLogs/${date:format=yy}${date:format=MM}${date:format=dd}/${logger}.log",
                    //"${basedir}/AppLogs/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log",
                    Layout =
                        "${date:format=dd/MM/yyy HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${message}",
                    ArchiveAboveSize = 5242880
                };
                config.AddTarget("file", fileTarget);
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
            }
            catch (Exception ex)
            {

            }
        }
        private static string GetConnectionString(string ConfigDir)
        {
            string _defaultConnString =
                "workstation id=192.168.0.168; Data Source=192.168.0.168; Initial catalog=PACS;uid=sa;pwd=123456";
            try
            {
                if (ConfigDir.Trim() == "") ConfigDir = @"C:\VMSConfig\HISconfig";
                string _filePath = ConfigDir + @"\Servers.xml";
                if (File.Exists(_filePath))
                {
                    var _XMLdoc = new XmlDocument();
                    _XMLdoc.Load(_filePath);
                    XmlNode _XMLNode = _XMLdoc.SelectSingleNode(@"NewDataSet/SQLSERVER/CONNECTSTRING");
                    if (_XMLNode != null)
                        return _XMLNode.InnerText;
                }
                return _defaultConnString;
            }
            catch
            {
                return _defaultConnString;
            }
        }
        internal class CustomSqlProvider : SqlDataProvider
        {
            public CustomSqlProvider(string connectionString)
            {
                DefaultConnectionString = connectionString;
            }

            public override string Name
            {
                get { return "ORM"; }
            }
        }
        public static void InitSubSonic(string SqlConnstr, string ProviderName)
        {
            //MyLog.Trace("\r\n-----------------------------------------------------------------------");
            //MyLog.Trace(string.Format("Connection String: {0}", SqlConnstr));
            DataService.Providers = new DataProviderCollection();
            var myProvider = new CustomSqlProvider(SqlConnstr);
            if (DataService.Providers[ProviderName] == null)
            {
                DataService.Providers.Add(myProvider);
                DataService.Provider = myProvider;
            }
            else
            {
                DataService.Provider.DefaultConnectionString = SqlConnstr;
            }
        }
        protected override void OnStart(string[] args)
        {
            try
            {
                HLCFW.myLog.Trace("Start Server..........");
                HLCFW.StartServer();

            }
            catch (Exception ex)
            {
               HLCFW.myLog.Trace("OnStart.Exception-->" + ex.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
              
            }
            catch (Exception ex)
            {
            }
        }

       
       
    }
}


