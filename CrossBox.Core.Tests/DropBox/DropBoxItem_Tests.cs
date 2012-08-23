using CrossBox.Core.DropBox;
using NUnit.Framework;

namespace CrossBox.Core.Tests.DropBox
{
    [TestFixture]
    public class DropBoxItem_Tests
    {
        [Test]
        public void Assure_Path_Is_Preserved()
        {
            const string path = "Full path here";
            var file = new DropBoxFile(path, "name");
            Assert.That(file.FullPath, Is.EqualTo(path));
        }

        [Test]
        public void Assure_Name_Is_Preserved()
        {
            const string name = "Filename";
            var file = new DropBoxFile("folder", name);
            Assert.That(file.Name, Is.EqualTo(name));
        }

    }
}
