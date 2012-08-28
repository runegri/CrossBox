using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using CrossBox.Core;
using Microsoft.Phone.Controls;

namespace CrossBox.UI.WP7
{
    public class Setup : MvxBaseWindowsPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame)
            : base(rootFrame)
        {
        }

        protected override MvxApplication CreateApp()
        {
            return new CrossBoxApp();
        }
    }
}
