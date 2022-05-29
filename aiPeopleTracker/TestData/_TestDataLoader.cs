using System.Diagnostics;
using AutoMapper;
using Unity;

namespace aiPeopleTracker.TestData
{
    class TestDataLoader
    {
        public static void Load(IUnityContainer ioc )
        {
            var mapper = ioc.Resolve<IMapper>();

            CamerasTestDataGenerator.Generate(ioc, mapper);
            FloorPlanTestDataGenerator.Generate(ioc, mapper);
            CameraSettingsTestDataGenerator.Generate(ioc, mapper);
            LayoutTemplatesTestDataGenerator.Generate(ioc, mapper);
            LayoutTemplatesCameraLinkTestDataGenerator.Generate(ioc, mapper);
            PersonTagsTestDataGenerator.Generate(ioc);
            PersonTestDataGenerator.Generate(ioc, mapper);
            

            Debug.WriteLine("Тестовые данные успешно загружены в БД");
        }
    }
}
