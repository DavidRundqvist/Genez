using Common.Coding;

namespace Model.PersonInformation {
    [ValueObject]
    public class Mother : Relation {
        public Mother(PersonFile relative, Source source, Reliability reliability = Reliability.Reliable) 
            : base(relative, source, reliability) {}
    }
}