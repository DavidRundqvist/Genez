using System.Xml.Serialization;
using Common;
using Infrastructure.Persistence.Information.Events;
using Infrastructure.Persistence.Information.Relations;
using Model;

namespace Infrastructure.Persistence.Information {
    [XmlInclude(typeof (EventDTO))]
    [XmlInclude(typeof (GenderDTO))]
    [XmlInclude(typeof (NameDTO))]
    [XmlInclude(typeof (RelationDTO))]
    [XmlInclude(typeof (PortraitDTO))]
    public abstract class InformationDTO {
        public abstract T Accept<T>(IDTOVisitor<T> visitor);

        public Reliability Reliability {get; set; }
    }

    
}