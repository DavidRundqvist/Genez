using System.Collections.Generic;
using System.Linq;
using Model.PersonInformation;

namespace Model.Constraints {
    public class ParentGenderConstraint : IConstraint {
        public ConstraintResult Examine(IEnumerable<Information> information) {
            var mothers = information.GetMothers();
            var fathers = information.GetFathers();

            if (mothers.Any(m => m.IsMale) || fathers.Any(f => f.IsFemale)) {
                return ConstraintResult.ErrorResult("Wrong gender of parent");
            }
            return ConstraintResult.OkResult();
        }
    }
}