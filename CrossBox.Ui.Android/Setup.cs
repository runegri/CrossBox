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
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Binding.Android;
using CrossBox.Core;

namespace CrossBox.Ui.Android
{
    public class Setup : MvxBaseAndroidBindingSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override MvxApplication CreateApp()
        {
            return new CrossBoxApp();
        }
    }
}