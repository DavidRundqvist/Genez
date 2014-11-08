using System.Collections.Generic;

namespace Model {
    public interface IRepository {
        IEnumerable<PersonFile> GetAllPeople();
        void Add(IEnumerable<PersonFile> people);
        void Remove(IEnumerable<PersonFile> people);
        void Update(IEnumerable<PersonFile> people);
    }
}