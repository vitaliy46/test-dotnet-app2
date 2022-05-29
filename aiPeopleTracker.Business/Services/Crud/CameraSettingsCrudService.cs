using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using AutoMapper;
using NLog;

namespace aiPeopleTracker.Business.Services.Crud
{
    public class CameraSettingsCrudService : CrudServiceBase<CameraSettings, int, ICameraSettingsRepository, CameraSettingsDto>, ICameraSettingsCrudService
    {
        public CameraSettingsCrudService(ICameraSettingsRepository repository) : base(repository)
        {
        }
    }
}