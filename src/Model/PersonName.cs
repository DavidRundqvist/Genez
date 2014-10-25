using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Model.Coding;

namespace Model {

    [ValueObject]
    public class PersonName {
        private readonly ReadOnlyCollection<NameComponent> _components;
        public PersonName(IEnumerable<NameComponent> components) {
            _components = components.ToList().AsReadOnly();
        }

        public override string ToString() {
            return _components.OrderBy(c => c.Type).Select(c => c.Text).ToArray().Join();
        }
    }

    [ValueObject]
    public class NameComponent {
        public readonly string Text;
        public readonly NameType Type;
        public NameComponent(string text, NameType type) {
            Text = text;
            Type = type;
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