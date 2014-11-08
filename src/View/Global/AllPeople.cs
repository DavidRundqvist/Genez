using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Common.Enumerable;
using Model;

namespace View.Global {
    public class AllPeople : EventCollection<PersonPresentation> {
        private readonly PersonRegistry _registry;
        private readonly PersonPresentationFactory _factory;

        private readonly ObservableCollection<PersonPresentation> _wpfCollection = new ObservableCollection<PersonPresentation>();

        public AllPeople(PersonRegistry registry, PersonPresentationFactory factory) {
            _registry = registry;
            _factory = factory;
            this.BindTo(registry, person => _factory.CreatePresentation(person));
            _wpfCollection.BindTo(this, p => p);
        }

        public ObservableCollection<PersonPresentation> WpfCollection {
            get { return _wpfCollection; }
        }

        public IEnumerable<PersonPresentation> GetAncestor(PersonPresentation child, int generations) {
            var ancestorFiles = child.Person.GetAncestors(generations);
            return GetPresentations(ancestorFiles);
        }

        public IEnumerable<PersonPresentation> GetChildren(PersonPresentation child, int generations) {
            var childrenFiles = _registry.GetChildren(child.Person, generations);
            return GetPresentations(childrenFiles);
        }


        private IEnumerable<PersonPresentation> GetPresentations(IEnumerable<PersonFile> ancestorFiles) {
            return ancestorFiles.Join(_wpfCollection, file => file, pres => pres.Person, (file, pres) => pres);
        }
    }
}