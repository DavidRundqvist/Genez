using System;
using Model.PersonInformation;

namespace Infrastructure.Persistence.Information {
    public class PortraitDTO : InformationDTO {
        public string FileName;

        public static PortraitDTO From(Portrait info) {
            return new PortraitDTO() {FileName = info.ImageFile, Reliability = info.Reliability};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}