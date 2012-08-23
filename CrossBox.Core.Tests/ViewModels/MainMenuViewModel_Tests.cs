using System;
using System.Linq;
using Cirrious.MvvmCross.IoC;
using CrossBox.Core.DropBox;
using CrossBox.Core.Services;
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

        public void SetUp_ReturnFolderContent()
        {
            MvxOpenNetCfContainer.Current
                .RegisterServiceInstance<IDropBoxClient>(new DropBoxClientMock_ReturnsFolderContent(_contents));

        }

        [Test]
        public void Assure_Content_List_Has_Expected_Number_Of_Items()
        {
            SetUp_ReturnFolderContent();
            var viewModel = new MainMenuViewModel();
            viewModel.SelectFolder("/", () => 
                Assert.That(viewModel.FolderContents, Has.Count.EqualTo(_contents.Length)));            
        }

        [Test]
        public void Assure_Content_List_Has_Expected_Number_Of_Files()
        {
            SetUp_ReturnFolderContent();
            var viewModel = new MainMenuViewModel();
            viewModel.SelectFolder("/", () => 
                Assert.That(viewModel.FolderContents.Where(c => !c.IsDirectory).SingleOrDefault(), Is.Not.Null));
        }

        [Test]
        public void Assure_Content_List_Has_Directory_With_Expected_Properties()
        {
            SetUp_ReturnFolderContent();

            var expectedFolder = _contents.OfType<DropBoxFolder>().First();

            var viewModel = new MainMenuViewModel();
            viewModel.SelectFolder("/", () =>
            {
                var folder = viewModel.FolderContents.Where(c => c.IsDirectory).Single();
                Assert.That(folder.Name, Is.EqualTo(expectedFolder.Name));
                Assert.That(folder.FullPath, Is.EqualTo(expectedFolder.FullPath));
            });

        }

        [Test]
        public void Assure_DropBox_Errors_Are_Reported()
        {
            Exception reportedException = null;

            var errorReporter = new ErrorReporterMock(exception => reportedException = exception);
            MvxOpenNetCfContainer.Current.RegisterServiceInstance<IErrorReporter>(errorReporter);

            MvxOpenNetCfContainer.Current
                .RegisterServiceInstance<IDropBoxClient>(new DropBoxClientMock_FailsOnGetFolderContent("Error"));

            var viewModel = new MainMenuViewModel();
            viewModel.SelectFolder("", () => 
                Assert.That(reportedException, Is.Not.Null));

        }

    }
}
