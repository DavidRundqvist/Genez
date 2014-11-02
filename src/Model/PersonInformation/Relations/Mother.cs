using Common.Coding;

namespace Model.PersonInformation.Relations {
    [ValueObject]
    public class Mother : Relation {
        public Mother(PersonFile relative, Source source, Reliability reliability = Reliability.Reliable) 
            : base(relative, source, reliability) {}

        public override T Accept<T>(IInformationVisitor<T> visitor) {return visitor.Visit(this);}
    }
}