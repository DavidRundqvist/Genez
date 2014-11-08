using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enumerable;
using Common.WPF.Presentation;
using Model;

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
                    .Convert(m => new NamePresentation(m).ToString())
                    .GetValueOrDefault("Unknown");
            }
        }

        public string CompleteName {
            get {
                if (!_person.Names.Any())
                    return "Unknown";

                return _person.Names
                    .Select(n => new NamePresentation(n))
                    .Select(n => n.ToString())
                    .Join(Environment.NewLine);
            }
        }
    }
}