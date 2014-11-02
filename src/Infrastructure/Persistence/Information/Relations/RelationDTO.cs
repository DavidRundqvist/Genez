using System.Xml.Serialization;

namespace Infrastructure.Persistence.Information.Relations {
    [XmlInclude(typeof(MotherDTO))]
    [XmlInclude(typeof(FatherDTO))]
    public class RelationDTO : InformationDTO {}
}