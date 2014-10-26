using System.ComponentModel;

namespace View.Global {
    public interface IProperty<T> : INotifyPropertyChanged {
        /// <summary>
        /// The value of this property.
        /// Can be null
        /// </summary>
        T Value { get; }
    }
}