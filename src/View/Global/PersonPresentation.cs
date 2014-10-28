using System.Windows;
using System.Windows.Media;
using Common.WPF.Presentation;
using GraphX;
using Model;

namespace View.Global {
    public class PersonPresentation : VertexBase {

        private static readonly SolidColorBrush _maleColor = new SolidColorBrush(Colors.Blue);
        private static readonly SolidColorBrush _femaleColor = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush _unknownGenderColor = new SolidColorBrush(Colors.Orange);

        private readonly PersonFile _person;
        private readonly Property<bool> _isSelected = new Property<bool>(false);

        public PersonPresentation(PersonFile person) {
            _person = person;
        }

        public string FirstName {get { return Person.ReliableName.Convert(name => name.Given).GetValueOrDefault("Unknown"); }}
        public string FamilyName {get { return Person.ReliableName.Convert(name => name.Family).GetValueOrDefault("Unknown"); }}
        public PersonFile Person {get { return _person; }}
        public override string ToString() {return Name;}
        public Property<bool> IsSelected { get { return _isSelected; } }
        public string Name { get { return _person.ToString(); } }
        public string BirthDate { get { return _person.BirthDate.GetValueOrDefault("?"); }}
        public string LifeTime { get { return string.Format("{0} - {1}", BirthDate, DeathDate); } }

        public Brush GenderColor {
            get {
                if (_person.IsMale) {
                    return _maleColor;
                }
                if (_person.IsFemale) {
                    return _femaleColor;
                }
                return _unknownGenderColor;
            }
        }


        public string DeathDate {
            get { return _person.IsDead
                        ? _person.DeathDate.GetValueOrDefault("?")
                        : "";
            }
        }


        public Visibility DeathMarkVisibility {
            get {
                return _person.IsDead
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }
        }
    }
}