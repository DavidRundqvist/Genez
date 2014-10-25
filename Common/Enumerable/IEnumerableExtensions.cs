using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Enumerable
{
    public static class IEnumerableExtensions
    {
        public static ISet<T> ToSet<T>(this IEnumerable<T> self)
        {
            return new HashSet<T>(self.Distinct());
            
        }
        
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> self, T other) {
            return self.Concat(new []{other});
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> self, T other) {
            return self.Except(new[] { other });
        }

        public static string Join(this IEnumerable<string> self) {
            return string.Join(" ", self);
        }
    }
}