using Model;
using View.AncestryGraph;
using View.Global;
using View.PersonList;

namespace View.MainWindow {
    public class DesignMainWindowPresentation : MainWindowPresentation{
        public DesignMainWindowPresentation() : base(
            new DesignPersonListPresentation(), 
            new AncestryGraphPresentation(new SelectedPeople(new AllPeople(new DesignPersonRegistry()))),
            new SelectedPeople(new AllPeople(new PersonRegistry()))) {}
    }


}