using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.WPF {
    /// <summary>
    /// http://peteohanlon.wordpress.com/2008/10/22/bulk-loading-in-observablecollection/
    /// Why doesn't WPF have this method built-in...
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BatchObservableCollection<T> : ObservableCollection<T> {
        private bool _suppressNotification = false;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            if (!_suppressNotification) {
                base.OnCollectionChanged(e);
            }
        }

        public void AddBatch(IEnumerable<T> list) {

            _suppressNotification = true;

            foreach (T item in list) {
                Add(item);
            }
            _suppressNotification = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
