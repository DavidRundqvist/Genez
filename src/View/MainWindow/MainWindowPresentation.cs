using View.AncestryGraph;
using View.Global;
using View.PersonList;

namespace View.MainWindow {
    public class MainWindowPresentation {
        private readonly SelectedPeople _selectedPeople;
        public AncestryGraphPresentation AncestryGraphPresentation { get; private set; }
        public PersonListPresentation PersonListPresentation { get; private set; }

        public MainWindowPresentation(
            PersonListPresentation personListPresentation, 
            AncestryGraphPresentation ancestryGraphPresentation,
            SelectedPeople selectedPeople) {
            _selectedPeople = selectedPeople;
            AncestryGraphPresentation = ancestryGraphPresentation;
            PersonListPresentation = personListPresentation;
        }


        public IProperty<string> SelectionCount {
            get { return _selectedPeople.SelectionCount; }
        }
    }
}