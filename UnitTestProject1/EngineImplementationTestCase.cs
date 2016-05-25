using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Prism.Regions;
using TwaijaComposite.Modules.ApplicationEngine;
using System.Collections.Generic;

namespace TwaijaUnitTestCases
{
    [TestClass]
    public class EngineImplementationTestCase
    {
           Mock<IController> mockcontroller=null;
           Mock<IController> mockcontroller2 = null;
           Mock<IRegionManager> mockregionmanager=null;
           Mock<IRegionCollection> mockregioncollection=null;
           Mock<IRegion> mockregion = null;
        
           public Engine CreateEngine()
           {
              return  new Engine(new IController[] { mockcontroller.Object,mockcontroller2.Object }, mockregionmanager.Object);
           }
        [TestInitialize]
        public void ContructMocks()
        {
            //arrange
            mockcontroller = new Mock<IController>();
            mockcontroller2 = new Mock<IController>();
            mockregionmanager = new Mock<IRegionManager>();
            mockregioncollection = new Mock<IRegionCollection>();
            mockregion = new Mock<IRegion>();
            mockcontroller.Setup(p => p.Initialize()).Verifiable();
            mockcontroller.Setup(p => p.Initialize()).Verifiable();
            mockregionmanager.Setup(p => p.Regions).Returns(mockregioncollection.Object);
            mockregioncollection.Setup(p => p[RegionNames.MainSpace]).Returns(mockregion.Object);
            mockregion.Setup(p => p.Add(It.IsAny<object>()));
        }
        [TestCleanup]
        public void DeconstructMocks()
        {
           
        }
        [TestMethod]
        public void EngineConstructorTest()
        {
           
            //act
            var engine = CreateEngine();
            //assert
            Assert.IsNotNull(engine);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullRegionManagerPassedToTheEngineConstructorTest()
        {
            //act
            var engine = new Engine(new IController[] { mockcontroller.Object }, null);
        }

        [TestMethod]
        public void StartEngineTest()
        {
            //act
            var engine = CreateEngine();
            engine.StartEngine();
        }
        [TestMethod]
        public void ActivateComponentsTest()
        {
            //arrange

            //act
            var engine = CreateEngine();
            engine.ActivateComponents();
            //assert
            mockcontroller.Verify(p=> p.Initialize(),Times.Once());
            mockcontroller2.Verify(p => p.Initialize(), Times.Once());
        }
        [TestMethod]
        public void ActivateMainViewTest()
        {
            //arrange
            List<object> views=new List<object>();
            Mock<IViewsCollection> mockviewscollection=new Mock<IViewsCollection>();      
            mockviewscollection.Setup(p=>p.Contains(It.IsAny<object>())).Returns<object>((s)=>views.Contains(s));
            mockregion.Setup(p => p.Activate(It.IsAny<object>())).Verifiable();
            mockregion.Setup(p => p.Add(It.IsAny<object>())).Callback<object>(s => { views.Add(s); });
            mockregion.Setup(p => p.Views).Returns(mockviewscollection.Object);
            //act
            var engine =CreateEngine();
            var view = new object();
            engine.ActivateMainView(view);
            engine.ActivateMainView(view);
            //assert
            mockregionmanager.Verify();
            //this verifies if the same view is already activated it should be added only once
            mockregion.Verify(p => p.Add(It.IsAny<object>()), Times.Once());
            mockregion.Verify(p => p.Activate(It.IsAny<object>()));
        }
        [TestMethod]
        public void DeactivateMainViewTest()
        {
            //arrange
            mockregion.Setup(p => p.Deactivate(It.IsAny<object>())).Verifiable();
            //act
            var engine = CreateEngine();
            engine.DeactivateMainView(new object());
            //assert
            mockregionmanager.Verify();
            mockregion.Verify(p => p.Deactivate(It.IsAny<object>()));
        }
    }
}
