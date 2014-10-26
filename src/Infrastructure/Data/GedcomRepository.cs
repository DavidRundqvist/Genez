using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gedcom;
using GedcomParser;
using Model;
using Model.PersonInformation;

namespace Infrastructure.Data {
    public class GedcomRepository : IPersonRepository {

        private readonly GedcomDatabase _gedcomDatabase;

        protected GedcomRepository(FileInfo fileInfo) {
            var reader = new GedcomRecordReader();
            reader.ReadGedcom(fileInfo.FullName);
            _gedcomDatabase = reader.Database;
        }

        private PersonFile ToPerson(GedcomIndividualRecord arg) {
            var result = new PersonFile();
            AddName(result, arg.GetName());
            AddGender(result, arg.Sex);
             
            return result;
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
            if (IsOk(gedcomName.Prefix))  yield return new NameComponent(gedcomName.Given, NameType.Title);
            if (IsOk(gedcomName.Nick))    yield return new NameComponent(gedcomName.Given, NameType.Primary);            
        }

        private static bool IsOk(string text) {
            return !string.IsNullOrEmpty(text);
        }


        public IEnumerable<PersonFile> GetAllPeople() {
            var people = _gedcomDatabase.Individuals.ToDictionary(ind => ind, ToPerson);



            foreach (GedcomFamilyRecord family in _gedcomDatabase.Families)
            {
                var mother = family.Wife != null ? _gedcomDatabase[family.Wife] as GedcomIndividualRecord : null;
                var father = family.Husband != null ? _gedcomDatabase[family.Husband] as GedcomIndividualRecord : null;
                foreach (string childID in family.Children)
                {
                    var child = _gedcomDatabase[childID] as GedcomIndividualRecord;
                    if (father != null) people[child].AddFather(people[father], new Source());
                    if (mother != null) people[child].AddMother(people[mother], new Source());
                }
            }
 

            return people.Values;
        }


        public static GedcomRepository Parse(FileInfo file) {
            return new GedcomRepository(file);
        }

    }
}