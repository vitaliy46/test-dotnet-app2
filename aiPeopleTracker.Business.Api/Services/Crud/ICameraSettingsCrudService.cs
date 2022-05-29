using aiPeopleTracker.Business.Api.Entity;

namespace aiPeopleTracker.Business.Api.Services.Crud
{
    public interface ICameraSettingsCrudService :
        IGetSingleSupport<CameraSettings, int>,
        ICreateSupport<CameraSettings>,
        IUpdateSupport<CameraSettings>
       
    {
       
    }
}