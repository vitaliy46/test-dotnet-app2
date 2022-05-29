using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using ATTrade.Data;

namespace aiPeopleTracker.Dal.Repositories
{
    public class LayoutTemplateCameraLinkRepository : RepositoryBase<LayoutTemplateCameraLinkDto, int>, ILayoutTemplateCameraLinkRepository
    {
        public LayoutTemplateCameraLinkRepository(AiPeopleContext connection) : base(connection)
        {
        }
    }
}
