using System;
using System.Xml.Serialization;
using Common.Coding;
using Model;

namespace Infrastructure.Persistence.Information.Relations {
    [XmlInclude(typeof (MotherDTO))]
    [XmlInclude(typeof (FatherDTO))]
    public class RelationDTO : InformationDTO {
        public Guid Relative;
    }

}