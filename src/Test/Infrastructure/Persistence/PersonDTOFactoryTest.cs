using System.Linq;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Information;
using Model;
using NUnit.Framework;

namespace Test.Infrastructure.Persistence {
    [TestFixture]
    public class PersonDTOFactoryTest {

        private PersonDTOFactory _sut;
        private InformationDTOFactory _informationFactory;

        [SetUp]
        public void Setup() {
            _informationFactory = new InformationDTOFactory();
            _sut = new PersonDTOFactory(_informationFactory);
        }

        [TearDown]
        public void TearDown() {}

        [Test]
        public void Should_create_correct_dtos() {
            // arrange
            var registry = new DesignPersonRegistry();
            var person = registry.First();

            // act
            var result = _sut.ToDTO(person);

            // assert
            var names = result.Information.OfType<NameDTO>().ToList();
            Assert.IsNotEmpty(names);
        }
    }
}