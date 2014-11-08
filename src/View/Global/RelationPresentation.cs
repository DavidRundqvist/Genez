using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Converters;
using Common.WPF.Presentation;
using GraphX;
using Model;
using Model.PersonInformation;
using Model.PersonInformation.Relations;

namespace View.Global {
    public class RelationPresentation : EdgeBase<PersonPresentation> {
        private static readonly SolidColorBrush _maleColor = new SolidColorBrush(Colors.Blue);
        private static readonly SolidColorBrush _femaleColor = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush _neutralColor = new SolidColorBrush(Colors.Black);

        private readonly Relation _relation;
        private readonly Property<bool> _reliable;
        private readonly Property<SolidColorBrush> _relationColor;
        private readonly Property<string> _description;

        public RelationPresentation(PersonPresentation source, PersonPresentation target, Relation relation) : base(source, target, 1.0f) {
            _relation = relation;
            _reliable = new Property<bool>(relation.Reliability == Reliability.Reliable);
            _relationColor = new Property<SolidColorBrush>(GetColor());
            _description = new Property<string>(GetDescription(source, target, relation));
        }

        private string GetDescription(PersonPresentation source, PersonPresentation target, Relation relation) {
            var relationType = GetRelationType(relation);
            var possibility = _reliable.Value ? "" : "(maybe) ";
            
            var result = string.Format("{0} is {3}the {1} of {2}", 
                target.Name.Value.MainName, 
                relationType,
                source.Name.Value.MainName, 
                possibility);           
            return result;
        }

        private string GetRelationType(Relation relation) {
            // todo: replace with visitor?
            if (relation is Mother) return "mother";
            if (relation is Father) return "father";
            return "relative";
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
        public IProperty<string> Description {get { return _description; }}
    }
}