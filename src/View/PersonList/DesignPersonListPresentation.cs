using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.IO;

namespace View.PersonList {
    public class DesignPersonListPresentation : PersonListPresentation {
        public DesignPersonListPresentation() : base(new DesignPeople()) { }
    }
}