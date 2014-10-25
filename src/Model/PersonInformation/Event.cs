using Common.Coding;

namespace Model.PersonInformation {

    [ValueObject]
    public abstract class Event : Information {
        protected Event(Source source) : this(source, source.Reliability) {}
        protected Event(Source source, Reliability reliability) : base(source, reliability) {}
    }
}