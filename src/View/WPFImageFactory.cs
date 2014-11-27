using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common;
using Infrastructure.IO;

namespace View {
    public class WPFImageFactory {
        private readonly DirectoryInfo _fileSystem;
        public WPFImageFactory(DirectoryInfo fileSystem) {
            _fileSystem = fileSystem;
        }

        public ImageSource GetImage(string imageId) {
            var imageFile = _fileSystem.GetFile(imageId);

            if (!imageFile.Exists)
                return null;
            using (var source = imageFile.OpenRead()) {
                return BitmapFrame.Create(source, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }
    }
}