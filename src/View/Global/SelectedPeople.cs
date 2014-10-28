using System.Collections.ObjectModel;
using System.Linq;
using Common.Enumerable;
using Common.WPF.Presentation;
using View.PersonList;

namespace View.Global {
    public class SelectedPeople : EventCollection<PersonPresentation> {
        
        private readonly AllPeople _allPeople;
        private readonly Property<string> _selectionCount = new Property<string>(""); 


        public SelectedPeople(AllPeople allPeople) {
            _allPeople = allPeople;
            foreach (var presentation in allPeople) {
                presentation.IsSelected.PropertyChanged += PersonSelectionChanged;
            }

            _allPeople.CollectionChanged += AllPeopleCollectionChanged;
        }

        public IProperty<string> SelectionCount {
            get { return _selectionCount; }
        }

        private void AllPeopleCollectionChanged(object sender, CollectionEventArgs<PersonPresentation> args) {
            foreach (var presentation in args.Added) {
                presentation.IsSelected.PropertyChanged += PersonSelectionChanged;
            }
            foreach (var presentation in args.Removed) {
                presentation.IsSelected.PropertyChanged -= PersonSelectionChanged;
            }
        }

        void PersonSelectionChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            var shouldBeInList = _allPeople.Where(p => p.IsSelected.Value).ToList();
            var toAdd = shouldBeInList.Except(this).ToArray();
            var toRemove = this.Except(shouldBeInList).ToArray();            
            Replace(toRemove, toAdd);
            _selectionCount.Value = string.Format("Selected people: {0}", this.Count);
        }
    }
}