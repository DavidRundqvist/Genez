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
            _components = components.ToList().AsReadOnly();
        }

        public ReadOnlyCollection<NameComponent> Components {
            get { return _components; }
        }

        public override string ToString() {
            return Components.OrderBy(c => c.Type).Select(c => c.Text).ToArray().Join();
        }

        public Maybe<string> Given {get { return _components.FirstMaybe(c => c.Type == NameType.Given).Convert(c => c.Text); }}
        public Maybe<string> Family {get { return _components.FirstMaybe(c => c.Type == NameType.Family).Convert(c => c.Text); }}
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
        Rank,   // King
        Given,   // William
        Middle,
        Family,  // Normandy
        Primary,                
        Title, // I of England
    }

}