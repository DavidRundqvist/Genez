using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Coding;
using Common.Enumerable;
using Model.PersonInformation;
using Model.PersonInformation.Events;


namespace Model {
    /// <summary>
    /// Person file, represents everything the genealogist knows about a person
    /// </summary>
    [Entity]
    public class PersonFile {

        /// <summary>
        /// The ID of the person
        /// </summary>
        private Guid _id;

        /// <summary>
        /// The information about a person
        /// </summary>
        private readonly ISet<Information> _information = new HashSet<Information>();


        /// <summary>
        /// Ctor
        /// </summary>
        public PersonFile(Guid id) {
            _id = id;
        }
        public PersonFile() : this(Guid.NewGuid()) {}

        #region Properties
        public IEnumerable<Information> Information {
            get { return _information; }
        }

        public IEnumerable<Information> Facts {
            get { return _information.Where(i => i.Reliability == Reliability.Reliable); }
        }

        public IEnumerable<Information> Guesses {
            get { return _information.Where(i => i.Reliability != Reliability.Reliable); }
        }
        #endregion

        #region Methods
        public void Add(Information information) {
            _information.Add(information);
        }

        public void Remove(Information information) {
            _information.Remove(information);            
        }

        /// <summary>
        /// Adds the information and removes all other information of the same type
        /// </summary>
        public void Set(Information information) {
            var conflictingInformation = _information.Where(i => i.InformationType == information.InformationType).ToList();
            conflictingInformation.ForEach(Remove);
            Add(information);
        }

        public void AddMother(PersonFile mother, Source source, Reliability reliability = Reliability.Reliable) {
            Add(new Mother(mother, source, reliability));
        }
        public void AddFather(PersonFile father, Source source, Reliability reliability = Reliability.Reliable) {
            Add(new Father(father, source, reliability));
        }
        #endregion

        #region Convenience
        public bool IsMale {
            get {
                return Facts
                    .OfType<Gender>()
                    .Any(c => c.Value == GenderType.Male);
            }
        }

        public bool IsFemale {
            get {
                return Facts
                    .OfType<Gender>()
                    .Any(c => c.Value == GenderType.Female);
            }
        }

        public IEnumerable<PersonName> ReliableNames {
            get { return Facts.OfType<Name>().Select(n => n.TheName); }
        }

        public Maybe<PersonName> ReliableName {
            get {
                return ReliableNames.Count() == 1 
                    ? Maybe.From(ReliableNames.First()) 
                    : Maybe.Empty;
            }
        }

        public override string ToString() {
            return ReliableName.Convert(name => name.ToString()).GetValueOrDefault("Unknown name");
        }

        public IEnumerable<PersonFile> GetAncestors(int numberOfGenerations) {
            if (numberOfGenerations < 1) {
                return Enumerable.Empty<PersonFile>();
            }

            IEnumerable<PersonFile> result = Mothers.Concat(Fathers).ToList();
            result = result.Concat(result.SelectMany(p => p.GetAncestors(numberOfGenerations - 1)));
            return result;
        }

        private IEnumerable<PersonFile> Mothers { get { return Information.OfType<Mother>().Select(m => m.Relative); }}
        private IEnumerable<PersonFile> Fathers { get { return Information.OfType<Father>().Select(m => m.Relative); }}

        public Maybe<string> BirthDate {
            get { return Facts.OfType<Birth>().FirstMaybe().Convert(b => b.Date); }
        }

        public Maybe<string> DeathDate {
            get { return Facts.OfType<Death>().FirstMaybe().Convert(b => b.Date); }
        }

        public bool IsDead { get { return Facts.OfType<Death>().Any(); } }

        #endregion
    }
}
