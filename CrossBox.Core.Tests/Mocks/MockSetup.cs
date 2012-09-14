using System;
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
        private readonly IFileSelector _fileSelector;
        private readonly Action<Exception> _reportErrorAction;
        private readonly ViewDispatcherMock _dispatcher = new ViewDispatcherMock();

        public MockSetup(IDropBoxClient dropBoxClient, IFileSelector fileSelector, Action<Exception> reportErrorAction = null)
        {
            _dropBoxClient = dropBoxClient;
            _fileSelector = fileSelector;
            _reportErrorAction = reportErrorAction;
        }

        protected override MvxApplication CreateApp()
        {
            return new MockApplication(new ErrorReporterMock(_reportErrorAction), _dropBoxClient, _fileSelector);
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
        IMvxServiceProducer<IDropBoxClient>,
        IMvxServiceProducer<IFileSelector>
    {

        public MockApplication(IErrorReporter errorReporter, IDropBoxClient dropBoxClient, IFileSelector fileSelector)
        {
            this.RegisterServiceInstance(errorReporter);
            this.RegisterServiceInstance(dropBoxClient);
            this.RegisterServiceInstance(fileSelector);
        }
    }
}
