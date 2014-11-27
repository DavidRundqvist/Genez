using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common;
using Common.Coding;
using Infrastructure.IO;
using Infrastructure.Persistence;
using Model;

namespace Infrastructure.Data {
    public class DiskRepository : IRepository{

        private readonly DirectoryInfo _fileSystem;
        private readonly PersonDTOFactory _personDtoFactory;
        private readonly PersonFactory _personFactory;

        private readonly XmlSerializer _personSerializer;

        public DiskRepository(DirectoryInfo fileSystem, PersonDTOFactory personDtoFactory, PersonFactory personFactory) {
            _fileSystem = fileSystem;
            _personDtoFactory = personDtoFactory;
            _personFactory = personFactory;

            _personSerializer = new XmlSerializer(typeof (PersonDTO));
        }


        public Task<IEnumerable<PersonFile>> GetAllPeople() {
            return Task.Run(() => {
                                var files = _fileSystem.GetSubFolder("People").GetFiles("*.xml");
                                var dtos = files.Select(LoadPerson);
                                return _personFactory.From(dtos);
                            });
        }


        private PersonDTO LoadPerson(FileInfo file) {
            using (var stream = file.OpenRead()) {
                var dto = (PersonDTO) _personSerializer.Deserialize(stream);
                return dto;
            }
        }

        public Task Add(IEnumerable<PersonFile> people) {
            return Task.Run(() => {
                                foreach (var personFile in people) {
                                    var fileInfo = GetFileInfo(personFile.Id);
                                    var dto = _personDtoFactory.ToDTO(personFile);
                                    Persist(dto, fileInfo);
                                }
                            });
        }




        public void Remove(IEnumerable<PersonFile> people) {
            foreach (var personFile in people) {
                var fileInfo = GetFileInfo(personFile.Id);
                fileInfo.Delete();
            }
        }

        public void Update(IEnumerable<PersonFile> people) {           
            foreach (var personFile in people) {
                var dto = _personDtoFactory.ToDTO(personFile);
                var fileInfo = GetFileInfo(personFile.Id);
                Persist(dto, fileInfo);                
            }            
        }


        private void Persist(PersonDTO dto, FileInfo fileInfo) {
            using (var stream = fileInfo.OpenWrite()) {                
                _personSerializer.Serialize(stream, dto);
            }
        }

        private FileInfo GetFileInfo(Id<PersonFile> id) {
            return _fileSystem.GetFile(string.Format(@"{0}\{1}.{2}", "People", id.Guid, "xml"));
        }
    }
}