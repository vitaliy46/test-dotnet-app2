using aiPeopleTracker.Business.Api.Entity;

namespace aiPeopleTracker.Business.Api.Services.Crud
{
    
    public interface IPersonTagCrudService : 
        ICreateSupport<PersonTag>,
        IUpdateSupport<PersonTag>,
        IGetSingleSupport<PersonTag, int>,
        IGetListSupport<PersonTag>
    {
    }
}