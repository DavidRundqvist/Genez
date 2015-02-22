using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enumerable;
using Common.WPF.Presentation;
using Model;
using Model.PersonInformation;
using QuickGraph;
using View.Global;
using View.PersonList;

namespace View.AncestryGraph {

    public class AncestryGraphPresentation {
        private readonly AllPeople _allPeople;
        private readonly GraphPeople _graphPeople;
        private readonly GraphControlsPresentation _graphControlsPresentation;
        public event EventHandler Changed;
        private readonly AncestryGraph _graph;

        public AncestryGraphPresentation(AllPeople allPeople, GraphPeople graphPeople, GraphControlsPresentation graphControlsPresentation, AncestryGraph graph) {
            _allPeople = allPeople;
            _graphPeople = graphPeople;
            _graphControlsPresentation = graphControlsPresentation;
            _graph = graph;
            _graphPeople.CollectionChanged += (sender, args) => UpdateGraph();
            _graphPeople.RelationsChanged += (semder, args) => UpdateGraph();
            AncestorGenerations.Value.PropertyChanged += (s, e) => UpdateGraph();
            ChildGenerations.Value.PropertyChanged += (s, e) => UpdateGraph();
        }

        public SliderPresentation AncestorGenerations { get { return _graphControlsPresentation.AncestorGenerations; }}
        public SliderPresentation ChildGenerations { get { return _graphControlsPresentation.ChildGenerations; }}
        public AncestryGraph Graph {get { return _graph; }}

        public AllPeople AllPeople {get { return _allPeople; }}

        private void UpdateGraph() {
            _graph.Clear();
            var ancestors = _graphPeople.SelectMany(p => AllPeople.GetAncestor(p, AncestorGenerations.Value.Value));
            var children = _graphPeople.SelectMany(p => AllPeople.GetChildren(p, ChildGenerations.Value.Value));

            var people = children.Concat(_graphPeople).Concat(ancestors);
            _graph.Add(people);                
            OnChanged();
        }

        protected virtual void OnChanged() {
            var handler = Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }

    }}