using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrossBox.Core.Services
{
    public interface IErrorReporter
    {
        void ReportError(Exception exception);
    }
}
