using Common.Enumerable;

namespace View.Global {
    public class GraphPeople : FilteredCollection<PersonPresentation> {
        public GraphPeople(AllPeople backingCollection)
            : base(backingCollection, person => person.ShowInGraph) {            
        }
    }
}