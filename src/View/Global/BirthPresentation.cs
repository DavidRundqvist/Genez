using System;
using System.Linq;
using Common.Enumerable;
using Common.WPF.Presentation;
using Model;
using Model.PersonInformation.Events;

namespace View.Global {
    public class BirthPresentation {
        private readonly PersonFile _person;
        public BirthPresentation(PersonFile person) {
            _person = person;
            _person.Changed += PersonChanged;
            Date = new Property<string>("");
            SynchronizePresentationToPerson();
        }

        void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            _person.Set(new Birth(Date.Value, new Source()));
        }

        void PersonChanged(object sender, EventArgs e) {
            SynchronizePresentationToPerson();
        }

        private void SynchronizePresentationToPerson() {
            var date = _person.Information.OfType<Birth>().Select(b => b.Date.GetValueOrDefault("?")).Join();
            Date.Value = date;
        }

        public Property<string> Date { get; private set; }

    }
}