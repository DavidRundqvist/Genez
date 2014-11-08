using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Coding;
using Infrastructure.Persistence.Information;
using Model;

namespace Infrastructure.Persistence {
    public class PersonFactory {
        private readonly InformationFactory _informationFactory;
        public PersonFactory(InformationFactory informationFactory) {
            _informationFactory = informationFactory;
        }

        public IEnumerable<PersonFile> From(IEnumerable<PersonDTO> personDtos) {

            var result = personDtos.ToDictionary(
                p => p,
                p => new PersonFile(new Id<PersonFile>(p.Id)));

            _informationFactory.Clear();
            _informationFactory.Add(result.Values.ToArray());

            foreach (var tuple in result) {
                var dto = tuple.Key;
                var person = tuple.Value;
                var info = dto.Information.Select(i => i.Accept(_informationFactory));
                foreach (var information in info) {
                    person.Add(information);
                }
            }

            return result.Values;
        }         
    }
}