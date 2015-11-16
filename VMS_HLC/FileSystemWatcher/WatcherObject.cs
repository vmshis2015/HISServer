using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Timers;
using VNS.Libs;
using Timer = System.Timers.Timer;

namespace VMS.FSW
{
    public class WatcherObject
    {
        #region Attributes
       
        private readonly Timer _myTimer = new Timer(15000);
        public Dictionary<string, FileSystemEventArgs> ListEvent = new Dictionary<string, FileSystemEventArgs>();
        public Dictionary<string, object> ListObject = new Dictionary<string, object>();
        private FileSystemWatcher _fileWatcher;
        private DateTime _lastModifitime = DateTime.Now;
        public string WatchedPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private DirectoryInfo _watcherPathInfo;// = new DirectoryInfo(WatchedPath);

        public event OnChangeHandler Change;
        public event OnRenameHandler Rename;

        #endregion

        #region Contructor

        public WatcherObject(string resultPath)
        {
            WatchedPath = resultPath;
            _watcherPathInfo = new DirectoryInfo(resultPath);
            _myTimer.Elapsed += _myTimer_Elapsed;
        }

        public WatcherObject(string resultPath,int intervalTime)
        {
            WatchedPath = resultPath;
            _watcherPathInfo = new DirectoryInfo(resultPath);
            _myTimer.Interval = intervalTime;
            _myTimer.Elapsed += _myTimer_Elapsed;
        }

        public WatcherObject()
        {
            _myTimer.Elapsed += _myTimer_Elapsed;
        }

        #endregion

        #region Public Method

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void StartWatch()
        {
            bool writeNotExists = false;
            while (!Directory.Exists(WatchedPath))
            //while (!CheckPath(WatchedPath))
            {
                if (!writeNotExists)
                {
                    writeNotExists = true;
                }
                //Nếu chưa tìm thấy thư mục cần theo dõi thì lặp lại liên tục
                Thread.Sleep(2000);
            }
            _lastModifitime = GetlastModifiedTime();
            // Create a new FileSystemWatcher and set its properties.
            _fileWatcher = new FileSystemWatcher
            {
                Path = WatchedPath,
                NotifyFilter = NotifyFilters.LastWrite
            };
            // Lấy về thời gian thay đổi cuối cùng của thư mục
            
            _myTimer.Start();
            
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Hàm kiểm tra file mới thay đổi và phát sinh sự kiện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _myTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _myTimer.Stop();
            // Lấy ra tên file mới
            try
            {
                IEnumerable<FileInfo> fileList=null;
                using (new NetworkConnection(WatchedPath, Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false), Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
                    fileList = _watcherPathInfo.GetFiles("*.txt", SearchOption.TopDirectoryOnly);
                fileList.OrderBy(c => c.LastWriteTime);
                List<string> fileQuery = new List<string>();
                foreach (FileInfo f in fileList)
                    //if (f.LastWriteTime.Ticks > _lastModifitime.Ticks)//Tạm bỏ đi do sẽ di chuyển file đã phân tích sang thư mục khác+ đề phòng file đọc bị lỗi lại không được đưa vào danh sách để đọc lại
                        fileQuery.Add(f.FullName);
                foreach (string s in fileQuery)
                {
                    
                    Change.Invoke(_fileWatcher, new FileSystemEventArgs(WatcherChangeTypes.Changed, WatchedPath, s.Substring(s.LastIndexOf('\\') + 1)));
                    _lastModifitime = new FileInfo(s).LastWriteTime;
                }
            }
            catch
            {
            }
            finally
            {
                _myTimer.Start();
            }
        }

        /// <summary>
        ///     Lấy về thời gian thay đổi file cuối cùng
        /// </summary>
        /// <returns></returns>
        private DateTime GetlastModifiedTime()
        {
            try
            {
                 IEnumerable<FileInfo> fileList =null;
                using (new NetworkConnection(WatchedPath, Utility.CreateCredentials(Utility.Laygiatrithamsohethong("ASTM_UID", "UserName", false), Utility.Laygiatrithamsohethong("ASTM_PWD", "PassWord", false))))
              fileList = _watcherPathInfo.GetFiles("*.txt",
                    SearchOption.TopDirectoryOnly);
                // Nếu không tồn tại file nào trả về thời gian hiện tại
                if (!fileList.Any()) return DateTime.Now;
                
                // Nếu có file trả về thời gian thay đổi của file cuối cùng
                return (from file in fileList
                        orderby file.LastWriteTime descending
                        select file.LastWriteTime).FirstOrDefault();
                
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
        #endregion
    }
}