using Infrastructure.Persistence.Information;
using Infrastructure.Persistence.Information.Events;
using Infrastructure.Persistence.Information.Relations;
using Model.PersonInformation;
using Model.PersonInformation.Events;
using Model.PersonInformation.Relations;

namespace Infrastructure.Persistence {
    public class InformationDTOFactory : IInformationVisitor<InformationDTO> {
        public InformationDTO Visit(Birth info) {
            return BirthDTO.From(info);
        }

        public InformationDTO Visit(Death info) {
            return DeathDTO.From(info);
        }

        public InformationDTO Visit(Name info) {
            return NameDTO.From(info);
        }

        public InformationDTO Visit(Mother info) {
            return MotherDTO.From(info);
        }

        public InformationDTO Visit(Father info) {
            return FatherDTO.From(info);
        }

        public InformationDTO Visit(Gender info) {
            return GenderDTO.From(info);
        }

        public InformationDTO Visit(Portrait info) {
            return PortraitDTO.From(info);
        }
    }
}