using System.IO;
using System.Linq;
using Model;
using View.Global;

namespace View.PersonList {
    public class DesignPeople : AllPeople {
        public DesignPeople()
            : base(
                new DesignPersonRegistry(),
                new PersonPresentationFactory(new WPFImageFactory(new DirectoryInfo(@"c:\temp")))) {
            this.First().IsSelected.Value = true;
        }
    }
}