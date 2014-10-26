using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.PersonInformation;
using QuickGraph;
using View.PersonList;

namespace View.AncestryGraph {
    /// <summary>
    /// This is our custom data graph derived from BidirectionalGraph class using custom data types.
    /// Data graph stores vertices and edges data that is used by _graphArea and end-user for a variety of operations.
    /// Data graph content handled manually by user (add/remove objects). The main idea is that you can dynamicaly
    /// remove/add objects into the _graphArea layout and then use data graph to restore original layout content.
    /// </summary>
    public class AncestryGraphPresentation : BidirectionalGraph<PersonVertex, RelationEdge> {
        private readonly PersonRegistry _personRegistry;
        public event EventHandler Changed;

        protected virtual void OnChanged() {
            var handler = Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public AncestryGraphPresentation(PersonRegistry personRegistry) {
            _personRegistry = personRegistry;
            _personRegistry.CollectionChanged += _personRegistry_CollectionChanged;
        }

        void _personRegistry_CollectionChanged(object sender, Common.Enumerable.CollectionEventArgs<PersonFile> args) {

            //var people = args.Added;
            //var people = new DesignPersonRegistry();            

            var proband = args.Added.First(p => p.ToString().Contains("David Rundqvist"));
            var people = new[]{proband}.Concat(proband.Ancestors.ToList());

            var personVertices = new Dictionary<PersonFile, PersonVertex>();
            
            foreach (var person in people) {
                var vertex = new PersonVertex(person);
                this.AddVertex(vertex);
                personVertices[person] = vertex;
            }
            foreach (var person in people) {
                var personVertex = personVertices[person];                
                var relatives = person.Information.OfType<Relation>();
                foreach (var relative in relatives) {
                    if (relative.Relative != person) {
                        var relativeVertex = personVertices[relative.Relative];
                        this.AddEdge(new RelationEdge(personVertex, relativeVertex));
                    }
                }

            }

            




            OnChanged();
        }
    }}