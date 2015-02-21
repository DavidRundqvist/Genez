using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.Coding;
using Model;

namespace Infrastructure.Persistence.Information.Relations {
    [KnownType(typeof (MotherDTO))]
    [KnownType(typeof (FatherDTO))]
    [DataContract(Namespace = "")]
    public abstract class RelationDTO : InformationDTO {
        [DataMember]
        public Guid Relative;
    }

}