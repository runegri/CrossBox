using System;
using Cirrious.MvvmCross.IoC;
using Cirrious.MvvmCross.ViewModels;
using CrossBox.Core.Services;

namespace CrossBox.Core.ViewModels
{
    public abstract class CrossBoxViewModel : MvxViewModel
    {

        public MvxOpenNetCfContainer Container
        {
            get { return MvxOpenNetCfContainer.Current; }
        }

        public void ReportError(Exception exception)
        {
            Container.Resolve<IErrorReporter>().ReportError(exception);
        }

    }
}
