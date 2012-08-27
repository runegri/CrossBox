using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
