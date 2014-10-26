using System.Windows.Controls;
using GraphX.Controls;
using GraphX.GraphSharp.Algorithms.Layout.Simple.FDP;
using GraphX.GraphSharp.Algorithms.OverlapRemoval;

namespace View.AncestryGraph {
    /// <summary>
    /// Interaction logic for AncestryGraph.xaml
    /// </summary>
    public partial class AncestryGraph : UserControl {
        public AncestryGraph() {
            InitializeComponent();
            

            Loaded += AncestryGraph_Loaded;

            //Customize Zoombox a bit
            //Set minimap (overview) window to be visible by default
            //ZoomControl.SetViewFinderVisibility(zoomctrl, System.Windows.Visibility.Hidden);
            //Set Fill zooming strategy so whole graph will be always visible

            ConfigureGraphArea();
        }

        void AncestryGraph_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            
            Presentation.Changed += (s, args) => GraphArea.RelayoutGraph();
             GraphArea.GenerateGraph(Presentation);
        }

        private AncestryGraphPresentation Presentation { get { return DataContext as AncestryGraphPresentation; } }

        private void ConfigureGraphArea() {
            //Lets create logic core and filled data graph with edges and vertices
            var logicCore = new AncestryGraphLogics() { Graph = Presentation };
            //This property sets layout algorithm that will be used to calculate vertices positions
            //Different algorithms uses different values and some of them uses edge Weight property.
            logicCore.DefaultLayoutAlgorithm = GraphX.LayoutAlgorithmTypeEnum.KK;
            //Now we can set parameters for selected algorithm using AlgorithmFactory property. This property provides methods for
            //creating all available algorithms and algo parameters.
            logicCore.DefaultLayoutAlgorithmParams = logicCore.AlgorithmFactory.CreateLayoutParameters(GraphX.LayoutAlgorithmTypeEnum.KK);
            //Unfortunately to change algo parameters you need to specify params type which is different for every algorithm.
            ((KKLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).MaxIterations = 100;

            //This property sets vertex overlap removal algorithm.
            //Such algorithms help to arrange vertices in the layout so no one overlaps each other.
            logicCore.DefaultOverlapRemovalAlgorithm = GraphX.OverlapRemovalAlgorithmTypeEnum.FSA;
            logicCore.DefaultOverlapRemovalAlgorithmParams = logicCore.AlgorithmFactory.CreateOverlapRemovalParameters(GraphX.OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)logicCore.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)logicCore.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;

            //This property sets edge routing algorithm that is used to build route paths according to algorithm logic.
            //For ex., SimpleER algorithm will try to set edge paths around vertices so no edge will intersect any vertex.
            //Bundling algorithm will try to tie different edges that follows same direction to a single channel making complex graphs more appealing.
            logicCore.DefaultEdgeRoutingAlgorithm = GraphX.EdgeRoutingAlgorithmTypeEnum.SimpleER;

            //This property sets async algorithms computation so methods like: GraphArea.RelayoutGraph() and GraphArea.GenerateGraph()
            //will run async with the UI thread. Completion of the specified methods can be catched by corresponding events:
            //GraphArea.RelayoutFinished and GraphArea.GenerateGraphFinished.
            logicCore.AsyncAlgorithmCompute = false;

            //Finally assign logic core to GraphArea object
            GraphArea.LogicCore = logicCore;// as IGXLogicCore<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>>;

            
           
            
        }
    }
}
