using System.Xml.Serialization;
using Common;
using Model;

namespace Infrastructure.Persistence.Information.Events {
    [XmlInclude(typeof (BirthDTO))]
    [XmlInclude(typeof (DeathDTO))]
    public abstract class EventDTO : InformationDTO {
        public string Date;

    }    
}