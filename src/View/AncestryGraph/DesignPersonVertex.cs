using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace View.AncestryGraph {
    public class DesignPersonVertex : PersonVertex{
        public DesignPersonVertex() : base(new DesignPersonRegistry().First(), true) {}
    }
}