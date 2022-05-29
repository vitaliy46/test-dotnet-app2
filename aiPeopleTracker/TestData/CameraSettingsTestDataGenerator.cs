using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using AutoMapper;
using LinqToExcel;
using Unity;

namespace aiPeopleTracker.TestData
{
    class CameraSettingsTestDataGenerator
    {
        public static void Generate(IUnityContainer ioc, IMapper mapper)
        {
            var crudServuce = ioc.Resolve<ICameraSettingsCrudService>();
            var repo = ioc.Resolve<ICameraSettingsRepository>();
            var floorPlanCrudServuce = ioc.Resolve<IFloorPlanCrudService>();

            var floorPlans = floorPlanCrudServuce.GetList(null);
            var countPlans = floorPlans.Count;
            Random rm = new Random();

            if (!repo.All.Any())
            {
                var list =  Data();

                foreach (var cameraSettings in list)
                {
                    var floorPlan = floorPlans[rm.Next(0, countPlans)];

                    cameraSettings.FloorPlanId = floorPlan.Id;

                    crudServuce.Create(mapper.Map<CameraSettings>(cameraSettings));
                }
            }
        }

        private static IList<CameraSettingsDto> Data()
        {
            var excel = new ExcelQueryFactory(Path.Combine("TestData", "XlsFiles", "CameraSettings.xls"));

            var list = excel.Worksheet<CameraSettingsDto>("CameraSettings").ToList();

            return list.ToList();
        }
    }
}
