using System.Collections.Generic;
using System.Windows.Documents;
using Common.WPF.Presentation;
using View.AncestryGraph;
using View.Editor;
using View.PersonList;

namespace View.MainWindow {
    public class MainWindowPresentation {
        public ICommands Commands { get; private set; }
        public AncestryGraphPresentation AncestryGraphPresentation { get; private set; }
        public PersonListPresentation PersonListPresentation { get; private set; }
        public PersonEditorPresentation EditorPresentation { get; private set; }

        public MainWindowPresentation(
            PersonListPresentation personListPresentation, 
            AncestryGraphPresentation ancestryGraphPresentation,
            ICommands commands, PersonEditorPresentation editorPresentation) {
            EditorPresentation = editorPresentation;
            Commands = commands;
            AncestryGraphPresentation = ancestryGraphPresentation;
            PersonListPresentation = personListPresentation;
        }


 
    }
}