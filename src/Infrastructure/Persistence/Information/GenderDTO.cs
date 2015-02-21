using System.Runtime.Serialization;
using Model.PersonInformation;

namespace Infrastructure.Persistence.Information {
    [DataContract(Namespace = "")]
    public class GenderDTO : InformationDTO {
        [DataMember]
        public GenderType Sex;

        public static GenderDTO From(Gender info) {
            return new GenderDTO() {Sex = info.Sex, Reliability = info.Reliability};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}