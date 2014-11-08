using Common;
using Common.Coding;

namespace Model.PersonInformation
{
    [ValueObject]
    public class Name : Information
    {
        private readonly PersonName _name;

        public Name(PersonName name, Source source) : this(name, source, source.Reliability) {}
        public Name(PersonName name, Source source, Reliability reliability = Reliability.Reliable)
            : base(source, reliability) {
            _name = name;
        }

        public PersonName TheName {
            get { return _name; }
        }

        public override Maybe<string> Value {
            get { return _name.ToString(); }
        }

        public override T Accept<T>(IInformationVisitor<T> visitor) {
            return visitor.Visit(this);
        }

        public override string ToString() {
            return TheName.ToString();
        }
    }
}