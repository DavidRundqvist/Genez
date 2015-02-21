using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;
using Infrastructure.IO;
using Model;
using View.AncestryGraph;
using View.Editor;
using View.Global;
using View.PersonList;
using View.Properties;

namespace View.MainWindow {
    public class DesignMainWindowPresentation : MainWindowPresentation{
        public DesignMainWindowPresentation() : base(
            new DesignPersonListPresentation(), 
            CreateGraph(),
            new DesignCommands(),
            new DesignPersonEditorPresentation()) {}

        private static AncestryGraphPresentation CreateGraph() {
            var allPeople = new AllPeople(
                new DesignPersonRegistry(),
                new PersonPresentationFactory(
                    new WPFImageFactory(
                        new DirectoryInfo(@"C:\temp"))));

            return new AncestryGraphPresentation(
                allPeople,
                new GraphPeople(allPeople),
                new GraphControlsPresentation(
                    new AncestorGenerations(),
                    new ChildGenerations()),
                new AncestryGraph.AncestryGraph());
        }
    }
}