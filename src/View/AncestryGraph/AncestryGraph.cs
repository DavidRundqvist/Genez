using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Enumerable;
using Model.PersonInformation;
using Model.PersonInformation.Relations;
using QuickGraph;
using View.Global;

namespace View.AncestryGraph {
    public class AncestryGraph : BidirectionalGraph<PersonPresentation, RelationPresentation> {
        public void Add(IEnumerable<PersonPresentation> people) {
            people = people.ToList();
            foreach (var person in people) {
                AddVertex(person);
            }
            foreach (var person in people) {
                var relatives = person.Person.Information.OfType<Relation>();
                foreach (var relation in relatives) {
                    var relativeLocal = relation;
                    var relativePres = people.FirstMaybe(p => p.Person == relativeLocal.Relative); // todo: optimize?
                    if (relativePres.HasValue) {
                        AddEdge(new RelationPresentation(person, relativePres.Value, relation));
                    }
                }
            }            
        }
    }
}