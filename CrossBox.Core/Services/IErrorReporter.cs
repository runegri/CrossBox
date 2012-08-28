using System;

namespace CrossBox.Core.Services
{
    public interface IErrorReporter
    {
        void ReportError(Exception exception);
    }
}
