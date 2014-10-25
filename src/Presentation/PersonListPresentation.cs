using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Common.Enumerable;
using Model;

namespace Presentation {
    public class PersonListPresentation {
        protected readonly PersonRegistry _registry;
        private readonly ObservableCollection<PersonPresentation> _people = new ObservableCollection<PersonPresentation>();
        public PersonListPresentation(PersonRegistry registry) {
            _registry = registry;
            _people.BindTo(registry, CreatePerson, RemovePerson);
        }

        private void RemovePerson(PersonPresentation personPresentation) {
            // todo: remove event listeners

        }

        private PersonPresentation CreatePerson(PersonFile arg) {
            return new PersonPresentation(arg);
        }

        public IEnumerable<PersonPresentation> People {
            get { return _people; }
        }
    }
}