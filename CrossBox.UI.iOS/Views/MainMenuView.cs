using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using CrossBox.Core.ViewModels;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Binding.Interfaces;
using Cirrious.MvvmCross.Views;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using System.Collections.Generic;
using MonoTouch.Foundation;
using System.Drawing;

namespace CrossBox.UI.iOS
{
	public class MainMenuView : 
		MvxBindingTouchTableViewController<MainMenuViewModel>, 
		IMvxServiceConsumer<IMvxBinder>
	{
		public MainMenuView (MvxShowViewModelRequest request) :
			base(request)
		{
		}

		private UIActivityIndicatorView _activityIndicator;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "CrossBox";

			var tableSource = new TableViewSource (TableView);
			tableSource.SelectionChanged += (sender, e) => ViewModel.SelectItemCommand.Execute (e.AddedItems [0]);

			_activityIndicator = new UIActivityIndicatorView (View.Frame)
			{
				ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray
			};
			Add (_activityIndicator);
			View.BringSubviewToFront (_activityIndicator);

			this.AddBindings (new Dictionary<object, string>
			                 {
								{ tableSource, "{'ItemsSource':{'Path':'FolderContents'}}"},
								{ _activityIndicator, "{'Hidden':{'Path':'IsLoading', 'Converter':'InverseVisibility'}}" }
							 }
			);

			TableView.Source = tableSource;
			TableView.ReloadData ();
		}


		public class TableViewSource : MvxBindableTableViewSource
		{
			readonly static NSString CellId = new NSString ("DropBoxItemTableCell");

			public TableViewSource (UITableView tableView) : base(tableView)
			{
			}

			protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
			{
				var reuseCell = tableView.DequeueReusableCell (CellId);
				if (reuseCell != null) {
					return reuseCell;
				}

				var newCell = new TableViewCell (UITableViewCellStyle.Subtitle, CellId);
				return newCell;
			}

		}

		public class TableViewCell : MvxBindableTableViewCell
		{
			const string Binding = @"{'TitleText':{'Path':'Name'}, 'DetailText':{'Path':'FullPath'}}";

			public TableViewCell (UITableViewCellStyle style, NSString cellId) : 
				base(Binding, style, cellId)
			{
				Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
		}
	}
}

