using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Webkit;
using CrossBox.Core.DropBox;

namespace CrossBox.Core.Android.DropBox
{
    public class AndroidDropBoxClient : DropboxClient
    {
        private readonly Context _context;

        private const int AuthenticateReturn = 99999;

        private ISharedPreferences _preferences;
        private ISharedPreferences Preferences
        {
            get
            {
                return _preferences ??
                      (_preferences = PreferenceManager.GetDefaultSharedPreferences(_context));
            }
        }

        private Action _onSuccess;
        private Action<Exception> _onError;

        public static DropboxClient CreateInstance(string apiKey, string appSecret, Context context)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Instance already created");
            }
            Instance = new AndroidDropBoxClient(apiKey, appSecret, context);
            return Instance;
        }

        private AndroidDropBoxClient(string apiKey, string appSecret, Context context)
            : base(apiKey, appSecret)
        {
            _context = context;
        }

        public override string UserToken
        {
            get { return Preferences.GetString("UserToken" + ApiKey, null); }
            set
            {
                var editor = Preferences.Edit();
                editor.PutString("UserToken" + ApiKey, value);
                editor.Commit();
            }
        }
        public override string UserSecret
        {
            get { return Preferences.GetString("UserSecret" + ApiKey, null); }
            set
            {
                var editor = Preferences.Edit();
                editor.PutString("UserSecret" + ApiKey, value);
                editor.Commit();
            }
        }

        public override void AuthenticatedCallback()
        {
            if (_onSuccess == null || _onError == null)
            {
                CleanUpHandlers();
                throw new InvalidOperationException("Activity result without callbacks");
            }

            Client.GetAccessTokenAsync(
                userToken =>
                {
                    UserToken = userToken.Token;
                    UserSecret = userToken.Secret;
                    _onSuccess();
                    CleanUpHandlers();
                },
                tokenException =>
                {
                    _onError(tokenException);
                    CleanUpHandlers();
                });
        }

        private void CleanUpHandlers()
        {
            _onSuccess = null;
            _onError = null;
        }

        public override void EnsureIsAuthenticated(Action onSuccess, Action<Exception> onError)
        {
            if (IsAuthenticated)
            {
                onSuccess();
                return;
            }

            _onSuccess = onSuccess;
            _onError = onError;

            Client.GetTokenAsync(
                userLogin =>
                {
                    var url = Client.BuildAuthorizeUrl(userLogin);
                    var intent = new Intent(_context, typeof(AuthenticateAppActivity));
                    intent.PutExtra("url", url);
                    intent.SetFlags(ActivityFlags.NewTask);


                    _context.StartActivity(intent);
            //_context.StartActivityForResult(intent, AuthenticateReturn);
                },
                error => { });
        }

    }

    [Activity(LaunchMode = LaunchMode.SingleInstance)]
    public class AuthenticateAppActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var browser = new WebView(this);
            browser.SetWebViewClient(new AuthenticateAppWebViewClient(this));
            browser.Settings.JavaScriptEnabled = true;

            AddContentView(browser,
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent));

            var url = Intent.GetStringExtra("url");
            browser.LoadUrl(url);
        }

        private class AuthenticateAppWebViewClient : WebViewClient
        {
            private readonly AuthenticateAppActivity _authenticateAppActivity;

            public AuthenticateAppWebViewClient(AuthenticateAppActivity authenticateAppActivity)
            {
                _authenticateAppActivity = authenticateAppActivity;
            }

            public override void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);

                if (url.EndsWith("/1/oauth/authorize"))
                {
                    DropboxClient.Instance.AuthenticatedCallback();
                    _authenticateAppActivity.Finish();
                }
            }
        }
    }

}