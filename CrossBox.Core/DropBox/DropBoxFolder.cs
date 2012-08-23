namespace CrossBox.Core.DropBox
{
    public class DropBoxFolder : DropBoxItem
    {
        public DropBoxFolder(string fullPath, string name)
            : base(fullPath, name, DropBoxItemType.Folder)
        { }
    }
}