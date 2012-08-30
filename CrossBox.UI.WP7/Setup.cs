using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using CrossBox.Core;
using CrossBox.Core.DropBox;
using Microsoft.Phone.Controls;

namespace CrossBox.UI.WP7
{
    public class Setup : MvxBaseWindowsPhoneSetup, IMvxServiceProducer<IDropBoxClient>
    {
        public Setup(PhoneApplicationFrame rootFrame)
            : base(rootFrame)
        {
            
        }

        protected override MvxApplication CreateApp()
        {
            this.RegisterServiceInstance<IDropBoxClient>(Wp7DropBoxClient.CreateInstance(CrossBoxApp.AppKey, CrossBoxApp.AppSecret));
            return new CrossBoxApp();
        }
    }
}
