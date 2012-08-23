using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossBox.Core.DropBox;

namespace CrossBox.Core.Tests.Mocks
{
    public class DropBoxClientMock_ReturnsFolderContent : IDropBoxClient
    {
        private readonly IEnumerable<DropBoxItem> _folderContent;

        public DropBoxClientMock_ReturnsFolderContent(IEnumerable<DropBoxItem> folderContent)
        {
            _folderContent = folderContent;
        }

        public void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError)
        {
            onSuccess();
        }

        public void GetFolderContent(string folder, Action<IEnumerable<DropBoxItem>> onSuccess, Action<Exception> onError)
        {
            onSuccess(_folderContent);
        }

        public void GetFileContent(string path, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            throw new NotImplementedException();
        }
    }
}
