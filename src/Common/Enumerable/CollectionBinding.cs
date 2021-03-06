using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common.WPF;

namespace Common.Enumerable {
    public static class CollectionBinding {
        public static void BindTo<T1, T2>(this ObservableCollection<T2> self, IEventCollection<T1> other, Func<T1, T2> ctor, Action<T2> dtor) {

            var map = new HashSet<Tuple<T1, T2>>();
            other.CollectionChanged += (sender, args) => {
                                           var toAdd = args.Added.Select(t2 => Tuple.Create<T1, T2>(t2, ctor(t2))).ToArray();
                                           self.AddRange(toAdd.SelectT2());
                                           map.AddRange(toAdd);

                                           var toRemove = map.Join(args.Removed, tuple => tuple.Item1, item => item, (tuple, item) => tuple).ToArray();
                                           foreach (var item in toRemove.SelectT2()) {
                                               self.Remove(item);
                                               dtor(item);
                                           }
                                           map.RemoveRange(toRemove);
                                       };

            var added = other.Select(t2 => Tuple.Create<T1, T2>(t2, ctor(t2))).ToArray();
            self.AddRange(added.SelectT2());            
            map.AddRange(added);                 
        }

        public static void BindTo<T1, T2>(this IEventCollection<T2> self, IEventCollection<T1> other, Func<T1, T2> ctor, Action<T2> dtor) {

            var map = new HashSet<Tuple<T1, T2>>();
            other.CollectionChanged += (sender, args) => {
                                           var toAdd = args.Added.Select(t2 => Tuple.Create<T1, T2>(t2, ctor(t2))).ToArray();
                                           self.Add(toAdd.SelectT2().ToArray());
                                           map.AddRange(toAdd);

                                           var toRemove = map.Join(args.Removed, tuple => tuple.Item1, item => item, (tuple, item) => tuple).ToArray();
                                           foreach (var item in toRemove.SelectT2()) {
                                               self.Remove(item);
                                               dtor(item);
                                           }
                                           map.RemoveRange(toRemove);
                                       };

            var added = other.Select(t2 => Tuple.Create<T1, T2>(t2, ctor(t2))).ToArray();
            self.Add(added.SelectT2().ToArray());
            map.AddRange(added);            
        }

        public static void BindTo<T1, T2>(this IEventCollection<T2> self, IEventCollection<T1> other, Func<T1, T2> ctor) {
            self.BindTo(other, ctor, t2 => { });
            

        }

        public static void BindTo<T1, T2>(this ObservableCollection<T2> self, IEventCollection<T1> other, Func<T1, T2> ctor) {
            self.BindTo(other, ctor, t2 => { });
        }
    }
}