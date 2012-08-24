﻿using System;
using System.Collections.Generic;
using CrossBox.Core.DropBox;

namespace CrossBox.Core.Tests.Mocks
{
    public class DropBoxClientMock_ReturnsFolderContent : IDropBoxClient
    {
        private readonly IEnumerable<DropBoxItem> _folderContent;

        public bool EnsureIsAuthenticatedWasRun;
        public bool GetFolderContentWasRun;
        public bool GetFileContentWasRun;

        public DropBoxClientMock_ReturnsFolderContent(IEnumerable<DropBoxItem> folderContent)
        {
            _folderContent = folderContent;
        }

        public void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError)
        {
            EnsureIsAuthenticatedWasRun = true;
            onSuccess();
        }

        public void GetFolderContent(string folder, Action<IEnumerable<DropBoxItem>> onSuccess, Action<Exception> onError)
        {
            GetFolderContentWasRun = true;
            onSuccess(_folderContent);
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
