using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Coding {
    public class Id<T> {
        private readonly Guid _backingId;
        public Id(Guid backingId) {
            _backingId = backingId;
        }

        public Guid Guid {
            get { return _backingId; }
        }
    }
}