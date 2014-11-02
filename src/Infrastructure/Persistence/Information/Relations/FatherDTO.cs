using Model.PersonInformation.Relations;

namespace Infrastructure.Persistence.Information.Relations {
    public class FatherDTO : RelationDTO {
        public static FatherDTO From(Father info) {
            return new FatherDTO() {Relative = info.Relative.Id.Guid};
        }
    }
}