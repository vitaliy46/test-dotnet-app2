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
using AutoMapper;
using LinqToExcel;
using Unity;

namespace aiPeopleTracker.TestData
{
    class PersonTagsTestDataGenerator
    {
        public static void Generate(IUnityContainer ioc)
        {
            var crudService = ioc.Resolve<IPersonTagCrudService>();

            var tags = new[] {"Сотрудник компании", "Не распознан", "Гость", "Преступник"};

            if (!crudService.GetList(null).Any())
            {
                tags.ForEach(d=> crudService.Create( new PersonTag{Name =d }));
            }
        }

    }
  
}
