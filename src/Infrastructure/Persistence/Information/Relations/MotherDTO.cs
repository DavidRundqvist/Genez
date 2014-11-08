using Model.PersonInformation.Relations;

namespace Infrastructure.Persistence.Information.Relations {
    public class MotherDTO : RelationDTO {
        public static MotherDTO From(Mother info) {
            return new MotherDTO() {Relative = info.Relative.Id.Guid};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}