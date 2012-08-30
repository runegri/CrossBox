using Android.App;
using Android.Content;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Binding.Android;
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
    }
}