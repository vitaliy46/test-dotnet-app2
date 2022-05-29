using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Business.Services.BusinessLogic;
using aiPeopleTracker.Business.Services.Crud;
using aiPeopleTracker.Dal.Api.Repositories;
using aiPeopleTracker.Dal.Repositories;
using aiPeopleTracker.ViewModels;
using aiPeopleTracker.Views;
using aiPeopleTracker.Wpf.Api;
using aiPeopleTracker.Wpf.Controls.Multitimeline;
using aiPeopleTracker.WpfComponents;
using ATTrade.Data;
using AutoMapper;
using NLog;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace aiPeopleTracker.Startup
{
    public static class UnityConfig
    {
        public static IUnityContainer Create(MapperConfiguration mapperConfig)
        {
            var container = new UnityContainer();

            container.RegisterFactory<ILogger>(uc => LogManager.GetCurrentClassLogger());
            container.RegisterFactory<IMapper>(uc => mapperConfig.CreateMapper());

            //WPF компоненты
            container.RegisterType<IScaleComponent, ScaleComponent>();

            container.RegisterType<MultitimelineObjectControl>();
            
            // Crud сервисы
            container.RegisterType<ICameraCrudService, CameraCrudService>();
            container.RegisterType<ICameraSettingsCrudService, CameraSettingsCrudService>();
            container.RegisterType<CameraSettingsCrudService, CameraSettingsCrudService>();
            container.RegisterType<IFloorPlanCrudService, FloorPlanCrudService>();
            container.RegisterType<ILayoutTemplateCameraLinkCrudService, LayoutTemplateCameraLinkCrudService>();
            container.RegisterType<ILayoutTemplateCrudService, LayoutTemplateCrudService>();
            container.RegisterType<IPersonCrudService, PersonCrudService>();
            container.RegisterType<IPersonTagCrudService, PersonTagCrudService>();

            // App сервисы
            container.RegisterType<ICameraAppService, CameraAppService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<IAnaliticService, AnaliticService>();
            //В этом бюлдере храниться статический мунтитаймлайн для исключение дуляжа
            container.RegisterType<IMultitimelineBuilder, MultitimelineBuilder>(lifetimeManager:new SingletonLifetimeManager());
            container.RegisterType<IVideoStreamAppService, VideoStreamAppService>();

            
            //Контекст данных
            container.RegisterType<AiPeopleContext, AiPeopleContext>(new ContainerControlledLifetimeManager());
            // Репозитории
            container.RegisterType<ICameraRepository, CameraRepository>();
            container.RegisterType<ICameraSettingsRepository, CameraSettingsRepository>();
            container.RegisterType<IFloorPlanRepository, FloorPlanRepository>();
            container.RegisterType<ILayoutTemplateCameraLinkRepository, LayoutTemplateCameraLinkRepository>();
            container.RegisterType<ILayoutTemplateRepository, LayoutTemplateRepository>();
            container.RegisterType<IPersonRepository, PersonRepository>();
            container.RegisterType<IPersonTagRepository, PersonTagRepository>();

            container.RegisterType<LayoutTemplatesListViewModel>();
            container.RegisterType<LayoutTemplatesListView>();
            container.RegisterType<LayoutTemplateViewModel>();
            container.RegisterType<LayoutTemplateView>();
            container.RegisterType<CameraViewModel>();
            container.RegisterType<CameraView>();
            container.RegisterType<RecognizedPersonView>();
            container.RegisterType<RecognizedPersonViewModel>();
            container.RegisterType<RecognizedObjectView>();
            container.RegisterType<RecognizedObjectViewModel>();

            return container;
        }
    }
}