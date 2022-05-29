using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using ATTrade.Data;

namespace aiPeopleTracker.Dal.Repositories
{
    public class FloorPlanRepository : RepositoryBase<FloorPlanDto, int>, IFloorPlanRepository
    {
        public FloorPlanRepository(AiPeopleContext connection) : base(connection)
        {
        }
    }
}
