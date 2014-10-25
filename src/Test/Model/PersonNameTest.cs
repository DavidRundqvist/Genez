using Model;
// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;

namespace Test.Model {
    [TestFixture]
    public class PersonNameTest {

        [Test]
        public void Should_override_ToString() {
            // arrange
            var sut = new PersonName(new[] {
                                               new NameComponent("1st of England", NameType.Title),
                                               new NameComponent("William", NameType.Given),
                                               new NameComponent("of Normandy", NameType.Family),
                                               new NameComponent("King", NameType.Rank),
                                           });

            // act
            var result = sut.ToString();

            // assert
        }
    }
}