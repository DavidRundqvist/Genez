using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common;
using Infrastructure.Persistence.Information.Events;
using Infrastructure.Persistence.Information.Relations;
using Model;

namespace Infrastructure.Persistence.Information {
    [KnownType(typeof (EventDTO))]
    [KnownType(typeof (GenderDTO))]
    [KnownType(typeof (NameDTO))]
    [KnownType(typeof (RelationDTO))]
    [KnownType(typeof (PortraitDTO))]
    [DataContract(Namespace = "")]
    public abstract class InformationDTO {
        public abstract T Accept<T>(IDTOVisitor<T> visitor);

        [DataMember]
        public Reliability Reliability {get; set; }
    }

    
}