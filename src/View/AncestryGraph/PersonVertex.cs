using System;
using System.Windows;
using System.Windows.Media;
using GraphX;
using Model;

namespace View.AncestryGraph {
    public class PersonVertex : VertexBase{
        private readonly PersonFile _person;
        private static readonly SolidColorBrush _maleColor = new SolidColorBrush(Colors.Blue);
        private static readonly SolidColorBrush _femaleColor = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush _unknownGenderColor = new SolidColorBrush(Colors.Orange);

        public PersonVertex(PersonFile person, bool isSelected) {
            _person = person;
            IsSelected = isSelected;
        }

        public string Name { get { return _person.ToString(); } }

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

        public string Birth { get { return _person.BirthDate.GetValueOrDefault("?"); }}

        public string Death {
            get { return _person.IsDead
                        ? _person.DeathDate.GetValueOrDefault("?")
                        : "";
            }
        }

        public string LifeTime { get { return string.Format("{0} - {1}", Birth, Death); } }

        public bool IsSelected { get; private set; }

        public Visibility IsDead {
            get {
                return _person.IsDead
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }
        }

    }
}