﻿using System.Collections.Generic;
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
        private readonly DelegatingProperty<PersonNamePresentation> _name;
        private readonly ObservableCollection<InformationPresentation> _detailedInformation = new ObservableCollection<InformationPresentation>();
        private readonly DelegatingProperty<ImageSource> _image;
        private readonly DelegatingProperty<string> _lifeTime;

        public PersonPresentation(PersonFile person, Maybe<ImageSource> image) {
            _person = person;
            _name = new DelegatingProperty<PersonNamePresentation>(() => new PersonNamePresentation(person));
            _detailedInformation.BindTo(person.Information, i => new InformationPresentation(i));
            _image = new DelegatingProperty<ImageSource>(() => image.GetValueOrDefault());
            _lifeTime = new DelegatingProperty<string>(CalculateLifeTime);
            person.Changed += PersonChanged;
        }

        void PersonChanged(object sender, System.EventArgs e) {
            _image.RaisePropertyChanged();
            _lifeTime.RaisePropertyChanged();
            _name.RaisePropertyChanged();
        }

        private string CalculateLifeTime() {
            var birth = _person.Information.OfType<Birth>().FirstMaybe();
            var death = _person.Information.OfType<Death>().FirstMaybe();

            var birthDate = birth.Convert(GetDatePresentation).GetValueOrDefault("?");
            var deathDate = death.Convert(GetDatePresentation).GetValueOrDefault("");
            return string.Format("{0} - {1}", birthDate, deathDate);
        }

        private string GetDatePresentation(Event theEvent) {
            return theEvent.Date.Convert(s => s + (theEvent.IsReliable ? "" : "?")).GetValueOrDefault("?");
        }

        public PersonFile Person {get { return _person; }}
        public override string ToString() {return Name.Value.ToString();}
        public Property<bool> IsSelected { get { return _isSelected; } }
        public IProperty<PersonNamePresentation> Name { get { return _name; } }
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

        public ObservableCollection<InformationPresentation> DetailedInformation { get { return _detailedInformation; } }

        public void TryRemove(IEnumerable<InformationPresentation> infos) {
            var existingInfo = _person.Information.Join(infos, i => i, i => i.Information, (i, ip) => i).ToArray();
            _person.Remove(existingInfo);
        }
    }
}