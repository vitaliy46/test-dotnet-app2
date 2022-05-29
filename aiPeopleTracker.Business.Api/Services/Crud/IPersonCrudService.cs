using aiPeopleTracker.Business.Api.Entity;

namespace aiPeopleTracker.Business.Api.Services.Crud
{
    
    public interface IPersonCrudService : 
        IGetSingleSupport<Person, int>, 
        IGetListSupport<Person>,
        ICreateSupport<Person>
    {
        Person AddTag(int personId, int tagId);

        Person DeleteTag(int personId, int tagId);
    }
}