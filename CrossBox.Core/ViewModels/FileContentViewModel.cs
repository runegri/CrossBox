using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using CrossBox.Core.DropBox;
using Cirrious.MvvmCross.ExtensionMethods;

namespace CrossBox.Core.ViewModels
{
    public class FileContentViewModel : CrossBoxViewModel, IMvxServiceConsumer<IDropBoxClient>
    {
        public FileContentViewModel(string fileName)
        {
            FileName = fileName;
            IsLoading = true;
            Client.GetFileContent(fileName, FileContentLoaded, ReportError);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                FirePropertyChanged(() => IsLoading);
            }
        }

        public string FileName { get; private set; }
        private string _content;
        public string Content
        {
            get { return _content; }
            private set
            {
                _content = value;
                FirePropertyChanged(() => Content);
            }
        }

        private void FileContentLoaded(DropBoxFile file)
        {
            if (file.HasContent)
            {
                Content = file.ContentAsText;
            }
            IsLoading = false;
        }

        private IDropBoxClient Client
        {
            get { return this.GetService<IDropBoxClient>(); }
        }
    }
}