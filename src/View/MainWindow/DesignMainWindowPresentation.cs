using View.AncestryGraph;
using View.PersonList;

namespace View.MainWindow {
    public class DesignMainWindowPresentation : MainWindowPresentation{
        public DesignMainWindowPresentation() : base(
            new DesignPersonListPresentation(), 
            new AncestryGraphPresentation(new DesignPersonRegistry())) {}
    }


}