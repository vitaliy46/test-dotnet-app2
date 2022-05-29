using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using ATTrade.Data;

namespace aiPeopleTracker.Dal.Repositories
{
    public class CameraRepository : RepositoryBase<CameraDto, int>, ICameraRepository
    {
        public CameraRepository(AiPeopleContext connection) : base(connection)
        {
        }
    }
}
