using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Information;
using Infrastructure.Persistence.Information.Events;
using Infrastructure.Persistence.Information.Relations;
using NUnit.Framework;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;

namespace Test.Infrastructure.Persistence {
    [TestFixture]
    public class PersonDTOTest {

        private XmlSerializer _serializer;

        [SetUp]
        public void Setup() {
            _serializer = new XmlSerializer(typeof (PersonDTO));
        }

        [TearDown]
        public void TearDown() {}

        [Test]
        public void Should_serialize_test_person() {
            // arrange
            var p = new PersonDTO();
            p.Information = new List<InformationDTO>() {
                                                           new BirthDTO(),
                                                           new DeathDTO(),
                                                           new NameDTO(),
                                                           new FatherDTO()
                                                       };

            // act
            var writer = new StringWriter();
            _serializer.Serialize(writer, p);

            var resultString = writer.ToString();
            var reader = new StringReader(resultString);            
            var result = _serializer.Deserialize(reader);

            // assert

        }
    }
}