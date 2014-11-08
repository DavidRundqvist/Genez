using Common;
using Model;

namespace View.Global {
    public class DesignRelationPresentation : RelationPresentation {
        public DesignRelationPresentation() : base(
            new PersonPresentation(DesignPersonRegistry.TheOne.FirstPerson, Maybe.Empty),
            new PersonPresentation(DesignPersonRegistry.TheOne.FirstRelation.Relative, Maybe.Empty),
            DesignPersonRegistry.TheOne.FirstRelation) {}
    }
}