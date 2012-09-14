using System.Text;
using CrossBox.Core.DropBox;
using CrossBox.Core.Services;
using CrossBox.Core.Tests.Mocks;
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

        private MockSetup _setup;
        private IDropBoxClient _client;
        private IFileSelector _fileSelector = new FileSelectorMock(null);

        [SetUp]
        public void SetUp()
        {
            _client = new DropBoxClientMock_ReturnsFileContent();
            _setup = new MockSetup(_client, _fileSelector);
            _setup.Initialize();
            _viewModel = new FileContentViewModel("file.txt");
        }

        [TearDown]
        public void TearDown()
        {
            if (_setup != null)
            {
                _setup.Dispose();
            }
        }


        [Test]
        public void Assure_FileContentViewModel_Wraps_FullName_Property()
        {
            _client.GetFileContent("file.txt",
                file => Assert.That(_viewModel.FileName, Is.EqualTo(file.FullPath)),
                ex => { });
        }

        [Test]
        public void Assure_FileContentViewModel_Wraps_Content_Property()
        {
            _client.GetFileContent("file.txt",
                file => Assert.That(_viewModel.Content, Is.EqualTo(file.ContentAsText)),
                ex => { });
        }

        [Test]
        public void Assure_Content_Property_Does_Not_Fail_For_Empty_File()
        {
            _client.GetFileContent("empty.txt",
                file => Assert.DoesNotThrow(() => { var tmp = _viewModel.Content; }),
                ex => { });

        }
    }
}
