using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Enumerable;
using Model;

namespace View.PersonList {
    public class PersonListPresentation {
        private readonly ObservableCollection<PersonPresentation> _people = new ObservableCollection<PersonPresentation>();
        public PersonListPresentation(PersonRegistry registry) {
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