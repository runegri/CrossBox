using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Android.Views;
using CrossBox.Core.ViewModels;

namespace CrossBox.Ui.Android.Views
{
    [Activity]
    public class MainMenuView : MvxBindingActivityView<MainMenuViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Page_MainMenuView);
        }
    }
}