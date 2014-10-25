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
    }
}