using System;
using Microsoft.Phone.Tasks;
using System.Linq;

namespace CrossBox.Core.Services
{
    public class Wp7FileSelector : IFileSelector
    {
        public void SelectFile(Action<SelectedFile> onFileSelected)
        {
            var photoChooserTask = new PhotoChooserTask { ShowCamera = true };
            photoChooserTask.Completed += (s, e) =>
            {
                var photoBytes = new byte[e.ChosenPhoto.Length];
                e.ChosenPhoto.Read(photoBytes, 0, photoBytes.Length);

                var fileNameOnly = e.OriginalFileName.Split('\\').Last();

                var selectedFile = new SelectedFile(fileNameOnly, photoBytes);
                onFileSelected(selectedFile);
            };
            photoChooserTask.Show();
        }
    }
}
