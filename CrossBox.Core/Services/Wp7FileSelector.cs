using System;
using Microsoft.Phone.Tasks;

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

                var selectedFile = new SelectedFile(e.OriginalFileName, photoBytes);
                onFileSelected(selectedFile);
            };
            photoChooserTask.Show();
        }
    }
}
