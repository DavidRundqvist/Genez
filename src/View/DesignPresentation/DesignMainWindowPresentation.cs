using System;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Presentation;

namespace View.DesignPresentation {
    public class DesignMainWindowPresentation : MainWindowPresentation{
        public DesignMainWindowPresentation() 
            : base(new DesignPersonListPresentation()) {}
    }
}