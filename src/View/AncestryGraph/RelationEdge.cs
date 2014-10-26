using GraphX;

namespace View.AncestryGraph {
    public class RelationEdge : EdgeBase<PersonVertex> {
        public RelationEdge(PersonVertex source, PersonVertex target, double weight = 1) : base(source, target, weight) {}
    }
}