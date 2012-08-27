using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossBox.Core.DropBox;

namespace CrossBox.Core.ViewModels
{
    public class FileContentViewModel : CrossBoxViewModel
    {
        private readonly DropBoxFile _dropBoxFile;

        public FileContentViewModel(DropBoxFile dropBoxFile)
        {
            _dropBoxFile = dropBoxFile;
        }

        public string FileName { get { return _dropBoxFile.FullPath; } }
        public string Content { get { return _dropBoxFile.ContentAsText; } }
    }
}
