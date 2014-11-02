using System.Xml.Serialization;

namespace Infrastructure.Persistence.Information.Events {
    [XmlInclude(typeof(BirthDTO))]
    [XmlInclude(typeof(DeathDTO))]
    public class EventDTO : InformationDTO {    }
}