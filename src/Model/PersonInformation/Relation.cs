using Common.Coding;

namespace Model.PersonInformation {

    [ValueObject]
    public class Relation : Information {
        private readonly PersonFile _relative;

        public Relation(PersonFile relative, Source source, Reliability reliability) : base(source, reliability) {
            _relative = relative;
        }


        public PersonFile Relative {
            get { return _relative; }
        }
    }
}