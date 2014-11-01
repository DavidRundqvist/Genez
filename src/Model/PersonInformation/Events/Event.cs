using Common;
using Common.Coding;

namespace Model.PersonInformation.Events {

    [ValueObject]
    public abstract class Event : Information {
        private readonly Maybe<string> _value;

        protected Event(Maybe<string> date, Source source, Reliability reliability = Reliability.Reliable) : base(source, reliability) {
            _value = date;
        }

        public override Maybe<string> Value {
            get { return _value; }            
        }
    }
}