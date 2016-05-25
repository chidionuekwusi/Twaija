using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwaijaComposite.Modules.ApplicationEngine;
using Moq;
using Microsoft.Practices.Prism.Events;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaUnitTestCases
{
    [TestClass]
    public class EngineProxyUnitTestCase
    {
        [TestMethod]
        public void NullParameterPassedToConstructorTest()
        {
            //arrange
            Mock<ApplicationCloseEvent> mockevent = new Mock<ApplicationCloseEvent>();
            IEngine engine= new Mock<IEngine>().Object;
            IOptionFileWriterService filewriter= new Mock<IOptionFileWriterService>().Object;
            INavigationService navigationService=new Mock<INavigationService>().Object;
            mockevent.Setup(p => p.Subscribe(It.IsAny<Action<ApplicationStateProxy>>())).Verifiable();          
            Mock<IEventAggregator> eventaggregator=new Mock<IEventAggregator>();
            eventaggregator.Setup(p => p.GetEvent<ApplicationCloseEvent>());
            object view =new object();

            //act and assert
            try
            {
                var proxy = new EngineProxy(null, engine, filewriter, navigationService, eventaggregator.Object);
                throw new AssertFailedException("Failed throw exception when null view was passed");
            }
            catch (Exception)
            {

                Assert.IsTrue(true, "contructor throws exception when view object is null");
            }
            try
            {
                var proxy = new EngineProxy(view, null, filewriter, navigationService, eventaggregator.Object);
                throw new AssertFailedException("Failed throw exception when null engine was passed");
            }
            catch (Exception)
            {

                Assert.IsTrue(true, "contructor throws exception when engine object is null");
            }
            try
            {
                var proxy = new EngineProxy(view, engine, null, navigationService, eventaggregator.Object);
                throw new AssertFailedException("Failed throw exception when null ioptionwriter was passed");
            }
            catch (Exception)
            {

                Assert.IsTrue(true, "contructor throws exception when IOptionwriter object is null");
            }
            try
            {
                var proxy = new EngineProxy(view, engine, filewriter, null, eventaggregator.Object);
                throw new AssertFailedException("Failed throw exception when null INavigationService was passed");
            }
            catch (Exception)
            {

                Assert.IsTrue(true, "contructor throws exception when view object is null");
            }
            try
            {
                var proxy = new EngineProxy(null, engine, filewriter, navigationService, null);
                throw new AssertFailedException("Failed throw exception when null IEventAggregator was passed");
            }
            catch (Exception)
            {
                Assert.IsTrue(true, "contructor throws exception when IEventAggregator object is null");
            }


        }
    }
}
