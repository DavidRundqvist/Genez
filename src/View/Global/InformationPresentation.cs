using System;
using Common.WPF.Presentation;
using Model;
using Model.PersonInformation;

namespace View.Global {
    public class InformationPresentation : IEquatable<InformationPresentation> {
        private readonly Information _information;
        private readonly Property<string> _header;
        private readonly Property<string> _value;
        private readonly Property<string> _reliable;
        private readonly Property<string> _reliability;
        private readonly Property<bool> _isSelected;

        public InformationPresentation(Information information) {
            _information = information;
            _header = new Property<string>(information.InformationType.Name);
            _value = new Property<string>(information.Value.GetValueOrDefault("Unknown"));
            _reliable = new Property<string>(information.Reliability == Model.Reliability.Reliable ? "" : "(Maybe)");
            _reliability = new Property<string>(information.Reliability.ToString());
            _isSelected = new Property<bool>(false);
        }

        public Property<string> Header { get { return _header; }}
        public Property<string> Value {get { return _value; }}
        public Property<string> Reliable {get { return _reliable; }}
        public Property<string> Reliability {get { return _reliability; }}
        public Property<bool> IsSelected {get { return _isSelected; }}
        public Information Information {get { return _information; }}

        public bool Equals(InformationPresentation other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value.Equals(other._value) && _reliability.Equals(other._reliability);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((InformationPresentation) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (_value.GetHashCode()*397) ^ _reliability.GetHashCode();
            }
        }
    }
}