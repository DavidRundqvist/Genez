using Common;
using Common.Coding;

namespace Model.PersonInformation.Relations {

    [ValueObject]
    public abstract class Relation : Information {
        private readonly PersonFile _relative;

        protected Relation(PersonFile relative, Source source, Reliability reliability) : base(source, reliability) {
            _relative = relative;
        }


        public PersonFile Relative {
            get { return _relative; }
        }

        public override Maybe<string> Value {
            get { return Maybe.From(_relative.ToString()); }
        }
    }
}