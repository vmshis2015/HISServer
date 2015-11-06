using System.IO;
using NLog;

namespace VMS.FSW
{

    #region Delegate function to handler event

    public delegate void OnChangeHandler(object source, FileSystemEventArgs e);

    public delegate void OnRenameHandler(object source, RenamedEventArgs e);

    #endregion

    internal interface IFileWatcherServer
    {
        
        #region Properties

        //Biến lưu các đường dẫn cần theo dõi.
        //string[] PathList { get; }

        #endregion

        #region Public Method

        void AddWatcher(string folderPath);
        void StartServer();
        void StopServer();

        #endregion
    }
}