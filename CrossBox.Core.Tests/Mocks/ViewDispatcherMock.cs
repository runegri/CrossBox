using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Interfaces.Views;
using Cirrious.MvvmCross.Views;

namespace CrossBox.Core.Tests.Mocks
{
    public class ViewDispatcherMock : IMvxViewDispatcher, IMvxViewDispatcherProvider
    {
        private readonly List<MvxShowViewModelRequest> _requests = new List<MvxShowViewModelRequest>();

        public bool RequestMainThreadAction(Action action)
        {
            action();
            return true;
        }

        public List<MvxShowViewModelRequest> Requests {get { return _requests; }}

        public bool RequestNavigate(MvxShowViewModelRequest request)
        {
            Requests.Add(request);
            return true;
        }

        public bool RequestClose(IMvxViewModel whichViewModel)
        {
            throw new NotImplementedException();
        }

        public bool RequestRemoveBackStep()
        {
            throw new NotImplementedException();
        }

        public IMvxViewDispatcher Dispatcher
        {
            get { return this; }
        }
    }
}
