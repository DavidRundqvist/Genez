using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model;
using Model.PersonInformation;

namespace Infrastructure.Persistence.Information {
    [DataContract(Namespace = "")]
    public class NameDTO : InformationDTO {
        [DataMember]
        public PersonNameDTO Name;

        public static NameDTO From(Name name) {
            return new NameDTO(){Name = PersonNameDTO.From(name.TheName), Reliability = name.Reliability};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }


    [DataContract(Namespace = "")]
    public class PersonNameDTO {
        [DataMember]
        public List<NameComponentDTO> Components;

        public static PersonNameDTO From(PersonName name) {
            return new PersonNameDTO() {Components = name.Components.Select(NameComponentDTO.From).ToList()};
        }

        public static PersonName From(PersonNameDTO name) {
            return new PersonName(name.Components.Select(NameComponentDTO.From));
        }

    }
    
    [DataContract(Namespace = "")]
    public class NameComponentDTO {
        [DataMember]
        public string Text;

        [DataMember]
        public NameType Type;

        public static NameComponentDTO From(NameComponent name) {
            return new NameComponentDTO() {Text = name.Text, Type = name.Type};
        }
        public static NameComponent From(NameComponentDTO name) {
            return new NameComponent(name.Text, name.Type);
        }
    }
}