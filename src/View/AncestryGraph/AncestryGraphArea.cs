using GraphX;
using GraphX.GraphSharp.Algorithms.OverlapRemoval;
using QuickGraph;

namespace View.AncestryGraph {

    /// <summary>
    /// This is custom _graphArea representation using custom data types.
    /// _graphArea is the visual panel component responsible for drawing visuals (vertices and edges).
    /// It is also provides many global preferences and methods that makes GraphX so customizable and user-friendly.
    /// </summary>
    public class AncestryGraphArea : GraphArea<PersonVertex, RelationEdge, BidirectionalGraph<PersonVertex, RelationEdge>> {
        public AncestryGraphArea() {
            this.LogicCore = new AncestryGraphLogics();
        }

        public void ShowGraph() {
            GenerateGraph(Presentation, true, true, true);
        }

        private AncestryGraphPresentation Presentation { get { return DataContext as AncestryGraphPresentation; } }
    }
}