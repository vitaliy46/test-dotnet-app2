using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Dal.Api.Dto;
using AutoMapper;

namespace aiPeopleTracker.Startup
{
    public static class MapperConfig
    {
        public static MapperConfiguration Create()
        {
            return new MapperConfiguration(cfg => {
                cfg.CreateMap<LayoutTemplate, LayoutTemplateDto>().ReverseMap();

                cfg.CreateMap<FloorPlan, FloorPlanDto>().ReverseMap();

                cfg.CreateMap<Person, PersonDto>().ReverseMap();

                cfg.CreateMap<PersonTag, PersonTagDto>().ReverseMap();

                cfg.CreateMap<Camera, CameraDto>();
                   // .ForMember(dest => dest.State, opt => opt.MapFrom(src => (int)src.State));

                cfg.CreateMap<CameraDto, Camera>();
                  //  .ForMember(dest => dest.State, opt => opt.MapFrom(src => (CameraState)src.State));

                cfg.CreateMap<LayoutTemplateCameraLink, LayoutTemplateCameraLinkDto>().ReverseMap();

                cfg.CreateMap<CameraSettings, CameraSettingsDto>().ReverseMap();
            });
        }
    }
}