using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.Api.Repositories
{
    public interface ILayoutTemplateCameraLinkRepository : 
        IRepository<LayoutTemplateCameraLinkDto, int>, 
        ICreateSupport<LayoutTemplateCameraLinkDto>,
        IUpdateSupport<LayoutTemplateCameraLinkDto>,
        IDeleteSupport<int>
    {
    }
}
