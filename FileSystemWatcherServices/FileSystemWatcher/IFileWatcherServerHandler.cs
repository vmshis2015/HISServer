using System.IO;

namespace VMS.FSW
{
    internal interface IFileWatcherServerHandler
    {
        void OnChanged(object source, FileSystemEventArgs e);
        void OnRenamed(object source, RenamedEventArgs e);
    }
}