using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Coding {
    public static class EventExtensions {
        public static void Raise(this EventHandler self, EventArgs args) {
            if (self != null)
                self(self, args);
        }

        public static void Raise(this EventHandler self) {
            self.Raise(EventArgs.Empty);
        }
    }
}