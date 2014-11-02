using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Coding;
using Model;

namespace Infrastructure.Persistence {
    public class PersonRegistryDTO {
        public List<PersonDTOReference> People = new List<PersonDTOReference>();

        public bool Contains(Id<PersonFile> id) {
            return People.Any(p => p.Id == id.Guid);
        }

        public void Add(PersonDTOReference dto) {
            People.Add(dto);
        }

        public void Remove(Id<PersonFile> id) {
            People.RemoveAll(dto => dto.Id == id.Guid);
        }
    }
}