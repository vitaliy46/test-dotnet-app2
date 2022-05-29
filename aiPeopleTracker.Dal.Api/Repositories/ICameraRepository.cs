using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.Api.Repositories
{
    public interface ICameraRepository : 
        IRepository<CameraDto, int>, 
        ICreateSupport<CameraDto>,
        IUpdateSupport<CameraDto>,
        IDeleteSupport<int>
    {
    }
}
