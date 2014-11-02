using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Common.Coding;
using Infrastructure.IO;
using Infrastructure.Persistence;
using Model;

namespace Infrastructure.Data {
    public class DiskRepository : IRepository{

        private readonly IFileSystem _fileSystem;
        private readonly PersonDTOFactory _personDtoFactory;

        private readonly FileName _registryFileName;
        private readonly PersonRegistryDTO _personRegistryDto = new PersonRegistryDTO();
        private readonly XmlSerializer _personSerializer;
        private readonly XmlSerializer _registrySerializer;

        public DiskRepository(IFileSystem fileSystem, PersonDTOFactory personDtoFactory) {
            _fileSystem = fileSystem;
            _personDtoFactory = personDtoFactory;
            _registryFileName = new FileName("PeopleRegistry.xml");

            _personSerializer = new XmlSerializer(typeof (PersonDTO));
            _registrySerializer = new XmlSerializer(typeof (PersonRegistryDTO));
        }


        public IEnumerable<PersonFile> GetAllPeople() {
            return Enumerable.Empty<PersonFile>();
        }

        public void Store(IEnumerable<PersonFile> people) {
            var saveRegistry = false;
            foreach (var personFile in people) {
                var dto = _personDtoFactory.ToDTO(personFile);
                var fileName = GetFileName(personFile.Id);
                Persist(dto, fileName);
                saveRegistry |= AddToRegistry(personFile, fileName);
            }
            if (saveRegistry) {
                PersistRegistry();
            }
        }

        private void PersistRegistry() {
            using (var stream = _fileSystem.OpenWriteStream(_registryFileName)) {
                _registrySerializer.Serialize(stream, _personRegistryDto);
            }
        }

        private bool AddToRegistry(PersonFile personFile, FileName fileName) {
            if (!_personRegistryDto.Contains(personFile.Id)) {
                _personRegistryDto.Add(new PersonDTOReference(){FileName = fileName, Id = personFile.Id.Guid});
                return true;
            }
            return false;
        }

        public void Remove(IEnumerable<PersonFile> people) {
            var saveRegistry = false;
            foreach (var personFile in people) {
                var fileName = GetFileName(personFile.Id);
                _fileSystem.Delete(fileName);
                saveRegistry |= RemoveFromRegistry(personFile);
            }
            if (saveRegistry) {
                PersistRegistry();
            }            
        }

        private bool RemoveFromRegistry(PersonFile personFile) {
            if (_personRegistryDto.Contains(personFile.Id)) {
                _personRegistryDto.Remove(personFile.Id);
                return true;
            }
            return false;            
        }


        private void Persist(PersonDTO dto, FileName fileName) {
            using (var stream = _fileSystem.OpenWriteStream(fileName)) {                
                _personSerializer.Serialize(stream, dto);
            }
        }

        private FileName GetFileName(Id<PersonFile> id) {
            return new FileName(string.Format(@"{0}\{1}.{2}", "People", id.Guid, "xml"));
        }
    }
}