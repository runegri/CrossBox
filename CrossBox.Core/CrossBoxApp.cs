using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.ExtensionMethods;

namespace CrossBox.Core
{
    public class CrossBoxApp : MvxApplication, IMvxServiceProducer<IMvxStartNavigation>
    {
        public const string AppKey = "6klylndnjaxt7z2";
        public const string AppSecret = "6cz7plryu0i5l54";
     
        public CrossBoxApp()
        {
            var startApplication = new StartApplication();
            this.RegisterServiceInstance<IMvxStartNavigation>(startApplication);
        }

        
    }
}
