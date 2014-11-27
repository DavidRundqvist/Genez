using System.IO;
using Model;

namespace Infrastructure.Data {
    public class GedcomRepositoryFactory {

        public IRepository GetGedcomRepository(FileInfo file) {
            return new GedcomRepository(file);
        }
    }
}