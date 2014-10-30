using System.Windows.Controls;
using System.Windows.Input;
using GraphX;
using GraphX.Controls;
using GraphX.GraphSharp.Algorithms.Layout.Simple.FDP;
using GraphX.GraphSharp.Algorithms.OverlapRemoval;

namespace View.AncestryGraph {
    /// <summary>
    /// Interaction logic for AncestryGraphView.xaml
    /// </summary>
    public partial class AncestryGraphView : UserControl {
        public AncestryGraphView() {
            InitializeComponent();
            Loaded += AncestryGraphLoaded;
        }

        void AncestryGraphLoaded(object sender, System.Windows.RoutedEventArgs e) {
            Presentation.Changed += GraphChanged;
        }

        void GraphChanged(object sender, System.EventArgs e) {
            _graphArea.ShowGraph(Presentation.Graph);
            _zoomControl.ZoomToFill();
        }

        private AncestryGraphPresentation Presentation { get { return DataContext as AncestryGraphPresentation; } }

 
    }
}
