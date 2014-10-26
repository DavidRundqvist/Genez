using View.AncestryGraph;
using View.PersonList;

namespace View.MainWindow {
    public class MainWindowPresentation {
        public AncestryGraphPresentation AncestryGraphPresentation { get; private set; }
        public PersonListPresentation PersonListPresentation { get; private set; }

        public MainWindowPresentation(PersonListPresentation personListPresentation, AncestryGraphPresentation ancestryGraphPresentation) {
            AncestryGraphPresentation = ancestryGraphPresentation;
            PersonListPresentation = personListPresentation;
        }
    }
}