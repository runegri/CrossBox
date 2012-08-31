using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ExtensionMethods;

namespace CrossBox.UI.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate, IMvxServiceConsumer<IMvxStartNavigation>
	{

		UIWindow _window;

		public override bool FinishedLaunching (UIApplication application, NSDictionary launcOptions)
		{
			_window = new UIWindow(UIScreen.MainScreen.Bounds);

			var presenter = new MvxTouchViewPresenter(this, _window);
			var setup = new Setup(this, presenter, _window);
			setup.Initialize();

			var start = this.GetService<IMvxStartNavigation>();
			start.Start();

			_window.RootViewController = new UINavigationController();
			_window.MakeKeyAndVisible();

			return true;
		}

	}
}

