using Model.PersonInformation.Events;

namespace Infrastructure.Persistence.Information.Events {
    public class BirthDTO : EventDTO {
        public static BirthDTO From(Birth info) {
            return new BirthDTO() {Date = info.Value.GetValueOrDefault("")};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}