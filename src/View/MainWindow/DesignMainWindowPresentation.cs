using Infrastructure.IO;
using Model;
using View.AncestryGraph;
using View.Global;
using View.PersonList;

namespace View.MainWindow {
    public class DesignMainWindowPresentation : MainWindowPresentation{
        public DesignMainWindowPresentation() : base(
            new DesignPersonListPresentation(), 
            new AncestryGraphPresentation(new AllPeople(new DesignPersonRegistry(), new PersonPresentationFactory(new WPFImageFactory(new FileSystem()))), new ShowInGraphPeople(new AllPeople(new DesignPersonRegistry(), new PersonPresentationFactory(new WPFImageFactory(new FileSystem())))),new GraphControlsPresentation(new AncestorGenerations(), new ChildGenerations()), new AncestryGraph.AncestryGraph())) {}
    }


}