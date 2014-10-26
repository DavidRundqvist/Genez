using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using View.Global;

namespace View.AncestryGraph {
    public class GraphControlsPresentation {
        private readonly SliderPresentation _ancestorGenerations;
        private readonly SliderPresentation _childGenerations;

        public GraphControlsPresentation(AncestorGenerations ancestorGenerations, ChildGenerations childGenerations) {
            _ancestorGenerations = new SliderPresentation(ancestorGenerations, 0, 10, "Ancestor Generations");
            _childGenerations = new SliderPresentation(childGenerations, 0, 10, "Child Generations");
        }

        public SliderPresentation AncestorGenerations {
            get { return _ancestorGenerations; }
        }

        public SliderPresentation ChildGenerations {
            get { return _childGenerations; }
        }
    }
}