using Model;
using Model.Constraints;
using Model.PersonInformation;
using Model.PersonInformation.Relations;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace Test.Model.Constraints {
    [TestFixture]
    public class ParentGenderConstraintTest {

        private IConstraint _sut;

        [SetUp]
        public void Setup() {
            _sut = new ParentGenderConstraint();
        }

        [TearDown]
        public void TearDown() {}

        [Test]
        public void Should_not_allow_male_mothers() {
            // arrange
            var mother = new PersonFile();
            mother.Add(new Gender(GenderType.Male, new Source()));
            var motherRelation = new Mother(mother, new Source());

            // act
            var result = _sut.Examine(new[] { motherRelation });

            // assert
            Assert.IsFalse(result.IsOk);
        }
    }
}