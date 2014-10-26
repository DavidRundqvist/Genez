using GraphX;
using Model;

namespace View.AncestryGraph {
    public class PersonVertex : VertexBase{
        private readonly PersonFile _person;

        public PersonVertex(PersonFile person) {
            _person = person;
        }

        public string Name { get { return _person.ToString(); } }
    }
}