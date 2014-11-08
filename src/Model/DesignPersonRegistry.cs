using System.Collections.Generic;
using System.Linq;
using Common;
using Model.PersonInformation;
using Model.PersonInformation.Events;
using Model.PersonInformation.Relations;

namespace Model {

    public class DesignPersonRegistry : PersonRegistry {

        public static DesignPersonRegistry TheOne = new DesignPersonRegistry();


        public DesignPersonRegistry() {
            var designPeople = CreateDesignPeople().ToArray();
            Add(designPeople);            
        }

        private IEnumerable<PersonFile> CreateDesignPeople() {
            var source = new Source();

            var tyrion = CreatePerson("", "Tyrion", "Lannister", "the Imp");            
            var jaime = CreatePerson("Ser", "Jaime", "Lannister", "the Kingslayer");            
            var cersei = CreatePerson("Queen", "Cersei", "Lannister");            
            var tywin = CreatePerson("Lord", "Tywin", "Lannister");            
            var joanna = CreatePerson("Lady", "Joanna", "Lannister");
            var robert = CreatePerson("King", "Robert", "Baratheon");
            var joffrey = CreatePerson("Prince", "Joffrey", "Baratheon");
            var tommen = CreatePerson("Prince", "Tommen", "Baratheon");
            var myrcella = CreatePerson("Princess", "Myrcella", "Baratheon");

            AddName(joffrey, source, "King", "Joffrey", "Baratheon", "");

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
            myrcella.Add(new Gender(GenderType.Female, source));

            tywin.Add(new Death(Maybe.From("Last book"), source));
            joanna.Add(new Death(Maybe.From("Before book 1"), source));
            robert.Add(new Death(Maybe.From("Book 1"), source));
            joffrey.Add(new Death(Maybe.From("Book 5"), source));

            return new[] {tyrion, jaime, cersei, tywin, joanna, robert, joffrey, tommen, myrcella};
        }

        private PersonFile CreatePerson(string rank, string firstName, string lastName, string nick = "") {
            var result = new PersonFile();
            var source = new Source();
            AddName(result, source, rank, firstName, lastName, nick);
            return result;
        }

        private void AddName(PersonFile result, Source source, string rank, string firstName, string lastName, string nick = "") {
            var personName = new PersonName(new[] {
                                                      new NameComponent(rank, NameType.Rank),
                                                      new NameComponent(firstName, NameType.Given),
                                                      new NameComponent(nick, NameType.Nick),
                                                      new NameComponent(lastName, NameType.Family)
                                                  });
            result.Add(new Name(personName, source));
        }

        public Relation FirstRelation {get { return FirstPerson.Information.OfType<Relation>().First(); }}
        public PersonFile FirstPerson {get { return this.First(); }}
    }
}