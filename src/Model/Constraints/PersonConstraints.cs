using System.Collections.Generic;
using System.Linq;
using Model.PersonInformation;

namespace Model.Constraints {
    public class PersonConstraints {
        private readonly PersonFile _person;
        private readonly List<IConstraint> _constraints;

        public PersonConstraints(PersonFile person) {
            _person = person;
            _constraints = new List<IConstraint> {
                                                     new FatherConstraint(), 
                                                     new MotherConstraint(),
                                                     new ParentGenderConstraint()
                                                 };
        }


        public IEnumerable<string> Errors {
            get { return FindErrors(_person.Information); }

        }

        private IEnumerable<string> FindErrors(IEnumerable<Information> information) {
            return _constraints
                .Select(c => c.Examine(information))
                .Where(r => !r.IsOk)
                .Select(r => r.Error)
                .ToList();
            
        }


        public bool IsOk { get { return !Errors.Any(); }}

        public bool CanAdd(params Information[] information) {
            var potentialSet = _person.Information.Concat(information);
            var errors = FindErrors(potentialSet);
            return !errors.Any();
        }

        public bool CanRemove(params Information[] information) {
            var potentialSet = _person.Information.Except(information);
            var errors = FindErrors(potentialSet);
            return !errors.Any();
        }

    }
}