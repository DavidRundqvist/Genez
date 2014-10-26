using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace View.PersonList {
    public class DesignPersonListPresentation : PersonListPresentation {
        public DesignPersonListPresentation() : base(new DesignPersonRegistry()) {}
    }
}