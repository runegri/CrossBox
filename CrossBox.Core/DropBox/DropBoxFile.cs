namespace CrossBox.Core.DropBox
{
    public class DropBoxFile : DropBoxItem
    {
        public DropBoxFile(string fullPath, string name)
            : base(fullPath, name, DropBoxItemType.File)
        {
        }
    }
}