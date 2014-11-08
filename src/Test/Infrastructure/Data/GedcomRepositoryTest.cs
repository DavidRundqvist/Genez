using System;
using System.IO;
using System.Linq;
using System.Text;
using Infrastructure.Data;
using Model;
using NUnit.Framework;
using Moq;

namespace Test.Infrastructure.Data {
    [TestFixture]
    public class GedcomRepositoryTest {


        [Test]
        public void Should_load_people_from_gedcom() {
            // arrange
            var sut = GedcomRepository.Parse(new FileInfo(@"TestData\2009-01-04.ged"));

            // act
            var result = sut.GetAllPeople();

            // assert
            var david = result.FirstOrDefault(p => p.Names.Any(name => name.ToString() == "David Rundqvist"));
            Assert.IsNotNull(david);
        }
    }
}