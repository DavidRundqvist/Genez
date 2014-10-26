using System;
using Model;
using QuickGraph;
using View.PersonList;

namespace View.AncestryGraph {
    /// <summary>
    /// This is our custom data graph derived from BidirectionalGraph class using custom data types.
    /// Data graph stores vertices and edges data that is used by GraphArea and end-user for a variety of operations.
    /// Data graph content handled manually by user (add/remove objects). The main idea is that you can dynamicaly
    /// remove/add objects into the GraphArea layout and then use data graph to restore original layout content.
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
            foreach (var person in args.Added) {
                this.AddVertex(new PersonVertex(person));
            }
            OnChanged();
        }
    }}