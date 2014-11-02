using System.Linq;
using Model;
using Model.Constraints;
using Model.PersonInformation;
using Model.PersonInformation.Relations;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace Test.Model.Constraints {
    [TestFixture]
    public class SingularRelativeConstraintTest {
        private IConstraint _sut;

        [SetUp]
        public void Setup() {
            _sut = new MotherConstraint();
        }

        [TearDown]
        public void TearDown() {}

        [Test]
        public void Should_allow_several_sources_claiming_same_parent() {
            // arrange
            var person = new PersonFile();
            var m1 = new Mother(person, new Source());
            var m2 = new Mother(person, new Source());

            // act
            var result = _sut.Examine(new[] {m1, m2});

            // assert
            Assert.IsTrue(result.IsOk);
        }

        [Test]
        public void Should_allow_several_uncertain_different_parents() {
            // arrange            
            var m1 = new Mother(new PersonFile(), new Source(), Reliability.Unreliable);
            var m2 = new Mother(new PersonFile(), new Source(), Reliability.Unreliable);

            // act
            var result = _sut.Examine(new[] { m1, m2 });

            // assert
            Assert.IsTrue(result.IsOk);
        }

        [Test]
        public void Should_not_allow_several_certain_parents() {
            // arrange
            var m1 = new Mother(new PersonFile(), new Source(), Reliability.Reliable);
            var m2 = new Mother(new PersonFile(), new Source(), Reliability.Reliable);

            // act
            var result = _sut.Examine(new[] { m1, m2 });

            // assert
            Assert.IsFalse(result.IsOk);
        }

        [Test]
        public void Should_not_allow_several_certain_and_uncertain_parents() {
            // arrange
            var m1 = new Mother(new PersonFile(), new Source(), Reliability.Reliable);
            var m2 = new Mother(new PersonFile(), new Source(), Reliability.Unreliable);

            // act
            var result = _sut.Examine(new[] { m1, m2 });

            // assert
            Assert.IsFalse(result.IsOk);
        }

    }
}