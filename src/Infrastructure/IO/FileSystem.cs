using System;
using System.IO;
using Common;
using Infrastructure.Persistence;

namespace Infrastructure.IO {
    public class FileSystem : IFileSystem {
        private readonly DirectoryInfo _rootDir;

        public FileSystem() {
            _rootDir = new DirectoryInfo(@"..\..\..\Test\TestData\Registry");
        }


        public bool DoesFileExist(FileName fileName) {
            return GetFile(fileName).Exists;
        }

        public Stream OpenWriteStream(FileName fileName) {
            var file = GetFileAndCreateFolder(fileName);
            return file.OpenWrite();
        }

        public Stream OpenReadStream(FileName fileName) {
            var file = GetFile(fileName);
            return file.OpenRead();
        }

        private FileInfo GetFileAndCreateFolder(FileName fileName) {
            var result = GetFile(fileName);
            var dir = result.Directory;
            if (!dir.Exists)
                dir.Create();
            return result;
        }

        private FileInfo GetFile(FileName fileName) {
            return new FileInfo(Path.Combine(_rootDir.FullName, fileName.FileNameString));
        }


        public void Delete(FileName fileName) {
            var file = GetFile(fileName);
            if (file.Exists)
                file.Delete();            
        }
    }
}