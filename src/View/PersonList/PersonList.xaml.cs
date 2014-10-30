using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using View.Global;

namespace View.PersonList {
    /// <summary>
    /// Interaction logic for PersonList.xaml
    /// </summary>
    public partial class PersonList : UserControl {
        public PersonList() {
            InitializeComponent();
        }

        private void PersonDoubleClicked(object sender, MouseButtonEventArgs e) {
            if (sender is FrameworkElement) {
                var fe = sender as FrameworkElement;
                if (fe.DataContext is PersonPresentation) {
                    var person = fe.DataContext as PersonPresentation;
                    person.ShowInGraph.Value = !person.ShowInGraph.Value;
                }
            }

        }
    }
}
