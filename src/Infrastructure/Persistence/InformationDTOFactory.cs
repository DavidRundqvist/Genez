using Infrastructure.Persistence.Information;
using Infrastructure.Persistence.Information.Events;
using Infrastructure.Persistence.Information.Relations;
using Model.PersonInformation;
using Model.PersonInformation.Events;
using Model.PersonInformation.Relations;

namespace Infrastructure.Persistence {
    public class InformationDTOFactory : IInformationVisitor<InformationDTO> {
        public InformationDTO Visit(Birth info) {
            return new BirthDTO();
        }

        public InformationDTO Visit(Death info) {
            return new DeathDTO();
        }

        public InformationDTO Visit(Name info) {
            return new NameDTO();
        }

        public InformationDTO Visit(Mother info) {
            return new MotherDTO();
        }

        public InformationDTO Visit(Father info) {
            return new FatherDTO();
        }

        public InformationDTO Visit(Gender info) {
            return new GenderDTO();
        }
    }
}