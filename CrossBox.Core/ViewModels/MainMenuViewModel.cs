using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Commands;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using CrossBox.Core.DropBox;
using CrossBox.Core.Services;

namespace CrossBox.Core.ViewModels
{
    public class MainMenuViewModel : CrossBoxViewModel, IMvxServiceConsumer<IDropBoxClient>, IMvxServiceConsumer<IFileSelector>
    {        
        public MainMenuViewModel(string folder = "/")
        {
            _folderContents = new ObservableCollection<DropBoxObjectViewModel>();
            SelectFolder(folder ?? "/");
        }

        public void SelectFolder(string folder, Action onDone = null)
        {
            FolderContents.Clear();
            IsLoading = true;
            AuthenticateAndRun(() =>
                Client.GetFolderContent(folder,
                contents => InvokeOnMainThread(() => 
                {
                    contents
                        .Select(item => new DropBoxObjectViewModel(item))
                        .ToList()
                        .ForEach(FolderContents.Add);

                    FolderName = folder;
                    IsLoading = false;

                    if (onDone != null)
                    {
                        onDone();
                    }
                }),
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

        private void ItemSelected(DropBoxObjectViewModel selectedObject)
        {
            if (selectedObject.IsDirectory)
            {
                RequestNavigate<MainMenuViewModel>(new {folder = selectedObject.FullPath});
            }
            else
            {
                RequestNavigate<FileContentViewModel>(new { fileName = selectedObject.FullPath });
            }
        }

        public IMvxCommand UploadFileCommand
        {
            get { return new MvxRelayCommand(UploadFile); }
        }

        private void UploadFile()
        {
            var file = this.GetService<IFileSelector>().SelectFile();
            this.GetService<IDropBoxClient>().UploadFile(FolderName, file.FileName, file.FileData, FileUploaded, ReportError);
        }

        private void FileUploaded(DropBoxFile file)
        {
            SelectFolder(FolderName);
        }
    }
}
