﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Global;

namespace View.AncestryGraph {
    /// <summary>
    /// Interaction logic for PersonNodeView.xaml
    /// </summary>
    public partial class PersonNodeView : UserControl {
        public PersonNodeView() {
            InitializeComponent();
        }

        public PersonPresentation Presentation { get { return DataContext as PersonPresentation; } }
    }
}
