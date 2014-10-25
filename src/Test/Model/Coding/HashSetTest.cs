// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;

namespace Test.Model.Coding
{
    [TestFixture]
    public class HashSetTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Should_not_add_duplicates()
        {
            // arrange
            var sut = new HashSet<int>();

            // act
            sut.Add(17);
            sut.Add(17);

            // assert
            Assert.AreEqual(1, sut.Count );
        }
    }
}