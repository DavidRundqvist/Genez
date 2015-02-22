using System.Windows.Controls;
using System.Windows.Input;

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


        private void OnMouseDown(object sender, MouseButtonEventArgs e) {
            Presentation.AllPeople.UnselectAll();
        }

        private void NodeOnMouseDown(object sender, MouseButtonEventArgs e) {
            if (!Keyboard.Modifiers.HasFlag(ModifierKeys.Control)) {
                Presentation.AllPeople.UnselectAll();
            }

            (sender as PersonNodeView).Presentation.IsSelected.Value = true;
        }
    }
}
