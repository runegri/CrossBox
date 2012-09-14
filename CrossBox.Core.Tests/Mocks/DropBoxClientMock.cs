using System;
using System.Collections.Generic;
using System.IO;
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
            onSuccess(new DropBoxFile(path, Path.GetFileName(path)));
        }

        public void UploadFile(string path, string fileName, byte[] content, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            throw new NotImplementedException();
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

        public void UploadFile(string path, string fileName, byte[] content, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            throw new NotImplementedException();
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

        public void UploadFile(string path, string fileName, byte[] content, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            throw new NotImplementedException();
        }
    }

    public class DropBoxClientMock_ReturnsFileContent : IDropBoxClient
    {
        public void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError)
        {
            onSuccess();
        }

        public void GetFolderContent(string folder, Action<IEnumerable<DropBoxItem>> onSuccess, Action<Exception> onError)
        {
            onSuccess(new DropBoxItem[] { GetFileWithContent() });
        }

        private static DropBoxFile GetFileWithContent()
        {
            var content = System.Text.Encoding.UTF8.GetBytes("content here");
            return new DropBoxFile("file.txt", "file.txt", content);
        }

        private static DropBoxFile GetEmptyFile()
        {
            return new DropBoxFile("empty.txt", "empty.txt");
        }

        public void GetFileContent(string path, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            if (path.Equals("empty.txt"))
            {
                onSuccess(GetEmptyFile());
            }
            else
            {
                onSuccess(GetFileWithContent());
            }
        }

        public void UploadFile(string path, string fileName, byte[] content, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            throw new NotImplementedException();
        }
    }

    public class DropBoxClientMock_StoresUploadedFile : IDropBoxClient
    {
        public DropBoxFile UploadedFile { get; private set; }

        public void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError)
        {
            onSuccess();
        }

        public void GetFolderContent(string folder, Action<IEnumerable<DropBoxItem>> onSuccess, Action<Exception> onError)
        {
            onSuccess(new DropBoxItem[0]);
        }

        public void GetFileContent(string path, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            
        }

        public void UploadFile(string path, string fileName, byte[] content, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            UploadedFile = new DropBoxFile(path, fileName, content);
            onSuccess(UploadedFile);
        }
    }
}
