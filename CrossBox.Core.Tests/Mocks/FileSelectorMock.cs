using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossBox.Core.Services;

namespace CrossBox.Core.Tests.Mocks
{
    public class FileSelectorMock : IFileSelector
    {
        private readonly SelectedFile _selectedFile;

        public FileSelectorMock(SelectedFile selectedFile)
        {
            _selectedFile = selectedFile;
        }

        public SelectedFile SelectFile()
        {
            return _selectedFile;
        }
    }
}
