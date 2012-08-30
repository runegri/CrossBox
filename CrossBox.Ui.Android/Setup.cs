using Android.App;
using Android.Content;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Binding.Android;
using Cirrious.MvvmCross.Converters.Visibility;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using CrossBox.Core;
using CrossBox.Core.Android.DropBox;
using CrossBox.Core.DropBox;

namespace CrossBox.Ui.Android
{
    public class Setup : MvxBaseAndroidBindingSetup, IMvxServiceProducer<IDropBoxClient>
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override MvxApplication CreateApp()
        {
            this.RegisterServiceInstance<IDropBoxClient>(AndroidDropBoxClient.CreateInstance(CrossBoxApp.AppKey, CrossBoxApp.AppSecret, ApplicationContext));
            return new CrossBoxApp();
        }

        public class Converters
        {
            public readonly MvxVisibilityConverter Visibility = new MvxVisibilityConverter();
            public readonly MvxInvertedVisibilityConverter InverseVisibility = new MvxInvertedVisibilityConverter();
        }

        protected override System.Collections.Generic.IEnumerable<System.Type> ValueConverterHolders
        {
            get { return new[] { typeof(Converters) }; }
        }
    }
}