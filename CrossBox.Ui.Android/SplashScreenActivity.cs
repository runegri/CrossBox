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
using Cirrious.MvvmCross.Android.Views;

namespace CrossBox.Ui.Android
{
    [Activity(Label = "CrossBox", MainLauncher = true)]
    public class SplashScreenActivity : MvxBaseSplashScreenActivity
    {
        public SplashScreenActivity()
            : base(Resource.Layout.SplashScreen)
        {

        }
    }
}