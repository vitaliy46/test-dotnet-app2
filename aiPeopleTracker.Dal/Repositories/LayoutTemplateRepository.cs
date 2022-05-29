using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using ATTrade.Data;

namespace aiPeopleTracker.Dal.Repositories
{
    public class LayoutTemplateRepository : RepositoryBase<LayoutTemplateDto, int>, ILayoutTemplateRepository
    {
        public LayoutTemplateRepository(AiPeopleContext connection) : base(connection)
        {
        }
    }
}
