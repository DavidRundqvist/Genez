using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentation {
    public class MainWindowPresentation {
        public PersonListPresentation PersonListPresentation { get; private set; }
        public MainWindowPresentation(PersonListPresentation personListPresentation) {
            PersonListPresentation = personListPresentation;
        }
    }
}