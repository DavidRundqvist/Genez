using System;
using Common.Enumerable;
using Infrastructure.IO;
using Model;

namespace Infrastructure.Persistence {
    public class RegistryPersistence {
        private readonly IFileSystem _fileSystem;
        private readonly PersonRegistry _registry;
        private readonly ISerializer _serializer;
        private readonly PersonDTOFactory _personDtoFactory;

        public RegistryPersistence(IFileSystem fileSystem, PersonRegistry registry, ISerializer serializer, PersonDTOFactory personDtoFactory) {
            _fileSystem = fileSystem;
            _registry = registry;
            _serializer = serializer;
            _personDtoFactory = personDtoFactory;
            
        }

        public void Initialize() {
            _registry.CollectionChanged += RegistryCollectionChanged;
            _registry.ForEach(Add);
        }

        void RegistryCollectionChanged(object sender, global::Common.Enumerable.CollectionEventArgs<PersonFile> args) {
            foreach (var personFile in args.Added) {
                Add(personFile);
            }
            foreach (var personFile in args.Removed) {
                Remove(personFile);
            }
        }

        private void Add(PersonFile personFile) {
            personFile.Changed += PersonFileChanged;
            Persist(personFile);
        }

        private void Remove(PersonFile personFile) {
            _fileSystem.Delete(personFile.Id);
        }

        void PersonFileChanged(object sender, EventArgs eventArgs) {
            var p = sender as PersonFile;
            Persist(p);
        }

        private void Persist(PersonFile person) {
            var dto = _personDtoFactory.ToDTO(person);
            using (var stream = _fileSystem.OpenWriteStream(dto.Id)) {
                _serializer.Serialize(stream, dto);
            }
        }
    }
}