using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.IoC;
using Cirrious.MvvmCross.ViewModels;
using CrossBox.Core.DropBox;

namespace CrossBox.Core.ViewModels
{
    public class MainMenuViewModel : CrossBoxViewModel
    {

        private const string AppKey = "";
        private const string AppSecret = "";



        public MainMenuViewModel()
        {
            _folderContents = new List<DropBoxObjectViewModel>();
        }

        public void SelectFolder(string folder, Action onDone = null)
        {
            Client.GetFolderContent(folder, 
                contents =>
                    {
                        _folderContents.Clear();
                        _folderContents.AddRange(contents.Select(item => new DropBoxObjectViewModel(item)));
                        if (onDone != null)
                        {
                            onDone();
                        }
                    },
                ReportError);
            
            
        }

        private readonly List<DropBoxObjectViewModel> _folderContents;
        public List<DropBoxObjectViewModel> FolderContents { get { return _folderContents; } }

        private IDropBoxClient _client;
        private IDropBoxClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = Container.Resolve<IDropBoxClient>();
                }
                return _client;
            }
        }


    }
}
