using GraphX;
using GraphX.GraphSharp.Algorithms.Layout;
using GraphX.GraphSharp.Algorithms.Layout.Simple.Tree;
using GraphX.GraphSharp.Algorithms.OverlapRemoval;
using GraphX.Logic;
using QuickGraph;
using View.Global;

namespace View.AncestryGraph {

    /// <summary>
    /// This is custom _graphArea representation using custom data types.
    /// _graphArea is the visual panel component responsible for drawing visuals (vertices and edges).
    /// It is also provides many global preferences and methods that makes GraphX so customizable and user-friendly.
    /// </summary>
    public class AncestryGraphArea : GraphArea<PersonPresentation, RelationEdge, BidirectionalGraph<PersonPresentation, RelationEdge>> {
        public AncestryGraphArea() {
            this.LogicCore = new AncestryGraphLogics();
        }

        public void ShowGraph(AncestryGraph graph) {
            GenerateGraph(graph, true, true, true);
        }

        /// <summary>
        /// Logics core object which contains all algorithms and logic settings
        /// </summary>
        private class AncestryGraphLogics :
            GXLogicCore<PersonPresentation, RelationEdge, BidirectionalGraph<PersonPresentation, RelationEdge>> {
            public AncestryGraphLogics() {
                DefaultLayoutAlgorithm = GraphX.LayoutAlgorithmTypeEnum.Tree;
                DefaultLayoutAlgorithmParams = AlgorithmFactory.CreateLayoutParameters(DefaultLayoutAlgorithm);

                var parameters = DefaultLayoutAlgorithmParams as SimpleTreeLayoutParameters;
                parameters.Direction = LayoutDirection.BottomToTop;
                parameters.LayerGap = 100;
                parameters.VertexGap = 100;
                parameters.SpanningTreeGeneration = SpanningTreeGeneration.BFS;

                DefaultOverlapRemovalAlgorithm = GraphX.OverlapRemovalAlgorithmTypeEnum.FSA;
                DefaultOverlapRemovalAlgorithmParams =
                    AlgorithmFactory.CreateOverlapRemovalParameters(GraphX.OverlapRemovalAlgorithmTypeEnum.FSA);
                ((OverlapRemovalParameters) DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
                ((OverlapRemovalParameters) DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;

                AsyncAlgorithmCompute = false;

            }
        }
    }
}