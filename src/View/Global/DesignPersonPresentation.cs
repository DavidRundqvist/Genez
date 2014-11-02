using System.Linq;
using Model;

namespace View.Global {
    public class DesignPersonPresentation : PersonPresentation{
        public DesignPersonPresentation() : base(new DesignPersonRegistry().First()) {}
    }
}