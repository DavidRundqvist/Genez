using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Enumerable;
using Model.PersonInformation;

namespace Model {
    public class PersonRegistry : EventCollection<PersonFile> {
        public IEnumerable<PersonFile> GetChildren(PersonFile person, int generations) {
            if (generations < 1)
                return Enumerable.Empty<PersonFile>();
                        
            var firstGeneration = this.Where(p => p.GetParents().Contains(person)).ToList();
            var subGenerations = firstGeneration.SelectMany(child => GetChildren(child, generations - 1)).ToList();

            var result = firstGeneration.Concat(subGenerations);
            return result;
        }
    }
}