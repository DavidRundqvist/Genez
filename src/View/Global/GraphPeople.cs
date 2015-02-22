using System;
using System.Collections.Generic;
using System.Linq;
using Common.Coding;
using Common.Enumerable;
using Model.PersonInformation;
using Model.PersonInformation.Relations;

namespace View.Global {
    public class GraphPeople : FilteredCollection<PersonPresentation> {

        public event EventHandler RelationsChanged;

        public GraphPeople(AllPeople backingCollection)
            : base(backingCollection, person => person.ShowInGraph) {            
        }

        protected override void BindTo(IEnumerable<PersonPresentation> items) {
            items = items.ToList();
            base.BindTo(items);
            foreach (var personPresentation in items) {
                personPresentation.Person.Changed += PersonChanged;
            }
        }

        protected override void UnbindFrom(IEnumerable<PersonPresentation> items) {
            items = items.ToList();
            base.UnbindFrom(items);
            foreach (var personPresentation in items) {
                personPresentation.Person.Changed += PersonChanged;
            }
        }

        private void PersonChanged(object sender, CollectionEventArgs<Information> args) {
            var relationChanges = args.Added.Concat(args.Removed).OfType<Relation>();
            if (relationChanges.Any()) {
                RelationsChanged.Raise();
            }
        }
    }
}