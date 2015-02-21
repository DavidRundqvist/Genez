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
    /// <summary>
    /// This is our custom data graph derived from BidirectionalGraph class using custom data types.
    /// Data graph stores vertices and edges data that is used by _graphArea and end-user for a variety of operations.
    /// Data graph content handled manually by user (add/remove objects). The main idea is that you can dynamicaly
    /// remove/add objects into the _graphArea layout and then use data graph to restore original layout content.
    /// </summary>
    public class AncestryGraphPresentation {
        private readonly AllPeople _allPeople;
        private readonly ShowInGraphPeople _showInGraphPeople;
        private readonly GraphControlsPresentation _graphControlsPresentation;
        public event EventHandler Changed;
        private readonly AncestryGraph _graph;

        public AncestryGraphPresentation(AllPeople allPeople, ShowInGraphPeople showInGraphPeople, GraphControlsPresentation graphControlsPresentation, AncestryGraph graph) {
            _allPeople = allPeople;
            _showInGraphPeople = showInGraphPeople;
            _graphControlsPresentation = graphControlsPresentation;
            _graph = graph;
            _showInGraphPeople.CollectionChanged += (sender, args) => UpdateGraph();
            AncestorGenerations.Value.PropertyChanged += (s, e) => UpdateGraph();
            ChildGenerations.Value.PropertyChanged += (s, e) => UpdateGraph();
        }

        public SliderPresentation AncestorGenerations { get { return _graphControlsPresentation.AncestorGenerations; }}
        public SliderPresentation ChildGenerations { get { return _graphControlsPresentation.ChildGenerations; }}
        public AncestryGraph Graph {get { return _graph; }}

        public AllPeople AllPeople {get { return _allPeople; }}

        private void UpdateGraph() {
            _graph.Clear();
            var ancestors = _showInGraphPeople.SelectMany(p => AllPeople.GetAncestor(p, AncestorGenerations.Value.Value));
            var children = _showInGraphPeople.SelectMany(p => AllPeople.GetChildren(p, ChildGenerations.Value.Value));

            var people = children.Concat(_showInGraphPeople).Concat(ancestors);
            _graph.Add(people);                
            OnChanged();
        }

        protected virtual void OnChanged() {
            var handler = Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }

    }}