using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Common.Enumerable;
using Model;

namespace View.Global {
    public class AllPeople : EventCollection<PersonPresentation> {

        private readonly ObservableCollection<PersonPresentation> _wpfCollection = new ObservableCollection<PersonPresentation>();

        public AllPeople(PersonRegistry registry) {
            this.BindTo(registry, arg => new PersonPresentation(arg));
            _wpfCollection.BindTo(this, p => p);
        }

        public ObservableCollection<PersonPresentation> WpfCollection {
            get { return _wpfCollection; }
        }

        public IEnumerable<PersonPresentation> GetAncestor(PersonPresentation child, int generations) {
            var ancestorFiles = child.Person.GetAncestors(generations);
            return GetPresentations(ancestorFiles);
        }

        private IEnumerable<PersonPresentation> GetPresentations(IEnumerable<PersonFile> ancestorFiles) {
            return ancestorFiles.Join(_wpfCollection, file => file, pres => pres.Person, (file, pres) => pres);
        }
    }
}