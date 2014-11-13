using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using Common;
using Common.Coding;
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

            // some information taken from: http://awoiaf.westeros.org/
            var source = new Source();

            var tyrion = CreatePerson(new Guid("492432CA-3D43-40B2-BA2E-60D01A5E24A1"), "", "Tyrion", "Lannister", "the Imp");            
            var jaime = CreatePerson(new Guid("492432CA-3D43-40B2-BA2E-60D01A5E24A2"), "Ser", "Jaime", "Lannister", nick: "the Kingslayer");            
            var cersei = CreatePerson(new Guid("492432CA-3D43-40B2-BA2E-60D01A5E24A3"), "Queen", "Cersei", "Lannister");            
            var tywin = CreatePerson(new Guid("492432CA-3D43-40B2-BA2E-60D01A5E24A4"), "Lord", "Tywin", "Lannister");            
            var joanna = CreatePerson(new Guid("492432CA-3D43-40B2-BA2E-60D01A5E24A5"), "Lady", "Joanna", "Lannister");
            var robert = CreatePerson(new Guid("492432CA-3D43-40B2-BA2E-60D01A5E24A6"), "King", "Robert", "Baratheon");
            var joffrey = CreatePerson(new Guid("492432CA-3D43-40B2-BA2E-60D01A5E24A7"), "Prince", "Joffrey", "Baratheon");
            var tommen = CreatePerson(new Guid("492432CA-3D43-40B2-BA2E-60D01A5E24A8"), "Prince", "Tommen", "Baratheon");
            var myrcella = CreatePerson(new Guid("492432CA-3D43-40B2-BA2E-60D01A5E24A9"), "Princess", "Myrcella", "Baratheon");

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

            tyrion.Add(new Birth(Maybe.From("272AC"), source, Reliability.Unreliable));
            tyrion.Add(new Birth(Maybe.From("273AC"), source, Reliability.Unreliable));
            tywin.Add(new Birth(Maybe.From("242AC"), source));
            tywin.Add(new Death(Maybe.From("300AC"), source));
            joanna.Add(new Death(Maybe.From("274AC"), source, Reliability.Unreliable));
            robert.Add(new Death(Maybe.From("298AC"), source));            
            joffrey.Add(new Birth(Maybe.From("286AC"), source));
            joffrey.Add(new Death(Maybe.From("300AC"), source));
            jaime.Add(new Birth("266AC", source));
            cersei.Add(new Birth("266AC", source));
            myrcella.Add(new Birth("290AC", source));
            tommen.Add(new Birth("291AC", source));

            tyrion.AddPortrait(new FileName(@"Images\tyrion.jpg"), source);
            cersei.AddPortrait(new FileName(@"Images\Cersei.jpg"), source);
            tywin.AddPortrait(new FileName(@"Images\Tywin.jpg"), source);
            robert.AddPortrait(new FileName(@"Images\robert.jpg"), source);
            jaime.AddPortrait(new FileName(@"Images\jaime.jpg"), source);
            joanna.AddPortrait(new FileName(@"Images\joanna.png"), source);
            joffrey.AddPortrait(new FileName(@"Images\joffrey.jpg"), source);
            myrcella.AddPortrait(new FileName(@"Images\myrcella.jpg"), source);
            tommen.AddPortrait(new FileName(@"Images\tommen.jpg"), source);

            return new[] {tyrion, jaime, cersei, tywin, joanna, robert, joffrey, tommen, myrcella};
        }

        private PersonFile CreatePerson(Guid personId, string rank, string firstName, string lastName, string nick = "") {
            var result = new PersonFile(new Id<PersonFile>(personId));
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