using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.IO;
using Model;
using View.Global;

namespace View.PersonList {
    public class DesignPersonListPresentation : PersonListPresentation {
        public DesignPersonListPresentation() : base(new AllPeople(new DesignPersonRegistry(), new PersonPresentationFactory(new WPFImageFactory(new FileSystem())))) {}
    }
}