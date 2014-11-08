using System.Linq;
using System.Linq.Expressions;
using System.Windows.Media;
using Common;
using Common.WPF.Presentation;
using Model;
using Model.PersonInformation.Relations;

namespace View.Global {
    public class DesignPersonPresentation : PersonPresentation{
        public DesignPersonPresentation() : base(DesignPersonRegistry.TheOne.FirstPerson, Maybe.Empty) {}

    }
}