using System.Collections.Generic;
using System.Linq;
using Model.PersonInformation;

namespace Model.Constraints {
    public class SingularRelativeConstraint<T>  : IConstraint where T : Relation {
        public ConstraintResult Examine(IEnumerable<Information> information) {
            var relations = information.OfType<T>().ToList();

            if (relations.Select(m => m.Relative).Distinct().Count() > 1) {
                if (relations.Any(m => m.Reliability == Reliability.Reliable)) {
                    return ConstraintResult.ErrorResult("Can only have one certain relative of type " + typeof(T).Name);
                }
            }

            return ConstraintResult.OkResult();
        }
    }
}