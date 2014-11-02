using System.Linq;
using Model;

namespace Infrastructure.Persistence {
    public class PersonDTOFactory {
        private readonly InformationDTOFactory _factory;
        public PersonDTOFactory(InformationDTOFactory factory) {
            _factory = factory;
        }

        public PersonDTO ToDTO(PersonFile person) {
            var information = person.Information.Select(i => i.Accept(_factory)).ToList();
            return new PersonDTO() {Information = information, Id = person.Id};
        }
    }
}