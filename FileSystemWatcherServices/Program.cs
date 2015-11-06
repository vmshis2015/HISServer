using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace VMS.FSW
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            try
            {
                Process pc = Process.GetCurrentProcess();
                Directory.SetCurrentDirectory(pc.MainModule.FileName.Substring(0, pc.MainModule.FileName.LastIndexOf(@"\")));
                var servicesToRun = new ServiceBase[] { new FSWService() };
                ServiceBase.Run(servicesToRun);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
            
        }
    }
}
