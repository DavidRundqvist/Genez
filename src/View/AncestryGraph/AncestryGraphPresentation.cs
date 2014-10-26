using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.PersonInformation;
using QuickGraph;
using View.Global;
using View.PersonList;

namespace View.AncestryGraph {
    /// <summary>
    /// This is our custom data graph derived from BidirectionalGraph class using custom data types.
    /// Data graph stores vertices and edges data that is used by _graphArea and end-user for a variety of operations.
    /// Data graph content handled manually by user (add/remove objects). The main idea is that you can dynamicaly
    /// remove/add objects into the _graphArea layout and then use data graph to restore original layout content.
    /// </summary>
    public class AncestryGraphPresentation : BidirectionalGraph<PersonVertex, RelationEdge> {
        private readonly SelectedPeople _selectedPeople;
        private readonly GraphControlsPresentation _graphControlsPresentation;
        public event EventHandler Changed;


        public AncestryGraphPresentation(SelectedPeople selectedPeople, GraphControlsPresentation graphControlsPresentation) {
            _selectedPeople = selectedPeople;
            _graphControlsPresentation = graphControlsPresentation;
            _selectedPeople.CollectionChanged += (sender, args) => UpdateGraph();
            AncestorGenerations.Value.PropertyChanged += (s, e) => UpdateGraph();
            ChildGenerations.Value.PropertyChanged += (s, e) => UpdateGraph();

        }

        public SliderPresentation AncestorGenerations { get { return _graphControlsPresentation.AncestorGenerations; }}
        public SliderPresentation ChildGenerations { get { return _graphControlsPresentation.ChildGenerations; }}

        private void UpdateGraph() {
            var personFiles = _selectedPeople
                .Select(p => p.Person)
                .ToList();
            var people = personFiles
                .Concat(personFiles.SelectMany(file => file.GetAncestors(AncestorGenerations.Value.Value)))
                .Distinct()
                .ToList();

            var personVertices = new Dictionary<PersonFile, PersonVertex>();
            this.Clear();

            foreach (var person in people) {
                var vertex = new PersonVertex(person, _selectedPeople.Any(p => p.Person == person));
                this.AddVertex(vertex);
                personVertices[person] = vertex;
            }
            foreach (var person in people) {
                var personVertex = personVertices[person];
                var relatives = person.Information.OfType<Relation>();
                foreach (var relative in relatives) {
                    if (relative.Relative != person) {
                        if (personVertices.ContainsKey(relative.Relative)) {
                            var relativeVertex = personVertices[relative.Relative];
                            this.AddEdge(new RelationEdge(personVertex, relativeVertex));
                        }
                    }
                }
            }
            OnChanged();
        }

        protected virtual void OnChanged() {
            var handler = Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }

    }}