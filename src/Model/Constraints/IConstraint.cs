using System.Collections.Generic;
using Model.PersonInformation;

namespace Model.Constraints {
    public interface IConstraint {
        ConstraintResult Examine(IEnumerable<Information> information);
    }
}