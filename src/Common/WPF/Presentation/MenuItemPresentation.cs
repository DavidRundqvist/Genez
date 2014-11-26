using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common.Enumerable;

namespace Common.WPF.Presentation {
    public class MenuItemPresentation : ButtonPresentation {
        private readonly ObservableCollection<MenuItemPresentation> _children = new ObservableCollection<MenuItemPresentation>();

        // Leaves
        public MenuItemPresentation(string header, Action action, IProperty<bool> isEnabled, BitmapImage icon = null) : base(header, action, isEnabled, icon) {}

        public MenuItemPresentation(string header, Action action, string iconSource)
            : this(header, action, new Property<bool>(true), new BitmapImage(new Uri(iconSource, UriKind.Relative))) {}    
            
        public MenuItemPresentation(string header, Action action) : base(header, action) {}

        // Tree nodes
        public MenuItemPresentation(string header, IProperty<bool> isEnabled, BitmapImage icon, IEnumerable<MenuItemPresentation> children)
            : base(header, () => { }, isEnabled, icon) {
            _children.AddRange(children);            
        }

        public MenuItemPresentation(string header, IProperty<bool> isEnabled, IEnumerable<MenuItemPresentation> children) 
            : this(header, isEnabled, null, children) {}

        public MenuItemPresentation(string header, IEnumerable<MenuItemPresentation> children) 
            : this(header, new Property<bool>(true), children){}
        

        public IList<MenuItemPresentation> Children { get { return _children; } }
    }
}