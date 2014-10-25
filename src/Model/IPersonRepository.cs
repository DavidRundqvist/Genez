using System.Collections.Generic;

namespace Model {
    public interface IPersonRepository {
        IEnumerable<PersonFile> GetAllPeople();
    }
}