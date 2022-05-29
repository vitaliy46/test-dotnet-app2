using System.Collections.Generic;
using System.IO;
using System.Linq;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Business.Collections;
using aiPeopleTracker.Dal.Api.Dto;
using AutoMapper;
using LinqToExcel;
using Unity;

namespace aiPeopleTracker.TestData
{
    class CamerasTestDataGenerator
    {
        public static void Generate(IUnityContainer ioc, IMapper mapper)
        {
            var crudServuce = ioc.Resolve<ICameraCrudService>();

            if (!crudServuce.GetList(null).Any())
            {
                Data().ForEach(d=> crudServuce.Create(mapper.Map<Camera>(d)));
            }
        }

        private static IList<CameraDto> Data()
        {
            var excel = new ExcelQueryFactory(Path.Combine("TestData", "XlsFiles", "Cameras.xls"));

            var list = excel.Worksheet<CameraDto>("Cameras").ToList();

            return list.ToList();
        }
    }
}
