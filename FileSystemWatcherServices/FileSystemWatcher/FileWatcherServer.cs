using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using NLog;

namespace VMS.FSW
{
    public abstract class FileWatcherServer : IFileWatcherServer, IFileWatcherServerHandler
    {
        #region Implementation of IFileWatcherServer

        //Biến lưu danh sách các đường dẫn được theo dõi
        private readonly List<WatcherObject> _watchers = new List<WatcherObject>();
        public Logger myLog = LogManager.GetLogger("ASTM_AnalysisService");
        //Biến lưu danh sách các Thread khởi tạo:
        private readonly List<Thread>  _threads = new List<Thread>();

        public void AddWatcher(string folderPath,int intervalTime)
        {
            try
            {
                var watcherObject = new WatcherObject(folderPath, intervalTime);
                watcherObject.Change += OnChanged;
                watcherObject.Rename += OnRenamed;
                _watchers.Add(watcherObject);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddWatcher(string folderPath)
        {
            try
            {
                var watcherObject = new WatcherObject(folderPath);
                watcherObject.Change += OnChanged;
                watcherObject.Rename += OnRenamed;
                _watchers.Add(watcherObject);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StartServer()
        {
            try
            {
                foreach (WatcherObject watcher in _watchers)
                {
                    var watcherThread = new Thread(watcher.StartWatch);
                    _threads.Add(watcherThread);
                    watcherThread.Start();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StopServer()
        {
            try
            {
                foreach (Thread thread in _threads)
                {
                    try
                    {
                        thread.Abort();
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Implementation of IFileWatcherServerHandler

        public abstract void OnChanged(object source, FileSystemEventArgs e);
        public abstract void OnRenamed(object source, RenamedEventArgs e);

        #endregion
    }
}