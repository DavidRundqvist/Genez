using System.Runtime.Serialization;
using Model.PersonInformation.Relations;

namespace Infrastructure.Persistence.Information.Relations {
    [DataContract(Namespace = "")]
    public class MotherDTO : RelationDTO {
        public static MotherDTO From(Mother info) {
            return new MotherDTO() { Relative = info.Relative.Id.Guid, Reliability = info.Reliability };
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}