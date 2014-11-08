﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly Property<bool> _showInGraph = new Property<bool>(false);
        private readonly Property<PersonNamePresentation> _name;
        private readonly IEnumerable<InformationPresentation> _additionalInformation;

        public PersonPresentation(PersonFile person) {
            _person = person;
            _name = new Property<PersonNamePresentation>(GetName(person));
            _additionalInformation = person.Information.Select(i => new InformationPresentation(i));
        }

        private PersonNamePresentation GetName(PersonFile person) {
            return new PersonNamePresentation(person);
        }

        public string FirstName {get { return Person.MainName.Convert(name => name.TheName.Given).GetValueOrDefault("Unknown"); }}
        public string FamilyName {get { return Person.MainName.Convert(name => name.TheName.Family).GetValueOrDefault("Unknown"); }}
        public PersonFile Person {get { return _person; }}
        public override string ToString() {return Name.Value.ToString();}
        public Property<bool> IsSelected { get { return _isSelected; } }
        public Property<PersonNamePresentation> Name { get { return _name; } }
        public string BirthDate { get { return _person.BirthDate.GetValueOrDefault("?"); }}
        public string LifeTime { get { return string.Format("{0} - {1}", BirthDate, DeathDate); } }
        public Property<bool> ShowInGraph { get { return _showInGraph; } }

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

        public IEnumerable<InformationPresentation> AdditionalInformation { get { return _additionalInformation; } }
    }
}