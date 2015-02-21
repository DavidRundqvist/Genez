using Common.Enumerable;

namespace View.Global {
    public class SelectedPeople : FilteredCollection<PersonPresentation>
    {
        public SelectedPeople(AllPeople backingCollection) 
            : base(backingCollection, person => person.IsSelected) {
        }
    }
}