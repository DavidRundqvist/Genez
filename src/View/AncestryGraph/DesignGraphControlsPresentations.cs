using View.Global;

namespace View.AncestryGraph {
    public class DesignGraphControlsPresentations : GraphControlsPresentation {
        public DesignGraphControlsPresentations() : base(new AncestorGenerations(), new ChildGenerations()) {}
    }
}