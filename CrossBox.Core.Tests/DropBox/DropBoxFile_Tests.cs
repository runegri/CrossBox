using CrossBox.Core.DropBox;
using NUnit.Framework;

namespace CrossBox.Core.Tests.DropBox
{
    [TestFixture]
    public class DropBoxFile_Tests
    {

        [Test]
        public void Assure_ItemType_For_DropBoxFile_Is_File()
        {
            var file = new DropBoxFile("path", "name");
            Assert.That(file.ItemType, Is.EqualTo(DropBoxItemType.File));
        }

    }
}
