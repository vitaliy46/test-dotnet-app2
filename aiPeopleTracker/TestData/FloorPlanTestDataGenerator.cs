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
    class FloorPlanTestDataGenerator
    {
        public static void Generate(IUnityContainer ioc, IMapper mapper)
        {
            var crudServuce = ioc.Resolve<IFloorPlanCrudService>();

            if (!crudServuce.GetList(null).Any())
            {
                Data().ForEach(d=>crudServuce.Create(mapper.Map<FloorPlan>(d)));
            }
        }

        private static IList<FloorPlanDto> Data()
        {
            var excel = new ExcelQueryFactory(Path.Combine("TestData", "XlsFiles", "FloorPlans.xls"));

            var list = excel.Worksheet<FloorPlanDto>("FloorPlans").ToList();

            return list.ToList();
        }
    }
}
