using System;
using System.Collections.Generic;

namespace CrossBox.Core.DropBox
{
    public interface IDropBoxClient
    {
        void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError);
        void GetFolderContent(string folder, Action<IEnumerable<DropBoxItem>> onSuccess, Action<Exception> onError);
        void GetFileContent(string path, Action<DropBoxFile> onSuccess, Action<Exception> onError);
        void UploadFile(string path, string fileName, byte[] content, Action<DropBoxFile> onSuccess, Action<Exception> onError);
    }
}
