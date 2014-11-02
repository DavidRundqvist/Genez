using Model.PersonInformation;

namespace Infrastructure.Persistence.Information {
    public class GenderDTO : InformationDTO {
        public GenderType Sex;

        public static GenderDTO From(Gender info) {
            return new GenderDTO() {Sex = info.Sex};
        }
    }
}