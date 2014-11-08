using Model;

namespace View.Global {
    public class DesignRelationPresentation : RelationPresentation {
        public DesignRelationPresentation() : base(
            new PersonPresentation(DesignPersonRegistry.TheOne.FirstPerson),
            new PersonPresentation(DesignPersonRegistry.TheOne.FirstRelation.Relative),
            DesignPersonRegistry.TheOne.FirstRelation) {}
    }
}