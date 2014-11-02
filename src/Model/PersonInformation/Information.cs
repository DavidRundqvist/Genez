using System;
using Common;
using Common.Coding;

namespace Model.PersonInformation {
    [ValueObject]
    public abstract class Information //: IEquatable<Information> 
    {
        private readonly Reliability _reliability;
        private readonly Source _source;

        protected Information(Source source) : this(source, source.Reliability) {}

        protected Information(Source source, Reliability reliability) {
            _reliability = reliability;
            _source = source;
        }

        public Source AccordingTo {get { return _source; }}
        public Reliability Reliability {get { return _reliability; }}
        virtual public Type InformationType {get { return this.GetType(); }}
        public abstract Maybe<string> Value { get; }

        //public abstract bool Equals(Information other);

        public abstract T Accept<T>(IInformationVisitor<T> visitor);
    }


    /*
     * 
     * Birth: date, place
     * Death: date, place
     * Burial: date, place
     * Wedding: date, place, Person 1, Person 2
     * Divorce: date, Person 1, Person 2
     * Home: timespan, place,
     * Occupation: timespan, place     
     * Name: timespan
     * 
     * 
     * Mother: person
     * Father: person
     * Guardian: person
     * Appearance
     * Gender
     * 
     * 
     */

}