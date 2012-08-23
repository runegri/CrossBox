using CrossBox.Core.DropBox;
using NUnit.Framework;

namespace CrossBox.Core.Tests.DropBox
{
    [TestFixture]
    public class DropBoxFolder_Tests
    {
        [Test]
        public void Assure_ItemType_For_DropBoxFolder_Is_Folder()
        {
            var folder = new DropBoxFolder("path", "name");
            Assert.That(folder.ItemType, Is.EqualTo(DropBoxItemType.Folder));
        }
    }
}