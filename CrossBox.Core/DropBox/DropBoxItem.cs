using DropNet.Models;

namespace CrossBox.Core.DropBox
{
    public abstract class DropBoxItem
    {
        private readonly string _fullPath;
        private readonly string _name;
        private readonly DropBoxItemType _itemType;

        protected DropBoxItem(string fullPath, string name, DropBoxItemType itemType)
        {
            _fullPath = fullPath;
            _name = name;
            _itemType = itemType;
        }

        public string FullPath { get { return _fullPath; } }
        public string Name { get { return _name; } }
        public DropBoxItemType ItemType { get { return _itemType; } }

        public static DropBoxItem FromMetaData(MetaData metaData)
        {
            if(metaData.Is_Dir)
            {
                return new DropBoxFolder(metaData.Path, metaData.Name);
            }
            return new DropBoxFile(metaData.Path, metaData.Name);
        }
    }
}
