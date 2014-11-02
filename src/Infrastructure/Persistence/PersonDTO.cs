using System;
using System.Collections.Generic;
using Infrastructure.Persistence.Information;

namespace Infrastructure.Persistence {
    public class PersonDTO {
        public Guid Id;
        public List<InformationDTO> Information;
    }
}