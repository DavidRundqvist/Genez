using System.ComponentModel;

namespace Common.WPF.Presentation {
    public interface IProperty<T> : INotifyPropertyChanged {
        /// <summary>
        /// The value of this property.
        /// Can be null
        /// </summary>
        T Value { get; }
    }
}