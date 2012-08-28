using System.Text;
using CrossBox.Core.DropBox;
using CrossBox.Core.ViewModels;
using NUnit.Framework;

namespace CrossBox.Core.Tests.ViewModels
{
    [TestFixture]
    public class FileContentViewModel_Tests
    {
        const string ContentString = "content here!";
        readonly byte[] _contentBytes = Encoding.UTF8.GetBytes(ContentString);
        private FileContentViewModel _viewModel;
        private DropBoxFile _file;

        [SetUp]
        public void SetUp()
        {
            _file = new DropBoxFile("/path/file.txt", "file.txt", _contentBytes);
            _viewModel = new FileContentViewModel(_file);
        }

        [Test]
        public void Assure_FileContentViewModel_Wraps_FullName_Property()
        {
            Assert.That(_viewModel.FileName, Is.EqualTo(_file.FullPath));
        }

        [Test]
        public void Assure_FileContentViewModel_Wraps_Content_Property()
        {
            Assert.That(_viewModel.Content, Is.EqualTo(_file.ContentAsText));
        }

        [Test]
        public void Assure_Content_Property_Does_Not_Fail_For_Empty_File()
        {
            var file = new DropBoxFile("/path/file.txt", "file.txt");
            var viewModel = new FileContentViewModel(file);

            Assert.DoesNotThrow(() => { var tmp = viewModel.Content; });
        }

    }
}
