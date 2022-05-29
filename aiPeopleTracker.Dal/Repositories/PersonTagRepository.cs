using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using ATTrade.Data;

namespace aiPeopleTracker.Dal.Repositories
{
    public class PersonTagRepository : RepositoryBase<PersonTagDto, int>, IPersonTagRepository
    {
        public PersonTagRepository(AiPeopleContext connection) : base(connection)
        {
        }
    }
}
