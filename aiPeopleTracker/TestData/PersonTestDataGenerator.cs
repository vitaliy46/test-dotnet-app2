using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Business.Collections;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using AutoMapper;
using LinqToExcel;
using Unity;

namespace aiPeopleTracker.TestData
{
    class PersonTestDataGenerator
    {
        public static void Generate(IUnityContainer ioc, IMapper mapper)
        {
            var crudService = ioc.Resolve<IPersonCrudService>();
            var tagRepo = ioc.Resolve<IPersonTagRepository>();

            var tags = tagRepo.All.ToList();

            Random rd = new Random();

            if (!crudService.GetList(null).Any())
            {
                Data(ioc).ForEach(d=>
                {
                    var person = mapper.Map<Person>(d);

                    person = crudService.Create(person);
                    
                    crudService.AddTag(person.Id, tags[rd.Next(0, tags.Count)].Id);

                    crudService.AddTag(person.Id, tags[rd.Next(0, tags.Count)].Id);
                });
            }
        }

        private static IList<PersonDto> Data(IUnityContainer ioc)
        {
            var fileService = ioc.Resolve<IFileService>();
           
            var excel = new ExcelQueryFactory(Path.Combine("TestData", "XlsFiles", "Persons.xls"));

            var list = excel.Worksheet<PersonDto>("Persons").ToList();
            
            list = list.Select((p,i) =>
            {
                p.Photo = fileService.ReadFile(Path.Combine("TestData", "Photos",$"{++i}.jpg"));
               
                return p;
            }).ToList();

            return list;
        }
       
    }
  
}
