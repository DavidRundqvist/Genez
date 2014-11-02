using Model.PersonInformation.Events;

namespace Infrastructure.Persistence.Information.Events {
    public class BirthDTO : EventDTO {
        public static BirthDTO From(Birth info) {
            return new BirthDTO() {Date = info.Value.GetValueOrDefault("")};
        }
    }
}