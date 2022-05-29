using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.Api.Repositories
{
    public interface IFloorPlanRepository : 
        IRepository<FloorPlanDto, int>, 
        ICreateSupport<FloorPlanDto>,
        IUpdateSupport<FloorPlanDto>,
        IDeleteSupport<int>
    {
    }
}
