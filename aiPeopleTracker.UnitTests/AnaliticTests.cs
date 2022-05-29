using System;
using System.Linq;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Business.Services.BusinessLogic;
using aiPeopleTracker.Business.Services.Crud;
using aiPeopleTracker.Core.Common;
using aiPeopleTracker.Startup;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Unity;

namespace aiPeopleTracker.UnitTests
{
    [TestClass]
    public class AnaliticTests
    {
        public MapperConfiguration mapperConfig = MapperConfig.Create();

        private IUnityContainer ioc;

        public AnaliticTests()
        {
            ioc =  UnityConfig.Create(mapperConfig);
        }

        [TestMethod]
        public void RecognizedPiople()
        {
            var multitimelineBuilder = ioc.Resolve<IMultitimelineBuilder>();
            var cameraSettingsCrudService = ioc.Resolve<CameraSettingsCrudService>();

            IPersonCrudService personService = 
                Mock.Of<IPersonCrudService>(s=>
                s.GetList(null)== new SortableObservableCollection<Person>()
                {
                    new Person(){Id = 1,Name = "Иван"},
                    new Person(){Id = 1,Name = "Марья"}
                } );

            var service = new AnaliticService(personService, multitimelineBuilder, cameraSettingsCrudService);

            var result = service.GetRecognizedPersons(1, new byte[20]);

            Assert.IsTrue(result.RecognizedPeople.Any());
        }
    }
}
