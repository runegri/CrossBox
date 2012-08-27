using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.Console.Platform;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.Views;
using CrossBox.Core.DropBox;
using CrossBox.Core.Services;

namespace CrossBox.Core.Tests.Mocks
{
    public class MockSetup : MvxBaseConsoleSetup
    {
        private readonly IDropBoxClient _dropBoxClient;
        private readonly Action<Exception> _reportErrorAction;
        private readonly ViewDispatcherMock _dispatcher = new ViewDispatcherMock();

        public MockSetup(IDropBoxClient dropBoxClient, Action<Exception> reportErrorAction = null)
        {
            _dropBoxClient = dropBoxClient;
            _reportErrorAction = reportErrorAction;
        }

        protected override MvxApplication CreateApp()
        {
            return new MockApplication(new ErrorReporterMock(_reportErrorAction), _dropBoxClient);
        }

        protected override IMvxViewDispatcherProvider CreateViewDispatcherProvider()
        {
            return _dispatcher;
        }

        public ViewDispatcherMock Dispatcher { get { return _dispatcher; }}
    }

    public class MockApplication : 
        MvxApplication, 
        IMvxServiceProducer<IErrorReporter>, 
        IMvxServiceProducer<IDropBoxClient>
    {

        public MockApplication(IErrorReporter errorReporter, IDropBoxClient dropBoxClient)
        {
            this.RegisterServiceInstance(errorReporter);
            this.RegisterServiceInstance(dropBoxClient);
        }
    }
}
