using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Enumerable;
using Model;
using View.Global;

namespace View.PersonList {
    public class PersonListPresentation {
        private readonly AllPeople _people;

        public PersonListPresentation(AllPeople people) {
            _people = people;
        }

        public ObservableCollection<PersonPresentation> People {
            get { return _people.WpfCollection; }
        }


        // todo: add filters etc.
    }
}