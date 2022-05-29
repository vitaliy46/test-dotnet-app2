using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.Api.Repositories
{
    public interface ICameraSettingsRepository : 
        IRepository<CameraSettingsDto, int>, 
        ICreateSupport<CameraSettingsDto>,
        IUpdateSupport<CameraSettingsDto>,
        IDeleteSupport<int>
    {
    }
}
