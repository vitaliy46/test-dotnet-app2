using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.Api.Repositories
{
    public interface ILayoutTemplateRepository : 
        IRepository<LayoutTemplateDto, int>, 
        ICreateSupport<LayoutTemplateDto>,
        IUpdateSupport<LayoutTemplateDto>,
        IDeleteSupport<int>
    {
    }
}
