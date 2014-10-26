using GraphX;
using GraphX.GraphSharp.Algorithms.Layout;
using GraphX.GraphSharp.Algorithms.Layout.Simple.Tree;
using GraphX.GraphSharp.Algorithms.OverlapRemoval;
using GraphX.Logic;
using QuickGraph;

namespace View.AncestryGraph {
    /// <summary>
    /// Logics core object which contains all algorithms and logic settings
    /// </summary>
    public class AncestryGraphLogics :
        GXLogicCore<PersonVertex, RelationEdge, BidirectionalGraph<PersonVertex, RelationEdge>> {
        public AncestryGraphLogics() {
            DefaultLayoutAlgorithm = GraphX.LayoutAlgorithmTypeEnum.Tree;
            DefaultLayoutAlgorithmParams = AlgorithmFactory.CreateLayoutParameters(DefaultLayoutAlgorithm);

            var parameters = DefaultLayoutAlgorithmParams as SimpleTreeLayoutParameters;
            parameters.Direction = LayoutDirection.BottomToTop;
            parameters.LayerGap = 100;
            parameters.VertexGap = 100;
            parameters.OptimizeWidthAndHeight = true;
            
            DefaultOverlapRemovalAlgorithm = GraphX.OverlapRemovalAlgorithmTypeEnum.FSA;
            DefaultOverlapRemovalAlgorithmParams = AlgorithmFactory.CreateOverlapRemovalParameters(GraphX.OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;

            AsyncAlgorithmCompute = false;            

        }
    }
}