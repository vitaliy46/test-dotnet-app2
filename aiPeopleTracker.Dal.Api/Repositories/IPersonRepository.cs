using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.Api.Repositories
{
    public interface IPersonRepository : 
        IRepository<PersonDto, int>, 
        ICreateSupport<PersonDto>,
        IUpdateSupport<PersonDto>,
        IDeleteSupport<int>
    {
    }
}
