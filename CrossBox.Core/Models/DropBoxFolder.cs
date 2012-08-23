namespace CrossBox.Core.Models
{
    public class DropBoxFolder : DropBoxItem
    {
        public DropBoxFolder(string fullPath, string name)
            : base(fullPath, name, DropBoxItemType.Folder)
        { }
    }
}