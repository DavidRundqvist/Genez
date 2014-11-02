using System.Collections.Generic;
using System.Linq;
using Common;
using Model.PersonInformation;
using Model.PersonInformation.Events;

namespace Model {

    public class DesignPersonRegistry : PersonRegistry {
        public DesignPersonRegistry() {
            var designPeople = CreateDesignPeople().ToArray();


            Add(designPeople);            
        }

        private IEnumerable<PersonFile> CreateDesignPeople() {
            var source = new Source();

            var tyrion = CreatePerson("Tyrion", "Lannister");            
            var jaime = CreatePerson("Jaime", "Lannister");            
            var cersei = CreatePerson("Cersei", "Lannister");            
            var tywin = CreatePerson("Tywin", "Lannister");            
            var joanna = CreatePerson("Joanna", "Lannister");
            var robert = CreatePerson("Robert", "Baratheon");
            var joffrey = CreatePerson("Joffrey", "Baratheon");
            var tommen = CreatePerson("Tommen", "Baratheon");
            var myrcella = CreatePerson("Myrcella", "Baratheon");

            joanna.Add(new Gender(GenderType.Female, source));
            tywin.Add(new Gender(GenderType.Male, source));
            robert.Add(new Gender(GenderType.Male, source));

            tyrion.AddFather(tywin, source);
            tyrion.AddMother(joanna, source);
            tyrion.Add(new Gender(GenderType.Male, source));
            jaime.AddFather(tywin, source);
            jaime.AddMother(joanna, source);
            jaime.Add(new Gender(GenderType.Male, source));
            cersei.AddFather(tywin, source);
            cersei.AddMother(joanna, source);
            cersei.Add(new Gender(GenderType.Female, source));

            joffrey.AddFather(robert, source, Reliability.Unreliable);
            joffrey.AddFather(jaime, source, Reliability.Unreliable);
            joffrey.AddMother(cersei, source, Reliability.Reliable);
            tommen.AddFather(robert, source, Reliability.Unreliable);
            tommen.AddFather(jaime, source, Reliability.Unreliable);
            tommen.AddMother(cersei, source, Reliability.Reliable);
            myrcella.AddFather(robert, source, Reliability.Unreliable);
            myrcella.AddFather(jaime, source, Reliability.Unreliable);
            myrcella.AddMother(cersei, source, Reliability.Reliable);

            joffrey.Add(new Gender(GenderType.Male, source));
            tommen.Add(new Gender(GenderType.Male, source));
            myrcella.Add(new Gender(GenderType.Male, source));

            tywin.Add(new Death(Maybe.From("Last book"), source));
            joanna.Add(new Death(Maybe.From("Before book 1"), source));
            robert.Add(new Death(Maybe.From("Book 1"), source));
            joffrey.Add(new Death(Maybe.From("Book 5"), source));

            return new[] {tyrion, jaime, cersei, tywin, joanna, robert, joffrey, tommen, myrcella};
        }

        private PersonFile CreatePerson(string firstName, string lastName) {
            var result = new PersonFile();
            var source = new Source();
            result.Add(new Name(new PersonName(new[]{new NameComponent(firstName, NameType.Given), new NameComponent(lastName, NameType.Family)}), source));
            return result;
        }
    }
}