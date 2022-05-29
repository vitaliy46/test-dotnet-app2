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
    class LayoutTemplatesTestDataGenerator
    {
        public static void Generate(IUnityContainer ioc, IMapper mapper)
        {
            var crudServuce = ioc.Resolve<ILayoutTemplateCrudService>();

            if (!crudServuce.GetList(null).Any())
            {
                Data().ForEach(d=> crudServuce.Create(mapper.Map<LayoutTemplate>(d)));
            }
        }

        private static IList<LayoutTemplateDto> Data()
        {
            var excel = new ExcelQueryFactory(Path.Combine("TestData", "XlsFiles", "LayoutTemplates.xls"));

            var list = excel.Worksheet<LayoutTemplateDto>("LayoutTemplates").ToList();

            return list.ToList();
        }
    }
}
