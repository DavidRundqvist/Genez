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
        private readonly PersonFactory _personFactory;

        private readonly FileName _registryFileName;
        private readonly XmlSerializer _personSerializer;
        private readonly XmlSerializer _registrySerializer;

        public DiskRepository(IFileSystem fileSystem, PersonDTOFactory personDtoFactory, PersonFactory personFactory) {
            _fileSystem = fileSystem;
            _personDtoFactory = personDtoFactory;
            _personFactory = personFactory;
            _registryFileName = new FileName("PeopleRegistry.xml");

            _personSerializer = new XmlSerializer(typeof (PersonDTO));
            _registrySerializer = new XmlSerializer(typeof (PersonRegistryDTO));
        }


        public IEnumerable<PersonFile> GetAllPeople() {
            var registry = LoadRegistry();
            var personDtos = LoadPeople(registry.People);
            return _personFactory.From(personDtos);
        }

        private IEnumerable<PersonDTO> LoadPeople(IEnumerable<PersonDTOReference> people) {
            foreach (var dtoReference in people) {
                var fileName = GetFileName(new Id<PersonFile>(dtoReference.Id));
                using (var stream = _fileSystem.OpenReadStream(fileName)) {
                    var dto = (PersonDTO) _personSerializer.Deserialize(stream);
                    yield return dto;
                }
            }
        }

        public void Add(IEnumerable<PersonFile> people) {
            var registry = LoadRegistry();
            foreach (var personFile in people) {
                var dto = _personDtoFactory.ToDTO(personFile);
                var fileName = GetFileName(personFile.Id);
                Persist(dto, fileName);
                registry.Add(new PersonDTOReference{FileName = fileName, Id = dto.Id});
            }
            Persist(registry);
        }

        private void Persist(PersonRegistryDTO personRegistryDto) {
            using (var stream = _fileSystem.OpenWriteStream(_registryFileName)) {
                _registrySerializer.Serialize(stream, personRegistryDto);
            }
        }

        private PersonRegistryDTO LoadRegistry() {            
            if (!_fileSystem.DoesFileExist(_registryFileName))
                return new PersonRegistryDTO();

            using (var stream = _fileSystem.OpenReadStream(_registryFileName)) {
                return (PersonRegistryDTO) _registrySerializer.Deserialize(stream);
            }            
        }


        public void Remove(IEnumerable<PersonFile> people) {
            var registry = LoadRegistry();
            foreach (var personFile in people) {
                var fileName = GetFileName(personFile.Id);
                _fileSystem.Delete(fileName);
                registry.Remove(personFile.Id);
            }
            Persist(registry);          
        }

        public void Update(IEnumerable<PersonFile> people) {           
            foreach (var personFile in people) {
                var dto = _personDtoFactory.ToDTO(personFile);
                var fileName = GetFileName(personFile.Id);
                Persist(dto, fileName);                
            }            
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