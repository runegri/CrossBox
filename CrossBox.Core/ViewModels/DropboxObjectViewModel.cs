using Cirrious.MvvmCross.ViewModels;
using CrossBox.Core.DropBox;

namespace CrossBox.Core.ViewModels
{
    public class DropBoxObjectViewModel : MvxViewModel
    {
        private readonly DropBoxItem _item;

        public DropBoxObjectViewModel(DropBoxItem item)
        {
            _item = item;
        }

        public string Name
        {
            get { return _item.Name; }
        }


        public string FullPath
        {
            get { return _item.FullPath; }
        }

        public bool IsDirectory
        {
            get { return _item.ItemType == DropBoxItemType.Folder; }
        }
    }
}