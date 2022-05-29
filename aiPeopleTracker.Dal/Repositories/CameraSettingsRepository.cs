using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using ATTrade.Data;

namespace aiPeopleTracker.Dal.Repositories
{
    public class CameraSettingsRepository : RepositoryBase<CameraSettingsDto, int>, ICameraSettingsRepository
    {
        public CameraSettingsRepository(AiPeopleContext connection) : base(connection)
        {
        }
    }
}
