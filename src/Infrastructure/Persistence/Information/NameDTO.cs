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
    }



    public class PersonNameDTO {
        public List<NameComponentDTO> Components;

        public static PersonNameDTO From(PersonName name) {
            return new PersonNameDTO() {Components = name.Components.Select(NameComponentDTO.From).ToList()};
        }

    }

    public class NameComponentDTO {
        public string Text;
        public NameType Type;

        public static NameComponentDTO From(NameComponent name) {
            return new NameComponentDTO() {Text = name.Text, Type = name.Type};
        }
    }
}