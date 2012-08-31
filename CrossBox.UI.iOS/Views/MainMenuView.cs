using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using CrossBox.Core.ViewModels;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Binding.Interfaces;
using Cirrious.MvvmCross.Views;

namespace CrossBox.UI.iOS
{
	public class MainMenuView : 
		MvxBindingTouchTableViewController<MainMenuViewModel>, 
		IMvxServiceConsumer<IMvxBinder>
	{
		public MainMenuView (MvxShowViewModelRequest request) : base(request)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "CrossBox";

			//Bind("Title", this, "FolderName", null);
		}
	}
}

