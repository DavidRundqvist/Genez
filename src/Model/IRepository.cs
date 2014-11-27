using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model {
    public interface IRepository {
        Task<IEnumerable<PersonFile>> GetAllPeople();
        Task Add(IEnumerable<PersonFile> people);
        void Remove(IEnumerable<PersonFile> people);
        void Update(IEnumerable<PersonFile> people);
    }
}