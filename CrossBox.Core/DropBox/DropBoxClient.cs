using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DropNet;
using DropNet.Exceptions;

namespace CrossBox.Core.DropBox
{
    public abstract class DropboxClient : IDropBoxClient
    {
        public static DropboxClient Instance { get; protected set; }

        private DropNetClient _client;
        protected DropNetClient Client
        {
            get
            {
                if (_client == null)
                {
                    if (IsAuthenticated)
                    {
                        _client = new DropNetClient(ApiKey, AppSecret, UserToken, UserSecret);
                    }
                    else
                    {
                        _client = new DropNetClient(ApiKey, AppSecret);
                    }
                }
                return _client;
            }
        }

        protected DropboxClient(string apiKey, string appSecret)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Can only create one instance!");
            }

            ApiKey = apiKey;
            AppSecret = appSecret;
        }

        public string ApiKey { get; private set; }
        public string AppSecret { get; private set; }

        public abstract string UserSecret { get; set; }
        public abstract string UserToken { get; set; }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(UserToken) && !string.IsNullOrEmpty(UserSecret); }
        }

        public abstract void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError);

        public void GetFolderContent(string folder, Action<IEnumerable<DropBoxItem>> onSuccess, Action<Exception> onError)
        {
            if (!IsAuthenticated)
            {
                var exception = new DropboxException("Not authenticated");
                onError(exception);
                return;
            }

            Client.GetMetaDataAsync(folder,
                                     metaData =>
                                     {
                                         var contents = metaData
                                             .Contents
                                             .Select(DropBoxItem.FromMetaData)
                                             .ToList();

                                         onSuccess(contents);
                                     },
                                     dropBoxException => onError(dropBoxException));
        }


        public void GetFileContent(string path, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            Client.GetFileAsync(path,
                                response =>
                                {
                                    var fileContent = new DropBoxFile(Path.GetFileName(path), path, response.RawBytes);
                                    onSuccess(fileContent);
                                },
                                dropBoxException => onError(dropBoxException));
        }

        public void UploadFile(string path, string fileName, byte[] content, Action<DropBoxFile> onSuccess, Action<Exception> onError)
        {
            Client.UploadFileAsync(path, fileName, content,
                metaData =>
                {
                    var dropBoxFile = new DropBoxFile(fileName, path, content);
                    onSuccess(dropBoxFile);
                },
                dropBoxException => onError(dropBoxException)
                );
        }

        public virtual void AuthenticatedCallback() { }
    }
}