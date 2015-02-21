using System;
using System.Runtime.Serialization;
using Model.PersonInformation;

namespace Infrastructure.Persistence.Information {
    [DataContract(Namespace = "")]
    public class PortraitDTO : InformationDTO {
        [DataMember]
        public string FileName;

        public static PortraitDTO From(Portrait info) {
            return new PortraitDTO() {FileName = info.ImageFile, Reliability = info.Reliability};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}