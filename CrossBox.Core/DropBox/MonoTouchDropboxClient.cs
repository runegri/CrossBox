using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using DropNet;

namespace CrossBox.Core.DropBox
{
	public class MonoTouchDropBoxClient : DropboxClient
	{

		private UIViewController _viewController;
		private Action _onSuccess;
		private Action<Exception> _onError;

		public static DropboxClient CreateInstance(string apiKey, string appSecret, UIWindow window)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Instance already created");
            }
            Instance = new MonoTouchDropBoxClient(apiKey, appSecret, window);
            return Instance;
        }

		private readonly UIWindow _window;

		private MonoTouchDropBoxClient (string apiKey, string appSecret, UIWindow window) : base(apiKey, appSecret)
		{
			_window = window;
		}

		public override string UserSecret {
			get { return NSUserDefaults.StandardUserDefaults.StringForKey("UserSecret" + ApiKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString(value, "UserSecret" + ApiKey); }
		}

		public override string UserToken {
			get { return NSUserDefaults.StandardUserDefaults.StringForKey("UserToken" + ApiKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString(value, "UserToken" + ApiKey); }
		}

		public override void EnsureIsAuthenticated (Action onSuccess, Action<Exception> onError)
		{
			if(IsAuthenticated)
			{
				onSuccess();
				return;
			}

			_onSuccess = onSuccess;
			_onError = onError;

			_viewController = _window.RootViewController;

			Client.GetTokenAsync(
				userLogin => 
				{	
					_window.InvokeOnMainThread(() => 
					{
						var uri = new NSUrl(Client.BuildAuthorizeUrl(userLogin));
						var viewController = new CrossBoxBrowserViewController(Client, uri);
						_window.RootViewController = viewController;
					});
				},
				tokenException => onError(tokenException));

		}

		public override void AuthenticatedCallback ()
		{
			_window.RootViewController = _viewController;

			if(_onSuccess == null || _onError == null)
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
				tokenExcpetion => 
				{
					_onError(tokenExcpetion);
					CleanUpHandlers();
				});

		}

		private void CleanUpHandlers()
		{
			_onSuccess = null;
			_onError = null;
		}

	}

	public partial class CrossBoxBrowserViewController : UIViewController
	{

		private readonly DropNetClient _dropboxClient;
		private readonly NSUrl _url;

		public CrossBoxBrowserViewController (DropNetClient dropboxClient, NSUrl url)
		{
			_dropboxClient = dropboxClient;
			_url = url;
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InvokeOnMainThread(() => 
            {
				var webView = new UIWebView();
				webView.Frame = new System.Drawing.RectangleF(0,0, 320, 480);
				webView.LoadFinished += OnLoadFinished;
				this.Add(webView);

				webView.LoadRequest(new NSUrlRequest(_url));

				Title = "Authenticate with Dropbox";
			});

		}

		void OnLoadFinished (object sender, EventArgs e)
		{

			var webView = (UIWebView)sender;
			var url = webView.Request.Url.AbsoluteString;

			if(url.EndsWith("/1/oauth/authorize"))
			{
				DropboxClient.Instance.AuthenticatedCallback();
				Dispose();
			}
		}
	}
}



