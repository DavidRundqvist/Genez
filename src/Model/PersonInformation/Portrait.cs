using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Coding;

namespace Model.PersonInformation {
    
    public class Portrait : Information{
        private readonly FileName _imageFile ;
        public Portrait(FileName imageFile, Source source, Reliability reliability = Reliability.Reliable) : base(source, reliability) {
            _imageFile = imageFile;
        }

        public override Maybe<string> Value {
            get { return ImageFile.ToString(); }
        }

        public FileName ImageFile {
            get { return _imageFile; }
        }

        public override T Accept<T>(IInformationVisitor<T> visitor) {
            return visitor.Visit(this);
        }
    }
}