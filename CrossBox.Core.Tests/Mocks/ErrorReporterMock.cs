using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossBox.Core.Services;

namespace CrossBox.Core.Tests.Mocks
{
    public class ErrorReporterMock : IErrorReporter
    {
        private readonly Action<Exception> _reportErrorAction;

        public ErrorReporterMock(Action<Exception> reportErrorAction)
        {
            _reportErrorAction = reportErrorAction;
        }

        public void ReportError(Exception exception)
        {
            _reportErrorAction(exception);
        }
    }
}
