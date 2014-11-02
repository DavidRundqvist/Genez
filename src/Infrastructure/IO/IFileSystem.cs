using System;
using System.IO;

namespace Infrastructure.IO {
    public interface IFileSystem {
        Stream OpenWriteStream(Guid id);
        void Delete(Guid id);
    }
}