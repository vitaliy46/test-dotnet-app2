using aiPeopleTracker.Business.Api.Entity;

namespace aiPeopleTracker.Business.Api.Services.Crud
{
    public interface ILayoutTemplateCrudService : 
        IGetSingleSupport<LayoutTemplate, int>,
        IGetListSupport<LayoutTemplate>,
        ICreateSupport<LayoutTemplate>,
        IDeleteSupport<int>
    {
    }
}