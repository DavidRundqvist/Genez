using Model;

namespace View.Global {
    public class PersonPresentation {
        private readonly PersonFile _person;
        private readonly Property<bool> _isSelected = new Property<bool>(false);

        public PersonPresentation(PersonFile person) {
            _person = person;
        }

        public string Name { get { return Person.ToString(); } }

        public string FirstName {
            get { return Person.ReliableName.Convert(name => name.Given).GetValueOrDefault("Unknown"); }
        }
        public string FamilyName {
            get { return Person.ReliableName.Convert(name => name.Family).GetValueOrDefault("Unknown"); }
        }

        public PersonFile Person {
            get { return _person; }
        }

        public override string ToString() {
            return Name;
        }

        public Property<bool> IsSelected { get { return _isSelected; } }
    }
}