using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public MainMenuViewModel()
        {
            _folderContents = new ObservableCollection<DropBoxObjectViewModel>();
            SelectFolder("");
        }

        public void SelectFolder(string folder, Action onDone = null)
        {
            AuthenticateAndRun(() =>
                Client.GetFolderContent(folder,
                contents => 
                {
                    
                    FolderContents.Clear();
                    contents
                        .Select(item => new DropBoxObjectViewModel(item))
                        .ToList()
                        .ForEach(FolderContents.Add);

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

        private readonly ObservableCollection<DropBoxObjectViewModel> _folderContents;
        public ObservableCollection<DropBoxObjectViewModel> FolderContents { get { return _folderContents; } }

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
                RequestNavigate<FileContentViewModel>(new {fileName = selectedObject.FullPath});
            }
        }
    }
}
