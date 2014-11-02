using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Infrastructure.Persistence {
    public class XmlSerializer : ISerializer {
        private readonly System.Xml.Serialization.XmlSerializer _backing = new System.Xml.Serialization.XmlSerializer(typeof(PersonDTO));

        public void Serialize(Stream target, PersonDTO dto) {
            _backing.Serialize(target, dto);
        }
    }
}