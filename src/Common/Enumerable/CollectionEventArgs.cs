using System;

namespace Common.Enumerable {
    public class CollectionEventArgs<T> : EventArgs {
        public T[] Added { get; private set; }
        public T[] Removed { get; private set; }
        public CollectionEventArgs(T[] added, T[] removed) {
            Added = added;
            Removed = removed;
        }

        public static CollectionEventArgs<T> CreateAdded(params T[] items) {
            return new CollectionEventArgs<T>(items, new T[0]);
        }

        public static CollectionEventArgs<T> CreateRemoved(params T[] items) {
            return new CollectionEventArgs<T>(new T[0], items);
        }

    }
}