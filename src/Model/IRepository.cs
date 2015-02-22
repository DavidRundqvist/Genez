using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model {
    public interface IRepository {
        Task<IEnumerable<PersonFile>> GetAllPeople();
        Task Add(IEnumerable<PersonFile> people);
        Task Remove(IEnumerable<PersonFile> people);
        Task Update(IEnumerable<PersonFile> people);
    }
}