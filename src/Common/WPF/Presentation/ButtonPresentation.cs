using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common.Coding;

namespace Common.WPF.Presentation {
    public class ButtonPresentation : ICommand {
        private readonly string _header;
        private readonly BitmapImage _icon;
        private readonly IProperty<bool> _isEnabled;
        private readonly Action _action;

        public ButtonPresentation(string header, Action action, IProperty<bool> isEnabled, BitmapImage icon = null) {
            _header = header;
            _icon = icon;
            _isEnabled = isEnabled;
            _action = action;

            IsEnabled.PropertyChanged += (s, e) => CanExecuteChanged.Raise();
        }

        public ButtonPresentation(string header, Action action) 
            : this(header, action, new Property<bool>(true)) {
        }

        public string Header {
            get { return _header; }
        }

        public Image Icon {
            get {
                var image = new Image() {Source = _icon};
                return image;
            }
        }

        public IProperty<bool> IsEnabled {
            get { return _isEnabled; }
        }

        public bool CanExecute(object parameter) {
            return IsEnabled.Value;
        }

        public void Execute(object parameter) {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}