using System.Collections.Generic;

namespace Common.Enumerable {
    public interface IEventCollection<T> : ICollection<T> {
        event CollectionEventHandler<T> CollectionChanged;
        void Add(params T[] items);
        void Remove(params T[] items);
        void Replace(IEnumerable<T> remove, IEnumerable<T> add);
    }
}