using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Core;
using NUnit.Framework;

namespace Test {
    public class AssertExtensions {
        public static void AssertSequences<T>(IEnumerable<T> expected, IEnumerable<T> actual) {
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}