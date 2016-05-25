using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwaijaComposite.Modules.ApplicationEngine;
using Moq;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.ApplicationEngine.ViewModels;
using Microsoft.Practices.Prism.Events;
namespace TwaijaUnitTestCases
{
    [TestClass]
    public class ApplicationEngineModuleTestCase
    {
    
        [TestMethod]
        public void InitializeApplicationEngineModuleTest()
        {
            //arrange
            Mock<IUnityContainer> mockcontroller=new Mock<IUnityContainer>();
            Mock<IEngine> mockengineproxy=new Mock<IEngine>();
            Mock<IEventAggregator> mockeventaggr = new Mock<IEventAggregator>();
            mockengineproxy.Setup(p=>p.StartEngine()).Verifiable();
            mockcontroller.Setup(p => p.Resolve(typeof(IEventAggregator), null)).Returns(mockeventaggr.Object);
            mockcontroller.Setup(p => p.RegisterType(typeof(IPictureServicesRepository), It.Is<Type>(type => typeof(IPictureServicesRepository).IsAssignableFrom(type)), null, It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember>())).Verifiable();
            mockcontroller.Setup(p => p.RegisterType(typeof(IPostMessageServiceRepository),It.Is<Type>(type=>typeof(IPostMessageServiceRepository).IsAssignableFrom(type)),null,It.IsAny<LifetimeManager>(),It.IsAny<InjectionMember>())).Verifiable();
            mockcontroller.Setup(p => p.RegisterType(null,typeof(ApplicationEngineViewModel),null,It.IsAny<LifetimeManager>())).Verifiable();
            mockcontroller.Setup(p => p.RegisterType(typeof(IEngine),typeof(Engine),null,It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember>())).Verifiable();
            mockcontroller.Setup(p => p.RegisterType(typeof(object), typeof(MainApplicationTemplateView),It.Is<string>(i => i.Contains(ViewNames.MainApplicationTemplateView)), It.IsAny<LifetimeManager>())).Verifiable();
            mockcontroller.Setup(p => p.RegisterType(typeof(IEngine),typeof(EngineProxy),"EngineProxy",It.IsAny<LifetimeManager>(), It.IsAny<InjectionMember>())).Verifiable();
            mockcontroller.Setup(p => p.Resolve(typeof(IEngine),"EngineProxy")).Returns(mockengineproxy.Object);
            
            //act
            ApplicationEngineModule module = new ApplicationEngineModule(mockcontroller.Object);
            module.Initialize();

            //assert
            mockcontroller.Verify();
            mockengineproxy.Verify();

        }
    }
}

