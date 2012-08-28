using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.ExtensionMethods;

namespace CrossBox.Core
{
    public class CrossBoxApp : MvxApplication, IMvxServiceProducer<IMvxStartNavigation>
    {
        public CrossBoxApp()
        {
            var startApplication = new StartApplication();
            this.RegisterServiceInstance<IMvxStartNavigation>(startApplication);
        }

        
    }
}
