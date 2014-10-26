using System.Windows.Controls;
using GraphX;
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
        }

        void AncestryGraph_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            _graphArea.ShowGraph();
            _zoomControl.ZoomToFill();            
        }
    }
}
