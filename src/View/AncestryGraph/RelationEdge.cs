using GraphX;
using View.Global;

namespace View.AncestryGraph {
    public class RelationEdge : EdgeBase<PersonPresentation> {
        public RelationEdge(PersonPresentation source, PersonPresentation target, double weight = 1) : base(source, target, weight) {}
    }
}