using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using ATTrade.Data;

namespace aiPeopleTracker.Dal.Repositories
{
    public class PersonRepository : RepositoryBase<PersonDto, int>, IPersonRepository
    {
        public PersonRepository(AiPeopleContext connection) : base(connection)
        {
        }
    }
}
