using System;
using System.IO;

namespace Infrastructure.IO {
    public class FileSystem : IFileSystem {
        private DirectoryInfo _rootDir;

        public FileSystem() {
            _rootDir = new DirectoryInfo(".");
        }


        public Stream OpenWriteStream(Guid id) {
            var fileName = GetFileName(id);
            return File.OpenWrite(fileName);
        }

        private static string GetFileName(Guid id) {
            var fileName = string.Format("{0}.{1}", id, "xml");
            return fileName;
        }

        public void Delete(Guid id) {
            File.Delete(GetFileName(id));
        }
    }
}