using System.Runtime.Serialization;
using Model.PersonInformation.Relations;

namespace Infrastructure.Persistence.Information.Relations {
    [DataContract(Namespace = "")]
    public class FatherDTO : RelationDTO
    {
        public static FatherDTO From(Father info) {
            return new FatherDTO() {Relative = info.Relative.Id.Guid, Reliability = info.Reliability};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}