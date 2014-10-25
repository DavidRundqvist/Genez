using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Test.Launch {
    [TestFixture]
    public class ContainerTest {

        private IUnityContainer _sut;

        [SetUp]
        public void Setup() {
            _sut = global::Launch.Container.CreateContainer();
        }

        [TearDown]
        public void TearDown() {
            _sut.Dispose();
        }

        [Test, STAThread]
        public void Should_resolve_application() {
            // arrange


            // act
            _sut.Resolve<Application>();

            // assert

        }
    }
}