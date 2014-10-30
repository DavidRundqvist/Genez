using System.Collections.ObjectModel;
using System.Linq;
using Common.Enumerable;
using Common.WPF.Presentation;
using View.PersonList;

namespace View.Global {
    public class ShowInGraphPeople : EventCollection<PersonPresentation> {
        
        private readonly AllPeople _allPeople;

        public ShowInGraphPeople(AllPeople allPeople) {
            _allPeople = allPeople;
            foreach (var presentation in allPeople) {
                presentation.ShowInGraph.PropertyChanged += PersonGraphChanged;
            }

            _allPeople.CollectionChanged += AllPeopleCollectionChanged;
        }

        private void AllPeopleCollectionChanged(object sender, CollectionEventArgs<PersonPresentation> args) {
            foreach (var presentation in args.Added) {
                presentation.ShowInGraph.PropertyChanged += PersonGraphChanged;
            }
            foreach (var presentation in args.Removed) {
                presentation.ShowInGraph.PropertyChanged -= PersonGraphChanged;
            }
        }

        void PersonGraphChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            var shouldBeInList = _allPeople.Where(p => p.ShowInGraph.Value).ToList();
            var toAdd = shouldBeInList.Except(this).ToArray();
            var toRemove = this.Except(shouldBeInList).ToArray();            
            Replace(toRemove, toAdd);
        }
    }
}