using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Infrastructure.Persistence.Information;

namespace Infrastructure.Persistence {
    [DataContract(Namespace = "")]
    public class PersonDTO {
        [DataMember]
        public Guid Id;
        
        [DataMember]
        public List<InformationDTO> Information;
    }
}