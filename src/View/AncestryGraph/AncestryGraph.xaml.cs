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
            Loaded += AncestryGraphLoaded;
        }

        void AncestryGraphLoaded(object sender, System.Windows.RoutedEventArgs e) {
            Presentation.Changed += GraphChanged;
        }

        void GraphChanged(object sender, System.EventArgs e) {
            _graphArea.ShowGraph();
            _zoomControl.ZoomToFill();
        }

        private AncestryGraphPresentation Presentation { get { return DataContext as AncestryGraphPresentation; } }
    }
}
