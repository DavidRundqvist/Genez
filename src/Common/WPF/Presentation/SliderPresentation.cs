namespace Common.WPF.Presentation {
    public class SliderPresentation {
        private readonly Property<int> _value;
        private readonly string _header;
        private readonly Property<int> _min;
        private readonly Property<int> _max;
        private readonly Property<string> _headerProperty;
        

        public SliderPresentation(Property<int> value, int min, int max, string header) {
            _value = value;
            _header = header;
            _min = new Property<int>(min);
            _max = new Property<int>(max);           
            _headerProperty = new Property<string>(FormatHeader());
            _value.PropertyChanged += (s, e) => _headerProperty.Value = FormatHeader();
        }

        private string FormatHeader() {
            return string.Format("{0}: {1}", _header, _value.Value);
        }

        public SliderPresentation(int value, int min, int max, string header)
            : this(new Property<int>(value), min, max, header) {}


        public Property<int> Value { get { return _value; } }
        public IProperty<int> Min { get { return _min; } }
        public IProperty<int> Max { get { return _max; } }
        public IProperty<string> Header { get { return _headerProperty; }}
    }
}