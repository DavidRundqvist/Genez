using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Enumerable;
using Common.WPF.Presentation;
using Model;
using Model.PersonInformation;

namespace View.Global {
    public class PersonNamePresentation {
        private readonly PersonFile _person;

        public PersonNamePresentation(PersonFile person) {
            _person = person;
        }

        public override string ToString() {
            return CompleteName;
        }

        public string MainName {
            get {
                var maybe = _person.MainName;                
                return maybe
                    .Convert(GetNameString)
                    .GetValueOrDefault("Unknown");
            }
        }

        private static string GetNameString(Name name) {
            return name.TheName + (name.IsReliable ? "" : " (maybe)");
        }

        public string CompleteName {
            get {
                if (!_person.Names.Any())
                    return "Unknown";

                return _person.Names
                    .Select(GetNameString)
                    .Join(Environment.NewLine);
            }
        }

        public string FirstName {
            get {
                return _person.MainName.Convert(name => name.TheName.Given).GetValueOrDefault("Unknown");
            }
        }

        public string LastName {
            get {
                return _person.MainName.Convert(name => name.TheName.Family).GetValueOrDefault("Unknown");
            }
        }
    }
}