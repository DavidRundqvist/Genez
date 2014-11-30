using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using Infrastructure.IO;
using Infrastructure.Persistence;
using Model;
using Model.PersonInformation;
using NUnit.Framework;
using Moq;

namespace Test.Infrastructure.Persistence {
    [TestFixture]
    public class RegistryPersistenceTest {

        private RegistryPersistence _sut;
        private Mock<IRepository> _repository;
        private PersonRegistry _registry;
        private PersonRegistry _designRegistry;
        

        [SetUp]
        public void Setup() {
            _repository = new Mock<IRepository>();
            _registry = new PersonRegistry();
            _designRegistry = new DesignPersonRegistry();
            _sut = new RegistryPersistence(_repository.Object);
            _sut.AttachTo(_registry);
        }

        [TearDown]
        public void TearDown() {}

        [Test]
        public void Should_store_added_people() {
            // arrange


            // act
            _registry.Add(_designRegistry.First());

            // assert
            _repository.Verify(r => r.Add(It.Is<IEnumerable<PersonFile>>(people => people.Contains(_designRegistry.First()))));
        }

        [Test]
        public void Should_delete_file_when_removing_person() {
            // arrange
            _registry.Add(_designRegistry.First());

            // act
            _registry.Remove(_designRegistry.First());

            // assert
            _repository.Verify(r => r.Remove(It.Is<IEnumerable<PersonFile>>(people => people.Contains(_designRegistry.First()))));
        }

        [Test]
        public void Should_update_file_when_person_changes() {
            // arrange
            _registry.Add(_designRegistry.First());

            // act
            _registry.First().Add(new Name(new PersonName(new[]{new NameComponent("The Cripple", NameType.Given)}),new Source() ));

            // assert
            _repository.Verify(r => r.Update(It.Is<IEnumerable<PersonFile>>(people => people.Contains(_designRegistry.First()))), Times.Exactly(1));
        }

    }
}