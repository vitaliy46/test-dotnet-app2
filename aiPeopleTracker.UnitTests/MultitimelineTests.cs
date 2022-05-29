using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Business.Data;
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
    public class MultitimelineTests
    {
        public MapperConfiguration mapperConfig = MapperConfig.Create();

        private IUnityContainer ioc;

        public MultitimelineTests()
        {
            ioc = UnityConfig.Create(mapperConfig);
        }

        [TestMethod]
        public void CreateMultitimeline()
        {
            var personService = ioc.Resolve<IPersonCrudService>();
            var multitimelineBuilder = ioc.Resolve<IMultitimelineBuilder>();
            var cameraSettingsCrudService = ioc.Resolve<CameraSettingsCrudService>();


            var service = new AnaliticService(personService, multitimelineBuilder, cameraSettingsCrudService);

            var result = service.GetMultitimelineByPerson(1);

            Assert.IsTrue(result.VideoClips.Any());
        }

        [TestMethod]
        public void ChangeTrekingPositionAndAddPlayableVideoClip()
        {
            var personService = ioc.Resolve<IPersonCrudService>();
            var multitimelineBuilder = ioc.Resolve<IMultitimelineBuilder>();
            var cameraSettingsCrudService = ioc.Resolve<CameraSettingsCrudService>();

            var service = new AnaliticService(personService, multitimelineBuilder, cameraSettingsCrudService);

            var multitimeline = service.GetMultitimelineByPerson(1);

            Assert.IsFalse(multitimeline.PlayableVideoClips.Any());

           // multitimeline.Tracker.MoveTo((multitimeline.LeftEdge - multitimeline.Duration) / 2);
            Assert.IsTrue(multitimeline.PlayableVideoClips.Any());
        }

        [TestMethod]
        public void CheckBreaksAndOverlapsBetwinVideoClips()
        {
            var personService = ioc.Resolve<IPersonCrudService>();
            var multitimelineBuilder = ioc.Resolve<IMultitimelineBuilder>();
            var cameraSettingsCrudService = ioc.Resolve<CameraSettingsCrudService>();

            var service = new AnaliticService(personService, multitimelineBuilder, cameraSettingsCrudService);

            var multitimeline = service.GetMultitimelineByPerson(1);
            ( int count, List<object> cameras) breakCameras;
            breakCameras.count = 0;
            breakCameras.cameras = new List<object>();

            var overlaps = 0;

            multitimeline.VideoClips.Aggregate((prevoseClip, currentClip) =>
            {
                if (prevoseClip.BeginTime + prevoseClip.Duration < currentClip.BeginTime)
                {
                    breakCameras.count++;
                    breakCameras.cameras.Add(new
                    {
                        prevoseId = prevoseClip.Camera.Id,
                        currentId = currentClip.Camera.Id
                    });
                    Debug.WriteLine(currentClip.BeginTime - prevoseClip.BeginTime + prevoseClip.Duration );
                }

                overlaps += (prevoseClip.Camera.Id == currentClip.Camera.Id &&
                            prevoseClip.BeginTime + prevoseClip.Duration > currentClip.BeginTime) ? 1 : 0;


                return currentClip;
            });

            Assert.Equals(breakCameras.count, 0);
            
        }

        [TestMethod]
        public void Play()
        {
            var personService = ioc.Resolve<IPersonCrudService>();
            var multitimelineBuilder = ioc.Resolve<IMultitimelineBuilder>();
            var cameraSettingsCrudService = ioc.Resolve<CameraSettingsCrudService>();

            var service = new AnaliticService(personService, multitimelineBuilder, cameraSettingsCrudService);

            var multitimeline = service.GetMultitimelineByPerson(1);

            //multitimeline.Tracker.Play();
            //TODO нужно переделать
            //Assert.IsTrue(result.Any());
            //Debug.WriteLine("Результат получен");
            //foreach (var pair in result)
            //{
            //    Debug.WriteLine($"{pair.Key} - {pair.Value}");
            //}

            //Thread.Sleep(5000);

            //multitimeline.Tracker.Stop();

            //Debug.WriteLine("Тест завершился успешно");

        }

    }
}
