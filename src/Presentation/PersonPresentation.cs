using Model;

namespace Presentation {
    public class PersonPresentation {
        private readonly PersonFile _person;
        public PersonPresentation(PersonFile person) {
            _person = person;
        }

        public string Name { get { return _person.ToString(); } }

        public string FirstName {
            get { return _person.ReliableName.Convert(name => name.Given).GetValueOrDefault("Unknown"); }
        }
        public string FamilyName {
            get { return _person.ReliableName.Convert(name => name.Family).GetValueOrDefault("Unknown"); }
        }

        public override string ToString() {
            return Name;
        }
    }
}