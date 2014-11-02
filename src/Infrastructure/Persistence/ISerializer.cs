using System.IO;

namespace Infrastructure.Persistence {
    public interface ISerializer {
        void Serialize(Stream target, PersonDTO dto);
    }
}