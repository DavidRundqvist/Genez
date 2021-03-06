﻿using System.Runtime.Serialization;
using Model.PersonInformation.Events;

namespace Infrastructure.Persistence.Information.Events {
    [DataContract(Namespace = "")]
    public class BirthDTO : EventDTO {
        public static BirthDTO From(Birth info) {
            return new BirthDTO() {Date = info.Value.GetValueOrDefault(""), Reliability = info.Reliability};
        }

        public override T Accept<T>(IDTOVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}