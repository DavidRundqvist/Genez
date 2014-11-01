using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Model;
using Model.PersonInformation;
using Model.PersonInformation.Events;

namespace View.Global {

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
            var source = new Source();
            result.Add(new Name(new PersonName(new[]{new NameComponent(firstName, NameType.Given), new NameComponent(lastName, NameType.Family)}), source));
            result.Add(new Gender(GenderType.Male, source));
            result.Add(new Birth(Maybe.From((DateTime.Now - TimeSpan.FromDays(3600)).ToShortDateString()), source));
            result.Add(new Death(Maybe.From(DateTime.Now.ToShortDateString()), source));
            return result;
        }
    }
}