using Android.App;
using Cirrious.MvvmCross.Binding.Android.Views;
using CrossBox.Core.ViewModels;

namespace CrossBox.Ui.Android.Views
{
    [Activity]
    public class FileContentView : MvxBindingActivityView<FileContentViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Page_FileContentView);
        }
    }
}