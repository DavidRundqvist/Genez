using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.WPF.Presentation {
    public class Property<T> : IProperty<T> {

        private T _value;

        public Property(T value) {
            _value = value;
        }

        public Property() : this(default(T)) {}


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The value of this property.
        /// Can be null
        /// </summary>
        public T Value {
            get {
                return _value;
            }
            set {
                if (!Equals(_value, value)) {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}