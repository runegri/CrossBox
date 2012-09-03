using System;
using Cirrious.MvvmCross.Dialog.Touch;
using CrossBox.Core;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Interfaces;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.ExtensionMethods;
using CrossBox.Core.DropBox;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Converters.Visibility;

namespace CrossBox.UI.iOS
{
	public class Setup : MvxTouchDialogBindingSetup, IMvxServiceProducer<IDropBoxClient>
	{

		private readonly UIWindow _window;

		public Setup (MvxApplicationDelegate appDelegate, IMvxTouchViewPresenter presenter, UIWindow window) 
			: base(appDelegate, presenter)
		{
			_window = window;
		}

		protected override void InitializeIoC ()
		{
			base.InitializeIoC ();
			this.RegisterServiceInstance<IDropBoxClient> (
				MonoTouchDropBoxClient.CreateInstance (CrossBoxApp.AppKey, CrossBoxApp.AppSecret, _window));
		}

		protected override Cirrious.MvvmCross.Application.MvxApplication CreateApp ()
		{
			return new CrossBoxApp ();
		}

		protected override System.Collections.Generic.IEnumerable<Type> ValueConverterHolders {
			get {
				return new[] { typeof(Converters)};
			}
		}

		public class Converters
		{
			public readonly MvxInvertedVisibilityConverter InverseVisibility = new MvxInvertedVisibilityConverter();
		}
	}
}

