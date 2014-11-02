using System;
using System.IO;
using Infrastructure.Persistence;

namespace Infrastructure.IO {
    public interface IFileSystem {
        Stream OpenWriteStream(FileName fileName);
        void Delete(FileName fileName);
    }
}