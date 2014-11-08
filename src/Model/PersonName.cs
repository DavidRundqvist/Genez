using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common;
using Common.Coding;
using Common.Enumerable;

namespace Model {

    [ValueObject]
    public class PersonName {
        private readonly ReadOnlyCollection<NameComponent> _components;
        public PersonName(IEnumerable<NameComponent> components) {
            _components = components.Where(s => !string.IsNullOrEmpty(s.Text)).ToList().AsReadOnly();
        }

        public IOrderedEnumerable<NameComponent> Components {
            get { return _components.OrderBy(c => c.Type); }
        }

        public override string ToString() {
            return Components.Select(c => c.Text).ToArray().Join();
        }

        public Maybe<string> Rank {get { return GetName(NameType.Rank); }}
        public Maybe<string> Given {get { return GetName(NameType.Given); }}
        public Maybe<string> Family {get { return GetName(NameType.Family); }}
        public Maybe<string> Nick {get { return GetName(NameType.Nick); }}
        public Maybe<string> Title {get { return GetName(NameType.Title); }}
        

        public Maybe<string> GetName(NameType nameType) {return _components.FirstMaybe(c => c.Type == nameType).Convert(c => c.Text);}

    }

    [ValueObject]
    public class NameComponent {
        public readonly string Text;
        public readonly NameType Type;
        public NameComponent(string text, NameType type) {
            Text = text;
            Type = type;
        }

        public override string ToString() {
            return string.Format("{0}: {1}", Type, Text);
        }
    }

    public enum NameType {
        Rank,   // Ser
        Given,   // Jaime
        Family,  // Lannister
        Nick, // The Kingslayer
        Title, // Heir to Casterly Rock        
    }

}