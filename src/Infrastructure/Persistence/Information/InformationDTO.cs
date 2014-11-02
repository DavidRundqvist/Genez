using System.Xml.Serialization;
using Common;
using Infrastructure.Persistence.Information.Events;
using Infrastructure.Persistence.Information.Relations;

namespace Infrastructure.Persistence.Information {
    [XmlInclude(typeof (EventDTO))]
    [XmlInclude(typeof (GenderDTO))]
    [XmlInclude(typeof (NameDTO))]
    [XmlInclude(typeof (RelationDTO))]
    public class InformationDTO {}
}