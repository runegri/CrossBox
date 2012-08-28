using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Commands;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using CrossBox.Core.DropBox;

namespace CrossBox.Core.ViewModels
{
    public class MainMenuViewModel : CrossBoxViewModel, IMvxServiceConsumer<IDropBoxClient>
    {

        private const string AppKey = "6klylndnjaxt7z2";
        private const string AppSecret = "6cz7plryu0i5l54";

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
            else
            {
                Client.GetFileContent(
                    selectedObject.FullPath,
                    file => RequestNavigate<FileContentViewModel>(new { dropBoxFile = file }),
                    ReportError);
            }
        }
    }
}
