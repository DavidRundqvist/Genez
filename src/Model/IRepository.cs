using System.Collections.Generic;

namespace Model {
    public interface IRepository {
        IEnumerable<PersonFile> GetAllPeople();
        void Store(IEnumerable<PersonFile> people);
        void Remove(IEnumerable<PersonFile> people);
    }
}