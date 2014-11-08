using Common;
using Common.Coding;

namespace Model.PersonInformation.Events {

    [ValueObject]
    public abstract class Event : Information {
        private readonly Maybe<string> _date;

        protected Event(Maybe<string> date, Source source, Reliability reliability = Reliability.Reliable) : base(source, reliability) {
            _date = date;
        }

        public override Maybe<string> Value {
            get { return Date; }            
        }

        public Maybe<string> Date {
            get { return _date; }
        }
    }
}