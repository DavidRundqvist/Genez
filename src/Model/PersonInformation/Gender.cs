using Model.Coding;

namespace Model.PersonInformation {
    public enum GenderType {
        Female,
        Male,
        Other
    }

    [ValueObject]
    public class Gender : Information {
        private readonly GenderType _gender;

        public Gender(GenderType gender, Source source) : this(gender, source, source.Reliability) {}
        public Gender(GenderType gender, Source source, Reliability reliability = Reliability.Reliable) : base(source, reliability) {
            _gender = gender;
        }

        public GenderType Value { get { return _gender; }}
    }
}