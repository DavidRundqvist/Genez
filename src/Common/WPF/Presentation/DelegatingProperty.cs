using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.WPF.Presentation {
    public class DelegatingProperty<T> : IProperty<T> {
        private readonly Func<T> _retriever;

        public DelegatingProperty(Func<T> retriever ) {
            _retriever = retriever;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public T Value {
            get { return _retriever(); }
        }

        public void RaisePropertyChanged() {
            OnPropertyChanged();
        }

        protected virtual void OnPropertyChanged() {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs("Value"));
        }
    }
}