using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common;
using Common.Enumerable;
using Common.WPF.Presentation;
using GraphX;
using Model;
using Model.PersonInformation.Events;

namespace View.Global {
    public class PersonPresentation : VertexBase {

        private static readonly SolidColorBrush _maleColor = new SolidColorBrush(Colors.Blue);
        private static readonly SolidColorBrush _femaleColor = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush _unknownGenderColor = new SolidColorBrush(Colors.Orange);

        private readonly PersonFile _person;
        private readonly Property<bool> _isSelected = new Property<bool>(false);
        private readonly Property<bool> _showInGraph = new Property<bool>(false);
        private readonly Property<PersonNamePresentation> _name;
        private readonly IEnumerable<InformationPresentation> _additionalInformation;
        private readonly Property<ImageSource> _image;
        private readonly Property<string> _lifeTime;

        public PersonPresentation(PersonFile person, Maybe<ImageSource> image) {
            _person = person;
            _name = new Property<PersonNamePresentation>(new PersonNamePresentation(person));
            _additionalInformation = person.Information.Select(i => new InformationPresentation(i));
            _image = new Property<ImageSource>(image.GetValueOrDefault());
            _lifeTime = new Property<string>(CalculateLifeTime());
        }

        private string CalculateLifeTime() {
            var birth = _person.Information.OfType<Birth>().FirstMaybe();
            var death = _person.Information.OfType<Death>().FirstMaybe();

            var birthDate = GetEventPresentation(birth);
            var deathDate = GetEventPresentation(death);
            return string.Format("{0} - {1}", birthDate, deathDate);
        }

        private string GetEventPresentation<T>(Maybe<T> death) where T : Event {
            return death.Convert(GetDatePresentation).GetValueOrDefault("?");
        }
        private string GetDatePresentation(Event theEvent) {return theEvent.Date.Convert(s => s + (theEvent.IsReliable ? "" : "?")).GetValueOrDefault("?");}

        public string FirstName {get { return Person.MainName.Convert(name => name.TheName.Given).GetValueOrDefault("Unknown"); }}
        public string FamilyName {get { return Person.MainName.Convert(name => name.TheName.Family).GetValueOrDefault("Unknown"); }}
        public PersonFile Person {get { return _person; }}
        public override string ToString() {return Name.Value.ToString();}
        public Property<bool> IsSelected { get { return _isSelected; } }
        public Property<PersonNamePresentation> Name { get { return _name; } }
        public IProperty<string> LifeTime { get { return _lifeTime; } }
        public Property<bool> ShowInGraph { get { return _showInGraph; } }
        public IProperty<ImageSource> Image {get { return _image; }}

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




        public Visibility DeathMarkVisibility {
            get {
                return _person.IsDead
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }
        }

        public IEnumerable<InformationPresentation> AdditionalInformation { get { return _additionalInformation; } }
    }
}