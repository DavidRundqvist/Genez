using Model.PersonInformation;

namespace Infrastructure.Persistence.Information {
    public class GenderDTO : InformationDTO {
        public GenderType Sex;

        public static GenderDTO From(Gender info) {
            return new GenderDTO() {Sex = info.Sex, Reliability = info.Reliability};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}