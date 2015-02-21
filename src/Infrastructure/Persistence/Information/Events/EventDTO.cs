using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common;
using Model;

namespace Infrastructure.Persistence.Information.Events {
    [KnownType(typeof (BirthDTO))]
    [KnownType(typeof (DeathDTO))]
    [DataContract(Namespace = "")]
    public abstract class EventDTO : InformationDTO {
        [DataMember]
        public string Date;
    }    
}