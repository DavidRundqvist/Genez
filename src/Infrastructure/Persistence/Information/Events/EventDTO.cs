using System.Xml.Serialization;
using Common;

namespace Infrastructure.Persistence.Information.Events {
    [XmlInclude(typeof (BirthDTO))]
    [XmlInclude(typeof (DeathDTO))]
    public class EventDTO : InformationDTO {
        public string Date;
    }    
}