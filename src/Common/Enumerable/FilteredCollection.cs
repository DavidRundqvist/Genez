using System;
using System.Collections.Generic;
using System.Linq;
using Common.WPF.Presentation;

namespace Common.Enumerable {
    public class FilteredCollection<T> : EventCollection<T> {
        private readonly EventCollection<T>  _backingCollection;
        private readonly Func<T, Property<bool>> _propertySelector;

        public FilteredCollection(EventCollection<T> backingCollection, Func<T, Property<bool>> propertySelector)
        {
            _backingCollection = backingCollection;
            _propertySelector = propertySelector;
            _backingCollection.CollectionChanged += BackingCollectionChanged;
            BindTo(backingCollection);
            Synchronize();
        }

        private void BackingCollectionChanged(object sender, CollectionEventArgs<T> args) {
            BindTo(args.Added);
            UnbindFrom(args.Removed);
        }

        private void UnbindFrom(IEnumerable<T> args) {
            foreach (var item in args) {
                _propertySelector(item).PropertyChanged -= ItemPropertyChanged;
            }
        }

        private void BindTo(IEnumerable<T> items) {
            foreach (var item in items) {
                _propertySelector(item).PropertyChanged += ItemPropertyChanged;
            }
        }

        void ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            Synchronize();
        }

        private void Synchronize() {
            var shouldBeInList = _backingCollection.Where(item => _propertySelector(item).Value).ToList();
            var toAdd = shouldBeInList.Except(this).ToArray();
            var toRemove = this.Except(shouldBeInList).ToArray();
            Replace(toRemove, toAdd);
        }
    }
}