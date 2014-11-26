using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common;
using Infrastructure.IO;

namespace View {
    public class WPFImageFactory {
        private readonly IFileSystem _fileSystem;
        public WPFImageFactory(IFileSystem fileSystem) {
            _fileSystem = fileSystem;
        }

        public ImageSource GetImage(FileName imageId) {
            if (!_fileSystem.DoesFileExist(imageId))
                return null;
            using (var source = _fileSystem.OpenReadStream(imageId)) {
                return BitmapFrame.Create(source, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }
    }
}