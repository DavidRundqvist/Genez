using Common.WPF.Presentation;
using Model.PersonInformation;

namespace View.Global {
    public class InformationPresentation {
        private readonly Property<string> _header;
        private readonly Property<string> _value;

        public InformationPresentation(Information information) {
            _header = new Property<string>(information.InformationType.Name);
            _value = new Property<string>(GetValue(information));
        }

        private static string GetValue(Information information) {
            return information.Value.GetValueOrDefault("Unknown");
        }


        public Property<string> Header {
            get { return _header; }
        }

        public Property<string> Value {
            get { return _value; }
        }
    }
}