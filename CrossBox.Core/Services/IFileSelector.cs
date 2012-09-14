using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrossBox.Core.Services
{
    public interface IFileSelector
    {
        void SelectFile(Action<SelectedFile> onFileSelected);
    }

    public class SelectedFile
    {
        public string FileName { get; private set; }
        public byte[] FileData { get; private set; }

        public SelectedFile(string fileName, byte[] fileData)
        {
            FileName = fileName;
            FileData = fileData;
        }
    }
}
