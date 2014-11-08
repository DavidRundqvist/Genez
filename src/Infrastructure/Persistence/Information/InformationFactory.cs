using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Coding;
using Infrastructure.Persistence.Information.Events;
using Infrastructure.Persistence.Information.Relations;
using Model;
using Model.PersonInformation;
using Model.PersonInformation.Events;
using Model.PersonInformation.Relations;

namespace Infrastructure.Persistence.Information {
    public class InformationFactory : IDTOVisitor<Model.PersonInformation.Information> {
        readonly List<PersonFile> _people = new List<PersonFile>();


        public void Add(params PersonFile[] people) {
            _people.AddRange(people);
        }

        public void Clear() {
            _people.Clear();            
        }
            
        PersonFile Find(Id<PersonFile> id) {
            return _people.First(p => p.Id == id);
        }

        PersonFile Find(Guid personId) {
            return Find(new Id<PersonFile>(personId));
        }

        public Model.PersonInformation.Information Visit(MotherDTO dto) {
            return new Mother(Find(dto.Relative), new Source());
        }

        public Model.PersonInformation.Information Visit(GenderDTO dto) {
            return new Gender(dto.Sex, new Source());
        }

        public Model.PersonInformation.Information Visit(FatherDTO dto) {
            return new Father(Find(dto.Relative), new Source());
        }

        public Model.PersonInformation.Information Visit(DeathDTO dto) {
            return new Death(GetDate(dto), new Source());
        }

        private Maybe<string> GetDate(EventDTO dto) {
            return string.IsNullOrEmpty(dto.Date)
                ? Maybe.Empty
                : Maybe.From(dto.Date);
        }

        public Model.PersonInformation.Information Visit(BirthDTO dto) {
            return new Birth(GetDate(dto), new Source());
        }

        public Model.PersonInformation.Information Visit(NameDTO dto) {
            return new Name(PersonNameDTO.From(dto.Name), new Source());
        }
    }
}