using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Gedcom;
using GedcomParser;
using Model;
using Model.PersonInformation;
using Model.PersonInformation.Events;

namespace Infrastructure.Data {
    public class GedcomRepository : IRepository {

        private readonly Lazy<GedcomDatabase> _gedcomDatabase;

        protected internal GedcomRepository(FileInfo fileInfo) {
            _gedcomDatabase = new Lazy<GedcomDatabase>(() => {
                                                           var reader = new GedcomRecordReader();
                                                           reader.ReadGedcom(fileInfo.FullName);
                                                           return reader.Database;
                                                       });
        }

        private PersonFile ToPerson(GedcomIndividualRecord arg) {
            var result = new PersonFile();
            AddName(result, arg.GetName());
            AddGender(result, arg.Sex);
            AddEvents(result, arg.Events);
            return result;
        }

        private void AddEvents(PersonFile result, GedcomRecordList<GedcomIndividualEvent> events) {
            foreach (var individualEvent in events) {
                var dateString = individualEvent.Date != null 
                    ? Maybe.From(individualEvent.Date.DateString) 
                    : Maybe.Empty;
                
                switch (individualEvent.EventType) {
                    case GedcomEvent.GedcomEventType.BIRT:                        
                            result.Add(new Birth(dateString, new Source()));
                        break;
                    case GedcomEvent.GedcomEventType.DEAT:
                            result.Add(new Death(dateString, new Source()));
                        break;
                    default:
                        break;
                }                
            }

        }

        private void AddName(PersonFile person, GedcomName gedcomName) {
            if (!string.IsNullOrEmpty(gedcomName.Name)) {
                person.Add(GetName(gedcomName));
            }
        }

        private void AddGender(PersonFile person, GedcomSex sex) {
            if (sex != GedcomSex.Undetermined)    {
                person.Add(GetGender(sex));                   
            }
        }

        private Gender GetGender(GedcomSex sex) {
            switch (sex) {
                case GedcomSex.Male:
                    return new Gender(GenderType.Male, new Source());
                case GedcomSex.Female:
                    return new Gender(GenderType.Female, new Source());
                default:
                    return new Gender(GenderType.Other, new Source());
            }
        }

        private Name GetName(GedcomName gedcomName) {
            var nameComponents = GetNames(gedcomName);
            return new Name(new PersonName(nameComponents), new Source() );
        }

        private IEnumerable<NameComponent> GetNames(GedcomName gedcomName) {
            if (IsOk(gedcomName.Given))   yield return new NameComponent(gedcomName.Given, NameType.Given);
            if (IsOk(gedcomName.Surname)) yield return new NameComponent(gedcomName.Surname, NameType.Family);
            if (IsOk(gedcomName.Prefix))  yield return new NameComponent(gedcomName.Prefix, NameType.Rank);
            if (IsOk(gedcomName.Nick))    yield return new NameComponent(gedcomName.Nick, NameType.Nick);            
            if (IsOk(gedcomName.Suffix))  yield return new NameComponent(gedcomName.Nick, NameType.Title);            
        }

        private static bool IsOk(string text) {
            return !string.IsNullOrEmpty(text);
        }


        public Task<IEnumerable<PersonFile>> GetAllPeople() {
            return Task.Run(() => {
                                var db = _gedcomDatabase.Value;

                                var people = db.Individuals.ToDictionary(ind => ind, ToPerson);
                                foreach (GedcomFamilyRecord family in db.Families) {
                                    var mother = family.Wife != null
                                        ? db[family.Wife] as GedcomIndividualRecord
                                        : null;
                                    var father = family.Husband != null
                                        ? db[family.Husband] as GedcomIndividualRecord
                                        : null;
                                    foreach (string childID in family.Children) {
                                        var child = db[childID] as GedcomIndividualRecord;
                                        if (father != null) people[child].AddFather(people[father], new Source());
                                        if (mother != null) people[child].AddMother(people[mother], new Source());
                                    }
                                }
                                IEnumerable<PersonFile> result = people.Values;
                                return result;
                            });
        }

        public Task Add(IEnumerable<PersonFile> people) {
            throw new System.NotImplementedException();
        }

        public Task Remove(IEnumerable<PersonFile> people) {
            throw new System.NotImplementedException();
        }

        public Task Update(IEnumerable<PersonFile> people) {
            throw new System.NotImplementedException();
        }
    }
}