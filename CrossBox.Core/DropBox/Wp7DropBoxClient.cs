using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace CrossBox.Core.DropBox
{
    public class Wp7DropBoxClient : DropboxClient
    {
        public static DropboxClient CreateInstance(string apiKey, string appSecret)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Instance already created");
            }
            Instance = new Wp7DropBoxClient(apiKey, appSecret);
            return Instance;
        }

        private Wp7DropBoxClient(string apiKey, string appSecret)
            : base(apiKey, appSecret)
        { }

        public override string UserSecret
        {
            get
            {
                object userSecret;
                if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue("UserSecret" + ApiKey, out userSecret))
                {
                    return null;
                }
                return userSecret as string;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["UserSecret" + ApiKey] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        public override string UserToken
        {
            get
            {
                object userToken;
                if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue("UserToken" + ApiKey, out userToken))
                {
                    return null;
                }
                return userToken as string;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["UserToken" + ApiKey] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
        public override void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError)
        {
            if (IsAuthenticated)
            {
                onSuccess();
                return;
            }

            var browser = new WebBrowser { IsScriptEnabled = true };

            var rootVisual = (ContentControl)Application.Current.RootVisual;
            var oldChildren = rootVisual.Content;
            browser.LoadCompleted += (sender, args) =>
                                         {
                                             if (args.Uri.AbsolutePath.EndsWith("/1/oauth/authorize") && !IsAuthenticated)
                                             {
                                                 Client.GetAccessTokenAsync(
                                                     userToken =>
                                                     {
                                                         UserToken = userToken.Token;
                                                         UserSecret = userToken.Secret;
                                                         rootVisual.Content = oldChildren;
                                                         onSuccess();
                                                     },
                                                     tokenException => { /* Do nothing... */ }
                                                     );
                                             }
                                         };

            rootVisual.Content = browser;

            Client.GetTokenAsync(
                userLogin =>
                {
                    var uri = new Uri(Client.BuildAuthorizeUrl(userLogin));
                    browser.Navigate(uri);
                },
                dropboxException => onError(dropboxException));
        }
    }
}
