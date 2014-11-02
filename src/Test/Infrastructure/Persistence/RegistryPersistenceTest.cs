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
        private Mock<IFileSystem> _fileSystem;
        private PersonRegistry _registry;
        private PersonRegistry _designRegistry;
        private Mock<ISerializer> _serializer;

        [SetUp]
        public void Setup() {
            _fileSystem = new Mock<IFileSystem>();
            _registry = new PersonRegistry();
            _designRegistry = new DesignPersonRegistry();
            _serializer = new Mock<ISerializer>();
            _sut = new RegistryPersistence(_fileSystem.Object, _registry, _serializer.Object, new PersonDTOFactory(new InformationDTOFactory()));
        }

        [TearDown]
        public void TearDown() {}

        [Test]
        public void Should_create_new_file_for_added_person() {
            // arrange


            // act
            _registry.Add(_designRegistry.First());

            // assert
            _fileSystem.Verify(f => f.OpenWriteStream(_designRegistry.First().Id));
        }

        [Test]
        public void Should_delete_file_when_removing_person() {
            // arrange
            _registry.Add(_designRegistry.First());

            // act
            _registry.Remove(_designRegistry.First());

            // assert
            _fileSystem.Verify(f => f.Delete(_designRegistry.First().Id));
        }

        [Test]
        public void Should_update_file_when_person_changes() {
            // arrange
            _registry.Add(_designRegistry.First());

            // act
            _registry.First().Add(new Name(new PersonName(new[]{new NameComponent("The Cripple", NameType.Given)}),new Source() ));

            // assert
            _fileSystem.Verify(f => f.OpenWriteStream(_designRegistry.First().Id), Times.Exactly(2));
        }

    }
}