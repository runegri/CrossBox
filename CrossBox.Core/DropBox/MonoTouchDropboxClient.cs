using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using DropNet;

namespace CrossBox.Core.DropBox
{
	public class MonoTouchDropBoxClient : DropboxClient
	{
		private Action _onSuccess;
		private Action<Exception> _onError;

		public static DropboxClient CreateInstance (string apiKey, string appSecret, UIWindow window)
		{
			if (Instance != null) {
				throw new InvalidOperationException ("Instance already created");
			}
			Instance = new MonoTouchDropBoxClient (apiKey, appSecret, window);
			return Instance;
		}

		private readonly UIWindow _window;

		private MonoTouchDropBoxClient (string apiKey, string appSecret, UIWindow window) : base(apiKey, appSecret)
		{
			_window = window;
		}

		public override string UserSecret {
			get { return NSUserDefaults.StandardUserDefaults.StringForKey ("UserSecret" + ApiKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString (value, "UserSecret" + ApiKey); }
		}

		public override string UserToken {
			get { return NSUserDefaults.StandardUserDefaults.StringForKey ("UserToken" + ApiKey); }
			set { NSUserDefaults.StandardUserDefaults.SetString (value, "UserToken" + ApiKey); }
		}

		public override void EnsureIsAuthenticated (Action onSuccess, Action<Exception> onError)
		{
			if (IsAuthenticated) {
				onSuccess ();
				return;
			}

			_onSuccess = onSuccess;
			_onError = onError;

			Client.GetTokenAsync (
				userLogin => 
			{	
				_window.InvokeOnMainThread (() => 
				{
					var uri = new NSUrl (Client.BuildAuthorizeUrl (userLogin));
					var viewController = new CrossBoxBrowserViewController (Client, uri);

//					var parentVC = GetNavController (_window.Subviews [0]);
					_window.GetNavigationController().PushViewController(viewController, true);	
				}
				);
			},
				tokenException => onError (tokenException));

		}

		public override void AuthenticatedCallback ()
		{
			_window.InvokeOnMainThread (() => 
			{
				_window.GetNavigationController().PopViewControllerAnimated (true);
				if (_onSuccess == null || _onError == null) 
				{
					CleanUpHandlers ();
					throw new InvalidOperationException ("Activity result without callbacks");
				}

				Client.GetAccessTokenAsync (
				userToken => 
				{
					UserToken = userToken.Token;
					UserSecret = userToken.Secret;
					_onSuccess ();
					CleanUpHandlers ();
				},
				tokenExcpetion => 
				{
					_onError (tokenExcpetion);
					CleanUpHandlers ();
				}
				);
			}
			);
		}

		private void CleanUpHandlers ()
		{
			_onSuccess = null;
			_onError = null;
		}

	}

	public class CrossBoxBrowserViewController : UIViewController
	{

		private readonly DropNetClient _dropboxClient;
		private readonly NSUrl _url;
		private UIWebView _webView;

		// This constructor will be called by the mono runtime. It creates an instance that is not 
		// to be used...
		public CrossBoxBrowserViewController(IntPtr ptr) {}

		public CrossBoxBrowserViewController (DropNetClient dropboxClient, NSUrl url)
		{
			_dropboxClient = dropboxClient;
			_url = url;
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InvokeOnMainThread (() => 
			{
				_webView = new UIWebView ();
				_webView.Frame = new System.Drawing.RectangleF (0, 0, 320, 480);
				_webView.LoadFinished += OnLoadFinished;
				this.Add (_webView);

				_webView.LoadRequest (new NSUrlRequest (_url));

				Title = "Dropbox Authentication";
			}
			);

		}

		void OnLoadFinished (object sender, EventArgs e)
		{
			var url = _webView.Request.Url.AbsoluteString;

			if (url.EndsWith ("/1/oauth/authorize")) {
				DropboxClient.Instance.AuthenticatedCallback ();
				Dispose ();
			}
		}
	}

	public static class ViewHelper
	{
		
		public static UINavigationController GetNavigationController (this UIView view)
		{
			var nextResponer = view.NextResponder;
			if (nextResponer is UINavigationController) {
				return (UINavigationController)nextResponer;
			}

			if (nextResponer is UIViewController && ((UIViewController)nextResponer).NavigationController != null) {
				return ((UIViewController)nextResponer).NavigationController;
			}

			if (nextResponer is UIView) {
				return GetNavigationController ((UIView)nextResponer);
			}

			if(nextResponer is UIApplication && view.Subviews.Length > 0) {
				return GetNavigationController(view.Subviews[0]);
			}

			return null;
		}


	}
}



