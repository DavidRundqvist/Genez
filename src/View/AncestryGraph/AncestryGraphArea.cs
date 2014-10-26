using GraphX;
using QuickGraph;

namespace View.AncestryGraph {

    /// <summary>
    /// This is custom GraphArea representation using custom data types.
    /// GraphArea is the visual panel component responsible for drawing visuals (vertices and edges).
    /// It is also provides many global preferences and methods that makes GraphX so customizable and user-friendly.
    /// </summary>
    public class AncestryGraphArea : GraphArea<PersonVertex, RelationEdge, BidirectionalGraph<PersonVertex, RelationEdge>> {

    }
}