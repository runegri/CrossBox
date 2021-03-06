using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using CrossBox.Core.ViewModels;
using Cirrious.MvvmCross.Views;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using System.Collections.Generic;
using System.Drawing;

namespace CrossBox.UI.iOS
{
	public class FileContentView : MvxBindingTouchViewController<FileContentViewModel>
	{


		UITextView _textView;
		UIActivityIndicatorView _activityIndicator;

		public FileContentView (MvxShowViewModelRequest request) :
			base(request)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = ViewModel.FileName;

			_textView = new UITextView ();
			_textView.Frame = new RectangleF(0,0,320,480);
			Add (_textView);

			_activityIndicator = new UIActivityIndicatorView (View.Frame)
			{
				ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray
			};
			Add (_activityIndicator);
			View.BringSubviewToFront (_activityIndicator);


			this.AddBindings (new Dictionary<object, string>
			    {
					{_textView, "{'Text':{'Path':'Content'}}"},
					{ _activityIndicator, "{'Hidden':{'Path':'IsLoading', 'Converter':'InverseVisibility'}}" }
				});

		}
	}
}

