using Model.PersonInformation.Events;

namespace Infrastructure.Persistence.Information.Events {
    public class DeathDTO : EventDTO {
        public static DeathDTO From(Death info) {
            return new DeathDTO() {Date = info.Value.GetValueOrDefault(""), Reliability = info.Reliability};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}