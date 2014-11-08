using Common;
using Common.Coding;

namespace Model {
    public class Image {
        private readonly Id<Image> _id;
        private readonly FileName _file;

        public Image(Id<Image> id, FileName file) {
            _id = id;
            _file = file;
        }

        public Id<Image> Id {
            get { return _id; }
        }

        public FileName File {
            get { return _file; }
        }
    }
}