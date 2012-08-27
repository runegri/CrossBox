using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Commands;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.IoC;
using Cirrious.MvvmCross.ViewModels;
using CrossBox.Core.DropBox;
using System.Threading;

namespace CrossBox.Core.ViewModels
{
    public class MainMenuViewModel : CrossBoxViewModel, IMvxServiceConsumer<IDropBoxClient>
    {

        private const string AppKey = "";
        private const string AppSecret = "";

        public MainMenuViewModel()
        {
            _folderContents = new List<DropBoxObjectViewModel>();

        }

        public void SelectFolder(string folder, Action onDone = null)
        {
            AuthenticateAndRun(() => 
                Client.GetFolderContent(folder,
                contents =>
                {
                    _folderContents.Clear();
                    _folderContents.AddRange(contents.Select(item => new DropBoxObjectViewModel(item)));
                    FirePropertyChanged(() => FolderContents);

                    FolderName = folder;

                    if (onDone != null)
                    {
                        onDone();
                    }
                },
                ReportError));
        }

        private void AuthenticateAndRun(Action onSuccess)
        {
            Client.EnsureIsAuthenticated(onSuccess, ReportError);
        }

        private readonly List<DropBoxObjectViewModel> _folderContents;
        public List<DropBoxObjectViewModel> FolderContents { get { return _folderContents; } }

        private IDropBoxClient Client
        {
            get { return this.GetService<IDropBoxClient>(); }
        }

        public IMvxCommand SelectItemCommand
        {
            get { return new MvxRelayCommand<DropBoxObjectViewModel>(ItemSelected); }
        }

        private string _folderName;
        public string FolderName
        {
            get { return _folderName; }
            set
            {
                _folderName = value;
                FirePropertyChanged(() => FolderName);
            }
        }

        private void ItemSelected(DropBoxObjectViewModel selectedObject)
        {
            if (selectedObject.IsDirectory)
            {
                SelectFolder(selectedObject.FullPath);
            }
        }
    }
}
