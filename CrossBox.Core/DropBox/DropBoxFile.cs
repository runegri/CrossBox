using System.Text;

namespace CrossBox.Core.DropBox
{
    public class DropBoxFile : DropBoxItem
    {
        private readonly byte[] _content;

        public DropBoxFile(string fullPath, string name)
            : base(fullPath, name, DropBoxItemType.File)
        {
        }

        public DropBoxFile(string fullPath, string name, byte[] content)
            : this(fullPath, name)
        {
            _content = content;
        }

        public bool HasContent { get { return _content != null && _content.Length > 0; } }
        public byte[] Content { get { return _content; } }
        public string ContentAsText { get { return HasContent ? Encoding.UTF8.GetString(_content) : string.Empty; } }
    }
}