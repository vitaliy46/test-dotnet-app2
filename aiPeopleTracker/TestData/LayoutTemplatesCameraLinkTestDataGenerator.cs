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
    class LayoutTemplatesCameraLinkTestDataGenerator
    {
        public static void Generate(IUnityContainer ioc, IMapper mapper)
        {
            var crudServuce = ioc.Resolve<ILayoutTemplateCameraLinkCrudService>();

            if (!crudServuce.GetList(null).Any())
            {
                Data().ForEach(d=> crudServuce.Create(mapper.Map<LayoutTemplateCameraLink>(d)));
            }
        }

        private static IList<LayoutTemplateCameraLinkDto> Data()
        {
            var excel = new ExcelQueryFactory(Path.Combine("TestData", "XlsFiles", "LayoutTemplateCameraLink.xls"));

            var list = excel.Worksheet<LayoutTemplateCameraLinkDto>("LayoutTemplateCameraLink").ToList();

            return list.ToList();
        }
    }
}
