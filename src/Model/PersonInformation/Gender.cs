using Common;
using Common.Coding;

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

        public GenderType Sex { get { return _gender; }}

        public override Maybe<string> Value {
            get { return Sex.ToString(); }
        }
    }
}