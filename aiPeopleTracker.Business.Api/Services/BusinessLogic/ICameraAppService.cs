using aiPeopleTracker.Business.Api.Data;

namespace aiPeopleTracker.Business.Api.Services.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICameraAppService
    {
        CamerasByStates GetCamerasCountByStates();

       
    }
}