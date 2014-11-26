using System;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Test.Application {
    [TestFixture]
    public class ContainerTest {

        private IUnityContainer _sut;

        [SetUp]
        public void Setup() {
            _sut = global::Application.Container.CreateContainer();
        }

        [TearDown]
        public void TearDown() {
            _sut.Dispose();
        }

        [Test, STAThread]
        public void Should_resolve_application() {
            // arrange


            // act
            _sut.Resolve<System.Windows.Application>();

            // assert

        }
    }
}