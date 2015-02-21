using System;
using System.Collections.Generic;
using System.Linq;
using Common.WPF;

namespace Common.Enumerable
{
    public static class IEnumerableExtensions {
        public static ISet<T> ToSet<T>(this IEnumerable<T> self) {
            return new HashSet<T>(self.Distinct());

        }

        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action) {
            foreach (var item in self) {
                action(item);
            }
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> self, T other) {
            return self.Concat(new[] {other});
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> self, T other) {
            return self.Except(new[] {other});
        }

        public static string Join(this IEnumerable<string> self) {
            return self.Join(" ");
        }

        public static string Join(this IEnumerable<string> self, string separator) {
            return string.Join(separator, self);
        }


        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> items) {
            // Optimizations
            var batch = self as BatchObservableCollection<T>;
            if (batch != null) {
                batch.AddBatch(items);
                return;
            }

            var eventCollection = self as EventCollection<T>;
            if (eventCollection != null) {
                eventCollection.Add(items.ToArray());
                return;
            }

            // Normal
            foreach (var item in items) {
                self.Add(item);
            }
        }

        public static void RemoveRange<T>(this ICollection<T> self, IEnumerable<T> items) {
            foreach (var item in items) {
                self.Remove(item);
            }
        }

        public static Maybe<T> FirstMaybe<T>(this IEnumerable<T> self, Func<T, bool> predicate) {
            var item = self.FirstOrDefault<T>(predicate);
            return Maybe.From(item);
        }
        public static Maybe<T> FirstMaybe<T>(this IEnumerable<T> self) {
            var item = self.FirstOrDefault<T>();
            return Maybe.From(item);
        }


        public static IEnumerable<T1> SelectT1<T1, T2>(this IEnumerable<Tuple<T1, T2>> self) {
            return self.Select(item => item.Item1);
        }

        public static IEnumerable<T2> SelectT2<T1, T2>(this IEnumerable<Tuple<T1, T2>> self) {
            return self.Select(item => item.Item2);
        }
    }
}