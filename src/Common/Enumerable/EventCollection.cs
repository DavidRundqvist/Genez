using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Enumerable {
    
    /// <summary>
    /// Collection with events
    /// Since ObservableCollection and BindingList are so awkward...
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventCollection<T> : IEventCollection<T> {
        private readonly HashSet<T> _backing = new HashSet<T>();


        public IEnumerator<T> GetEnumerator() {
            return _backing.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(T item) {
            _backing.Add(item);
            OnCollectionChanged(CollectionEventArgs<T>.CreateAdded(item));
        }

        public void Clear() {
            var items = _backing.ToArray();
            _backing.Clear();
            OnCollectionChanged(CollectionEventArgs<T>.CreateRemoved(items));
        }

        public bool Contains(T item) {
            return _backing.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            _backing.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item) {
            var result = _backing.Remove(item);
            OnCollectionChanged(CollectionEventArgs<T>.CreateRemoved(item));
            return result;
        }

        public int Count { get { return _backing.Count; } }

        public bool IsReadOnly {
            get { return false; }
        }

        public event CollectionEventHandler<T> CollectionChanged;
        public void Add(params T[] items) {
            var toAdd = items.ToArray();
            foreach (var item in toAdd) {
                _backing.Add(item);
            }
            OnCollectionChanged(CollectionEventArgs<T>.CreateAdded(toAdd));
        }

        public void Remove(params T[] items) {
            var toRemove = items.ToArray();
            foreach (var item in toRemove) {
                _backing.Remove(item);
            }
            OnCollectionChanged(CollectionEventArgs<T>.CreateRemoved(toRemove));
        }


        public void Replace(IEnumerable<T> remove, IEnumerable<T> add) {
            var toAdd = add.ToArray();
            var toRemove = remove.ToArray();

            foreach (var item in toAdd) {
                _backing.Add(item);
            }
            foreach (var item in toRemove) {
                _backing.Remove(item);
            }

            OnCollectionChanged(new CollectionEventArgs<T>(toAdd, toRemove));
        }

        protected virtual void OnCollectionChanged(CollectionEventArgs<T> args) {
            var handler = CollectionChanged;
            if (handler != null) handler(this, args);
        }
    }
}