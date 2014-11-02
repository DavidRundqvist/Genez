using Model;
using Model.PersonInformation;
using Model.PersonInformation.Relations;
// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;

namespace Test.Model {
    [TestFixture]
    public class PersonFileTest {
        private PersonFile _sut;

        [SetUp]
        public void Setup() {
            _sut = new PersonFile();
        }

        [TearDown]
        public void TearDown() {}

        [Test]
        public void Should_set_information() {
            // arrange
            _sut.AddMother(new PersonFile(), new Source());
            _sut.AddFather(new PersonFile(), new Source(), Reliability.Unreliable);
            _sut.AddFather(new PersonFile(), new Source(), Reliability.Unreliable);

            // act
            var father = new Father(new PersonFile(), new Source());
            _sut.Set(father);

            // assert
            Assert.AreEqual(1, _sut.Information.OfType<Mother>().Count());
            Assert.AreEqual(1, _sut.Information.OfType<Father>().Count());
        }
    }
}