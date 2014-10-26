using System.Collections.Generic;
using System.Linq;
using Model;
using Model.PersonInformation;

namespace View {

    public class DesignPersonRegistry : PersonRegistry {
        public DesignPersonRegistry() {
            var designPeople = CreateDesignPeople().ToArray();

            designPeople[0].AddFather(designPeople[2], new Source());

            Add(designPeople);            
        }

        private IEnumerable<PersonFile> CreateDesignPeople() {
            yield return CreatePerson("David", "Rundqvist");
            yield return CreatePerson("Darja", "Utgof");
            yield return CreatePerson("Carl", "Rundqvist");
            yield return CreatePerson("Camilla", "Rundqvist");
            yield return CreatePerson("Monica", "Rundqvist");
        }

        private PersonFile CreatePerson(string firstName, string lastName) {
            var result = new PersonFile();
            result.Add(new Name(new PersonName(new[]{new NameComponent(firstName, NameType.Given), new NameComponent(lastName, NameType.Family)}), new Source()));
            result.Add(new Gender(GenderType.Male, new Source()));
            return result;
        }
    }
}