using Common;
using Common.Coding;

namespace Model.PersonInformation.Events {
    [ValueObject]
    public class Birth : Event {
        public Birth(Maybe<string> date, Source source, Reliability reliability = Reliability.Reliable) : base(date, source, reliability) {}
    }
}