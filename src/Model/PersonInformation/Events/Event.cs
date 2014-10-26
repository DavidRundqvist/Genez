using Common;
using Common.Coding;

namespace Model.PersonInformation.Events {

    [ValueObject]
    public abstract class Event : Information {
        protected Event(Maybe<string> date, Source source, Reliability reliability = Reliability.Reliable) : base(source, reliability) {
            Date = date;
        }

        public Maybe<string> Date { get; private set; }
    }
}