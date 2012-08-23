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
    public class DropBoxObjectViewModel_Tests
    {
        [Test]
        public void Assure_Item_Name_Is_Exposed()
        {
            var item = new DropBoxFile("full path", "name");
            var viewModel = new DropBoxObjectViewModel(item);
            Assert.That(viewModel.Name, Is.EqualTo(item.Name));
        }

        [Test]
        public void Assure_Item_Path_Is_Exposed()
        {
            var item = new DropBoxFile("full path", "name");
            var viewModel = new DropBoxObjectViewModel(item);
            Assert.That(viewModel.FullPath, Is.EqualTo(item.FullPath));
        }
    }
}
