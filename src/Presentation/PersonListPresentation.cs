using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Presentation {
    public class PersonListPresentation {
        protected readonly ObservableCollection<PersonPresentation> _people;
        public PersonListPresentation(ObservableCollection<PersonPresentation> people) {
            _people = people;
        }

        public IEnumerable<PersonPresentation> People {
            get { return _people; }
        }
    }
}