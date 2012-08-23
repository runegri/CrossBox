using System.Linq;
using Cirrious.MvvmCross.IoC;
using CrossBox.Core.DropBox;
using CrossBox.Core.Tests.Mocks;
using CrossBox.Core.ViewModels;
using NUnit.Framework;

namespace CrossBox.Core.Tests.ViewModels
{
    [TestFixture]
    public class MainMenuViewModel_Tests
    {

        private readonly DropBoxItem[] _contents = new DropBoxItem[]
                                              {
                                                  new DropBoxFolder("folder path", "folder name"),
                                                  new DropBoxFile("file path", "file name")
                                              };

        [SetUp]
        public void SetUp()
        {
            MvxOpenNetCfContainer.Current
                .RegisterServiceInstance<IDropBoxClient>(new DropBoxClientMock_ReturnsFolderContent(_contents));

        }

        [Test]
        public void Assure_Content_List_Has_Expected_Number_Of_Items()
        {
            var viewModel = new MainMenuViewModel();
            viewModel.SelectFolder("/", () => 
                Assert.That(viewModel.FolderContents, Has.Count.EqualTo(_contents.Length)));            
        }

        [Test]
        public void Assure_Content_List_Has_Expected_Number_Of_Files()
        {
            var viewModel = new MainMenuViewModel();
            viewModel.SelectFolder("/", () => 
                Assert.That(viewModel.FolderContents.Where(c => !c.IsDirectory).SingleOrDefault(), Is.Not.Null));
        }

        [Test]
        public void Assure_Content_List_Has_Directory_With_Expected_Properties()
        {
            var viewModel = new MainMenuViewModel();

            var expectedFolder = _contents.OfType<DropBoxFolder>().First();

            viewModel.SelectFolder("/", () =>
            {
                var folder = viewModel.FolderContents.Where(c => c.IsDirectory).Single();
                Assert.That(folder.Name, Is.EqualTo(expectedFolder.Name));
                Assert.That(folder.FullPath, Is.EqualTo(expectedFolder.FullPath));
            });

        }

    }
}
