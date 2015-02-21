using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Navigation;
using Common.WPF;
using View.Global;
using View.PersonList;

namespace View.Editor {
    public class DesignPersonEditorPresentation : PersonEditorPresentation {
        public DesignPersonEditorPresentation() : base(
            new SelectedPeople(new DesignPeople())) {}
    }
}