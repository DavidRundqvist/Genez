using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Common;
using Common.Coding;
using Common.Enumerable;
using Infrastructure.IO;
using Infrastructure.Persistence;
using Model;

namespace Infrastructure.Data {
    public class DiskRepository : IRepository{

        private readonly DirectoryInfo _fileSystem;
        private readonly PersonDTOFactory _personDtoFactory;
        private readonly PersonFactory _personFactory;

        private readonly DataContractSerializer _personSerializer;

        public DiskRepository(DirectoryInfo fileSystem, PersonDTOFactory personDtoFactory, PersonFactory personFactory) {
            _fileSystem = fileSystem;
            _personDtoFactory = personDtoFactory;
            _personFactory = personFactory;

            _personSerializer = new DataContractSerializer(typeof (PersonDTO));
        }


        public Task<IEnumerable<PersonFile>> GetAllPeople() {
            return Task.Run(() => {
                                var files = _fileSystem.GetSubFolder("People").GetFiles("*.xml");
                                var dtos = files.Select(LoadPerson);
                                return _personFactory.From(dtos);
                            });
        }

        public Task Add(IEnumerable<PersonFile> people) {
            var dtos = people.Select(p => _personDtoFactory.ToDTO(p));
            return Persist(dtos);
        }

        public Task Remove(IEnumerable<PersonFile> people) {
            var ids = people.Select(p => p.Id.Guid);
            return Task.Run(() => ids.ForEach(id => GetFileInfo(id).Delete()));
        }

        public Task Update(IEnumerable<PersonFile> people) {
            var dtos = people.Select(p => _personDtoFactory.ToDTO(p));
            return Persist(dtos);          
        }


        private Task Persist(IEnumerable<PersonDTO> dtos) {
            return Task.Run(() => {
                foreach (var person in dtos) {
                    var fileInfo = GetFileInfo(person.Id);
                    SavePerson(person, fileInfo);
                }
            });
        }


        private void SavePerson(PersonDTO dto, FileInfo fileInfo) {
            using (var stream = fileInfo.Open(FileMode.Create, FileAccess.Write)) {
                using (var writer = new XmlTextWriter(stream, Encoding.Unicode) {Formatting = Formatting.Indented}) {
                    _personSerializer.WriteObject(writer, dto);
                }
            }
        }

        private PersonDTO LoadPerson(FileInfo file) {
            using (var stream = file.OpenRead()) {
                using (var reader = new XmlTextReader(stream)) {
                    var dto = (PersonDTO)_personSerializer.ReadObject(reader);
                    return dto;
                }
            }
        }

        private FileInfo GetFileInfo(Guid id) {
            return _fileSystem.GetFile(string.Format(@"{0}\{1}.{2}", "People", id, "xml"));
        }
    }
}