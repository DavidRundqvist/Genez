using GraphX.Logic;
using QuickGraph;

namespace View.AncestryGraph {
    /// <summary>
    /// Logics core object which contains all algorithms and logic settings
    /// </summary>
    public class AncestryGraphLogics : GXLogicCore<PersonVertex, RelationEdge, BidirectionalGraph<PersonVertex, RelationEdge>> { }
}