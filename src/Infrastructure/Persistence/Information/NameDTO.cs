using System.Collections.Generic;
using System.Linq;
using Model;
using Model.PersonInformation;

namespace Infrastructure.Persistence.Information {
    public class NameDTO : InformationDTO {
        public PersonNameDTO Name;

        public static NameDTO From(Name name) {
            return new NameDTO(){Name = PersonNameDTO.From(name.TheName)};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }



    public class PersonNameDTO {
        public List<NameComponentDTO> Components;

        public static PersonNameDTO From(PersonName name) {
            return new PersonNameDTO() {Components = name.Components.Select(NameComponentDTO.From).ToList()};
        }

        public static PersonName From(PersonNameDTO name) {
            return new PersonName(name.Components.Select(NameComponentDTO.From));
        }

    }

    public class NameComponentDTO {
        public string Text;
        public NameType Type;

        public static NameComponentDTO From(NameComponent name) {
            return new NameComponentDTO() {Text = name.Text, Type = name.Type};
        }
        public static NameComponent From(NameComponentDTO name) {
            return new NameComponent(name.Text, name.Type);
        }
    }
}