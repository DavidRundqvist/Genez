using System.Linq;
using Common.Enumerable;
using Common.WPF.Presentation;
using Model;
using Model.PersonInformation.Events;
using View.Global;

namespace View.Editor {
    public class PersonEditorPresentation {
        private readonly SelectedPeople _selectedPeople;
        private bool _syncingWithSelection;

        public PersonEditorPresentation(SelectedPeople selectedPeople) {
            _selectedPeople = selectedPeople;
            BirthDate = new Property<string>("");
            WireEvents();
            SynchronizeWithSelection();
        }

        private void WireEvents() {
            // Sync presentation with backing:
            _selectedPeople.CollectionChanged += (s, e) => SynchronizeWithSelection();

            // Sync backing with presentation
            BirthDate.PropertyChanged += (s, e) => SynchronizeWithPresentation();
        }

        private void SynchronizeWithPresentation() {
            if (_syncingWithSelection)
                return;

            _selectedPeople.ForEach(p => p.Person.Set(new Birth(BirthDate.Value, new Source())));
        }

        private void SynchronizeWithSelection() {
            _syncingWithSelection = true;
            var dates = _selectedPeople.Select(p => p.BirthDate.Date.Value).ToList();
            if (dates.Distinct().Count() == 1)
            {
                BirthDate.Value = dates.First();
            }
            else
            {
                BirthDate.Value = "";
            }
            _syncingWithSelection = false;
        }

        public Property<string> BirthDate { get; private set; }
    }
}