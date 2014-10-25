﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using Model.Coding;
using Model.Coding.Exceptions;
using Model.Constraints;
using Model.PersonInformation;


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


        #endregion
    }
}