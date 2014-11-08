using Model.PersonInformation.Relations;

namespace Infrastructure.Persistence.Information.Relations {
    public class FatherDTO : RelationDTO {
        public static FatherDTO From(Father info) {
            return new FatherDTO() {Relative = info.Relative.Id.Guid};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}