﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using View.Global;

namespace View.AncestryGraph {
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}