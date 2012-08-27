using System;
using System.Collections.Generic;
using CrossBox.Core.DropBox;

namespace CrossBox.Core.Tests.Mocks
{
    public class DropBoxClientMock_ReturnsFolderContent : IDropBoxClient
    {

        public bool EnsureIsAuthenticatedWasRun;
        public bool GetFolderContentWasRun;
        public bool GetFileContentWasRun;

        private readonly Func<string, IEnumerable<DropBoxItem>> _selectFolderItems;
        
        public DropBoxClientMock_ReturnsFolderContent(Func<string, IEnumerable<DropBoxItem>> selectFolderItems)
        {
            _selectFolderItems = selectFolderItems;
        }

        public void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError)
        {
            EnsureIsAuthenticatedWasRun = true;
            onSuccess();
        }

        public void GetFolderContent(string folder, Action<IEnumerable<DropBoxItem>> onSuccess, Action<Exception> onError)
        {
            if (_selectFolderItems != null)
            {
                var result = _selectFolderItems(folder);
                onSuccess(result);
            }
            else
            {
                onSuccess(new DropBoxItem[0]);
            }
        }

        public void GetFileContent(string path, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            GetFileContentWasRun = true;
        }
    }

    public class DropBoxClientMock_FailsOnGetFolderContent : IDropBoxClient
    {
        private readonly string _errorMessage;

        public DropBoxClientMock_FailsOnGetFolderContent(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError)
        {
        }

        public void GetFolderContent(string folder, Action<IEnumerable<DropBoxItem>> onSuccess, Action<Exception> onError)
        {
            onError(new Exception(_errorMessage));
        }

        public void GetFileContent(string path, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
        }
    }

    public class DropBoxClientMock_FailsOnEnsureIsAuthenticated : IDropBoxClient
    {
        private readonly string _errorMessage;

        public DropBoxClientMock_FailsOnEnsureIsAuthenticated(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError)
        {
            onError(new Exception(_errorMessage));
        }

        public void GetFolderContent(string folder, Action<IEnumerable<DropBoxItem>> onSuccess, Action<Exception> onError)
        {
        }

        public void GetFileContent(string path, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
        }
    }
}
