using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common.Enumerable;
using Model;
using Model.PersonInformation;
using Presentation;

namespace View.DesignPresentation {
    public class DesignPersonListPresentation : PersonListPresentation {
        public DesignPersonListPresentation() : base(new ObservableCollection<PersonPresentation>()) {
            var designPeople = CreateDesignPeople();
            base._people.AddRange(designPeople.Select(p => new PersonPresentation(p)));
        }

        private IEnumerable<PersonFile> CreateDesignPeople() {
            yield return CreatePerson("David", "Rundqvist");
            yield return CreatePerson("Darja", "Utgof");
            yield return CreatePerson("Carl", "Rundqvist");
            yield return CreatePerson("Camilla", "Rundqvist");
            yield return CreatePerson("Monica", "Rundqvist");

        }

        private PersonFile CreatePerson(string firstName, string lastName) {
            var result = new PersonFile();
            result.Add(new Name(new PersonName(new[]{new NameComponent(firstName, NameType.Given), new NameComponent(lastName, NameType.Family)}), new Source()));
            return result;
        }
    }
}