using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Common.Enumerable;
using Common.WPF;
using Common.WPF.Presentation;
using Model;
using Model.PersonInformation.Events;
using View.Global;

namespace View.Editor {
    public class PersonEditorPresentation {
        private readonly SelectedPeople _selectedPeople;
        private readonly EventCollection<InformationPresentation> _information = new EventCollection<InformationPresentation>();
        private readonly ButtonPresentation _addButton;
        private readonly ButtonPresentation _removeButton;
        private readonly FilteredCollection<InformationPresentation> _selectedInformation;
        private readonly ObservableCollection<InformationPresentation> _wpfInformation;
        private readonly DelegatingProperty<Visibility> _editorVisibility;
        private readonly DelegatingProperty<bool> _removeEnabled;


        public PersonEditorPresentation(SelectedPeople selectedPeople) {
            _selectedPeople = selectedPeople;
            _selectedInformation = new FilteredCollection<InformationPresentation>(_information, p => p.IsSelected);
            _selectedPeople.CollectionChanged += (s, e) => SelectedPeopleChanged();            
            _selectedInformation.CollectionChanged += (s, e) => SelectedInformationChanged();            
            _wpfInformation = new ObservableCollection<InformationPresentation>();
            _wpfInformation.BindTo(_information, p => p);

            _addButton = new ButtonPresentation("Add", Add);
            _removeEnabled = new DelegatingProperty<bool>(() => _selectedInformation.Any());
            _removeButton = new ButtonPresentation("Remove", Remove, _removeEnabled);                
            _editorVisibility = new DelegatingProperty<Visibility>(() => _selectedPeople.Any() ? Visibility.Visible : Visibility.Collapsed);
            SelectedPeopleChanged();
        }

        private void SelectedInformationChanged() {
            _removeEnabled.RaisePropertyChanged();
        }

        public ObservableCollection<InformationPresentation> Information { get { return _wpfInformation; } }
        public ButtonPresentation AddButton { get { return _addButton; } }
        public ButtonPresentation RemoveButton { get { return _removeButton; } }
        public IProperty<Visibility> EditorVisibility {get { return _editorVisibility; }}

        private void Remove() {
            foreach (var person in _selectedPeople) {
                foreach (var informationPresentation in _selectedInformation) {
                    person.TryRemove(informationPresentation);
                }
            }
            SelectedPeopleChanged(); // hack
        }

        private void Add() {
        }

        private void SelectedPeopleChanged() {            
            _information.Clear();
            _information.Add(_selectedPeople.SelectMany(p => p.DetailedInformation).Distinct().ToArray());
            _editorVisibility.RaisePropertyChanged();
            _removeEnabled.RaisePropertyChanged();
        }

    }
}