using System.IO;
using Common;
using Common.Coding;

namespace Model {
    public class Image {
        private readonly Id<Image> _id;
        private readonly FileInfo _file;

        public Image(Id<Image> id, FileInfo file) {
            _id = id;
            _file = file;
        }

        public Id<Image> Id {
            get { return _id; }
        }

        public FileInfo File {
            get { return _file; }
        }
    }
}