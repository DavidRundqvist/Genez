using System;
using System.Xml.Serialization;
using Common.Coding;
using Model;

namespace Infrastructure.Persistence.Information.Relations {
    [XmlInclude(typeof (MotherDTO))]
    [XmlInclude(typeof (FatherDTO))]
    public abstract class RelationDTO : InformationDTO {
        public Guid Relative;
    }

}