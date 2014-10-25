using System.Collections.ObjectModel;
using System.Text;
using Common.Enumerable;
using NUnit.Framework;
using Moq;

namespace Test.Common {
    [TestFixture]
    public class EventCollectionTest {

        private IEventCollection<string> _sut;

        [SetUp]
        public void Setup() {
            _sut = new EventCollection<string>();
        }

        [TearDown]
        public void TearDown() {}

        [Test]
        public void Should_raise_events_when_adding() {
            // arrange
            var strings = new[] {"hej", "tjo"};
            CollectionEventArgs<string> raisedEvent = null;
            _sut.CollectionChanged += (s, e) => raisedEvent = e;

            // act
            _sut.Add(strings);

            // assert
            AssertExtensions.AssertSequences(strings, raisedEvent.Added);
        }

        [Test]
        public void Should_raise_events_when_removing() {
            // arrange
            var strings = new[] {"hej", "tjo"};
            CollectionEventArgs<string> raisedEvent = null;
            _sut.CollectionChanged += (s, e) => raisedEvent = e;

            // act
            _sut.Add(strings);
            _sut.Remove(strings);

            // assert
            AssertExtensions.AssertSequences(strings, raisedEvent.Removed);
        }

        [Test]
        public void Should_raise_events_when_replacing() {
            // arrange
            var firstItems = new[] {"hej", "tjo"};
            var secondItems = new[] {"hej då", "tjo fräs", "god natt"};
            CollectionEventArgs<string> raisedEvent = null;
            _sut.CollectionChanged += (s, e) => raisedEvent = e;

            // act
            _sut.Add(firstItems);
            _sut.Replace(firstItems, secondItems);

            // assert
            AssertExtensions.AssertSequences(firstItems, raisedEvent.Removed);
            AssertExtensions.AssertSequences(secondItems, raisedEvent.Added);
        }
    }
}