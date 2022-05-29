using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.Api.Repositories
{
    public interface IPersonTagRepository : 
        IRepository<PersonTagDto, int>, 
        ICreateSupport<PersonTagDto>,
        IUpdateSupport<PersonTagDto>,
        IDeleteSupport<int>
    {
    }
}
