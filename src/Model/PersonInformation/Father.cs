using Common.Coding;

namespace Model.PersonInformation {
    
    [ValueObject]
    public class Father : Relation {
        public Father(PersonFile relative, Source source, Reliability reliability = Reliability.Reliable) 
            : base(relative, source, reliability) {}
    }
}