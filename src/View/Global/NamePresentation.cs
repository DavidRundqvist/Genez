using System;
using System.Runtime.InteropServices;
using System.Text;
using Model;
using Model.PersonInformation;

namespace View.Global {
    public class NamePresentation {
        private readonly Name _name;
        public NamePresentation(Name name) {
            _name = name;
        }


        public override string ToString() {
            return _name.TheName.ToString() + (_name.IsReliable ? "" : " (maybe)");
        }
      
    }
}