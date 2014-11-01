using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Common.WPF.Presentation;
using GraphX;
using Model;
using Model.PersonInformation;

namespace View.Global {
    public class RelationPresentation : EdgeBase<PersonPresentation> {
        private static readonly SolidColorBrush _maleColor = new SolidColorBrush(Colors.Blue);
        private static readonly SolidColorBrush _femaleColor = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush _neutralColor = new SolidColorBrush(Colors.Black);

        private readonly Relation _relation;
        private readonly Property<bool> _reliable;
        private readonly Property<SolidColorBrush> _relationColor;

        public RelationPresentation(PersonPresentation source, PersonPresentation target, Relation relation) : base(source, target, 1.0f) {
            _relation = relation;
            _reliable = new Property<bool>(relation.Reliability == Reliability.Reliable);
            _relationColor = new Property<SolidColorBrush>(GetColor());

        }

        private SolidColorBrush GetColor() {
            if (_relation is Father)
                return _maleColor;

            if (_relation is Mother)
                return _femaleColor;

            return _neutralColor;
        }

        public IProperty<bool> Reliable { get { return _reliable; } }
        public IProperty<SolidColorBrush> RelationColor { get { return _relationColor; }}
    }
}