using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using CrossBox.Core;
using CrossBox.Core.DropBox;
using CrossBox.Core.WP7.DropBox;
using Microsoft.Phone.Controls;

namespace CrossBox.UI.WP7
{
    public class Setup : MvxBaseWindowsPhoneSetup, IMvxServiceProducer<IDropBoxClient>
    {

        private const string AppKey = "6klylndnjaxt7z2";
        private const string AppSecret = "6cz7plryu0i5l54";

        public Setup(PhoneApplicationFrame rootFrame)
            : base(rootFrame)
        {
            
        }

        protected override MvxApplication CreateApp()
        {
            this.RegisterServiceInstance<IDropBoxClient>(Wp7DropBoxClient.CreateInstance(AppKey, AppSecret));
            return new CrossBoxApp();
        }
    }
}
